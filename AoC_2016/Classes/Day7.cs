using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day7
    {
        // Somewhere to store our bacis input
        public string[] input;

        public Day7()
        {
            // Read input from file into the array
            input = File.ReadAllLines(@"Inputs\Day7.txt");
            //input = new string[4] { "abba[mnop]qrst", "abcd[bddb]xyyx", "aaaa[qwer]tyui", "ioxxoj[asdfgh]zxcvbn" };
        }

        public void Solve()
        {
            // Make a list of IPV7 addresses
            List<IPV7> addressList = new List<IPV7>();

            for (int i = 0; i < input.Length; i++)
            {
                addressList.Add(new IPV7(input[i]));
            }
            int SupportsTSL = 0;

            foreach (IPV7 i in addressList)
            {
                if (i.SupportsTSL)
                    SupportsTSL++;
            }
            Console.WriteLine("Of the {0} IPv7 addresses collected {1} supports TSL.",addressList.Count(), SupportsTSL);
        }
        class IPV7
        {
            public string SupernetSequence { get; protected set; }
            public string HypernetSequence { get; protected set; }
            public bool SupportsTSL { get; protected set; }

            public IPV7(string IPV7)
            {
                DecodeIPV7(IPV7);

                // Does it support TSL?
                if (HasABBA(SupernetSequence) && !HasABBA(HypernetSequence))
                    SupportsTSL = true;

            private void DecodeIPV7(string IPV7)
            {
                StringBuilder readSupernetSequence = new StringBuilder("");
                StringBuilder readHypernetSequence = new StringBuilder("");
                bool isInHyperNetSequence = false;

                for (int i = 0; i < IPV7.Length; i++)
                {
                    // First lets check if we are entering a Hypernet sequence
                    if (IPV7[i] == '[')
                        isInHyperNetSequence = true;
                    // or if we are leaving one
                    else if (IPV7[i] == ']')
                        isInHyperNetSequence = false;

                    // If we are in a HypernetSequence
                    if (isInHyperNetSequence)
                    {
                        if(IPV7[i] != '[')
                        {
                            readHypernetSequence.Append(IPV7[i]);
                            readSupernetSequence.Append("?");
                        }
                    }
                    else
                    {
                        if (IPV7[i] != ']')
                        {
                            readHypernetSequence.Append("?");
                            readSupernetSequence.Append(IPV7[i]);
                        }
                    }
                }
                this.HypernetSequence = readHypernetSequence.ToString();
                this.SupernetSequence = readSupernetSequence.ToString();
            }
        }
    }
}
