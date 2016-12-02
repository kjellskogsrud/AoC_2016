using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day2
    {
        // Somewhere to store our bacis input
        public string[] input;

        // We can handle this a a grid, and map the buttons to the coordinates.
        int[,] KeyMap = new int[3,3];

        // Then we rmember where we are
        int x;
        int y;


        public Day2()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day2.txt");
            //input = new string[4] {"ULL","RRDDD","LURDL","UUUUD" };

            // Write the keymap
            // 1  2  3          00  10  20
            // 4  5  6          01  11  21
            // 7  8  9          02  12  22
            KeyMap[0, 0] = 1;
            KeyMap[1, 0] = 2;
            KeyMap[2, 0] = 3;
            KeyMap[0, 1] = 4;
            KeyMap[1, 1] = 5;
            KeyMap[2, 1] = 6;
            KeyMap[0, 2] = 7;
            KeyMap[1, 2] = 8;
            KeyMap[2, 2] = 9;
        }

        public void Solve()
        {
            Console.Write("The pincode for the door is: ");
            // Start at 5
            x = 1;
            y = 1;

            // Take the first line
            for (int i = 0; i < input.Length; i++)
            {
                // Read each char
                char[] instructions = input[i].ToCharArray();
                for (int c = 0; c < instructions.Length; c++)
                {
                    // Follow the Instructions
                    Move(instructions[c]);
                }

                // When the line is finished. Read the Code:
                Console.Write(" {0} ",KeyMap[x,y]);
            }
        }

        private void Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    this.y--;
                    break;
                case 'D':
                    this.y++;
                    break;
                case 'L':
                    this.x--;
                    break;
                case 'R':
                    this.x++;
                    break;
                default:
                    break;
            }
            // Clamp Y
            if (this.y < 0)
                this.y = 0;
            else if (this.y > 2)
                this.y = 2;

            // Clamp X
            if (this.x < 0)
                this.x = 0;
            else if (this.x > 2)
                this.x = 2;
        }
 
    }
}
