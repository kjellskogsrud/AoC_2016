using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day9
    {
        // Somewhere to store our bacis input
        public string[] input;

        public Day9()
        {
            // Read input from file into the array.
            input = File.ReadAllLines(@"Inputs\Day9.txt");             
        }

        public void Solve()
        {
            Console.WriteLine("Counted: {0}",Decompress(input[0]));
        }

        private double Decompress(string inputString)
        {
            double charCount = 0;
            bool isMarker = false;
            int subsequentChars = 1;
            int repeat = 1;
            string currentMarker = "";

            // Check each char
            for (int c = 0; c < inputString.Length; c++)
            {
                // Detect start and end of markers
                if (inputString[c] == '(' && currentMarker == "")
                {
                    isMarker = true;
                    continue;
                }
                else if (inputString[c] == ')')
                {
                    isMarker = false;
                    continue;
                }

                // What to do with this char?
                // If we are not in a marker
                if (!isMarker)
                {
                    // So we are currently not in a marker
                    // Have we an unparsed marker?
                    if(currentMarker != "")
                    {
                        subsequentChars = int.Parse(currentMarker.Split('x')[0]);
                        repeat = int.Parse(currentMarker.Split('x')[1]);
                        currentMarker = "";
                    }

                    // Then if subsequent chars is greater than 5
                    if(subsequentChars > 5)
                    {
                        // then we have a substring, that could possible contain another marker
                        // If so we should decompress that
                        string substring = inputString.Substring(c, subsequentChars);
                        charCount = charCount + (Decompress(substring) * repeat);
                        
                        // And then we skip the string we just handed to Decompress.
                        c = c + (subsequentChars-1);

                        // Then we reset the subsequent chars to 1.
                        subsequentChars = 1;
                        repeat = 1;

                        // And move on to the next char
                        continue;
                    }
                    // But if we dont have more than 5 chars, then it can not hold another marker,
                    else
                    {
                        charCount = charCount + (subsequentChars * repeat);
                        c = c + (subsequentChars - 1);
                        subsequentChars = 1;
                        repeat = 1;
                    }
                }
                else
                {
                    currentMarker = currentMarker + inputString[c].ToString();
                }
            }
            return charCount;
        }
    }
}
