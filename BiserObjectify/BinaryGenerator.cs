﻿using BiserObjectify.Properties;
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

        int varCntTotal = 0;

        public string Run(Type incomingType, HashSet<string> exclusions)
        {
            var tf = incomingType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            myType = incomingType;

            StringBuilder sb = new StringBuilder();

            StringBuilder sbEncode = new StringBuilder();
            StringBuilder sbDecode = new StringBuilder();
            Type iType = null;

            UsedVars.Clear();
            UsedObjects.Clear();

            //Binary Encoder
            //List<string> endings = new List<string>();

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

                EncodeSingle(iType, sbEncode, name);
            }
            

            //Binary Decoder          
            varCntTotal = 0;
            UsedVars.Clear();

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

                UsedVars.Add($"m.{name}");
                DecodeSingle(iType, sbDecode, $"m.{name}");               
               
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

            return res;
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="iType"></param>
       /// <param name="sbEncode"></param>
       /// <param name="varName"></param>
       /// <param name="varCnt"></param>
        void EncodeSingle(Type iType, StringBuilder sbEncode, string varName)
        {           
            if (iType.IsArray)
            {
                if (iType == typeof(byte[]))
                {
                    sbEncode.Append($"\r\nencoder.Add({varName});");
                }
                else
                {
                    varCntTotal++;
                    int lv = varCntTotal;
                    sbEncode.Append($"\r\nif({varName} == null) \r\nencoder.Add((byte)1);\r\nelse {{ \r\nencoder.Add((byte)0);");

                    sbEncode.Append($"\r\nfor(int it{lv}=0; it{lv} < {varName}.Rank; it{lv}++)");
                    sbEncode.Append($"\r\nencoder.Add({varName}.GetLength(it{lv}));");
                    varCntTotal++;
                    sbEncode.Append($"\r\nforeach(var el{varCntTotal} in {varName})");
                    EncodeSingle(iType.GetElementType(), sbEncode, $"el{varCntTotal}");

                    sbEncode.Append($"\r\n}}"); //eo if
                }
            }
            else if (iType.GetInterface("ICollection`1") != null)
            {              
                if (iType.GetInterface("ISet`1") != null || iType.GetInterface("IList`1") != null)
                {
                    varCntTotal++;                  
                    sbEncode.Append($"\r\nencoder.Add({varName}, (r{varCntTotal}) => {{");
                    EncodeSingle(iType.GenericTypeArguments[0], sbEncode, "r" + varCntTotal);
                    sbEncode.Append($"}});");
                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {
                    varCntTotal++;                   
                    sbEncode.Append($"\r\nencoder.Add({varName}, (r{varCntTotal}) => {{");
                    EncodeSingle(iType.GenericTypeArguments[0], sbEncode, "r" + varCntTotal + ".Key");                 
                    EncodeSingle(iType.GenericTypeArguments[1], sbEncode, "r" + varCntTotal + ".Value");
                    sbEncode.Append($"}});");
                }
                
            }
            else if (iType.GetInterface("ITuple") != null)
            {                
                int tn = 1;
                foreach (var gta in iType.GetGenericArguments())
                {             
                    EncodeSingle(gta, sbEncode,$"{varName}.Item{tn}");                 
                    tn++;
                }
            }
            else if (iType.IsEnum)
            {
                //var nullableEnum = Nullable.GetUnderlyingType(iType);
                Type underlyingType = Enum.GetUnderlyingType(iType);

                sbEncode.Append($"\r\nencoder.Add(({underlyingType.ToString()}){varName});");
            }
            else
            {
                sbEncode.Append($"\r\nencoder.Add({varName});");                
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="sbDecode"></param>
        /// <param name="varName"></param>
        /// <param name="varCnt"></param>
        int DecodeSingle(Type iType, StringBuilder sbDecode, string varName)
        {
            
            if (iType == typeof(byte[]))
            {
                
                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbDecode.Append("\r\nvar ");
                }
                else
                    sbDecode.Append("\r\n");
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
             
                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbDecode.Append("\r\nvar ");
                }
                else
                    sbDecode.Append("\r\n");               
               
                for (int i = 0; i < iType.GetArrayRank(); i++)
                {
                    if (i > 0)
                    {
                        msb1.Append(", ");
                        msb3.Append(", ");
                    }
                                        
                    varCntTotal++;
                    int ardpv = varCntTotal;

                    msb1.Append("decoder.GetInt()");
                    msb3.Append($"ard{ardpv}_{i}");
                    //---
                    if (i == iType.GetArrayRank() - 1)
                    {//last element
                        msb2.Append($"\r\nfor(int ard{ardpv}_{i} = 0; ard{ardpv}_{i} < {varName}.GetLength({i}); ard{ardpv}_{i}++) {{");                        
                        varCntTotal++;
                        UsedVars.Add($"{varName}[{msb3.ToString()}]");
                        DecodeSingle(iType.GetElementType(), msb2, $"{varName}[{msb3.ToString()}]");
                        msb2.Append($"\r\n}}");
                    }
                    else
                        msb2.Append($"\r\nfor(int ard{ardpv}_{i} = 0; ard{ardpv}_{i} < {varName}.GetLength({i}); ard{ardpv}_{i}++)");
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
                revArrStr = revArrStr.Substring(revArrStr.IndexOf("]")+1);

                if (iof > 0)                    
                    strf = $"{strf.Substring(0, strf.Length - iof)}[{msb1.ToString()}]{revArrStr.ToString()}";                
                else
                    strf = $"{strf}[{msb1.ToString()}]";
                
                sbDecode.Append($"{varName} = decoder.CheckNull() ? null : new {strf};");
                sbDecode.Append($"\r\nif({varName} != null){{");
                sbDecode.Append($"{msb2.ToString()}");
                sbDecode.Append($"\r\n}}");
            }
            else if (iType.GetInterface("ICollection`1") != null)
            {
                StringBuilder msb = new StringBuilder();

                if (iType.GetInterface("ISet`1") != null || iType.GetInterface("IList`1") != null)
                {
                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        msb.Append("\r\nvar ");
                    }
                    else
                        msb.Append("\r\n");
                    
                    msb.Append($"{varName} = decoder.CheckNull() ? null : new {StandardTypes.GetFriendlyName(iType)}();");
                    msb.Append($"\r\nif({varName} != null){{");
                    msb.Append($"\r\n\tdecoder.GetCollection(() => {{");

                    StringBuilder sbi = new StringBuilder();
                    
                    varCntTotal++;
                    int pv2 = varCntTotal;                  

                    DecodeSingle(iType.GenericTypeArguments[0], sbi, $"pvar{pv2}");
                    msb.Append(sbi.ToString());

                    msb.Append($"\r\n\t\treturn pvar{pv2};");
                    msb.Append($"\r\n\t}}, {varName}, true);");

                    msb.Append($"\r\n}}"); 

                    sbDecode.Append(msb.ToString());                   
                }
                else if (iType.GetInterface("IDictionary`2") != null)
                {
                    if (!UsedVars.Contains(varName))
                    {
                        UsedVars.Add(varName);
                        msb.Append("\r\nvar ");
                    }
                    else
                        msb.Append("\r\n");

                    msb.Append($"{varName} = decoder.CheckNull() ? null : new {StandardTypes.GetFriendlyName(iType)}();");
                    msb.Append($"\r\nif({varName} != null){{");

                    msb.Append($"\r\n\tdecoder.GetCollection(() => {{");

                    StringBuilder sbi = new StringBuilder();

                    varCntTotal++;
                    int pv2 = varCntTotal;
                    DecodeSingle(iType.GenericTypeArguments[0], sbi, $"pvar{pv2}");
                    msb.Append(sbi.ToString());
                    msb.Append($"\r\n\t\treturn pvar{pv2};"); 
                    msb.Append($"\r\n}},");
                    msb.Append($"\r\n() => {{");

                    sbi.Clear();                    
                    varCntTotal++;
                    int pv3 = varCntTotal;
                    DecodeSingle(iType.GenericTypeArguments[1], sbi, $"pvar{pv3}");

                    msb.Append(sbi.ToString());
                    msb.Append($"\r\n\t\treturn pvar{pv3};");
                    msb.Append($"\r\n\t}}, {varName}, true);");
                    msb.Append($"\r\n}}"); 
                                       
                    sbDecode.Append(msb.ToString());                   
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
                    int kv = varCntTotal;
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
                    
                    msb.Append($"\r\n{StandardTypes.GetFriendlyName(gta)} pvar{kv} = {defaultValue};");                   
                    
                    tuplSbi.Add(sbi.ToString());
                }
                
                int elcnt = 0;
                foreach (var el in tuplSbi)
                {
                    elcnt++;                
                    msb.Append(el);                
                }
                                                               
                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    msb.Append("\r\nvar ");
                }
                else
                    msb.Append("\r\n");

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
            }
            else if (iType.IsEnum)
            {
                //var nullableEnum = Nullable.GetUnderlyingType(iType);
                Type underlyingType = Enum.GetUnderlyingType(iType);

                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbDecode.Append("\r\nvar ");
                }
                else
                    sbDecode.Append("\r\n");

                sbDecode.Append($"{varName} = ({iType.Name})");

                if (StandardTypes.STypes.TryGetValue(underlyingType, out var tf))
                {
                    sbDecode.Append(tf.FGet);
                }
                else
                    throw new Exception("Enum.GetUnderlyingType generation has failed");

                sbDecode.Append(";");
            }
            else
            {
                if (!UsedVars.Contains(varName))
                {
                    UsedVars.Add(varName);
                    sbDecode.Append("\r\nvar ");
                }
                else
                    sbDecode.Append("\r\n");

                sbDecode.Append($"{varName} = ");
              
                if (StandardTypes.STypes.TryGetValue(iType, out var tf))
                {
                    sbDecode.Append(tf.FGet);
                }
                else
                {
                    if (iType == myType)
                        throw new Exception("Cross-Reference exception. Object can't contain itself as a property");

                    UsedObjects.Add(iType);
                    sbDecode.Append(StandardTypes.GetFriendlyName(iType) + ".BiserDecode(null, decoder)");
                }
                sbDecode.Append(";");
            }

            return varCntTotal;
        }

      

       


        ///// <summary>
        ///// 
        ///// </summary>
        //internal class MapperContent
        //{
        //    public List<string> Lst = new List<string>();

        //    public string PrepareContent()
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        foreach (var el in Lst)
        //        {
        //            sb.Append(el);
        //        }

        //        return sb.ToString();
        //    }
        //}
    }//eoc
}
