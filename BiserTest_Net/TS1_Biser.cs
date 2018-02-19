﻿/* 
  Copyright (C) 2012 tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
    /// <summary>
    /// Biser Extension
    /// </summary>
    public partial class TS1 : Biser.IEncoder
    {
        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(P1)
            .Add(P2)
            .Add(P3)
            .Add(P4, (r) => { enc.Add(r); })
            .Add(P5, (r) => { enc.Add(r.Key); enc.Add(r.Value); })
            .Add(P6, (r) => { enc.Add(r.Key); enc.Add(r.Value, (r1) => { enc.Add(r1); }); })
            .Add(P7)
            .Add(P8, (r) => { enc.Add(r.Item1); enc.Add(r.Item2); enc.Add(r.Item3); })
            .Add(P9.Item1).Add(P9.Item2).Add(P9.Item3).Add(P9.Item4)
            ;
            return enc;
        }

        public static TS1 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!!!!!!!!!!!!! change return type
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
                decoder = new Biser.Decoder(extDecoder);
                if (decoder.IsNull)
                    return null;
            }

            TS1 m = new TS1();  //!!!!!!!!!!!!!! change return type
            
            m.P1 = decoder.GetInt();
            m.P2 = decoder.GetInt();
            m.P3 = decoder.GetDecimal();

            m.P4 = decoder.CheckNull() ? null : new List<TS2>();
            if(m.P4 != null)
                decoder.GetCollection(() => { return TS2.BiserDecode(null, decoder); }, m.P4, true);

            m.P5 = decoder.CheckNull() ? null : new Dictionary<long, TS3>();
            if (m.P5 != null)
                decoder.GetCollection(() => {
                    return decoder.GetLong(); }, 
                    () => {  return TS3.BiserDecode(null, decoder); }, m.P5, true);

            m.P6 = decoder.CheckNull() ? null : new Dictionary<uint, List<TS3>>();
            if (m.P6 != null)
                decoder.GetCollection(
                    () => { return decoder.GetUInt(); },
                    () =>
                    {
                        var iList = decoder.CheckNull() ? null : new List<TS3>();
                        if (iList != null)
                        {
                            decoder.GetCollection(() => { return TS3.BiserDecode(extDecoder: decoder); }, iList, true);
                        }
                        return iList;
                    },
                    m.P6, true);

            m.P7 = TS2.BiserDecode(extDecoder: decoder);

            m.P8 = decoder.CheckNull() ? null : new List<Tuple<string, byte[], TS3>>();
            if (m.P8 != null)
                decoder.GetCollection
                    (() => { return new Tuple<string, byte[], TS3>
                        (decoder.GetString(), 
                        decoder.GetByteArray(), 
                        TS3.BiserDecode(null, decoder));
                    }, m.P8, true);

            m.P9 = new Tuple<float, TS2, TS3, decimal?>
                (decoder.GetFloat(), TS2.BiserDecode(null, decoder), TS3.BiserDecode(null, decoder), decoder.GetDecimal_NULL());

            return m;
        }
    }
}
