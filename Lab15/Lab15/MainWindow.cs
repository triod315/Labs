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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SaveTextToFileAsync(string text, SaveFileDialog saveFileDialog)
        {            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    await streamWriter.WriteAsync(text);
                }
            }
        }

        /// <summary>
        /// save text to file *.txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileAsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FieldForm activeForm = (FieldForm)ActiveMdiChild;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            SaveTextToFileAsync(activeForm.textField.Text, saveFileDialog);
            
            activeForm.Text = (saveFileDialog.FileName!="") ? saveFileDialog.FileName:activeForm.Text;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        ///


        /// <summary>
        /// Load text from file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "txt file (*.txt)|*.txt|html file (*.html)|*.html",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FieldForm activeForm = new FieldForm();
                activeForm.MdiParent = this;
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    activeForm.textField.Text = await sr.ReadToEndAsync();
                }
                activeForm.Text = openFileDialog.SafeFileName;
                activeForm.Show();
                
            }
        }

        /// <summary>
        /// Save file in html format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileAsHtmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FieldForm activeForm = (FieldForm)ActiveMdiChild;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "html file(*.html)|*.html",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            string tmp_var = activeForm.textField.Text; //html code of text in richTextBox1
            tmp_var=tmp_var.Replace("&", "&amp;");
            tmp_var = tmp_var.Replace(" ", "&nbsp;");
            tmp_var = tmp_var.Replace("<", "&lt;");
            tmp_var = tmp_var.Replace(">", "&gt;");
            tmp_var = tmp_var.Replace("\n", "<br />");
            tmp_var = tmp_var.Replace("\"","&quot;");

            SaveTextToFileAsync(tmp_var, saveFileDialog);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_form about_Form= new About_form();
            about_Form.Show();

        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void NewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FieldForm fieldForm = new FieldForm
            {
                MdiParent = this
            };
            fieldForm.Show();
            
            fieldForm.Text = "unnamed file";
        }

        private void Text_editor_MdiChildActivate(object sender, EventArgs e)
        {
            Form activeFieldForm = ActiveMdiChild;
            if (activeFieldForm != null)
            {
                saveFileAsTextToolStripMenuItem.Enabled = true;
                saveFileAsHtmlToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveFileAsTextToolStripMenuItem.Enabled = false;
                saveFileAsHtmlToolStripMenuItem.Enabled = false;
            }
        }

        private void ViewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About_form about_Form = new About_form();
            about_Form.Show();
        }

    }
}
