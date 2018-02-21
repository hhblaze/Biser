using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Biser
{
    public static class BiserExtensions
    {
        static Dictionary<Type, Tuple<Action<Encoder, object>, Func<Decoder, object>>> dSimpleDecoders = new Dictionary<Type, Tuple<Action<Encoder, object>, Func<Decoder, object>>>();
        static Type TIDecoder = typeof(IDecoder);
        static Type TIEncoder = typeof(IEncoder);
        

        

        public static byte[] BiserEncodeList<T>(this IEnumerable<T> objs)
        {
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = null;
            Type t = typeof(T);
            //Check BiserDecodeList
            var enc = new Encoder();

            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                enc.Add(objs, r => { f.Item1(enc,r); });
                return enc.Encode();
            }                

            if (TIEncoder.IsAssignableFrom(t))
            {
                f = new Tuple<Action<Encoder, object>, Func<Decoder, object>>(
                    (e, o) =>
                    {
                        e.Add((IEncoder)o);
                    },
                    (d) => {
                        if (TIDecoder.IsAssignableFrom(t))
                            return (T)(((IDecoder)GetInstanceCreator(typeof(T))()).BiserDecodeToObject(d));
                        else
                            throw new Exception($"Biser: type {t.ToString()} doesn't implement IDecoder");                        
                    });

                dSimpleDecoders[t] = f;

                enc.Add(objs, r => { f.Item1(enc, r); });
                return enc.Encode();
            }

            FillDecoder();

            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                enc.Add(objs, r => { f.Item1(enc, r); });
                return enc.Encode();
            }

            throw new Exception($"Biser: type {t.ToString()} doesn't implement IEncoder");
        }


        public static byte[] BiserEncode<T>(this T obj)
        {
                        
            Type t = typeof(T);          
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = null;

            //Check BiserDecodeList
            var enc = new Encoder();

            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                f.Item1(enc, obj);
                return enc.Encode();
            }

            if (TIEncoder.IsAssignableFrom(t))
            {
                f = new Tuple<Action<Encoder, object>, Func<Decoder, object>>(
                    (e, o) =>
                    {
                        e.Add((IEncoder)o);
                    },
                    (d) => {
                        if (TIDecoder.IsAssignableFrom(t))
                            return (T)(((IDecoder)GetInstanceCreator(typeof(T))()).BiserDecodeToObject(d));
                        else
                            throw new Exception($"Biser: type {t.ToString()} doesn't implement IDecoder");
                    });

                dSimpleDecoders[t] = f;

                f.Item1(enc, obj);
                return enc.Encode();
            }

            FillDecoder();

            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                f.Item1(enc, obj);
                return enc.Encode();
            }

            throw new Exception($"Biser: type {t.ToString()} doesn't implement IEncoder");

        }


        /// <summary>
        /// Creates new decoder. 
        /// <para>Decoding type must be either .NET primitive or to implement IDecoder</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static T BiserDecode<T>(this byte[] enc)
        {
            var decoder = new Decoder(enc);
            return decoder.BiserDecode<T>();            
        }

        /// <summary>
        /// Re-uses existing decoder.
        /// <para>Decoding type must be either .NET primitive or to implement IDecoder</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="decoder"></param>
        /// <returns></returns>
        public static T BiserDecode<T>(this Decoder decoder)
        {
            Tuple<Action<Encoder,object>, Func<Decoder, object>> f = null;
            Type t = typeof(T);            
            
            if (dSimpleDecoders.TryGetValue(t, out f))
                return (T)f.Item2(decoder);

            if (TIDecoder.IsAssignableFrom(t))
            {
                f = new Tuple<Action<Encoder, object>, Func<Decoder, object>>(
                    (e, o) =>
                    {
                        if (TIEncoder.IsAssignableFrom(t))
                            e.Add((IEncoder)o);
                        else
                            throw new Exception($"Biser: type {t.ToString()} doesn't implement IEncoder");
                    },
                    (d) =>
                    {
                        return (T)(((IDecoder)GetInstanceCreator(typeof(T))()).BiserDecodeToObject(d));
                    }
                );

                dSimpleDecoders[t] = f;

                return (T)f.Item2(decoder);              
            }

            FillDecoder();

            if (dSimpleDecoders.TryGetValue(t, out f))
                return (T)f.Item2(decoder);

            throw new Exception($"Biser: type {t.ToString()} doesn't implement IDecoder");
        }

        public static List<T> BiserDecodeList<T>(this byte[] enc)
        {
            //Emulates extension for fast encoding/decoding list
            //Of course, NOT SO EFFICIENT as an explicit instance creation because of GetInstanceCreator and a serie of casts

            /*
             TS5 voc = new TS5()
            {
                TermId = 12,
                VoteType = TS5.eVoteType.VoteReject
            };
           
            var lst = new List<TS5> { voc, voc, voc };            
            var btEn = lst.BiserEncode();            
            var lst1 = btEn.BiserDecodeList<TS5>();  
            
             */

            if (enc == null)
                return null;

            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = null;
            Type t = typeof(T);

            var t1 = (List<T>)GetInstanceCreator(typeof(List<T>))();
            var decoder = new Decoder(enc);
            
            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
                return t1;
            }

            if (TIDecoder.IsAssignableFrom(t))
            {
                f = new Tuple<Action<Encoder, object>, Func<Decoder, object>>(
                (e, o) =>
                {
                    if (TIEncoder.IsAssignableFrom(t))
                        e.Add((IEncoder)o);
                    else
                        throw new Exception($"Biser: type {t.ToString()} doesn't implement IEncoder");
                },
                (d) =>
                {
                    return (T)(((IDecoder)GetInstanceCreator(typeof(T))()).BiserDecodeToObject(d));
                });

                dSimpleDecoders[t] = f;

                decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
                return t1;
            }

            FillDecoder();

            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
                return t1;
            }

            throw new Exception($"Biser: type {t.ToString()} doesn't implement IDecoder");
        }

        public static HashSet<T> BiserDecodeHashSet<T>(this byte[] enc)
        {
            //Emulates extension for fast encoding/decoding hashset
            //Of course, NOT SO EFFICIENT as an explicit instance creation because of GetInstanceCreator and a serie of casts

            if (enc == null)
                return null;

            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = null;
            Type t = typeof(T);

            var t1 = (HashSet<T>)GetInstanceCreator(typeof(HashSet<T>))();
            var decoder = new Decoder(enc);

            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
                return t1;
            }

            if (TIDecoder.IsAssignableFrom(t))
            {
                f = new Tuple<Action<Encoder, object>, Func<Decoder, object>>(
                (e, o) =>
                {
                    if (TIEncoder.IsAssignableFrom(t))
                        e.Add((IEncoder)o);
                    else
                        throw new Exception($"Biser: type {t.ToString()} doesn't implement IEncoder");
                },
                (d) =>
                {
                    return (T)(((IDecoder)GetInstanceCreator(typeof(T))()).BiserDecodeToObject(d));
                });

                dSimpleDecoders[t] = f;

                decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
                return t1;
            }

            FillDecoder();

            if (dSimpleDecoders.TryGetValue(t, out f))
            {
                decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
                return t1;
            }

            throw new Exception($"Biser: type {t.ToString()} doesn't implement IDecoder");
        }


        static void FillDecoder()
        {
            if (dSimpleDecoders.Count > 0)
                return;
            
            dSimpleDecoders[typeof(long)] =new Tuple<Action<Encoder,object>, Func<Decoder, object>>((e,o)=> { e.Add((long)o); }, (d) => { return d.GetLong(); });
            dSimpleDecoders[typeof(long?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((long?)o); }, (d) => { return d.GetLong_NULL(); });
            dSimpleDecoders[typeof(int)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((int)o); }, (d) => { return d.GetInt(); });
            dSimpleDecoders[typeof(int?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((int?)o); }, (d) => { return d.GetInt_NULL(); });            
            dSimpleDecoders[typeof(ulong)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ulong)o); }, (d) => { return d.GetULong(); });
            dSimpleDecoders[typeof(ulong?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ulong?)o); }, (d) => { return d.GetULong_NULL(); });
            dSimpleDecoders[typeof(uint)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((uint)o); }, (d) => { return d.GetUInt(); });
            dSimpleDecoders[typeof(uint?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((uint?)o); }, (d) => { return d.GetUInt_NULL(); });
            dSimpleDecoders[typeof(short)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((short)o); }, (d) => { return d.GetShort(); });
            dSimpleDecoders[typeof(short?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((short?)o); }, (d) => { return d.GetShort_NULL(); });
            dSimpleDecoders[typeof(ushort)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ushort)o); }, (d) => { return d.GetUShort(); });
            dSimpleDecoders[typeof(ushort?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ushort?)o); }, (d) => { return d.GetUShort_NULL(); });
            dSimpleDecoders[typeof(byte)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((byte)o); }, (d) => { return d.GetByte(); });
            dSimpleDecoders[typeof(byte?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((byte?)o); }, (d) => { return d.GetByte_NULL(); });
            dSimpleDecoders[typeof(float)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((float)o); }, (d) => { return d.GetFloat(); });
            dSimpleDecoders[typeof(float?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((float?)o); }, (d) => { return d.GetFloat_NULL(); });
            dSimpleDecoders[typeof(double)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((double)o); }, (d) => { return d.GetDouble(); });
            dSimpleDecoders[typeof(double?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((double?)o); }, (d) => { return d.GetDouble_NULL(); });
            dSimpleDecoders[typeof(decimal)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((decimal)o); }, (d) => { return d.GetDecimal(); });
            dSimpleDecoders[typeof(decimal?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((decimal?)o); }, (d) => { return d.GetDecimal_NULL(); });
            dSimpleDecoders[typeof(DateTime)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((DateTime)o); }, (d) => { return d.GetDateTime(); });
            dSimpleDecoders[typeof(DateTime?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((DateTime?)o); }, (d) => { return d.GetDateTime_NULL(); });
            dSimpleDecoders[typeof(sbyte)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((sbyte)o); }, (d) => { return d.GetSByte(); });
            dSimpleDecoders[typeof(sbyte?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((sbyte?)o); }, (d) => { return d.GetSByte_NULL(); });
            dSimpleDecoders[typeof(bool)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((bool)o); }, (d) => { return d.GetBool(); });
            dSimpleDecoders[typeof(bool?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((bool?)o); }, (d) => { return d.GetBool_NULL(); });
            dSimpleDecoders[typeof(string)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((string)o); }, (d) => { return d.GetString(); });
            dSimpleDecoders[typeof(byte[])] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((byte[])o); }, (d) => { return d.GetByteArray(); });
            dSimpleDecoders[typeof(char)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((char)o); }, (d) => { return d.GetChar(); });
            dSimpleDecoders[typeof(char?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((char?)o); }, (d) => { return d.GetChar_NULL(); });
            dSimpleDecoders[typeof(Guid)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((Guid)o); }, (d) => { return d.GetGuid(); });
            dSimpleDecoders[typeof(Guid?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((Guid?)o); }, (d) => { return d.GetGuid_NULL(); });
        }




        /// <summary>
        /// Holder of compiled instance creators
        /// </summary>
        static Dictionary<Type, Func<object>> dInstanceCreator = new Dictionary<Type, Func<object>>();
        /// <summary>
        /// Returns an instance creator for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Func<object> GetInstanceCreator(Type type)
        {
            Func<object> f = null;
            if (dInstanceCreator.TryGetValue(type, out f))
                return f;
            var constructorCallExpression = Expression.New(type);
            var constructorCallingLambda = Expression
                .Lambda<Func<object>>(constructorCallExpression)
                .Compile();
            dInstanceCreator[type] = constructorCallingLambda;
            return constructorCallingLambda;
        }
    }
}
