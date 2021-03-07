using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture_view
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            roundedPanel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            roundedPanel2.BackColor = Color.FromArgb(120, 0, 0, 0);
        }
        private void exiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void file_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"E:\";
            openFileDialog1.Title = "Files";

            openFileDialog1.Multiselect = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog1.FilterIndex = 2;
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    pathes.Add(file);
                    string name = "";
                    for(var i=file.Length-1;i>=0;i--)
                    {
                        if(file[i] =='\\')
                        {
                            break;
                        }
                        name = file[i] + name;

                    }
                    listBox1.Items.Add(name);
                }
            }
        }
        private void selectOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.SelectionMode = SelectionMode.One;
        }
        private void selectMultiyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.SelectionMode = SelectionMode.MultiSimple;
        }
        private void slideShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            count = 0;
            roundedPanel2.Controls.Clear();
            listBox1.SelectionMode = SelectionMode.None;
            timer1.Interval = 1000;
            timer1.Start();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count == pathes.Count)
            {
                roundedPanel2.Controls.Clear();
                label1.Text = "";
                timer1.Stop();
                return;
            }
            var path = pathes[count++].ToString();
            picture.Image = Image.FromFile(path);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            roundedPanel2.Controls.Add(picture);
            picture.Dock = DockStyle.Fill;
            label1.Text = path;
            
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            roundedPanel2.Controls.Clear();
            label1.Text = "";
            picture = new PictureBox();
            if(listBox1.SelectionMode == SelectionMode.One)
            {
                string path = pathes[listBox1.SelectedIndex].ToString();
                picture.Image = Image.FromFile(path);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                roundedPanel2.Controls.Add(picture);
                picture.Dock = DockStyle.Fill;
                label1.Text = path;
            }
            else
            {
                int pos_x = 18, pos_y = 20;
               for(int i=0;i<listBox1.SelectedIndices.Count;i++)
                {
                    int ind = listBox1.SelectedIndices[i];
                    string path = pathes[ind].ToString();
                    picture = new PictureBox();
                    picture.Image = Image.FromFile(path);
                    picture.Location = new Point(pos_x, pos_y);
                    picture.Size = new Size(150, 100);
                    picture.SizeMode = PictureBoxSizeMode.StretchImage;
                    roundedPanel2.Controls.Add(picture);
                    pos_x += 165;
                    if(pos_x+300 >= 694)
                    {
                        pos_x = 18;
                        pos_y += 120;
                    }
                }
            }
        }
        private ArrayList pathes = new ArrayList();
        PictureBox picture = new PictureBox();
        private int count = 0;
    }
}
