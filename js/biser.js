var Biser= function(){
	var that = this; //ECMAScript 3/5 compatibility: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/Arrow_functions	
	this.RootBiser=this;	
	this.ear = [];	//main array filled after serialization and used for deserialization
	this.earPos = -1;
	this.decPos = -1;
	this.qb = 0; //quantity of bytes, necessary to create latest varInt (used in getdigit)
	this.IsNull = function() //e.g checks following object
	{		
		return this.GetDigit() == 1; //1 - null, 0 - object will follow
	};
	this.ELong = function(value) //Max Range is -999999999999999  -  999999999999999, that is far away from C# (9223372036854775807)
	{
		this.GetVarintBytes(this.EncodeZigZag(value, 64));
		return this;
	};
	this.DLong = function() 
	{		
		return this.DecodeZigZag(this.GetDigit());
	};
	this.ELong = function(value) 
	{
		this.GetVarintBytes(this.EncodeZigZag(value, 64));
		return this;
	};
	this.EBool = function(value) 
	{		
		this.GetVarintBytes((value == true) ? 1 : 0);
		return this;
	};	
	this.DBool = function() 
	{		
		return this.GetDigit() == 1;
	};	
	this.EDate = function(value) 
	{//ms since 1970		
		that.ELong(value.getTime());			
		return this;
	};
	this.DDate = function() 
	{//restores from ms since 1970
		return new Date(that.DLong());
	};	
	this.EDouble = function(value) 
	{
		//Doubles (2^53, maximal precision is 15 digit with or without comma)
		var buffer = new ArrayBuffer(8);
		var dbl = new Float64Array(buffer);
		dbl[0] = value;
		var marr1 =  new Int8Array(buffer);			
		this.RootBiser.ear[++this.RootBiser.earPos]= (IsBigEndian() == true ? 1 : 0); //1 when BigEndian architecture
		for (var i = 0; i < marr1.length; i++)
			this.RootBiser.ear[++this.RootBiser.earPos]= marr1[i];
		return this;
	};
	this.DDouble = function() 
	{
		var buffer = new ArrayBuffer(8);		
		var marr1 =  new Int8Array(buffer);
		if(this.ReadNext() == 0){  //Little-Endian
			for (var i = 0; i < marr1.length; i++)
				marr1[i] = this.ReadNext();							
		}
		else{
			for (var i = marr1.length-1; i >= 0; i--)
				marr1[i] = this.ReadNext();
		}
		var dbl = new Float64Array(buffer);
		return dbl[0];		
	};
	
	this.EString = function(value) 
	{
		var btStr=[]; //will be array of ints		
		var pos=0;
		for (var i = 0; i < value.length; i++) 
			btStr[i] = value.charCodeAt(i);
			
		this.EArray(btStr, function(x){ that.ELong(x);}	);
				
		return this;
	};
	this.DString = function() 
	{	

		var btStr = this.DArray(function(){ return that.DLong();});
		var arr1=[];
		
		for (var i = 0; i < btStr.length; i++) 
			arr1.push(String.fromCharCode(btStr[i]));
	
		return arr1.join("");	
		
	};	
	this.EUint8Array = function(value)
	{	
		that.ELong(value.length);
		if(value.length>0)
		{
			for (var i = 0; i < value.length; i++)
				this.RootBiser.ear[++this.RootBiser.earPos]= value[i];		
		}
		return this;
	};
	this.DUint8Array = function()	//use result.buffer to ArrayBuffer of it
	{
		var len = that.DLong();		
		var buffer = new ArrayBuffer(len);
		var byteArray = new Uint8Array(buffer);
		if(len>0)
		{
			for (var i = 0; i < len; i++)
				byteArray[i]= this.ReadNext();		
		}
		return byteArray;
	};
	this.EArray = function(pArr,foo) 
	{	
		if (pArr && pArr.length > 0) {			//&& (typeof foo == "function") - leaving out of check
			this.RootBiser.ear[++this.RootBiser.earPos] = 0;			
			var ip=++this.RootBiser.earPos;	//initial position
			this.RootBiser.ear[ip] = 0;
			
			for(var i=0; i<pArr.length; i++){
				foo(pArr[i]);
			}			
			var len = this.RootBiser.earPos - ip;
						
			var intLenSize = (len > 268435455) ? 5 : (len > 2097151) ? 4 : (len > 16383) ? 3 : (len > 127) ? 2 : 1;  //4 bytes max for collections and groups

			var rp=0;
			
            if (intLenSize > 1)
            {			
				var jl = intLenSize - 1;
			
				for(var j=0; j<jl; j++){
					this.RootBiser.ear[++this.RootBiser.earPos] = this.RootBiser.ear[ip+1+j];
				}
				rp = this.RootBiser.earPos;
				this.RootBiser.earPos = ip-1;
				this.GetVarintBytes(len);	
				this.RootBiser.earPos = rp;					
            }
            else
            {			
				rp = this.RootBiser.earPos;
				this.RootBiser.earPos = ip-1;
				this.GetVarintBytes(len);	
				this.RootBiser.earPos = rp;				
            }
		}
		else
		{
			this.RootBiser.ear[++this.RootBiser.earPos] = 1;
		}
		return this;
	};
	this.DArray = function(foo) 
	{
		var ret=[];
		var byteValue = this.ReadNext();				
		if (byteValue == 0) {	//&& (typeof foo == "function") - leaving out of check
			var len = this.GetDigit();			
			var ip = this.RootBiser.decPos;			
			var ri=0;			
			this.collectionShift = 0;
			var tail = this.RootBiser.qb;
			
			var activeCollectionShift = null;
			var activeCollectionLen = null;
			
			if(tail>1){	//Collection has a tail
				activeCollectionShift = this.RootBiser.collectionShift;
				activeCollectionLen = this.RootBiser.collectionLen;				
				this.RootBiser.collectionShift = this.RootBiser.qb - 1;
				this.RootBiser.collectionLen = len;				
			}
			
			while((len - (this.RootBiser.decPos - ip + (tail-1)))>0){											
				ret[ri]= foo();				
				ri++;
			}
			
			if(tail>1)
			{
				this.RootBiser.collectionShift = activeCollectionShift;
				this.RootBiser.collectionLen = activeCollectionLen;				
				this.RootBiser.decPos += tail-1;				
			}
		}
		
		return ret;		
	};
	this.collectionShift = 0;	
	this.collectionLen = 0;
	this.ReadNext = function()
	{
		var byteValue = 0;	
		if(this.RootBiser.collectionShift>0){
			byteValue = this.RootBiser.ear[this.RootBiser.decPos + this.RootBiser.collectionLen + 1 - this.RootBiser.collectionShift];
			this.RootBiser.collectionShift--;				
		}
		else
			byteValue = this.RootBiser.ear[++this.RootBiser.decPos];
			
		return byteValue;
	};
	longShiftLeft= function(value, shiftBits)
	{
		var shift = Math.pow(2, shiftBits);
		return Math.floor(value * shift);		
	};	
	longShiftRight= function(value, shiftBits)
	{
		
		var shift = Math.pow(2, shiftBits);
		return Math.floor(value / shift);		
	};	
	IsBigEndian = function ()
	{ 
		return (new Uint8Array(new Uint32Array([0x12345678]).buffer))[0] === 0x12; 
	};
	var hi=0x80000000;
	var low = 0x7fffffff;
	longAnd= function(v1,v2)
	{
		var hi1 = ~~(v1 / hi);
		var low1 = v1 & low;		
		var hi2 = ~~(v2 / hi);    
		var low2 = v2 & low;
		var h = hi1 & hi2;
		var l = low1 & low2;	
		return Math.floor(h*hi + l);		
	};	
	longOr= function(v1,v2)
	{
		var hi1 = ~~(v1 / hi);
		var low1 = v1 & low;		
		var hi2 = ~~(v2 / hi);    
		var low2 = v2 & low;
		var h = hi1 | hi2;
		var l = low1 | low2;	
		return Math.floor(h*hi + l);
	};
	longXOr= function(v1,v2)
	{
		var hi1 = ~~(v1 / hi);
		var low1 = v1 & low;		
		var hi2 = ~~(v2 / hi);    
		var low2 = v2 & low;
		var h = hi1 ^ hi2;
		var l = low1 ^ low2;	
		return Math.floor(h*hi + l);
	};
	this.EncodeZigZag = function(value, bitLength)
	{	
		if(value>=0)
			return longShiftLeft(value,1);
		else 
			return longShiftLeft(-1 * value,1)-1;	
	};
	this.DecodeZigZag = function(value) //ulong value returns long
	{
		if (longAnd(value, 0x1) == 0x1)
			return (-1 * (longShiftRight(value, 1) + 1));
			
		return longShiftRight(value, 1);		
	};
	this.GetDigit = function() 
	{	
		var byteValue = 0;
		var result = 0;		
		var shift = 0;
		this.RootBiser.qb = 0;
		while(true)
		{
			byteValue = this.ReadNext();						
			result = longOr(result, longShiftLeft(longAnd(byteValue,0x7f),shift));						
			this.RootBiser.qb++;						
			if ((byteValue & 0x80) != 0x80)
				return result;
			shift += 7;
		}		
	};
	this.GetVarintBytes = function(value) 
	{	
		var byteVal;
		do
		{
			byteVal = longAnd(value, 0x7f);
			value = longShiftRight(value,7);
			if (value != 0)
				byteVal |= 0x80;						
			this.RootBiser.ear[++this.RootBiser.earPos] = byteVal;			
		} while (value != 0);	
	};
	GetVarintBytesInternal = function(value,array,pos) 
	{
		var byteVal;		
		do
		{
			byteVal = longAnd(value, 0x7f);
			value = longShiftRight(value,7);					
			if (value != 0)
				byteVal |= 0x80;			
			array[pos++] = byteVal;			
		} while (value != 0);
		return pos;		
	};
	this.Final = function() //for showing ear content
	{
		console.log("---");		
		for (var i = 0; i < this.RootBiser.ear.length; i++) {			
			console.log('Entry ' + i + ': ' + this.RootBiser.ear[i]);
		}		
	};
	
}