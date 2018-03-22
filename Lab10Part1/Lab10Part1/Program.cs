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
        static string GetFileName()//get file name from console and check correct format
        {
            bool err_flag = false;
            string fname;
            do
            {
                Console.WriteLine("Input file name");
                fname = Console.ReadLine();
                for (int i = 0; i < fname.Length; i++)
                {
                    if (Path.GetInvalidFileNameChars().Contains(fname[i]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: file name contain illegal character: " + fname[i]);
                        Console.ResetColor();
                        err_flag = true;
                        continue;
                    }
                    err_flag = false;
                }
            } while (err_flag);
            return fname;
        }
        static void CreateFile(ref BinaryWriter binary_writer)//create and fill binary file
        {
            const string path = @"C:\Users\user\Desktop\";

            int n;//count of elements
            string file_name = path + GetFileName();
            binary_writer = new BinaryWriter(File.Open(file_name, FileMode.Create));
            do
            {
               Console.WriteLine("Write count of elements");
            }
            while (!int.TryParse(Console.ReadLine(), out n) && n > 1);//chek for correct format of n
            int temp_var;
            for (int i = 0; i < n; i++)
            {
                do
                {
                     Console.WriteLine("Введіть {0} елемент", i + 1);
                }
                while (!int.TryParse(Console.ReadLine(), out temp_var));//chek for correct format of temp_var
                binary_writer.Write(temp_var);
            }
            binary_writer.Close();
        }

        static void JustDoIt(ref BinaryReader binary_reader,string file_name)
        {
            int counter = 0;
            int sum = 0;
            int max_sum = -65535;
            int temp_elem;
            int prevois_elem = -65535;
            binary_reader = new BinaryReader(File.Open(file_name, FileMode.Open));
            while (binary_reader.BaseStream.Position != binary_reader.BaseStream.Length)
            {
                temp_elem = binary_reader.ReadInt32();
                counter++;
                if (temp_elem <= prevois_elem) { sum = 0; counter = 0; }
                sum += temp_elem;
                prevois_elem = temp_elem;
                if (sum > max_sum && counter > 1) max_sum = sum;
            }
            if (max_sum == -65535)
                Console.WriteLine("Growing sequence not found");
            else
                Console.WriteLine("Max sum=" + max_sum);
            binary_reader.Close();
        }

        static void Main(string[] args)
        {
            BinaryWriter bw = null;
            BinaryReader binary_reader = null;
            try
            {
                const string path = @"C:\Users\user\Desktop\";
                
                CreateFile(ref bw);
                
                string file_name;
                do
                {
                    file_name = path + GetFileName();
                } while (!File.Exists(file_name));
                JustDoIt(ref binary_reader, file_name);

            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
            }
            catch (OverflowException oe)
            {
                Console.WriteLine(oe.Message);
            }
            catch (Exception err)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("FATAL ERROR: " + err.Message);
                Console.ResetColor();
            }
            finally
            {
                binary_reader.Close();
                bw.Close();
            }
        }
    }
}
