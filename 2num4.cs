using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Collections.Specialized.BitVector32;

namespace Project
{
    class Program
    {


        public interface INumericalIntegration
        {
            double CalculateIntegral(Func<double, double> function, double a, double b, double eps);

            string MethodName { get; }
        }
        public sealed class LeftRectangleMethod : INumericalIntegration
        {

            public double CalculateIntegral(Func<double, double> function, double a, double b, double eps)
            {
                CheckingForErrors(function, a, b, eps);

                double calculate(Func<double, double> function, double a, double b, int n)
                {

                    double h = (b - a) / n;
                    double sum = 0.0;
                    for (int i = 0; i <= n - 1; i++)
                    {
                        sum += h * function(a + i * h);
                    }
                    return sum;

                }

                int k = 10;
                int i = 0;
                double diff;
                do
                {
                    i++;
                    diff = Math.Abs(calculate(function, a, b, k * i) - calculate(function, a, b, k * (i + 1)));
                }
                while (diff > eps);
                return calculate(function, a, b, k * (i + 1));
            }

            public string MethodName => "Left Rectangle Method";
        }

        public class RightRectangleMethod : INumericalIntegration
        {
            public double CalculateIntegral(Func<double, double> function, double a, double b, double eps)
            {
                CheckingForErrors(function, a, b, eps);

                double calculate(Func<double, double> function, double a, double b, int n)
                {
                    double h = (b - a) / n;
                    double sum = 0.0;
                    for (int i = 1; i <= n; i++)
                    {
                        sum += h * function(a + i * h);
                    }
                    return sum;
                }

                int k = 10;
                int i = 0;
                double diff;
                do
                {
                    i++;
                    diff = Math.Abs(calculate(function, a, b, k * i) - calculate(function, a, b, k * (i + 1)));
                }
                while (diff > eps);
                return calculate(function, a, b, k * (i + 1));
            }

            public string MethodName => "Right Rectangle Method";
        }

        public class MidpointRectangleMethod : INumericalIntegration
        {
            public double CalculateIntegral(Func<double, double> function, double a, double b, double eps)
            {

                CheckingForErrors(function, a, b, eps);

                double calculate(Func<double, double> function, double a, double b, int n)
                {

                    double h = (b - a) / n;
                    double sum = 0.0;
                    for (int i = 0; i <= n - 1; i++)
                    {
                        double x = a + h * (i + 0.5);
                        sum += function(x);
                    }
                    return h * sum;

                }

                int k = 10;
                int i = 0;
                double diff;
                do
                {
                    i++;
                    diff = Math.Abs(calculate(function, a, b, k * i) - calculate(function, a, b, k * (i + 1)));
                }
                while (diff > eps);
                return calculate(function, a, b, k * (i + 1));
            }

            public string MethodName => "Midpoint Rectangle Method";
        }
        public class TrapezeMethod : INumericalIntegration
        {
            public double CalculateIntegral(Func<double, double> function, double a, double b, double eps)
            {
                CheckingForErrors(function, a, b, eps);

                double calculate(Func<double, double> function, double a, double b, int n)
                {
                    double h = (b - a) / n;

                    double sum = function(a) + function(b);
                    for (int i = 1; i <= n - 1; i++)
                    {
                        sum += 2 * function(a + i * h);
                    }
                    sum *= h / 2;
                    return sum;
                }

                int k = 10;
                int i = 0;
                double diff;
                do
                {
                    i++;
                    diff = Math.Abs(calculate(function, a, b, k * i) - calculate(function, a, b, k * (i + 1)));
                }
                while (diff > eps);
                return calculate(function, a, b, k * (i + 1));
            }

            public string MethodName => "Trapeze Method";

        }
        public class SympsonMethod : INumericalIntegration
        {
            public double CalculateIntegral(Func<double, double> function, double a, double b, double eps)
            {
                CheckingForErrors(function, a, b, eps);

                double calculate(Func<double, double> function, double a, double b, int n)
                {
                    double h = (b - a) / n;

                    double sum = function(a) + function(b);
                    int k;
                    for (int i = 1; i <= n - 1; i++)
                    {
                        k = 2 + 2 * (i % 2);
                        sum += k * function(a + i * h);
                    }
                    sum *= h / 3;
                    return sum;
                }

                int k = 10;
                int i = 0;
                double diff;
                do
                {
                    i++;
                    diff = Math.Abs(calculate(function, a, b, k * i) - calculate(function, a, b, k * (i + 1)));
                }
                while (diff > eps);
                return calculate(function, a, b, k * (i + 1));
            }

            public string MethodName => "Sympson Method";

        }

        public static void CheckingForErrors(Func<double, double> function, double a, double b, double eps)
        {
            if (a > b)
            {
                throw new ArgumentException("the value was entered incorrectly ", nameof(a));
            }
            if (eps <= 0)
            {
                throw new ArgumentException("the value was entered incorrectly", nameof(eps));
            }
            if (function == null)
            {
                throw new ArgumentException("the function was entered incorrectly", nameof(function));
            }
        }
        static void Main()
        {
            var solvers = new INumericalIntegration[]
            {
                new LeftRectangleMethod(),
                new RightRectangleMethod(),
                new MidpointRectangleMethod(),
                new TrapezeMethod(),
                new SympsonMethod()
             };

            double Foo(double x) => Math.Pow(Math.Sin(x*x),2)/ Math.Pow(Math.E, x);
            double leftBound = 5;
            double rightBound = 8;
            double epsilon = 0.0005;
            try
            {
                foreach (var solver in solvers)
                {
                    
                    Console.WriteLine(solver.MethodName);
                    Console.WriteLine(solver.CalculateIntegral(Foo, leftBound, rightBound, epsilon));
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.Write(ex.Message);
            }
        }





    }
}
