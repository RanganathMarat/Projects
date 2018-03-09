#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Philips.PmsMR.Protobuf.ModelReflection;
using System.Reflection;

namespace Philips.PmsMR.Protobuf.ProtoGenerator
{
    /// <summary>
    /// ProtoGenerator class which has methods to parse command line arguments and generates the proto files.
    /// </summary>
    internal class ProtoGenerator
    {
        /// <summary>
        /// Sample command line> ProtoGenerator.exe --out c:\tmp\test --namespace XTC --assembly_path .\Philips.PmsMR.ExternalControl_cs.dll
        /// </summary>
        /// <param name="args"></param>
        public bool ParserArguments(string[] args)
        {
            var parser = new ArgumentParser();
            if (!parser.Execute(args, "Generates .proto-file from a given assembly and namespace within the assembly", defaults.ToDictionary(x => x.OptionName)))
            {
                return false;
            }

            var namespaceTag = parser.OptionValueMap[namespaceKey];
            var assemblyPath = parser.OptionValueMap[assemblyNamePathKey];
            var targetDir = parser.OptionValueMap[outputDirectoryPath];

            if (!String.IsNullOrEmpty(assemblyPath))
            {
                var directories = new[]
                {
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    System.IO.Path.GetDirectoryName(assemblyPath)
                };
                var mapper = ResolveAssembly(directories, assemblyPath, namespaceTag);

                string assemblyDoc = null;
                if (bool.Parse(parser.OptionValueMap[decorateComments]))
                {
                    assemblyDoc = RetrieveAssemblyDocumentation(assemblyPath);
                }

                if (mapper != null)
                {
                    var modeler = mapper.DefaultModeler;
                    var exporter = modeler.Exporter;
                    string fileName = namespaceTag;
                    GenerateProto(targetDir, fileName, exporter.GenerateProtoContents(fileName, mapper.Result, assemblyDoc));
                    return true;
                }
                else
                {
                    Console.Error.WriteLine("Could not resolve assembly to find a mapper");
                }
            }
            else
            {
                Console.Out.WriteLine("No assembly paths given");
            }

            if (!String.IsNullOrEmpty(parser.OptionValueMap[spitOutProtogen]))
            {
                return WriteProtogen(parser.OptionValueMap[spitOutProtogen]);
            }

            Console.Error.WriteLine("No tasks given");
            return false;
        }

        public static void GenerateProto(string targetDir, string fileName, string fileContents)
        {
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            string namespaceType = fileName;
            fileName += ".proto";
            string filePath = Path.Combine(targetDir, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, fileContents);
        }

        public static bool WriteProtogen(string targetDir)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();
            var files = new string[] { "protogen.exe", "common.xslt", "csharp.xslt" };
            foreach (var file in files)
            {
                if (!names.Contains(file, StringComparer.InvariantCulture))
                {
                    Console.Error.Write("Cannot find " + file + " amongst ");
                    foreach (var availableName in names)
                    {
                        Console.Error.Write(availableName + ",");
                    }
                    Console.Error.WriteLine();
                    return false;
                }
                using (var stream = assembly.GetManifestResourceStream(file))
                {
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    var newPath = Path.Combine(targetDir, file);
                    File.WriteAllBytes(newPath, buffer);
                }
            }
            return true;
        }

        internal static string RetrieveAssemblyDocumentation(string assemblyPath)
        {
            var docFile = Path.Combine(Path.GetDirectoryName(assemblyPath), Path.GetFileNameWithoutExtension(assemblyPath) + ".xml");
            if (File.Exists(docFile))
            {
                return File.ReadAllText(docFile);
            }
            return null;
        }

        private const string namespaceKey = "namespace";
        private const string assemblyNamePathKey = "assembly_path";
        private const string outputDirectoryPath = "output_directory_path";
        private const string spitOutProtogen = "protogen_directory_path";
        private const string decorateComments = "decorate_with_comments";
        private static readonly string currentDirectory = Directory.GetCurrentDirectory();

        private static readonly List<ArgumentParser.ArgumentTypeInformation> defaults = new List<ArgumentParser.ArgumentTypeInformation>
        {
            new ArgumentParser.ValuedTypeInformation
                        {
                            DefaultValue = "",
                            OptionName = namespaceKey,
                            OptionalDescription = " Supported tags: e.g., XTC",
                            ValuedValidatorFunc = s => true
                        },
             new ArgumentParser.ValuedTypeInformation
                        {
                            DefaultValue = "",
                            OptionName = assemblyNamePathKey,
                            OptionalDescription = "Path to the assembly in which the namespace is searched",
                            ValuedValidatorFunc = s => File.Exists(s)
                        },
             new ArgumentParser.ValuedTypeInformation
                        {
                            DefaultValue = currentDirectory,
                            OptionName = outputDirectoryPath,
                            OptionalDescription = "Output Directory path where proto files needs to be generated. Default is the current directory",
                            ValuedValidatorFunc = s => true
                        },
            new ArgumentParser.ValuedTypeInformation 
            {
                DefaultValue = "",
                OptionName = spitOutProtogen,
                OptionalDescription = "Directory path where to write protogen.exe binaries for generating C# source files from proto-files",
                ValuedValidatorFunc = s => true
            },
            new ArgumentParser.BooleanFlagTypeInformation 
            {
                BooleanValue = true,
                DefaultValue = "false",
                OptionalDescription = "Flag to indicate that the resulting proto-contents need to be decorated with C# comments",
                OptionName = decorateComments
            }

        };

        private TypeMapper ResolveAssembly(string[] directories, string assemblyPath, string namespaceTag)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) =>
            {
                string[] name = eventArgs.Name.Split();
                string assem = name[0].Substring(0, name[0].Length - 1);
                if (assem.EndsWith("\\"))
                {
                    assem = assem.Substring(0, assem.Length - 1);
                }

                foreach (string dir in directories)
                {
                    string path = Path.Combine(dir, assem);
                    if (File.Exists(path + ".dll"))
                    {
                        return Assembly.LoadFrom(path + ".dll");
                    }
                    if (File.Exists(path + ".exe"))
                    {
                        return Assembly.LoadFrom(path + ".exe");
                    }
                }
                return null;
            };
            var assembly = Assembly.LoadFrom(assemblyPath);

            Type[] typesInAssembly;
            try
            {
                typesInAssembly = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                typesInAssembly = e.Types;
            }

            TypeMapper mapper = null;
            foreach (Type t in typesInAssembly)
            {
                try
                {
                    if (t != null && t.Namespace != null && t.Namespace.StartsWith(namespaceTag))
                    {
                        if (typeof(DataModel.INamespaceTag).IsAssignableFrom(t))
                        {
                            mapper = new TypeMapper(assembly.CreateInstance(t.FullName) as DataModel.INamespaceTag);
                            break;
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    continue;
                }
            }
            return mapper;
        }
    }
}
