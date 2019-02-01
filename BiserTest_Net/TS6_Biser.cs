using System;
using System.Collections.Generic;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IEncoder
    {


        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder encoder = new Biser.Encoder(existingEncoder);


            encoder.Add(P4, (r1) => {
                encoder.Add(r1, (r2) => {
                    encoder.Add(r2.Key);
                    encoder.Add(r2.Value.Item1);
                    encoder.Add(r2.Value.Item2);
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
            m.P16 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, Tuple<System.Int32, System.String>>>();
            if (m.P16 != null)
            {
                decoder.GetCollection(() => {
                    var pvar6 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, Tuple<System.Int32, System.String>>();
                    if (pvar6 != null)
                    {
                        decoder.GetCollection(() => {
                            var pvar7 = decoder.GetInt();
                            return pvar7;
                        },
                    () => {
                        System.Int32 pvar9 = 0;
                        System.String pvar10 = null;
                        pvar9 = decoder.GetInt();
                        pvar10 = decoder.GetString();
                        var pvar8 = new Tuple<System.Int32, System.String>(pvar9, pvar10);
                        return pvar8;
                    }, pvar6, true);
                    }
                    return pvar6;
                }, m.P16, true);
            }


            return m;
        }


    }
}