using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Alg_Lab1Part1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static double[] GenereteXarray(double from, double h, double to)
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

        static void ShowChart(Chart chart, double[] in_x_arr, double[] in_y_arr)
        {
            Series ser = new Series();
            ser.ChartType = SeriesChartType.Point;
            for (int i = 0; i < in_x_arr.Length; i++)
            {
                ser.Points.AddXY(in_x_arr[i], in_y_arr[i]);
            }
            chart.Series.Add(ser);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            double from = -1.5; //початкова межа
            double to = 1.5;    //кінцева межа
            double h = 0.1;     //крок x в таблиці
            int n = 9;          //кількість вузлів

            double[] x_array = { -1.6, -1.1, -0.6, -0.2, 0.2, 0.7, 0.93, 1.2, 1.51 };
            double[] y_array = { 1.48921, 0.71496, 0.87946, 1.07213, 1.07213, 0.81541, 0.70223, 0.78554, 1.29161 };

            Console.WriteLine("Вузли функцiї");

            double[] out_x_array = GenereteXarray(from, h, to);
           /* double[] true_y_array = new double[out_x_array.Length];

            for (int i = 0; i < out_x_array.Length; i++)
            {
                true_y_array[i] = Math.Log(Math.Pow(out_x_array[i], 4) - 2 * Math.Pow(out_x_array[i], 2) + 3);
                
            }*/
            ShowChart(chart1,x_array,y_array);


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
