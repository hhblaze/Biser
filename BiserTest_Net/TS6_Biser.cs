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
            if (P11 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it1 = 0; it1 < P11.Rank; it1++)
                    encoder.Add(P11.GetLength(it1));
                foreach (var el2 in P11)
                    if (el2 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it3 = 0; it3 < el2.Rank; it3++)
                            encoder.Add(el2.GetLength(it3));
                        foreach (var el4 in el2)
                            encoder.Add(el4);
                    }
            }
            if (P12 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it1 = 0; it1 < P12.Rank; it1++)
                    encoder.Add(P12.GetLength(it1));
                foreach (var el2 in P12)
                    encoder.Add(el2);
            }
            encoder.Add(P13, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add(r2);
                });
            });
            encoder.Add(P14, (r1) => {
                encoder.Add(r1.Key);
                encoder.Add(r1.Value);
            });
            encoder.Add(P15.Item1);
            encoder.Add(P15.Item2);
            encoder.Add(P15.Item3);
            encoder.Add(P15.Item4);
            encoder.Add(P16, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add(r2.Key);
                    encoder.Add(r2.Value.Item1);
                    encoder.Add(r2.Value.Item2);
                });
            });
            if (P17 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it1 = 0; it1 < P17.Rank; it1++)
                    encoder.Add(P17.GetLength(it1));
                foreach (var el2 in P17)
                    encoder.Add(el2);
            }
            if (P18 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it1 = 0; it1 < P18.Rank; it1++)
                    encoder.Add(P18.GetLength(it1));
                foreach (var el2 in P18)
                    if (el2 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it3 = 0; it3 < el2.Rank; it3++)
                            encoder.Add(el2.GetLength(it3));
                        foreach (var el4 in el2)
                            if (el4 == null)
                                encoder.Add((byte)1);
                            else
                            {
                                encoder.Add((byte)0);
                                for (int it5 = 0; it5 < el4.Rank; it5++)
                                    encoder.Add(el4.GetLength(it5));
                                foreach (var el6 in el4)
                                    if (el6 == null)
                                        encoder.Add((byte)1);
                                    else
                                    {
                                        encoder.Add((byte)0);
                                        for (int it7 = 0; it7 < el6.Rank; it7++)
                                            encoder.Add(el6.GetLength(it7));
                                        foreach (var el8 in el6)
                                            if (el8 == null)
                                                encoder.Add((byte)1);
                                            else
                                            {
                                                encoder.Add((byte)0);
                                                for (int it9 = 0; it9 < el8.Rank; it9++)
                                                    encoder.Add(el8.GetLength(it9));
                                                foreach (var el10 in el8)
                                                    encoder.Add(el10, (r11) => {
                                                        if (r11 == null)
                                                            encoder.Add((byte)1);
                                                        else
                                                        {
                                                            encoder.Add((byte)0);
                                                            for (int it12 = 0; it12 < r11.Rank; it12++)
                                                                encoder.Add(r11.GetLength(it12));
                                                            foreach (var el13 in r11)
                                                                if (el13 == null)
                                                                    encoder.Add((byte)1);
                                                                else
                                                                {
                                                                    encoder.Add((byte)0);
                                                                    for (int it14 = 0; it14 < el13.Rank; it14++)
                                                                        encoder.Add(el13.GetLength(it14));
                                                                    foreach (var el15 in el13)
                                                                        if (el15 == null)
                                                                            encoder.Add((byte)1);
                                                                        else
                                                                        {
                                                                            encoder.Add((byte)0);
                                                                            for (int it16 = 0; it16 < el15.Rank; it16++)
                                                                                encoder.Add(el15.GetLength(it16));
                                                                            foreach (var el17 in el15)
                                                                                encoder.Add(el17);
                                                                        }
                                                                }
                                                        }
                                                    });
                                            }
                                    }
                            }
                    }
            }
            encoder.Add(P19, (r1) => {
                encoder.Add(r1.Key);
                encoder.Add(r1.Value, (r2) => {
                    if (r2 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it3 = 0; it3 < r2.Rank; it3++)
                            encoder.Add(r2.GetLength(it3));
                        foreach (var el4 in r2)
                            if (el4 == null)
                                encoder.Add((byte)1);
                            else
                            {
                                encoder.Add((byte)0);
                                for (int it5 = 0; it5 < el4.Rank; it5++)
                                    encoder.Add(el4.GetLength(it5));
                                foreach (var el6 in el4)
                                    if (el6 == null)
                                        encoder.Add((byte)1);
                                    else
                                    {
                                        encoder.Add((byte)0);
                                        for (int it7 = 0; it7 < el6.Rank; it7++)
                                            encoder.Add(el6.GetLength(it7));
                                        foreach (var el8 in el6)
                                            encoder.Add(el8);
                                    }
                            }
                    }
                });
            });
            encoder.Add(P20, (r1) => {
                if (r1 == null)
                    encoder.Add((byte)1);
                else
                {
                    encoder.Add((byte)0);
                    for (int it2 = 0; it2 < r1.Rank; it2++)
                        encoder.Add(r1.GetLength(it2));
                    foreach (var el3 in r1)
                        encoder.Add(el3);
                }
            });
            encoder.Add(P21);
            encoder.Add(P22);
            if (P23 == null)
                encoder.Add((byte)1);
            else
            {
                encoder.Add((byte)0);
                for (int it1 = 0; it1 < P23.Rank; it1++)
                    encoder.Add(P23.GetLength(it1));
                foreach (var el2 in P23)
                    if (el2 == null)
                        encoder.Add((byte)1);
                    else
                    {
                        encoder.Add((byte)0);
                        for (int it3 = 0; it3 < el2.Rank; it3++)
                            encoder.Add(el2.GetLength(it3));
                        foreach (var el4 in el2)
                            if (el4 == null)
                                encoder.Add((byte)1);
                            else
                            {
                                encoder.Add((byte)0);
                                for (int it5 = 0; it5 < el4.Rank; it5++)
                                    encoder.Add(el4.GetLength(it5));
                                foreach (var el6 in el4)
                                    if (el6 == null)
                                        encoder.Add((byte)1);
                                    else
                                    {
                                        encoder.Add((byte)0);
                                        for (int it7 = 0; it7 < el6.Rank; it7++)
                                            encoder.Add(el6.GetLength(it7));
                                        foreach (var el8 in el6)
                                            if (el8 == null)
                                                encoder.Add((byte)1);
                                            else
                                            {
                                                encoder.Add((byte)0);
                                                for (int it9 = 0; it9 < el8.Rank; it9++)
                                                    encoder.Add(el8.GetLength(it9));
                                                foreach (var el10 in el8)
                                                    if (el10 == null)
                                                        encoder.Add((byte)1);
                                                    else
                                                    {
                                                        encoder.Add((byte)0);
                                                        for (int it11 = 0; it11 < el10.Rank; it11++)
                                                            encoder.Add(el10.GetLength(it11));
                                                        foreach (var el12 in el10)
                                                            if (el12 == null)
                                                                encoder.Add((byte)1);
                                                            else
                                                            {
                                                                encoder.Add((byte)0);
                                                                for (int it13 = 0; it13 < el12.Rank; it13++)
                                                                    encoder.Add(el12.GetLength(it13));
                                                                foreach (var el14 in el12)
                                                                    encoder.Add(el14);
                                                            }
                                                    }
                                            }
                                    }
                            }
                    }
            }
            encoder.Add(P24, (r1) => {
                if (r1 == null)
                    encoder.Add((byte)1);
                else
                {
                    encoder.Add((byte)0);
                    for (int it2 = 0; it2 < r1.Rank; it2++)
                        encoder.Add(r1.GetLength(it2));
                    foreach (var el3 in r1)
                        if (el3 == null)
                            encoder.Add((byte)1);
                        else
                        {
                            encoder.Add((byte)0);
                            for (int it4 = 0; it4 < el3.Rank; it4++)
                                encoder.Add(el3.GetLength(it4));
                            foreach (var el5 in el3)
                                if (el5 == null)
                                    encoder.Add((byte)1);
                                else
                                {
                                    encoder.Add((byte)0);
                                    for (int it6 = 0; it6 < el5.Rank; it6++)
                                        encoder.Add(el5.GetLength(it6));
                                    foreach (var el7 in el5)
                                        encoder.Add(el7);
                                }
                        }
                }
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
            m.P11 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][];

            for (int ard20_0 = 0; ard20_0 < m.P11.GetLength(0); ard20_0++)
            {
                m.P11[ard20_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()];

                for (int ard22_0 = 0; ard22_0 < m.P11[ard20_0].GetLength(0); ard22_0++)
                {
                    m.P11[ard20_0][ard22_0] = decoder.GetInt();
                }
            }
            m.P12 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];

            for (int ard24_0 = 0; ard24_0 < m.P12.GetLength(0); ard24_0++)
                for (int ard25_1 = 0; ard25_1 < m.P12.GetLength(1); ard25_1++)
                    for (int ard26_2 = 0; ard26_2 < m.P12.GetLength(2); ard26_2++)
                    {
                        m.P12[ard24_0, ard25_1, ard26_2] = decoder.GetInt();
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

            for (int ard41_0 = 0; ard41_0 < m.P17.GetLength(0); ard41_0++)
            {
                m.P17[ard41_0] = decoder.GetInt();
            }
            m.P18 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][][][,,,,];

            for (int ard43_0 = 0; ard43_0 < m.P18.GetLength(0); ard43_0++)
                for (int ard44_1 = 0; ard44_1 < m.P18.GetLength(1); ard44_1++)
                    for (int ard45_2 = 0; ard45_2 < m.P18.GetLength(2); ard45_2++)
                    {
                        m.P18[ard43_0, ard44_1, ard45_2] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt()][][][,,,,];

                        for (int ard47_0 = 0; ard47_0 < m.P18[ard43_0, ard44_1, ard45_2].GetLength(0); ard47_0++)
                        {
                            m.P18[ard43_0, ard44_1, ard45_2][ard47_0] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt()][][,,,,];

                            for (int ard49_0 = 0; ard49_0 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0].GetLength(0); ard49_0++)
                            {
                                m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt()][,,,,];

                                for (int ard51_0 = 0; ard51_0 < m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0].GetLength(0); ard51_0++)
                                {
                                    m.P18[ard43_0, ard44_1, ard45_2][ard47_0][ard49_0][ard51_0] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[,,][][,,]>[decoder.GetInt(), decoder.GetInt(), decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];

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

                                                                for (int ard60_0 = 0; ard60_0 < pvar59.GetLength(0); ard60_0++)
                                                                    for (int ard61_1 = 0; ard61_1 < pvar59.GetLength(1); ard61_1++)
                                                                        for (int ard62_2 = 0; ard62_2 < pvar59.GetLength(2); ard62_2++)
                                                                        {
                                                                            pvar59[ard60_0, ard61_1, ard62_2] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,];

                                                                            for (int ard64_0 = 0; ard64_0 < pvar59[ard60_0, ard61_1, ard62_2].GetLength(0); ard64_0++)
                                                                            {
                                                                                pvar59[ard60_0, ard61_1, ard62_2][ard64_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];

                                                                                for (int ard66_0 = 0; ard66_0 < pvar59[ard60_0, ard61_1, ard62_2][ard64_0].GetLength(0); ard66_0++)
                                                                                    for (int ard67_1 = 0; ard67_1 < pvar59[ard60_0, ard61_1, ard62_2][ard64_0].GetLength(1); ard67_1++)
                                                                                        for (int ard68_2 = 0; ard68_2 < pvar59[ard60_0, ard61_1, ard62_2][ard64_0].GetLength(2); ard68_2++)
                                                                                        {
                                                                                            pvar59[ard60_0, ard61_1, ard62_2][ard64_0][ard66_0, ard67_1, ard68_2] = decoder.GetInt();
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

                        for (int ard73_0 = 0; ard73_0 < pvar72.GetLength(0); ard73_0++)
                            for (int ard74_1 = 0; ard74_1 < pvar72.GetLength(1); ard74_1++)
                            {
                                pvar72[ard73_0, ard74_1] = decoder.CheckNull() ? null : new System.String[decoder.GetInt()][,,];

                                for (int ard76_0 = 0; ard76_0 < pvar72[ard73_0, ard74_1].GetLength(0); ard76_0++)
                                {
                                    pvar72[ard73_0, ard74_1][ard76_0] = decoder.CheckNull() ? null : new System.String[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];

                                    for (int ard78_0 = 0; ard78_0 < pvar72[ard73_0, ard74_1][ard76_0].GetLength(0); ard78_0++)
                                        for (int ard79_1 = 0; ard79_1 < pvar72[ard73_0, ard74_1][ard76_0].GetLength(1); ard79_1++)
                                            for (int ard80_2 = 0; ard80_2 < pvar72[ard73_0, ard74_1][ard76_0].GetLength(2); ard80_2++)
                                            {
                                                pvar72[ard73_0, ard74_1][ard76_0][ard78_0, ard79_1, ard80_2] = decoder.GetString();
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

                    for (int ard83_0 = 0; ard83_0 < pvar82.GetLength(0); ard83_0++)
                    {
                        pvar82[ard83_0] = decoder.GetInt();
                    }
                    return pvar82;
                }, m.P20, true);
            }
            m.P21 = BiserTest_Net.TS2.BiserDecode(null, decoder);
            m.P22 = BiserTest_Net.TS3.BiserDecode(null, decoder);
            m.P23 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,][][][,,,][][,];

            for (int ard85_0 = 0; ard85_0 < m.P23.GetLength(0); ard85_0++)
            {
                m.P23[ard85_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][][,,,][][,];

                for (int ard87_0 = 0; ard87_0 < m.P23[ard85_0].GetLength(0); ard87_0++)
                    for (int ard88_1 = 0; ard88_1 < m.P23[ard85_0].GetLength(1); ard88_1++)
                        for (int ard89_2 = 0; ard89_2 < m.P23[ard85_0].GetLength(2); ard89_2++)
                        {
                            m.P23[ard85_0][ard87_0, ard88_1, ard89_2] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][][,,,][][,];

                            for (int ard91_0 = 0; ard91_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2].GetLength(0); ard91_0++)
                            {
                                m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,,][][,];

                                for (int ard93_0 = 0; ard93_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0].GetLength(0); ard93_0++)
                                {
                                    m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][][,];

                                    for (int ard95_0 = 0; ard95_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(0); ard95_0++)
                                        for (int ard96_1 = 0; ard96_1 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(1); ard96_1++)
                                            for (int ard97_2 = 0; ard97_2 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(2); ard97_2++)
                                                for (int ard98_3 = 0; ard98_3 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0].GetLength(3); ard98_3++)
                                                {
                                                    m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,];

                                                    for (int ard100_0 = 0; ard100_0 < m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3].GetLength(0); ard100_0++)
                                                    {
                                                        m.P23[ard85_0][ard87_0, ard88_1, ard89_2][ard91_0][ard93_0][ard95_0, ard96_1, ard97_2, ard98_3][ard100_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt()];

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
            m.P24 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[][,,][]>();
            if (m.P24 != null)
            {
                decoder.GetCollection(() => {
                    var pvar105 = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()][,,][];

                    for (int ard106_0 = 0; ard106_0 < pvar105.GetLength(0); ard106_0++)
                    {
                        pvar105[ard106_0] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()][];

                        for (int ard108_0 = 0; ard108_0 < pvar105[ard106_0].GetLength(0); ard108_0++)
                            for (int ard109_1 = 0; ard109_1 < pvar105[ard106_0].GetLength(1); ard109_1++)
                                for (int ard110_2 = 0; ard110_2 < pvar105[ard106_0].GetLength(2); ard110_2++)
                                {
                                    pvar105[ard106_0][ard108_0, ard109_1, ard110_2] = decoder.CheckNull() ? null : new System.Int32[decoder.GetInt()];

                                    for (int ard112_0 = 0; ard112_0 < pvar105[ard106_0][ard108_0, ard109_1, ard110_2].GetLength(0); ard112_0++)
                                    {
                                        pvar105[ard106_0][ard108_0, ard109_1, ard110_2][ard112_0] = decoder.GetInt();
                                    }
                                }
                    }
                    return pvar105;
                }, m.P24, true);
            }


            return m;
        }


    }
}