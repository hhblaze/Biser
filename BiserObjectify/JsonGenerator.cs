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

        int varCntTotal = 0;

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

            //int varCnt = 0;
            varCntTotal = 0;


            //JSON Encoder


            foreach (var f in tf)
            {
                var name = f.Name;
                iType = f.PropertyType;//.FieldType;       

                if (iType == typeof(object))
                    continue;

                EncodeSingle(iType, sbJsonEncode, name, true);
            }

            //List<string> endings = new List<string>();

            //foreach (var f in tf)
            //{
            //    var name = f.Name;
            //    iType = f.PropertyType;//.FieldType;      

            //    if (iType == typeof(object))
            //        continue;

            //    endings.Clear();

            //    if (iType.GetInterface("ITuple") != null)
            //    {

            //        sbJsonEncode.Append(tmplEnc8.Replace("PROP", name));
            //        int tn = 1;
            //        foreach (var gta in iType.GetGenericArguments())
            //        {
            //            sbJsonEncode.Append(tmplEnc9.Replace("ITEMPROP", "Item" + tn.ToString()));//.Replace("PROP", name));                                      
            //            EncodeSingle1(gta, sbJsonEncode, name + ".Item" + tn.ToString(), 0, true);
            //            sbJsonEncode.Append(tmplEnc9ending);
            //            tn++;
            //        }

            //        sbJsonEncode.Append(tmplEnc8ending);
            //        continue;
            //    }
            //    else
            //    {
            //        sbJsonEncode.Append(tmplEnc6.Replace("PROP", name));
            //        endings.Add(tmplEnc6ending);
            //    }

            //    //EncodeSingle(iType, sbJsonEncode, name);
            //    EncodeSingle1(iType, sbJsonEncode, "");

            //    for (int i = endings.Count - 1; i >= 0; i--)
            //    {
            //        sbJsonEncode.Append(endings[i]);
            //    }
            //}



            //JSON Decoder
            //varCnt = 0;
            varCntTotal = 0;

            foreach (var f in tf)
            {
                var name = f.Name;
                iType = f.PropertyType;//.FieldType;

                if (iType == typeof(object))
                    continue;

                sbJsonDecode.Append($"\n\t\t\t\tcase \"{name.ToLower()}\":");
                UsedVars.Add($"m.{name}");
                DecodeSingle(iType, sbJsonDecode, $"m.{name}");
                //varCnt = varCntTotal;
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


        void EncodeSingle(Type iType, StringBuilder sbJsonEncode, string varName, bool root)
        {

            //List<string> endings = new List<string>();

            if (iType.IsArray)
            {
                if (iType == typeof(byte[]))
                {
                    if (root)
                    {
                        sbJsonEncode.Append($"\nencoder.Add(\"{varName}\", {varName});");
                    }
                    else
                        sbJsonEncode.Append($"\nencoder.Add({varName});");
                }
                else
                {

                    if (iType.GetArrayRank() > 1)
                    {
                        //We can't encode array dimensions (that are ints or longs) in standard JSON together with ArrayType

                        Console.WriteLine("BiserObjectify: multi-dimensional arrays are supported in Binary serializer only!");
                        Debug.WriteLine("BiserObjectify: multi-dimensional arrays are supported in Binary serializer only!");
                        return;


                        ////Code itself is working generating first array of Dimensions, then arraay of values
                        ///
                        //varCntTotal++;

                        //sbJsonEncode.Append($"\nvar arrdim{varCntTotal}=new List<int>();");
                        //for (int i = 0; i < iType.GetArrayRank(); i++)
                        //{
                        //    sbJsonEncode.Append($"\narrdim{varCntTotal}.Add({varName}.GetLength({i}));");
                        //}

                        //var listType1 = typeof(List<>).MakeGenericType(typeof(int));
                        //EncodeSingle(listType1, sbJsonEncode, $"arrdim{varCntTotal}", true);

                        //varCntTotal++;
                        //int pv = varCntTotal;
                        //StringBuilder msb = new StringBuilder();
                        //var listType = typeof(List<>).MakeGenericType(iType.GetElementType());
                        //sbJsonEncode.Append($"\n{StandardTypes.GetFriendlyName(listType)} r{pv}= new {StandardTypes.GetFriendlyName(listType)}();");
                        //sbJsonEncode.Append($"\nforeach(var el in {varName})");
                        //sbJsonEncode.Append($"\nr{pv}.Add(el);");

                        //EncodeSingle(listType, msb, $"r{pv}", root);
                        //msb.Replace($"\"r{pv}\"", $"\"{varName}\"");
                        //sbJsonEncode.Append(msb.ToString());
                    }
                    else
                    {//one dimensional or jagged array                     
                        varCntTotal++;
                        int pv = varCntTotal;
                        StringBuilder msb = new StringBuilder();
                        sbJsonEncode.Append($"\nvar r{varCntTotal}={varName}.ToList();");
                        var listType = typeof(List<>).MakeGenericType(iType.GetElementType());
                        EncodeSingle(listType, msb, $"r{pv}", root);
                        msb.Replace($"\"r{pv}\"", $"\"{varName}\"");
                        sbJsonEncode.Append(msb.ToString());
                    }

                }

            }
            else if (iType.GetInterface("ICollection`1") != null)
            {
                if (root)
                {
                    sbJsonEncode.Append($"\nencoder.Add(\"{varName}\", {varName}");
                }
                else
                    sbJsonEncode.Append($"\nencoder.Add({varName}");

                if (iType.GetInterface("ISet`1") != null || iType.GetInterface("IList`1") != null)
                {
                    varCntTotal++;

                    sbJsonEncode.Append($", (r{varCntTotal}) => {{");
                    EncodeSingle(iType.GenericTypeArguments[0], sbJsonEncode, "r" + varCntTotal, false);
                    sbJsonEncode.Append($"}});");
                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {
                    varCntTotal++;

                    sbJsonEncode.Append($", (r{varCntTotal}) => {{");
                    EncodeSingle(iType.GenericTypeArguments[1], sbJsonEncode, "r" + varCntTotal + "", false);                 
                    sbJsonEncode.Append($"}});");
                }

            }
            else if (iType.GetInterface("ITuple") != null)
            {
                if (root)
                {
                    sbJsonEncode.Append($"\nencoder.Add(\"{varName}\", ");
                }
                else
                    sbJsonEncode.Append($"\nencoder.Add(");

                sbJsonEncode.Append($"({varName} == null) ? new Dictionary<string,Action>() : new Dictionary<string, Action>() {{");

                int tn = 1;
                foreach (var gta in iType.GetGenericArguments())
                {
                    sbJsonEncode.Append($"{((tn==1)?"":", ")}{{ \"Item{tn}\", () => {{");
                    EncodeSingle(gta, sbJsonEncode, $"{varName}.Item{tn}", false);
                    sbJsonEncode.Append($"}}}}");
                    tn++;
                }

                sbJsonEncode.Append($"}});");

            }
            else
            {
                if(root)
                {
                    sbJsonEncode.Append($"\nencoder.Add(\"{varName}\", {varName});");
                }
                else
                    sbJsonEncode.Append($"\nencoder.Add({varName});");

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="sbJsonDecode"></param>
        /// <param name="varName"></param>
        /// <param name="varCnt"></param>
        /// <param name="mapper"></param>
        void DecodeSingle(Type iType, StringBuilder sbJsonDecode, string varName)
        {
            
            if (iType == typeof(byte[]))
            {                
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
                StringBuilder msb1 = new StringBuilder();
                StringBuilder msb2 = new StringBuilder();
                StringBuilder msb3 = new StringBuilder();

                if(iType.GetArrayRank()>1)
                {
                    //Console.WriteLine("BiserObjectify: multi-dimensional arrays are supported in Binary serializer only!");
                    //Debug.WriteLine("BiserObjectify: multi-dimensional arrays are supported in Binary serializer only!");
                    return;
                }

                //Reading List to prepare transformation
                Dictionary<int, string> varmap = new Dictionary<int, string>();
                for (int i = 0; i < iType.GetArrayRank(); i++)
                {
                    var listType = typeof(List<>).MakeGenericType(iType.GetElementType());

                    varCntTotal++;                  
                    varmap.Add(i, $"intlst{varCntTotal}");
                    DecodeSingle(listType, sbJsonDecode, $"intlst{varCntTotal}");
                }

                //sbJsonDecode.Append("\n//------------------");

                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbJsonDecode.Append("\nvar ");
                }
                else
                    sbJsonDecode.Append("\n");

                for (int i = 0; i < iType.GetArrayRank(); i++)
                {
                    if (i > 0)
                    {
                        msb1.Append(", ");
                        msb3.Append(", ");
                    }

                    varCntTotal++;
                    int ardpv = varCntTotal;
                                        
                    msb1.Append($"{varmap[i]}.Count");
                    msb3.Append($"ard{ardpv}_{i}");
                    //---
                    if (i == iType.GetArrayRank() - 1)
                    {//last element
                        msb2.Append($"\nfor(int ard{ardpv}_{i} = 0; ard{ardpv}_{i} < {varName}.GetLength({i}); ard{ardpv}_{i}++) {{");

                        //msb2.Append($"\n{varName}[ard{ardpv}_{i}] = {varmap[i]}[ard{ardpv}_{i}];");
                        msb2.Append($"\n{varName}[{msb3.ToString()}] = {varmap[i]}[ard{ardpv}_{i}];");
                        msb2.Append($"\n}}");
                    }
                    else
                        msb2.Append($"\nfor(int ard{ardpv}_{i} = 0; ard{ardpv}_{i} < {varName}.GetLength({i}); ard{ardpv}_{i}++)");
                }

                int iof = 0;
                string strf = StandardTypes.GetFriendlyName(iType);
                StringBuilder revArr = new StringBuilder();
                for (int j = strf.Length - 1; j >= 0; j--)
                {
                    var l = strf[j];
                    if (l == '[' || l == ']' || l == ',')
                    {
                        iof++;
                        if (l == '[')
                            l = ']';
                        else if (l == ']')
                            l = '[';
                        revArr.Append(l);
                    }
                    else
                        break;
                }

                var revArrStr = revArr.ToString();
                revArrStr = revArrStr.Substring(revArrStr.IndexOf("]") + 1);

                if (iof > 0)
                    strf = $"{strf.Substring(0, strf.Length - iof)}[{msb1.ToString()}]{revArrStr.ToString()}";
                else
                    strf = $"{strf}[{msb1.ToString()}]";

                sbJsonDecode.Append($"{varName} = decoder.CheckNull() ? null : new {strf};");
                sbJsonDecode.Append($"\nif({varName} != null){{");
                sbJsonDecode.Append($"{msb2.ToString()}");
                sbJsonDecode.Append($"\n}}");
            }
            else if (iType.GetInterface("ICollection`1") != null)
            {
                StringBuilder msb = new StringBuilder();

                if (iType.GetInterface("ISet`1") != null || iType.GetInterface("IList`1") != null)
                {
                   
                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        msb.Append("\nvar ");
                    }
                    else
                        msb.Append("\n");

                    msb.Append($"{varName} = decoder.CheckNull() ? null : new {StandardTypes.GetFriendlyName(iType)}();");
                    msb.Append($"\nif({varName} != null){{");

                    varCntTotal++;
                    int pv1 = varCntTotal;
                    msb.Append($"\n\tforeach(var el{pv1} in decoder.GetList()) {{");

                    StringBuilder sbi = new StringBuilder();
                    
                    varCntTotal++;
                    int pv2 = varCntTotal;
                    DecodeSingle(iType.GenericTypeArguments[0], sbi, $"pvar{pv2}");
                    msb.Append(sbi.ToString());

                    msb.Append($"\n\t\t{varName}.Add(pvar{pv2});");
                    msb.Append($"\n\t}}");

                    msb.Append($"\n}}"); //eof if varname != null
                    
                    sbJsonDecode.Append(msb.ToString());                   
                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {
                    var kT = iType.GenericTypeArguments[0];
                   
                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        msb.Append("\nvar ");
                    }
                    else
                        msb.Append("\n");

                    msb.Append($"{varName} = decoder.CheckNull() ? null : new {StandardTypes.GetFriendlyName(iType)}();");
                    msb.Append($"\nif({varName} != null){{");

                    varCntTotal++;
                    int pv1 = varCntTotal;
                    msb.Append($"\n\tforeach(var el{pv1} in decoder.GetDictionary<{kT}>()) {{");

                    StringBuilder sbi = new StringBuilder();
                    
                    varCntTotal++;
                    int pv2 = varCntTotal;
                    DecodeSingle(iType.GenericTypeArguments[1], sbi, $"pvar{pv2}");

                    msb.Append(sbi.ToString());

                    msb.Append($"\n\t\t{varName}.Add(el{pv1}, pvar{pv2});");

                    msb.Append($"\n\t}}");

                    msb.Append($"\n}}"); //eof if varname != null

                    sbJsonDecode.Append(msb.ToString());
                   
                }


            }
            else if (iType.GetInterface("ITuple") != null)
            {
                Dictionary<int, Type> dTuple = new Dictionary<int, Type>();
                StringBuilder sbi = new StringBuilder();
               
                StringBuilder msb = new StringBuilder();
                bool first = true;
                StringBuilder tupleType = new StringBuilder();

                List<string> tuplSbi = new List<string>();
                foreach (var gta in iType.GetGenericArguments())
                {
                   
                    sbi.Clear();                    
                    varCntTotal++;
                    dTuple.Add(varCntTotal, gta);
                   
                    UsedVars.Add($"pvar{varCntTotal}");
                    DecodeSingle(gta, sbi, $"pvar{varCntTotal}");
                                       
                    if (first)
                        first = false;
                    else
                        tupleType.Append(", ");
                    tupleType.Append(StandardTypes.GetFriendlyName(gta));

                    var defaultValue = StandardTypes.GetDefaultValue(gta);
                    if (defaultValue == null)
                        defaultValue = $"default({StandardTypes.GetFriendlyName(gta)})";
                    msb.Append($"\n{StandardTypes.GetFriendlyName(gta)} pvar{varCntTotal} = {defaultValue};");
                    //varCnt = varCntNew;
                   
                    tuplSbi.Add(sbi.ToString());

                }

                varCntTotal++;

                msb.Append($"\nforeach (var tupleProps{varCntTotal} in decoder.GetDictionary<string>()){{");
                msb.Append($"\nswitch(tupleProps{varCntTotal}){{");
                int elcnt = 0;
                foreach (var el in tuplSbi)
                {
                    elcnt++;
                    msb.Append($"\ncase \"Item{elcnt}\":");
                    msb.Append(el);
                    msb.Append($"\nbreak;");
                }
                msb.Append("\n}}");

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
            }
            else
            {
                if (!UsedVars.Contains(varName))
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
                else
                {
                    if (iType == myType)
                        throw new Exception("Cross-Reference exception. Object can't contain itself as a property");

                    //adding object to UsedObjects list
                    UsedObjects.Add(iType);

                    sbJsonDecode.Append(StandardTypes.GetFriendlyName(iType) + ".BiserJsonDecode(null, decoder)");
                }

                sbJsonDecode.Append(";");

            }

            //return varCntTotal;
        }


    }//eoc
}
