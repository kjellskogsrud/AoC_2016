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
            // For part 2 we need a compleat list of all the point we pass through.
            // not just the directions at each intersection.
            // To do this we make a list of positions.
            List<Position> placesWeHaveBeen = new List<Position>();

            // Since the answer might be where we started, we will add the start.
            placesWeHaveBeen.Add(new Position(0, 0));

            // and to let us know when to stop looking, we set a bool

            bool duplicatePlaceFound = false;
            // We need to move differantly than in part 1
            foreach (Instruction i in Directions)
            {
                /* Part 1 code:
                //this.face = Face(this.face, i.Rotation);
                //Move(i.Distance, this.face);
                */

                // First we start by updating our faceing:
                this.face = Face(this.face, i.Rotation);

                // Then instead of moving all the blocks at once we move one by one.
                for (int c = 0; c < i.Distance; c++)
                {
                    Move(1, this.face);

                    // Have we been here before?
                    // We can check this by compareing our coordinates with all the other coordinates we have visited.
                    foreach (Position p in placesWeHaveBeen)
                    {
                        if (this.x == p.X && this.y == p.Y)
                        {
                            // we have been here before!
                            duplicatePlaceFound = true;
                            break;
                        }
                    }
                    // Add this place to the list of places we have been
                    placesWeHaveBeen.Add(new Position(this.x, this.y));
                    if (duplicatePlaceFound)
                        break;
                }
                if (duplicatePlaceFound)
                    break;            
            }
            Console.WriteLine("The end is {0} blocks away from here.", Math.Abs(this.x) + Math.Abs(this.y));
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
            public Position(int x,int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
    }
}
