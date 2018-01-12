using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab10Part2
{
    class Program
    {
        static string GetFileName()//get file name from console and check correct format in future
        {
            string path= @"C:\Users\user\Desktop\";
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
            } while (err_flag || (Path.GetExtension(fname)!=".txt"));
            return path+fname;
        }
        static void Main(string[] args)
        {
            try
            {
                string input_file_name;
                do
                {
                    Console.Write("f1 ");
                    input_file_name = GetFileName();
                }
                while (!File.Exists(input_file_name));
                Console.Write("f2 ");
                string output_file_name = GetFileName();

                string input_file_content = "";
                string text_to_rewrite = "";
                Console.WriteLine("Write this word");
                string this_word = Console.ReadLine();

                StreamReader f1_reader = new StreamReader(input_file_name);
                StreamWriter f2_writer = new StreamWriter(output_file_name);
                try
                {
                    string s = "";

                    while ((s = f1_reader.ReadLine()) != null)
                    {
                        if (s.Split(' ')[0] == this_word)
                        {
                            input_file_content = text_to_rewrite + s;
                            break;
                        }
                        text_to_rewrite += s + "\r\n";
                    }
                    f2_writer.WriteLine(input_file_content);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                finally
                {
                    f1_reader.Close();
                    f2_writer.Close();
                }
            }
            catch (Exception er)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FATAL ERROR: "+er.Message);
                Console.ResetColor();
            }
        }
    }
}
