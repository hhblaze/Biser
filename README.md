# Biser.NET
[![Image of Build](https://img.shields.io/badge/License-BSD%203,%20FOSS-FC0574.svg)](https://github.com/hhblaze/Biser/blob/master/LICENSE)
![Image of Build](https://img.shields.io/badge/Roadmap-completed-33CC33.svg)
[![NuGet Badge](https://buildstats.info/nuget/Biser)](https://www.nuget.org/packages/Biser/)
[![Image of Build](https://img.shields.io/badge/Powered%20by-tiesky.com-1883F5.svg)](http://tiesky.com)

Binary serializer for .NET dotnet ("biser" in Russian means "beads")

- Can be an alternative to [protobuf-net](https://github.com/mgravell/protobuf-net) in some circumstances. 
- Has the same [operational speed (Benchmark)](https://github.com/hhblaze/Biser/blob/master/Benchmark/Program.cs) as protobuf-net, smaller payload and 15KB DLL size 
or +8KB to the final compiled length, when embedded as a source code.
- Has custom encoding possibilities of any complexity.
- Different encoding/decoding scenarios for one object are supported.
- Thread safe. No need to "warm up" serializing/encoding entites.
- Managed code without external references.

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

hhblaze@gmail.com
