using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day6
    {
        // Somewhere to store our bacis input
        public string[] input;

        public Day6()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day6.txt");

        }
        public void Solve()
        {
            string[] columns = new string[8] { "", "" , "" , "" , "" , "" , "" , "" };
            for (int i = 0; i < input.Length; i++)
            {
                for (int c = 0; c < columns.Length; c++)
                {
                    columns[c] = columns[c] + input[i][c];
                }
            }

            // Part 1 solution:
            //for (int i = 0; i < columns.Length; i++)
            //{
            //    columns[i] = string.Join("",columns[i].GroupBy(c => c).OrderByDescending(c => c.Count()).Take(26).Select(c => c.Key).ToList());
            //    for (int c = 0; c < columns[i].Length; c++)
            //    {
            //        if (columns[i][c] == 1)
            //        {
            //            Console.Write(columns[i][c]);
            //        }
            //    }
            //}
            Console.Write("\n");
            Console.WriteLine("----------------------");
            // Part 2 solution
            Dictionary<char, int>[] dictColumns = new Dictionary<char, int>[8];
            for (int i = 0; i < dictColumns.Length; i++)
            {
                columns[i] = string.Join("",columns[i].GroupBy(c => c).OrderByDescending(c => c.Count()).Take(26).Select(c => c.Key).ToList());
                for (int c = 0; c < columns[i].Length; c++)
                {
                    if (columns[i][c] == 1)
                    {
                        Console.Write(columns[i][c]);
                    }
                }
            }

            

        }

    }
}
