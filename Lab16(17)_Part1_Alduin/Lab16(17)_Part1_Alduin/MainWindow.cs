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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        enum Direction {Right, Left}

        private void PaintDragon(Graphics gr, int level,Direction turn, PointF startPoint, float dx,float dy)
        {
            if (level <= 0)
            {
                gr.DrawLine(Pens.Green,startPoint.X,startPoint.Y, startPoint.X+dx,startPoint.Y+dy);
            }
            else
            {
                float nx = (float)(dx / 2);
                float ny = (float)(dy / 2);
                float dx2 = -ny + nx;
                float dy2 = nx + ny;

                if (turn == Direction.Right)
                {
                    PaintDragon(gr, level - 1, Direction.Right, startPoint, dx2, dy2);
                    PointF endPoint = new PointF()
                    {
                        X = startPoint.X + dx2,
                        Y = startPoint.Y + dy2
                    };
                    PaintDragon(gr, level - 1, Direction.Left, endPoint, dy2, -dx2);
                }
                else
                {
                    PaintDragon(gr, level - 1, Direction.Right, startPoint, dy2, -dx2);
                    PointF endPoint = new PointF()
                    {
                        X = startPoint.X + dy2,
                        Y = startPoint.Y - dx2
                    };
                    PaintDragon(gr, level - 1, Direction.Left, endPoint, dx2, dy2);
                }
            }

        }



        private int dragon_level = 0;

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            e.Graphics.Clear(pictureBox.BackColor);

            float dx = Math.Min(pictureBox.ClientSize.Width / 1.5f, pictureBox.ClientSize.Height) / 3f;
            PointF startPoint = new PointF()
            {
               X = (pictureBox.ClientSize.Width - dx * 1.5f) / 2f + dx / 3f,
               Y = (pictureBox.ClientSize.Height - dx) / 2f + dx / 3f
            };

            int level = dragon_level;//nudLevel.Value;
            PaintDragon(e.Graphics, level, Direction.Right, startPoint, 2 * dx, 0f);
            Cursor = Cursors.Default;
        }


        private void pictureBox_Resize(object sender, EventArgs e)
        {
            pictureBox.Refresh();
        }

        Image dragon;

        private void levelPlus_Click(object sender, EventArgs e)
        {
            if (dragon_level < 20)
            {
                dragon_level++;
                pictureBox.Refresh();
                dragonLevelLabel.Text ="Level: "+ dragon_level;
            }
            else MessageBox.Show("dragon level > 20", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void levelMinus_Click(object sender, EventArgs e)
        {
            if (dragon_level > 0)
            {
                dragon_level--;
                pictureBox.Refresh();
                dragonLevelLabel.Text = "Level: " + dragon_level;
            } else MessageBox.Show("dragon level <= 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
