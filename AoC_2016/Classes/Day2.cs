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
        char[,] KeyMap = new char[5,5];

        // Then we rmember where we are
        int x;
        int y;


        public Day2()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day2.txt");
            //input = new string[4] {"ULL","RRDDD","LURDL","UUUUD" };

            // Write the keymap
            // #  #  1  #  #    00  10  20  30  40
            // #  2  3  4  #    01  11  21  31  41
            // 5  6  7  8  9    02  12  22  32  42  
            // #  A  B  C  #    03  13  23  33  43
            // #  #  D  #  #    04  14  24  34  44
            KeyMap[0, 0] = '#'; KeyMap[1, 0] = '#'; KeyMap[2, 0] = '1'; KeyMap[3, 0] = '#'; KeyMap[4, 0] = '#';
            KeyMap[0, 1] = '#'; KeyMap[1, 1] = '2'; KeyMap[2, 1] = '3'; KeyMap[3, 1] = '4'; KeyMap[4, 1] = '#';
            KeyMap[0, 2] = '5'; KeyMap[1, 2] = '6'; KeyMap[2, 2] = '7'; KeyMap[3, 2] = '8'; KeyMap[4, 2] = '9';
            KeyMap[0, 3] = '#'; KeyMap[1, 3] = 'A'; KeyMap[2, 3] = 'B'; KeyMap[3, 3] = 'C'; KeyMap[4, 3] = '#';
            KeyMap[0, 4] = '#'; KeyMap[1, 4] = '#'; KeyMap[2, 4] = 'D'; KeyMap[3, 4] = '#'; KeyMap[4, 4] = '#';

        }

        public void Solve()
        {
            Console.Write("The pincode for the door is: ");
            // Start at 5
            x = 0;
            y = 2;

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
            // Clamp Y (but now it's mostly to avoid any out of bounds errors in the array)
            if (this.y < 0)
                this.y = 0;
            else if (this.y > 4)
                this.y = 4;

            // Clamp X (but now it's mostly to avoid any out of bounds errors in the array)
            if (this.x < 0)
                this.x = 0;
            else if (this.x > 4)
                this.x = 4;

            // Based on what we did we check the actual keymap
            switch (direction)
            {
                case 'U':
                    if(KeyMap[this.x,this.y] == '#')
                        this.y++;
                    break;
                case 'D':
                    if (KeyMap[this.x, this.y] == '#')
                        this.y--;
                    break;
                case 'L':
                    if (KeyMap[this.x, this.y] == '#')
                        this.x++;
                    break;
                case 'R':
                    if (KeyMap[this.x, this.y] == '#')
                        this.x--;
                    break;
                default:
                    break;
            }

        }
 
    }
}
