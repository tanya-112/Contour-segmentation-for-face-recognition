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
    public partial class CannyForm : Form
    {
        string filePath;
        double sigma;
        double bottomThreshold;
        double upperThreshold;
        Bitmap bmp;
        public Bitmap cannyResult;



        public CannyForm(double sigma, double bottomThreshold, double upperThreshold, Bitmap bmp = null, decimal[,] imageArray = null)// перегружаем метод, чтобы принять на вход изображение, а не ссылку на него
        {
            InitializeComponent();
            this.bmp = bmp;
            this.sigma = sigma;
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

                decimal[,] imageArrayNew = CannyStandardVersion.GradUsingFirstDerOfGaus(imageArray, sigma);

                Bitmap bmp2 = new Bitmap(imageArrayNew.GetLength(1), imageArrayNew.GetLength(0));
                for (int i = 0; i < imageArrayNew.GetLength(0); i++)
                    for (int j = 0; j < imageArrayNew.GetLength(1); j++)
                    {
                        bmp2.SetPixel(j, i, Color.FromArgb((int)imageArrayNew[i, j], (int)imageArrayNew[i, j], (int)imageArrayNew[i, j]));
                    }
                pictureBox2.Image = bmp2;

                Bitmap bmpFromImageArrayNew = CannyStandardVersion.Non_Maximum_Suppression(bmp = null, imageArrayNew);
                pictureBox4.Image = CannyStandardVersion.Double_Threshold(bmpFromImageArrayNew, bottomThreshold, upperThreshold);
                Bitmap c = new Bitmap(bmpFromImageArrayNew);
                cannyResult = CannyStandardVersion.Hysteresis_Thresholding(c, "Canny");
                pictureBox3.Image = cannyResult;
            }
            else
            {
                pictureBox1.Image = bmp;

                Bitmap a = new Bitmap(pictureBox1.Image);
                CannyStandardVersion.GrayScale(a);
                CannyStandardVersion.GradUsingFirstDerOfGaus(a, sigma);
                pictureBox2.Image = a;
                Bitmap b = new Bitmap(a);
                CannyStandardVersion.Non_Maximum_Suppression(b);
                pictureBox4.Image = CannyStandardVersion.Double_Threshold(b, bottomThreshold, upperThreshold);
                Bitmap c = new Bitmap(b);
                pictureBox3.Image = CannyStandardVersion.Hysteresis_Thresholding(c, "Canny");
            }
        }

        public CannyForm(string filePath, double sigma, double bottomThreshold, double upperThreshold)// получаем из основной формы путь к выбранному файлу
        {
            InitializeComponent();
            this.filePath = filePath;
            this.sigma = sigma;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
            pictureBox1.Image = Image.FromFile(filePath);

            Bitmap a = new Bitmap(pictureBox1.Image);
            CannyStandardVersion.GrayScale(a);
            CannyStandardVersion.GradUsingFirstDerOfGaus(a, sigma);
            pictureBox2.Image = a;
            Bitmap b = new Bitmap(a);
            CannyStandardVersion.Non_Maximum_Suppression(b);
            pictureBox4.Image = CannyStandardVersion.Double_Threshold(b, bottomThreshold, upperThreshold);
            Bitmap c = new Bitmap(b);
            pictureBox3.Image = CannyStandardVersion.Hysteresis_Thresholding(c, "Canny");
        }

    }
}
