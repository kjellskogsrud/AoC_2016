using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day7
    {
        // Somewhere to store our bacis input
        public string[] input;

        public Day7()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day7.txt");
            //input = new string[4] { "abba[mnop]qrst", "abcd[bddb]xyyx", "aaaa[qwer]tyui", "ioxxoj[asdfgh]zxcvbn" };
        }


    }
}
