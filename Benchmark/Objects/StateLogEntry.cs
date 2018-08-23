using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Objects
{
    /// <summary>
    /// It's an operational class from https://github.com/hhblaze/Raft.Net/blob/master/Raft/StateMachine/StateLogEntry.cs
    /// </summary>
    [ProtoBuf.ProtoContract]
    public class StateLogEntry : Biser.IEncoder
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
        [ProtoBuf.ProtoMember(1, IsRequired = true)]
        public ulong Term { get; set; }

        /// <summary>
        /// 
        /// </summary>        
        [ProtoBuf.ProtoMember(2, IsRequired = true)]
        public ulong Index { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [ProtoBuf.ProtoMember(3, IsRequired = true)]
        public byte[] Data { get; set; }

        /// <summary>
        /// If value is committed by Leader
        /// </summary>        
        [ProtoBuf.ProtoMember(4, IsRequired = true)]
        public bool IsCommitted { get; set; }

        /// <summary>
        /// Out of protobuf
        /// </summary>        
        [ProtoBuf.ProtoMember(5, IsRequired = true)]
        public ulong PreviousStateLogId = 0;
        /// <summary>
        /// Out of protobuf
        /// </summary>    
        [ProtoBuf.ProtoMember(6, IsRequired = true)]
        public ulong PreviousStateLogTerm = 0;

        /// <summary>
        /// RedirectId
        /// </summary>
        [ProtoBuf.ProtoMember(7, IsRequired = true)]
        public ulong RedirectId = 0;
        

        #region "Biser"

        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(Term)
            .Add(Index)
            .Add(Data)
            .Add(IsCommitted)
            .Add(PreviousStateLogId)
            .Add(PreviousStateLogTerm)
            .Add(RedirectId)
            ;
            return enc;
        }

        public static StateLogEntry BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!!!!!!!!!!!!! change return type
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
                decoder = new Biser.Decoder(extDecoder);
                if (decoder.IsNull)
                    return null;
            }

            StateLogEntry m = new StateLogEntry();  //!!!!!!!!!!!!!! change return type

            m.Term = decoder.GetULong();
            m.Index = decoder.GetULong();
            m.Data = decoder.GetByteArray();
            m.IsCommitted = decoder.GetBool();
            m.PreviousStateLogId = decoder.GetULong();
            m.PreviousStateLogTerm = decoder.GetULong();
            m.RedirectId = decoder.GetULong();

            return m;
        }

        //public static StateLogEntry BiserDecodeV1(byte[] enc = null, Biser.DecoderV1 extDecoder = null) //!!!!!!!!!!!!!! change return type
        //{
        //    Biser.DecoderV1 decoder = null;
        //    if (extDecoder == null)
        //    {
        //        if (enc == null || enc.Length == 0)
        //            return null;
        //        decoder = new Biser.DecoderV1(enc);
        //        if (decoder.CheckNull())
        //            return null;
        //    }
        //    else
        //    {
        //        if (extDecoder.CheckNull())
        //            return null;
        //        else
        //            decoder = extDecoder;

        //        //decoder = new Biser.Decoder(extDecoder);
        //        //if (decoder.IsNull)
        //        //    return null;
        //    }

        //    StateLogEntry m = new StateLogEntry();  //!!!!!!!!!!!!!! change return type

        //    m.Term = decoder.GetULong();
        //    m.Index = decoder.GetULong();
        //    m.Data = decoder.GetByteArray();
        //    m.IsCommitted = decoder.GetBool();
        //    m.PreviousStateLogId = decoder.GetULong();
        //    m.PreviousStateLogTerm = decoder.GetULong();
        //    m.RedirectId = decoder.GetULong();

        //    return m;
        //}

        #endregion
    }
}
