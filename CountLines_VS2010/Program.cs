using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CountLines_VS2010
{
    class Program
    {
        private static List<Markers> _tokens = new List<Markers>()
            {
                new Markers() {Start = "//", End = "\r\n"},
                new Markers() {Start = "/*", End = "*/"},
                new Markers() {Start = "\"", End = "\""}
            };

        static void Main(string[] args)
        {
            string input = File.ReadAllText(@"D:\Projects\CodingDOJO\CountLines_VS2010\TestFiles\TextFile1.txt");

            StringBuilder output = new StringBuilder();
            while (input.Length > 0)
            {
                foreach(Markers m in _tokens)
                    m.Match(input);
                _tokens.f

            }

        }
    }

    enum state
    {
        InMarker,Text
    }

    class Markers
    {
        public string Start { get; set; }
        public string End { get; set; }
        public state State { get; set; }
        public int match{ get; set; }

        public void Match(string input)
        {
            this.match =  input.IndexOf(Start);
        }
    }

}
