using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day5
    {
        // Somewhere to store our bacis input
        string doorID = "ugkcyxxp";
        
        public void Solve()
        {
            int incrementer = 0;
            StringBuilder password = new StringBuilder("");
            while (password.ToString().Length < 9)
            {
                string hash = CalculateMD5Hash(doorID + incrementer.ToString());
                if (hash.StartsWith("00000"))
                {
                    password.Append(hash[5]);
                    Console.WriteLine("{0} | {1} | {2}", hash, hash[5], incrementer);
                }
                incrementer++;
            }
            Console.WriteLine("Incremented {0} times, and the pasword is {1}",incrementer,password.ToString());
           
        }
        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
