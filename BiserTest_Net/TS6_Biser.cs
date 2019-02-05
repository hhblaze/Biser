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
            encoder.Add("P5", P5, (r1) => {
                encoder.Add((r1 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r1.Item1);}}, { "Item2", () => {
encoder.Add(r1.Item2);}}});
            });
            encoder.Add("P6", (P6 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(P6.Item1);}}, { "Item2", () => {
encoder.Add(P6.Item2);}}, { "Item3", () => {
encoder.Add((P6.Item3 == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(P6.Item3.Item1, (r1) => {
encoder.Add(r1);});}}, { "Item2", () => {
encoder.Add(P6.Item3.Item2);}}});}}});
            encoder.Add("P7", P7, (r1) => {
                encoder.Add(r1);
            });
            encoder.Add("P8", P8, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add(r2);
                });
            });
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
                        m.P4 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.DateTime, Tuple<System.Int32, System.String>>>();
                        if (m.P4 != null)
                        {
                            foreach (var el1 in decoder.GetList())
                            {
                                var pvar2 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.DateTime, Tuple<System.Int32, System.String>>();
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
                        m.P5 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, Tuple<System.Int32, System.String>>();
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
                        Tuple<System.Collections.Generic.List<System.String>, System.DateTime> pvar15 = default(Tuple<System.Collections.Generic.List<System.String>, System.DateTime>);
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
                        m.P6 = new Tuple<System.Int32, System.String, Tuple<System.Collections.Generic.List<System.String>, System.DateTime>>(pvar13, pvar14, pvar15);
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
            encoder.Add(P5, (r1) => {
                encoder.Add(r1.Key);
                encoder.Add(r1.Value.Item1);
                encoder.Add(r1.Value.Item2);
            });
            encoder.Add(P6.Item1);
            encoder.Add(P6.Item2);
            encoder.Add(P6.Item3.Item1, (r1) => {
                encoder.Add(r1);
            });
            encoder.Add(P6.Item3.Item2);
            encoder.Add(P7, (r1) => {
                encoder.Add(r1);
            });
            encoder.Add(P8, (r1) => {
                encoder.Add(r1.Key);
                encoder.Add(r1.Value, (r2) => {
                    encoder.Add(r2);
                });
            });

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
                /*
                if (decoder.CheckNull())
                    return null;
				*/
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
            m.P4 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.DateTime, Tuple<System.Int32, System.String>>>();
            if (m.P4 != null)
            {
                decoder.GetCollection(() => {
                    var pvar1 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.DateTime, Tuple<System.Int32, System.String>>();
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
            m.P5 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, Tuple<System.Int32, System.String>>();
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
            Tuple<System.Collections.Generic.List<System.String>, System.DateTime> pvar12 = default(Tuple<System.Collections.Generic.List<System.String>, System.DateTime>);
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
            m.P6 = new Tuple<System.Int32, System.String, Tuple<System.Collections.Generic.List<System.String>, System.DateTime>>(pvar10, pvar11, pvar12);
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


            return m;
        }


    }
}