using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Classes;

namespace CountLines_VS2010
{
    class Program
    {
        private static List<Markers> _tokens = new List<Markers>()
            {
                new Markers() {Start = "\"", End = "\"",State = state.Text},
                new Markers() {Start = "//", End = "\r\n"},
                new Markers() {Start = "/*", End = "*/"}
            };

        static void Main(string[] args)
        {
            foreach (var file in args)
            {
                if (File.Exists(file))
                {
                    string input = File.ReadAllText(file);
                    List<string> cleanString = Classes.Parser.GetCleanedString(input, _tokens);
                    foreach (var crap in cleanString)
                        Console.WriteLine(crap);
                    Console.WriteLine(cleanString.Count);
                }
            }
            Console.ReadLine();

        }
    }

   

}
