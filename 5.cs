using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    class Progaram
    {
        static void Main()
        {
            Console.WriteLine(" if the input is from a file, enter 1, if the input is from the console, enter 2");
            int input_flag = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Input 'a' if you want sort the words of a string alphabetically and display a word consisting of the last characters of these words. ");
            Console.WriteLine("Input 'b' if you want in each word of the line, raise the case of the first letter of the word and lower the case of the last letters.");
            Console.WriteLine("Input 'c' if you want count how many times a given (console input) word occurs in this line.");
            Console.WriteLine("Input 'd' if you want replace the penultimate word in this line with the entered word (input with nconsole).");
            Console.WriteLine("Input 'e' if you want Find k − th word in the line starting with a capital letter (console input).");
            string flag = Console.ReadLine();
            if (input_flag == 1)
            {
                Console.WriteLine("enter the path to the file: ") ;
                string file = Console.ReadLine();
                if (File.Exists(file))
                {
                    string input = File.ReadAllText(file);
                    if (String.Compare(flag, "a")==0)
                    {
                        flag_a(input);
                    }
                    else if (String.Compare(flag, "b") == 0)
                    {
                        Console.WriteLine(flag_b(input));
                    }
                    else if (String.Compare(flag, "c") == 0)
                    {
                        Console.WriteLine(flag_c(input));
                    }
                    else if (String.Compare(flag, "d") == 0)
                    {
                        Console.WriteLine(flag_d(input));
                    }
                    else if (String.Compare(flag, "e") == 0)
                    {
                        Console.WriteLine((flag_e(input)));
                    }
                    else Console.WriteLine("unknown flag");
                }
                else
                    Console.WriteLine("file does not exist");
            }
            else if (input_flag == 2)
            {
                string input = Console.ReadLine();
                if (String.Compare(flag, "a") == 0)
                {
                    flag_a(input);
                }
                else if (String.Compare(flag, "b") == 0)
                {
                    Console.WriteLine(flag_b(input));
                }
                else if (String.Compare(flag, "c") == 0)
                {
                    Console.WriteLine(flag_c(input));
                }
                else if (String.Compare(flag, "d") == 0)
                {
                    Console.WriteLine(flag_d(input));
                }
                else if (String.Compare(flag, "e") == 0)
                {
                    Console.WriteLine((flag_e(input)));
                }
                else Console.WriteLine("unknown flag");

            }
            else Console.WriteLine("incorrect input");

        }
        public static void flag_a(string input)
        {
            string result = "";
            string[] words = input.Split(' ');
            Array.Sort(words, StringComparer.InvariantCultureIgnoreCase);
            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i]);
                result += words[i][words[i].Length - 1];
            
            }
            Console.WriteLine($"resulting word: {result} ");
        }
        public static string flag_b(string input)
        {
            string result = "";
            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Substring(0, 1).ToUpper() +
                            words[i].Substring(1, words[i].Length - 2) + words[i][words[i].Length - 1].ToString().ToLower();
            }
            return string.Join(" ", words);
        }
        public static int flag_c(string input)
        {
            int count = 0;
            Console.WriteLine("enter the word you want to count: ");
            string searchWord = Console.ReadLine();
            string[] words = input.Split(' ');
            foreach (string word in words)
            {
                if (String.Compare(word, searchWord) == 0) count++;
            }
            return count;
        }
        public static string flag_d(string input)
        {
            Console.WriteLine("enter a replacement word: ");
            string replacement = Console.ReadLine();
            string[] words = input.Split(' ');
            words[words.Length - 2] = replacement;
            return string.Join(" ", words);
        }
        public static string flag_e(string input) 
        {
            Console.WriteLine("enter k: ");
            int k = Convert.ToInt16(Console.ReadLine());
            string[] words = input.Split(' ');
            int count = 0;
            foreach(string word in words)
            {
                if (char.IsUpper(word[0]))
                    count++;
                if (count == k) 
                    return word;
                
            }
            Console.WriteLine("not found");
            return null;
        }
        
    }
}
