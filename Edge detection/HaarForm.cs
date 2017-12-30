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
    public partial class HaarForm : Form
    {
        string filePath;
        double bottomThreshold;
        double upperThreshold;
        int waveletLength;
        Bitmap bmp;
        public Bitmap resultBmp;
        public decimal [,] imageArray;

        public HaarForm(string filePath, int waveletLength, double bottomThreshold, double upperThreshold)
        {
            InitializeComponent();
            this.filePath = filePath;
            this.waveletLength = waveletLength;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
            pictureBox1.Image = Image.FromFile(filePath);
            findHaarResultsFromBmp();
        }

        public HaarForm(int waveletLength, double bottomThreshold, double upperThreshold, Bitmap bmp = null, decimal[,] imageArray = null)// перегружаем метод, чтобы принять на вход изображение, а не ссылку на него
        {
            InitializeComponent();
            this.bmp = bmp;
            this.waveletLength = waveletLength;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
            pictureBox1.Image = bmp;

            if (imageArray != null)
            {
                this.imageArray = imageArray;
                Bitmap bmp1 = new Bitmap(imageArray.GetLength(1), imageArray.GetLength(0));
                for (int i = 0; i < imageArray.GetLength(0); i++)
                    for (int j = 0; j < imageArray.GetLength(1); j++)
                    {
                        bmp1.SetPixel(j, i, Color.FromArgb((int)imageArray[i, j], (int)imageArray[i, j], (int)imageArray[i, j]));
                    }
                pictureBox1.Image = bmp1;
                decimal[,] imageArrayCopyNeeded = new decimal[imageArray.GetLength(0), imageArray.GetLength(1)];

                for (int i = 0; i < imageArray.GetLength(0); i++)
                    for (int j = 0; j < imageArray.GetLength(1); j++)
                        imageArrayCopyNeeded[i, j] = imageArray[i, j];

                decimal[,] imageArrayNew = CannyWithHaarWavelet.HaarWaveletHorisontal(imageArray, waveletLength);
                Bitmap bmpFromArray = new Bitmap(imageArrayNew.GetLength(1), imageArrayNew.GetLength(0));
                for (int i = 0; i < bmpFromArray.Height; i++)
                    for (int j = 0; j < bmpFromArray.Width; j++)
                    {
                        bmpFromArray.SetPixel(j, i, Color.FromArgb((int)imageArrayNew[i, j], (int)imageArrayNew[i, j], (int)imageArrayNew[i, j]));
                    }

                pictureBox2_1.Image = bmpFromArray;
                Bitmap b = new Bitmap(bmpFromArray);
                Bitmap bmpFromImageArrayNew = CannyWithHaarWavelet.Non_Maximum_Suppression(bmp = null, imageArrayNew);
                pictureBox3_1.Image = CannyWithHaarWavelet.Double_Threshold(bmpFromImageArrayNew, bottomThreshold, upperThreshold);
                Bitmap c = new Bitmap(bmpFromImageArrayNew);
                resultBmp = CannyWithHaarWavelet.Hysteresis_Thresholding(c, "Haar");
                pictureBox4.Image = resultBmp;               
            }
            else if (bmp != null)
            {
                Bitmap a = new Bitmap(pictureBox1.Image);
                this.imageArray = new decimal[a.Height, a.Width];
                for (int i = 0; i < a.Height; i++)
                    for (int j = 0; j < a.Width; j++)
                        this.imageArray[i, j] = a.GetPixel(j, i).R;

                CannyWithHaarWavelet.GrayScale(a);
                pictureBox2_1.Image = CannyWithHaarWavelet.HaarWaveletHorisontal(a, waveletLength);
                Bitmap b = new Bitmap(a);
                CannyWithHaarWavelet.Non_Maximum_Suppression(b);
                pictureBox3_1.Image = CannyWithHaarWavelet.Double_Threshold(b, bottomThreshold, upperThreshold);
                Bitmap c = new Bitmap(b);
                CannyWithHaarWavelet.Hysteresis_Thresholding(c, "Haar");


                Bitmap a2 = new Bitmap(pictureBox1.Image);
                CannyWithHaarWavelet.GrayScale(a2);
                pictureBox2_2.Image = CannyWithHaarWavelet.HaarWaveletVertical(a2, waveletLength);
                Bitmap b2 = new Bitmap(a2);
                CannyWithHaarWavelet.Non_Maximum_Suppression(b2);
                pictureBox3_2.Image = CannyWithHaarWavelet.Double_Threshold(b2, bottomThreshold, upperThreshold);
                Bitmap c2 = new Bitmap(b2);
                CannyWithHaarWavelet.Hysteresis_Thresholding(c2, "Haar");

                Bitmap resultBmp = CannyWithHaarWavelet.SumHorisAndVerticHaarResults(c, c2);
                pictureBox4.Image = resultBmp;

            }
        }

        internal void findHaarResultsFromBmp()
        {
            Bitmap a = new Bitmap(pictureBox1.Image);
            this.imageArray = new decimal[a.Height, a.Width];
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    this.imageArray[i, j] = a.GetPixel(j, i).R;

            CannyWithHaarWavelet.GrayScale(a);
            pictureBox2_1.Image = CannyWithHaarWavelet.HaarWaveletHorisontal(a, waveletLength);
            Bitmap b = new Bitmap(a);
            CannyWithHaarWavelet.Non_Maximum_Suppression(b);
            pictureBox3_1.Image = CannyWithHaarWavelet.Double_Threshold(b, bottomThreshold, upperThreshold);
            Bitmap c = new Bitmap(b);
            CannyWithHaarWavelet.Hysteresis_Thresholding(c, "Haar");


            Bitmap a2 = new Bitmap(pictureBox1.Image);
            CannyWithHaarWavelet.GrayScale(a2);
            pictureBox2_2.Image = CannyWithHaarWavelet.HaarWaveletVertical(a2, waveletLength);
            Bitmap b2 = new Bitmap(a2);
            CannyWithHaarWavelet.Non_Maximum_Suppression(b2);
            Bitmap b3 = CannyWithHaarWavelet.Double_Threshold(b2, bottomThreshold, upperThreshold);
            pictureBox3_2.Image = b3;
            Bitmap c2 = new Bitmap(b2);
            CannyWithHaarWavelet.Hysteresis_Thresholding(c2, "Haar");

            Bitmap resultBmp = CannyWithHaarWavelet.SumHorisAndVerticHaarResults(c, c2);
            pictureBox4.Image = resultBmp;
        }

        private void useHaarInsideExternalContour_button_Click(object sender, EventArgs e) 
        {
            Bitmap bmpWidthGivenEdge = new Bitmap(pictureBox4.Image);
            Bitmap bmpInsideTheContour = CannyWithHaarWavelet.FindEdgesInsideGivenEdge(bmpWidthGivenEdge, this.imageArray);
            HaarForm newHaarForm = new HaarForm(Int32.Parse(waveletLength_textBox.Text), Convert.ToDouble(bottomThresholdHaar_textBox.Text),
               Convert.ToDouble(upperThresholdHaar_textBox.Text), bmp = bmpInsideTheContour);
            newHaarForm.Show();

        }

    }
}
