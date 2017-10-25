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
        List<int> countorIndecesX;
        List<int> countorIndecesY;


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
                pictureBox4.Image= Form1.Hysteresis_Thresholding(bmpFromImageArray, "Canny");
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

        private void useHaarInCannyMethod_button_Click(object sender, EventArgs e)
        {
            countorIndecesX = new List<int>();
            countorIndecesY = new List<int>();
            Bitmap bmp = new Bitmap(pictureBox4.Image);

            int[,] imageArray = new int[bmp.Height, bmp.Width];
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                    imageArray[j, i] = bmp.GetPixel(i, j).R;

            // bool foundNeighbor = false;

            for (int i = 0; i < imageArray.GetLength(0); i++)
                for (int j = 0; j < imageArray.GetLength(1); j++)
                {
                    //foundNeighbor = false;
                    if (imageArray[i, j] == 255)
                    {
                        countorIndecesX.Add(i);
                        int rememberXIndexIfNeedTiRemove = countorIndecesX.Count;
                        countorIndecesY.Add(j);
                        int rememberYIndexIfNeedTiRemove = countorIndecesY.Count;                       
                        recursiveFindNeiborContourIndeces(imageArray,countorIndecesX,countorIndecesY, i,j);
                        //return; //это ведь "выйти из цикла"?

                        
                        //else
                            //return;// выйти из цикла
                    }
                }

            FindCountorIndeces(imageArray, countorIndecesX.Count, countorIndecesY.Count + 1);

        }

        public void recursiveFindNeiborContourIndeces(int [,] imageArray, List<int> countorIndecesX,List<int> countorIndecesY,
            int startPositionX, int startPositionY)
        {
            bool foundNeighbor = false;
            int i = startPositionX;
            int j = startPositionY;
            //int rememberXIndexIfNeedTiRemove;
            //int rememberYIndexIfNeedTiRemove
            if (imageArray[i, j + 1] == 255 &&
                            CheckIfNotALoop(imageArray, i, j + 1, countorIndecesX, countorIndecesY))
                        {
                            j++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }
                        else if (imageArray[i + 1, j + 1] == 255 &&
                             CheckIfNotALoop(imageArray, i + 1, j + 1, countorIndecesX, countorIndecesY))
                        {
                            i++;
                            j++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }
                        else if (imageArray[i + 1, j] == 255 &&
                             CheckIfNotALoop(imageArray, i + 1, j, countorIndecesX, countorIndecesY))
                        {
                            i++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }

                        else if (imageArray[i + 1, j - 1] == 255 &&
                             CheckIfNotALoop(imageArray, i + 1, j - 1, countorIndecesX, countorIndecesY))
                        {
                            i++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }
                        else if (imageArray[i, j - 1] == 255 &&
                             CheckIfNotALoop(imageArray, i, j - 1, countorIndecesX, countorIndecesY))
                        {
                            i++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }
                        else if (imageArray[i - 1, j - 1] == 255 &&
                             CheckIfNotALoop(imageArray, i - 1, j - 1, countorIndecesX, countorIndecesY))
                        {
                            i++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }
                        else if (imageArray[i - 1, j] == 255 &&
                             CheckIfNotALoop(imageArray, i - 1, j, countorIndecesX, countorIndecesY))
                        {
                            i++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }

                        else if (imageArray[i - 1, j + 1] == 255 &&
                             CheckIfNotALoop(imageArray, i - 1, j + 1, countorIndecesX, countorIndecesY))
                        {
                            i++;
                            countorIndecesX.Add(i);
                            countorIndecesY.Add(j);
                            foundNeighbor = true;
                        }
                        
                        if (foundNeighbor == false)
                        {
                            //countorIndecesX.RemoveAt(rememberXIndexIfNeedTiRemove);
                            //countorIndecesY.RemoveAt(rememberYIndexIfNeedTiRemove);
                             countorIndecesX.RemoveAt(startPositionX);
                             countorIndecesY.RemoveAt(startPositionY);
                return;
            }
            while (countorIndecesX[countorIndecesX.Count] != countorIndecesX[0] ||
    countorIndecesY[countorIndecesY.Count] != countorIndecesY[0])// if haven't reached the beginnig
                recursiveFindNeiborContourIndeces(imageArray, countorIndecesX, countorIndecesY, countorIndecesX[countorIndecesX.Count], countorIndecesY[countorIndecesY.Count]);

    }

        //public bool CheckIfReachedTheBeginning()
        //{
        //    bool reachedBeginnig = false;

        //    if 
        //}
        public bool CheckIfNotALoop(int[,] imageArray, int iCheck, int jCheck, List<int> listToCheckInX, List<int> listToCheckInY)
        {
            //if(imageArray[iCheck, jCheck] != countorIndecesX[countorIndecesX.Count] ||
            //                imageArray[iCheck, jCheck + 1] != countorIndecesY[countorIndecesY.Count])
            bool isNotALoop = true;
            for (int i = 0; i < imageArray.GetLength(0); i++)
                for (int j = 0; j < imageArray.GetLength(1); j++)
                {
                    if (iCheck == countorIndecesX[i] &&
                                    jCheck == countorIndecesY[j])
                    {
                        isNotALoop = false;
                        break; //??
                    }
                }
            return isNotALoop;
        }


        public void FindCountorIndeces(int [,] imageArray, int startPositionX, int startPositionY)
        {
            //int startPositionX = countorIndecesX[0];
            //int startPositionY = countorIndecesX[1];
            int x = startPositionX;
            int y = startPositionY;
            if (imageArray[x, y] == 255)
            {
                countorIndecesX.Add(x);
                countorIndecesY.Add(y);
                //if (y != imageArray.GetLength(1))
                //    y++;
                //else
                //    y = 0
            }
            while (x != countorIndecesX[0] && y != countorIndecesY[0])
            {
                

            }
        }
    }
}
