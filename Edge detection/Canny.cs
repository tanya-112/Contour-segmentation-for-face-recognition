using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edge_detection
{
    public partial class Canny : Form
    {
        string filePath;
        double sigma;
        short k;
        double bottomThreshold;
        double upperThreshold;
        Bitmap bmp;
        public Bitmap cannyResult;



        public Canny(double sigma, short k, double bottomThreshold, double upperThreshold, Bitmap bmp = null, double[,] imageArray = null)// перегружаем метод Canny, чтобы принять на вход изображение, а не ссылку на него
        {
            InitializeComponent();
            this.bmp = bmp;
            this.sigma = sigma;
            this.k = k;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
            if (imageArray != null)
            {
                Bitmap bmp1 = new Bitmap(imageArray.GetLength(1), imageArray.GetLength(0));
                for (int i = 0; i < imageArray.GetLength(0); i++)
                    for (int j = 0; j < imageArray.GetLength(1); j++)
                    {
                        bmp1.SetPixel(j, i, Color.FromArgb((int)imageArray[i, j], (int)imageArray[i, j], (int)imageArray[i, j]));
                    }
                pictureBox1.Image = bmp1;
                //imageArray = Form1.GrayScale(imageArray);


                imageArray = Form1.Gaussian_Blur(imageArray,sigma, k);

                imageArray = Form1.Sobel(imageArray);
                Bitmap bmp2 = new Bitmap(imageArray.GetLength(1), imageArray.GetLength(0));
                for (int i = 0; i < imageArray.GetLength(0); i++)
                    for (int j = 0; j < imageArray.GetLength(1); j++)
                    {
                        bmp2.SetPixel(j, i, Color.FromArgb((int)imageArray[i, j], (int)imageArray[i, j], (int)imageArray[i, j]));
                    }
                pictureBox1.Image = bmp2;

                Bitmap bmpFromImageArray = Form1.Non_Maximum_Suppression(bmp = null, imageArray);
                bmpFromImageArray = Form1.Double_Threshold(bmpFromImageArray, bottomThreshold, upperThreshold);
                pictureBox3.Image = bmpFromImageArray;
                //Bitmap c = new Bitmap(b);
               cannyResult = Form1.Hysteresis_Thresholding(bmpFromImageArray, "Canny");
                pictureBox4.Image = cannyResult;
            }
            else
            {
                pictureBox1.Image = bmp;

                Bitmap a = new Bitmap(pictureBox1.Image);

                Form1.GrayScale(a);
                Form1.Gaussian_Blur(bmp,sigma, k);
                Form1.Sobel(a);
                pictureBox2.Image = a;
                Bitmap b = new Bitmap(a);
                Form1.Non_Maximum_Suppression(b);
                pictureBox3.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
                Bitmap c = new Bitmap(b);
                pictureBox4.Image = Form1.Hysteresis_Thresholding(c, "Canny");
            }
        }

        public Canny(string filePath, double sigma, short k, double bottomThreshold, double upperThreshold)// получаем из основной формы путь к выбранному файлу
        {
            InitializeComponent();
            this.filePath = filePath;
            this.sigma = sigma;
            this.k = k;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
            pictureBox1.Image = Image.FromFile(filePath);
        }


        private void Canny_Load(object sender, EventArgs e)
        {
            Bitmap a = new Bitmap(pictureBox1.Image);

            Form1.GrayScale(a);
            Form1.Gaussian_Blur(a, sigma, k);
            Form1.Sobel(a);
            pictureBox2.Image = a;
            Bitmap b = new Bitmap(a);
            Form1.Non_Maximum_Suppression(b);
            pictureBox3.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
            Bitmap c = new Bitmap(b);
            pictureBox4.Image = Form1.Hysteresis_Thresholding(c, "Canny");
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

    }
}
