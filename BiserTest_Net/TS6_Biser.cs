using System;
using System.Collections.Generic;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IJsonEncoder
    {
        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {
            encoder.Add("P1", P1);
            encoder.Add("P2", P2);
            encoder.Add("P3", P3);
            encoder.Add("P4", P4, (r1) => { encoder.Add(r1, (r2) => { encoder.Add((r2 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() { { "Item1", () => encoder.Add(r2.Item1) }, { "Item2", () => encoder.Add(r2.Item2) }, }); }); });
            encoder.Add("P5", P5, (r1) => { encoder.Add((r1 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() { { "Item1", () => encoder.Add(r1.Item1) }, { "Item2", () => encoder.Add(r1.Item2) }, }); });
            encoder.Add("P6", P6 == null ? new Dictionary<string, Action>() : new Dictionary<string, Action>() { { "Item1", () => encoder.Add(P6.Item1) }, { "Item2", () => encoder.Add(P6.Item2) }, { "Item3", () => encoder.Add((P6.Item3 == null) ? new Dictionary<string, Action>() : new Dictionary<string, Action>() { { "Item1", () => encoder.Add(P6.Item3.Item1, (r1) => { encoder.Add(r1); }) }, { "Item2", () => encoder.Add(P6.Item3.Item2) }, }) }, });
            encoder.Add("P7", P7, (r1) => { encoder.Add(r1); });
            encoder.Add("P8", P8, (r1) => { encoder.Add(r1, (r2) => { encoder.Add(r2); }); });

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
            {
                //JSONSettings of the existing decoder will be used
                decoder = extDecoder;
            }

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
                        foreach (var tupleProps16 in decoder.GetDictionary<string>())
                        {

                            switch (tupleProps16)
                            {

                                case "Item1":
                                    pvar13 = decoder.GetInt();

                                    break;
                                case "Item2":
                                    pvar14 = decoder.GetString();

                                    break;
                                case "Item3":

                                    System.Collections.Generic.List<System.String> pvar16 = default(System.Collections.Generic.List<System.String>);
                                    System.DateTime pvar17 = default(DateTime);
                                    foreach (var tupleProps18 in decoder.GetDictionary<string>())
                                    {

                                        switch (tupleProps18)
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
                                                pvar17 = decoder.GetDateTime();

                                                break;
                                        }
                                    }
                                    pvar15 = new Tuple<System.Collections.Generic.List<System.String>, System.DateTime>(pvar16, pvar17);

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
                        decoder.SkipValue();//MUST BE HERE
                        break;
                }
            }
            return m;
        }
    }


}