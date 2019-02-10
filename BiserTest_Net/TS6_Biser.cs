using System;
using System.Collections.Generic;
using System.Linq;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IJsonEncoder
    {
        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {
            if (P12 != null)
            {
                var arrdim1 = new System.Collections.Generic.List<int>();
                arrdim1.Add(P12.GetLength(0));
                arrdim1.Add(P12.GetLength(1));
                arrdim1.Add(P12.GetLength(2));
                System.Collections.Generic.List<System.Int32> r2 = new System.Collections.Generic.List<System.Int32>();
                foreach (var el in P12)
                    r2.Add(el);
                var r3 = new Tuple<System.Collections.Generic.List<int>, System.Collections.Generic.List<System.Int32>>(arrdim1, r2);
                encoder.Add("P12", (r3 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() {{ "Item1", () => {
encoder.Add(r3.Item1, (r4) => {
encoder.Add(r4);});}}, { "Item2", () => {
encoder.Add(r3.Item2, (r5) => {
encoder.Add(r5);});}}});
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

                    case "p12":
                        System.Collections.Generic.List<System.Int32> pvar2 = null;
                        System.Collections.Generic.List<System.Int32> pvar5 = null;
                        foreach (var tupleProps8 in decoder.GetDictionary<string>())
                        {
                            switch (tupleProps8)
                            {
                                case "Item1":
                                    pvar2 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                    if (pvar2 != null)
                                    {
                                        foreach (var el3 in decoder.GetList())
                                        {
                                            var pvar4 = decoder.GetInt();
                                            pvar2.Add(pvar4);
                                        }
                                    }
                                    break;
                                case "Item2":
                                    pvar5 = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                                    if (pvar5 != null)
                                    {
                                        foreach (var el6 in decoder.GetList())
                                        {
                                            var pvar7 = decoder.GetInt();
                                            pvar5.Add(pvar7);
                                        }
                                    }
                                    break;
                            }
                        }
                        var pv1 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32>>(pvar2, pvar5);
                        m.P12 = new System.Int32[pv1.Item1[0], pv1.Item1[1], pv1.Item1[2]];
                        var arenm9 = pv1.Item2.GetEnumerator();
                        arenm9.MoveNext();
                        for (int ard10_0 = 0; ard10_0 < m.P12.GetLength(0); ard10_0++)
                            for (int ard10_1 = 0; ard10_1 < m.P12.GetLength(1); ard10_1++)
                                for (int ard10_2 = 0; ard10_2 < m.P12.GetLength(2); ard10_2++)
                                {
                                    m.P12[ard10_0, ard10_1, ard10_2] = arenm9.Current;
                                    arenm9.MoveNext();
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