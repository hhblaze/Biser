using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
    public partial class StateLogEntry
    {
        public StateLogEntry()
        {
            Term = 0;
            Index = 0;
            IsCommitted = false;
        }

        /// <summary>
        /// 
        /// </summary>        
        public ulong Term { get; set; }

        /// <summary>
        /// 
        /// </summary>        
        public ulong Index { get; set; }

        /// <summary>
        /// 
        /// </summary>        
        public byte[] Data { get; set; }

        /// <summary>
        /// If value is committed by Leader
        /// </summary>        
        public bool IsCommitted { get; set; }


        public byte[] ExternalID { get; set; }
    }

    public partial class StateLogEntry : Biser.IJsonEncoder
    {
        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {

            encoder.Add("Term", Term);
            encoder.Add("Index", Index);
            encoder.Add("Data", Data);
            encoder.Add("IsCommitted", IsCommitted);
            encoder.Add("ExternalID", ExternalID);
        }

        public static StateLogEntry BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null)
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

            StateLogEntry m = new StateLogEntry();
            foreach (var props in decoder.GetDictionary<string>())
            {
                switch (props.ToLower())
                {

                    case "term":
                        m.Term = decoder.GetULong();
                        break;
                    case "index":
                        m.Index = decoder.GetULong();
                        break;
                    case "data":
                        m.Data = decoder.GetByteArray();
                        break;
                    case "iscommitted":
                        m.IsCommitted = decoder.GetBool();
                        break;
                    case "externalid":
                        m.ExternalID = decoder.GetByteArray();
                        break;
                    default:
                        decoder.SkipValue();
                        break;
                }
            }
            return m;
        }



    }





    //public partial class StateLogEntry : Biser.IEncoder
    //{


    //    public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
    //    {
    //        Biser.Encoder encoder = new Biser.Encoder(existingEncoder);


    //        encoder.Add(Term);
    //        encoder.Add(Index);
    //        encoder.Add(Data);
    //        encoder.Add(IsCommitted);
    //        encoder.Add(ExternalID);

    //        return encoder;
    //    }


    //    public static StateLogEntry BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null)
    //    {
    //        Biser.Decoder decoder = null;
    //        if (extDecoder == null)
    //        {
    //            if (enc == null || enc.Length == 0)
    //                return null;
    //            decoder = new Biser.Decoder(enc);
    //        }
    //        else
    //        {
    //            if (extDecoder.CheckNull())
    //                return null;
    //            else
    //                decoder = extDecoder;
    //        }

    //        StateLogEntry m = new StateLogEntry();



    //        m.Term = decoder.GetULong();
    //        m.Index = decoder.GetULong();
    //        m.Data = decoder.GetByteArray();
    //        m.IsCommitted = decoder.GetBool();
    //        m.ExternalID = decoder.GetByteArray();


    //        return m;
    //    }


    //}
}
