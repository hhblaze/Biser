using System;
using System.Collections.Generic;
using System.Linq;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IEncoder
    {


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
            encoder.Add(P13, (r12) => {
                encoder.Add(r12, (r13) => {
                    encoder.Add(r13);
                });
            });
            encoder.Add(P14, (r14) => {
                encoder.Add(r14.Key);
                encoder.Add(r14.Value);
            });
            encoder.Add(P15.Item1);
            encoder.Add(P15.Item2);
            encoder.Add(P15.Item3);
            encoder.Add(P15.Item4);
            encoder.Add(P16, (r15) => {
                encoder.Add(r15, (r16) => {
                    encoder.Add(r16.Key);
                    encoder.Add(r16.Value.Item1);
                    encoder.Add(r16.Value.Item2);
                });
            });
            encoder.Add(P20, (r17) => {
                if (r17 == null)
                    encoder.Add((byte)1);
                else
                {
                    encoder.Add((byte)0);
                    for (int it18 = 0; it18 < r17.Rank; it18++)
                        encoder.Add(r17.GetLength(it18));
                    foreach (var el19 in r17)
                        encoder.Add(el19);
                }
            });
            encoder.Add(P21);
            encoder.Add(P22);

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
            m.P13 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>();
            if (m.P13 != null)
            {
                decoder.GetCollection(() => {
                    var pvar24 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                    if (pvar24 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar25 = decoder.GetInt();
                            return pvar25;
                        }, pvar24, true);
                    }
                    return pvar24;
                }, m.P13, true);
            }
            m.P14 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.String>();
            if (m.P14 != null)
            {
                decoder.GetCollection(() => {
                    var pvar26 = decoder.GetInt();
                    return pvar26;
                },
            () => {
                var pvar27 = decoder.GetString();
                return pvar27;
            }, m.P14, true);
            }
            System.Int32 pvar28 = 0;
            System.String pvar29 = null;
            System.DateTime pvar30 = default(DateTime);
            System.Byte[] pvar31 = null;
            pvar28 = decoder.GetInt();
            pvar29 = decoder.GetString();
            pvar30 = decoder.GetDateTime();
            pvar31 = decoder.GetByteArray();
            m.P15 = new Tuple<System.Int32, System.String, System.DateTime, System.Byte[]>(pvar28, pvar29, pvar30, pvar31);
            m.P16 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>>();
            if (m.P16 != null)
            {
                decoder.GetCollection(() => {
                    var pvar32 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, System.Tuple<System.Int32, System.String>>();
                    if (pvar32 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar33 = decoder.GetInt();
                            return pvar33;
                        },
                    () => {
                        System.Int32 pvar35 = 0;
                        System.String pvar36 = null;
                        pvar35 = decoder.GetInt();
                        pvar36 = decoder.GetString();
                        var pvar34 = new Tuple<System.Int32, System.String>(pvar35, pvar36);
                        return pvar34;
                    }, pvar32, true);
                    }
                    return pvar32;
                }, m.P16, true);
            }
            m.P20 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[]>();
            if (m.P20 != null)
            {
                decoder.GetCollection(() => {
                    var pvar37 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()];
                    if (pvar37 != null)
                    {
                        for (int ard38_0 = 0; ard38_0 < pvar37.GetLength(0); ard38_0++)
                        {
                            pvar37[ard38_0] = decoder.GetInt();
                        }
                    }
                    return pvar37;
                }, m.P20, true);
            }
            m.P21 = BiserTest_Net.TS2.BiserDecode(null, decoder);
            m.P22 = BiserTest_Net.TS3.BiserDecode(null, decoder);


            return m;
        }


    }
}