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
    public partial class Haar : Form
    {
        string filePath;
        double bottomThreshold;
        double upperThreshold;
        int waveletLength;
        Bitmap bmp;
        public Haar(string filePath, int waveletLength, double bottomThreshold, double upperThreshold)
        {
            InitializeComponent();
            this.filePath = filePath;
            this.waveletLength = waveletLength;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
            pictureBox1.Image = Image.FromFile(filePath);
        }

        public Haar(int waveletLength, double bottomThreshold, double upperThreshold, Bitmap bmp = null, double[,] imageArray = null)// перегружаем метод, чтобы принять на вход изображение, а не ссылку на него
        {
            InitializeComponent();
            this.bmp = bmp;
            this.waveletLength = waveletLength;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
            pictureBox1.Image = bmp;

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

                imageArray = Form1.HaarWavelet(imageArray, waveletLength);
                Bitmap bmp2 = new Bitmap(imageArray.GetLength(1), imageArray.GetLength(0));
                for (int i = 0; i < imageArray.GetLength(0); i++)
                    for (int j = 0; j < imageArray.GetLength(1); j++)
                    {
                        bmp2.SetPixel(j, i, Color.FromArgb((int)imageArray[i, j], (int)imageArray[i, j], (int)imageArray[i, j]));
                    }
                pictureBox2.Image = bmp2;

                Bitmap bmpFromImageArray = Form1.Non_Maximum_Suppression(bmp = null, imageArray);
                bmpFromImageArray = Form1.Double_Threshold(bmpFromImageArray, bottomThreshold, upperThreshold);
                pictureBox3.Image = bmpFromImageArray;
                //Bitmap c = new Bitmap(b);
                pictureBox4.Image = Form1.Hysteresis_Thresholding(bmpFromImageArray, "Haar");
                //Bitmap a = new Bitmap(pictureBox1.Image);
                //Form1.GrayScale(a);
                //Form1.HaarWavelet(a, waveletLength);
                //pictureBox2.Image = a;
                //Bitmap b = new Bitmap(a);
                //Form1.Non_Maximum_Suppression(b);
                //pictureBox3.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
                //Bitmap c = new Bitmap(b);
                //pictureBox4.Image = Form1.Hysteresis_Thresholding(c, "Haar");
            }
        }
        private void Haar_Load(object sender, EventArgs e)
        {
            //Bitmap a = new Bitmap(pictureBox1.Image);
            //Form1.GrayScale(a);
            //Form1.HaarWavelet(a, waveletLength);
            //pictureBox2.Image = a;
            //Bitmap b = new Bitmap(a);
            //Form1.Non_Maximum_Suppression(b);
            //pictureBox3.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
            //Bitmap c = new Bitmap(b);
            //pictureBox4.Image = Form1.Hysteresis_Thresholding(c, "Haar");
        }
    }
}
