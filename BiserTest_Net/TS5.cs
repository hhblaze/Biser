﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biser;

namespace BiserTest_Net
{
    /// <summary>
    /// NOT A TEMPLATE, testing Biser.IDecoder
    /// Testing 
    /// </summary>
    public class TS5 : Biser.IEncoder, Biser.IDecoder
    {
        public enum eVoteType
        {
            VoteFor,
            VoteReject
        }
        public TS5()
        {
            TermId = 0;
            VoteType = eVoteType.VoteFor;
        }

        //public ulong TermId { get; set; }
        public uint TermId { get; set; }

        public eVoteType VoteType { get; set; }


        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(TermId)
            .Add((int)VoteType)
            ;
            return enc;
        }

        public static TS5 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!!!!!!!!!!!!! change return type
        {
            Biser.Decoder decoder = null;
            if (extDecoder == null)
            {
                if (enc == null || enc.Length == 0)
                    return null;
                decoder = new Biser.Decoder(enc);
                if (decoder.CheckNull())
                    return null;
            }
            else
            {

                if (extDecoder.CheckNull())
                    return null;
                else
                    decoder = extDecoder;

                //decoder = new Biser.Decoder(extDecoder);
                //if (decoder.IsNull)
                //    return null;
            }

            TS5 m = new TS5();  //!!!!!!!!!!!!!! change return type

            m.TermId = decoder.GetUInt();
            m.VoteType = (eVoteType)decoder.GetInt();

            return m;
        }

        /// <summary>
        /// To create extensions (check IDecoder interface implementaiton, Program.cs and BiserExtension.cs)
        /// </summary>
        /// <param name="extDecoder"></param>
        /// <returns></returns>
        public object BiserDecodeToObject(Biser.Decoder extDecoder)
        {
            return TS5.BiserDecode(null, extDecoder);
        }

        /// <summary>
        /// To create extensions (check IDecoder interface implementaiton, Program.cs and BiserExtension.cs)
        /// </summary>
        /// <param name="enc"></param>
        /// <returns></returns>
        public object BiserDecodeToObject(byte[] encoded)
        {
            return TS5.BiserDecode(encoded);
        }
    }
}
