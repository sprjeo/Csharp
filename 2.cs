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
            if (eps <= 0)
            {
                throw new ArgumentException("incorrect epsilon value", nameof(eps));
            }

            Console.WriteLine( e_1(eps));
           Console.WriteLine( e_2(eps));
           Console.WriteLine( e_3(eps));
            Console.WriteLine(p_1(eps));
            Console.WriteLine( p_2(eps));
           Console.WriteLine( p_3(eps));
           Console.WriteLine( ln_1(eps));
           Console.WriteLine( ln_2(eps));
           Console.WriteLine( ln_3(eps));
           Console.WriteLine( sqrt_1(eps));
           Console.WriteLine( sqrt_2(eps));
           Console.WriteLine( sqrt_3(eps));
           Console.WriteLine( gamma_1(eps));
           Console.WriteLine( gamma_2(eps));
            Console.WriteLine(gamma_3(eps));
        }
            

        public static double e_1(double epsilon)
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
                    return previous;
                    
                }
            }

        }
        public static double e_2(double epsilon)
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
                    return previous;
                    
                }
            }

        }
        public static double e_3(double epsilon)
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
                    return previous;
                }
            }
        }
        public static double p_1(double epsilon)
        {   
            long n = 1;
            double previous = (Math.Pow(16,n)*Math.Pow(fact(n), 4)) / (n * fact(2*n)  );
           
            while (true)
            {
                n++;
                double current = (Math.Pow(16, n) * Math.Pow(fact(n), 4)) / (n * fact(2 * n));
                
                if (Math.Abs(current - previous) > epsilon)
                {
                    previous = current;
                    
                }
                else
                {
                    return previous;
                    
                }
            }
            
        }
        public static double p_2(double epsilon)
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
                    return accum;           
                }
            }
        }
        public static double p_3(double epsilon)
        {
            for (double i = 0; i <= 2 * Math.PI; i += epsilon / 10.0)
            {
                double value = Math.Cos(i);
                if (Math.Abs(value + 1) < epsilon)
                {
                    return i;
                    
                }
                
            }
            throw new ArgumentException("error");

        }
        public static double ln_1(double epsilon)
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
                    return previous;     
                }
            }
        }
        public static double ln_2(double epsilon)
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
                    return previous;   
                }
            }
        }
        public static double ln_3(double epsilon)
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
                    return x;  
                }
            }
        }
        public static double sqrt_1(double epsilon)
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
            return x;
        }
        public static double sqrt_2(double epsilon)
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
                    return previous;      
                }
            }
        }
        public static double sqrt_3(double epsilon)
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
                    return x;     
                }

            }
        }
        public static double gamma_1(double epsilon)
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
                    return accum;        
                }
            }
            throw new ArgumentException("error");
        }
        public static double gamma_2(double epsilon)
        {
            long k = 1;
            double n = 1.0 / epsilon;
            double previous = (-Math.Pow(Math.PI, 2)) / 6;
            while (k < n)
            {
                k++;
                previous += (1.0 / Math.Pow(((int)Math.Sqrt(k)), 2) - 1.0 / k);
            }
            return previous;
        }
        public static double gamma_3(double epsilon)
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
                    return previous;        
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