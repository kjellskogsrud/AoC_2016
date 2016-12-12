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

        public string[] decompressedInput;
        InfoDisplay myDisplay = new InfoDisplay();
        Thread drawThread;

        public Day9()
        {
            // Read input from file into the array.
            input = File.ReadAllLines(@"Inputs\Day9.txt");
            decompressedInput = File.ReadAllLines(@"Inputs\Day9.txt");
            
        }

        public void Solve()
        {
            drawThread = new Thread(new ThreadStart(myDisplay.Draw));
            drawThread.Start();
            for (int i = 0; i < input.Length; i++)
            {
                myDisplay.i = i;
                bool stillMarkers = true;
                int Pass = 0;
                while (stillMarkers)
                {
                    Pass++;
                    myDisplay.Pass = Pass;
                    int decompressedMarkers = 0;
                    stillMarkers = false;
                    string line = decompressedInput[i];
                    decompressedInput[i] = "";
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
                            stillMarkers = true;
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
                            decompressedMarkers++;
                            myDisplay.MarkersProcessed = decompressedMarkers;
                            currentMarker = "";
                        }

                        // We are now on the first char to be either written directly or added to the repeat.
                        // Start adding the repeate chars.
                        if (subsequentCharacters > 0)
                        {
                            repeatString = line.Substring(c, subsequentCharacters);
                            // Offset the c index to keep going after the subsequent chars
                            c = c + (subsequentCharacters - 1);
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
                            myDisplay.currentLineLength = decompressedInput[i].Length;
                        }
                        repeat = 0;

                    }
                    //Console.WriteLine("Line {0}, has {1} chars with {2} decompressed markers this run.", i, decompressedInput[i].Length,decompressedMarkers);
                    //Console.Write("{0} => {1}",line,decompressedInput[i]);
                    
                }
            }
            myDisplay.StopDraw();
        }
        class InfoDisplay
        {
            public int LastLineLength;
            public int currentLineLength;
            public int MarkersProcessed;
            public int Pass;
            public int i;

            bool doDraw = false;

            public void Draw()
            {
                doDraw = true;
                while (doDraw)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Working on line{0}, pass {1}:\nThe string is {2} chars long,\nwith {3} markers processed.",i,Pass,currentLineLength,MarkersProcessed);
                }
            }
            public void StopDraw()
            {
                doDraw = false;
            }
        }

    }
}
