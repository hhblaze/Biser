using System;
using System.Collections.Generic;

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
            encoder.Add(P13, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add(r2);
                });
            });
            encoder.Add(P16, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add(r2.Key);
                    encoder.Add(r2.Value.Item1);
                    encoder.Add(r2.Value.Item2);
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
            m.P13 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>();
            if (m.P13 != null)
            {
                decoder.GetCollection(() => {
                    var pvar20 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                    if (pvar20 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar21 = decoder.GetInt();
                            return pvar21;
                        }, pvar20, true);
                    }
                    return pvar20;
                }, m.P13, true);
            }
            m.P16 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, Tuple<System.Int32, System.String>>>();
            if (m.P16 != null)
            {
                decoder.GetCollection(() => {
                    var pvar22 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, Tuple<System.Int32, System.String>>();
                    if (pvar22 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar23 = decoder.GetInt();
                            return pvar23;
                        },
                    () => {
                        System.Int32 pvar25 = 0;
                        System.String pvar26 = null;
                        pvar25 = decoder.GetInt();
                        pvar26 = decoder.GetString();
                        var pvar24 = new Tuple<System.Int32, System.String>(pvar25, pvar26);
                        return pvar24;
                    }, pvar22, true);
                    }
                    return pvar22;
                }, m.P16, true);
            }


            return m;
        }


    }
}