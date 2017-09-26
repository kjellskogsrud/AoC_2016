using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day11
    {

        public void Solve()
        {

        }
    }


    // Some experminetal methods to convert int to an array of booleans.
    // I probably won't use any of it. 
    public static class Int32Extenssions
    {
        public static Boolean[] ToBooleanArray(this Int32 i)
        {
            return Convert.ToString(i, 2).Select(s => s.Equals('1')).ToArray();
        }

        public static Boolean[] ToBooleanArray(this Int32 i, int lenght)
        {
            Boolean[] returnArray = new Boolean[lenght];
            for (int j = 0; j < returnArray.Length; j++)
            {
                returnArray[j] = false;
            }
            Boolean[] convertedInt = Convert.ToString(i, 2).Select(s => s.Equals('1')).ToArray();
            if (returnArray.Length == convertedInt.Length)
            {
                return convertedInt;
            }
            else if (returnArray.Length > convertedInt.Length)
            {
                
                for(int j = convertedInt.Length-1; j >= 0; j--)
                {
                    returnArray[returnArray.Length - (convertedInt.Length) + j] = convertedInt[j];
                }   
                            
                return returnArray;
            }
            else if (returnArray.Length < convertedInt.Length)
            {
                convertedInt.Reverse();
                for (int j = 0; j < returnArray.Length; j++)
                {
                    returnArray[j] = convertedInt[j];
                }
                returnArray.Reverse();
            }
            return returnArray;
        }
    }

//The first floor contains a thulium generator, a thulium-compatible microchip, a plutonium generator, and a strontium generator.
//The second floor contains a plutonium-compatible microchip and a strontium-compatible microchip.
//The third floor contains a promethium generator, a promethium-compatible microchip, a ruthenium generator, and a ruthenium-compatible microchip.
//The fourth floor contains nothing relevant.
}
