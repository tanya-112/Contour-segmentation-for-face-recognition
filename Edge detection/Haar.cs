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
        List<int> contourIndicesX;
        List<int> contourIndicesY;
        public Bitmap resultBmp;

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
                double[,] imageArrayCopyNeeded = new double[imageArray.GetLength(0), imageArray.GetLength(1)];

                for (int i = 0; i < imageArray.GetLength(0); i++)
                    for (int j = 0; j < imageArray.GetLength(1); j++)
                        imageArrayCopyNeeded[i, j] = imageArray[i, j];

                //ВСЕ, ЧТО НИЖЕ 1 РАЗ ЗАКОММЕНТИРОВАЛА, НАДО ПЕРЕСТРОИТЬ ПОД 2 МЕТОДА ХААРА(ГОРИЗ.И ВЕРТИК.) !!!

                        //Bitmap a = new Bitmap(pictureBox1.Image);
                //Form1.GrayScale(imageArray);
                Form1.HaarWaveletHorisontal(imageArray, waveletLength);
                Bitmap bmpFromArray = new Bitmap(imageArray.GetLength(1), imageArray.GetLength(0));
                for (int i = 0; i < bmpFromArray.Height; i++)
                    for (int j = 0; j < bmpFromArray.Width; j++)
                    {
                        bmpFromArray.SetPixel(j,i,Color.FromArgb((int)imageArray[i,j], (int)imageArray[i, j],(int)imageArray[i, j]));
                    }

                pictureBox2_1.Image = bmpFromArray;
                Bitmap b = new Bitmap(bmpFromArray);
                Form1.Non_Maximum_Suppression(b);
                pictureBox3_1.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
                Bitmap c = new Bitmap(b);
                Form1.Hysteresis_Thresholding(c, "Haar");


                //Bitmap a2 = new Bitmap(pictureBox1.Image);
                //Form1.GrayScale(imageArrayCopyNeeded);
                
               Form1.HaarWaveletVertical(imageArrayCopyNeeded, waveletLength);
                Bitmap bmpFromArray2 = new Bitmap(imageArrayCopyNeeded.GetLength(1), imageArrayCopyNeeded.GetLength(0));
                for (int i = 0; i < bmpFromArray2.Height; i++)
                    for (int j = 0; j < bmpFromArray2.Width; j++)
                    {
                        bmpFromArray2.SetPixel(j, i, Color.FromArgb((int)imageArrayCopyNeeded[i, j], (int)imageArrayCopyNeeded[i, j], (int)imageArrayCopyNeeded[i, j]));
                    }
                pictureBox2_2.Image = bmpFromArray2;
                Bitmap b2 = new Bitmap(bmpFromArray2);
               Form1.Non_Maximum_Suppression(b2);
                pictureBox3_2.Image = Form1.Double_Threshold(b2, bottomThreshold, upperThreshold);
                 Bitmap c2 = new Bitmap(b2);
                Form1.Hysteresis_Thresholding(c2, "Haar");

                resultBmp = Form1.SumHorisAndVerticHaarResults(c2, c);
                pictureBox4.Image = resultBmp;

                //imageArray = Form1.HaarWaveletHorisontal(imageArray, waveletLength);
                //Bitmap bmp2 = new Bitmap(imageArray.GetLength(1), imageArray.GetLength(0));
                //for (int i = 0; i < imageArray.GetLength(0); i++)
                //    for (int j = 0; j < imageArray.GetLength(1); j++)
                //    {
                //        bmp2.SetPixel(j, i, Color.FromArgb((int)imageArray[i, j], (int)imageArray[i, j], (int)imageArray[i, j]));
                //    }
                //pictureBox2_1.Image = bmp2;

                //Bitmap bmpFromImageArray = Form1.Non_Maximum_Suppression(bmp = null, imageArray);
                //bmpFromImageArray = Form1.Double_Threshold(bmpFromImageArray, bottomThreshold, upperThreshold);
                //pictureBox3_1.Image = bmpFromImageArray;
                ////Bitmap c = new Bitmap(b);
                //pictureBox4.Image = Form1.Hysteresis_Thresholding(bmpFromImageArray, "Haar");
                ////Bitmap a = new Bitmap(pictureBox1.Image);
                ////Form1.GrayScale(a);
                ////Form1.HaarWavelet(a, waveletLength);
                ////pictureBox2.Image = a;
                ////Bitmap b = new Bitmap(a);
                ////Form1.Non_Maximum_Suppression(b);
                ////pictureBox3.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
                ////Bitmap c = new Bitmap(b);
                ////pictureBox4.Image = Form1.Hysteresis_Thresholding(c, "Haar");
            }
        }

        private void Haar_Load(object sender, EventArgs e)
        {
            Bitmap a = new Bitmap(pictureBox1.Image);
            Form1.GrayScale(a);
            pictureBox2_1.Image = Form1.HaarWaveletHorisontal(a, waveletLength);           
            Bitmap b = new Bitmap(a);
            Form1.Non_Maximum_Suppression(b);
            pictureBox3_1.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
            Bitmap c = new Bitmap(b);
            Form1.Hysteresis_Thresholding(c, "Haar");


            Bitmap a2 = new Bitmap(pictureBox1.Image);
            Form1.GrayScale(a2);
            pictureBox2_2.Image =Form1.HaarWaveletVertical(a2, waveletLength);
            Bitmap b2 = new Bitmap(a2);
            Form1.Non_Maximum_Suppression(b2);
            pictureBox3_2.Image = Form1.Double_Threshold(b2, bottomThreshold, upperThreshold);
            Bitmap c2 = new Bitmap(b2);
            Form1.Hysteresis_Thresholding(a, "Haar");

            Bitmap resultBmp = Form1.SumHorisAndVerticHaarResults(c, a2);
            pictureBox4.Image =resultBmp;


        }

        private void useHaarInCannyMethod_button_Click(object sender, EventArgs e)
        {
            contourIndicesX = new List<int>();
            contourIndicesY = new List<int>();
            Bitmap bmp = new Bitmap(pictureBox4.Image);

            int[,] imageArray = new int[bmp.Height, bmp.Width];
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                    imageArray[j, i] = bmp.GetPixel(i, j).R;


            for (int i = 0; i < imageArray.GetLength(0); i++)
                for (int j = 0; j < imageArray.GetLength(1); j++)
                {
                    if (imageArray[i, j] == 255)
                    {
                        contourIndicesX.Add(i);
                        int rememberXIndexIfNeedTiRemove = contourIndicesX.Count;
                        contourIndicesY.Add(j);
                        int rememberYIndexIfNeedTiRemove = contourIndicesY.Count;
                        recursiveFindNeighborContourIndices(imageArray, contourIndicesX, contourIndicesY, i, j);
                        break;
                    }
                }
            int abc = 1;

        }

        public void recursiveFindNeighborContourIndices(int[,] imageArray, List<int> contourIndicesX, List<int> contourIndicesY,
            int startPositionX, int startPositionY)
        {
            bool foundNeighbor = false;
            int i = startPositionX;
            int j = startPositionY;
            if (imageArray[i, j + 1] == 255 &&
                            CheckIfNotALoop(imageArray, i, j + 1, contourIndicesX, contourIndicesY))
            {
                j++;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }
            else if (imageArray[i + 1, j + 1] == 255 &&
                 CheckIfNotALoop(imageArray, i + 1, j + 1, contourIndicesX, contourIndicesY))
            {
                i++;
                j++;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }
            else if (imageArray[i + 1, j] == 255 &&
                 CheckIfNotALoop(imageArray, i + 1, j, contourIndicesX, contourIndicesY))
            {
                i++;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }

            else if (imageArray[i + 1, j - 1] == 255 &&
                 CheckIfNotALoop(imageArray, i + 1, j - 1, contourIndicesX, contourIndicesY))
            {
                i++;
                j--;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }
            else if (imageArray[i, j - 1] == 255 &&
                 CheckIfNotALoop(imageArray, i, j - 1, contourIndicesX, contourIndicesY))
            {
                j--;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }
            else if (imageArray[i - 1, j - 1] == 255 &&
                 CheckIfNotALoop(imageArray, i - 1, j - 1, contourIndicesX, contourIndicesY))
            {
                i--;
                j--;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }
            else if (imageArray[i - 1, j] == 255 &&
                 CheckIfNotALoop(imageArray, i - 1, j, contourIndicesX, contourIndicesY))
            {
                i--;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }

            else if (imageArray[i - 1, j + 1] == 255 &&
                 CheckIfNotALoop(imageArray, i - 1, j + 1, contourIndicesX, contourIndicesY))
            {
                i--;
                j++;
                contourIndicesX.Add(i);
                contourIndicesY.Add(j);
                foundNeighbor = true;
            }

            if (foundNeighbor == false)
            {
                contourIndicesX.RemoveAt(startPositionX);
                contourIndicesY.RemoveAt(startPositionY);
                return;
            }
            while (contourIndicesX[contourIndicesX.Count - 1] != contourIndicesX[0] ||
                contourIndicesY[contourIndicesY.Count - 1] != contourIndicesY[0]) // if haven't reached the beginnig
                recursiveFindNeighborContourIndices(imageArray, contourIndicesX, contourIndicesY, contourIndicesX[contourIndicesX.Count - 1],
                    contourIndicesY[contourIndicesY.Count - 1]);

            //удаляем по последнему индексу, т.к. они совпадают с началом просложенного контура, который уже занесен в список
            contourIndicesX.RemoveAt(contourIndicesX.Count - 1);
            contourIndicesY.RemoveAt(contourIndicesY.Count - 1);

            int abc = 123;
        }


        public bool CheckIfNotALoop(int[,] imageArray, int iCheck, int jCheck, List<int> listToCheckInX, List<int> listToCheckInY)
        {
            bool isNotALoop = true;
            for (int i = 0; i < contourIndicesX.Count; i++)
                {
                    if (iCheck == contourIndicesX[i] &&
                                    jCheck == contourIndicesY[i] && (iCheck!= contourIndicesX[0] || jCheck!= contourIndicesY[0]))
                    {
                        isNotALoop = false;
                        break; //??
                    }
                }
            return isNotALoop;
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
