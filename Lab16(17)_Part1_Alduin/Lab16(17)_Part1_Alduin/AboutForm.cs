using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab16_17__Part1_Alduin
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            productNameLabel.Text += Application.ProductName;
            productVersionLabel.Text += Application.ProductVersion;
            productDeveloperLabel.Text += Application.CompanyName;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/triod315/Labs/tree/master/Lab15");
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
