using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab15
{
    public partial class About_form : Form
    {
        public About_form()
        {
            InitializeComponent();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/triod315/Labs/tree/master/Lab15");
        }

        private void About_form_Load(object sender, EventArgs e)
        {
            productNameLabel.Text += Application.ProductName;
            productVersionLabel.Text += Application.ProductVersion;
            productDeveloperLabel.Text += Application.CompanyName;
        }
    }
}
