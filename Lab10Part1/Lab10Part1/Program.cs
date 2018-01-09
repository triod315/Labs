using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab10Part1
{
    class Program
    {
        static string GetFileName()//get file name from console and check correct format in future
        {
            string fname = Console.ReadLine();
            return fname;
        }
        static void CreateFile()//create and fill binary file
        {
            int n;//count of elements
            string file_name = GetFileName();
            BinaryWriter binary_writer = new BinaryWriter(File.Open(file_name, FileMode.Create));
            do
            {
                Console.WriteLine("Write count of elements");
            }
            while (!int.TryParse(Console.ReadLine(), out n));//chek for correct format of n
            int temp_var;
            for (int i = 0; i < n; i++)
            {
                do
                {
                    Console.WriteLine("Введіть {0} елемент", i+1);
                }
                while (!int.TryParse(Console.ReadLine(), out temp_var));//chek for correct format of temp_var
                binary_writer.Write(temp_var);
            }
            binary_writer.Close();
        }

        static void Main(string[] args)
        {
            CreateFile();
            int sum = 0;
            int max_sum = -65535;
            int temp_elem;
            int prevois_elem=-65535;
            string file_name = GetFileName();
            BinaryReader bianry_reader = new BinaryReader(File.Open(file_name, FileMode.Open));
            
            while (bianry_reader.BaseStream.Position != bianry_reader.BaseStream.Length)
            {
                temp_elem = bianry_reader.ReadInt32();
                if (temp_elem > prevois_elem) sum += temp_elem;
                else
                {
                    if (sum > max_sum) max_sum = sum;
                    sum = 0;
                    sum += temp_elem;
                }
                prevois_elem = temp_elem;
                if(bianry_reader.BaseStream.Position == bianry_reader.BaseStream.Length) if (sum > max_sum) max_sum = sum;
            }
            Console.WriteLine("Max sum="+max_sum);
            bianry_reader.Close();
        }
    }
}
