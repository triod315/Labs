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


namespace Lab16_17__Part2_Flower
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int GCD(int a, int b)
        {
            while (b != 0)
                b = a % (a = b);
            return a;
        }

        private void PrintChart_Click(object sender, EventArgs e)
        {
            try
            {

                double h = 0.001;
                int n = int.Parse(nTextBox.Text);
                int d = int.Parse(dTextBox.Text);
                int gcd = GCD(n, d);
                n /= gcd;
                d /= gcd;
                double k = (double)n / d;
                double t;

                chart1.Series.Clear();
                Series ser = new Series();
                ser.ChartType = SeriesChartType.Polar;

                for (t = 0; t < d*2 * Math.PI; t += h)
                {
                    ser.Points.AddXY(t * 180 / Math.PI, Math.Sin(k*t));

                }
                chart1.Series.Add(ser);
                label1.Text = "number of petals: " + n;
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with program");
            }
            
        }
        
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutForm= new About();
            aboutForm.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                chart1.Series[0].XAxisType = AxisType.Primary;
                chart1.Series[0].YAxisType = AxisType.Primary;
            }
            else
            {
                chart1.Series[0].XAxisType = AxisType.Secondary;
                chart1.Series[0].YAxisType = AxisType.Secondary;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            PrintChart.Click += checkBox1_CheckedChanged;
        }

        void ValidateTextBox(TextBox textBox)
        {
            int d;
            if (int.TryParse(textBox.Text, out d))
            {
                errorProvider.SetError(textBox, "");
                textBox.BackColor = Color.White;
            }
            else
            {
                errorProvider.SetError(textBox, "Input correct number");
                textBox.BackColor = Color.Red;
            }
        }



        private void nTextBox_Validated(object sender, EventArgs e)
        {
            ValidateTextBox(nTextBox);
        }

        private void dTextBox_Validated(object sender, EventArgs e)
        {
            ValidateTextBox(dTextBox);
        }
    }
}
