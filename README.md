# Biser .NET
Binary serializer for .NET dotnet ("biser" means "beads" in Russian Language)

Alternative to protobuf-net. 
With the same operational speed, smaller payload and 15KB DLL size.

Integrated part of [DBreeze database](https://github.com/hhblaze/DBreeze)

[Examples of encoders/decoders](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/Program.cs)
[Documentation](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub)

Binary serializer "biser" means "beads" in Russian Language

-------------

In Biser serializing and deserializing functions (later encoding/decoding) are supplied together with POCO class (A Plain Old CLR Objects) as partial extension. 
These functions are very simple and build up with the help of Biser existing primitives.

Use copy-paste from [here](https://github.com/hhblaze/Biser/tree/master/BiserTest_Net) to create the most popular encoders/decoders

![dp1](https://github.com/hhblaze/Biser/blob/master/Docu/dp1.jpg?raw=true)

### Encoding

It’s possible to encode primitive .NET type, IEncode and IEnumerable:

.NET Primitives and IEncode can be just added into encoder.Add function.
For IEnumerable we need to define how to encode its content:

![dp2](https://github.com/hhblaze/Biser/blob/master/Docu/dp2.jpg?raw=true)

This trick gives us ability to encode any data sequence inside of IEnumerable, e.g:

To encode complex object 
public List<Tuple<string,byte[],TS3>> P8 { get; set; }
we need to make following
.Add(P8, (r) => { enc.Add(r.Item1); enc.Add(r.Item2); enc.Add(r.Item3); })
To encode Dictionary 
public Dictionary<long,TS3> P5 { get; set; }
we need to make following
.Add(P5, (r) => { enc.Add(r.Key); enc.Add(r.Value); })

### Decoding

Primitive types are decoded in such way [TS1 class decoder example] (https://github.com/hhblaze/Biser/blob/master/BiserTest_Net/TS1_Biser.cs):

![dp3](https://github.com/hhblaze/Biser/blob/master/Docu/dp3.jpg?raw=true)

hhblaze@gmail.com
