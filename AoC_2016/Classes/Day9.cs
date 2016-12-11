using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day9
    {
        // Somewhere to store our bacis input
        public string[] input;

        public string[] decompressedInput;

        public Day9()
        {
            // Read input from file into the array.
            input = File.ReadAllLines(@"Inputs\Day9.txt");
            decompressedInput = new string[input.Length];
        }

        public void Solve()
        {
            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                bool isMarker = false;
                string currentMarker = "";
                int subsequentCharacters = 0;
                int repeat = 0;
                string repeatString = "";
                for (int c = 0; c < line.Length; c++)
                {
                    // We go one char at a time
                    // Is this the start of a marker?
                    if (currentMarker == "" && line[c] == '(')
                    {
                        isMarker = true;
                        continue;
                    }
                    // or is this a stopmarker?
                    else if (isMarker && line[c] == ')')
                    {
                        isMarker = false;
                        continue;
                    }

                    // Are we in a marker?
                    if (isMarker)
                    {
                        currentMarker = currentMarker + line[c];
                        continue;
                    }
                    // If we are not in a marker, are we currently processing a marker?
                    else if (currentMarker != "")
                    {
                        // Read the current marker:
                        subsequentCharacters = int.Parse(currentMarker.Split('x')[0]);
                        repeat = int.Parse(currentMarker.Split('x')[1]);
                        currentMarker = "";
                    }

                    // We are now on the first char to be either written directly or added to the repeat.
                    // Start adding the repeate chars.
                    if (subsequentCharacters > 0)
                    {
                        repeatString = line.Substring(c, subsequentCharacters);
                        // Offset the c index to keep going after the subsequent chars
                        c = c + (subsequentCharacters-1);
                        subsequentCharacters = 0;
                    }
                    else
                    {
                        repeat = 1;
                        repeatString = line[c].ToString();
                    }


                    // repeat all the chars in the repeat string for repeat numer of times.
                    for (int r = 0; r < repeat; r++)
                    {
                        decompressedInput[i] = decompressedInput[i] + repeatString;
                    }
                    repeat = 0;

                }
                Console.WriteLine("Line {0}, has {1} chars.", i,decompressedInput[i].Length);
                //Console.Write("{0} => {1}",line,decompressedInput[i]);
                Console.ReadLine();
            }

        }
    }


}
