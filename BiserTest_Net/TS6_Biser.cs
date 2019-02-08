using System;
using System.Collections.Generic;
using System.Linq;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IJsonEncoder
    {
        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {

            var r1 = P11.ToList();
            encoder.Add("P11", r1, (r2) => {
                var r3 = r2.ToList();
                encoder.Add(r3, (r4) => {
                    encoder.Add(r4);
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

                    case "p11":
                        var intlst1 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32[]>();
                        if (intlst1 != null)
                        {
                            foreach (var el2 in decoder.GetList())
                            {
                                var intlst4 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                if (intlst4 != null)
                                {
                                    foreach (var el5 in decoder.GetList())
                                    {
                                        var pvar6 = decoder.GetInt();
                                        intlst4.Add(pvar6);
                                    }
                                }
                                var pvar3 = decoder.CheckNull() ? null : new System.Int32[intlst4.Count];
                                if (pvar3 != null)
                                {
                                    for (int ard7_0 = 0; ard7_0 < pvar3.GetLength(0); ard7_0++)
                                    {
                                        pvar3[ard7_0] = intlst4[ard7_0];
                                    }
                                }
                                intlst1.Add(pvar3);
                            }
                        }
                        m.P11 = decoder.CheckNull() ? null : new System.Int32[intlst1.Count][];
                        if (m.P11 != null)
                        {
                            for (int ard8_0 = 0; ard8_0 < m.P11.GetLength(0); ard8_0++)
                            {
                                m.P11[ard8_0] = intlst1[ard8_0];
                            }
                        }
                        break;
                    case "p12":
                        break;
                    default:
                        decoder.SkipValue();
                        break;
                }
            }
            return m;
        }



    }
}