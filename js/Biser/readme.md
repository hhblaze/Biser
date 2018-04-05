# Biser

[Biser](https://github.com/hhblaze/Biser) is a binary serializer for javascript and .NET (dotnet C# VB.NET).

### Javascript. Encoding / Decoding example.

```javascript
//Object preparation
var obj1=function()
{
	this.MyString = "";
	this.MyDigit = 0;
	this.BiserEncode = function(enc)
	{
		var mb1=new Biser();
		mb1.RootBiser = enc.RootBiser;
		mb1
		.EString(this.MyString)
		.EDouble(this.MyDigit)
		;		
	};
	this.BiserDecode = function(enc)
	{
		var mb1=new Biser();
		mb1.RootBiser = enc.RootBiser;
		this.MyString = mb1.DString();
		this.MyDigit = mb1.DDouble();
		return this;		
	};
};

function Main()
{
//preparation of simple objects
	var tar = [];	
	for (var i = 0; i < 32; i++) {		
		tar[i] = -146546886;
	}	
	var tar1 = [];	
	for (var i = 0; i < 33; i++) {		
		tar1[i] = "AbcdefПривет";
	}
	
	var o1 = new obj1();
	o1.MyString = "tes12";
	o1.MyDigit = 15.47;	
	
	var tar2 = [];
	for (var i = 0; i < 3; i++) {		
		var o2 = new obj1();
		o2.MyString = "kes1244";
		o2.MyDigit = 27.789+i;
		tar2[i] = o2;
	}	
	
	var cbp = new ArrayBuffer(3);
	var ucbp= new Uint8Array(cbp);
	ucbp[0] = 18;
	ucbp[1] = 19;
	ucbp[2] = 20;
	
	
	//Encoding itself
	var mb = new Biser();
	mb
	.ELong(1521797378957)		
	.ELong(-1521797378957)		
	.ELong(-123879)	
	.ELong(12378987878)	
	.EArray(tar,function(x){mb.ELong(x);})
	.EDouble(124e-112)
	.EDouble(156.678)
	.EString("HelloПривет1")		
	.EArray(tar1,function(x){mb.EString(x);})
	.EDouble(1666.12)
	.EDate(new Date(Date.now()))
	;	
	
	o1.BiserEncode(mb); //EObject that implements BiserEncode function?
	
	mb.EArray(tar2,function(x){ x.BiserEncode(mb);});
	
	var mb1=new Biser();
	mb1.RootBiser = mb.RootBiser;
	mb1
	.ELong(12)
	;
	
	mb.EUint8Array(ucbp);
	mb.EBool(false);
	mb.EBool(true);
	mb.ELong(17);
	
	//---End of Encoding
	
	//Decoding
	console.log(mb.DLong());
	console.log(mb.DLong());
	console.log(mb.DLong());
	console.log(mb.DLong());	
	var arr = mb.DArray(function(){ return mb.DLong();});
		console.log(arr.length + " of " + arr[0]);			
	console.log(mb.DDouble());
	console.log(mb.DDouble());
	console.log(mb.DString());
	var arr1 = mb.DArray(function(){ return mb.DString();});
		console.log(arr1.length + " of " + arr1[3]);	
	console.log(mb.DDouble());
	console.log(mb.DDate());

	var o1b = (new obj1()).BiserDecode(mb);
	console.log(o1b.MyString);
	console.log(o1b.MyDigit);
	
	var arr2 = mb.DArray(function(){ return (new obj1()).BiserDecode(mb);});
		console.log(arr2.length + " of " + arr2[1].MyString + " and " + arr2[1].MyDigit);
	
	var mbD1=new Biser();
	mbD1.RootBiser = mb.RootBiser;
	console.log(mbD1.DLong());
		////console.log(mb.DLong());  //or on
		
	var uintarr = mb.DUint8Array();
	console.log(uintarr[1]);
	var mySecArray = new Uint8Array(uintarr.buffer);
	console.log(mySecArray[2]);
	
	console.log(mb.DBool());
	console.log(mb.DBool());
	console.log(mb.DLong());
	
	//---End of Decoding	
	
	//Shows the content of the encoder that can be sent to .NET (dotnet) server
	console.log(mb.ear);
	return;
```

### Javascript-C#-Javascript. Encoding / Decoding example.

```javascript
//Javascript part
function SendToDotNetServerAndReceiveAnswer()
{
	var xhr = new XMLHttpRequest();	
	xhr.open('POST', 'testBin');	
	xhr.responseType = "arraybuffer";

	xhr.onload = function() {
		if (xhr.status === 200) {
			
			var arrayBuffer = xhr.response; 
			
			  if (arrayBuffer) {
			  //Receiving from C# server
				var mb = new Biser();
				mb.ear = new Uint8Array(arrayBuffer);
				console.log(mb.DLong());
				console.log(mb.DLong());
				console.log(mb.DLong());
				console.log(mb.DLong());
				console.log(mb.DDate());
				console.log(mb.DDate());
				console.log(mb.DDouble());
				console.log(mb.DDouble());
				console.log(mb.DString());
				
				var arr1 = mb.DArray(function(){ return mb.DString();});
					console.log(arr1.length + " of " + arr1[2]);	
		
				console.log(mb.DBool());
				console.log(mb.DBool());
				
				var arr2 = mb.DArray(function(){ return (new obj1()).BiserDecode(mb);});
					console.log(arr2.length + " of " + arr2[1].MyString + " and " + arr2[1].MyDigit);
					
				var uintarr = mb.DUint8Array();
					console.log(uintarr[2]);
					
				var arr3 = mb.DArray(function(){ return mb.DUint8Array();});
					console.log(arr3.length + " of " + arr3[2][1]);
			  }
			
		}
		else {
			alert('Request failed.  Returned status of ' + xhr.status);
		}
	};
	
	//Prepare some objects to send
	var tar1 = [];	
	for (var i = 0; i < 33; i++) {		
		tar1[i] = "AbcdefПривет";
	}
	
	var cbp = new ArrayBuffer(3);
	var ucbp= new Uint8Array(cbp);
	ucbp[0] = 18;
	ucbp[1] = 250;
	ucbp[2] = 128;
	
	var tar2 = [];
	for (var i = 0; i < 3; i++) {		
		var o2 = new obj1();
		o2.MyString = "kes1244";
		o2.MyDigit = 27.789+i;
		tar2[i] = o2;
	}	
	
	//Sending to C# server
	var isend=new Biser();
		isend
		.ELong(-999999999999999)		 //Works up to .ELong(999999999999999) or (-999999999999999) ulong can take one digit more.  this "99999999999999987" returns "99999999999999984", c# 9223372036854775807 -long.max
		.ELong(125879)
		.EDouble(-17000.3287)
		.EString("abcHello привет hi")
		.ELong(-125)
		.EDate(new Date(Date.now()))
		.EArray(tar1,function(x){isend.EString(x);})	
		.EDouble(15.17)		
		.EBool(false)	
		.EBool(true)	
		.EUint8Array(ucbp)
		.EArray(tar2,function(x){ x.BiserEncode(isend);})
		;
		
	xhr.send( new Uint8Array(isend.ear));

}
```

```C#
//C# part
using Biser;

//req.PostContent - received from server
var decoder = new Biser.Decoder(req.PostContent);

Console.WriteLine(decoder.JSGetLong());
Console.WriteLine(decoder.JSGetLong());                  
Console.WriteLine(decoder.JSGetDouble());                    
Console.WriteLine(decoder.JSGetString());
Console.WriteLine(decoder.JSGetLong());
Console.WriteLine(decoder.JSGetDate().ToString("dd.MM.yyyy HH:mm:ss.ms"));

List<string> lst = decoder.CheckNull() ? null : new List<string>();
if (lst != null)
{
	decoder.GetCollection(() => { return decoder.JSGetString(); }, lst, true);
	//foreach (var item in lst)
	//    Console.WriteLine(item);
}

Console.WriteLine(decoder.JSGetDouble());
Console.WriteLine(decoder.JSGetBool());
Console.WriteLine(decoder.JSGetBool());

var abt = decoder.JSGetByteArray();

List<Tuple<string,double>> lst1 = decoder.CheckNull() ? null : new List<Tuple<string, double>>();
if (lst != null)
{
	decoder.GetCollection(() => { return new Tuple<string,double>
		(decoder.JSGetString(),decoder.JSGetDouble()); }, lst1, true);
	foreach (var item in lst1)
		Console.WriteLine(item.Item1);
}


//Sending to JS client

resp.TransferType = GccObjects.Net.Web.HttpResponseModule.eTransferType.AS_BYTE_ARRAY;

List<string> lstStr = new List<string> { "hi", "nice", "приZtrüüü" };
List<Tuple<string,double>> lstTpl = new List<Tuple<string, double>>
{
	new Tuple<string, double>("v1",12.15),
	new Tuple<string, double>("v2",14.16),
	new Tuple<string, double>("v3",17.10),
};
List<byte[]> lstBta = new List<byte[]> { new byte[] { 1,2,3}, new byte[] { 200, 170, 15 }, new byte[] { 59, 14, 150 } };

var enc = new Biser.Encoder();
enc
   .JSAdd(-999999999999999)
   .JSAdd(999999999999999)
   .JSAdd(1278)
   .JSAdd(-1278)
   .JSAdd(DateTime.Now)
   .JSAdd(DateTime.UtcNow)
   .JSAdd(-17000.3287)
   .JSAdd(15.17)
   .JSAdd("abcПр")
   .Add(lstStr, r => { enc.JSAdd(r); })
   .JSAdd(true)
   .JSAdd(false)
   .Add(lstTpl, r => { enc.JSAdd(r.Item1); enc.JSAdd(r.Item2); })
   .JSAdd(new byte[] { 8, 128 ,254})
   .Add(lstBta, r => { enc.JSAdd(r); })                       
;
resp.Result_As_ByteArray = enc.Encode();	//this should be send to the browser
```