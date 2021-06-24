using System;
using System.Collections.Generic;
using System.IO;
using TabTabCompiler.Compilation;

namespace TabTabCompiler
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Help();
            if (args.Length == 0)
            {
                return;
            }

            var ars = IndexOf(args, "-s");
            if (ars != -1)
            {
                if (ars + 1 >= args.Length)
                {
                    Console.WriteLine("Error");
                }
                else
                {
                    if (File.Exists(args[ars+1]))
                    {
                        Compiler.Compile(File.OpenText(args[ars + 1]));
                        foreach (var output in Compiler.Outputs)
                        {
                            Console.WriteLine($"{output.Line}:{output.Position} - {output.Type}:{output.Text}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {args[ars + 1]} was not found.");
                    }
                    
                }
            }

            Console.ReadKey();
        }

        private static void Help()
        {
            Console.WriteLine("TabTabScript Compiler v1.0.0.");
            Console.WriteLine("Arguments:");
        }

        private static int IndexOf(IReadOnlyList<string> args, string text)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (args[i] == text)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
