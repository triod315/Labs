using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg_Lab1_Var6_Part1_console
{
    class Program
    {

        static void PrintTable(double[] x_arr, double[] y_arr)
        {
            Console.WriteLine("\n_________________________________________________________________________");
            for (int i=0;i<x_arr.Length;i++)
            {
                Console.Write("{0,-8:f2}",x_arr[i]);
            }
            Console.WriteLine("\n-------------------------------------------------------------------------");
            for (int i = 0; i < y_arr.Length; i++)
            {
                Console.Write("{0,-8:f5}", y_arr[i]);
            }
            Console.WriteLine("\n_________________________________________________________________________");
        }

        static void LinearInterpolation(double[] in_x_arr, double[] in_y_arr, double[] out_x_arr, double[] out_y_arr, double from, double h)
        {
            out_x_arr[0] = from;
            int j = 0;

            double[] a = new double[in_x_arr.Length - 1];
            double[] b = new double[in_x_arr.Length - 1];

            for (int i = 0; i < in_x_arr.Length-1; i++)
            {
                a[i] = (in_y_arr[i + 1] - in_y_arr[i]) / (in_x_arr[i + 1] - in_x_arr[i]);
                b[i] = in_y_arr[i] - a[i] * in_x_arr[i];
            }

            for (int i = 0; i < out_x_arr.Length; i++)
            {
                if (i == 0)
                    out_x_arr[i] = from;
                else
                    out_x_arr[i] = out_x_arr[i - 1] + h;
                for (j = 0; j < in_x_arr.Length-1; j++)
                {
                    if (out_x_arr[i] < in_x_arr[j + 1] && out_x_arr[i] > in_x_arr[j]) break;
                }
               
                out_y_arr[i] = a[j] * out_x_arr[i] + b[j];
            }
        }

        static void Main(string[] args)
        {
            double from = -1.5; //початкова межа
            double to = 1.5;    //кінцева межа
            double h = 0.1f;     //крок x в таблиці
            int n = 9;          //кількість вузлів
            int new_n=(int)((to-from)/h)+2;        //кількість точок функції
  
            double[] x_array = { -1.6 , -1.1, -0.6, -0.2 , 0.2, 0.7, 0.93, 1.2, 1.51 };
            double[] y_array = { 1.48921, 0.71496, 0.87946, 1.07213, 1.07213, 0.81541, 0.70223, 0.78554, 1.29161 };

            PrintTable(x_array,y_array);

            double[] true_x_array = new double[new_n];
            double[] true_y_array = new double[new_n];

            Console.WriteLine(true_x_array.Length);

            true_x_array[0] = from;
            true_y_array[0] = Math.Log(Math.Pow(true_x_array[0], 4) - 2 * Math.Pow(true_x_array[0], 2) + 3);
            for (int i = 1; i < new_n; i++)
            {
                true_x_array[i] = true_x_array[i - 1] + h;
                true_y_array[i] = Math.Log(Math.Pow(true_x_array[i], 4) - 2 * Math.Pow(true_x_array[i], 2) + 3);

            }

            PrintTable(true_x_array,true_y_array);

            double[] lin_x_arr = new double[new_n];
            double[] lin_y_arr = new double[new_n];

            LinearInterpolation(x_array,y_array,lin_x_arr,lin_y_arr,from,h);

            PrintTable(lin_x_arr,lin_y_arr);
        }
    }
}
