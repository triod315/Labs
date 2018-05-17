using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab15
{
    public partial class Text_editor : Form
    {
        public Text_editor()
        {
            InitializeComponent();
        }

        async private void saveFileAsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    await streamWriter.WriteAsync(richTextBox1.Text);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            saveFileAsTextToolStripMenuItem.Enabled = true;
            saveFileAsHtmlToolStripMenuItem.Enabled = true;
        }


        async private void loadTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "txt file (*.txt)|*.txt|html file (*.html)|*.html",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            { 
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    richTextBox1.Text = await sr.ReadToEndAsync();
                }
                this.Text = openFileDialog.SafeFileName +" - "+ this.Text;
            }
        }

        async private void saveFileAsHtmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "html file(*.html)|*.html",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            string tmp_var = richTextBox1.Text;
            tmp_var=tmp_var.Replace("&", "&amp");
            tmp_var = tmp_var.Replace(" ", "&nbsp");
            tmp_var = tmp_var.Replace("<", "&lt");
            tmp_var = tmp_var.Replace(">", "&gt");
            tmp_var = tmp_var.Replace("\n", "<br />");
            tmp_var = tmp_var.Replace("\"","&quot");
        
            richTextBox1.Text = tmp_var;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    await sw.WriteAsync(tmp_var);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_form about_Form= new About_form();
            about_Form.Show();

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
