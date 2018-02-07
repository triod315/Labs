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

        static void Main(string[] args)
        {
            int n = 9;//кількість вузлів
            double[] x_array = { -1.6 , -1.1, -0.6, -0.2 , 0.2, 0.7, 0.93, 1.2, 1.51 };
            double[] y_array = { 1.48921, 0.71496, 0.87946, 1.07213, 1.07213, 0.81541, 0.70223, 0.78554, 1.29161 };
            PrintTable(x_array,y_array);
        }
    }
}
