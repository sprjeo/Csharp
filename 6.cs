using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    class Progaram
    {


        delegate double Func(double one);
        //Пусть есть уравнение f(x)=0.
        //Найдём приближенно корень этого уравнения на отрезке [a, b]
        //с погрешностью ε. В данном диапазоне корень должен быть только один.
        //Значения f(a) и f(b) должны иметь разные знаки.

        //параметры функции: границы интервала ab, делегат, точность
        static void Main()
        {
            // Продемонстрируем работу функции для уравнения 
            double root = dichotomyMethod(-2, 2, x => 1, 0.0001);
            Console.WriteLine(" " + root);

            
            
        }

        static double dichotomyMethod(double a, double b, Func equation, double precision)
        {
            
            double left = a;
            double right = b;
            if (equation(left)* equation(right) < 0) 
            { 
                    double middle = (left + right) / 2;

                 while (Math.Abs(equation(middle)) > precision)
                   {
                if (equation(left) * equation(middle) < 0)
                    right = middle;
                else
                    left = middle;

                middle = (left + right) / 2;
                 }

                return middle;
            }
            else
            {
                throw new ArgumentException("error");
            }
            
        }
    }
    
 }

