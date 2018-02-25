using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg_Lab2_Var6
{

    class Program
    {

        static double f(double x,double a)
        {
            return Math.Exp(-x) / (x + a);
        }
        /// <summary>
        /// Обчислює інтеграл
        /// </summary>
        /// <param name="from">нижня межа інтегрування</param>
        /// <param name="to">верхня межа інтегрування</param>
        /// <param name="h">крок</param>
        /// <param name="a">параметр</param>
        /// <returns>значення інтегрла</returns>
        static double CalcualteIntegral(double from, double to, double h,double a)
        {
            double Integral = 0;
            double x = from;
            do
            {
                Integral += f(x, a) + f(x + h, a);
                x += h;
            } while (x < to);

            Integral = (h / 2) * Integral;
            return Integral;
        }

        static void Main(string[] args)
        {
            try
            {
                double e = 0.0001;//необхідна точність інтегрування
                double h; //крок

                double[] Integrals = new double[10];//масив значень інтегралів
                double[] Eps = new double[10];//масив похибок

                double b = Math.Log(2 / e);//оцінка верхньої межі

                for (int a = 0; a < 10; a++)
                {
                    h = 1;
                    do
                    {
                        h *= 0.1;
                        Integrals[a] = CalcualteIntegral(0, b, h, a + 1);
                        Eps[a] = Math.Abs(Integrals[a] - CalcualteIntegral(0, b, h * 2, a + 1)) / 3;//похибка інтегрування, обчислена за методом Рунге 
                    } while (Eps[a] >= e);
                    Console.WriteLine("Для значення a={0} значення iнтегралу I({0})={1,0:f2} з кроком h={3,0:f3} точнiстю e={2,0:f8}", a + 1, Integrals[a], Eps[a],h);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Щось пішло не так :(");
                Console.WriteLine(e.Message);
            }
        }
    }
}
