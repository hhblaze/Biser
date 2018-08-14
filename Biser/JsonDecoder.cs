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
        JsonDecoder rootDecoder = null;
        //bool externalDecoderExists = false;

        internal int encPos = -1;

        public JsonDecoder(string encoded)
        {

            this.rootDecoder = this;
            this.encoded = encoded;
            if (encoded == null || encoded.Length == 0)
                return;


            //this.rootDecoder = this;
            //this.activeDecoder = this;

        }

        public JsonDecoder(JsonDecoder decoder)//, bool isCollection = false)
        {
            this.rootDecoder = decoder.rootDecoder;
            //externalDecoderExists = true;

            //if (!isCollection)
            //{
            //    //var prot = this.rootDecoder.GetDigit();
            //    //if (prot == 1)
            //    //    IsNull = true;
            //}
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
                this.rootDecoder.encPos++;
                if (this.rootDecoder.encPos >= this.rootDecoder.encoded.Length)
                    break;
                var c = this.rootDecoder.encoded[this.rootDecoder.encPos];
                if (CheckSkip(c))
                    continue;
                if (!ret)
                {
                    if (c == 'n'
                        &&
                        this.rootDecoder.encoded[this.rootDecoder.encPos + 1] == 'u'
                         &&
                        this.rootDecoder.encoded[this.rootDecoder.encPos + 2] == 'l'
                         &&
                        this.rootDecoder.encoded[this.rootDecoder.encPos + 3] == 'l'
                        )
                    {
                        ret = true;
                        this.rootDecoder.encPos += 3;
                    }
                    else
                    {
                        this.rootDecoder.encPos--;
                        return false;
                    }
                }

                if (c == ',' || c == ']' || c == '}')
                {
                    this.rootDecoder.encPos--;                    
                    break;
                }

            }

            return ret;
        }

        string GetNumber()
        {
            if (CheckNull())
                return null;
            StringBuilder sb = new StringBuilder();
            while(true)
            {
                this.rootDecoder.encPos++;
                if (this.rootDecoder.encPos >= this.rootDecoder.encoded.Length)
                    break;
                var c = this.rootDecoder.encoded[this.rootDecoder.encPos];
                if (CheckSkip(c))
                    continue;
               // if (c == ',' || c == ':' || c == ']' || c == '}')
               if (c == ',' || c == ']' || c == '}')
                {
                    this.rootDecoder.encPos--;
                    //lastChar = c;
                    break;
                }
                sb.Append(c);
            }
            
            return sb.ToString();
            //return  ret == "null" ? null : ret;
        }

        bool objectHasStarted = false;

        /// <summary>
        /// In case if object is deserialized, first we deserialize property and its name 
        /// will be returned back, then due to the property name can be choosen deserializer
        /// </summary>
        /// <returns></returns>
        public string GetPropertyName()
        {
            string s;
            while (true)
            {
                this.rootDecoder.encPos++;
                if (this.rootDecoder.encPos >= this.rootDecoder.encoded.Length)
                    return String.Empty;
                var c = this.rootDecoder.encoded[this.rootDecoder.encPos];
                if (CheckSkip(c))
                    continue;               

                if (!objectHasStarted)
                {
                    if (c == '{')
                    {                       
                        objectHasStarted = true;
                        s = GetString();
                        if (!String.IsNullOrEmpty(s))
                            SkipDelimiter();
                        return s;
                    }
                }
                else
                {
                    if (c == ',')
                        continue;
                    else if (c == '}')
                        return String.Empty; //correct end of object
                    else
                    {
                        this.rootDecoder.encPos--;
                        s = GetString();
                        if (!String.IsNullOrEmpty(s))
                            SkipDelimiter();
                        return s;
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
                this.rootDecoder.encPos++;
                if (this.rootDecoder.encPos >= this.rootDecoder.encoded.Length)
                    break;
                var c = this.rootDecoder.encoded[this.rootDecoder.encPos];
                if (CheckSkip(c))
                    continue;
                if (c == ':')
                    return;
            }
        }
        public string GetString()
        {
            if (CheckNull())
                return null;

            StringBuilder sb = new StringBuilder();
            int state = 0; //0 - before strting, 1 - inSTring
            while (true)
            {
                this.rootDecoder.encPos++;
                if (this.rootDecoder.encPos >= this.rootDecoder.encoded.Length)
                    break;
                var c = this.rootDecoder.encoded[this.rootDecoder.encPos];
                if (state != 1 && CheckSkip(c))
                    continue;
                else if (state != 1 && c == '}') //probably end of object, that even didn't start
                {
                    return String.Empty;
                }
                else if (state == 1 && c == '\\')
                {
                    this.rootDecoder.encPos++;
                    c = this.rootDecoder.encoded[this.rootDecoder.encPos];
                }
                else if (c == '\"')
                {
                    if (state == 1)
                        break;
                    else
                    {
                        state = 1;
                        continue;
                    }
                }

                sb.Append(c);
            }
         
            return sb.ToString();
        }

        public DateTime GetDateTime()
        {
            var s = GetString();
            return DateTime.UtcNow;
        }

        public DateTime? GetDateTime_NULL()
        {
            var s = GetString();
            return s == null ? null : (DateTime?)DateTime.UtcNow;
        }


        public int GetInt()
        {
            return Int32.Parse(GetNumber());            
        }

        public int? GetInt_NULL()
        {
            var v = GetNumber();
            return v == null ? null : (int?)Int32.Parse(v);
        }

        public long GetLong()
        {
            return Int64.Parse(GetNumber());
        }

        public long? GetLong_NULL()
        {
            var v = GetNumber();
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
                if(this.rootDecoder.CheckNull())
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
                this.rootDecoder.encPos++;
                if (this.rootDecoder.encPos >= this.rootDecoder.encoded.Length)
                    return;
                var c = this.rootDecoder.encoded[this.rootDecoder.encPos];
                Console.WriteLine(c);
                if (CheckSkip(c))
                    continue;
                if (c == ',')
                    continue;
                if (c == eoc)
                    return;
                if (state == 0)
                {
                    if(c == soc)
                        state = 1; //In collection
                }
                else
                {
                    this.rootDecoder.encPos--;
                }

                if(state == 1)
                {
                    if (lst != null)
                        lst.Add(fk());
                    else if (set != null)
                        set.Add(fk());
                    else if (dict != null)
                    {
                        s = GetString();
                        SkipDelimiter();
                        dict.Add((K)Convert.ChangeType(s, typeof(K)), fv());
                    }
                }
            }

        }//eof 

    }
}
