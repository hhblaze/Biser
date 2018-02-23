using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.Objects
{
    /// <summary>
    /// It's an operational class from https://github.com/hhblaze/Raft.Net/blob/master/Raft/Objects/StateLogEntrySuggestion.cs
    /// </summary>
    [ProtoBuf.ProtoContract]
    public class StateLogEntrySuggestion : Biser.IEncoder
    {
        public StateLogEntrySuggestion()
        {

        }

        /// <summary>
        /// Current leader TermId, must be always included
        /// </summary> 
        [ProtoBuf.ProtoMember(1, IsRequired = true)]
        public ulong LeaderTerm { get; set; }

        [ProtoBuf.ProtoMember(2, IsRequired = true)]
        public StateLogEntry StateLogEntry { get; set; }

        [ProtoBuf.ProtoMember(3, IsRequired = true)]
        public bool IsCommitted { get; set; } = false;


        #region "Biser"
        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(LeaderTerm)
            .Add(StateLogEntry)
            .Add(IsCommitted)
            ;
            return enc;
        }

        public static StateLogEntrySuggestion BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!!!!!!!!!!!!! change return type
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

            StateLogEntrySuggestion m = new StateLogEntrySuggestion();  //!!!!!!!!!!!!!! change return type

            m.LeaderTerm = decoder.GetULong();
            m.StateLogEntry = StateLogEntry.BiserDecode(extDecoder: decoder);
            m.IsCommitted = decoder.GetBool();

            return m;
        }
        #endregion
    }
}
