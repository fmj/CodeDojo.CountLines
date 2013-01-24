using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    public enum state
    {
        InMarker, None, Text
    }
    public class Markers
    {
        public string Start { get; set; }
        public string End { get; set; }
        public state State { get; set; }
        public int StartMatch { get; set; }
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
