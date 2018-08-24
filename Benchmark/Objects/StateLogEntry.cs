using MessagePack;
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
    [MessagePackObject]
    [ProtoBuf.ProtoContract]
    public class StateLogEntry : Biser.IEncoder, Biser.IJsonEncoder
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
        [Key(0)]
        [ProtoBuf.ProtoMember(1, IsRequired = true)]
        public ulong Term { get; set; }

        /// <summary>
        /// 
        /// </summary>    
        [Key(1)]
        [ProtoBuf.ProtoMember(2, IsRequired = true)]
        public ulong Index { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [Key(2)]
        [ProtoBuf.ProtoMember(3, IsRequired = true)]
        public byte[] Data { get; set; }

        /// <summary>
        /// If value is committed by Leader
        /// </summary>   
        [Key(3)]
        [ProtoBuf.ProtoMember(4, IsRequired = true)]
        public bool IsCommitted { get; set; }

        /// <summary>
        /// Out of protobuf
        /// </summary>  
        [Key(4)]
        [ProtoBuf.ProtoMember(5, IsRequired = true)]
        public ulong PreviousStateLogId = 0;
        /// <summary>
        /// Out of protobuf
        /// </summary>    
        [Key(5)]
        [ProtoBuf.ProtoMember(6, IsRequired = true)]
        public ulong PreviousStateLogTerm = 0;

        /// <summary>
        /// RedirectId
        /// </summary>
        [Key(6)]
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
                if (extDecoder.CheckNull())
                    return null;
                else
                    decoder = extDecoder;
                //decoder = new Biser.Decoder(extDecoder);
                //if (decoder.IsNull)
                //    return null;
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
        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {            
            encoder.Add("Term", this.Term);
            encoder.Add("Index", this.Index);
            encoder.Add("Data", this.Data);
            encoder.Add("IsCommitted", this.IsCommitted);
            encoder.Add("PreviousStateLogId", this.PreviousStateLogId);
            encoder.Add("PreviousStateLogTerm", this.PreviousStateLogTerm);
            encoder.Add("RedirectId", this.RedirectId);

        }

        public static StateLogEntry BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null) //!!!!!!!!!!!!!! change return type
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

            StateLogEntry m = new StateLogEntry();  //!!!!!!!!!!!!!! change return type
            foreach (var props in decoder.GetDictionary<string>())
            {
                switch (props)
                {
                    case "Term":
                        m.Term = decoder.GetULong();
                        break;
                    case "Index":
                        m.Index = decoder.GetULong();
                        break;
                    case "Data":
                        m.Data = decoder.GetByteArray();
                        break;
                    case "IsCommitted":
                        m.IsCommitted = decoder.GetBool();
                        break;
                    case "PreviousStateLogId":
                        m.PreviousStateLogId = decoder.GetULong();
                        break;
                    case "PreviousStateLogTerm":
                        m.PreviousStateLogTerm = decoder.GetULong();
                        break;
                    case "RedirectId":
                        m.RedirectId = decoder.GetULong();
                        break;
                    default:
                        decoder.SkipValue(); //Must be here
                        break;
                }
            }
            return m;

        }//eof

        #endregion
    }
}
