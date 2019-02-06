# Biser.NET
[![Image of Build](https://img.shields.io/badge/License-BSD%203,%20FOSS-FC0574.svg)](https://github.com/hhblaze/Biser/blob/master/LICENSE)
![Image of Build](https://img.shields.io/badge/Roadmap-completed-33CC33.svg)
[![NuGet Badge](https://buildstats.info/nuget/Biser)](https://www.nuget.org/packages/Biser/)
[![NuGet Badge](https://buildstats.info/nuget/BiserObjectify)](https://www.nuget.org/packages/BiserObjectify/)
[![Image of Build](https://img.shields.io/badge/Powered%20by-tiesky.com-1883F5.svg)](http://tiesky.com)

Cross-platform BINARY and JSON serializer for .NET dotnet.

- Doesn't use reflection inside, only managed code, perfectly works in [MONO WASM](https://github.com/aspnet/Blazor) and [CoreRT](https://github.com/dotnet/corert) where AOT compilers are used,
that makes it a possible alternative to [protobuf-net](https://github.com/mgravell/protobuf-net), [MessagePack](https://github.com/neuecc/MessagePack-CSharp),  [NetJSON](https://github.com/rpgmaker/NetJSON), [JSON.NET](https://www.newtonsoft.com/json).
- Has the same [operational speed (Benchmark)](https://github.com/hhblaze/Biser/blob/master/Benchmark/Program.cs) as protobuf-net and NetJSON, smaller payload and a tiny source code.
- Has custom encoding possibilities of any complexity.
- Different encoding/decoding scenarios for one object are supported.
- Thread safe. No need to "warm up" serializing/encoding entities.


Integrated part of [DBreeze database](https://github.com/hhblaze/DBreeze), used in [Raft.NET](https://github.com/hhblaze/Raft.Net)

### Quick start

- Grab from NuGet **Biser** (or **DBreeze** that contains Biser), grab from Nuget **BiserObjectify**.
- Let’s assume you have several objects to serialize. It is necessary to prepare them: 

Call next line to create code for the serialzer:
```C#
 var resbof = BiserObjectify.Generator.Run(typeof(TS6),true, 
      @"D:\Temp\1\", forBiserBinary: true, forBiserJson: true);
```

First argument is the type of the root object to be serialized (it can contain other objects that also must be serialized).
Second argument means that BiserObjectify must prepare serializer for all objects included into the root object.
Third argument points to the folder where C# files per object will be created.
The fourth and fifth arguments mean that we want to use both Binary and JSON serializers

resbof variable will contain the same information that in generated files also as a Dictionary.

- Copy generated files into your project and embed/link them to the project. Try to recompile. 
- Probably, it will be necessary to add “partial” keyword to objects that must be serialized:

```C#
 public partial class TS6
    {
        public string P1 { get; set; }
...
```

- Remove BiserObjectify from your project, it will not be necessary until next time.
Usage:
```C#
 TS6 t6 = new TS6()
            {                
                P1 = "dsfs",
                P2 = 456,
                P3 = DateTime.UtcNow,
                P4 = new List<Dictionary<DateTime, Tuple<int, string>>>
                    {
                        new Dictionary<DateTime, Tuple<int, string>>{
                            { DateTime.UtcNow.AddMinutes(-1), new Tuple<int, string>(12,"testvar") },
                            { DateTime.UtcNow.AddMinutes(-2), new Tuple<int, string>(125,"testvar123") }
                        },
                        new Dictionary<DateTime, Tuple<int, string>>{
                            { DateTime.UtcNow.AddMinutes(-3), new Tuple<int, string>(17,"dsfsdtestvar") },
                            { DateTime.UtcNow.AddMinutes(-4), new Tuple<int, string>(15625,"sdfsdtestvar") }
                        }
                    },
                P5 = new Dictionary<int, Tuple<int, string>> {
                     { 12, new Tuple<int, string>(478,"dsffdf") },
                     { 178, new Tuple<int, string>(5687,"sdfsd") }
                 },
                P6 = new Tuple<int, string, Tuple<List<string>, DateTime>>(445, "dsfdfgfgfg", 
                new Tuple<List<string>, DateTime>(new List<string> { "a1", "a2" }, DateTime.Now.AddDays(58))),
                P7 = new List<string> { "fgdfgrdfg", "dfgfdgdfg" },
                P8 = new Dictionary<int, List<string>> {
                        { 34,new List<string> { "drtttz","ghhtht"} },
                        { 4534,new List<string> { "dfgfghfgz","6546ghhtht"} }
                    },

...
}

```


#### Binary serialization:
```C#
 var serializedObjectAsByteArray = t6.BiserEncoder().Encode();
 var retoredBinaryObject= TS6.BiserDecode(serializedObjectAsByteArray);
```

#### JSON serialization:
```C#
 var jsonSettings = new Biser.JsonSettings { DateFormat = Biser.JsonSettings.DateTimeStyle.ISO };
 string prettifiedJsonString = new Biser.JsonEncoder(t6, jsonSet)
            .GetJSON(Biser.JsonSettings.JsonStringStyle.Prettify);
 var restoredJsonObject= TS6.BiserJsonDecode(prettifiedJsonString, settings: jsonSettings);
```

-------------
For the deep understanding:

- [Documentation binary Biser](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub)
- [Documentation JSON Biser](https://docs.google.com/document/d/e/2PACX-1vQa3C506Esw3Fkroj4OA5erGOHEZpAtnXcQQ90R0w1wnFqO_16CH0dUfBJZt_ppB15ykoZWI9eR8KcG/pub#id.yqadcf2f2moz)

- [Examples of manual encoders/decoders](https://github.com/hhblaze/Biser/blob/master/BiserTest_Net)
-------------

hhblaze@gmail.com
