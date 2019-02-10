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

        public string Run(Type incomingType, HashSet<string> exclusions)
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

                if (!(f.GetSetMethod()?.IsPublic ?? false)) //skipping without setter
                    continue;

                if (!(f.GetGetMethod()?.IsPublic ?? false)) //skipping without getter
                    continue;

                if (iType == typeof(object))
                    continue;

                if (exclusions.Contains(f.Name))
                    continue;

                EncodeSingle(iType, sbJsonEncode, name, true);
            }


            //JSON Decoder
            
            varCntTotal = 0;

            foreach (var f in tf)
            {
                var name = f.Name;
                iType = f.PropertyType;//.FieldType;

                if (!(f.GetSetMethod()?.IsPublic ?? false)) //skipping without setter
                    continue;

                if (!(f.GetGetMethod()?.IsPublic ?? false)) //skipping without getter
                    continue;

                if (iType == typeof(object))
                    continue;

                if (exclusions.Contains(f.Name))
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
                    sbJsonEncode.Append($"if({varName} != null) {{");
                    if (iType.GetArrayRank() > 1)
                    {

                        //Multi-dimensional arrays will be represented as a Tuple, where first element is a List<int> containing dimensions
                        //and second element is a sequence of an array elements.

                        //--------------check array null conditions

                        varCntTotal++;
                        int pvard = varCntTotal;
                        sbJsonEncode.Append($"\nvar arrdim{pvard}=new System.Collections.Generic.List<int>();");
                        for (int i = 0; i < iType.GetArrayRank(); i++)
                            sbJsonEncode.Append($"\narrdim{pvard}.Add({varName}.GetLength({i}));");

                        var listType1 = typeof(List<>).MakeGenericType(typeof(int));
                       
                        varCntTotal++;
                        int pv = varCntTotal;
                        StringBuilder msb = new StringBuilder();
                        var listType = typeof(List<>).MakeGenericType(iType.GetElementType());
                        sbJsonEncode.Append($"\n{StandardTypes.GetFriendlyName(listType)} r{pv}= new {StandardTypes.GetFriendlyName(listType)}();");
                        sbJsonEncode.Append($"\nforeach(var el in {varName})");
                        sbJsonEncode.Append($"\nr{pv}.Add(el);");

                        varCntTotal++;
                        int pvtpl = varCntTotal;
                        var listType2 = typeof(Tuple<,>).MakeGenericType(typeof(List<int>), listType);
                        sbJsonEncode.Append($"\nvar r{pvtpl} = new Tuple<System.Collections.Generic.List<int>, {StandardTypes.GetFriendlyName(listType)}>(arrdim{pvard}, r{pv});");
                        EncodeSingle(listType2, msb, $"r{pvtpl}", root);
                        msb.Replace($"\"r{pvtpl}\"", $"\"{varName}\"");
                        sbJsonEncode.Append(msb.ToString());
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
                    sbJsonEncode.Append($"}}");

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

                var listType = typeof(List<>).MakeGenericType(iType.GetElementType());
               

                if (iType.GetArrayRank()>1)
                {
                    var listType2 = typeof(Tuple<,>).MakeGenericType(typeof(List<int>), listType);
                    varCntTotal++;
                    int pv = varCntTotal;
                    DecodeSingle(listType2, sbJsonDecode, $"pv{pv}");

                    //at this momemnt we can have Tuple
                    //var pv1 = new Tuple<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.List<System.Int32>>(pvar2, pvar5);
                    for (int i = 0; i < iType.GetArrayRank(); i++)
                    {
                        if (i > 0)
                        {
                            msb1.Append(", ");                           
                        }
                        msb1.Append($"pv{pv}.Item1[{i}]");
                    }

                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        sbJsonDecode.Append("\nvar ");
                    }
                    else
                        sbJsonDecode.Append("\n");


                    //int iof = 0;
                    //string strf = StandardTypes.GetFriendlyName(iType);
                    //for (int j = strf.Length - 1; j >= 0; j--)
                    //{
                    //    var l = strf[j];
                    //    if (l == '[' || l == ']' || l == ',')
                    //    {
                    //        iof++;
                    //        if (l == '[')
                    //            break;
                    //    }
                    //    else
                    //        break;
                    //}
                    //sbJsonDecode.Append($"{varName} = new {strf.Substring(0, strf.Length - iof)}[{msb1.ToString()}];");

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

                  
                    sbJsonDecode.Append($"{varName} = new {strf};");

                    varCntTotal++;
                    int arenm = varCntTotal;

                    sbJsonDecode.Append($"\nvar arenm{arenm} = pv{pv}.Item2.GetEnumerator();");
                    sbJsonDecode.Append($"\narenm{arenm}.MoveNext();");

                    varCntTotal++;
                    int ardpv = varCntTotal;

                    for (int i = 0; i < iType.GetArrayRank(); i++)
                    {                        
                        if (i > 0)
                        {                            
                            msb3.Append(", ");
                        }
                        msb3.Append($"ard{ardpv}_{i}");

                        if (i == iType.GetArrayRank() - 1)
                        {//last element
                            msb2.Append($"\nfor(int ard{ardpv}_{i} = 0; ard{ardpv}_{i} < {varName}.GetLength({i}); ard{ardpv}_{i}++) {{");
                                                        
                            msb2.Append($"\n{varName}[{msb3.ToString()}] = arenm{arenm}.Current;");
                            msb2.Append($"\narenm{arenm}.MoveNext();");
                            msb2.Append($"\n}}");
                        }
                        else
                            msb2.Append($"\nfor(int ard{ardpv}_{i} = 0; ard{ardpv}_{i} < {varName}.GetLength({i}); ard{ardpv}_{i}++)");
                    }

                    sbJsonDecode.Append(msb2.ToString());
                }
                else
                {

                    //Reading List to prepare transformation
                    Dictionary<int, string> varmap = new Dictionary<int, string>();
                    for (int i = 0; i < iType.GetArrayRank(); i++)
                    {
                        //  var listType = typeof(List<>).MakeGenericType(iType.GetElementType());

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
                    int ka = varCntTotal;
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
                    msb.Append($"\n{StandardTypes.GetFriendlyName(gta)} pvar{ka} = {defaultValue};");
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
