# Biser.NET
[![Image of Build](https://img.shields.io/badge/License-BSD%203,%20FOSS-FC0574.svg)](https://github.com/hhblaze/Biser/blob/master/LICENSE)
![Image of Build](https://img.shields.io/badge/Roadmap-completed-33CC33.svg)
[![NuGet Badge](https://buildstats.info/nuget/Biser)](https://www.nuget.org/packages/Biser/)
[![Image of Build](https://img.shields.io/badge/Powered%20by-tiesky.com-1883F5.svg)](http://tiesky.com)

Binary serializer for .NET dotnet and javascript ("biser" in Russian means "beads")

- Can be an alternative to [protobuf-net](https://github.com/mgravell/protobuf-net) in some circumstances. 
- Has the same [operational speed (Benchmark)](https://github.com/hhblaze/Biser/blob/master/Benchmark/Program.cs) as protobuf-net, smaller payload and 15KB DLL size 
or +8KB to the final compiled length, when embedded as a source code.
- Has custom encoding possibilities of any complexity.
- Different encoding/decoding scenarios for one object are supported.
- Thread safe. No need to "warm up" serializing/encoding entities.
- Managed code without external references.
- Encoder.JSAdd and Decoder.JSGet make compatible byte[] for exchanging data between javascript runtime (check documentation).

Integrated part of [DBreeze database](https://github.com/hhblaze/DBreeze), used in [Raft.NET](https://github.com/hhblaze/Raft.Net)

- [Examples of encoders/decoders](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/Program.cs)
- [Documentation](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub)

-------------
### Getting started

#### From simple to complex. Encoding .NET Primitives

![dp10](https://github.com/hhblaze/Biser/blob/master/Docu/dp10.jpg?raw=true)

```C#
        static void TestListDictionary()
        {
            //Encoding
            Biser.Encoder enc = new Biser.Encoder();
            enc.Add((int)123);            
            enc.Add(new List<string> { "Hi", "there" }, r => { enc.Add(r); });
            enc.Add((float)-458.4f);

            enc.Add(new Dictionary<uint, string> { { 1, "Well" }, { 2, "done" } }
           , r => { enc.Add(r.Key); enc.Add(r.Value); });

            enc.Add((decimal)-587.7m);

            //TS4 implements IEncoder
            enc.Add(new Dictionary<uint, TS4> { { 1, new TS4 { TermId = 1 } }, { 2, new TS4 { TermId = 5 } } }
            , r => { enc.Add(r.Key); enc.Add(r.Value); });

            enc.Add(new TS4 { TermId = 188 });
            

            //Decoding

            var decoder = new Biser.Decoder(enc.Encode());

            Console.WriteLine(decoder.GetInt());


            //////Alternative to the following instruction. Slower than supplying List directly
            ////foreach (var item in decoder.GetCollection().Select(r => r.GetString()))
            ////{
            ////    Console.WriteLine(item);
            ////}

            List<string> lst = decoder.CheckNull() ? null : new List<string>();      
            if (lst != null)
            {
                decoder.GetCollection(() => { return decoder.GetString(); }, lst, true);
                foreach (var item in lst)
                    Console.WriteLine(item);
            }

            Console.WriteLine(decoder.GetFloat());


            ////////Alternative to the following instruction. Slower than supplying Dictionary directly
            //////foreach (var item in decoder.GetCollection())
            //////{
            //////    Console.WriteLine($"K: {item.GetUInt()}; V: {item.GetString()}");
            //////}

            Dictionary<uint, string> d1 = decoder.CheckNull() ? null : new Dictionary<uint, string>();
            if (d1 != null)
            {
                decoder.GetCollection(
                    () => { return decoder.GetUInt(); },
                    () => { return decoder.GetString(); },
                    d1, true);
                foreach (var item in d1)
                    Console.WriteLine(item.Key + "; " + item.Value);
            }

            Console.WriteLine(decoder.GetDecimal());

            Dictionary<uint, TS4> d2 = decoder.CheckNull() ? null : new Dictionary<uint, TS4>();
            if (d2 != null)
            {
                decoder.GetCollection(
                    () => { return decoder.GetUInt(); },
                    () => { return TS4.BiserDecode(extDecoder: decoder); },
                    d2, true);
                foreach (var item in d2)
                    Console.WriteLine(item.Key + "; " + item.Value.TermId);
            }
            
            Console.WriteLine(TS4.BiserDecode(extDecoder: decoder).TermId);

        }
```
#### Encoding custom objects

In Biser serializing and deserializing functions (encoding/decoding) are supplied together with POCO class (A Plain Old CLR Objects) as a partial class extension. 
These functions are very simple and are built up with the help of Biser existing primitives.

Use copy-paste from [here](https://github.com/hhblaze/Biser/tree/master/BiserTest_Net) / [template](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/TS4.cs) / [template](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/TS1_Biser.cs) to create the most popular encoders/decoders:

![dp1](https://github.com/hhblaze/Biser/blob/master/Docu/dp1.jpg?raw=true)


-------------
### Encoding

It’s possible to encode primitive .NET type, IEncoder (custom object) and IEnumerable (with the content of any complexity):

.NET Primitives and IEncoder can be just added into encoder.Add function.
For IEnumerable we need to define how to encode its content:

![dp2](https://github.com/hhblaze/Biser/blob/master/Docu/dp2.jpg?raw=true)

This trick gives us an ability to encode any data sequence inside of IEnumerable, e.g:

To encode complex object 

public List<Tuple<string,byte[],TS3>> P8 { get; set; }

we need to make following

.Add(P8, (r) => { enc.Add(r.Item1); enc.Add(r.Item2); enc.Add(r.Item3); })

To encode Dictionary 

public Dictionary<long,TS3> P5 { get; set; }

we need to make following

.Add(P5, (r) => { enc.Add(r.Key); enc.Add(r.Value); })

To Encode Dictionary with List as a Value

public Dictionary<long,List<TS3>> P6 { get; set; }

we need to make following

.Add(P6, (r) => { enc.Add(r.Key); enc.Add(r.Value, (r1) => { enc.Add(r1); }); })


-------------
### Decoding

Primitive types are decoded in such way [TS1 class decoder example](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/TS1_Biser.cs):

![dp3](https://github.com/hhblaze/Biser/blob/master/Docu/dp3.jpg?raw=true)

IEnumerables can have null values inside:

![dp4](https://github.com/hhblaze/Biser/blob/master/Docu/dp4.jpg?raw=true)


-------------
### Beads on a string. N-dimensional arrays and custom serialization language

![beads](https://github.com/hhblaze/Biser/blob/master/Docu/beads.png?raw=true)

Putting all values that we are interested just one after another like beads on a string, later in the same sequence we will get them back:

[Examples from here](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/Program.cs)

![dp5](https://github.com/hhblaze/Biser/blob/master/Docu/dp5.jpg?raw=true)

Deserializing values:

![dp7](https://github.com/hhblaze/Biser/blob/master/Docu/dp7.jpg?raw=true)

Custom serialization:

![dp8](https://github.com/hhblaze/Biser/blob/master/Docu/dp8.jpg?raw=true)
![dp9](https://github.com/hhblaze/Biser/blob/master/Docu/dp9.jpg?raw=true)

If the length of the collection is known in advance, it is possible to economize 1 byte for NULL representation by adding collection length equal to -1.
Integrated encoder.Add(IEnumerable) doesn’t know the length of the collection in advance and works a bit different than in this example,
effectively storing all necessary information without iterating collection twice.


-------------
### Biser extensions

There is a set of useful extensions are concentrated in [BiserExtensions.cs](https://github.com/hhblaze/Biser/blob/master/Biser/BiserExtensions.cs)

It can be copied and pasted into your project or used directly from DLL. Note that decoding extensions based on [IDecoder](https://github.com/hhblaze/Biser/blob/master/Biser/IEncoder.cs) interface for custom objects.
Also automatic creation of instances (like in Biser.BiserExtensions.BiserDecodeList example) is not always efficient as an explicit instance creation, 
though for someone it can be very handy.

[TS5 implements IEncoder and IDecoder](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/TS5.cs)

```C#
	static void TestBE1()
	{
		//Testing extensions with IDecoder and Biser.Extension interface
		    
            var ttz= ((int)15).BiserEncode();
            var btx = (new HashSet<TS5> { new TS5 { TermId = 15 }, new TS5 { TermId = 16 }, new TS5 { TermId = 17 } }).BiserEncodeList();
            var ttzD = ttz.BiserDecode<int>();
            var btxD = btx.BiserDecodeHashSet<TS5>();       
	}
```


-------------

### Biser as a crossplatform serializer. Starting from versio 1.3 can prepare byte[] readable by C# and javascript.

For .NET special IJSEncoder, IJSDecoder are prepared, also in Encoder all JSAdd function and in Decoder all JSGet
functions will prepare compatible with javascript conversions.

For javascript [Biser.js](https://www.npmjs.com/package/biser) can be taken from www.npmjs.org

Set of primitive convertible  types is limited to: long, double, string, byte[], bool, DateTime. 
And arrays containing any of these types. 
Complex types (objects classes) are possible.


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

hhblaze@gmail.com
