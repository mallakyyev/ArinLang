using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Models
{
    public class TestStat
    {
        public int Count { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
    public class UserStats
    {
        public string Email { get; set; }
        public List<int> Words_Y { get; set; } = new List<int>();
        public List<string> Words_X { get; set; } = new List<string>();

        public List<int> Phrases_Y { get; set; } = new List<int>();
        public List<string> Phrases_X { get; set; } = new List<string>();

        public List<int> Names_Y { get; set; } = new List<int>();
        public List<string> Names_X { get; set; } = new List<string>();

        public List<int> WordSent_Y { get; set; } = new List<int>();
        public List<string> WordSent_X { get; set; } = new List<string>();

    }
}
