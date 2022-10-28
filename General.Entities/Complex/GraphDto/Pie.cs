using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities.Complex.GraphDto
{
    public class Pie
    {
        public Pie()
        {
            Series = new List<int>();
            Labels = new List<string>();
        }
        public List<int> Series { get; set; }
        public List<string> Labels { get; set; }
    }
}
