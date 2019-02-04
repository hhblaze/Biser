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
    internal class BinaryGenerator
    {
        HashSet<string> UsedVars = new HashSet<string>();        
        public HashSet<Type> UsedObjects = new HashSet<Type>();
        Type myType = null;

        public string Run(Type incomingType)
        {
            var tf = incomingType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            myType = incomingType;

            StringBuilder sb = new StringBuilder();

            StringBuilder sbEncode = new StringBuilder();
            StringBuilder sbDecode = new StringBuilder();
            Type iType = null;

            int varCnt = 0;
            int varCntTotal = 0;

            UsedVars.Clear();
            UsedObjects.Clear();

            //Binary Encoder
            List<string> endings = new List<string>();

            foreach (var f in tf)
            {              
                var name = f.Name;
                iType = f.PropertyType;//.FieldType;       

                if (iType == typeof(object))
                    continue;

                EncodeSingle(iType, sbEncode, name, varCnt);
            }
            

            //Binary Decoder
            varCnt = 0;
            varCntTotal = 0;
            UsedVars.Clear();

            foreach (var f in tf)
            {
                var name = f.Name;
                iType = f.PropertyType;//.FieldType;

                if (iType == typeof(object))
                    continue;

                //sbDecode.Append($"\n\t\t\t\tcase \"{name.ToLower()}\":");
                UsedVars.Add($"m.{name}");
                DecodeSingle(iType, sbDecode, $"m.{name}", varCnt, ref varCntTotal, null);
                varCnt = varCntTotal;
                //sbDecode.Append("\n\t\t\t\t\tbreak;");
               
            }

            //Binary Final view
            var nsLen = incomingType.FullName.Length - incomingType.Name.Length - 1;
            sb.Append(
               Resource1.tmplBinary
               .ReplaceMultiple(new Dictionary<string, string> {
                    {"{@ObjName}", incomingType.Name },
                    { "{@ContentEncode}", sbEncode.ToString()},
                    { "{@ContentDecode}", sbDecode.ToString()}
               }));

            var res = sb.ToString();

           // System.IO.File.WriteAllText(@"D:\Temp\1\TS6_Biser.cs", res);
            //Debug.WriteLine(res);
            //Console.WriteLine(res);
            return res;
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="iType"></param>
       /// <param name="sbEncode"></param>
       /// <param name="varName"></param>
       /// <param name="varCnt"></param>
        void EncodeSingle(Type iType, StringBuilder sbEncode, string varName, int varCnt)
        {
            //if (iType == typeof(byte[]))
            //{
            //    sbEncode.Append($"\nencoder.Add({varName});");
                
            //}
            //else 
            if (iType.IsArray)
            {
                if (iType == typeof(byte[]))
                {
                    sbEncode.Append($"\nencoder.Add({varName});");

                }
                else
                {
                    varCnt++;
                    int lv = varCnt;
                    sbEncode.Append($"\nif({varName} == null) \nencoder.Add((byte)1);\nelse {{");

                    if(iType.GetArrayRank() > 1)
                    {
                        sbEncode.Append($"\nfor(int it{lv}=0; it{lv} < {varName}.Rank; it{lv}++)");
                        sbEncode.Append($"\nencoder.Add({varName}.GetLength(it{lv}));");
                        varCnt++;
                        sbEncode.Append($"\nforeach(var el{varCnt} in {varName})");
                        EncodeSingle(iType.GetElementType(), sbEncode, $"el{varCnt}", varCnt);
                    }
                    else
                    { //else rank == 1 (jagged)


                        //not implemented must be represented as -> e.g. int[] must be List<int>
                        //int[][] can be represented as //so can be represented as List<List<List<

                        //implementation for list, but without initial count
                        //varCnt++;
                        //sbEncode.Append($"\nencoder.Add({varName}, (r{varCnt}) => {{");
                        //EncodeSingle(iType.GetElementType(), sbEncode, "r" + varCnt, varCnt);
                        //sbEncode.Append($"}});");
                    }

                    sbEncode.Append($"\n}}"); //eo if
                }
            }
            else if (iType.GetInterface("ICollection`1") != null)
            {              
                if (iType.GetInterface("ISet`1") != null || iType.GetInterface("IList`1") != null)
                {
                    varCnt++;
                  
                    sbEncode.Append($"\nencoder.Add({varName}, (r{varCnt}) => {{");
                    EncodeSingle(iType.GenericTypeArguments[0], sbEncode, "r" + varCnt, varCnt);
                    sbEncode.Append($"}});");
                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {
                    varCnt++;
                   
                    sbEncode.Append($"\nencoder.Add({varName}, (r{varCnt}) => {{");
                    EncodeSingle(iType.GenericTypeArguments[0], sbEncode, "r" + varCnt+".Key", varCnt);                 
                    EncodeSingle(iType.GenericTypeArguments[1], sbEncode, "r" + varCnt + ".Value", varCnt);
                    sbEncode.Append($"}});");
                }
                
            }
            else if (iType.GetInterface("ITuple") != null)
            {                
                int tn = 1;
                foreach (var gta in iType.GetGenericArguments())
                {             
                    EncodeSingle(gta, sbEncode,$"{varName}.Item{tn}", varCnt);                 
                    tn++;
                }
            }
            else
            {
                sbEncode.Append($"\nencoder.Add({varName});");                
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="sbDecode"></param>
        /// <param name="varName"></param>
        /// <param name="varCnt"></param>
        /// <param name="mapper"></param>
        int DecodeSingle(Type iType, StringBuilder sbDecode, string varName, int varCnt, ref int varCntTotal, MapperContent mapper)
        {
            
            if (iType == typeof(byte[]))
            {
                if (mapper != null)
                {
                    mapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));
                }

                //if (mapper != null)
                //    sbJsonDecode.Append("var ");
                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbDecode.Append("\nvar ");
                }
                else
                    sbDecode.Append("\n");
                sbDecode.Append($"{varName} = ");

                if (StandardTypes.STypes.TryGetValue(iType, out var tf))
                {
                    sbDecode.Append(tf.FGet);
                }

                sbDecode.Append(";");
            }
            else if (iType.IsArray)
            {
                StringBuilder msb1 = new StringBuilder();
                StringBuilder msb2 = new StringBuilder();
                StringBuilder msb3 = new StringBuilder();

                var myMapper = new MapperContent { };

                if (mapper != null)
                {
                    mapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));
                }

                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbDecode.Append("\nvar ");
                }
                else
                    sbDecode.Append("\n");
                sbDecode.Append($"{varName} = null;");

                sbDecode.Append($"\nif(!decoder.CheckNull()) {{");

                if (iType.GetArrayRank() > 1)
                {                  
                    for (int i = 0; i < iType.GetArrayRank(); i++)
                    {
                        if (i > 0)
                        {
                            msb1.Append(", ");
                            msb3.Append(", ");
                        }

                        msb1.Append("decoder.GetInt()");
                        msb3.Append($"ard{i}");
                        //---
                        if (i == iType.GetArrayRank() - 1)
                        {//last element
                            msb2.Append($"\nfor(int ard{i} = 0; ard{i} < {varName}.GetLength({i}); ard{i}++) {{");
                            varCnt++;
                            varCntTotal++;
                            UsedVars.Add($"{varName}[{msb3.ToString()}]");
                            DecodeSingle(iType.GetElementType(), msb2, $"{varName}[{msb3.ToString()}]", varCnt, ref varCntTotal, myMapper); //myMapper
                            msb2.Append($"\n}}");
                        }
                        else
                        {
                            msb2.Append($"\nfor(int ard{i} = 0; ard{i} < {varName}.GetLength({i}); ard{i}++)");
                        }
                    }
                    sbDecode.Append($"\n{varName} = new {myMapper.PrepareContent()}[");
                    sbDecode.Append(msb1.ToString());
                    msb1.Clear();
                    sbDecode.Append($"];");

                    sbDecode.Append($"\n{msb2.ToString()}");
                    //msb2.Append($"\n");
                    //sbDecode.Append($"\n");
                    //sbDecode.Append($"\n");
                }
                else
                {//jagged array

                    //not implemented must be represented as -> e.g. int[] must be List<int>
                    //int[][] can be represented as //so can be represented as List<List<List<
                }

                sbDecode.Append($"\n}}"); //eof decoder check NULL

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
                   
                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        msb.Append("\nvar ");
                    }
                    else
                        msb.Append("\n");

                    msb.Append($"{varName} = decoder.CheckNull() ? null : new {{@45879846845}}();");
                    msb.Append($"\nif({varName} != null){{");

                    //varCnt++;
                    //varCntTotal++;                  

                    msb.Append($"\n\tdecoder.GetCollection(() => {{");

                    StringBuilder sbi = new StringBuilder();
                    varCnt++;
                    varCntTotal++;
                    int pv2 = varCnt;
                    DecodeSingle(iType, sbi, $"pvar{pv2}", varCnt, ref varCntTotal, myMapper);
                    msb.Append(sbi.ToString());

                    //msb.Append($"\n\t\t{varName}.Add(pvar{pv2});"); //****
                    msb.Append($"\n\t\treturn pvar{pv2};"); //****
                    msb.Append($"\n\t}}, {varName}, true);");

                    msb.Append($"\n}}"); //eof if varname != null

                    myMapper.Lst.Add(">");

                    msb.Replace("{@45879846845}", myMapper.PrepareContent());

                    sbDecode.Append(msb.ToString());
                    if (mapper != null)
                        mapper.Lst.Add(myMapper.PrepareContent());
                    //mapper.Lst.AddRange(myMapper.Lst);
                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {

                    ////    iType = iType.GenericTypeArguments[1];

                    //Generating newGuid
                    var myMapper = new MapperContent { };

                    myMapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));
                    myMapper.Lst.Add("<");

                    //var kT = iType.GenericTypeArguments[0];
                    //var vT = iType.GenericTypeArguments[1];

                    myMapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType.GenericTypeArguments[0]));  //Key should be simple !!!!!!!!
                    myMapper.Lst.Add(", ");

                    //iType = iType.GenericTypeArguments[1];

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

                    //varCnt++;
                    //varCntTotal++;
                    //int pv1 = varCnt;
                    //msb.Append($"\n\tforeach(var el{pv1} in decoder.GetDictionary<{kT}>()) {{");
                    msb.Append($"\n\tdecoder.GetCollection(() => {{");

                    StringBuilder sbi = new StringBuilder();

                    varCnt++;
                    varCntTotal++;
                    int pv2 = varCnt;
                    DecodeSingle(iType.GenericTypeArguments[0], sbi, $"pvar{pv2}", varCnt, ref varCntTotal, null);
                    msb.Append(sbi.ToString());
                    msb.Append($"\n\t\treturn pvar{pv2};"); 
                    msb.Append($"\n}},");
                    msb.Append($"\n() => {{");

                    sbi.Clear();
                    varCnt++;
                    varCntTotal++;
                    int pv3 = varCnt;
                    DecodeSingle(iType.GenericTypeArguments[1], sbi, $"pvar{pv3}", varCnt, ref varCntTotal, myMapper);

                    msb.Append(sbi.ToString());

                    msb.Append($"\n\t\treturn pvar{pv3};");
                    //msb.Append($"\n\t\t{varName}.Add(el{pv1}, pvar{pv2});"); //****

                    msb.Append($"\n\t}}, {varName}, true);");

                    msb.Append($"\n}}"); //eof if varname != null

                    myMapper.Lst.Add(">");

                    msb.Replace("{@45879846845}", myMapper.PrepareContent());


                    sbDecode.Append(msb.ToString());
                    if (mapper != null)
                        mapper.Lst.Add(myMapper.PrepareContent());
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

                    int varCntNew = DecodeSingle(gta, sbi, $"pvar{varCnt}", varCnt, ref varCntTotal, myMapper);

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

                //varCnt++;
                //varCntTotal++;
                //msb.Append($"\nfor(int tupleProps{varCnt} = 1;tupleProps{varCnt} <= {iType.GetGenericArguments().Length};tupleProps{varCnt}++){{");


                int elcnt = 0;
                foreach (var el in tuplSbi)
                {
                    elcnt++;                
                    msb.Append(el);
                
                }
                //msb.Append("\n}");
                                               
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


                sbDecode.Append(msb.ToString());

                if (mapper != null)
                    mapper.Lst.Add($"Tuple<{tupleType.ToString()}>");
            }
            else
            {
                ////or simple type
                if (mapper != null)
                {
                    mapper.Lst.Add(StandardTypes.GetCSharpTypeName(iType));

                    //sbJsonDecode.Append("var ");
                }

                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbDecode.Append("\nvar ");
                }
                else
                    sbDecode.Append("\n");

                sbDecode.Append($"{varName} = ");
                //sbJsonDecode.Append($"var {varName} = ");

                if (StandardTypes.STypes.TryGetValue(iType, out var tf))
                {
                    sbDecode.Append(tf.FGet);
                }
                else
                {
                    if (iType == myType)
                        throw new Exception("Cross-Reference exception. Object can't contain itself as a property");

                    //adding object to UsedObjects list
                    UsedObjects.Add(iType);

                    sbDecode.Append(StandardTypes.GetCSharpTypeName(iType) + ".BiserDecode(null, decoder)");
                }


                sbDecode.Append(";");

            }

            return varCntTotal;
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
