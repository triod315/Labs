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

        double[] x_array = { -1.6, -1.1, -0.6, -0.2, 0.2, 0.7, 0.93, 1.2, 1.51 };
        double[] y_array = { 1.48921, 0.71496, 0.87946, 1.07213, 1.07213, 0.81541, 0.70223, 0.78554, 1.29161 };

        /// <summary>
        /// Заповнює масив x з заданим кроком
        /// </summary>
        /// <param name="from">початок проміжку</param>
        /// <param name="h">крок</param>
        /// <param name="to">кінець проміжку</param>
        /// <returns>масив x</returns>
        static double[] GenereteXarray(double from, double h, double to)
        {
            int n = (int)((to - from) / h) + 1;
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

            for (int i = 0; i < in_x_arr.Length - 1; i++)
            {
                a[i] = (in_y_arr[i + 1] - in_y_arr[i]) / (in_x_arr[i + 1] - in_x_arr[i]);
                b[i] = in_y_arr[i] - a[i] * in_x_arr[i];
            }

            int j = 0;
            for (int i = 0; i < out_x_arr.Length; i++)
            {
                for (j = 0; j < in_x_arr.Length - 1; j++)
                {
                    if (out_x_arr[i] < in_x_arr[j + 1] && out_x_arr[i] > in_x_arr[j]) break;
                }
                if(j!=in_x_arr.Length)
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
            double l = 1;
            for (int j = 0; j < out_x_arr.Length; j++)
            {
                out_y_arr[j] = 0;
                for (int i = 0; i < in_y_arr.Length; i++)
                {
                    for (int k = 0; k < in_y_arr.Length; k++)
                    {
                        if (k != i)
                            l *= (out_x_arr[j] - in_x_arr[k]) / (in_x_arr[i] - in_x_arr[k]);
                    }

                    out_y_arr[j] += in_y_arr[i] * l;
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
        static void Differentiate(double[] out_x_arr, double[] in_y_arr, double[] first_diff_y, double[] secon_diff_y, double h)
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


        static void ShowChart(Chart chart, double[] in_x_arr, double[] in_y_arr)
        {
            chart.Series.Clear();
            Series ser = new Series();
            ser.ChartType = SeriesChartType.Point;
            for (int i = 0; i < in_x_arr.Length; i++)
            {
                ser.Points.AddXY(in_x_arr[i], in_y_arr[i]);
            }
            chart.Series.Add(ser);
        }

        static void DisplayTable(DataGridView table,double[] x_arr, double[] y_arr)
        {
            table.Rows.Clear();
            table.ColumnCount = x_arr.Length;
            for (int i = 0; i< x_arr.Length;i++)
            {
                table[i, 0].Value = x_arr[i];
                table[i, 1].Value = y_arr[i];
            }
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            double from = -1.5; //початкова межа
            double to = 1.5;    //кінцева межа
            double h;     //крок x в таблиці
            do { } while (!double.TryParse(textBox1.Text, out h));
            int n = 9;          //кількість вузлів

            

            Console.WriteLine("Вузли функцiї");

            double[] out_x_array = GenereteXarray(from, h, to);
           /* double[] true_y_array = new double[out_x_array.Length];

            for (int i = 0; i < out_x_array.Length; i++)
            {
                true_y_array[i] = Math.Log(Math.Pow(out_x_array[i], 4) - 2 * Math.Pow(out_x_array[i], 2) + 3);
                
            }*/
            ShowChart(chart1,x_array,y_array);



            double[] out_y_arr = new double[out_x_array.Length];//масив значень функції отриманих апроксимацією

            if (comboBox1.SelectedIndex == 0)
            {
                LinearInterpolation(x_array, y_array, out_x_array, out_y_arr);
            }
            else
            {
                LagrangeInterpolation(x_array, y_array, out_x_array, out_y_arr);
            }

            ShowChart(chart2, out_x_array, out_y_arr);

            double[] first_diff_y = new double[out_x_array.Length];//масив значень похідної функції отриманих числовим дифференціюванням
            double[] second_diff_y = new double[out_x_array.Length];//масив значень другої похідної функції отриманих числовим дифференціюванням

            Differentiate(out_x_array, out_y_arr, first_diff_y, second_diff_y, h);
            ShowChart(chart3,out_x_array,first_diff_y);
            ShowChart(chart4, out_x_array, second_diff_y);

        }

        private void GetPoints()
        {
            int n; 

            Form n_input = new Form();
            Label inf_txt = new Label();
            TextBox n_string = new TextBox();

 
            n_input.Height = 150;
            inf_txt.Text = "Input count of points";
            inf_txt.Width = 120;
            inf_txt.TextAlign = ContentAlignment.MiddleCenter;
            inf_txt.Location = new Point(n_input.Width/2-inf_txt.Width/2,10);
            n_string.Location = new Point(n_input.Width / 2 - n_string.Width / 2, inf_txt.Height+10);
            n_input.FormBorderStyle = FormBorderStyle.FixedDialog;
            n_input.Controls.Add(inf_txt);
            n_input.Controls.Add(n_string);
            n_input.MinimizeBox = false;
            n_input.MaximizeBox = false;
            Button btn = new Button();
            btn.Location = new Point(n_input.Width / 2 - btn.Width / 2, inf_txt.Height+n_string.Height + 15);
            btn.Text = "OK";
            void Ok_clik(object sender, EventArgs e)
            {
                try
                {
                    n = int.Parse(n_string.Text);
                    n_input.Close();
                    GetTable(n);
                } catch (Exception) { }
            }
            btn.Click += new EventHandler(Ok_clik);
            n_input.AcceptButton = btn;
            n_input.Controls.Add(btn);
            n_input.StartPosition = FormStartPosition.CenterScreen;

            n_input.ShowDialog();

        }

        private void GetTable(int n)
        {
            Form table_form = new Form();
            DataGridView table = new DataGridView();
            table.RowCount = 3;
            table.ColumnCount = n;
            table.AllowUserToAddRows = false;
            table_form.Width = 1200;
            table.Width = 1180;
            table.Height = 50;
            table.ColumnHeadersVisible = false;

            table.RowHeadersWidth = 50;

            table.Rows[0].HeaderCell.Value = "f(x)";
            table.Rows[1].HeaderCell.Value = "x";

            table_form.Height = 150;
            table_form.StartPosition = FormStartPosition.CenterScreen;
            table_form.MinimizeBox = false;
            table_form.MaximizeBox = false;
            table_form.FormBorderStyle = FormBorderStyle.FixedDialog;
            table.ScrollBars = ScrollBars.Horizontal;

            void Ok_pressed(object sender, EventArgs e)
            {
                x_array = new double[n];
                y_array = new double[n];
                for (int i = 0; i < n; i++)
                {
                    x_array[i] = Convert.ToDouble(table[i, 0].Value);
                    y_array[i] = Convert.ToDouble(table[i, 1].Value);
                }
                table_form.Close();
            }

            Button Ok = new Button();
            Ok.Text="Ok";
            Ok.Location = new Point(table_form.Width/2-Ok.Width/2,table.Height+20);
            Ok.Click += new EventHandler(Ok_pressed);



            table_form.Controls.Add(Ok);
            table_form.Controls.Add(table);

            table_form.ShowDialog();


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void inputTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPoints();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetPoints();
        }
    }
}
