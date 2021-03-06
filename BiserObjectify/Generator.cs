﻿using BiserObjectify.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserObjectify
{
    public static class Generator
    {

        /// <summary>
        /// When generated calsses must be used under DBreeze, set parameter generateForDBreeze to true or use project "Conditional Compilational Symbol" equal to "BiserForDBreeze"
        /// </summary>
        /// <param name="incomingType"></param>
        /// <param name="generateIncludedTypes"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="forBiserBinary"></param>
        /// <param name="forBiserJson"></param>
        /// <param name="exclusions">excluded properties Names</param>
        /// <param name="generateForDBreeze">Default is false. Adds some extra namespaces if classes must be used by Biser integrated into DBreeze</param>
        /// <returns></returns>
        public static Dictionary<string, string> Run(Type incomingType, bool generateIncludedTypes, string destinationFolder, bool forBiserBinary=true, bool forBiserJson = true, HashSet<string> exclusions = null, bool generateForDBreeze=false)
        {
            Dictionary<string, string> retT = new Dictionary<string, string>();
            StandardTypes.InitDict();
            JsonGenerator jg = new JsonGenerator();
            BinaryGenerator bg = new BinaryGenerator();

            HashSet<Type> typesToProcess = new HashSet<Type>();
            HashSet<Type> typesProcessed = new HashSet<Type>();

            typesToProcess.Add(incomingType);
            Type toProcess = null;

            if (exclusions == null)
                exclusions = new HashSet<string>();

            if (!String.IsNullOrEmpty(destinationFolder) && !System.IO.Directory.Exists(destinationFolder))
                System.IO.Directory.CreateDirectory(destinationFolder);

            while (true)
            {                
                toProcess = typesToProcess.Where(r => !typesProcessed.Contains(r)).FirstOrDefault();
                if (toProcess == null)
                    break;


                string tmplIfcJson = "";
                string tmplIfcBinary = "";

                string contentJson = "";
                string contentBinary = "";

                if (forBiserJson)
                {

                    tmplIfcJson = "Biser.IJsonEncoder";
                    contentJson = jg.Run(toProcess, exclusions);
                    foreach (var el in jg.UsedObjects)
                        typesToProcess.Add(el);
                }

                if (forBiserBinary)
                {
                    tmplIfcBinary = " Biser.IEncoder";
                    contentBinary = bg.Run(toProcess, exclusions);                    
                    foreach (var el in bg.UsedObjects)
                        typesToProcess.Add(el);
                }

                string tmplIfcComma1 = (forBiserJson && forBiserBinary) ? "," : "";


                var nsLen = toProcess.FullName.Length - toProcess.Name.Length - 1;


                var replaceDictionary = new Dictionary<string, string> {
                    { "{@NamespaceName}", toProcess.FullName.Substring(0, nsLen) },
                    { "{@ObjName}", toProcess.Name},                   
                    { "{@IfcJson}", tmplIfcJson},
                    { "{@IfcBinary}", tmplIfcBinary},
                    { "{@IfcComma1}", tmplIfcComma1 },
                    { "{@ContentJson}", contentJson },
                    { "{@ContentBinary}", contentBinary}
                    };


                if (generateForDBreeze)
                {
                    replaceDictionary["{@BiserForDBreeze}"] = "using DBreeze.Utils;";
                }
                else
                    replaceDictionary["{@BiserForDBreeze}"] = Resource1.tmplBiserForDBreeze;

              
                var ret = Resource1.tmplBiserContainer.ReplaceMultiple(replaceDictionary);

                if (!String.IsNullOrEmpty(destinationFolder))
                {
                    //System.IO.File.WriteAllText(System.IO.Path.Combine(destinationFolder, toProcess.FullName + "_Biser.cs"), ret);
                    System.IO.File.WriteAllText(System.IO.Path.Combine(destinationFolder, toProcess.Name + "_Biser.cs"), ret);
                    //System.IO.File.WriteAllText(@"D:\Temp\1\TS6_Biser.cs", ret);
                }


                //retT.Add(incomingType.FullName + "_Biser.cs", ret);
                retT.Add(toProcess.FullName + "_Biser", ret);


                typesProcessed.Add(toProcess);

                if (!generateIncludedTypes)
                    break;

            }//eo while           

            return retT;
        }//eof



    }//eoc
}
