# Biser .NET
Binary serializer for .NET dotnet ("biser" in Russian means "beads")

- Can be an alternative to protobuf-net in some circumstances. 
- Has the same operational speed as protobuf-net, smaller payload and 15KB DLL size 
or +8KB to the final compiled length if embedded as source code.
- Has custom encoding possibilities of any complexity.
- Managed code without external references.

Integrated part of [DBreeze database](https://github.com/hhblaze/DBreeze), used in [Raft.NET](https://github.com/hhblaze/Raft.Net)

- [Examples of encoders/decoders](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/Program.cs)
- [Documentation](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub)
- [![NuGet Badge](https://buildstats.info/nuget/Biser)](https://www.nuget.org/packages/Biser/)

-------------
### Getting started

#### From simple to complex. Encoding .NET Primitives

![dp10](https://github.com/hhblaze/Biser/blob/master/Docu/dp10.jpg?raw=true)

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

If the length of the collection is known in advance, it is possible to ecomize 1 byte for NULL representation and add collection length as -1.
Integrated encoder.Add(IEnumerable) doesn’t know the length of the collection in advance and works a bit different than in this example,
effectively storing all necessary information without iterating collection twice.


hhblaze@gmail.com
