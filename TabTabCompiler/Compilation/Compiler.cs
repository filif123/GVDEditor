using System.Collections.Generic;
using System.IO;
using GVDEditor.Entities;
using TabTabCompiler.Components;

namespace TabTabCompiler.Compilation
{
    public static class Compiler
    {
        public static List<CodeOutput> Outputs = new();
        public static List<TrainType> TrainTypes = new();

        public static void Compile(string code)
        {
            Outputs.Clear();
            //1. faza ziskanie tokenov
            var sc = new Scanner(code);
            var tokens = sc.Result;
            
            
        }

        public static void Compile(StreamReader stream)
        {
            string code = stream.ReadToEnd();
            Compile(code);
        }
    }
}
