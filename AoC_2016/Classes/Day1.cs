using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    public class Day1
    {
        // Input from http://adventofcode.com/2016/day/1/input
        string input = "R3, L5, R1, R2, L5, R2, R3, L2, L5, R5, L4, L3, R5, L1, R3, R4, R1, L3, R3, L2, L5, L2, R4, R5, R5, L4, L3, L3, R4, R4, R5, L5, L3, R2, R2, L3, L4, L5, R1, R3, L3, R2, L3, R5, L194, L2, L5, R2, R1, R1, L1, L5, L4, R4, R2, R2, L4, L1, R2, R53, R3, L5, R72, R2, L5, R3, L4, R187, L4, L5, L2, R1, R3, R5, L4, L4, R2, R5, L5, L4, L3, R5, L2, R1, R1, R4, L1, R2, L3, R5, L4, R2, L3, R1, L4, R4, L1, L2, R3, L1, L1, R4, R3, L4, R2, R5, L2, L3, L3, L1, R3, R5, R2, R3, R1, R2, L1, L4, L5, L2, R4, R5, L2, R4, R4, L3, R2, R1, L4, R3, L3, L4, L3, L1, R3, L2, R2, L4, L4, L5, R3, R5, R3, L2, R5, L2, L1, L5, L1, R2, R4, L5, R2, L4, L5, L4, L5, L2, L5, L4, R5, R3, R2, R2, L3, R3, L2, L5";

        // Make a list of moves to use for solving
        List<Instruction> Directions = new List<Instruction>();

        // We need to know where we are
        int x;
        int y;

        // And also where we are facing
        Faceing face = Faceing.NORTH;
        enum Faceing { NORTH, SOUTH, EAST, WEST };

        public Day1()
        {
            // Split the input string to an array
            string[] inputArray = input.Split(',');

            // Read the array to the input list
            for (int i = 0; i < inputArray.Length; i++)
            {
                // Clean whitespace
                inputArray[i] = inputArray[i].Trim();

                // Add to the list of moves
                Directions.Add(new Instruction() { Rotation = inputArray[i].Substring(0, 1).ToCharArray()[0], Distance = int.Parse(inputArray[i].Substring(1)) });
            }
        }

        public void Solve()
        {
            foreach (Instruction i in Directions)
            {
                this.face = Face(this.face, i.Rotation);
                Move(i.Distance, this.face);
            }
            Console.WriteLine("The end is {0} blocks away from here.", Math.Abs(x) + Math.Abs(y));
        }


        private Faceing Face(Faceing face, char turn)
        {
            if (turn == 'R')
            {
                switch (face)
                {
                    case Faceing.NORTH:
                        return Faceing.EAST;
                    case Faceing.SOUTH:
                        return Faceing.WEST;
                    case Faceing.EAST:
                        return Faceing.SOUTH;
                    case Faceing.WEST:
                        return Faceing.NORTH;
                    default:
                        return face;
                }
            }
            else
            {
                switch (face)
                {
                    case Faceing.NORTH:
                        return Faceing.WEST;
                    case Faceing.SOUTH:
                        return Faceing.EAST;
                    case Faceing.EAST:
                        return Faceing.NORTH;
                    case Faceing.WEST:
                        return Faceing.SOUTH;
                    default:
                        return face;
                }
            }
        }
        private void Move(int blocks, Faceing face)
        {
            switch (face)
            {
                case Faceing.NORTH:
                    this.x = this.x + blocks;
                    break;
                case Faceing.SOUTH:
                    this.x = this.x - blocks;
                    break;
                case Faceing.EAST:
                    this.y = this.y + blocks;
                    break;
                case Faceing.WEST:
                    this.y = this.y - blocks;
                    break;
                default:
                    break;
            }
        }
    
        class Instruction
        {
            public char Rotation;
            public int Distance;
        }
        class Position
        {
            public int X;
            public int Y;
        }
    }
}
