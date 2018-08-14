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
        //internal char lastChar;
        //JsonDecoder rootDecoder = null;
        //bool externalDecoderExists = false;

        internal int encPos = -1;
       

        public JsonDecoder(string encoded)
        {

            //this = this;

            this.encoded = encoded;
            if (encoded == null || encoded.Length == 0)
                return;

        }

        //public JsonDecoder(JsonDecoder decoder)//, bool isCollection = false)
        //{
        //    this = decoder.rootDecoder;         
        //}

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
                // if (c == ',' || c == ':' || c == ']' || c == '}')
                if (c == ',' || c == ']' || c == '}')
                {
                    this.encPos--;
                    //lastChar = c;
                    break;
                }
                sb.Append(c);
            }

            return sb.ToString();
            //return  ret == "null" ? null : ret;
        }

        //bool objectHasStarted = false;

        /// <summary>
        /// In case if object is deserialized, first we deserialize property and its name 
        /// will be returned back, then due to the property name can be choosen deserializer
        /// </summary>
        /// <returns></returns>
        public string GetProperty()
        {
            string s;
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    return String.Empty;
                var c = this.encoded[this.encPos];

                if (c == '{' || c == ',')
                {
                    s = GetString(false);
                    if (!String.IsNullOrEmpty(s))
                        SkipDelimiter();
                    return s;
                }
                else if (c == '}')
                    return String.Empty; //correct end of object

                continue;

                //if (CheckSkip(c))
                //continue;

                //if (c == '{')
                //{
                //    s = GetString(false);
                //    if (!String.IsNullOrEmpty(s))
                //        SkipDelimiter();
                //    return s;
                //}
                //else if (c == ',')
                //    continue;
                //else if (c == '}')
                //    return String.Empty; //correct end of object
                //else
                //{
                //    this.encPos--;
                //    s = GetString(false);
                //    if (!String.IsNullOrEmpty(s))
                //        SkipDelimiter();
                //    return s;
                //}

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
        public string GetString(bool checkNull = true)
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

        public DateTime GetDateTime()
        {
            var s = GetString(false);
            return DateTime.UtcNow;
        }

        public DateTime? GetDateTime_NULL()
        {
            var s = GetString(true);
            return s == null ? null : (DateTime?)DateTime.UtcNow;
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
            return v == null ? null : (int?)Int64.Parse(v);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="fk"></param>
        /// <param name="fv"></param>
        /// <param name="dict"></param>
        /// <param name="isNullChecked"></param>
        public void GetCollection<K, V>(Func<K> fk, Func<V> fv, IDictionary<K, V> dict, bool isNullChecked = false)
        {
            GetCollection(fk, fv, dict, null, null, isNullChecked);
        }
        public void GetCollection<K>(Func<K> fk, IList<K> lst, bool isNullChecked = false)
        {
            GetCollection(fk, fk, null, lst, null, isNullChecked);
        }

        public void GetCollection<K>(Func<K> fk, ISet<K> set, bool isNullChecked = false)
        {
            GetCollection(fk, fk, null, null, set, isNullChecked);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="fk"></param>
        /// <param name="fv"></param>
        /// <param name="dict"></param>
        /// <param name="lst"></param>
        /// <param name="set"></param>
        /// <param name="isNullChecked"></param>
        void GetCollection<K, V>(Func<K> fk, Func<V> fv, IDictionary<K, V> dict, IList<K> lst, ISet<K> set, bool isNullChecked = false)
        {
            if (!isNullChecked)
            {
                if (this.CheckNull())
                {
                    dict = null;
                    lst = null;
                    set = null;
                    return;
                }
            }

            char eoc = (dict != null) ? '}' : ']'; //end of collection
            char soc = (dict != null) ? '{' : '['; //start of collection

            int state = 0; //collection start
            string s;
            while (true)
            {
                this.encPos++;
                if (this.encPos >= this.encoded.Length)
                    return;
                var c = this.encoded[this.encPos];

                if (CheckSkip(c))
                    continue;
                if (c == ',')
                    continue;
                if (c == eoc)
                    return;
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
                    if (lst != null)
                    {
                        lst.Add(fk());
                    }
                    else if (set != null)
                    {
                        set.Add(fk());
                    }
                    else if (dict != null)
                    {
                        s = GetString(false);
                        SkipDelimiter();                        
                        dict.Add((K)Convert.ChangeType(s, typeof(K)), fv());
                    }
                }
            }

        }//eof 

    }
}
