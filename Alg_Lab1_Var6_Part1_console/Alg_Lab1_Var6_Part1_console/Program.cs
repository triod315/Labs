using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Alg_Lab1_Var6_Part1_console
{
    class Program
    {

        /// <summary>
        /// Виводить таблицю аргументів і значень функції
        /// </summary>
        /// <param name="x_arr">масив аргументів функці</param>
        /// <param name="y_arr">масив значень функції</param>
        static void PrintTable(double[] x_arr, double[] y_arr)
        {
            Console.WriteLine("\n_________________________________________________________________________");
            for (int i = 0; i < x_arr.Length; i++)
            {
                Console.Write("{0,-10:f2}", x_arr[i]);
            }
            Console.WriteLine("\n-------------------------------------------------------------------------");
            for (int i = 0; i < y_arr.Length; i++)
            {
                Console.Write("{0,-10:f5}", y_arr[i]);
            }
            Console.WriteLine("\n_________________________________________________________________________");
        }

        static void PrintTableInFile(double[] x_arr, double[] y_arr)
        {
            StreamWriter sw = new StreamWriter("file.txt");
            for (int i = 0; i < x_arr.Length; i++)
            {
                sw.Write("{");
                sw.Write("{0,4:f2},", x_arr[i]);
                sw.Write("{0,-8:f5}", y_arr[i]);
                sw.Write("},");
            }
            sw.Close();
        }
        /// <summary>
        /// Заповнює масив x з заданим кроком
        /// </summary>
        /// <param name="from">початок проміжку</param>
        /// <param name="h">крок</param>
        /// <param name="to">кінець проміжку</param>
        /// <returns>масив x</returns>
        static double[] GenereteXarray(double from,double h,double to)
        {
            int n = (int)((to - from) / h) + 2;
            double[] x_arr = new double[n];
            x_arr[0] = from;
            for (int i = 1; i < n; i++)
            {
                x_arr[i] = x_arr[i - 1] + h;
            }
            return x_arr;
        }


        /// <summary>
        /// Лінійна апроксимація функції, заданої тчками
        /// </summary>
        /// <param name="in_x_arr">масив x координат вузлів</param>
        /// <param name="in_y_arr">масив у координат вузлів</param>
        /// <param name="out_x_arr"> масив х аргументів функції</param>
        /// <param name="out_y_arr">вихідний масив значень функції</param>
        static void LinearInterpolation(double[] in_x_arr, double[] in_y_arr, double[] out_x_arr, double[] out_y_arr)
        {
            double[] a = new double[in_x_arr.Length - 1];
            double[] b = new double[in_x_arr.Length - 1];
             
            for (int i = 0; i < in_x_arr.Length-1; i++)
            {
                a[i] = (in_y_arr[i + 1] - in_y_arr[i]) / (in_x_arr[i + 1] - in_x_arr[i]);
                b[i] = in_y_arr[i] - a[i] * in_x_arr[i];
            }

            int j = 0;
            for (int i = 0; i < out_x_arr.Length; i++)
            {
                for (j = 0; j < in_x_arr.Length-1; j++)
                {
                    if (out_x_arr[i] < in_x_arr[j + 1] && out_x_arr[i] > in_x_arr[j]) break;
                }
               
                out_y_arr[i] = a[j] * out_x_arr[i] + b[j];
            }
        }

        /// <summary>
        /// апроксимація функції методом Лагранжа
        /// </summary>
        /// <param name="in_x_arr">масив x координат вузлів</param>
        /// <param name="in_y_arr">масив у координат вузлів</param>
        /// <param name="out_x_arr">вихідний масив аргументів функції</param>
        /// <param name="out_y_arr">вихідний масив значень фукції</param>
        static void LagrangeInterpolation(double[] in_x_arr, double[] in_y_arr, double[] out_x_arr, double[] out_y_arr)
        {
            double l=1;
            for (int j = 0; j < out_x_arr.Length; j++)
            {
                out_y_arr[j] = 0;
                for (int i = 0; i < in_y_arr.Length; i++)
                {
                    for (int k = 0; k < in_y_arr.Length; k++)
                    {
                        if (k != i)
                            l *= (out_x_arr[j] - in_x_arr[k]) /( in_x_arr[i] - in_x_arr[k]);
                    }

                    out_y_arr[j] += in_y_arr[i] *l;
                    l = 1;
                }
            }
        }

        /// <summary>
        /// Обчислює значення першої татдругої поїхідної шляхом числового диференціювання
        /// </summary>
        /// <param name="out_x_arr">масив аргументів функції</param>
        /// <param name="in_y_arr">масив значень функції</param>
        /// <param name="first_diff_y">масив значень першої похідної</param>
        /// <param name="secon_diff_y">масив значень другої похідної</param>
        /// <param name="h">крок</param>
        static void Differentiate(double[] out_x_arr,double[] in_y_arr,double[] first_diff_y,double[] secon_diff_y,double h )
        {
            //для обислення похідної в крайніх точках використано формули правої та лівої похідної
            first_diff_y[0] = (in_y_arr[1] - in_y_arr[0]) / h;
            first_diff_y[out_x_arr.Length - 1] = (in_y_arr[out_x_arr.Length - 1] - in_y_arr[out_x_arr.Length - 2]) / h;

            for (int i = 1; i < out_x_arr.Length - 1; i++)
            {
                first_diff_y[i] = (in_y_arr[i + 1] - in_y_arr[i - 1]) / (2 * h);
            }

            //для обчислення другої похідної в крайніх точках використано формули правої та лівої похідної та значення першої похідної
            secon_diff_y[0] = (first_diff_y[1] - first_diff_y[0]) / h;
            secon_diff_y[out_x_arr.Length - 1] = (first_diff_y[out_x_arr.Length - 1] - first_diff_y[out_x_arr.Length - 2]) / h;

            for (int i = 1; i < out_x_arr.Length - 1; i++)
            {
                secon_diff_y[i] = (in_y_arr[i + 1] + in_y_arr[i - 1] - 2 * in_y_arr[i]) / (h * h);
            }
        }


        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            double from = -1.5; //початкова межа
            double to = 1.5;    //кінцева межа
            double h = 0.1f;     //крок x в таблиці
            int n = 9;          //кількість вузлів
  
            double[] x_array = { -1.6 , -1.1, -0.6, -0.2 , 0.2, 0.7, 0.93, 1.2, 1.51 };
            double[] y_array = { 1.48921, 0.71496, 0.87946, 1.07213, 1.07213, 0.81541, 0.70223, 0.78554, 1.29161 };

            Console.WriteLine("Вузли функцiї");
            PrintTable(x_array,y_array);

            double[] out_x_array = GenereteXarray(from,h,to);
            double[] true_y_array = new double[out_x_array.Length];

            for (int i = 0; i < out_x_array.Length; i++)
            {
                true_y_array[i] = Math.Log(Math.Pow(out_x_array[i], 4) - 2 * Math.Pow(out_x_array[i], 2) + 3);

            }
            Console.WriteLine("\nIстиннi значення функцiї");
            PrintTable(out_x_array,true_y_array);

            double[] lin_y_arr = new double[out_x_array.Length];//масив значень функції отриманих лінійною апроксимацією

            LinearInterpolation(x_array,y_array,out_x_array,lin_y_arr);

            Console.WriteLine("\nЗначення функцiї, отриманi лiнiйною апроксимацiєю");
            PrintTable(out_x_array,lin_y_arr);

            double[] lag_y_arr = new double[out_x_array.Length];//масив значень функції отриманих апроксимацією Лагранжа
            LagrangeInterpolation(x_array, y_array, out_x_array, lag_y_arr);

            Console.WriteLine("\nЗначення функцiї, отриманi апроксимацiєю Лагранжа");
            PrintTable(out_x_array, lag_y_arr);

           

            double[] true_first_diff_y = new double[out_x_array.Length];//масив істинних значень похідної функції
            for(int i = 0; i < out_x_array.Length; i++)
            {
                true_first_diff_y[i] = (-4 * out_x_array[i] + 4*Math.Pow(out_x_array[i], 3))/ (Math.Pow(out_x_array[i], 4) - 2 * Math.Pow(out_x_array[i], 2) + 3);
            }

            Console.WriteLine("\nІстинні значення першої похідної");
            PrintTable(out_x_array, true_first_diff_y);

            double[] true_second_diff_y = new double[out_x_array.Length];//масив істинних значень другої похідної функції
            for (int i=0;i<out_x_array.Length;i++)
            {
                true_second_diff_y[i] = 4 * (-Math.Pow(out_x_array[i], 6) + Math.Pow(out_x_array[i], 4)+ 7*Math.Pow(out_x_array[i], 2)-3)/Math.Pow(Math.Pow(out_x_array[i], 4)-2* Math.Pow(out_x_array[i], 2)+3,2);
            }

            Console.WriteLine("\nІстинні значення другої похідної");
            PrintTable(out_x_array, true_second_diff_y);

            double[] first_diff_y = new double[out_x_array.Length];//масив значень похідної функції отриманих числовим дифференціюванням
            double[] second_diff_y = new double[out_x_array.Length];//масив значень другої похідної функції отриманих числовим дифференціюванням

            Differentiate(out_x_array, lin_y_arr, first_diff_y, second_diff_y, h);

            Console.WriteLine("\nЗначення першої похідної");
            PrintTable(out_x_array, first_diff_y);

            Console.WriteLine("\nЗначення другої похідної");
            PrintTable(out_x_array, second_diff_y);

            PrintTableInFile(out_x_array, first_diff_y);
        }
    }
}
