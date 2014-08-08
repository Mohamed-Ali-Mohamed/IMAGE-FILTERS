using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMAGE_FILTERS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;
        string OpenedFilePath;
        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            if (OpenedFilePath == null)
                return;
            ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
            string Text = textBox1.Text;
            string Text_Sort = comboBox1.Text;
            string Text_Filter = comboBox2.Text;
            if (Text_Sort.Length == 0)
                Text_Sort = "0";
            int Sort = Text_Sort[0] - '0';
            if (Text_Filter.Length == 0)
                Text_Filter = "1";
            int Filter = Text_Filter[0] - '0';
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] >= '0' && Text[i] <= '9')
                    continue;
                else
                    return;
            }
            if (Text.Length == 0)
                return;
            int Max_Size = int.Parse(Text);
            int Start=System.Environment.TickCount;
            ImageOperations.ImageFilter(ImageMatrix, Max_Size, Sort, Filter);
            int End = System.Environment.TickCount;
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
            double Time=End-Start;
            Time /= 1000;
            label3.Text = (Time).ToString();
            label3.Text += " s";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            string Text = textBox1.Text;
            string Text_Sort = comboBox1.Text;
            string Text_Filter = comboBox2.Text;
            if (Text_Sort.Length == 0)
                Text_Sort = "0";
            int Sort = Text_Sort[0] - '0';
            if (Text_Filter.Length == 0)
                Text_Filter = "1";
            int Filter = Text_Filter[0] - '0';
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] >= '0' && Text[i] <= '9')
                    continue;
                else
                    return;
            }
            if (Text.Length == 0)
                return;
            int Max_Size = int.Parse(Text);
            int Start = System.Environment.TickCount;
            ImageOperations.ImageFilter(ImageMatrix, Max_Size, Sort, Filter);
            int End = System.Environment.TickCount;
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
            double Time = End - Start;
            Time /= 1000;
            label3.Text = (Time).ToString();
            label3.Text += " s";
        }
        
    }
}
