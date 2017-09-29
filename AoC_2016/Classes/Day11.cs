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

    // We want some sort of state controller. A way to keep track off the total state of all floors
    // and where the elevator is. 
    public class StateManager
    {
        // The statemanager class will give a referance to itself.
        // This is because there is not suppose to be more than one state controller, but it is useful to be able to access 
        // Members and Methods from anywhere.
        public static StateManager current { get; protected set; }

        public StateManager()
        {
            // Make a referance to this state manager when it is made.
            // We should probably make some code here to protect it so that if another instance of the
            // class StateManager is made, it is destroyed and returns the existing StateManager instead
            // Sounds like a job for future me...
            current = this;
        }

        // The state manager will need to know about the floors.
        public Dictionary<string,Floor> Floors = new Dictionary<string, Floor>();

        // Since the Statemanager is the "top dog" and knows all, this seems like a good place to 
        // define a list of types for the Generators and Microchips.
        public string[] Elements = new string[] { "thulium", "plutonium", "strontium", "promethium", "ruthenium" };

        // We also need a way to describe the combination of floors uniquely to determine if this is a state we have been in before. 
        // List<State> ListOfKnownStates = new List<State>();
        private void SetInitialState()
        {
           
        }
    }

    // We will also want a class to describe the floors
    public class Floor
    {
        // A floor can hold microchips and controllers.
        // We can represent these as string arrays
        public Dictionary<string,bool> Microchips = new Dictionary<string, bool>();
        public Dictionary<string,bool> Generators = new Dictionary<string, bool>();

        // We should be able to populate the Microchip and Generators dictionaries when we make the floors
        // To do that we must tell each floor about what kind of microchips and generators are here.
        public Floor(string[] microchips, string [] generators)
        {
            // First and empty floor has no microchips or generators
            foreach (string e in StateManager.current.Elements)
            {
                Microchips[e] = false;
                Generators[e] = false;
            }
            // Now to add what we have
            foreach (string m in microchips)
            {
                // NOTE: that this expects the elements to all be there, so any typo in the initial states will result in an exception.
                Microchips[m] = true;
            }
            foreach (string g in generators)
            {
                // NOTE: that this expects the elements to all be there, so any typo in the initial states will result in an exception.
                Generators[g] = true;
            }
        }

        // The Floor should be able to validate itself, regarding the state of the chips.
        // if a chip is present of a floor with generators, and it does not have it's generator here, it breaks.
        public bool Validate()
        {
            // Are there NOT generators here OR are there not Chips here?
            if (!Generators.ContainsValue(true) && !Microchips.ContainsValue(true))
            { 
                return true;
            }
            // Okay so there are generators here.
            else
            {
                // Than for each type of element.  
                foreach(string e in StateManager.current.Elements)
                {
                    // Is there a Chip here without the corresponding Generator?
                    if (Microchips[e] & !Generators[e])
                    {
                        // In so then it is now caput!!
                        return false;
                    }
                        
                }
            }
            // If we get here then there was either; No Generators here, No Microchips here, or No Microchip without it's generator here.
            return true;
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
