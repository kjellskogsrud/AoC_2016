using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day3
    {
        // Somewhere to store our bacis input
        public string[] input;

        public Day3()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day3.txt");
            
        }

        public void Solve()
        {
            // Bruteforce metode
            // We start with 0 found triangles:
            int foundTriangles = 0;
            for (int i = 0; i < input.Length; i++)
            {
                bool isTriangle = true;
                int[] values = LineToIntArray(input[i]);
                isTriangle = (values[1] + values[2] > values[0]) ? true : false;
                isTriangle = (values[0] + values[2] > values[1] && isTriangle) ? true : false;
                isTriangle = (values[1] + values[0] > values[2] && isTriangle) ? true : false;

                foundTriangles = (isTriangle) ? foundTriangles+1 : foundTriangles;
                //if (foundTriangles < 5)
                //{
                //    Console.WriteLine("{0},{1},{2} | {3} | {4} ",values[0], values[1], values[2],isTriangle,foundTriangles);
                //    Console.ReadLine();
                //}

            }
            Console.WriteLine("There are {0} possible triangles",foundTriangles);

        }

        private int[] LineToIntArray(string line)
        {
            int[] returnValue = new int[3];
            returnValue[0] = int.Parse(line.Substring(2, 3).Trim());
            returnValue[1] = int.Parse(line.Substring(7, 3).Trim());
            returnValue[2] = int.Parse(line.Substring(12, 3).Trim());

            return returnValue;
        }
    }
}
/*
  810  679   10
  783  255  616
  545  626  626
   84  910  149
  607  425  901
  556  616  883
  */
