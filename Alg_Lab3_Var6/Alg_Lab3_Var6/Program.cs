using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg_Lab3_Var6
{
    class Program
    {
        /// <summary>
        /// метод обчислює значення функції
        /// </summary>
        /// <param name="x">аргумент функції</param>
        /// <returns>значення функції</returns>
        static double func(double x)
        {
            return x * x * x - 5 * x * x + 4 * x + 0.092;
        }


        /// <summary>
        /// Метод отримує наближене значення кореня рівняння
        /// </summary>
        /// <param name="a">ліва межа проміжку на якому є тільки 1 корінь</param>
        /// <param name="b">права межа проміжку на якому є тільки 1 корінь</param>
        /// <param name="e">необхідна точність</param>
        /// <param name="eps">отримана точність</param>
        /// <returns>корінь рівняння</returns>
        static double Bisection(double a,double b,double e,out double eps)
        {
            double x;
            int k = 0;
            do
            {
                x = (a + b) / 2;
                if (func(a) * func(x) < 0) b = x;
                if (func(b) * func(x) < 0) a = x;
                k++;
            } while (Math.Abs(b-a)>e && func(x) != 0);
            eps = Math.Abs(b - a);
            Console.WriteLine("k="+ k);
            return x;
        }
        static void Main(string[] args)
        {
            try
            {
                double extr1_x = (5 - Math.Sqrt(13)) / 3;   //аргумент точкуи екстремуму
                double extr2_x = (5 + Math.Sqrt(13)) / 3;   //аргумент точкуи екстремуму   
                double a = -2;  //початкова межа відрізку на якому задана функція має 3 розв'язки
                double b = 5;   //кінцева межа відрізку на якому задана функція має 3 розв'язки
                double e = 0.0001;  //точність
                double real_eps;    //отримана точність

                Console.WriteLine("x1={0,2:f5} з точністю e={1,2:f5}", Bisection(a, extr1_x, e,out real_eps),real_eps);
                Console.WriteLine("x2={0,2:f5} з точністю e={1,2:f5}", Bisection(extr1_x, extr2_x, e, out real_eps),real_eps);
                Console.WriteLine("x3={0,2:f5} з точністю e={1,2:f5}", Bisection(extr2_x, b, e, out real_eps),real_eps);
            }
            catch (Exception err)
            {
                Console.WriteLine("Щось пішло не так =(\n"+err.Message);
            }
        }
    }
}
