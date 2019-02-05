# Biser.NET
[![Image of Build](https://img.shields.io/badge/License-BSD%203,%20FOSS-FC0574.svg)](https://github.com/hhblaze/Biser/blob/master/LICENSE)
![Image of Build](https://img.shields.io/badge/Roadmap-completed-33CC33.svg)
[![NuGet Badge](https://buildstats.info/nuget/Biser)](https://www.nuget.org/packages/Biser/)
[![Image of Build](https://img.shields.io/badge/Powered%20by-tiesky.com-1883F5.svg)](http://tiesky.com)

Cross-platform binary serializer for .NET dotnet family and [javascript](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub#id.8cf9hq1iypk9).  
Cross-platform [JSON](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub#id.yqadcf2f2moz) serializer for .NET dotnet family.

- Doesn't use reflection inside, only managed code, perfectly works in [MONO WASM](https://github.com/aspnet/Blazor) and [CoreRT](https://github.com/dotnet/corert) where AOT compilers are used,
that makes it a possible alternative to [protobuf-net](https://github.com/mgravell/protobuf-net), [MessagePack](https://github.com/neuecc/MessagePack-CSharp),  [NetJSON](https://github.com/rpgmaker/NetJSON), [JSON.NET](https://www.newtonsoft.com/json).
From the other side, needs a bit of effort to set up transformation map for objects encoding and decoding.
- Has the same [operational speed (Benchmark)](https://github.com/hhblaze/Biser/blob/master/Benchmark/Program.cs) as protobuf-net and NetJSON, smaller payload and a tiny source code.
- Has custom encoding possibilities of any complexity.
- Different encoding/decoding scenarios for one object are supported.
- Thread safe. No need to "warm up" serializing/encoding entities.
- Experimantally serialized binary objects can be exchanged with javascript ([documentation](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub#id.8cf9hq1iypk9)).

Integrated part of [DBreeze database](https://github.com/hhblaze/DBreeze), used in [Raft.NET](https://github.com/hhblaze/Raft.Net)

Quick start

Grab from NuGet Biser (or DBreeze that contains Biser), grab from Nuget BiserObjectify.
Let’s assume you have several objects to serialize. It is necessary to prepare them. 


Call next line to create code for the serialzer:
 var resbof = BiserObjectify.Generator.Run(typeof(TS6),true, @"D:\Temp\1\", forBiserBinary: true, forBiserJson: true);

First argument is the type of the root object to be serialized (it can contain other objects that also must be serialized).
Second argument means that BiserObjectify must be prepare serializer for all objects included into the root object.
Third argument points us to the folder where C# files for the serialization of each object will be created.
The fourth and fifth arguments mean that we want to use and binary and JSON serializers

resbof variable will contain the same information that in generated files also as Dictionary.

Copy generated files into your project and link them to the project. Try to recompile. 
Probably you will need to make all objects to be serialized as partial class:


 public partial class TS6
    {
        public string P1 { get; set; }
...


Remove BiserObjectify from your project, it will not be necessary until next time.
Usage:
 TS6 t6 = new TS6()
            {
                P1 = "dsfs",
                P2 = 456,
                P3 = DateTime.UtcNow,
...
}


Binary serialization:

 var serializedObjectAsByteArray = t6.BiserEncoder().Encode();
 var retoredBinaryObject= TS6.BiserDecode(serializedObjectAsByteArray);


JSON serialization:

 var jsonSettings = new Biser.JsonSettings { DateFormat = Biser.JsonSettings.DateTimeStyle.ISO };
 string prettifiedJsonString = new Biser.JsonEncoder(t6, jsonSet).GetJSON(Biser.JsonSettings.JsonStringStyle.Prettify);
 var restoredJsonObject= TS6.BiserJsonDecode(prettifiedJsonString, settings: jsonSettings);


- [Documentation binary Biser](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub)
- [Documentation JSON Biser](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub#id.yqadcf2f2moz)

- [Examples of encoders/decoders](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net)
-------------

hhblaze@gmail.com
