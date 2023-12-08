using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    class Progaram
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || String.Compare(args[0], "-c") == 0)
            {
                Console.WriteLine(flag_c());
            }
            else if (String.Compare(args[0], "-f") == 0 && args.Length == 2)
            {
                //Console.WriteLine(flag_f(args[1]));
                if (File.Exists(args[1]))
                    Console.WriteLine(flag_f(args[1]));
               else Console.WriteLine("file not exist");
                   

            }
            else Console.WriteLine("incorrect args");

        }
        public static int flag_c()
        {
            int count = 0;
            int sum = 0;
            Console.WriteLine("input characters: ");
            string[] symbols = Console.ReadLine().Split(' ');

            foreach (string symbol in symbols)
            {
                foreach (char c in symbol)
                {
                    int charCode = (int)c;
                    sum += charCode;
                    count++;
                }
            }
            if (count > 0)
                return sum/count;
            else
            {
                Console.WriteLine("No characters entered");
                return -1;
            }
        }
        public static int flag_f(string file)
        {
            string text = File.ReadAllText(file);
            int count = 0;
            int sum = 0;
            string[] symbols = text.Split(" ");

            foreach (string symbol in symbols)
            {
                foreach (char c in symbol)
                {
                    int charCode = (int)c;
                    sum += charCode;
                    count++;
                }
            }
            if (count > 0)
                return sum / count;
            else
            {
                Console.WriteLine("No characters in file");
                return -1;
            } 
        }
    }
}