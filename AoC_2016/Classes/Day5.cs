using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AoC_2016.Classes
{
    class Day5
    {
        // Somewhere to store our bacis input
        string doorID = "ugkcyxxp";
        
        public void Solve()
        {
            CinematicPresenter presenter = new CinematicPresenter();
            int incrementer = 0;
            char[] password = new char[8] { 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' };
            int foundChars = 0;
            Thread drawThread = new Thread(new ThreadStart(presenter.Draw));
            presenter.solvedPassword = password;
            presenter.hash = "000008f82c5b3924a1ecbebf60344e00"; // just some dummy text
            drawThread.Start();
            while (foundChars < 8)
            {
                string hash = CalculateMD5Hash(doorID + incrementer.ToString());
                presenter.hash = hash;
                if (hash.StartsWith("00000") && Regex.IsMatch(hash[5].ToString(), @"^[0-7]+$"))
                {
                    int index = int.Parse(hash[5].ToString());
                    // This is a good hash, but what if we found this char already?
                    if(password[index] == 'X')
                    {
                        password[index] = hash[6];
                        foundChars++;
                        presenter.solvedPassword = password;
                    }
                    //Console.WriteLine(hash);
                    //Console.WriteLine("The index is {0}, and the input is {1}",index,hash[6]);
                    //Console.WriteLine("Found chars is {0}, and the password is {1}",foundChars, string.Join("",password));
                    //Console.ReadLine();                                   
                }
                incrementer++;
                //Console.Write(foundChars);
                //Console.SetCursorPosition(0, 0);
                
            }
            Thread.Sleep(1000);
            presenter.Stop();           
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
        class CinematicPresenter
        {
            public string hash;
            public char[] solvedPassword;

            private bool doDraw = false;

            public void Draw()
            {
                doDraw = true;
                while (doDraw)
                {
                    //0000 08f8 2c5b 3924 a1ec bebf 6034 4e00
                    string[] junkArray = new string[8];
                    junkArray[0] = hash.Substring(0, 4);
                    junkArray[1] = hash.Substring(4, 4);
                    junkArray[2] = hash.Substring(8, 4);
                    junkArray[3] = hash.Substring(12, 4);
                    junkArray[4] = hash.Substring(16, 4);
                    junkArray[5] = hash.Substring(20, 4);
                    junkArray[6] = hash.Substring(24, 4);
                    junkArray[7] = hash.Substring(28, 4);

                    Console.SetCursorPosition(0, 0); // draw over what we had.
                    Console.Write(" "); // some space first
                    for (int i = 0; i < solvedPassword.Length; i++)
                    {
                        if (solvedPassword[i] == 'X')
                        {
                            Console.Write(junkArray[i]);
                        }
                        else
                        {
                            Console.Write("   {0}", solvedPassword[i]);
                        }
                        Console.Write(" "); // some more space
                    }
                    Thread.Sleep(5);
                }
            }
            public void Stop()
            {
                doDraw = false;
            }
        }
    }
}
