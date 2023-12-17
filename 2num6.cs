using System;
using System.Collections;
using System.Collections.Generic;

namespace Project
{
    public interface IPrimalityTest
    {
        bool IsPrime(int number, double minProbability);
    }
    //Абстракттый класс ProbabilisticPrimalityTest:

    public abstract class ProbabilisticPrimalityTest : IPrimalityTest
    {
        protected Random random = new Random();

        public abstract bool IsPrime(int number, double minProbability);

        public int CalculateIterations(double minProbability)
        {
            int iterations = 1;
            double probability = 1.0 - minProbability;

            while (Math.Pow(1.0 - 1.0 / Math.Pow(2, iterations), iterations) >= probability)
            {
                iterations++;
            }

            return iterations;
        }
    }
    


    public class DeterministicPrimalityTest : IPrimalityTest
    {
        public bool IsPrime(int number, double minProbability)
        {
            

            if (number <= 1)
            {
                return false;
            }
            if (number <= 3)
            {
                return true;
            }
            if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            int i = 5;
            while (i * i <= number)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                {
                    return false;
                }
                i += 6;
            }
            return true;
        }
    }

    public class FermatPrimalityTest : ProbabilisticPrimalityTest
    {
        private static Random random = new Random();

        public override bool IsPrime(int number, double minProbability)
        {
            var b = new NumberTheory();

            if (number <= 1)
                    return false;
                if (number <= 3)
                    return true;

                for (int i = 0; i < 1000; i++)
                {
                    int a = random.Next(2, number - 2);
                    if (b.GCD(a, number) != 1)
                        return false;
                    if (b.ModPow(a, number - 1, number) != 1)
                        return false;
                }
                return true;
            
            //
            //         if (number <= 1)
            //         {
            //             return false;
            //         }
            //         if (number <= 3)
            //         {
            //             return true;
            //         }
            //
            //         Random random = new Random();
            //         int numberOfWitnesses = 0;
            //         var b = new NumberTheory();
            //         for (int i = 0; i < 1000; i++)
            //         {
            //             int a = random.Next(2, number - 1);
            //             if (b.GCD(a, number) != 1)
            //             {
            //                 return false;
            //             }
            //             if (b.ModPow(a, number - 1, number) != 1)
            //             {
            //                 numberOfWitnesses++;
            //             }
            //         }
            //
            //         double probability = 1 - Math.Pow(0.5, numberOfWitnesses);
            //            return probability >= minProbability;
        }
    }

    public class SolovayStrassenPrimalityTest : ProbabilisticPrimalityTest
    {
        private static Random random = new Random();
        public override bool IsPrime(int number, double minProbability)
        {
            var b = new NumberTheory();
            if (number < 2)
                return false;
            if (number != 2 && number % 2 == 0)
                return false;

            for (int i = 0; i < 1000; i++)
            {
                int a = random.Next(2, number - 1);
                int jacobi = b.JacobiSymbol(a, number);
                int modPow = b.ModPow(a, (number - 1) / 2, number);

                if (jacobi == 0 || modPow != jacobi % number)
                    return false;
            }
            return true;
        }
    

        //          if (number <= 1)
        //          {
        //              return false;
        //          }
        //          if (number == 2)
        //          {
        //              return true;
        //          }
        //          if (number % 2 == 0)
        //          {
        //              return false;
        //          }
        //
        //          Random random = new Random();
        //          for (int i = 0; i < 1000; i++)
        //          {
        //              int a = random.Next(2, number - 1);
        //              var b = new NumberTheory();
        //              int jacobi = b.JacobiSymbol(a, number);
        //              int mod = b.ModPow(a, (number - 1) / 2, number);
        //              if (mod == 0 || jacobi == 0 || mod != jacobi % number)
        //              {
        //                  return false;
        //              }
        //          }
        //
        //          double probability = 1 - Math.Pow(0.5, 1000);
        //          return probability >= minProbability;
    }
    

    public class MillerRabinPrimalityTest : ProbabilisticPrimalityTest
    {
        public override bool IsPrime(int number, double minProbability)
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

            Random random = new Random();
            int d = number - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            for (int i = 0; i < 1000; i++)
            {
                int a = random.Next(2, number - 2);
                var b = new NumberTheory();

                int x = b.ModPow(a, d, number);
                if (x == 1 || x == number - 1)
                {
                    continue;
                }
                bool isProbability = false;
                for (int j = 0; j < s - 1; j++)
                {
                    x = b.ModPow(x, 2, number);
                    if (x == 1)
                    {
                        return false;
                    }
                    if (x == number - 1)
                    {
                        isProbability = true;
                        break;
                    }
                }
                if (!isProbability)
                {
                    return false;
                }
            }

            return true;
        }
    }
    public class NumberTheory
    {
        public int GCD(int a, int b)
        {
            
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        public int LegendreSymbol(int a, int p)
        {
            int result;

            if (a % p == 0)
            {
                result = 0;
            }
            else if (ModPow(a, (p - 1) / 2, p) == 1)
            {
                result = 1;
            }
            else
            {
                result = -1;
            }

            return result;
        }
        public int JacobiSymbol(int a, int n)
        {
            if (n <= 0 || n % 2 == 0)
            {
                throw new ArgumentException("n must be a positive odd integer");
            }

            if (a == 0)
            {
                return a;
            }
            if (a == 1)
            {
                return 1;
            }

            int e = 0;
            int a1 = a;
            while (a1 % 2 == 0)
            {
                a1 /= 2;
                e++;
            }

            int s;
            if (e % 2 == 0 || n % 8 == 1 || n % 8 == 7)
            {
                s = 1;
            }
            else
            {
                s = -1;
            }

            if (n % 4 == 3 && a1 % 4 == 3)
            {
                s = -s;
            }

            if (a1 == 1)
            {
                return s;
            }

            return s * JacobiSymbol(n % a1, a1);
        }



        public int ModPow(int x, int y, int p) //  Modular Exponentiation
        {
            if (y == 0)
            {
                return 1;
            }

            long z = ModPow(x, y / 2, p);

            if (y % 2 == 0)
            {
                return (int)((z * z) % p);
            }
            else
            {
                return (int)((x * z * z) % p);
            }
        }
    }


    class Program
    {
        static void Main()
        {
            int numberToTest =499;
            double minProbability = 0.95;
            DeterministicPrimalityTest determ = new DeterministicPrimalityTest();
            bool isPrime = determ.IsPrime(numberToTest, minProbability);
            if (isPrime)
            {
                Console.WriteLine("Deterministic Primality Test: number " + numberToTest + " primality");
            }
            else
            {
                Console.WriteLine("Deterministic Primality Test: number " + numberToTest + " not primality");
            }
            FermatPrimalityTest ferm = new FermatPrimalityTest();
            isPrime = ferm.IsPrime(numberToTest, minProbability);
            if (isPrime)
            {
                Console.WriteLine("Fermat Primality Test: number " + numberToTest + " primality");
            }
            else
            {
                Console.WriteLine("Fermat Primality Test: number " + numberToTest + " not primality");

            }
            SolovayStrassenPrimalityTest solov = new SolovayStrassenPrimalityTest();
            isPrime = solov.IsPrime(numberToTest, minProbability);
            if (isPrime)
            {
                Console.WriteLine("SolovayStrassen Primality Test: number " + numberToTest + " primality");
            }
            else
            {
                Console.WriteLine("SolovayStrassen Primality Test: number " + numberToTest + " not primality");
            }
            MillerRabinPrimalityTest mill = new MillerRabinPrimalityTest();
            isPrime = mill.IsPrime(numberToTest, minProbability);
            if (isPrime)
            {
                Console.WriteLine("MillerRabin Primality Test: number " + numberToTest + " primality");
            }
            else
            {
                Console.WriteLine("MillerRabin Primality Test: number " + numberToTest + " not primality");
            }


        }
    }
}



   



