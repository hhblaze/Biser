using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biser
{
    public class JsonDecoder
    {
        internal string encoded = null;       

        internal int encPos = -1;

        JsonSettings jsonSettings = null;

        public JsonDecoder(string encoded, JsonSettings settings=null)
        {
            jsonSettings = (settings == null) ? new JsonSettings() : settings;
           
            this.encoded = encoded;
            if (encoded == null || encoded.Length == 0)
                return;

        }

        bool CheckSkip(char c)
        {
            return (c == ' ' || c == '\t' || c == '\r' || c == '\n');
        }

        public bool CheckNull()
        {
            bool ret = false;
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    break;
                var c = this.encoded[this.encPos];
                if (CheckSkip(c))
                    continue;
                if (!ret)
                {
                    if (c == 'n'
                        //&&
                        //this.encoded[this.encPos + 1] == 'u'
                        // &&
                        //this.encoded[this.encPos + 2] == 'l'
                        // &&
                        //this.encoded[this.encPos + 3] == 'l'
                        )
                    {
                        ret = true;
                        this.encPos += 3;
                    }
                    else
                    {
                        this.encPos--;
                        return false;
                    }
                }

                if (c == ',' || c == ']' || c == '}')
                {
                    this.encPos--;
                    break;
                }

            }

            return ret;
        }

        string GetNumber(bool checkNull)
        {
            if (checkNull && CheckNull())
                return null;
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    break;
                var c = this.encoded[this.encPos];
                if (CheckSkip(c))
                    continue;
                
                if (c == ',' || c == ']' || c == '}')
                {
                    this.encPos--;                   
                    break;
                }
                sb.Append(c);
            }

            return sb.ToString();
           
        }

        string GetBoolean(bool checkNull)
        {
            if (checkNull && CheckNull())
                return null;
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    break;
                var c = this.encoded[this.encPos];
                if (CheckSkip(c))
                    continue;
               
                if (c == ',' || c == ']' || c == '}')
                {
                    this.encPos--;                   
                    break;
                }
                sb.Append(c);
            }

            return sb.ToString();

        }


        ///// <summary>
        ///// In case if object is deserialized, first we deserialize property and its name 
        ///// will be returned back, then due to the property name can be choosen deserializer
        ///// </summary>
        ///// <returns></returns>
        //public string GetProperty()
        //{
        //    string s;
        //    while (true)
        //    {
        //        this.encPos++;
        //        if (this.encPos >= this.encoded.Length)
        //            return String.Empty;
        //        var c = this.encoded[this.encPos];

        //        if (c == '{' || c == ',')
        //        {
        //            s = GetStr(false);
        //            if (!String.IsNullOrEmpty(s))
        //                SkipDelimiter();
        //            return s;
        //        }
        //        else if (c == '}')
        //            return String.Empty; //correct end of object

        //        continue;

        //    }
        //}

        //public JsonDecoder SkipProperty(bool array = false)
        //{
        //    string s;
        //    while (true)
        //    {
        //        this.encPos++;
        //        if (this.encPos >= this.encoded.Length)
        //            return null;
        //        var c = this.encoded[this.encPos];

        //        if (
        //            (!array && (c == '{' || c == ','))
        //            ||
        //            (array && (c == '[' || c == ','))
        //            )
        //        {
        //            if (!array)
        //            {
        //                s = GetStr(false);
        //                if (!String.IsNullOrEmpty(s))
        //                    SkipDelimiter();
        //            }
        //            return this;
        //        }
        //        else if (
        //            (!array && c == '}')
        //            ||
        //            (array && c == ']')
        //            )
        //            return null; //correct end of object

        //        continue;

        //    }
        //}


        //must be used as a default call
        public void SkipValue()
        {
            bool start = true;
            char d = ' '; //default for number
            char o = ' ';
            int cnt = 0;
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    break;
                var c = this.encoded[this.encPos];

                if (CheckSkip(c))
                    continue;

                if(start)
                {
                    if (c == '\"')
                    {
                        d = '\"';
                        o = '\"';
                    }
                    else if (c == '[')
                    {
                        d = '[';
                        o = ']';
                    }
                    else if (c == '{')
                    {
                        d = '{';
                        o = '}';
                    }
                    else if(c == 'n') //null
                    {
                        this.encPos+=3;
                        return;
                    }

                    start = false;
                }
                else
                {
                    if(d == ' ' && (c == ',' || c=='}' || c==']'))
                    {
                        this.encPos--;
                        return;
                    }
                    else if(d == '\"')
                    {
                        if(c == '\\')
                        {
                            this.encPos++;
                            continue;
                        }
                        else if(c == o)
                            return;
                    }
                    else if (d == '[' || d == '{')
                    {
                        if(c == d)
                        {
                            cnt++;
                        }
                        else if(c == o)
                        {
                            if (cnt == 0)
                                return;
                            else
                                cnt--;
                        }
                    }
                }

            }
        }


        /// <summary>
        /// Skips :
        /// </summary>
        void SkipDelimiter()
        {
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    break;
                var c = this.encoded[this.encPos];
               
                if (c == ':')
                    return;
                else
                    continue;
            }
        }
        string GetStr(bool checkNull = true)
        {
            if (checkNull && CheckNull())
                return null;

            StringBuilder sb = new StringBuilder();
            int state = 0; //0 - before strting, 1 - inSTring
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    break;
                var c = this.encoded[this.encPos];

                if (state != 1)
                {
                    if (c == '}')//probably end of object, that even didn't start
                        return String.Empty;
                    else if (c == '\"')
                        state = 1;

                    continue;
                }
                else
                {
                    if (c == '\\')
                    {
                        this.encPos++;
                        c = this.encoded[this.encPos];
                    }
                    else if (c == '\"')
                        break;

                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        //public T Get<T>(T prop)
        //{
        //    var typeofT = typeof(T);
        //    (K)Convert.ChangeType(s, typeof(K))
        //    if (typeofT == typeof(int))
        //        return (T)(object)this.GetInt();
        //    else if (typeof(System.Collections.IList).IsAssignableFrom(typeofT))
        //    {
        //        var arg1t = typeofT.GetGenericArguments()[0];
        //        if (arg1t.IsPrimitive)
        //        {

        //        }
        //        else if (typeof(IJsonEncoder).IsAssignableFrom(arg1t))
        //        {
        //            var inst = (T)Activator.CreateInstance(typeofT);
        //            var inst1 = Activator.CreateInstance(arg1t);

        //            System.Reflection.MethodInfo method = arg1t.GetMethod("BiserJsonDecoder");
        //            System.Reflection.MethodInfo genericMethod = method.MakeGenericMethod(arg1t);
        //            var obj1 = genericMethod.Invoke(inst1, new object[] { this });
        //            this.GetCollection(
        //                           () =>
        //                           {
        //                               return genericMethod.Invoke(inst1, new object[] { this });
        //                           }, inst, true);

        //            m.P4 = decoder.CheckNull() ? null : new List<TS2>();
        //            if (m.P4 != null)
        //                decoder.GetCollection(
        //                           () => { return TS2.BiserJsonDecode(null, decoder); }, m.P4, true);
        //            foreach (var item)
        //                var inst = Activator.CreateInstance(arg1t);
        //        }
        //    }



        //    return default(T);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="K"></typeparam>
        ///// <typeparam name="V"></typeparam>
        ///// <param name="fk"></param>
        ///// <param name="fv"></param>
        ///// <param name="dict"></param>
        ///// <param name="isNullChecked"></param>
        //public void GetCollection<K, V>(Func<K> fk, Func<V> fv, IDictionary<K, V> dict, bool isNullChecked = false)
        //{
        //    GetCollection(fk, fv, dict, null, null, isNullChecked);
        //}
        //public void GetCollection<K>(Func<K> fk, IList<K> lst, bool isNullChecked = false)
        //{
        //    GetCollection(fk, fk, null, lst, null, isNullChecked);
        //}

        //public void GetCollection<K>(Func<K> fk, ISet<K> set, bool isNullChecked = false)
        //{
        //    GetCollection(fk, fk, null, null, set, isNullChecked);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="K"></typeparam>
        ///// <typeparam name="V"></typeparam>
        ///// <param name="fk"></param>
        ///// <param name="fv"></param>
        ///// <param name="dict"></param>
        ///// <param name="lst"></param>
        ///// <param name="set"></param>
        ///// <param name="isNullChecked"></param>
        //void GetCollection<K, V>(Func<K> fk, Func<V> fv, IDictionary<K, V> dict, IList<K> lst, ISet<K> set, bool isNullChecked = false)
        //{
        //    if (!isNullChecked)
        //    {
        //        if (this.CheckNull())
        //        {
        //            dict = null;
        //            lst = null;
        //            set = null;
        //            return;
        //        }
        //    }

        //    char eoc = (dict != null) ? '}' : ']'; //end of collection
        //    char soc = (dict != null) ? '{' : '['; //start of collection

        //    int state = 0; //collection start
        //    string s;
        //    while (true)
        //    {
        //        this.encPos++;
        //        if (this.encPos >= this.encoded.Length)
        //            return;
        //        var c = this.encoded[this.encPos];

        //        if (CheckSkip(c))
        //            continue;
        //        if (c == ',')
        //            continue;
        //        if (c == eoc)
        //            return;
        //        if (state == 0)
        //        {
        //            if (c == soc)
        //                state = 1; //In collection
        //        }
        //        else
        //        {
        //            this.encPos--;
        //        }

        //        if (state == 1)
        //        {
        //            if (lst != null)
        //            {
        //                lst.Add(fk());
        //            }
        //            else if (set != null)
        //            {
        //                set.Add(fk());
        //            }
        //            else if (dict != null)
        //            {
        //                s = GetStr(false);
        //                SkipDelimiter();                        
        //                dict.Add((K)Convert.ChangeType(s, typeof(K)), fv());
        //            }
        //        }
        //    }

        //}//eof 


        /// <summary>
        /// Returns Key, Value must be retrieved extra
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        public IEnumerable<K> GetMap<K>()
        {
            bool array = false;
            if (this.CheckNull())
            {
            }
            else
            {

                char eoc = (!array) ? '}' : ']'; //end of collection
                char soc = (!array) ? '{' : '['; //start of collection

                int state = 0; //collection start                
                string s;
                while (true)
                {
                    this.encPos++;
                    if (this.encPos >= this.encoded.Length)
                        break;
                    var c = this.encoded[this.encPos];

                    if (CheckSkip(c))
                        continue;
                    if (c == ',')
                        continue;
                    if (c == eoc)
                        break;
                    if (state == 0)
                    {
                        if (c == soc)
                            state = 1; //In collection
                    }
                    else
                    {
                        this.encPos--;
                    }

                    if (state == 1)
                    {
                        if (array)
                        {
                            yield return default(K);
                        }
                        else
                        {
                            s = GetStr(false);
                            SkipDelimiter();
                            //dict.Add((K)Convert.ChangeType(s, typeof(K)), fv());
                            yield return (K)Convert.ChangeType(s, typeof(K));
                        }
                    }
                }
            }

        }//eof 


        public IEnumerable<int> GetArray()
        {
            bool array = true;
            if (this.CheckNull())
            {
            }
            else
            {

                char eoc = (!array) ? '}' : ']'; //end of collection
                char soc = (!array) ? '{' : '['; //start of collection

                int state = 0; //collection start                
                string s;
                while (true)
                {
                    this.encPos++;
                    if (this.encPos >= this.encoded.Length)
                        break;
                    var c = this.encoded[this.encPos];

                    if (CheckSkip(c))
                        continue;
                    if (c == ',')
                        continue;
                    if (c == eoc)
                        break;
                    if (state == 0)
                    {
                        if (c == soc)
                            state = 1; //In collection
                    }
                    else
                    {
                        this.encPos--;
                    }

                    if (state == 1)
                    {
                        if (array)
                        {
                            yield return 1;
                        }
                        //else
                        //{
                        //    s = GetStr(false);
                        //    SkipDelimiter();
                        //    //dict.Add((K)Convert.ChangeType(s, typeof(K)), fv());
                        //    yield return (K)Convert.ChangeType(s, typeof(K));
                        //}
                    }
                }
            }

        }//eof 







        public DateTime GetDateTime()
        {
            return ParseDateTime();           
        }

        public DateTime? GetDateTime_NULL()
        {
            if (CheckNull())
                return null;
            return ParseDateTime();           
        }

        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        DateTime ParseDateTime()
        {
            ulong v;            
            DateTime rdt;
            string s;
            switch (this.jsonSettings.DateFormat)
            {
                case JsonSettings.DateTimeStyle.Default:
                    //var tt3f = jsts1.P17.ToUniversalTime().Subtract(new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc)).TotalMilliseconds * 10000;   
                    /*"\/Date(13257180000000000)\/"*/
                    s = GetStr(false);                    
                    v = Convert.ToUInt64(s.Substring(0, s.Length - 2).Replace("/Date(", "")) / 10000;
                    //time if not UTC must be brought to UTC, stored in UTC and restored in UTC
                    rdt = epoch.AddMilliseconds(v);
                    return DateTime.SpecifyKind(rdt, DateTimeKind.Utc);
              
                case JsonSettings.DateTimeStyle.EpochTime:
                    //var tt3f = jsts1.P17.ToUniversalTime().Subtract(new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc)).TotalMilliseconds * 10000;   
                    /*"P17":13257818550000000*/
                    v = Convert.ToUInt64(GetNumber(false)) / 10000;
                    //time if not UTC must be brought to UTC, stored in UTC and restored in UTC
                    rdt = epoch.AddMilliseconds(v);
                    return DateTime.SpecifyKind(rdt, DateTimeKind.Utc);
                case JsonSettings.DateTimeStyle.ISO:
                case JsonSettings.DateTimeStyle.Javascript:
                    /*
                     * Encoder
                     * new DateTime(2018, 6, 5, 17,44,15,443, DateTimeKind.Local).ToString("o"); //Encoder ISO "2018-06-05T17:44:15.4430000Z" or "2018-06-05T17:44:15.4430000+02:00"
                     */
                    s = GetStr(false);
                    return DateTime.Parse(s, null, System.Globalization.DateTimeStyles.RoundtripKind);

            }

            return DateTime.MinValue;
        }

        public TimeSpan GetTimeSpan()
        {
            var s = GetStr(true);
            return s == null ? new TimeSpan() : (TimeSpan)TimeSpan.Parse(s);
        }


        public int GetInt()
        {
            return Int32.Parse(GetNumber(false));
        }

        public int? GetInt_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (int?)Int32.Parse(v);
        }

        public long GetLong()
        {
            return Int64.Parse(GetNumber(false));
        }

        public long? GetLong_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (long?)Int64.Parse(v);

        }

        public ulong GetULong()
        {
            return UInt64.Parse(GetNumber(false));
        }

        public ulong? GetULong_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (ulong?)UInt64.Parse(v);

        }

        public uint GetUInt()
        {
            return UInt32.Parse(GetNumber(false));
        }

        public uint? GetUInt_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (uint?)UInt32.Parse(v);
        }

        public short GetShort()
        {
            return short.Parse(GetNumber(false));
        }

        public short? GetShort_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (short?)short.Parse(v);
        }

        public ushort GetUShort()
        {
            return ushort.Parse(GetNumber(false));
        }

        public ushort? GetUShort_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (ushort?)ushort.Parse(v);
        }

        public bool GetBool()
        {
            var v = GetBoolean(false);
            return v.Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false;
        }

        public bool? GetBool_NULL()
        {
            var v = GetBoolean(true);
            return v == null ? null : (bool?)(v.Equals("true",StringComparison.OrdinalIgnoreCase) ? true : false);
        }

        public sbyte GetSByte()
        {
            return sbyte.Parse(GetNumber(false));
        }

        public sbyte? GetSByte_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (sbyte?)sbyte.Parse(v);
        }

        public byte GetByte()
        {
            return byte.Parse(GetNumber(false));
        }

        public byte? GetByte_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (byte?)byte.Parse(v);
        }

        public float GetFloat()
        {         
            return float.Parse(GetNumber(false), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
        }

        public float? GetFloat_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (float?)float.Parse(v, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
        }

        public double GetDouble()
        {
            return double.Parse(GetNumber(false), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
        }

        public double? GetDouble_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (double?)double.Parse(v, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);

        }

        public decimal GetDecimal()
        {
            return decimal.Parse(GetNumber(false), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
        }

        public decimal? GetDecimal_NULL()
        {
            var v = GetNumber(true);
            return v == null ? null : (decimal?)decimal.Parse(v, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
        }

        public char GetChar()
        {
            return GetStr(false)[0];
        }

        public char? GetChar_NULL()
        {
            var v = GetStr(true);
            return v == null ? null : (char?)v[0];

        }

        public string GetString()
        {
            return GetStr(true);
        }

        public byte[] GetByteArray()
        {
            var v = GetStr(true);
            return v == null ? null : Convert.FromBase64String(v);
        }

        public Guid GetGuid()
        {
            var v = GetStr(false);
            return new Guid(v);
        }

        public Guid? GetGuid_NULL()
        {
            var v = GetStr(true);
            return v == null ? null : (Guid?)(new Guid(v));
        }

    }//eoc
}//eon
