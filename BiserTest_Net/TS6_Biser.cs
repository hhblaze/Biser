using System;
using System.Collections.Generic;
using System.Linq;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IJsonEncoder, Biser.IEncoder
    {
        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {

            encoder.Add("P1", P1);
            encoder.Add("P2", P2);
            encoder.Add("P3", P3);
            encoder.Add("P4", P4, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add((r2 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r2.Item1);}}, { "Item2", () => {
encoder.Add(r2.Item2);}}});
                });
            });
            encoder.Add("P5", P5, (r3) => {
                encoder.Add((r3 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r3.Item1);}}, { "Item2", () => {
encoder.Add(r3.Item2);}}});
            });
            encoder.Add("P6", (P6 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(P6.Item1);}}, { "Item2", () => {
encoder.Add(P6.Item2);}}, { "Item3", () => {
encoder.Add((P6.Item3 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(P6.Item3.Item1, (r4) => {
encoder.Add(r4);});}}, { "Item2", () => {
encoder.Add(P6.Item3.Item2);}}});}}});
            encoder.Add("P7", P7, (r5) => {
                encoder.Add(r5);
            });
            encoder.Add("P8", P8, (r6) => {
                encoder.Add(r6, (r7) => {
                    encoder.Add(r7);
                });
            });
            encoder.Add("P10", P10); if (P11 != null)
            {
                var r8 = P11.ToList();
                encoder.Add("P11", r8, (r9) => {
                    if (r9 != null)
                    {
                        var r10 = r9.ToList();
                        encoder.Add(r10, (r11) => {
                            encoder.Add(r11);
                        });
                    }
                });
            }
            if (P12 != null)
            {
                var arrdim12 = new System.Collections.Generic.List<int>();
                arrdim12.Add(P12.GetLength(0));
                arrdim12.Add(P12.GetLength(1));
                arrdim12.Add(P12.GetLength(2));
                System.Collections.Generic.List<System.Int32> r13 = new System.Collections.Generic.List<System.Int32>();
                foreach (var el in P12)
                    r13.Add(el);
                var r14 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32>>(arrdim12, r13);
                encoder.Add("P12", (r14 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r14.Item1, (r15) => {
encoder.Add(r15);});}}, { "Item2", () => {
encoder.Add(r14.Item2, (r16) => {
encoder.Add(r16);});}}});
            }
            encoder.Add("P13", P13, (r17) => {
                encoder.Add(r17, (r18) => {
                    encoder.Add(r18);
                });
            });
            encoder.Add("P14", P14, (r19) => {
                encoder.Add(r19);
            });
            encoder.Add("P15", (P15 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(P15.Item1);}}, { "Item2", () => {
encoder.Add(P15.Item2);}}, { "Item3", () => {
encoder.Add(P15.Item3);}}, { "Item4", () => {
encoder.Add(P15.Item4);}}});
            encoder.Add("P16", P16, (r20) => {
                encoder.Add(r20, (r21) => {
                    encoder.Add((r21 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r21.Item1);}}, { "Item2", () => {
encoder.Add(r21.Item2);}}});
                });
            }); if (P17 != null)
            {
                var r22 = P17.ToList();
                encoder.Add("P17", r22, (r23) => {
                    encoder.Add(r23);
                });
            }
            if (P18 != null)
            {
                var arrdim24 = new System.Collections.Generic.List<int>();
                arrdim24.Add(P18.GetLength(0));
                arrdim24.Add(P18.GetLength(1));
                arrdim24.Add(P18.GetLength(2));
                System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][][][,,,,]> r25 = new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][][][,,,,]>();
                foreach (var el in P18)
                    r25.Add(el);
                var r26 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][][][,,,,]>>(arrdim24, r25);
                encoder.Add("P18", (r26 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r26.Item1, (r27) => {
encoder.Add(r27);});}}, { "Item2", () => {
encoder.Add(r26.Item2, (r28) => {if(r28 != null) {
var r29=r28.ToList();
encoder.Add(r29, (r30) => {if(r30 != null) {
var r31=r30.ToList();
encoder.Add(r31, (r32) => {if(r32 != null) {
var r33=r32.ToList();
encoder.Add(r33, (r34) => {if(r34 != null) {
var arrdim35=new System.Collections.Generic.List<int>();
arrdim35.Add(r34.GetLength(0));
arrdim35.Add(r34.GetLength(1));
arrdim35.Add(r34.GetLength(2));
arrdim35.Add(r34.GetLength(3));
arrdim35.Add(r34.GetLength(4));
System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>> r36= new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>>();
foreach(var el in r34)
r36.Add(el);
var r37 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>>>(arrdim35, r36);
encoder.Add((r37 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r37.Item1, (r38) => {
encoder.Add(r38);});}}, { "Item2", () => {
encoder.Add(r37.Item2, (r39) => {
encoder.Add(r39, (r40) => {if(r40 != null) {
var arrdim41=new System.Collections.Generic.List<int>();
arrdim41.Add(r40.GetLength(0));
arrdim41.Add(r40.GetLength(1));
arrdim41.Add(r40.GetLength(2));
System.Collections.Generic.List<System.Int32[][,,]> r42= new System.Collections.Generic.List<System.Int32[][,,]>();
foreach(var el in r40)
r42.Add(el);
var r43 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32[][,,]>>(arrdim41, r42);
encoder.Add((r43 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r43.Item1, (r44) => {
encoder.Add(r44);});}}, { "Item2", () => {
encoder.Add(r43.Item2, (r45) => {if(r45 != null) {
var r46=r45.ToList();
encoder.Add(r46, (r47) => {if(r47 != null) {
var arrdim48=new System.Collections.Generic.List<int>();
arrdim48.Add(r47.GetLength(0));
arrdim48.Add(r47.GetLength(1));
arrdim48.Add(r47.GetLength(2));
System.Collections.Generic.List<System.Int32> r49= new System.Collections.Generic.List<System.Int32>();
foreach(var el in r47)
r49.Add(el);
var r50 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32>>(arrdim48, r49);
encoder.Add((r50 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r50.Item1, (r51) => {
encoder.Add(r51);});}}, { "Item2", () => {
encoder.Add(r50.Item2, (r52) => {
encoder.Add(r52);});}}});}});}});}}});}});});}}});}});}});}});}});}}});
            }
            encoder.Add("P19", P19, (r53) => {
                encoder.Add(r53, (r54) => {
                    if (r54 != null)
                    {
                        var arrdim55 = new System.Collections.Generic.List<int>();
                        arrdim55.Add(r54.GetLength(0));
                        arrdim55.Add(r54.GetLength(1));
                        System.Collections.Generic.List<System.String[][,,]> r56 = new System.Collections.Generic.List<System.String[][,,]>();
                        foreach (var el in r54)
                            r56.Add(el);
                        var r57 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.String[][,,]>>(arrdim55, r56);
                        encoder.Add((r57 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r57.Item1, (r58) => {
encoder.Add(r58);});}}, { "Item2", () => {
encoder.Add(r57.Item2, (r59) => {if(r59 != null) {
var r60=r59.ToList();
encoder.Add(r60, (r61) => {if(r61 != null) {
var arrdim62=new System.Collections.Generic.List<int>();
arrdim62.Add(r61.GetLength(0));
arrdim62.Add(r61.GetLength(1));
arrdim62.Add(r61.GetLength(2));
System.Collections.Generic.List<System.String> r63= new System.Collections.Generic.List<System.String>();
foreach(var el in r61)
r63.Add(el);
var r64 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.String>>(arrdim62, r63);
encoder.Add((r64 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r64.Item1, (r65) => {
encoder.Add(r65);});}}, { "Item2", () => {
encoder.Add(r64.Item2, (r66) => {
encoder.Add(r66);});}}});}});}});}}});
                    }
                });
            });
            encoder.Add("P20", P20, (r67) => {
                if (r67 != null)
                {
                    var r68 = r67.ToList();
                    encoder.Add(r68, (r69) => {
                        encoder.Add(r69);
                    });
                }
            });
            encoder.Add("P21", P21);
            encoder.Add("P22", P22);
            encoder.Add("A1", A1); if (P23 != null)
            {
                var r70 = P23.ToList();
                encoder.Add("P23", r70, (r71) => {
                    if (r71 != null)
                    {
                        var arrdim72 = new System.Collections.Generic.List<int>();
                        arrdim72.Add(r71.GetLength(0));
                        arrdim72.Add(r71.GetLength(1));
                        arrdim72.Add(r71.GetLength(2));
                        System.Collections.Generic.List<System.Int32[][][,,,][][,]> r73 = new System.Collections.Generic.List<System.Int32[][][,,,][][,]>();
                        foreach (var el in r71)
                            r73.Add(el);
                        var r74 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32[][][,,,][][,]>>(arrdim72, r73);
                        encoder.Add((r74 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r74.Item1, (r75) => {
encoder.Add(r75);});}}, { "Item2", () => {
encoder.Add(r74.Item2, (r76) => {if(r76 != null) {
var r77=r76.ToList();
encoder.Add(r77, (r78) => {if(r78 != null) {
var r79=r78.ToList();
encoder.Add(r79, (r80) => {if(r80 != null) {
var arrdim81=new System.Collections.Generic.List<int>();
arrdim81.Add(r80.GetLength(0));
arrdim81.Add(r80.GetLength(1));
arrdim81.Add(r80.GetLength(2));
arrdim81.Add(r80.GetLength(3));
System.Collections.Generic.List<System.Int32[][,]> r82= new System.Collections.Generic.List<System.Int32[][,]>();
foreach(var el in r80)
r82.Add(el);
var r83 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32[][,]>>(arrdim81, r82);
encoder.Add((r83 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r83.Item1, (r84) => {
encoder.Add(r84);});}}, { "Item2", () => {
encoder.Add(r83.Item2, (r85) => {if(r85 != null) {
var r86=r85.ToList();
encoder.Add(r86, (r87) => {if(r87 != null) {
var arrdim88=new System.Collections.Generic.List<int>();
arrdim88.Add(r87.GetLength(0));
arrdim88.Add(r87.GetLength(1));
System.Collections.Generic.List<System.Int32> r89= new System.Collections.Generic.List<System.Int32>();
foreach(var el in r87)
r89.Add(el);
var r90 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32>>(arrdim88, r89);
encoder.Add((r90 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r90.Item1, (r91) => {
encoder.Add(r91);});}}, { "Item2", () => {
encoder.Add(r90.Item2, (r92) => {
encoder.Add(r92);});}}});}});}});}}});}});}});}});}}});
                    }
                });
            }
            encoder.Add("P24", P24, (r93) => {
                if (r93 != null)
                {
                    var r94 = r93.ToList();
                    encoder.Add(r94, (r95) => {
                        if (r95 != null)
                        {
                            var arrdim96 = new System.Collections.Generic.List<int>();
                            arrdim96.Add(r95.GetLength(0));
                            arrdim96.Add(r95.GetLength(1));
                            arrdim96.Add(r95.GetLength(2));
                            System.Collections.Generic.List<System.Int32[]> r97 = new System.Collections.Generic.List<System.Int32[]>();
                            foreach (var el in r95)
                                r97.Add(el);
                            var r98 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32[]>>(arrdim96, r97);
                            encoder.Add((r98 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r98.Item1, (r99) => {
encoder.Add(r99);});}}, { "Item2", () => {
encoder.Add(r98.Item2, (r100) => {if(r100 != null) {
var r101=r100.ToList();
encoder.Add(r101, (r102) => {
encoder.Add(r102);});}});}}});
                        }
                    });
                }
            }); if (P25 != null)
            {
                var arrdim103 = new System.Collections.Generic.List<int>();
                arrdim103.Add(P25.GetLength(0));
                arrdim103.Add(P25.GetLength(1));
                arrdim103.Add(P25.GetLength(2));
                arrdim103.Add(P25.GetLength(3));
                System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[][,,]> r104 = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[][,,]>();
                foreach (var el in P25)
                    r104.Add(el);
                var r105 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[][,,]>>(arrdim103, r104);
                encoder.Add("P25", (r105 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r105.Item1, (r106) => {
encoder.Add(r106);});}}, { "Item2", () => {
encoder.Add(r105.Item2, (r107) => {if(r107 != null) {
var r108=r107.ToList();
encoder.Add(r108, (r109) => {if(r109 != null) {
var arrdim110=new System.Collections.Generic.List<int>();
arrdim110.Add(r109.GetLength(0));
arrdim110.Add(r109.GetLength(1));
arrdim110.Add(r109.GetLength(2));
System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>> r111= new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>>();
foreach(var el in r109)
r111.Add(el);
var r112 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>>>(arrdim110, r111);
encoder.Add((r112 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r112.Item1, (r113) => {
encoder.Add(r113);});}}, { "Item2", () => {
encoder.Add(r112.Item2, (r114) => {
encoder.Add(r114, (r115) => {
encoder.Add(r115, (r116) => {if(r116 != null) {
var arrdim117=new System.Collections.Generic.List<int>();
arrdim117.Add(r116.GetLength(0));
arrdim117.Add(r116.GetLength(1));
System.Collections.Generic.List<System.String[][,,]> r118= new System.Collections.Generic.List<System.String[][,,]>();
foreach(var el in r116)
r118.Add(el);
var r119 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.String[][,,]>>(arrdim117, r118);
encoder.Add((r119 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r119.Item1, (r120) => {
encoder.Add(r120);});}}, { "Item2", () => {
encoder.Add(r119.Item2, (r121) => {if(r121 != null) {
var r122=r121.ToList();
encoder.Add(r122, (r123) => {if(r123 != null) {
var arrdim124=new System.Collections.Generic.List<int>();
arrdim124.Add(r123.GetLength(0));
arrdim124.Add(r123.GetLength(1));
arrdim124.Add(r123.GetLength(2));
System.Collections.Generic.List<System.String> r125= new System.Collections.Generic.List<System.String>();
foreach(var el in r123)
r125.Add(el);
var r126 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.String>>(arrdim124, r125);
encoder.Add((r126 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r126.Item1, (r127) => {
encoder.Add(r127);});}}, { "Item2", () => {
encoder.Add(r126.Item2, (r128) => {
encoder.Add(r128);});}}});}});}});}}});}});});});}}});}});}});}}});
            }
        }

        public static TS6 BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null)
        {
            Biser.JsonDecoder decoder = null;

            if (extDecoder == null)
            {
                if (enc == null || String.IsNullOrEmpty(enc))
                    return null;
                decoder = new Biser.JsonDecoder(enc, settings);
                if (decoder.CheckNull())
                    return null;
            }
            else
                decoder = extDecoder;

            TS6 m = new TS6();
            foreach (var props in decoder.GetDictionary<string>())
            {
                switch (props.ToLower())
                {

                    case "p1":
                        m.P1 = decoder.GetString();
                        break;
                    case "p2":
                        m.P2 = decoder.GetInt();
                        break;
                    case "p3":
                        m.P3 = decoder.GetDateTime();
                        break;
                    case "p4":
                        m.P4 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.DateTime, System.Tuple<System.Int32, System.String>>>();
                        if (m.P4 != null)
                        {
                            foreach (var el1 in decoder.GetList())
                            {
                                var pvar2 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.DateTime, System.Tuple<System.Int32, System.String>>();
                                if (pvar2 != null)
                                {
                                    foreach (var el3 in decoder.GetDictionary<System.DateTime>())
                                    {
                                        System.Int32 pvar5 = 0;
                                        System.String pvar6 = null;
                                        foreach (var tupleProps7 in decoder.GetDictionary<string>())
                                        {
                                            switch (tupleProps7)
                                            {
                                                case "Item1":
                                                    pvar5 = decoder.GetInt();
                                                    break;
                                                case "Item2":
                                                    pvar6 = decoder.GetString();
                                                    break;
                                            }
                                        }
                                        var pvar4 = new Tuple<System.Int32, System.String>(pvar5, pvar6);
                                        pvar2.Add(el3, pvar4);
                                    }
                                }
                                m.P4.Add(pvar2);
                            }
                        }
                        break;
                    case "p5":
                        m.P5 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>();
                        if (m.P5 != null)
                        {
                            foreach (var el8 in decoder.GetDictionary<System.Int32>())
                            {
                                System.Int32 pvar10 = 0;
                                System.String pvar11 = null;
                                foreach (var tupleProps12 in decoder.GetDictionary<string>())
                                {
                                    switch (tupleProps12)
                                    {
                                        case "Item1":
                                            pvar10 = decoder.GetInt();
                                            break;
                                        case "Item2":
                                            pvar11 = decoder.GetString();
                                            break;
                                    }
                                }
                                var pvar9 = new Tuple<System.Int32, System.String>(pvar10, pvar11);
                                m.P5.Add(el8, pvar9);
                            }
                        }
                        break;
                    case "p6":
                        System.Int32 pvar13 = 0;
                        System.String pvar14 = null;
                        System.Tuple<System.Collections.Generic.List<System.String>, System.DateTime> pvar15 = default(System.Tuple<System.Collections.Generic.List<System.String>, System.DateTime>);
                        foreach (var tupleProps21 in decoder.GetDictionary<string>())
                        {
                            switch (tupleProps21)
                            {
                                case "Item1":
                                    pvar13 = decoder.GetInt();
                                    break;
                                case "Item2":
                                    pvar14 = decoder.GetString();
                                    break;
                                case "Item3":
                                    System.Collections.Generic.List<System.String> pvar16 = null;
                                    System.DateTime pvar19 = default(DateTime);
                                    foreach (var tupleProps20 in decoder.GetDictionary<string>())
                                    {
                                        switch (tupleProps20)
                                        {
                                            case "Item1":
                                                pvar16 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
                                                if (pvar16 != null)
                                                {
                                                    foreach (var el17 in decoder.GetList())
                                                    {
                                                        var pvar18 = decoder.GetString();
                                                        pvar16.Add(pvar18);
                                                    }
                                                }
                                                break;
                                            case "Item2":
                                                pvar19 = decoder.GetDateTime();
                                                break;
                                        }
                                    }
                                    pvar15 = new Tuple<System.Collections.Generic.List<System.String>, System.DateTime>(pvar16, pvar19);
                                    break;
                            }
                        }
                        m.P6 = new Tuple<System.Int32, System.String, System.Tuple<System.Collections.Generic.List<System.String>, System.DateTime>>(pvar13, pvar14, pvar15);
                        break;
                    case "p7":
                        m.P7 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
                        if (m.P7 != null)
                        {
                            foreach (var el22 in decoder.GetList())
                            {
                                var pvar23 = decoder.GetString();
                                m.P7.Add(pvar23);
                            }
                        }
                        break;
                    case "p8":
                        m.P8 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String>>();
                        if (m.P8 != null)
                        {
                            foreach (var el24 in decoder.GetDictionary<System.Int32>())
                            {
                                var pvar25 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
                                if (pvar25 != null)
                                {
                                    foreach (var el26 in decoder.GetList())
                                    {
                                        var pvar27 = decoder.GetString();
                                        pvar25.Add(pvar27);
                                    }
                                }
                                m.P8.Add(el24, pvar25);
                            }
                        }
                        break;
                    case "p10":
                        m.P10 = decoder.GetShort_NULL();
                        break;
                    case "p11":
                        var intlst28 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[]>();
                        if (intlst28 != null)
                        {
                            foreach (var el29 in decoder.GetList())
                            {
                                var intlst31 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                if (intlst31 != null)
                                {
                                    foreach (var el32 in decoder.GetList())
                                    {
                                        var pvar33 = decoder.GetInt();
                                        intlst31.Add(pvar33);
                                    }
                                }
                                var pvar30 = decoder.CheckNull() ? null : new System.Int32[intlst31.Count];
                                if (pvar30 != null)
                                {
                                    for (int ard34_0 = 0; ard34_0 < pvar30.GetLength(0); ard34_0++)
                                    {
                                        pvar30[ard34_0] = intlst31[ard34_0];
                                    }
                                }
                                intlst28.Add(pvar30);
                            }
                        }
                        m.P11 = decoder.CheckNull() ? null : new System.Int32[intlst28.Count][];
                        if (m.P11 != null)
                        {
                            for (int ard35_0 = 0; ard35_0 < m.P11.GetLength(0); ard35_0++)
                            {
                                m.P11[ard35_0] = intlst28[ard35_0];
                            }
                        }
                        break;
                    case "p12":
                        System.Collections.Generic.List<System.Int32> pvar37 = null;
                        System.Collections.Generic.List<System.Int32> pvar40 = null;
                        foreach (var tupleProps43 in decoder.GetDictionary<string>())
                        {
                            switch (tupleProps43)
                            {
                                case "Item1":
                                    pvar37 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                    if (pvar37 != null)
                                    {
                                        foreach (var el38 in decoder.GetList())
                                        {
                                            var pvar39 = decoder.GetInt();
                                            pvar37.Add(pvar39);
                                        }
                                    }
                                    break;
                                case "Item2":
                                    pvar40 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                    if (pvar40 != null)
                                    {
                                        foreach (var el41 in decoder.GetList())
                                        {
                                            var pvar42 = decoder.GetInt();
                                            pvar40.Add(pvar42);
                                        }
                                    }
                                    break;
                            }
                        }
                        var pv36 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32>>(pvar37, pvar40);
                        m.P12 = new System.Int32[pv36.Item1[0], pv36.Item1[1], pv36.Item1[2]];
                        var arenm44 = pv36.Item2.GetEnumerator();
                        arenm44.MoveNext();
                        for (int ard45_0 = 0; ard45_0 < m.P12.GetLength(0); ard45_0++)
                            for (int ard45_1 = 0; ard45_1 < m.P12.GetLength(1); ard45_1++)
                                for (int ard45_2 = 0; ard45_2 < m.P12.GetLength(2); ard45_2++)
                                {
                                    m.P12[ard45_0, ard45_1, ard45_2] = arenm44.Current;
                                    arenm44.MoveNext();
                                }
                        break;
                    case "p13":
                        m.P13 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>();
                        if (m.P13 != null)
                        {
                            foreach (var el46 in decoder.GetList())
                            {
                                var pvar47 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                if (pvar47 != null)
                                {
                                    foreach (var el48 in decoder.GetList())
                                    {
                                        var pvar49 = decoder.GetInt();
                                        pvar47.Add(pvar49);
                                    }
                                }
                                m.P13.Add(pvar47);
                            }
                        }
                        break;
                    case "p14":
                        m.P14 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.String>();
                        if (m.P14 != null)
                        {
                            foreach (var el50 in decoder.GetDictionary<System.Int32>())
                            {
                                var pvar51 = decoder.GetString();
                                m.P14.Add(el50, pvar51);
                            }
                        }
                        break;
                    case "p15":
                        System.Int32 pvar52 = 0;
                        System.String pvar53 = null;
                        System.DateTime pvar54 = default(DateTime);
                        System.Byte[] pvar55 = null;
                        foreach (var tupleProps56 in decoder.GetDictionary<string>())
                        {
                            switch (tupleProps56)
                            {
                                case "Item1":
                                    pvar52 = decoder.GetInt();
                                    break;
                                case "Item2":
                                    pvar53 = decoder.GetString();
                                    break;
                                case "Item3":
                                    pvar54 = decoder.GetDateTime();
                                    break;
                                case "Item4":
                                    pvar55 = decoder.GetByteArray();
                                    break;
                            }
                        }
                        m.P15 = new Tuple<System.Int32, System.String, System.DateTime, System.Byte[]>(pvar52, pvar53, pvar54, pvar55);
                        break;
                    case "p16":
                        m.P16 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>>();
                        if (m.P16 != null)
                        {
                            foreach (var el57 in decoder.GetList())
                            {
                                var pvar58 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>();
                                if (pvar58 != null)
                                {
                                    foreach (var el59 in decoder.GetDictionary<System.Int32>())
                                    {
                                        System.Int32 pvar61 = 0;
                                        System.String pvar62 = null;
                                        foreach (var tupleProps63 in decoder.GetDictionary<string>())
                                        {
                                            switch (tupleProps63)
                                            {
                                                case "Item1":
                                                    pvar61 = decoder.GetInt();
                                                    break;
                                                case "Item2":
                                                    pvar62 = decoder.GetString();
                                                    break;
                                            }
                                        }
                                        var pvar60 = new Tuple<System.Int32, System.String>(pvar61, pvar62);
                                        pvar58.Add(el59, pvar60);
                                    }
                                }
                                m.P16.Add(pvar58);
                            }
                        }
                        break;
                    case "p17":
                        var intlst64 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                        if (intlst64 != null)
                        {
                            foreach (var el65 in decoder.GetList())
                            {
                                var pvar66 = decoder.GetInt();
                                intlst64.Add(pvar66);
                            }
                        }
                        m.P17 = decoder.CheckNull() ? null : new System.Int32[intlst64.Count];
                        if (m.P17 != null)
                        {
                            for (int ard67_0 = 0; ard67_0 < m.P17.GetLength(0); ard67_0++)
                            {
                                m.P17[ard67_0] = intlst64[ard67_0];
                            }
                        }
                        break;
                    case "p18":
                        System.Collections.Generic.List<System.Int32> pvar69 = null;
                        System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][][][,,,,]> pvar72 = null;
                        foreach (var tupleProps123 in decoder.GetDictionary<string>())
                        {
                            switch (tupleProps123)
                            {
                                case "Item1":
                                    pvar69 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                    if (pvar69 != null)
                                    {
                                        foreach (var el70 in decoder.GetList())
                                        {
                                            var pvar71 = decoder.GetInt();
                                            pvar69.Add(pvar71);
                                        }
                                    }
                                    break;
                                case "Item2":
                                    pvar72 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][][][,,,,]>();
                                    if (pvar72 != null)
                                    {
                                        foreach (var el73 in decoder.GetList())
                                        {
                                            var intlst75 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][][,,,,]>();
                                            if (intlst75 != null)
                                            {
                                                foreach (var el76 in decoder.GetList())
                                                {
                                                    var intlst78 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][,,,,]>();
                                                    if (intlst78 != null)
                                                    {
                                                        foreach (var el79 in decoder.GetList())
                                                        {
                                                            var intlst81 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[,,,,]>();
                                                            if (intlst81 != null)
                                                            {
                                                                foreach (var el82 in decoder.GetList())
                                                                {
                                                                    System.Collections.Generic.List<System.Int32> pvar85 = null;
                                                                    System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>> pvar88 = null;
                                                                    foreach (var tupleProps117 in decoder.GetDictionary<string>())
                                                                    {
                                                                        switch (tupleProps117)
                                                                        {
                                                                            case "Item1":
                                                                                pvar85 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                if (pvar85 != null)
                                                                                {
                                                                                    foreach (var el86 in decoder.GetList())
                                                                                    {
                                                                                        var pvar87 = decoder.GetInt();
                                                                                        pvar85.Add(pvar87);
                                                                                    }
                                                                                }
                                                                                break;
                                                                            case "Item2":
                                                                                pvar88 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>>();
                                                                                if (pvar88 != null)
                                                                                {
                                                                                    foreach (var el89 in decoder.GetList())
                                                                                    {
                                                                                        var pvar90 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>();
                                                                                        if (pvar90 != null)
                                                                                        {
                                                                                            foreach (var el91 in decoder.GetList())
                                                                                            {
                                                                                                System.Collections.Generic.List<System.Int32> pvar94 = null;
                                                                                                System.Collections.Generic.List<System.Int32[][,,]> pvar97 = null;
                                                                                                foreach (var tupleProps114 in decoder.GetDictionary<string>())
                                                                                                {
                                                                                                    switch (tupleProps114)
                                                                                                    {
                                                                                                        case "Item1":
                                                                                                            pvar94 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                                            if (pvar94 != null)
                                                                                                            {
                                                                                                                foreach (var el95 in decoder.GetList())
                                                                                                                {
                                                                                                                    var pvar96 = decoder.GetInt();
                                                                                                                    pvar94.Add(pvar96);
                                                                                                                }
                                                                                                            }
                                                                                                            break;
                                                                                                        case "Item2":
                                                                                                            pvar97 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[][,,]>();
                                                                                                            if (pvar97 != null)
                                                                                                            {
                                                                                                                foreach (var el98 in decoder.GetList())
                                                                                                                {
                                                                                                                    var intlst100 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,]>();
                                                                                                                    if (intlst100 != null)
                                                                                                                    {
                                                                                                                        foreach (var el101 in decoder.GetList())
                                                                                                                        {
                                                                                                                            System.Collections.Generic.List<System.Int32> pvar104 = null;
                                                                                                                            System.Collections.Generic.List<System.Int32> pvar107 = null;
                                                                                                                            foreach (var tupleProps110 in decoder.GetDictionary<string>())
                                                                                                                            {
                                                                                                                                switch (tupleProps110)
                                                                                                                                {
                                                                                                                                    case "Item1":
                                                                                                                                        pvar104 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                                                                        if (pvar104 != null)
                                                                                                                                        {
                                                                                                                                            foreach (var el105 in decoder.GetList())
                                                                                                                                            {
                                                                                                                                                var pvar106 = decoder.GetInt();
                                                                                                                                                pvar104.Add(pvar106);
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        break;
                                                                                                                                    case "Item2":
                                                                                                                                        pvar107 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                                                                        if (pvar107 != null)
                                                                                                                                        {
                                                                                                                                            foreach (var el108 in decoder.GetList())
                                                                                                                                            {
                                                                                                                                                var pvar109 = decoder.GetInt();
                                                                                                                                                pvar107.Add(pvar109);
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        break;
                                                                                                                                }
                                                                                                                            }
                                                                                                                            var pv103 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32>>(pvar104, pvar107);
                                                                                                                            var pvar102 = new System.Int32[pv103.Item1[0], pv103.Item1[1], pv103.Item1[2]];
                                                                                                                            var arenm111 = pv103.Item2.GetEnumerator();
                                                                                                                            arenm111.MoveNext();
                                                                                                                            for (int ard112_0 = 0; ard112_0 < pvar102.GetLength(0); ard112_0++)
                                                                                                                                for (int ard112_1 = 0; ard112_1 < pvar102.GetLength(1); ard112_1++)
                                                                                                                                    for (int ard112_2 = 0; ard112_2 < pvar102.GetLength(2); ard112_2++)
                                                                                                                                    {
                                                                                                                                        pvar102[ard112_0, ard112_1, ard112_2] = arenm111.Current;
                                                                                                                                        arenm111.MoveNext();
                                                                                                                                    }
                                                                                                                            intlst100.Add(pvar102);
                                                                                                                        }
                                                                                                                    }
                                                                                                                    var pvar99 = decoder.CheckNull() ? null : new System.Int32[intlst100.Count][,,];
                                                                                                                    if (pvar99 != null)
                                                                                                                    {
                                                                                                                        for (int ard113_0 = 0; ard113_0 < pvar99.GetLength(0); ard113_0++)
                                                                                                                        {
                                                                                                                            pvar99[ard113_0] = intlst100[ard113_0];
                                                                                                                        }
                                                                                                                    }
                                                                                                                    pvar97.Add(pvar99);
                                                                                                                }
                                                                                                            }
                                                                                                            break;
                                                                                                    }
                                                                                                }
                                                                                                var pv93 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32[][,,]>>(pvar94, pvar97);
                                                                                                var pvar92 = new System.Int32[pv93.Item1[0], pv93.Item1[1], pv93.Item1[2]][][,,];
                                                                                                var arenm115 = pv93.Item2.GetEnumerator();
                                                                                                arenm115.MoveNext();
                                                                                                for (int ard116_0 = 0; ard116_0 < pvar92.GetLength(0); ard116_0++)
                                                                                                    for (int ard116_1 = 0; ard116_1 < pvar92.GetLength(1); ard116_1++)
                                                                                                        for (int ard116_2 = 0; ard116_2 < pvar92.GetLength(2); ard116_2++)
                                                                                                        {
                                                                                                            pvar92[ard116_0, ard116_1, ard116_2] = arenm115.Current;
                                                                                                            arenm115.MoveNext();
                                                                                                        }
                                                                                                pvar90.Add(pvar92);
                                                                                            }
                                                                                        }
                                                                                        pvar88.Add(pvar90);
                                                                                    }
                                                                                }
                                                                                break;
                                                                        }
                                                                    }
                                                                    var pv84 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>>>(pvar85, pvar88);
                                                                    var pvar83 = new System.Collections.Generic.List<System.Int32[,,][][,,]>[pv84.Item1[0], pv84.Item1[1], pv84.Item1[2], pv84.Item1[3], pv84.Item1[4]];
                                                                    var arenm118 = pv84.Item2.GetEnumerator();
                                                                    arenm118.MoveNext();
                                                                    for (int ard119_0 = 0; ard119_0 < pvar83.GetLength(0); ard119_0++)
                                                                        for (int ard119_1 = 0; ard119_1 < pvar83.GetLength(1); ard119_1++)
                                                                            for (int ard119_2 = 0; ard119_2 < pvar83.GetLength(2); ard119_2++)
                                                                                for (int ard119_3 = 0; ard119_3 < pvar83.GetLength(3); ard119_3++)
                                                                                    for (int ard119_4 = 0; ard119_4 < pvar83.GetLength(4); ard119_4++)
                                                                                    {
                                                                                        pvar83[ard119_0, ard119_1, ard119_2, ard119_3, ard119_4] = arenm118.Current;
                                                                                        arenm118.MoveNext();
                                                                                    }
                                                                    intlst81.Add(pvar83);
                                                                }
                                                            }
                                                            var pvar80 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[intlst81.Count][,,,,];
                                                            if (pvar80 != null)
                                                            {
                                                                for (int ard120_0 = 0; ard120_0 < pvar80.GetLength(0); ard120_0++)
                                                                {
                                                                    pvar80[ard120_0] = intlst81[ard120_0];
                                                                }
                                                            }
                                                            intlst78.Add(pvar80);
                                                        }
                                                    }
                                                    var pvar77 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[intlst78.Count][][,,,,];
                                                    if (pvar77 != null)
                                                    {
                                                        for (int ard121_0 = 0; ard121_0 < pvar77.GetLength(0); ard121_0++)
                                                        {
                                                            pvar77[ard121_0] = intlst78[ard121_0];
                                                        }
                                                    }
                                                    intlst75.Add(pvar77);
                                                }
                                            }
                                            var pvar74 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[intlst75.Count][][][,,,,];
                                            if (pvar74 != null)
                                            {
                                                for (int ard122_0 = 0; ard122_0 < pvar74.GetLength(0); ard122_0++)
                                                {
                                                    pvar74[ard122_0] = intlst75[ard122_0];
                                                }
                                            }
                                            pvar72.Add(pvar74);
                                        }
                                    }
                                    break;
                            }
                        }
                        var pv68 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Collections.Generic.List<System.Int32[,,][][,,]>[][][][,,,,]>>(pvar69, pvar72);
                        m.P18 = new System.Collections.Generic.List<System.Int32[,,][][,,]>[pv68.Item1[0], pv68.Item1[1], pv68.Item1[2]][][][][,,,,];
                        var arenm124 = pv68.Item2.GetEnumerator();
                        arenm124.MoveNext();
                        for (int ard125_0 = 0; ard125_0 < m.P18.GetLength(0); ard125_0++)
                            for (int ard125_1 = 0; ard125_1 < m.P18.GetLength(1); ard125_1++)
                                for (int ard125_2 = 0; ard125_2 < m.P18.GetLength(2); ard125_2++)
                                {
                                    m.P18[ard125_0, ard125_1, ard125_2] = arenm124.Current;
                                    arenm124.MoveNext();
                                }
                        break;
                    case "p19":
                        m.P19 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>();
                        if (m.P19 != null)
                        {
                            foreach (var el126 in decoder.GetDictionary<System.Int32>())
                            {
                                var pvar127 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[,][][,,]>();
                                if (pvar127 != null)
                                {
                                    foreach (var el128 in decoder.GetList())
                                    {
                                        System.Collections.Generic.List<System.Int32> pvar131 = null;
                                        System.Collections.Generic.List<System.String[][,,]> pvar134 = null;
                                        foreach (var tupleProps151 in decoder.GetDictionary<string>())
                                        {
                                            switch (tupleProps151)
                                            {
                                                case "Item1":
                                                    pvar131 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                    if (pvar131 != null)
                                                    {
                                                        foreach (var el132 in decoder.GetList())
                                                        {
                                                            var pvar133 = decoder.GetInt();
                                                            pvar131.Add(pvar133);
                                                        }
                                                    }
                                                    break;
                                                case "Item2":
                                                    pvar134 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[][,,]>();
                                                    if (pvar134 != null)
                                                    {
                                                        foreach (var el135 in decoder.GetList())
                                                        {
                                                            var intlst137 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[,,]>();
                                                            if (intlst137 != null)
                                                            {
                                                                foreach (var el138 in decoder.GetList())
                                                                {
                                                                    System.Collections.Generic.List<System.Int32> pvar141 = null;
                                                                    System.Collections.Generic.List<System.String> pvar144 = null;
                                                                    foreach (var tupleProps147 in decoder.GetDictionary<string>())
                                                                    {
                                                                        switch (tupleProps147)
                                                                        {
                                                                            case "Item1":
                                                                                pvar141 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                if (pvar141 != null)
                                                                                {
                                                                                    foreach (var el142 in decoder.GetList())
                                                                                    {
                                                                                        var pvar143 = decoder.GetInt();
                                                                                        pvar141.Add(pvar143);
                                                                                    }
                                                                                }
                                                                                break;
                                                                            case "Item2":
                                                                                pvar144 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
                                                                                if (pvar144 != null)
                                                                                {
                                                                                    foreach (var el145 in decoder.GetList())
                                                                                    {
                                                                                        var pvar146 = decoder.GetString();
                                                                                        pvar144.Add(pvar146);
                                                                                    }
                                                                                }
                                                                                break;
                                                                        }
                                                                    }
                                                                    var pv140 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.String>>(pvar141, pvar144);
                                                                    var pvar139 = new System.String[pv140.Item1[0], pv140.Item1[1], pv140.Item1[2]];
                                                                    var arenm148 = pv140.Item2.GetEnumerator();
                                                                    arenm148.MoveNext();
                                                                    for (int ard149_0 = 0; ard149_0 < pvar139.GetLength(0); ard149_0++)
                                                                        for (int ard149_1 = 0; ard149_1 < pvar139.GetLength(1); ard149_1++)
                                                                            for (int ard149_2 = 0; ard149_2 < pvar139.GetLength(2); ard149_2++)
                                                                            {
                                                                                pvar139[ard149_0, ard149_1, ard149_2] = arenm148.Current;
                                                                                arenm148.MoveNext();
                                                                            }
                                                                    intlst137.Add(pvar139);
                                                                }
                                                            }
                                                            var pvar136 = decoder.CheckNull() ? null : new System.String[intlst137.Count][,,];
                                                            if (pvar136 != null)
                                                            {
                                                                for (int ard150_0 = 0; ard150_0 < pvar136.GetLength(0); ard150_0++)
                                                                {
                                                                    pvar136[ard150_0] = intlst137[ard150_0];
                                                                }
                                                            }
                                                            pvar134.Add(pvar136);
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        var pv130 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.String[][,,]>>(pvar131, pvar134);
                                        var pvar129 = new System.String[pv130.Item1[0], pv130.Item1[1]][][,,];
                                        var arenm152 = pv130.Item2.GetEnumerator();
                                        arenm152.MoveNext();
                                        for (int ard153_0 = 0; ard153_0 < pvar129.GetLength(0); ard153_0++)
                                            for (int ard153_1 = 0; ard153_1 < pvar129.GetLength(1); ard153_1++)
                                            {
                                                pvar129[ard153_0, ard153_1] = arenm152.Current;
                                                arenm152.MoveNext();
                                            }
                                        pvar127.Add(pvar129);
                                    }
                                }
                                m.P19.Add(el126, pvar127);
                            }
                        }
                        break;
                    case "p20":
                        m.P20 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[]>();
                        if (m.P20 != null)
                        {
                            foreach (var el154 in decoder.GetList())
                            {
                                var intlst156 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                if (intlst156 != null)
                                {
                                    foreach (var el157 in decoder.GetList())
                                    {
                                        var pvar158 = decoder.GetInt();
                                        intlst156.Add(pvar158);
                                    }
                                }
                                var pvar155 = decoder.CheckNull() ? null : new System.Int32[intlst156.Count];
                                if (pvar155 != null)
                                {
                                    for (int ard159_0 = 0; ard159_0 < pvar155.GetLength(0); ard159_0++)
                                    {
                                        pvar155[ard159_0] = intlst156[ard159_0];
                                    }
                                }
                                m.P20.Add(pvar155);
                            }
                        }
                        break;
                    case "p21":
                        m.P21 = BiserTest_Net.TS2.BiserJsonDecode(null, decoder);
                        break;
                    case "p22":
                        m.P22 = BiserTest_Net.TS3.BiserJsonDecode(null, decoder);
                        break;
                    case "a1":
                        m.A1 = BiserTest_Net.TS3.BiserJsonDecode(null, decoder);
                        break;
                    case "p23":
                        var intlst160 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][][,,,][][,]>();
                        if (intlst160 != null)
                        {
                            foreach (var el161 in decoder.GetList())
                            {
                                System.Collections.Generic.List<System.Int32> pvar164 = null;
                                System.Collections.Generic.List<System.Int32[][][,,,][][,]> pvar167 = null;
                                foreach (var tupleProps202 in decoder.GetDictionary<string>())
                                {
                                    switch (tupleProps202)
                                    {
                                        case "Item1":
                                            pvar164 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                            if (pvar164 != null)
                                            {
                                                foreach (var el165 in decoder.GetList())
                                                {
                                                    var pvar166 = decoder.GetInt();
                                                    pvar164.Add(pvar166);
                                                }
                                            }
                                            break;
                                        case "Item2":
                                            pvar167 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[][][,,,][][,]>();
                                            if (pvar167 != null)
                                            {
                                                foreach (var el168 in decoder.GetList())
                                                {
                                                    var intlst170 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[][,,,][][,]>();
                                                    if (intlst170 != null)
                                                    {
                                                        foreach (var el171 in decoder.GetList())
                                                        {
                                                            var intlst173 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,,][][,]>();
                                                            if (intlst173 != null)
                                                            {
                                                                foreach (var el174 in decoder.GetList())
                                                                {
                                                                    System.Collections.Generic.List<System.Int32> pvar177 = null;
                                                                    System.Collections.Generic.List<System.Int32[][,]> pvar180 = null;
                                                                    foreach (var tupleProps197 in decoder.GetDictionary<string>())
                                                                    {
                                                                        switch (tupleProps197)
                                                                        {
                                                                            case "Item1":
                                                                                pvar177 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                if (pvar177 != null)
                                                                                {
                                                                                    foreach (var el178 in decoder.GetList())
                                                                                    {
                                                                                        var pvar179 = decoder.GetInt();
                                                                                        pvar177.Add(pvar179);
                                                                                    }
                                                                                }
                                                                                break;
                                                                            case "Item2":
                                                                                pvar180 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[][,]>();
                                                                                if (pvar180 != null)
                                                                                {
                                                                                    foreach (var el181 in decoder.GetList())
                                                                                    {
                                                                                        var intlst183 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,]>();
                                                                                        if (intlst183 != null)
                                                                                        {
                                                                                            foreach (var el184 in decoder.GetList())
                                                                                            {
                                                                                                System.Collections.Generic.List<System.Int32> pvar187 = null;
                                                                                                System.Collections.Generic.List<System.Int32> pvar190 = null;
                                                                                                foreach (var tupleProps193 in decoder.GetDictionary<string>())
                                                                                                {
                                                                                                    switch (tupleProps193)
                                                                                                    {
                                                                                                        case "Item1":
                                                                                                            pvar187 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                                            if (pvar187 != null)
                                                                                                            {
                                                                                                                foreach (var el188 in decoder.GetList())
                                                                                                                {
                                                                                                                    var pvar189 = decoder.GetInt();
                                                                                                                    pvar187.Add(pvar189);
                                                                                                                }
                                                                                                            }
                                                                                                            break;
                                                                                                        case "Item2":
                                                                                                            pvar190 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                                            if (pvar190 != null)
                                                                                                            {
                                                                                                                foreach (var el191 in decoder.GetList())
                                                                                                                {
                                                                                                                    var pvar192 = decoder.GetInt();
                                                                                                                    pvar190.Add(pvar192);
                                                                                                                }
                                                                                                            }
                                                                                                            break;
                                                                                                    }
                                                                                                }
                                                                                                var pv186 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32>>(pvar187, pvar190);
                                                                                                var pvar185 = new System.Int32[pv186.Item1[0], pv186.Item1[1]];
                                                                                                var arenm194 = pv186.Item2.GetEnumerator();
                                                                                                arenm194.MoveNext();
                                                                                                for (int ard195_0 = 0; ard195_0 < pvar185.GetLength(0); ard195_0++)
                                                                                                    for (int ard195_1 = 0; ard195_1 < pvar185.GetLength(1); ard195_1++)
                                                                                                    {
                                                                                                        pvar185[ard195_0, ard195_1] = arenm194.Current;
                                                                                                        arenm194.MoveNext();
                                                                                                    }
                                                                                                intlst183.Add(pvar185);
                                                                                            }
                                                                                        }
                                                                                        var pvar182 = decoder.CheckNull() ? null : new System.Int32[intlst183.Count][,];
                                                                                        if (pvar182 != null)
                                                                                        {
                                                                                            for (int ard196_0 = 0; ard196_0 < pvar182.GetLength(0); ard196_0++)
                                                                                            {
                                                                                                pvar182[ard196_0] = intlst183[ard196_0];
                                                                                            }
                                                                                        }
                                                                                        pvar180.Add(pvar182);
                                                                                    }
                                                                                }
                                                                                break;
                                                                        }
                                                                    }
                                                                    var pv176 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32[][,]>>(pvar177, pvar180);
                                                                    var pvar175 = new System.Int32[pv176.Item1[0], pv176.Item1[1], pv176.Item1[2], pv176.Item1[3]][][,];
                                                                    var arenm198 = pv176.Item2.GetEnumerator();
                                                                    arenm198.MoveNext();
                                                                    for (int ard199_0 = 0; ard199_0 < pvar175.GetLength(0); ard199_0++)
                                                                        for (int ard199_1 = 0; ard199_1 < pvar175.GetLength(1); ard199_1++)
                                                                            for (int ard199_2 = 0; ard199_2 < pvar175.GetLength(2); ard199_2++)
                                                                                for (int ard199_3 = 0; ard199_3 < pvar175.GetLength(3); ard199_3++)
                                                                                {
                                                                                    pvar175[ard199_0, ard199_1, ard199_2, ard199_3] = arenm198.Current;
                                                                                    arenm198.MoveNext();
                                                                                }
                                                                    intlst173.Add(pvar175);
                                                                }
                                                            }
                                                            var pvar172 = decoder.CheckNull() ? null : new System.Int32[intlst173.Count][,,,][][,];
                                                            if (pvar172 != null)
                                                            {
                                                                for (int ard200_0 = 0; ard200_0 < pvar172.GetLength(0); ard200_0++)
                                                                {
                                                                    pvar172[ard200_0] = intlst173[ard200_0];
                                                                }
                                                            }
                                                            intlst170.Add(pvar172);
                                                        }
                                                    }
                                                    var pvar169 = decoder.CheckNull() ? null : new System.Int32[intlst170.Count][][,,,][][,];
                                                    if (pvar169 != null)
                                                    {
                                                        for (int ard201_0 = 0; ard201_0 < pvar169.GetLength(0); ard201_0++)
                                                        {
                                                            pvar169[ard201_0] = intlst170[ard201_0];
                                                        }
                                                    }
                                                    pvar167.Add(pvar169);
                                                }
                                            }
                                            break;
                                    }
                                }
                                var pv163 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32[][][,,,][][,]>>(pvar164, pvar167);
                                var pvar162 = new System.Int32[pv163.Item1[0], pv163.Item1[1], pv163.Item1[2]][][][,,,][][,];
                                var arenm203 = pv163.Item2.GetEnumerator();
                                arenm203.MoveNext();
                                for (int ard204_0 = 0; ard204_0 < pvar162.GetLength(0); ard204_0++)
                                    for (int ard204_1 = 0; ard204_1 < pvar162.GetLength(1); ard204_1++)
                                        for (int ard204_2 = 0; ard204_2 < pvar162.GetLength(2); ard204_2++)
                                        {
                                            pvar162[ard204_0, ard204_1, ard204_2] = arenm203.Current;
                                            arenm203.MoveNext();
                                        }
                                intlst160.Add(pvar162);
                            }
                        }
                        m.P23 = decoder.CheckNull() ? null : new System.Int32[intlst160.Count][,,][][][,,,][][,];
                        if (m.P23 != null)
                        {
                            for (int ard205_0 = 0; ard205_0 < m.P23.GetLength(0); ard205_0++)
                            {
                                m.P23[ard205_0] = intlst160[ard205_0];
                            }
                        }
                        break;
                    case "p24":
                        m.P24 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[][,,][]>();
                        if (m.P24 != null)
                        {
                            foreach (var el206 in decoder.GetList())
                            {
                                var intlst208 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][]>();
                                if (intlst208 != null)
                                {
                                    foreach (var el209 in decoder.GetList())
                                    {
                                        System.Collections.Generic.List<System.Int32> pvar212 = null;
                                        System.Collections.Generic.List<System.Int32[]> pvar215 = null;
                                        foreach (var tupleProps222 in decoder.GetDictionary<string>())
                                        {
                                            switch (tupleProps222)
                                            {
                                                case "Item1":
                                                    pvar212 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                    if (pvar212 != null)
                                                    {
                                                        foreach (var el213 in decoder.GetList())
                                                        {
                                                            var pvar214 = decoder.GetInt();
                                                            pvar212.Add(pvar214);
                                                        }
                                                    }
                                                    break;
                                                case "Item2":
                                                    pvar215 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[]>();
                                                    if (pvar215 != null)
                                                    {
                                                        foreach (var el216 in decoder.GetList())
                                                        {
                                                            var intlst218 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                            if (intlst218 != null)
                                                            {
                                                                foreach (var el219 in decoder.GetList())
                                                                {
                                                                    var pvar220 = decoder.GetInt();
                                                                    intlst218.Add(pvar220);
                                                                }
                                                            }
                                                            var pvar217 = decoder.CheckNull() ? null : new System.Int32[intlst218.Count];
                                                            if (pvar217 != null)
                                                            {
                                                                for (int ard221_0 = 0; ard221_0 < pvar217.GetLength(0); ard221_0++)
                                                                {
                                                                    pvar217[ard221_0] = intlst218[ard221_0];
                                                                }
                                                            }
                                                            pvar215.Add(pvar217);
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        var pv211 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32[]>>(pvar212, pvar215);
                                        var pvar210 = new System.Int32[pv211.Item1[0], pv211.Item1[1], pv211.Item1[2]][];
                                        var arenm223 = pv211.Item2.GetEnumerator();
                                        arenm223.MoveNext();
                                        for (int ard224_0 = 0; ard224_0 < pvar210.GetLength(0); ard224_0++)
                                            for (int ard224_1 = 0; ard224_1 < pvar210.GetLength(1); ard224_1++)
                                                for (int ard224_2 = 0; ard224_2 < pvar210.GetLength(2); ard224_2++)
                                                {
                                                    pvar210[ard224_0, ard224_1, ard224_2] = arenm223.Current;
                                                    arenm223.MoveNext();
                                                }
                                        intlst208.Add(pvar210);
                                    }
                                }
                                var pvar207 = decoder.CheckNull() ? null : new System.Int32[intlst208.Count][,,][];
                                if (pvar207 != null)
                                {
                                    for (int ard225_0 = 0; ard225_0 < pvar207.GetLength(0); ard225_0++)
                                    {
                                        pvar207[ard225_0] = intlst208[ard225_0];
                                    }
                                }
                                m.P24.Add(pvar207);
                            }
                        }
                        break;
                    case "p25":
                        System.Collections.Generic.List<System.Int32> pvar227 = null;
                        System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[][,,]> pvar230 = null;
                        foreach (var tupleProps275 in decoder.GetDictionary<string>())
                        {
                            switch (tupleProps275)
                            {
                                case "Item1":
                                    pvar227 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                    if (pvar227 != null)
                                    {
                                        foreach (var el228 in decoder.GetList())
                                        {
                                            var pvar229 = decoder.GetInt();
                                            pvar227.Add(pvar229);
                                        }
                                    }
                                    break;
                                case "Item2":
                                    pvar230 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[][,,]>();
                                    if (pvar230 != null)
                                    {
                                        foreach (var el231 in decoder.GetList())
                                        {
                                            var intlst233 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[,,]>();
                                            if (intlst233 != null)
                                            {
                                                foreach (var el234 in decoder.GetList())
                                                {
                                                    System.Collections.Generic.List<System.Int32> pvar237 = null;
                                                    System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>> pvar240 = null;
                                                    foreach (var tupleProps271 in decoder.GetDictionary<string>())
                                                    {
                                                        switch (tupleProps271)
                                                        {
                                                            case "Item1":
                                                                pvar237 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                if (pvar237 != null)
                                                                {
                                                                    foreach (var el238 in decoder.GetList())
                                                                    {
                                                                        var pvar239 = decoder.GetInt();
                                                                        pvar237.Add(pvar239);
                                                                    }
                                                                }
                                                                break;
                                                            case "Item2":
                                                                pvar240 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>>();
                                                                if (pvar240 != null)
                                                                {
                                                                    foreach (var el241 in decoder.GetList())
                                                                    {
                                                                        var pvar242 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>();
                                                                        if (pvar242 != null)
                                                                        {
                                                                            foreach (var el243 in decoder.GetDictionary<System.Int32>())
                                                                            {
                                                                                var pvar244 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[,][][,,]>();
                                                                                if (pvar244 != null)
                                                                                {
                                                                                    foreach (var el245 in decoder.GetList())
                                                                                    {
                                                                                        System.Collections.Generic.List<System.Int32> pvar248 = null;
                                                                                        System.Collections.Generic.List<System.String[][,,]> pvar251 = null;
                                                                                        foreach (var tupleProps268 in decoder.GetDictionary<string>())
                                                                                        {
                                                                                            switch (tupleProps268)
                                                                                            {
                                                                                                case "Item1":
                                                                                                    pvar248 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                                    if (pvar248 != null)
                                                                                                    {
                                                                                                        foreach (var el249 in decoder.GetList())
                                                                                                        {
                                                                                                            var pvar250 = decoder.GetInt();
                                                                                                            pvar248.Add(pvar250);
                                                                                                        }
                                                                                                    }
                                                                                                    break;
                                                                                                case "Item2":
                                                                                                    pvar251 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[][,,]>();
                                                                                                    if (pvar251 != null)
                                                                                                    {
                                                                                                        foreach (var el252 in decoder.GetList())
                                                                                                        {
                                                                                                            var intlst254 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[,,]>();
                                                                                                            if (intlst254 != null)
                                                                                                            {
                                                                                                                foreach (var el255 in decoder.GetList())
                                                                                                                {
                                                                                                                    System.Collections.Generic.List<System.Int32> pvar258 = null;
                                                                                                                    System.Collections.Generic.List<System.String> pvar261 = null;
                                                                                                                    foreach (var tupleProps264 in decoder.GetDictionary<string>())
                                                                                                                    {
                                                                                                                        switch (tupleProps264)
                                                                                                                        {
                                                                                                                            case "Item1":
                                                                                                                                pvar258 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                                                                                                                if (pvar258 != null)
                                                                                                                                {
                                                                                                                                    foreach (var el259 in decoder.GetList())
                                                                                                                                    {
                                                                                                                                        var pvar260 = decoder.GetInt();
                                                                                                                                        pvar258.Add(pvar260);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                break;
                                                                                                                            case "Item2":
                                                                                                                                pvar261 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
                                                                                                                                if (pvar261 != null)
                                                                                                                                {
                                                                                                                                    foreach (var el262 in decoder.GetList())
                                                                                                                                    {
                                                                                                                                        var pvar263 = decoder.GetString();
                                                                                                                                        pvar261.Add(pvar263);
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                break;
                                                                                                                        }
                                                                                                                    }
                                                                                                                    var pv257 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.String>>(pvar258, pvar261);
                                                                                                                    var pvar256 = new System.String[pv257.Item1[0], pv257.Item1[1], pv257.Item1[2]];
                                                                                                                    var arenm265 = pv257.Item2.GetEnumerator();
                                                                                                                    arenm265.MoveNext();
                                                                                                                    for (int ard266_0 = 0; ard266_0 < pvar256.GetLength(0); ard266_0++)
                                                                                                                        for (int ard266_1 = 0; ard266_1 < pvar256.GetLength(1); ard266_1++)
                                                                                                                            for (int ard266_2 = 0; ard266_2 < pvar256.GetLength(2); ard266_2++)
                                                                                                                            {
                                                                                                                                pvar256[ard266_0, ard266_1, ard266_2] = arenm265.Current;
                                                                                                                                arenm265.MoveNext();
                                                                                                                            }
                                                                                                                    intlst254.Add(pvar256);
                                                                                                                }
                                                                                                            }
                                                                                                            var pvar253 = decoder.CheckNull() ? null : new System.String[intlst254.Count][,,];
                                                                                                            if (pvar253 != null)
                                                                                                            {
                                                                                                                for (int ard267_0 = 0; ard267_0 < pvar253.GetLength(0); ard267_0++)
                                                                                                                {
                                                                                                                    pvar253[ard267_0] = intlst254[ard267_0];
                                                                                                                }
                                                                                                            }
                                                                                                            pvar251.Add(pvar253);
                                                                                                        }
                                                                                                    }
                                                                                                    break;
                                                                                            }
                                                                                        }
                                                                                        var pv247 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.String[][,,]>>(pvar248, pvar251);
                                                                                        var pvar246 = new System.String[pv247.Item1[0], pv247.Item1[1]][][,,];
                                                                                        var arenm269 = pv247.Item2.GetEnumerator();
                                                                                        arenm269.MoveNext();
                                                                                        for (int ard270_0 = 0; ard270_0 < pvar246.GetLength(0); ard270_0++)
                                                                                            for (int ard270_1 = 0; ard270_1 < pvar246.GetLength(1); ard270_1++)
                                                                                            {
                                                                                                pvar246[ard270_0, ard270_1] = arenm269.Current;
                                                                                                arenm269.MoveNext();
                                                                                            }
                                                                                        pvar244.Add(pvar246);
                                                                                    }
                                                                                }
                                                                                pvar242.Add(el243, pvar244);
                                                                            }
                                                                        }
                                                                        pvar240.Add(pvar242);
                                                                    }
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    var pv236 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>>>(pvar237, pvar240);
                                                    var pvar235 = new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[pv236.Item1[0], pv236.Item1[1], pv236.Item1[2]];
                                                    var arenm272 = pv236.Item2.GetEnumerator();
                                                    arenm272.MoveNext();
                                                    for (int ard273_0 = 0; ard273_0 < pvar235.GetLength(0); ard273_0++)
                                                        for (int ard273_1 = 0; ard273_1 < pvar235.GetLength(1); ard273_1++)
                                                            for (int ard273_2 = 0; ard273_2 < pvar235.GetLength(2); ard273_2++)
                                                            {
                                                                pvar235[ard273_0, ard273_1, ard273_2] = arenm272.Current;
                                                                arenm272.MoveNext();
                                                            }
                                                    intlst233.Add(pvar235);
                                                }
                                            }
                                            var pvar232 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[intlst233.Count][,,];
                                            if (pvar232 != null)
                                            {
                                                for (int ard274_0 = 0; ard274_0 < pvar232.GetLength(0); ard274_0++)
                                                {
                                                    pvar232[ard274_0] = intlst233[ard274_0];
                                                }
                                            }
                                            pvar230.Add(pvar232);
                                        }
                                    }
                                    break;
                            }
                        }
                        var pv226 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[][,,]>>(pvar227, pvar230);
                        m.P25 = new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[pv226.Item1[0], pv226.Item1[1], pv226.Item1[2], pv226.Item1[3]][][,,];
                        var arenm276 = pv226.Item2.GetEnumerator();
                        arenm276.MoveNext();
                        for (int ard277_0 = 0; ard277_0 < m.P25.GetLength(0); ard277_0++)
                            for (int ard277_1 = 0; ard277_1 < m.P25.GetLength(1); ard277_1++)
                                for (int ard277_2 = 0; ard277_2 < m.P25.GetLength(2); ard277_2++)
                                    for (int ard277_3 = 0; ard277_3 < m.P25.GetLength(3); ard277_3++)
                                    {
                                        m.P25[ard277_0, ard277_1, ard277_2, ard277_3] = arenm276.Current;
                                        arenm276.MoveNext();
                                    }
                        break;
                    default:
                        decoder.SkipValue();
                        break;
                }
            }
            return m;
        }

        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder encoder = new Biser.Encoder(existingEncoder);


            encoder.Add(P1);
            encoder.Add(P2);
            encoder.Add(P3);
            encoder.Add(P4, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add(r2.Key);
                    encoder.Add(r2.Value.Item1);
                    encoder.Add(r2.Value.Item2);
                });
            });
            encoder.Add(P5, (r3) => {
                encoder.Add(r3.Key);
                encoder.Add(r3.Value.Item1);
                encoder.Add(r3.Value.Item2);
            });
            encoder.Add(P6.Item1);
            encoder.Add(P6.Item2);
            encoder.Add(P6.Item3.Item1, (r4) => {
                encoder.Add(r4);
            });
            encoder.Add(P6.Item3.Item2);
            encoder.Add(P7, (r5) => {
                encoder.Add(r5);
            });
            encoder.Add(P8, (r6) => {
                encoder.Add(r6.Key);
                encoder.Add(r6.Value, (r7) => {
                    encoder.Add(r7);
                });
            });
            encoder.Add(P10);
            if (P11 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it8 = 0; it8 < P11.Rank; it8++)
                    encoder.Add(P11.GetLength(it8));
                foreach (var el9 in P11)
                    if (el9 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it10 = 0; it10 < el9.Rank; it10++)
                            encoder.Add(el9.GetLength(it10));
                        foreach (var el11 in el9)
                            encoder.Add(el11);
                    }
            }
            if (P12 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it12 = 0; it12 < P12.Rank; it12++)
                    encoder.Add(P12.GetLength(it12));
                foreach (var el13 in P12)
                    encoder.Add(el13);
            }
            encoder.Add(P13, (r14) => {
                encoder.Add(r14, (r15) => {
                    encoder.Add(r15);
                });
            });
            encoder.Add(P14, (r16) => {
                encoder.Add(r16.Key);
                encoder.Add(r16.Value);
            });
            encoder.Add(P15.Item1);
            encoder.Add(P15.Item2);
            encoder.Add(P15.Item3);
            encoder.Add(P15.Item4);
            encoder.Add(P16, (r17) => {
                encoder.Add(r17, (r18) => {
                    encoder.Add(r18.Key);
                    encoder.Add(r18.Value.Item1);
                    encoder.Add(r18.Value.Item2);
                });
            });
            if (P17 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it19 = 0; it19 < P17.Rank; it19++)
                    encoder.Add(P17.GetLength(it19));
                foreach (var el20 in P17)
                    encoder.Add(el20);
            }
            if (P18 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it21 = 0; it21 < P18.Rank; it21++)
                    encoder.Add(P18.GetLength(it21));
                foreach (var el22 in P18)
                    if (el22 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it23 = 0; it23 < el22.Rank; it23++)
                            encoder.Add(el22.GetLength(it23));
                        foreach (var el24 in el22)
                            if (el24 == null)
                                encoder.Add((byte)1);
                            else
                            {
                                encoder.Add((byte)0);
                                for (int it25 = 0; it25 < el24.Rank; it25++)
                                    encoder.Add(el24.GetLength(it25));
                                foreach (var el26 in el24)
                                    if (el26 == null)
                                        encoder.Add((byte)1);
                                    else
                                    {
                                        encoder.Add((byte)0);
                                        for (int it27 = 0; it27 < el26.Rank; it27++)
                                            encoder.Add(el26.GetLength(it27));
                                        foreach (var el28 in el26)
                                            if (el28 == null)
                                                encoder.Add((byte)1);
                                            else
                                            {
                                                encoder.Add((byte)0);
                                                for (int it29 = 0; it29 < el28.Rank; it29++)
                                                    encoder.Add(el28.GetLength(it29));
                                                foreach (var el30 in el28)
                                                    encoder.Add(el30, (r31) => {
                                                        if (r31 == null)
                                                            encoder.Add((byte)1);
                                                        else
                                                        {
                                                            encoder.Add((byte)0);
                                                            for (int it32 = 0; it32 < r31.Rank; it32++)
                                                                encoder.Add(r31.GetLength(it32));
                                                            foreach (var el33 in r31)
                                                                if (el33 == null)
                                                                    encoder.Add((byte)1);
                                                                else
                                                                {
                                                                    encoder.Add((byte)0);
                                                                    for (int it34 = 0; it34 < el33.Rank; it34++)
                                                                        encoder.Add(el33.GetLength(it34));
                                                                    foreach (var el35 in el33)
                                                                        if (el35 == null)
                                                                            encoder.Add((byte)1);
                                                                        else
                                                                        {
                                                                            encoder.Add((byte)0);
                                                                            for (int it36 = 0; it36 < el35.Rank; it36++)
                                                                                encoder.Add(el35.GetLength(it36));
                                                                            foreach (var el37 in el35)
                                                                                encoder.Add(el37);
                                                                        }
                                                                }
                                                        }
                                                    });
                                            }
                                    }
                            }
                    }
            }
            encoder.Add(P19, (r38) => {
                encoder.Add(r38.Key);
                encoder.Add(r38.Value, (r39) => {
                    if (r39 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it40 = 0; it40 < r39.Rank; it40++)
                            encoder.Add(r39.GetLength(it40));
                        foreach (var el41 in r39)
                            if (el41 == null)
                                encoder.Add((byte)1);
                            else
                            {
                                encoder.Add((byte)0);
                                for (int it42 = 0; it42 < el41.Rank; it42++)
                                    encoder.Add(el41.GetLength(it42));
                                foreach (var el43 in el41)
                                    if (el43 == null)
                                        encoder.Add((byte)1);
                                    else
                                    {
                                        encoder.Add((byte)0);
                                        for (int it44 = 0; it44 < el43.Rank; it44++)
                                            encoder.Add(el43.GetLength(it44));
                                        foreach (var el45 in el43)
                                            encoder.Add(el45);
                                    }
                            }
                    }
                });
            });
            encoder.Add(P20, (r46) => {
                if (r46 == null)
                    encoder.Add((byte)1);
                else
                {
                    encoder.Add((byte)0);
                    for (int it47 = 0; it47 < r46.Rank; it47++)
                        encoder.Add(r46.GetLength(it47));
                    foreach (var el48 in r46)
                        encoder.Add(el48);
                }
            });
            encoder.Add(P21);
            encoder.Add(P22);
            encoder.Add(A1);
            if (P23 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it49 = 0; it49 < P23.Rank; it49++)
                    encoder.Add(P23.GetLength(it49));
                foreach (var el50 in P23)
                    if (el50 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it51 = 0; it51 < el50.Rank; it51++)
                            encoder.Add(el50.GetLength(it51));
                        foreach (var el52 in el50)
                            if (el52 == null)
                                encoder.Add((byte)1);
                            else
                            {
                                encoder.Add((byte)0);
                                for (int it53 = 0; it53 < el52.Rank; it53++)
                                    encoder.Add(el52.GetLength(it53));
                                foreach (var el54 in el52)
                                    if (el54 == null)
                                        encoder.Add((byte)1);
                                    else
                                    {
                                        encoder.Add((byte)0);
                                        for (int it55 = 0; it55 < el54.Rank; it55++)
                                            encoder.Add(el54.GetLength(it55));
                                        foreach (var el56 in el54)
                                            if (el56 == null)
                                                encoder.Add((byte)1);
                                            else
                                            {
                                                encoder.Add((byte)0);
                                                for (int it57 = 0; it57 < el56.Rank; it57++)
                                                    encoder.Add(el56.GetLength(it57));
                                                foreach (var el58 in el56)
                                                    if (el58 == null)
                                                        encoder.Add((byte)1);
                                                    else
                                                    {
                                                        encoder.Add((byte)0);
                                                        for (int it59 = 0; it59 < el58.Rank; it59++)
                                                            encoder.Add(el58.GetLength(it59));
                                                        foreach (var el60 in el58)
                                                            if (el60 == null)
                                                                encoder.Add((byte)1);
                                                            else
                                                            {
                                                                encoder.Add((byte)0);
                                                                for (int it61 = 0; it61 < el60.Rank; it61++)
                                                                    encoder.Add(el60.GetLength(it61));
                                                                foreach (var el62 in el60)
                                                                    encoder.Add(el62);
                                                            }
                                                    }
                                            }
                                    }
                            }
                    }
            }
            encoder.Add(P24, (r63) => {
                if (r63 == null)
                    encoder.Add((byte)1);
                else
                {
                    encoder.Add((byte)0);
                    for (int it64 = 0; it64 < r63.Rank; it64++)
                        encoder.Add(r63.GetLength(it64));
                    foreach (var el65 in r63)
                        if (el65 == null)
                            encoder.Add((byte)1);
                        else
                        {
                            encoder.Add((byte)0);
                            for (int it66 = 0; it66 < el65.Rank; it66++)
                                encoder.Add(el65.GetLength(it66));
                            foreach (var el67 in el65)
                                if (el67 == null)
                                    encoder.Add((byte)1);
                                else
                                {
                                    encoder.Add((byte)0);
                                    for (int it68 = 0; it68 < el67.Rank; it68++)
                                        encoder.Add(el67.GetLength(it68));
                                    foreach (var el69 in el67)
                                        encoder.Add(el69);
                                }
                        }
                }
            });
            if (P25 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it70 = 0; it70 < P25.Rank; it70++)
                    encoder.Add(P25.GetLength(it70));
                foreach (var el71 in P25)
                    if (el71 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it72 = 0; it72 < el71.Rank; it72++)
                            encoder.Add(el71.GetLength(it72));
                        foreach (var el73 in el71)
                            if (el73 == null)
                                encoder.Add((byte)1);
                            else
                            {
                                encoder.Add((byte)0);
                                for (int it74 = 0; it74 < el73.Rank; it74++)
                                    encoder.Add(el73.GetLength(it74));
                                foreach (var el75 in el73)
                                    encoder.Add(el75, (r76) => {
                                        encoder.Add(r76.Key);
                                        encoder.Add(r76.Value, (r77) => {
                                            if (r77 == null)
                                                encoder.Add((byte)1);
                                            else
                                            {
                                                encoder.Add((byte)0);
                                                for (int it78 = 0; it78 < r77.Rank; it78++)
                                                    encoder.Add(r77.GetLength(it78));
                                                foreach (var el79 in r77)
                                                    if (el79 == null)
                                                        encoder.Add((byte)1);
                                                    else
                                                    {
                                                        encoder.Add((byte)0);
                                                        for (int it80 = 0; it80 < el79.Rank; it80++)
                                                            encoder.Add(el79.GetLength(it80));
                                                        foreach (var el81 in el79)
                                                            if (el81 == null)
                                                                encoder.Add((byte)1);
                                                            else
                                                            {
                                                                encoder.Add((byte)0);
                                                                for (int it82 = 0; it82 < el81.Rank; it82++)
                                                                    encoder.Add(el81.GetLength(it82));
                                                                foreach (var el83 in el81)
                                                                    encoder.Add(el83);
                                                            }
                                                    }
                                            }
                                        });
                                    });
                            }
                    }
            }

            return encoder;
        }


        public static TS6 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null)
        {
            Biser.Decoder decoder = null;
            if (extDecoder == null)
            {
                if (enc == null || enc.Length == 0)
                    return null;
                decoder = new Biser.Decoder(enc);
            }
            else
            {
                if (extDecoder.CheckNull())
                    return null;
                else
                    decoder = extDecoder;
            }

            TS6 m = new TS6();



            m.P1 = decoder.GetString();
            m.P2 = decoder.GetInt();
            m.P3 = decoder.GetDateTime();
            m.P4 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.DateTime, System.Tuple<System.Int32, System.String>>>();
            if (m.P4 != null)
            {
                decoder.GetCollection(() => {
                    var pvar1 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.DateTime, System.Tuple<System.Int32, System.String>>();
                    if (pvar1 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar2 = decoder.GetDateTime();
                            return pvar2;
                        },
                    () => {
                        System.Int32 pvar4 = 0;
                        System.String pvar5 = null;
                        pvar4 = decoder.GetInt();
                        pvar5 = decoder.GetString();
                        var pvar3 = new Tuple<System.Int32, System.String>(pvar4, pvar5);
                        return pvar3;
                    }, pvar1, true);
                    }
                    return pvar1;
                }, m.P4, true);
            }
            m.P5 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>();
            if (m.P5 != null)
            {
                decoder.GetCollection(() => {
                    var pvar6 = decoder.GetInt();
                    return pvar6;
                },
            () => {
                System.Int32 pvar8 = 0;
                System.String pvar9 = null;
                pvar8 = decoder.GetInt();
                pvar9 = decoder.GetString();
                var pvar7 = new Tuple<System.Int32, System.String>(pvar8, pvar9);
                return pvar7;
            }, m.P5, true);
            }
            System.Int32 pvar10 = 0;
            System.String pvar11 = null;
            System.Tuple<System.Collections.Generic.List<System.String>, System.DateTime> pvar12 = default(System.Tuple<System.Collections.Generic.List<System.String>, System.DateTime>);
            pvar10 = decoder.GetInt();
            pvar11 = decoder.GetString();
            System.Collections.Generic.List<System.String> pvar13 = null;
            System.DateTime pvar15 = default(DateTime);
            pvar13 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
            if (pvar13 != null)
            {
                decoder.GetCollection(() => {
                    var pvar14 = decoder.GetString();
                    return pvar14;
                }, pvar13, true);
            }
            pvar15 = decoder.GetDateTime();
            pvar12 = new Tuple<System.Collections.Generic.List<System.String>, System.DateTime>(pvar13, pvar15);
            m.P6 = new Tuple<System.Int32, System.String, System.Tuple<System.Collections.Generic.List<System.String>, System.DateTime>>(pvar10, pvar11, pvar12);
            m.P7 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
            if (m.P7 != null)
            {
                decoder.GetCollection(() => {
                    var pvar16 = decoder.GetString();
                    return pvar16;
                }, m.P7, true);
            }
            m.P8 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String>>();
            if (m.P8 != null)
            {
                decoder.GetCollection(() => {
                    var pvar17 = decoder.GetInt();
                    return pvar17;
                },
            () => {
                var pvar18 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String>();
                if (pvar18 != null)
                {
                    decoder.GetCollection(() => {
                        var pvar19 = decoder.GetString();
                        return pvar19;
                    }, pvar18, true);
                }
                return pvar18;
            }, m.P8, true);
            }
            m.P10 = decoder.GetShort_NULL();
            m.P11 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][];
            if (m.P11 != null)
            {
                for (int ard20_0 = 0; ard20_0 < m.P11.GetLength(0); ard20_0++)
                {
                    m.P11[ard20_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()];
                    if (m.P11[ard20_0] != null)
                    {
                        for (int ard22_0 = 0; ard22_0 < m.P11[ard20_0].GetLength(0); ard22_0++)
                        {
                            m.P11[ard20_0][ard22_0] = decoder.GetInt();
                        }
                    }
                }
            }
            m.P12 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];
            if (m.P12 != null)
            {
                for (int ard24_0 = 0; ard24_0 < m.P12.GetLength(0); ard24_0++)
                    for (int ard25_1 = 0; ard25_1 < m.P12.GetLength(1); ard25_1++)
                        for (int ard26_2 = 0; ard26_2 < m.P12.GetLength(2); ard26_2++)
                        {
                            m.P12[ard24_0, ard25_1, ard26_2] = decoder.GetInt();
                        }
            }
            m.P13 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>();
            if (m.P13 != null)
            {
                decoder.GetCollection(() => {
                    var pvar28 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                    if (pvar28 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar29 = decoder.GetInt();
                            return pvar29;
                        }, pvar28, true);
                    }
                    return pvar28;
                }, m.P13, true);
            }
            m.P14 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.String>();
            if (m.P14 != null)
            {
                decoder.GetCollection(() => {
                    var pvar30 = decoder.GetInt();
                    return pvar30;
                },
            () => {
                var pvar31 = decoder.GetString();
                return pvar31;
            }, m.P14, true);
            }
            System.Int32 pvar32 = 0;
            System.String pvar33 = null;
            System.DateTime pvar34 = default(DateTime);
            System.Byte[] pvar35 = null;
            pvar32 = decoder.GetInt();
            pvar33 = decoder.GetString();
            pvar34 = decoder.GetDateTime();
            pvar35 = decoder.GetByteArray();
            m.P15 = new Tuple<System.Int32, System.String, System.DateTime, System.Byte[]>(pvar32, pvar33, pvar34, pvar35);
            m.P16 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>>();
            if (m.P16 != null)
            {
                decoder.GetCollection(() => {
                    var pvar36 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>();
                    if (pvar36 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar37 = decoder.GetInt();
                            return pvar37;
                        },
                    () => {
                        System.Int32 pvar39 = 0;
                        System.String pvar40 = null;
                        pvar39 = decoder.GetInt();
                        pvar40 = decoder.GetString();
                        var pvar38 = new Tuple<System.Int32, System.String>(pvar39, pvar40);
                        return pvar38;
                    }, pvar36, true);
                    }
                    return pvar36;
                }, m.P16, true);
            }
            m.P17 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()];
            if (m.P17 != null)
            {
                for (int ard41_0 = 0; ard41_0 < m.P17.GetLength(0); ard41_0++)
                {
                    m.P17[ard41_0] = decoder.GetInt();
                }
            }
            m.P18 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][][][,,,,];
            if (m.P18 != null)
            {
                for (int ard43_0 = 0; ard43_0 < m.P18.GetLength(0); ard43_0++)
                    for (int ard44_1 = 0; ard44_1 < m.P18.GetLength(1); ard44_1++)
                        for (int ard45_2 = 0; ard45_2 < m.P18.GetLength(2); ard45_2++)
                        {
                            m.P18[ard43_0, ard44_1, ard45_2] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt()][][][,,,,];
                            if (m.P18[ard43_0, ard44_1, ard45_2] != null)
                            {
                                for (int ard47_0 = 0; ard47_0 < m.P18[ard43_0, ard44_1, ard45_2].GetLength(0); ard47_0++)
                                {
                                    m.P18[ard43_0, ard44_1, ard45_2][ard47_0] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt()][][,,,,];
                                    if (m.P18[ard43_0, ard44_1, ard45_2][ard47_0] != null)
                                    {
                                        for (int ard49_0 = 0; ard49_0 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0].GetLength(0); ard49_0++)
                                        {
                                            m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt()][,,,,];
                                            if (m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0] != null)
                                            {
                                                for (int ard51_0 = 0; ard51_0 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0].GetLength(0); ard51_0++)
                                                {
                                                    m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt(), decoder.GetInt(), decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];
                                                    if (m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0] != null)
                                                    {
                                                        for (int ard53_0 = 0; ard53_0 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0].GetLength(0); ard53_0++)
                                                            for (int ard54_1 = 0; ard54_1 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0].GetLength(1); ard54_1++)
                                                                for (int ard55_2 = 0; ard55_2 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0].GetLength(2); ard55_2++)
                                                                    for (int ard56_3 = 0; ard56_3 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0].GetLength(3); ard56_3++)
                                                                        for (int ard57_4 = 0; ard57_4 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0].GetLength(4); ard57_4++)
                                                                        {
                                                                            m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0][ard53_0, ard54_1, ard55_2, ard56_3, ard57_4] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>();
                                                                            if (m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0][ard53_0, ard54_1, ard55_2, ard56_3, ard57_4] != null)
                                                                            {
                                                                                decoder.GetCollection(() => {
                                                                                    var pvar59 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][,,];
                                                                                    if (pvar59 != null)
                                                                                    {
                                                                                        for (int ard60_0 = 0; ard60_0 < pvar59.GetLength(0); ard60_0++)
                                                                                            for (int ard61_1 = 0; ard61_1 < pvar59.GetLength(1); ard61_1++)
                                                                                                for (int ard62_2 = 0; ard62_2 < pvar59.GetLength(2); ard62_2++)
                                                                                                {
                                                                                                    pvar59[ard60_0, ard61_1, ard62_2] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,];
                                                                                                    if (pvar59[ard60_0, ard61_1, ard62_2] != null)
                                                                                                    {
                                                                                                        for (int ard64_0 = 0; ard64_0 < pvar59[ard60_0, ard61_1, ard62_2].GetLength(0); ard64_0++)
                                                                                                        {
                                                                                                            pvar59[ard60_0, ard61_1, ard62_2][ard64_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];
                                                                                                            if (pvar59[ard60_0, ard61_1, ard62_2][ard64_0] != null)
                                                                                                            {
                                                                                                                for (int ard66_0 = 0; ard66_0 < pvar59[ard60_0, ard61_1, ard62_2][ard64_0].GetLength(0); ard66_0++)
                                                                                                                    for (int ard67_1 = 0; ard67_1 < pvar59[ard60_0, ard61_1, ard62_2][ard64_0].GetLength(1); ard67_1++)
                                                                                                                        for (int ard68_2 = 0; ard68_2 < pvar59[ard60_0, ard61_1, ard62_2][ard64_0].GetLength(2); ard68_2++)
                                                                                                                        {
                                                                                                                            pvar59[ard60_0, ard61_1, ard62_2][ard64_0][ard66_0, ard67_1, ard68_2] = decoder.GetInt();
                                                                                                                        }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                    }
                                                                                    return pvar59;
                                                                                }, m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0][ard53_0, ard54_1, ard55_2, ard56_3, ard57_4], true);
                                                                            }
                                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
            }
            m.P19 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>();
            if (m.P19 != null)
            {
                decoder.GetCollection(() => {
                    var pvar70 = decoder.GetInt();
                    return pvar70;
                },
            () => {
                var pvar71 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[,][][,,]>();
                if (pvar71 != null)
                {
                    decoder.GetCollection(() => {
                        var pvar72 = decoder.CheckNull() ? null : new System.String[decoder.GetInt(), decoder.GetInt()][][,,];
                        if (pvar72 != null)
                        {
                            for (int ard73_0 = 0; ard73_0 < pvar72.GetLength(0); ard73_0++)
                                for (int ard74_1 = 0; ard74_1 < pvar72.GetLength(1); ard74_1++)
                                {
                                    pvar72[ard73_0, ard74_1] = decoder.CheckNull() ? null : new System.String[decoder.GetInt()][,,];
                                    if (pvar72[ard73_0, ard74_1] != null)
                                    {
                                        for (int ard76_0 = 0; ard76_0 < pvar72[ard73_0, ard74_1].GetLength(0); ard76_0++)
                                        {
                                            pvar72[ard73_0, ard74_1][ard76_0] = decoder.CheckNull() ? null : new System.String[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];
                                            if (pvar72[ard73_0, ard74_1][ard76_0] != null)
                                            {
                                                for (int ard78_0 = 0; ard78_0 < pvar72[ard73_0, ard74_1][ard76_0].GetLength(0); ard78_0++)
                                                    for (int ard79_1 = 0; ard79_1 < pvar72[ard73_0, ard74_1][ard76_0].GetLength(1); ard79_1++)
                                                        for (int ard80_2 = 0; ard80_2 < pvar72[ard73_0, ard74_1][ard76_0].GetLength(2); ard80_2++)
                                                        {
                                                            pvar72[ard73_0, ard74_1][ard76_0][ard78_0, ard79_1, ard80_2] = decoder.GetString();
                                                        }
                                            }
                                        }
                                    }
                                }
                        }
                        return pvar72;
                    }, pvar71, true);
                }
                return pvar71;
            }, m.P19, true);
            }
            m.P20 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[]>();
            if (m.P20 != null)
            {
                decoder.GetCollection(() => {
                    var pvar82 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()];
                    if (pvar82 != null)
                    {
                        for (int ard83_0 = 0; ard83_0 < pvar82.GetLength(0); ard83_0++)
                        {
                            pvar82[ard83_0] = decoder.GetInt();
                        }
                    }
                    return pvar82;
                }, m.P20, true);
            }
            m.P21 = BiserTest_Net.TS2.BiserDecode(null, decoder);
            m.P22 = BiserTest_Net.TS3.BiserDecode(null, decoder);
            m.A1 = BiserTest_Net.TS3.BiserDecode(null, decoder);
            m.P23 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,][][][,,,][][,];
            if (m.P23 != null)
            {
                for (int ard85_0 = 0; ard85_0 < m.P23.GetLength(0); ard85_0++)
                {
                    m.P23[ard85_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][][,,,][][,];
                    if (m.P23[ard85_0] != null)
                    {
                        for (int ard87_0 = 0; ard87_0 < m.P23[ard85_0].GetLength(0); ard87_0++)
                            for (int ard88_1 = 0; ard88_1 < m.P23[ard85_0].GetLength(1); ard88_1++)
                                for (int ard89_2 = 0; ard89_2 < m.P23[ard85_0].GetLength(2); ard89_2++)
                                {
                                    m.P23[ard85_0][ard87_0, ard88_1, ard89_2] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][][,,,][][,];
                                    if (m.P23[ard85_0][ard87_0, ard88_1, ard89_2] != null)
                                    {
                                        for (int ard91_0 = 0; ard91_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2].GetLength(0); ard91_0++)
                                        {
                                            m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,,][][,];
                                            if (m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0] != null)
                                            {
                                                for (int ard93_0 = 0; ard93_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0].GetLength(0); ard93_0++)
                                                {
                                                    m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][,];
                                                    if (m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0] != null)
                                                    {
                                                        for (int ard95_0 = 0; ard95_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(0); ard95_0++)
                                                            for (int ard96_1 = 0; ard96_1 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(1); ard96_1++)
                                                                for (int ard97_2 = 0; ard97_2 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(2); ard97_2++)
                                                                    for (int ard98_3 = 0; ard98_3 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(3); ard98_3++)
                                                                    {
                                                                        m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,];
                                                                        if (m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3] != null)
                                                                        {
                                                                            for (int ard100_0 = 0; ard100_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3].GetLength(0); ard100_0++)
                                                                            {
                                                                                m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3][ard100_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt()];
                                                                                if (m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3][ard100_0] != null)
                                                                                {
                                                                                    for (int ard102_0 = 0; ard102_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3][ard100_0].GetLength(0); ard102_0++)
                                                                                        for (int ard103_1 = 0; ard103_1 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3][ard100_0].GetLength(1); ard103_1++)
                                                                                        {
                                                                                            m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3][ard100_0][ard102_0, ard103_1] = decoder.GetInt();
                                                                                        }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                    }
                }
            }
            m.P24 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[][,,][]>();
            if (m.P24 != null)
            {
                decoder.GetCollection(() => {
                    var pvar105 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,][];
                    if (pvar105 != null)
                    {
                        for (int ard106_0 = 0; ard106_0 < pvar105.GetLength(0); ard106_0++)
                        {
                            pvar105[ard106_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][];
                            if (pvar105[ard106_0] != null)
                            {
                                for (int ard108_0 = 0; ard108_0 < pvar105[ard106_0].GetLength(0); ard108_0++)
                                    for (int ard109_1 = 0; ard109_1 < pvar105[ard106_0].GetLength(1); ard109_1++)
                                        for (int ard110_2 = 0; ard110_2 < pvar105[ard106_0].GetLength(2); ard110_2++)
                                        {
                                            pvar105[ard106_0][ard108_0, ard109_1, ard110_2] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()];
                                            if (pvar105[ard106_0][ard108_0, ard109_1, ard110_2] != null)
                                            {
                                                for (int ard112_0 = 0; ard112_0 < pvar105[ard106_0][ard108_0, ard109_1, ard110_2].GetLength(0); ard112_0++)
                                                {
                                                    pvar105[ard106_0][ard108_0, ard109_1, ard110_2][ard112_0] = decoder.GetInt();
                                                }
                                            }
                                        }
                            }
                        }
                    }
                    return pvar105;
                }, m.P24, true);
            }
            m.P25 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[decoder.GetInt(), decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][,,];
            if (m.P25 != null)
            {
                for (int ard114_0 = 0; ard114_0 < m.P25.GetLength(0); ard114_0++)
                    for (int ard115_1 = 0; ard115_1 < m.P25.GetLength(1); ard115_1++)
                        for (int ard116_2 = 0; ard116_2 < m.P25.GetLength(2); ard116_2++)
                            for (int ard117_3 = 0; ard117_3 < m.P25.GetLength(3); ard117_3++)
                            {
                                m.P25[ard114_0, ard115_1, ard116_2, ard117_3] = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[decoder.GetInt()][,,];
                                if (m.P25[ard114_0, ard115_1, ard116_2, ard117_3] != null)
                                {
                                    for (int ard119_0 = 0; ard119_0 < m.P25[ard114_0, ard115_1, ard116_2, ard117_3].GetLength(0); ard119_0++)
                                    {
                                        m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0] = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];
                                        if (m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0] != null)
                                        {
                                            for (int ard121_0 = 0; ard121_0 < m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0].GetLength(0); ard121_0++)
                                                for (int ard122_1 = 0; ard122_1 < m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0].GetLength(1); ard122_1++)
                                                    for (int ard123_2 = 0; ard123_2 < m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0].GetLength(2); ard123_2++)
                                                    {
                                                        m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0][ard121_0, ard122_1, ard123_2] = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.String[,][][,,]>>();
                                                        if (m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0][ard121_0, ard122_1, ard123_2] != null)
                                                        {
                                                            decoder.GetCollection(() => {
                                                                var pvar125 = decoder.GetInt();
                                                                return pvar125;
                                                            },
                                                        () => {
                                                            var pvar126 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.String[,][][,,]>();
                                                            if (pvar126 != null)
                                                            {
                                                                decoder.GetCollection(() => {
                                                                    var pvar127 = decoder.CheckNull() ? null : new System.String[decoder.GetInt(), decoder.GetInt()][][,,];
                                                                    if (pvar127 != null)
                                                                    {
                                                                        for (int ard128_0 = 0; ard128_0 < pvar127.GetLength(0); ard128_0++)
                                                                            for (int ard129_1 = 0; ard129_1 < pvar127.GetLength(1); ard129_1++)
                                                                            {
                                                                                pvar127[ard128_0, ard129_1] = decoder.CheckNull() ? null : new System.String[decoder.GetInt()][,,];
                                                                                if (pvar127[ard128_0, ard129_1] != null)
                                                                                {
                                                                                    for (int ard131_0 = 0; ard131_0 < pvar127[ard128_0, ard129_1].GetLength(0); ard131_0++)
                                                                                    {
                                                                                        pvar127[ard128_0, ard129_1][ard131_0] = decoder.CheckNull() ? null : new System.String[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];
                                                                                        if (pvar127[ard128_0, ard129_1][ard131_0] != null)
                                                                                        {
                                                                                            for (int ard133_0 = 0; ard133_0 < pvar127[ard128_0, ard129_1][ard131_0].GetLength(0); ard133_0++)
                                                                                                for (int ard134_1 = 0; ard134_1 < pvar127[ard128_0, ard129_1][ard131_0].GetLength(1); ard134_1++)
                                                                                                    for (int ard135_2 = 0; ard135_2 < pvar127[ard128_0, ard129_1][ard131_0].GetLength(2); ard135_2++)
                                                                                                    {
                                                                                                        pvar127[ard128_0, ard129_1][ard131_0][ard133_0, ard134_1, ard135_2] = decoder.GetString();
                                                                                                    }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                    }
                                                                    return pvar127;
                                                                }, pvar126, true);
                                                            }
                                                            return pvar126;
                                                        }, m.P25[ard114_0, ard115_1, ard116_2, ard117_3][ard119_0][ard121_0, ard122_1, ard123_2], true);
                                                        }
                                                    }
                                        }
                                    }
                                }
                            }
            }


            return m;
        }


    }
}