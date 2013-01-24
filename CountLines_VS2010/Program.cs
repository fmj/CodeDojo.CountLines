using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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
            string input = File.ReadAllText(@"D:\Projects\CodingDOJO\CountLines_VS2010\TestFiles\TextFile1.txt");

            StringBuilder output = new StringBuilder();
            while (input.Length > 0)
            {
                input = input.Trim();
                foreach(Markers m in _tokens)
                    m.Match(input);
                Markers curr = null;
                if (_tokens.Where(m => m.State == state.InMarker).Count() > 0)
                    curr = _tokens.Where(m => m.State == state.InMarker || m.State == state.Text).OrderBy(m => m.StartMatch).First();
                 //Add stuff before match
                //output.Append(input.Substring(0, curr.match));
                if (curr == null)
                {
                    output.Append(input);
                    input = "";
                    break;
                }
                else if (curr.State == state.Text)
                {
                    output.Append(input.Substring(0, curr.endMatch  + curr.End.Length));
                }
                else if (curr.StartMatch != 0)
                {
                    output.Append(input.Substring(0, curr.StartMatch));
                    if (curr.Start == "//") output.Append(System.Environment.NewLine);
                }
                //remove up until end of match
                input = input.Substring(curr.endMatch + curr.End.Length).Trim();
                Debug.WriteLine("Output: " + output.ToString());
                Debug.WriteLine("");
                Debug.WriteLine("Input: " + input);
            }
            foreach (var crap in output.ToString().Split(System.Environment.NewLine.ToArray()).Where(l=> l.Trim().Length != 0))
                Console.WriteLine(crap);
            Console.WriteLine(output.ToString().Split(System.Environment.NewLine.ToArray()).Where(l=> l.Trim().Length != 0).Count());
            Console.WriteLine(output.ToString());
            Console.ReadLine();

        }
    }

    enum state
    {
        InMarker,None,Text
    }

    class Markers
    {
        public string Start { get; set; }
        public string End { get; set; }
        public state State { get; set; }
        public int StartMatch{ get; set; }
        public int endMatch { get; set; }

        public void Match(string input)
        {
            if (input.IndexOf(Start) >= 0)
            {
                this.StartMatch = input.IndexOf(Start);
                this.endMatch = input.IndexOf(End, StartMatch + Start.Length);
                this.State = this.State == state.Text ? state.Text : state.InMarker;
            }
            else
                this.State = state.None;
        }
    }

}
