using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_D1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input from http://adventofcode.com/2016/day/1/input
            string input = "R3, L5, R1, R2, L5, R2, R3, L2, L5, R5, L4, L3, R5, L1, R3, R4, R1, L3, R3, L2, L5, L2, R4, R5, R5, L4, L3, L3, R4, R4, R5, L5, L3, R2, R2, L3, L4, L5, R1, R3, L3, R2, L3, R5, L194, L2, L5, R2, R1, R1, L1, L5, L4, R4, R2, R2, L4, L1, R2, R53, R3, L5, R72, R2, L5, R3, L4, R187, L4, L5, L2, R1, R3, R5, L4, L4, R2, R5, L5, L4, L3, R5, L2, R1, R1, R4, L1, R2, L3, R5, L4, R2, L3, R1, L4, R4, L1, L2, R3, L1, L1, R4, R3, L4, R2, R5, L2, L3, L3, L1, R3, R5, R2, R3, R1, R2, L1, L4, L5, L2, R4, R5, L2, R4, R4, L3, R2, R1, L4, R3, L3, L4, L3, L1, R3, L2, R2, L4, L4, L5, R3, R5, R3, L2, R5, L2, L1, L5, L1, R2, R4, L5, R2, L4, L5, L4, L5, L2, L5, L4, R5, R3, R2, R2, L3, R3, L2, L5";

            // remove whitespace
            input = input.Replace(" ", "");

            // Make that an array
            string[] directions = input.Split(',');

            // faceing represented like a clock. 12 north, 3 is east, 6 is south and 9 is west. 
            int faceing = 12; // we start facing north 

            // blocks
            int NSblocks = 0;
            int EWblocks = 0;

            // part 2:
            // we will take note of all the places we have been:
            List<Coordinates> places = new List<Coordinates>();
            
            // beginning with where we are now:
            places.Add(new Coordinates() { NS = 0, EW = 0 });
            
            for (int i = 0; i < directions.Length; i++)
            {
                string turn = directions[i].Substring(0, 1); // The first letter
                //Console.Write("Turn is {0}. | ",turn);

                faceing = Turn(turn, faceing); // turn to face that way.
                //Console.Write("Faceing is {0}. | ", faceing);
                int distance= 0;
                int.TryParse(directions[i].Substring(1), out distance);
               // Console.Write("Distance is {0}.\n", distance);

                Move(faceing, distance, ref NSblocks, ref EWblocks);
                //Console.WriteLine("NS: {0} | EW: {1}",NSblocks, EWblocks);
                //Console.ReadLine();

                //Remember these coordinates.
                places.Add(new Coordinates() { NS = NSblocks, EW = EWblocks });
            }

            // when all of that is done we make sure that its all positive
            if (NSblocks < 0)
                NSblocks = NSblocks * -1;

            if (EWblocks < 0)
                EWblocks = EWblocks * -1;

            int totalBlocks = NSblocks + EWblocks;
            
            // Answer to part 1:
            Console.WriteLine("TotalBlocks of all the instructions was: {0}",totalBlocks);

            // Look for the first place to be visited twice
            /


            Console.ReadLine();

        }

        static int Turn(string turn, int faceing)
        {
            int newfaceing = faceing;

            switch (turn)
            {
                case "R":
                    newfaceing = faceing + 3;
                    break;

                case "L":
                    newfaceing = faceing - 3;
                    break;
                default:
                    break;
            }

            // are we past 12?
            if (newfaceing > 12)
                newfaceing = newfaceing - 12;
            // or did we reach 0
            else if (newfaceing == 0)
                newfaceing = 12;
            
            return newfaceing;
        }
        static void Move(int faceing, int distance, ref int NSblocks, ref int EWblocks)
        {
            switch (faceing)
            {
                case 12:
                    // facing north we add to NSblocks
                    NSblocks = NSblocks + distance;
                    break;
                case 3:
                    // facing east we add to EWblocks
                    EWblocks = EWblocks + distance;
                    break;
                case 6:
                    // facing south we subtract from NSblocks
                    NSblocks = NSblocks - distance;
                    break;
                case 9:
                    //facing west we subtract from EW blocks
                    EWblocks = EWblocks - distance;
                    break;
                default:
                    break;
            }
        }
        class Coordinates
        {
            public int NS;
            public int EW;

            public bool iWashere = false;
        }
    }
}
