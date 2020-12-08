using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
	public partial class TS7 : Biser.IJsonEncoder, Biser.IEncoder
	{
		public void BiserJsonEncode(Biser.JsonEncoder encoder)
		{

			encoder.Add("VoteType", (System.Int16)VoteType);
			encoder.Add("Barabaka", Barabaka);
		}

		public static TS7 BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null)
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

			TS7 m = new TS7();
			foreach (var props in decoder.GetDictionary<string>())
			{
				switch (props.ToLower())
				{

					case "votetype":
						m.VoteType = (eVoteType)decoder.GetShort();
						break;
					case "barabaka":
						m.Barabaka = decoder.GetInt();
						break;
					default:
						decoder.SkipValue();
						break;
				}
			}
			return m;
		}

		public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
		{
			Biser.Encoder encoder = new Biser.Encoder(existingEncoder);


			encoder.Add((System.Int16)VoteType);
			encoder.Add(Barabaka);

			return encoder;
		}


		public static TS7 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null)
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
				if (extDecoder.CheckNull())
					return null;
				else
					decoder = extDecoder;
			}

			TS7 m = new TS7();



			m.VoteType = (eVoteType)decoder.GetShort();
			m.Barabaka = decoder.GetInt();


			return m;
		}


	}


}
