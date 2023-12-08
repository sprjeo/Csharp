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
                
                double[] answer = flag_c();
                
                Console.WriteLine($" geometric mean:{answer[0]} ");
                
                Console.WriteLine($" harmonic mean:{answer[1]} " );
            }
            else if (String.Compare(args[0], "-f") == 0 && args.Length == 2)
            {
                if (File.Exists(args[1]))
                {
                    double[] answer = flag_f(args[1]);
                   
                    Console.WriteLine($" geometric mean: {answer[0]}" );
                    
                    Console.WriteLine($" harmonic mean:{answer[1]} ");
                }
                else Console.WriteLine("file not exist");

            }
            else Console.WriteLine("incorrect args");
        }
        public static double[] flag_c()
        {
            Console.WriteLine("Input numbers: ");
            string input = Console.ReadLine();
            List <double> validNum = validity(input);
            return new double[] {geometric_mean(validNum), harmonic_mean(validNum)};
        }
        public static double[] flag_f(string file) 
        {
            string input = File.ReadAllText(file);
            List<double> validNum = validity(input);
            return new double[] { geometric_mean(validNum), harmonic_mean(validNum) };
        }

        public static  List<double> validity(string input)

        { 
            string[] numbers = input.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<double> validNumbers = new List<double>();
            
            foreach (string number in numbers)
            {
                
                string correct_number = number.Replace(".",",");
                

                if (double.TryParse(correct_number, out double parsedNumber))
                {
                    validNumbers.Add(parsedNumber);
                }
                else
                {
                    Console.WriteLine($"{correct_number} is not a number");

                }
            }
            if (validNumbers.Count == 0)
            {
                Console.WriteLine("No valid numbers found");
                return validNumbers;


            }
            else
            {
                return validNumbers;
            } 
        }
        
        public static double geometric_mean(List<double> numbers)
        {
            double count = 1.0;
            foreach (double number in numbers)
            {
                count *= number;
            }
            return Math.Pow(count, 1.0 / numbers.Count);
        }
        public static double harmonic_mean(List<double> numbers)
        {
            double count = 0.0;
            foreach (double number in numbers)
            {
                count += 1.0 / number;
            }
            return numbers.Count / count;
        }
    }
}
