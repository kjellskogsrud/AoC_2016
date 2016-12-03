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
            // To solve for vertical groups we want to change the input to a more workable format
            int[,] inputGrid = new int[input.Length, 3];

            // Then we look at each line in our first array and use it to build up the new one.
            for (int i = 0; i < input.Length; i++)
            {
                int[] lineValues = LineToIntArray(input[i]);
                inputGrid[i, 0] = lineValues[0];
                inputGrid[i, 1] = lineValues[1];
                inputGrid[i, 2] = lineValues[2];
            }

            // With new grid in hand we can work with the same bruteforce method as in part 1 with some modifications
            // Bruteforce metode
            // We start with 0 found triangles:
            int foundTriangles = 0;
            // we still count using Input as length but we increment by 3 each time.
            for (int i = 0; i < input.Length; i = i+3) 
            {
                // First Column
                bool isTriangle = true;
                isTriangle = (inputGrid[i+1,0] + inputGrid[i+2,0] > inputGrid[i,0]) ? true : false;
                isTriangle = (inputGrid[i,0] + inputGrid[i+2,0] > inputGrid[i+1,0] && isTriangle) ? true : false;
                isTriangle = (inputGrid[i,0] + inputGrid[i+1,0] > inputGrid[i+2,0] && isTriangle) ? true : false;
                foundTriangles = (isTriangle) ? foundTriangles+1 : foundTriangles;

                // Second Column
                isTriangle = true;
                isTriangle = (inputGrid[i + 1, 1] + inputGrid[i + 2, 1] > inputGrid[i, 1]) ? true : false;
                isTriangle = (inputGrid[i, 1] + inputGrid[i + 2, 1] > inputGrid[i + 1, 1] && isTriangle) ? true : false;
                isTriangle = (inputGrid[i, 1] + inputGrid[i + 1, 1] > inputGrid[i + 2, 1] && isTriangle) ? true : false;
                foundTriangles = (isTriangle) ? foundTriangles + 1 : foundTriangles;

                // Third Colum
                isTriangle = true;
                isTriangle = (inputGrid[i + 1, 2] + inputGrid[i + 2, 2] > inputGrid[i, 2]) ? true : false;
                isTriangle = (inputGrid[i, 2] + inputGrid[i + 2, 2] > inputGrid[i + 1, 2] && isTriangle) ? true : false;
                isTriangle = (inputGrid[i, 2] + inputGrid[i + 1, 2] > inputGrid[i + 2, 2] && isTriangle) ? true : false;
                foundTriangles = (isTriangle) ? foundTriangles + 1 : foundTriangles;

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
