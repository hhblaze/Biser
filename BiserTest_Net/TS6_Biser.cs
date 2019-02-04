using System;
using System.Collections.Generic;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IJsonEncoder
    {
        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {

            encoder.Add("P5", P5, (r1) => {
                encoder.Add((r1 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r1.Item1);}}, { "Item2", () => {
encoder.Add(r1.Item2);}}});
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

                    case "p5":
                        m.P5 = decoder.CheckNull() ? null : new System.Collections.Generic.Dictionary<System.Int32, Tuple<System.Int32, System.String>>();
                        if (m.P5 != null)
                        {
                            foreach (var el1 in decoder.GetDictionary<System.Int32>())
                            {
                                System.Int32 pvar3 = 0;
                                System.String pvar4 = null;
                                foreach (var tupleProps5 in decoder.GetDictionary<string>())
                                {
                                    switch (tupleProps5)
                                    {
                                        case "Item1":
                                            pvar3 = decoder.GetInt();
                                            break;
                                        case "Item2":
                                            pvar4 = decoder.GetString();
                                            break;
                                    }
                                }
                                var pvar2 = new Tuple<System.Int32, System.String>(pvar3, pvar4);
                                m.P5.Add(el1, pvar2);
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



    }
}