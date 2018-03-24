﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg_lab4_Var6
{
    class Program
    {

        static void PrintSolution(double[] arr)
        {
            Console.WriteLine("Solution");
            for (int i = 0; i < arr.Length; i++) Console.Write("{0,8:F4}", arr[i]);
            Console.WriteLine();
        }

        /// <summary>
        /// метод перевіряє збіжність ітерацій
        /// </summary>
        /// <param name="matrix">матриця коефіцієнтів</param>
        /// <returns>true якщо збіжність присутня</returns>
        static bool ChekMatrix(double[,] matrix)
        {
            double sum;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                sum=0;
                for (int j = 0; j < matrix.GetLength(0); j++) sum += Math.Abs(matrix[i, j]);
                if (sum >= 1) return false;
            }
            return true;
        }

        /// <summary>
        /// Метод обчислює наближений розв'язок системи лінійних рівнянь 
        /// </summary>
        /// <param name="A">матриця коефіцієнтів рівнянь</param>
        /// <param name="B">матриця вільних членів</param>
        /// <param name="e">необхідна точність</param>
        /// <returns>масив коренів рівняння</returns>
        static double[] Find_Solution(double[,] A, double[] B,double e)
        {
            double[,] a = new double[B.Length, B.Length];
            double[] b = new double[B.Length];
            for (int i = 0; i < B.Length; i++)
            {
                for (int j = 0; j < B.Length; j++)
                {
                    a[i, j] = (i == j) ? 0 : (-A[i, j] / A[i, i]);
                }
                b[i] = B[i] / A[i, i];
            }

            if (!ChekMatrix(a))
            {
                Console.WriteLine("I'm sorry, I'm too stupid to solve this");
                return null;
            }

            double[] x = new double[b.Length];
            double[] x0 = new double[b.Length];
            Array.Copy(b, x0, b.Length);
            int k = 0;//кількість ітерацій
            double max=0;//максимальне по модулю значення різниці k-го та k+1-го наближень
            do
            {
                for (int i = 0; i < b.Length; i++)
                {
                    max = 0;
                    x[i] = 0;
                    for (int j = 0; j < b.Length; j++)
                    {
                        x[i] += a[i, j] * x0[j];
                    }
                    x[i] += b[i];

                    if (Math.Abs(x[i] - x0[i]) > max) max = Math.Abs(x[i] - x0[i]);
                    x0[i] = x[i];
                }
                k++;
            } while (max > e);
            Console.WriteLine("count of iteration="+k);
            PrintSolution(x);
            return x;
        }

        static void Main(string[] args)
        {
            double[,] A = {
                            {0.53, 0.011,0.035,-0.09},
                            {-0.073, 0.58,0,0 },
                            {0.154, -0.12,0.42,-0.03},
                            {0,-0.061,0.05,0.32 }
                            };
            double[] B = { 1.39, 1.74, -2.05, -1.73 };
            double e = 0.0001;
            double[] x = Find_Solution(A, B, e);
        }
    }
}
