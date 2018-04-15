using System;

namespace Alg_Lab5_Sorting
{
    class MainClass
    {
        /// <summary>
        /// Swap the specified a and b.
        /// </summary>
        /// <param name="a">first element</param>
        /// <param name="b">second element</param>
        /// <typeparam name="T">type of a and b</typeparam>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
        }

        public static int Partition(double[] a,int p,int q)
        {
            double x = a[p];//опорний елемент
            int i = p - 1;
            int j = q + 1;
            while (i < j)
            {
                do
                {
                    i++;
                } while(a[i] < x);
                do
                {
                    j--;
                } while(a[j] > x);
                if (i < j)
                {
                    //Console.WriteLine($"{i}<->{j}");
                    Swap<double>(ref a[i], ref a[j]);
                    PrintArray(a);
                }
            }
            return j;
        }

        /// <summary>
        /// Quick sorting of array
        /// </summary>
        /// <param name="a">array</param>
        /// <param name="p">begin of array</param>
        /// <param name="q">end of array</param>
        public static void QuickSort(double[] a,int p,int q)
        {
            if (p < q)
            {
                int r = Partition(a, p, q);
                QuickSort(a, p, r);
                QuickSort(a, r + 1, q);
            }
        }           

        /// <summary>
        /// Prints the array.
        /// </summary>
        /// <param name="a">array for output</param>
        public static void PrintArray(double[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write ("{0,6:f2}", a [i]);
            Console.WriteLine();
        }

        /// <summary>
        /// Gets the array from console.
        /// </summary>
        /// <returns>The array from console.</returns>
        public static double[] GetArrayFromConsole()
        {
            Console.WriteLine("Input elements of array in one line");
            string[] str = Console.ReadLine().Split(' ');

            double[] a = new double[str.Length];
            for (int i=0;i<str.Length;i++)
            {
                a[i] = Convert.ToDouble(str[i]);
            }
            return a;
        }

        /// <summary>
        /// Shell sort.
        /// </summary>
        /// <param name="a">array for sorting</param>
        public static void ShellSort(double[] a)
        {
            int d;
            for (int k=(int)Math.Log(a.Length+1,2); k>=0; k--)
            {
                d = (int)Math.Pow(2, k) -1;
                for (int i = d; i < a.Length; i++)
                    for (int j = i; j >= d && a[j - d] > a[j]; j -= d)
                    {
                        PrintArray(a);
                        Swap<double>(ref a[j], ref a[j - d]);
                    }
            }
        }

        public static void ChangeArr(double[] arr)
        {
            for (int i = 0; i < arr.Length / 2; i++)
                Swap<double>(ref arr[i],ref arr[arr.Length - i-1]);
        }

        /// <summary>
        /// Creates the new array.
        /// </summary>
        /// <returns>The new array.</returns>
        /// <param name="a">first array</param>
        /// <param name="b">second array</param>
        public static double[] CreateNewArray(double[] a,double[] b)
        {
            double[] c = new double[a.Length + b.Length];
            for (int i = 0; i < a.Length + b.Length; i++)
            {
                c[i] = (i < a.Length) ? a[i] : b[i - a.Length];
            }
            return c;
        }
            
        public static void Main (string[] args)
        {
            Console.WriteLine("A");
            double[] a = GetArrayFromConsole();
            Console.WriteLine("B");
            double[] b = GetArrayFromConsole();
            double[] c = CreateNewArray(a, b);
            bool is_uprising = ((a.Length > 1 && a[a.Length - 1] > a[0]) || (b.Length > 1 && b[b.Length - 1] > b[0]));
            double[] c_clone=new double[c.Length];
            Array.Copy(c, c_clone, c.Length);

            Console.WriteLine("Quick sort");
            QuickSort (c,0,c.Length-1);
            Console.WriteLine("---------------------");
            Console.WriteLine("Shell sort");
            ShellSort(c_clone);
            Console.WriteLine("---------------------");

            if (is_uprising)
            {
                ChangeArr(c);
                ChangeArr(c_clone);
            }

            Console.WriteLine("-------------\nQuick sort result");
            PrintArray(c);
            Console.WriteLine("-------------\nShell sort result");
            PrintArray(c_clone);
