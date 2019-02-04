using BiserObjectify.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BiserObjectify
{
    internal class JsonGenerator
    {
        HashSet<string> UsedVars = new HashSet<string>();
        public HashSet<Type> UsedObjects = new HashSet<Type>();
        Type myType = null;

        string tmplEnc6 = "encoder.Add(\"PROP\", PROP"; //PROP
        string tmplEnc6ending = ");\n"; //PROP        
        string tmplEnc7 = ", (RN)=> { encoder.Add("; //PROP
        string tmplEnc7ending = "); }"; //PROP
        string tmplEnc8 = "encoder.Add(\"PROP\", PROP == null ? new Dictionary<string,Action>() : new Dictionary<string,Action>() {"; //PROP
        string tmplEnc8ending = "});\n"; //PROP        
        string tmplEnc9 = "{ \"ITEMPROP\",()=>encoder.Add(";// PROP.ITEMPROP"; //PROP
        string tmplEnc9ending = ")},"; //PROP

        public string Run(Type incomingType)
        {
            var tf = incomingType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            myType = incomingType;

            StringBuilder sb = new StringBuilder();

            StringBuilder sbJsonEncode = new StringBuilder();
            StringBuilder sbJsonDecode = new StringBuilder();
            Type iType = null;

            UsedVars.Clear();
            UsedObjects.Clear();

            //JSON Encoder
            List<string> endings = new List<string>();

            foreach (var f in tf)
            {              
                var name = f.Name;
                iType = f.PropertyType;//.FieldType;      

                if (iType == typeof(object))
                    continue;

                endings.Clear();              

                if (iType.GetInterface("ITuple") != null)
                {

                    sbJsonEncode.Append(tmplEnc8.Replace("PROP", name));
                    int tn = 1;
                    foreach (var gta in iType.GetGenericArguments())
                    {
                        sbJsonEncode.Append(tmplEnc9.Replace("ITEMPROP", "Item" + tn.ToString()));//.Replace("PROP", name));                                      
                        EncodeSingle(gta, sbJsonEncode, name + ".Item" + tn.ToString(), 0, true);
                        sbJsonEncode.Append(tmplEnc9ending);
                        tn++;
                    }

                    sbJsonEncode.Append(tmplEnc8ending);
                    continue;
                }
                else
                {
                    sbJsonEncode.Append(tmplEnc6.Replace("PROP", name));
                    endings.Add(tmplEnc6ending);
                }

                //EncodeSingle(iType, sbJsonEncode, name);
                EncodeSingle(iType, sbJsonEncode, "");

                for (int i = endings.Count - 1; i >= 0; i--)
                {
                    sbJsonEncode.Append(endings[i]);
                }
            }



            //JSON Decoder
            int varCnt = 0;
            int varCntTotal = 0;

            foreach (var f in tf)
            {
                var name = f.Name;
                iType = f.PropertyType;//.FieldType;

                if (iType == typeof(object))
                    continue;

                sbJsonDecode.Append($"\n\t\t\t\tcase \"{name.ToLower()}\":");
                UsedVars.Add($"m.{name}");
                DecodeSingle(iType, sbJsonDecode, $"m.{name}", varCnt, ref varCntTotal, null);
                varCnt = varCntTotal;
                sbJsonDecode.Append("\n\t\t\t\t\tbreak;");
               
            }

            //JSON Final view
            //var nsLen = incomingType.FullName.Length - incomingType.Name.Length - 1;
            sb.Append(
                Resource1.tmplJson
                .ReplaceMultiple(new Dictionary<string, string> {
                    {"{@ObjName}", incomingType.Name },
                    { "{@ContentJsonEncode}", sbJsonEncode.ToString()},
                    { "{@ContentJsonDecode}", sbJsonDecode.ToString()}
                }));
                //.Replace("{@ObjName}", incomingType.Name).Replace("{@ContentJsonEncode}", sbJsonEncode.ToString()).Replace("{@ContentJsonDecode}", sbJsonDecode.ToString()));

            var res = sb.ToString();

            //System.IO.File.WriteAllText(@"D:\Temp\1\TS6_Biser.cs", res);

            //Debug.WriteLine(res);
            //Console.WriteLine(res);

            return res;
        }

       
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="sbJsonDecode"></param>
        /// <param name="varName"></param>
        /// <param name="varCnt"></param>
        /// <param name="mapper"></param>
        int DecodeSingle(Type iType, StringBuilder sbJsonDecode, string varName, int varCnt, ref int varCntTotal, MapperContent mapper)
        {
            
            if (iType == typeof(byte[]))
            {
                if (mapper != null)
                {
                    mapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));
                }

                //if (mapper != null)
                //    sbJsonDecode.Append("var ");
                if(!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbJsonDecode.Append("\nvar ");
                }
                else
                    sbJsonDecode.Append("\n");
                sbJsonDecode.Append($"{varName} = ");

                if (StandardTypes.STypes.TryGetValue(iType, out var tf))
                {
                    sbJsonDecode.Append(tf.FGet);
                }

                sbJsonDecode.Append(";");
            }
            else if (iType.IsArray)
            {
                // throw new NotSupportedException();

                //not implemented must be represented as -> e.g. int[] must be List<int>
            }
            else if (iType.GetInterface("ICollection`1") != null)
            {
                StringBuilder msb = new StringBuilder();

                if (iType.GetInterface("ISet`1") != null || iType.GetInterface("IList`1") != null)
                {
                    //Generating newGuid                   
                    var myMapper = new MapperContent { };

                    myMapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));
                    myMapper.Lst.Add("<");

                    iType = iType.GenericTypeArguments[0];

                    //if (mapper != null)
                    //    msb.Append("var ");
                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        msb.Append("\nvar ");
                    }
                    else
                        msb.Append("\n");

                    msb.Append($"{varName} = decoder.CheckNull() ? null : new {{@45879846845}}();");
                    msb.Append($"\nif({varName} != null){{");

                    varCnt++;
                    varCntTotal++;
                    int pv1 = varCnt;
                    msb.Append($"\n\tforeach(var el{pv1} in decoder.GetList()) {{");

                    StringBuilder sbi = new StringBuilder();
                    varCnt++;
                    varCntTotal++;
                    int pv2 = varCnt;
                    DecodeSingle(iType, sbi, $"pvar{pv2}", varCnt, ref varCntTotal, myMapper);
                    msb.Append(sbi.ToString());

                    msb.Append($"\n\t\t{varName}.Add(pvar{pv2});");
                    msb.Append($"\n\t}}");

                    msb.Append($"\n}}"); //eof if varname != null

                    myMapper.Lst.Add(">");

                    msb.Replace("{@45879846845}", myMapper.PrepareContent());

                    sbJsonDecode.Append(msb.ToString());
                    if (mapper != null)
                        mapper.Lst.Add(myMapper.PrepareContent());
                    //mapper.Lst.AddRange(myMapper.Lst);
                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {

                    //    iType = iType.GenericTypeArguments[1];

                    //Generating newGuid
                    var myMapper = new MapperContent { };

                    myMapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));
                    myMapper.Lst.Add("<");

                    var kT = iType.GenericTypeArguments[0];
                    //var vT = iType.GenericTypeArguments[1];

                    myMapper.Lst.Add(StandardTypes.GetCSharpTypeName(kT));  //Key should be simple !!!!!!!!
                    myMapper.Lst.Add(", ");

                    iType = iType.GenericTypeArguments[1];

                    //if (mapper != null)
                    //    msb.Append("var ");
                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        msb.Append("\nvar ");
                    }
                    else
                        msb.Append("\n");

                    msb.Append($"{varName} = decoder.CheckNull() ? null : new {{@45879846845}}();");
                    msb.Append($"\nif({varName} != null){{");

                    varCnt++;
                    varCntTotal++;
                    int pv1 = varCnt;
                    msb.Append($"\n\tforeach(var el{pv1} in decoder.GetDictionary<{kT}>()) {{");

                    StringBuilder sbi = new StringBuilder();
                    varCnt++;
                    varCntTotal++;
                    int pv2 = varCnt;
                    DecodeSingle(iType, sbi, $"pvar{pv2}", varCnt, ref varCntTotal, myMapper);

                    msb.Append(sbi.ToString());

                    msb.Append($"\n\t\t{varName}.Add(el{pv1}, pvar{pv2});");

                    msb.Append($"\n\t}}");

                    msb.Append($"\n}}"); //eof if varname != null

                    myMapper.Lst.Add(">");

                    msb.Replace("{@45879846845}", myMapper.PrepareContent());

                    ////Filling data from myMapper to 
                    //foreach (var mapdata in myMapper.Lst)
                    //{
                    //    //   res = res.Replace($"{{@{me.Key}}}", me.Value.PrepareContent());
                    //}

                    sbJsonDecode.Append(msb.ToString());
                    if (mapper != null)
                        mapper.Lst.Add(myMapper.PrepareContent());
                    //mapper.Lst.AddRange(myMapper.Lst);

                }


            }
            else if (iType.GetInterface("ITuple") != null)
            {
                Dictionary<int, Type> dTuple = new Dictionary<int, Type>();
                StringBuilder sbi = new StringBuilder();
                MapperContent myMapper = null;
                StringBuilder msb = new StringBuilder();
                bool first = true;
                StringBuilder tupleType = new StringBuilder();

                List<string> tuplSbi = new List<string>();
                foreach (var gta in iType.GetGenericArguments())
                {
                    myMapper = new MapperContent { };
                    sbi.Clear();
                    varCnt++;
                    varCntTotal++;
                    dTuple.Add(varCnt, gta);

                   
                    UsedVars.Add($"pvar{varCnt}");

                    var varCntNew = DecodeSingle(gta, sbi, $"pvar{varCnt}", varCnt, ref varCntTotal, myMapper);
                                       
                    if (first)
                        first = false;
                    else
                        tupleType.Append(", ");
                    tupleType.Append(myMapper.PrepareContent());

                    var defaultValue = StandardTypes.GetDefaultValue(gta);
                    if (defaultValue == null)
                        defaultValue = $"default({myMapper.PrepareContent()})";
                    msb.Append($"\n{myMapper.PrepareContent()} pvar{varCnt} = {defaultValue};");
                    varCnt = varCntNew;
                    // tuplSbi.Add(sbi.ToString().Substring(4)); //cutting 'var '

                    ////////cutting 'var '  workaround
                    //////var sbis = sbi.ToString();
                    //////if (sbis.StartsWith("var "))
                    //////    sbis = sbis.Substring(4);
                    //////tuplSbi.Add(sbis);
                    tuplSbi.Add(sbi.ToString());

                }

                varCnt++;
                varCntTotal++;

                msb.Append($"\nforeach (var tupleProps{varCnt} in decoder.GetDictionary<string>()){{");
                msb.Append($"\nswitch(tupleProps{varCnt}){{");
                int elcnt = 0;
                foreach (var el in tuplSbi)
                {
                    elcnt++;
                    msb.Append($"\ncase \"Item{elcnt}\":");
                    msb.Append(el);
                    msb.Append($"\nbreak;");
                }
                msb.Append("\n}}");


                //if (mapper != null)
                //    msb.Append("var ");
                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    msb.Append("\nvar ");
                }
                else
                    msb.Append("\n");

                msb.Append($"{varName} = new Tuple<{tupleType.ToString()}>(");
                first = true;

                foreach (var el in dTuple)
                {
                    if (first)
                        first = false;
                    else
                        msb.Append(", ");
                    msb.Append($"pvar{el.Key}");
                }
                msb.Append(");");


                sbJsonDecode.Append(msb.ToString());

                if (mapper != null)
                    mapper.Lst.Add($"Tuple<{tupleType.ToString()}>");
            }
            else
            {
                //or simple type
                if (mapper != null)
                {
                    mapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));

                    //sbJsonDecode.Append("var ");
                }

                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbJsonDecode.Append("\nvar ");
                }
                else
                    sbJsonDecode.Append("\n");

                sbJsonDecode.Append($"{varName} = ");
                //sbJsonDecode.Append($"var {varName} = ");

                if (StandardTypes.STypes.TryGetValue(iType, out var tf))
                {
                    sbJsonDecode.Append(tf.FGet);
                }
                else
                {
                    if (iType == myType)
                        throw new Exception("Cross-Reference exception. Object can't contain itself as a property");

                    //adding object to UsedObjects list
                    UsedObjects.Add(iType);


                    sbJsonDecode.Append(StandardTypes.GetCSharpTypeName(iType) + ".BiserJsonDecode(null, decoder)");
                }


                sbJsonDecode.Append(";");

            }

            return varCntTotal;
        }

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="sbJsonEncode"></param>
        /// <param name="varName"></param>
        /// <param name="nest"></param>
        /// <param name="tuple"></param>
        void EncodeSingle(Type iType, StringBuilder sbJsonEncode, string varName, int nest = 0, bool tuple = false)
        {
            //int nest = 1;
            List<string> endings = new List<string>();


            if (iType == typeof(byte[]))
            {
                sbJsonEncode.Append(varName);
                return;
            }
            else if (iType.IsArray)
            {
                //not implemented must be represented as -> e.g. int[] must be List<int>
                //Multidemensional arrays are represented in BinaryBiser and not supported directly by javascript JSON
                //so can be represented as List<List<List<


                /*
                  t6.P11 = new int[2][];
    t6.P11[0] = new int[3];
    t6.P11[1] = new int[3];
    t6.P11[0][0] = 12;
    t6.P11[0][1] = 14;
    t6.P11[1][0] = 125;
    t6.P11[1][2] = 19;

    t6.P12 = new int[2, 3, 4];

    t6.P12[0, 0, 0] = 12;
    t6.P12[0, 0, 1] = 13;
    t6.P12[0, 1, 0] = 14;
    t6.P12[0, 1, 1] = 15;
    t6.P12[1, 0, 0] = 16;
    t6.P12[1, 0, 1] = 17;



    "P11":[[12,14,0],[125,0,19]],
    "P12":[2,3,4,0,0,0,12,13,0,0,14,15,0,0,0,0,0,0,16,17,0,0,0,0],



    t6.P12.Rank	3	int
    t6.P11.Rank	1	int
    t6.P11.LongLength	2	long
    t6.P11[0].Rank	1	int
    t6.P11[0].LongLength	3	long
                 */

            }
            else if (iType.GetInterface("ICollection`1") != null)
            {
                nest++;
                if (!String.IsNullOrEmpty(varName))  //when empty - coming from route
                    sbJsonEncode.Append(varName);

                //sbJsonEncode.Append(tmplEnc7.Replace("RN", "r" + (nest+1))); //", (RN)=> { encoder.Add("
                sbJsonEncode.Append(tmplEnc7.Replace("RN", "r" + nest)); //", (RN)=> { encoder.Add("
                endings.Add(tmplEnc7ending);

                if (iType.GetInterface("ISet`1") != null || iType.GetInterface("IList`1") != null)
                {
                    iType = iType.GenericTypeArguments[0];
                    //EncodeSingle(iType, sbJsonEncode, "r" + (nest + 1), (nest + 1));
                    EncodeSingle(iType, sbJsonEncode, "r" + nest, nest);

                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {
                    //sbJsonEncode.Append("r" + (nest + 1));
                    iType = iType.GenericTypeArguments[1];
                    //EncodeSingle(iType, sbJsonEncode, "r" + (nest + 1), (nest + 1));
                    EncodeSingle(iType, sbJsonEncode, "r" + nest, nest);
                }
                //else if (iType.GetInterface("ITuple") != null) //Special case 
                //{
                //    //Dictionary will be added
                //}
                //else
                //{
                //   // sbJsonEncode.Append("r" + (nest + 1));
                //}

            }
            else if (iType.GetInterface("ITuple") != null)
            {
                //Tuple comes here iType.GenericTypeArguments	{System.Type[3]}
                int tn = 1;
                sbJsonEncode.Append("(" + varName + " == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>(){");
                foreach (var gta in iType.GetGenericArguments())
                {
                    sbJsonEncode.Append(tmplEnc9.Replace("ITEMPROP", "Item" + tn.ToString()));//.Replace("PROP", name));
                    //EncodeSingle(gta, sbJsonEncode, varName + ".Item" + tn.ToString(), (nest+1), true);                    
                    EncodeSingle(gta, sbJsonEncode, varName + ".Item" + tn.ToString(), nest, true);
                    sbJsonEncode.Append(tmplEnc9ending);
                    tn++;
                }
                sbJsonEncode.Append("}");
                //skip
                return; //!!!!!!!!!!
            }
            else
            {
                //if(tuple)
                //{
                sbJsonEncode.Append(varName);
                //}

                //or simple type
                return;
            }

            for (int i = endings.Count - 1; i >= 0; i--)
            {
                sbJsonEncode.Append(endings[i]);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        internal class MapperContent
        {
            public List<string> Lst = new List<string>();

            public string PrepareContent()
            {
                StringBuilder sb = new StringBuilder();
                foreach (var el in Lst)
                {
                    sb.Append(el);
                }

                return sb.ToString();
            }
        }
    }//eoc
}
