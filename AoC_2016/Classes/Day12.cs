using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day12
    {
        // As always, a place to store inputs
        string[] input;

        public Day12()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day12.txt");

        }
        public void Solve()
        {
            // The Four integers.
            Dictionary<string, int> registers = new Dictionary<string, int>();
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("c", 1);
            registers.Add("d", 0);


            for (int i = 0; i < input.Length; i++)
            {
                // Separate the instruction parts
                string[] line = input[i].Split(' ');

                switch (line[0])
                {
                    case "cpy":
                        if (registers.ContainsKey(line[1]))
                        {
                            registers[line[2]] = registers[line[1]]; 
                        }
                        else
                        {
                            int newInt;
                            int.TryParse(line[1], out newInt);
                            registers[line[2]] = newInt;
                        }
                        break;
                    case "inc":
                        registers[line[1]]++;
                        break;
                    case "dec":
                        registers[line[1]]--;
                        break;
                    case "jnz":
                        int JumpEval;
                        if (registers.ContainsKey(line[1]))
                        {
                            JumpEval = registers[line[1]];
                        }
                        else
                        {
                            int.TryParse(line[1], out JumpEval);
                        }

                        if (JumpEval != 0)
                        {
                            int jumpDist;
                            int.TryParse(line[2], out jumpDist);
                            jumpDist = jumpDist - 1;
                            i = i + jumpDist;
                        }

                        break;


                    default:
                        break;
                }
            }
            Console.WriteLine("A, B, C, D :{0}, {1}, {2}, {3}",registers["a"], registers["b"], registers["c"], registers["d"]);

        }

    }
}
