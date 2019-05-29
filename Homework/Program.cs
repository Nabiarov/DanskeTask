/* Danske Bank task
 * Created by Edvinas Ščigla */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Homework
{
    class Program
    {
        /* 1st file for reading and checking
         * 2nd file for saving records 
         * Path location depending on project location */
       
        static string firstFilePath = @"C:\Users\Edvin\Source\Repos\Homework\Homework\remote_server\share\file.txt";
        static string secondFilePath = @"C:\Users\Edvin\Source\Repos\Homework\Homework\remote_server2\share\file.txt";

        static void Main(string[] args)
        {
            // checking if files exists, if not we`re creating it.
            if (!File.Exists(firstFilePath))
            {
                Console.WriteLine("1st file didn`t exist, creating one now");
                File.Create(firstFilePath);
            }

            if (!File.Exists(secondFilePath))
            {
                Console.WriteLine("2nd file didn`t exist, creating one now");
                File.Create(secondFilePath);
            }

            Console.WriteLine("Processing ...");
            readAndTransfer();
        }

    public static void readAndTransfer()
        {
     
            // substring to check.
            String symbolCheck = "ZZ.ZZ.ZZ";

            // if pattern is finded, next string should be reversed.
            Boolean patternFinded = false;

            try
            {
                TextReader reader = new StreamReader(firstFilePath);
                TextWriter writer = new StreamWriter(secondFilePath);

                TimeSpan interval = new TimeSpan(0, 0, 1);

                String buffer = "";
                while((buffer = reader.ReadLine()) != null)
                {
                    if (patternFinded == true)
                    {
                        string text = buffer;
                        writer.WriteLine(reverseString(text));

                        patternFinded = false;
                    }

                    else
                    {
                        if (buffer.Contains(symbolCheck))
                        {
                            if (buffer.EndsWith(symbolCheck))
                            {
                                writer.WriteLine(buffer);

                            }
                            else
                            {
                                writer.WriteLine(buffer);
                                patternFinded = true;
                            }
                        }
                        else
                        {
                            writer.WriteLine(buffer);
                        }
                    }

                    // 1 second pause between reading other line.
                    Thread.Sleep(interval);

                }

                reader.Close();
                writer.Close();

                /* To null file (not line by line)
                 * File.Delete(firstFilePath);
                 * File.Create(firstFilePath);
                 *  */
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Error: " + ioe.Message);
            }    

        }

        public static string reverseString(string text)
        {
            if (text == null) return null;

            char[] arr = text.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
