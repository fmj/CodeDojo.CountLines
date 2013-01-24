using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Classes
{
    public class Parser
    {
        public static List<string> GetCleanedString(string input, List<Markers> _tokens)
        {
            return GetCleanedCode(input,_tokens).ToString().Split(System.Environment.NewLine.ToArray()).Where(l => l.Trim().Length != 0).ToList();
        }

        public static int GetLineCount(string input,List<Markers> _tokens )
        {
            return GetCleanedString(input,_tokens).Count;
        }

        static StringBuilder GetCleanedCode(string input, List<Markers> _tokens)
        {
            StringBuilder output = new StringBuilder();
            while (input.Length > 0)
            {
                input = input.Trim();
                foreach(Markers m in _tokens)
                    m.Match(input);
                Markers curr = null;
                if (_tokens.Where(m => m.State == state.InMarker).Count() > 0)
                    curr = _tokens.Where(m => m.State == state.InMarker || m.State == state.Text).OrderBy(m => m.StartMatch).First();
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
            }
            return output;
        }
    }
}
