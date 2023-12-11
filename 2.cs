using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            double eps = Convert.ToDouble(args[0]);
            e_1(eps);
            e_2(eps);
            e_3(eps);
            p_1(eps);
            p_2(eps);
            p_3(eps);
            ln_1(eps);
            ln_2(eps);
            ln_3(eps);
            sqrt_1(eps);
            sqrt_2(eps);
            sqrt_3(eps);
            gamma_1(eps);
            gamma_2(eps);
            gamma_3(eps);
        }
            

            public static void e_1(double epsilon)
        {
            double n = 2;
            double previous = Math.Pow((1.0 + 1.0 / n), n);
            while (true)
            {
                n++;
                double current = Math.Pow((1.0 + 1.0 / n), n);
                if (Math.Abs(current - previous) > epsilon)
                {
                    previous = current;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
            }

        }
        public static void e_2(double epsilon)
        {
            long n = 0;
            double previous = 1.0 / (double)fact(n);
            n++;
            previous += 1.0 / (double)fact(n);
            while (true)
            {
                n++;
                double current = previous + 1.0 / (double)fact(n);
                if (Math.Abs(current - previous) > epsilon)
                {
                    previous = current;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
            }

        }
        public static void e_3(double epsilon)
        {
            double step = epsilon / 10;
            double previous = step;
            while (true)
            {
                double current = previous + step;
                if (Math.Abs(1.0 - Math.Log(current)) > epsilon)
                {
                    previous = current;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
            }
        }
        public static void p_1(double epsilon)
        {
            //epsilon = epsilon < 0.001 ? 0.00999 : epsilon;
            long n = 1;
            double previous = Math.Pow(2, 4 * n) * Math.Pow(fact(n), 4) / n / Math.Pow(fact(2 * n), 2);
            while (true)
            {
                n++;
                double current = Math.Pow(2, 4 * n) * Math.Pow(fact(n), 4) / n / Math.Pow(fact(2 * n), 2);
                if (Math.Abs(current - previous) > epsilon)
                {
                    previous = current;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
            }
        }
        public static void p_2(double epsilon)
        {
            long n = 1;
            double previous = Math.Pow(-1, n + 1) / (2 * n - 1);
            double accum = previous;
            while (true)
            {
                n++;
                double current = Math.Pow(-1, n + 1) / (2 * n - 1);
                if (Math.Abs(current - previous) > epsilon)
                {
                    accum += current;
                    previous = current;
                }
                else
                {
                    accum *= 4;
                    Console.WriteLine(accum);
                    break;
                }
            }
        }
        public static void p_3(double epsilon)
        {
            for (double i = 0; i <= 2 * Math.PI; i += epsilon / 10.0)
            {
                double value = Math.Cos(i);
                if (Math.Abs(value + 1) < epsilon)
                {
                    Console.WriteLine(i);
                    break;
                }
            }

        }
        public static void ln_1(double epsilon)
        {
            double n = 1;
            double previous = n * (Math.Pow(2, (1.0 / n)) - 1);
            while (true)
            {
                n++;
                double current = n * (Math.Pow(2, (1.0 / n)) - 1);
                if (Math.Abs(current - previous) > epsilon)
                {
                    previous = current;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
            }
        }
        public static void ln_2(double epsilon)
        {
            double n = 1;
            double previous = Math.Pow(-1, n - 1) / n;
            while (true)
            {
                n++;
                double current = previous + Math.Pow(-1, n - 1) / n;
                if (Math.Abs(current - previous) > epsilon)
                {
                    previous = current;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
            }
        }
        public static void ln_3(double epsilon)
        {
            double step = epsilon / 10.0;
            double x = step;
            while (true)
            {
                if (Math.Abs(Math.Exp(x) - 2.0) > epsilon)
                {
                    x += step;
                }
                else
                {
                    Console.WriteLine(x);
                    break;
                }
            }
        }
        public static void sqrt_1(double epsilon)
        {
            double n = 2;
            double x = 1;
            for (; ; )
            {
                double nx = (x + n / x) / 2;
                if (Math.Abs(x - nx) < epsilon)
                {
                    break;
                }
                x = nx;
            }
            Console.WriteLine(x);
        }
        public static void sqrt_2(double epsilon)
        {
            int k = 2;

            double previous = Math.Pow(2, Math.Pow(2, -k));

            while (true)
            {
                k++;
                double current = previous * Math.Pow(2, Math.Pow(2, -k));
                if (Math.Abs(previous - current) > epsilon)
                {
                    previous = current;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
            }
        }
        public static void sqrt_3(double epsilon)
        {
            double step = epsilon / 10.0;
            double x = step;
            while (true)
            {
                if (Math.Abs(Math.Pow(x, 2) - 2.0) > epsilon)
                {
                    x += step;
                }
                else
                {
                    Console.WriteLine(x);
                    break;
                }

            }
        }
        public static void gamma_1(double epsilon)
        {
            int n = (int)(1.0 / epsilon) * 100;
            double accum = 1.0;
            double previous = 1.0;
            for (int i = 2; i < n; i++)
            {
                double current = accum + (1.0 / i) - Math.Log(n);
                if (Math.Abs(current - previous) > epsilon)
                {
                    accum += 1.0 / i;
                    previous = current;
                }
                else
                {
                    accum = accum - Math.Log(i);
                    Console.WriteLine(accum);
                    break;
                }
            }
        }
        public static void gamma_2(double epsilon)
        {
            long k = 1;
            double n = 1.0 / epsilon;
            double previous = (-Math.Pow(Math.PI, 2)) / 6;
            while (k < n)
            {
                k++;
                previous += (1.0 / Math.Pow(((int)Math.Sqrt(k)), 2) - 1.0 / k);
            }
            Console.WriteLine(previous);
        }
        public static void gamma_3(double epsilon)
        {
            int k = 2;
            double previous = (((double)(k - 1)) / k);
            while (true)
            {
                k++;
                double current = 1;
                if (IsPrime(k))
                {
                    current = (((double)(k - 1)) / k);
                }
                else
                {
                    continue;
                }
                if (Math.Abs(current * previous - previous) > epsilon)
                {
                    previous *= current;
                }
                else
                {
                    previous *= Math.Log(k);
                    Console.WriteLine(previous);
                    break;
                }
            }
        }
        static long fact(long number)
        {
            if (number == 0)
            {
                return 1;
            }
            long result = 1;
            for (long i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }
            if (number == 2)
            {
                return true;
            }
            if (number % 2 == 0)
            {
                return false;
            }
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;

        }
    }
}