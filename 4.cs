using System;
using System.Collections.Generic;
using System.Linq;


namespace Project
{
    class Progaram
    {
        static void Main()
        {
            Console.WriteLine("enter a decimal fraction: ");
            string input = Console.ReadLine();
            if (double.TryParse(input, out double decimalFraction))
            {
                Console.WriteLine("enter the base of the number system (2 - 36): ");
                int k = Convert.ToInt32(Console.ReadLine());
                string result = ConvertDecimalFractionToBase(decimalFraction, k);
                Console.WriteLine($"Fraction in base {k}: {result}");
            }
            else
            {
                Console.WriteLine("the entered value is not a decimal fraction");
            }
        }
        public static string ConvertDecimalFractionToBase(double decimalFraction, int k)
        {
            string result = "";
            string result_int = "";
            string result_fr = "";
            
            int integer_part = (int)decimalFraction;
            double indicator = (decimalFraction - integer_part)*k;

            while (integer_part >= k)
            {
                if ((integer_part % k) >= 10 && (integer_part) % k <= 36)
                {
                    result_int += (char)((integer_part % k) - 10 + 'A');
                }
                else
                {
                    result_int += integer_part % k;
                }
                integer_part /= k;
                
            }
            if (integer_part >= 10 && integer_part <= 36)
            {
                result_int += (char)(integer_part - 10 + 'A');
            }
            else
            {
                result_int += integer_part;
            }
            while (indicator >= 0.001 && result_fr.Length < 20)
            {
                if ((int)indicator >= 10 && (int)indicator <= 36)
                {
                    result_fr += (char)((int)indicator - 10 + 'A');
                }
                else
                {
                    result_fr += (int)indicator;
                }
                indicator = (indicator - (int)indicator) * k;

            }
            result_int = ReverseString(result_int);
            result = result_int + "," + result_fr;



            return result;

        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

    }

    
}
