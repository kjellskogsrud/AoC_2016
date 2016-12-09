using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day8
    {
        // Somewhere to store our bacis input
        public string[] input;
        List<Instruction> instructionList = new List<Instruction>();

        // A representation of the tinyLDC
        TinyLCD tinyLCD = new TinyLCD();
        Thread drawThread;

        public Day8()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day8.txt");
        }
        public void Solve()
        {
            Console.ReadLine();
            drawThread = new Thread(new ThreadStart(tinyLCD.Draw));
            
            for (int i = 0; i < input.Length; i++)
            {
                instructionList.Add(new Instruction(input[i]));
                Instruction c = instructionList.Last();
            }
            drawThread.Start();
            foreach (Instruction i in instructionList)
            {
                if(i.Command == "rect")
                {
                    tinyLCD.Rect(i.X, i.Y);
                }
                else
                {
                    tinyLCD.Rotate(i.SubCommand, i.X + i.Y, i.By);
                }
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
            tinyLCD.StopDraw();
        }

        class Instruction
        {
            public string Command { get; protected set; }
            public string SubCommand { get; protected set; }
            public int X { get; protected set; }
            public int Y { get; protected set; }
            public int By;
            public Instruction(string instructionString)
            {
                if (instructionString.StartsWith("rect"))
                {
                    Command = "rect";
                    string AxB = instructionString.Split(' ')[1];
                    X = int.Parse(AxB.Split('x')[0]);
                    Y = int.Parse(AxB.Split('x')[1]);
                }
                else
                {
                    Command = instructionString.Split(' ')[0];
                    SubCommand = instructionString.Split(' ')[1];
                    string xyIsA = instructionString.Split(' ')[2];
                    if (SubCommand == "row")
                    {
                        
                        Y = int.Parse(xyIsA.Split('=')[1]);
                        X = 0;
                        By = int.Parse(instructionString.Split(' ')[4]);
                    }
                    else
                    {
                        Y = 0;
                        X = int.Parse(xyIsA.Split('=')[1]);
                        By = int.Parse(instructionString.Split(' ')[4]);
                    }

                }
            }
        }

        class TinyLCD
        {
            // The size of the Matrix
            public bool[,] Matrix { get; protected set; } = new bool[50, 6];

            public bool doDraw = false;

            public void Draw()
            {
                int pixlesLit = 0;
                doDraw = true;
                while (doDraw)
                {
                    pixlesLit = 0;
                    for (int y = 0; y < 6; y++)
                    {
                        Console.SetCursorPosition(0,y);
                        for (int x = 0; x < 50; x++)
                        {
                            if (Matrix[x, y])
                            {
                                Console.Write("#");
                                pixlesLit++;
                            }
                            else
                            {
                                Console.Write(".");
                            }
                        }
                    }
                    Console.Write("\nPixles Lit: {0}", pixlesLit);
                }
            }
            public void StopDraw()
            {
                doDraw = false;
            }

            public void Rect(int X, int Y)
            {
                for (int y = 0; y < Y; y++)
                {
                    for (int x = 0; x < X; x++)
                    {
                        Matrix[x, y] = true;
                    }
                }
            }
            public void Rotate(string subCommand, int XY, int By)
            {
                if(subCommand == "row")
                {
                    int x = 0;
                    int y = XY;
                    bool[,] wasMatrix = new bool[50, 6];
                    Array.Copy(Matrix, wasMatrix, 50 * 6);

                    for (int i = x; i < 50; i++)
                    {
                        int OffsetX = (i + By) % 50;
                        Matrix[OffsetX, y] = wasMatrix[i,y];                                          
                    }
                }   
                else
                {
                    int x = XY;
                    int y = 0;
                    bool[,] wasMatrix = new bool[50, 6];
                    Array.Copy(Matrix, wasMatrix, 50 * 6);

                    for (int i = y; i < 6; i++)
                    {
                        int OffsetY = (i + By) %6;
                        Matrix[x, OffsetY] = wasMatrix[x, i];                      
                    }
                }
            }

        }
    }
}
