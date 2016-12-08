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
            int SupportsSSL = 0;

            foreach (IPV7 i in addressList)
            {
                if (i.SupportsTSL)
                    SupportsTSL++;
                if (i.SupportsSSL)
                    SupportsSSL++;
            }
            Console.WriteLine("Of the {0} IPv7 addresses collected {1} supports TSL.",addressList.Count(), SupportsTSL);
            Console.WriteLine("Of the {0} IPv7 addresses collected {1} supports SSL.", addressList.Count(), SupportsSSL);
            
        }
        class IPV7
        {
            public string SupernetSequence { get; protected set; }
            public string HypernetSequence { get; protected set; }
            public bool SupportsTSL { get; protected set; }
            public bool SupportsSSL { get; protected set; }

            public IPV7(string IPV7)
            {
                DecodeIPV7(IPV7);

                // Does it support TSL?
                if (HasABBA(SupernetSequence) && !HasABBA(HypernetSequence))
                    SupportsTSL = true;

                // Does is support SSL?
                string[] ABAs; 
                if(HasABA(SupernetSequence,out ABAs)){
                    for (int i = 0; i < ABAs.Length; i++)
                    {
                        if (HasBAB(HypernetSequence, ABAs[i]))
                        {
                            SupportsSSL = true;
                        }
                    }
                }
            }

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
            private bool HasABBA(string sequence)
            {
                bool hasABBA = false;
                for (int i = 0; i < sequence.Length-3; i++)
                {
                    if (sequence[i] == sequence[i + 3] && sequence[i + 1] == sequence[i + 2] && sequence[i] != sequence[i + 1])
                    {
                        hasABBA = true;
                        return hasABBA;
                    }
                }
                return hasABBA;
            }
            private bool HasABA(string sequence, out string[] ABA)
            {
                List<string> foundABA = new List<string>();
                bool hasABA = false;

                for (int i = 0; i < sequence.Length - 2; i++)
                {
                    if (sequence[i] == sequence[i + 2] && sequence[i] != sequence[i+1])
                    {
                        //Console.WriteLine("Found ABA in Supernet: {0}", sequence[i].ToString() + sequence[i + 1].ToString() + sequence[i + 2].ToString());
                        foundABA.Add(sequence[i].ToString() + sequence[i + 1].ToString() + sequence[i + 2].ToString());
                        hasABA = true;
                        //Console.ReadLine();
                    }
                }
                ABA = foundABA.ToArray();
                return hasABA;
            }
            private bool HasBAB(string sequence, string ABA)
            {
                if (sequence.Contains(ABA[1].ToString() + ABA[0].ToString() + ABA[1].ToString()))
                    return true;
                return false;
            }
        }
    }
}
