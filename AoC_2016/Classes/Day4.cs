using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day4
    {
        // Somewhere to store our bacis input
        public string[] input;

        // Let's make a list of rooms
        List<Room> roomList = new List<Room>();

        int sectorSum = 0;

        public Day4()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day4.txt");

            // And put each line into the list of rooms
            for (int i = 0; i < input.Length; i++)
            {
                roomList.Add(new Room(input[i]));
            }

            // since Rooms validate themselves when they are made, 
            //it is then a simple matter of reading all the rooms and adding together the sector ID
            
            foreach (Room r in roomList)
            {
                if (r.isValid)
                {
                    sectorSum = sectorSum + r.SectorID;
                    if (r.DecryptedName.Contains("northpole"))
                    {
                        Console.WriteLine("Room: {0} is in sector {1}",r.DecryptedName,r.SectorID);
                    }
                }
            }
        }

        public void Solve()
        {
            Console.WriteLine("The sum is {0}",sectorSum);
            
        }

        // Let's define the rooms for a list of objects
        class Room
        {
            public string EncryptedName { get; protected set; }
            public string DecryptedName { get; protected set; }
            public int SectorID { get; protected set; }
            public string Checksum { get; protected set; }

            public bool isValid { get; protected set; }

            // A room is made by feeding it a line like this:
            // aaaaa-bbb-z-y-x-123[abxyz]
            public Room(string roomString)
            {
                // This method is then called to split the information.
                ParseRoomString(roomString);

                // The room then validates itself.
                isValid = Validate();
                DecryptedName = Decrypt();
            }

            private void ParseRoomString(string roomString)
            {
                // Make the room string into a char array
                char[] charArray = roomString.ToCharArray();

                // The string can be though of simpler as three groups:
                // a-z, 0-9 & a-z; encryptedName, sectorID & checksum.
                // So we can build three string from that:
                StringBuilder encryptedNameBuilder = new StringBuilder("");
                StringBuilder sectorIDBuilder = new StringBuilder("");
                StringBuilder checksumBuilder = new StringBuilder("");

                // Then reading each char in the string we first find the encrypted name
                bool encryptedNameComplete = false;
                for (int i = 0; i < charArray.Length; i++)
                {
                    // So using Regex, is this a character from a-z?
                    if (Regex.IsMatch(charArray[i].ToString(), @"^[a-zA-Z]+$") && !encryptedNameComplete)
                    {
                        encryptedNameBuilder.Append(charArray[i]);
                    }
                    // So if it was not a char from a-z, then maybe it is a number?
                    else if (Regex.IsMatch(charArray[i].ToString(), @"^[0-9]+$"))
                    {
                        sectorIDBuilder.Append(charArray[i]);
                        // And having reached the sectorID, that means we are done with the encrypted name.
                        encryptedNameComplete = true;
                    }
                    // But after that we will find more chars a-z.
                    else if (Regex.IsMatch(charArray[i].ToString(), @"^[a-zA-Z]+$") && encryptedNameComplete)
                    {
                        checksumBuilder.Append(charArray[i]);
                    }
                }

                // Now that all the strings are built, we can put them where they should be.
                this.EncryptedName = encryptedNameBuilder.ToString();
                this.SectorID = int.Parse(sectorIDBuilder.ToString());
                this.Checksum = checksumBuilder.ToString();
                
            }
            private bool Validate()
            {
                // this thing from: https://github.com/Pyrobolser/AdventOfCode2016/blob/master/AdventOfCode2016/Days/Day4.cs
                // thank you to pyrobolser, Today I learned about new fansy things to do with string, using LINQ. :)
                string calculatedCheckSum = string.Join("", EncryptedName.GroupBy(c => c).OrderByDescending(c => c.Count()).ThenBy(c => c.Key).Take(5).Select(c => c.Key).ToList());

                // return the result of this evaluation.
                return (calculatedCheckSum == this.Checksum) ? true : false;
            }
            private string Decrypt()
            {
                // All the letters
                string AtoZ = "abcdefghijklmnopqrstuvwxyz";

                StringBuilder returnString = new StringBuilder("");

                // Read each letter
                for (int i = 0; i < EncryptedName.Length; i++)
                {
                    int start = AtoZ.IndexOf(EncryptedName[i]);
                    returnString.Append(AtoZ[(start + SectorID) % 26]);
                }

                return returnString.ToString();
            }
        }
    }

}
