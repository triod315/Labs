using System;
using System.Collections.Generic;

namespace Alg_Lab6_Binary_Search
{
    class MainClass
    {

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
        /// Prints the array.
        /// </summary>
        /// <param name="a">array for output</param>
        public static void PrintArray(double[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write ("{0,6:f2}", a [i]);
            Console.WriteLine();
        }

        public static int BinarySearch(double[] arr,double el)
        {
            int start_pos = 0;
            int end_pos = arr.Length-1;
            int mid_pos;
            while (start_pos <= end_pos)
            {       
                mid_pos=(end_pos+start_pos) / 2;
                if (el < arr[mid_pos])
                    end_pos = mid_pos-1;
                else
                    if (el > arr[mid_pos])
                       start_pos = mid_pos + 1;
                    else
                       return mid_pos;                
            }
            return -1;
        }

        public static void SwapArray(ref double[] a,ref double[] b)
        {
            double[] c = a;
            a = b;
            b = c;
        }

        public static int LinearSearch(double[] a, double el)
        {
            for (int i = 0; i < a.Length; i++)
                if (a[i] == el)
                    return i;
            return -1;
        }

        public static void DDDDoIt(double[] a,double[] b)
        {
            List<double> result = new List<double>();
            List<double> restricted_elements = new List<double>();
                        for (int i=0;i<a.Length;i++)
            {
                if (BinarySearch(a, a[i]) != i && !restricted_elements.Contains(a[i]))
                {
                    restricted_elements.Add(a[i]);
                }
            }
            for (int i=0;i<b.Length;i++)
            {
                if (BinarySearch(b, b[i]) != i && !restricted_elements.Contains(b[i]))
                {
                    restricted_elements.Add(b[i]);
                }
            }
            if (b.Length > a.Length)
                SwapArray(ref a,ref b);
            for (int i = 0; i < b.Length; i++)
            {
                if (BinarySearch(a, b[i]) == -1 && !restricted_elements.Contains(b[i]))
                    result.Add(b[i]);
            }
            for (int i = 0; i < a.Length; i++)
                if (!restricted_elements.Contains(a[i]))
                    result.Add(a[i]);
            Console.WriteLine("Result: ");
            for (int i = 0; i < result.Count; i++)
                Console.Write(result[i]+" ");
        }

        public static void Main(string[] args)
        {
            double[] a = GetArrayFromConsole();
            double[] b = GetArrayFromConsole();
            Array.Sort(a);
            Array.Sort(b);
            DDDDoIt(a, b);
        }
    }
}
