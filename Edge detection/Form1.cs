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
    public partial class Form1 : Form
    {
        public string filePath;
        public static double[,] resultingImage;
        public static double[,] methodResult;
        public static double[,] respond;
        public static double[,] atan;
        public static double[,] suppressed;

        public Form1()
        {
            InitializeComponent();
        }

        private void chooseFile_button_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                useCannyMethod_button.Enabled = true;
                useHaarInCannyMethod_button.Enabled = true;
            }
        }

        private void useCannyMethod_button_Click(object sender, EventArgs e)
        {
            Canny CannyForm = new Canny(filePath, Convert.ToDouble(sigma_textBox.Text), Convert.ToInt16(k_textBox.Text), Convert.ToDouble(bottomThreshold_textBox.Text), Convert.ToDouble(upperThreshold_textBox.Text)); //создаем окно для вывода результатов метода Канни, передавая путь к выбранному файлу
            CannyForm.Show();

        }

        public static Bitmap GrayScale(Bitmap bmp)
        {
            int[,] incomeImage = new int[bmp.Height, bmp.Width];//тестирую (можно удалить)
            for (int i = 0; i < bmp.Width; i++)//тестирую (можно удалить)
                for (int j = 0; j < bmp.Height; j++)//тестирую (можно удалить)
                    incomeImage[j, i] = bmp.GetPixel(i, j).R;//тестирую (можно удалить)

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color bmpColor = bmp.GetPixel(x, y);
                    int colorGray = Convert.ToInt32(bmpColor.R * 0.299 + bmpColor.G * 0.587 + bmpColor.B * 0.114);
                    bmp.SetPixel(x, y, Color.FromArgb(colorGray, colorGray, colorGray));
                }
            }
            return bmp;
        }


        public static Bitmap Gaussian_Blur(Bitmap bmp, double sigma, short k)
        {
                int kk;
                double[,] bigPic;
                double[,] gaussKernel;
                Color cl;

                kk = 2 * k;
                sigma *= sigma;//т.к. в формуле Гаусса необходимо исп-ть сигму в квадрате

                int[,] incomeImage = new int[bmp.Height, bmp.Width];//тестирую (можно удалить)
                for (int i = 0; i < bmp.Width; i++)//тестирую (можно удалить)
                    for (int j = 0; j < bmp.Height; j++)//тестирую (можно удалить)
                        incomeImage[j, i] = bmp.GetPixel(i, j).R;//тестирую (можно удалить)
            if (k > 0) //если размер маски для размытия положительный, размываем, иначе - пропускаем весь этап размытия
            {
                gaussKernel = new double[kk + 1, kk + 1];
                bigPic = new double[bmp.Height + kk, bmp.Width + kk];
                // resultingImage = new double[bmp.Height + 6,bmp.Width + 6];/*resultingImage с каждой стороны больше исходного изображения на 3 пикселя
                //(это сделано для избежания краевого эффекта при обработке изображений в последующих методах)*/
                //формирование ядра
                double p;
                for (int x = 0; x <= kk; x++) //"=" - захватить саму точку, размер матрицы (2k+1)x(2k+1)
                    for (int y = 0; y <= kk; y++)
                    {
                        //p = -((x - k - 1) * (x - k - 1) + (y - k - 1) * (y - k - 1));
                        p = -((k - x) * (k - x) + (k - y) * (k - y));
                        gaussKernel[x, y] = (1.0 / (2.0 * Math.PI * sigma)) * Math.Exp(p / (2.0 * sigma));
                    }

                //H = new double[kk + 1, kk + 1];
                //H[0,0] = 0.000789;
                //H[0,1] = 0.006581;
                //H[0,2] = 0.013347;
                //H[0,3] = 0.006581;
                //H[0,4] = 0.000789;

                //H[1,0] = 0.006581;
                //H[1,1] = 0.054901;
                //H[1,2] = 0.111345;
                //H[1,3] = 0.054901;
                //H[1,4] = 0.006581;

                //H[2,0] = 0.013347;
                //H[2,1] = 0.111345;
                //H[2,2] = 0.225821;
                //H[2,3] = 0.111345;
                //H[2,4] = 0.013347;

                //H[3,0] = 0.006581;
                //H[3,1] = 0.054901;
                //H[3,2] = 0.111345;
                //H[3,3] = 0.054901;
                //H[3,4] = 0.006581;

                //H[4,0] = 0.000789;
                //H[4,1] = 0.006581;
                //H[4,2] = 0.013347;
                //H[4,3] = 0.006581;
                //H[4,4] = 0.000789;

                bigPic = FormBigPic(bmp, k);
                //int diffInPixelAmount=M.L

                double blurredPix;
                //свертка
                for (int x = 0; x < bigPic.GetLength(0) - kk; x++)
                    for (int y = 0; y < bigPic.GetLength(1) - kk; y++)
                    {
                        blurredPix = 0.0;
                        for (int u = 0; u <= kk; u++)
                            for (int v = 0; v <= kk; v++)
                                blurredPix += gaussKernel[u, v] * Convert.ToDouble(bigPic[u + x, v + y]);
                        bmp.SetPixel(y, x, Color.FromArgb(Convert.ToInt32(blurredPix), Convert.ToInt32(blurredPix), Convert.ToInt32(blurredPix)));//разве тут не надо у-1,х-1?
                    }
            }
            return bmp;
        }

        public static double[,] FormBigPic(Bitmap bmp, int k) //сформировать увеличенное (на k пикселей с каждой стороны) изображение
        {
            /*формирование вспомогательной матрицы Для правильного расчёта краевых точек.
              Поверхность увеличивается по ширине и высоте на 2k(исходя из того, что размер маски равен (2k+1)х(2k+1)).
              Значение точек в расширенной области заполняются значениями краевых точек.
             */
            Color cl;
            int kk = 2 * k;
            double[,] bigPic = new double[bmp.Height + kk, bmp.Width + kk];// здесь будет храниться увеличенное изображение для избежания краевого эффекта

            for (int x = 0; x < bmp.Width + kk; x++)
                for (int y = 0; y < bmp.Height + kk; y++)
                {
                    if (y <= k)
                    {
                        if (x < k) cl = bmp.GetPixel(0, 0);
                        else if (x >= (bmp.Width + k)) cl = bmp.GetPixel(bmp.Width - 1, 0);
                        else cl = bmp.GetPixel(x - k, 0);
                    }

                    else if (y >= (bmp.Height + k))
                    {
                        if (x < k) cl = bmp.GetPixel(0, bmp.Height - 1);
                        else if (x >= (bmp.Width + k)) cl = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
                        else cl = bmp.GetPixel(x - k, bmp.Height - 1);
                    }

                    else
                    {
                        if (x < k) cl = bmp.GetPixel(0, y - k);
                        else if (x >= bmp.Width + k) cl = bmp.GetPixel(bmp.Width - 1, y - k);
                        else cl = bmp.GetPixel(x - k, y - k);
                    }

                    bigPic[y, x] = cl.R;
                }
            return bigPic;
        }


        public static Bitmap GradUsingFirstDerOfGaus(Bitmap bmp, double sigma)
        {
            double respondX;
            double respondY;
            respond = new double[bmp.Height, bmp.Width];
            atan = new double[bmp.Height, bmp.Width];

            double[] pw = new double[30];
            for (int i = 0; i < 30; i++)
                pw[i] = i;
            double ssq = sigma * sigma;
            double gaussianDieOff = 0.0001;
            double[] pwSqure = new double[30];
            for (int i = 0; i < 30; i++)
                pwSqure[i] = pw[i] * pw[i];
            double[] exps = new double[30];
            int width = -1;
            for (int i = 0; i < 30; i++)
            {
                exps[i] = Math.Exp(-pwSqure[i] / (2 * ssq));
                if (exps[i] > gaussianDieOff)
                    width = i;
            }
            if (width == -1)
                width = 1;
            int[,] X = new int[(width * 2) + 1, (width * 2) + 1];
            int[,] Y = new int[(width * 2) + 1, (width * 2) + 1];
            int fillXY = -width;
            for (int i = 0; i < (width * 2) + 1; i++)
            {
                fillXY = -width;
                for (int j = 0; j < (width * 2) + 1; j++)
                {
                    X[i, j] = fillXY;
                    Y[j, i] = fillXY;
                    fillXY++;
                }
            }


            double[,] dgau2D = new double[(width * 2) + 1, (width * 2) + 1];
            double[,] dgao2Dtransp = new double[(width * 2) + 1, (width * 2) + 1];
            for (int i = 0; i < (width * 2) + 1; i++)
                for (int j = 0; j < (width * 2) + 1; j++)
                {
                    dgau2D[i, j] = -X[i, j] * Math.Exp(-(X[i, j] * X[i, j] + Y[i, j] * Y[i, j]) / (2 * ssq)) / (Math.PI * ssq);
                    dgao2Dtransp[j, i] = -X[i, j] * Math.Exp(-(X[i, j] * X[i, j] + Y[i, j] * Y[i, j]) / (2 * ssq)) / (Math.PI * ssq);
                }

            double[,] BigPic = new double[bmp.Height + 2 * width, bmp.Width + 2 * width];// здесь будет храниться увеличенное изображение для избежания краевого эффекта
            BigPic = Form1.FormBigPic(bmp, width);


            //свертка
            for (int x = 0; x < bmp.Height; x++)
                for (int y = 0; y < bmp.Width; y++)
                {
                    respondX = 0.0;
                    respondY = 0.0;
                    for (int u = 0; u < (width * 2) + 1; u++)
                        for (int v = 0; v < (width * 2) + 1; v++)
                        {
                            respondX += dgau2D[u, v] * BigPic[x + u, y + v];
                            respondY += dgao2Dtransp[u, v] * BigPic[x + u, y + v];
                        }
                    atan[x, y] = (Math.Atan2(respondY, respondX)) * (180 / Math.PI);
                    if (atan[x, y] < 0) atan[x, y] = atan[x, y] + 180;
                    if ((atan[x, y] > 0 && atan[x, y] < 22.5) || (atan[x, y] > 157.5 && atan[x, y] <= 180))
                    {
                        atan[x, y] = 0;
                    }
                    else if (atan[x, y] > 22.5 && atan[x, y] < 67.5)
                    {
                        atan[x, y] = 45;
                    }
                    else if (atan[x, y] > 67.5 && atan[x, y] < 112.5)
                    {
                        atan[x, y] = 90;
                    }
                    else if (atan[x, y] > 112.5 && atan[x, y] < 157.5)
                    {
                        atan[x, y] = 135;
                    }

                    respond[x, y] = Math.Sqrt(respondX * respondX + respondY * respondY);
                    bmp.SetPixel(y, x, Color.FromArgb(Convert.ToInt32(respond[x, y]), Convert.ToInt32(respond[x, y]), Convert.ToInt32(respond[x, y])));
                }
            return bmp;
        }

        public static Bitmap Sobel(Bitmap bmp)
        {

            int[,] incomeImage = new int[bmp.Height, bmp.Width];//тестирую (можно удалить)
            for (int i = 0; i < bmp.Width; i++)//тестирую (можно удалить)
                for (int j = 0; j < bmp.Height; j++)//тестирую (можно удалить)
                    incomeImage[j, i] = bmp.GetPixel(i, j).R;//тестирую (можно удалить)

            int[,] Gx;
            int[,] Gy;
            double[,] bigPic;
            double respondX;
            double respondY;
            //int k = 1;-
            //int kk = 2 * k;
            int k = 3;//кол-во пикселей, которые будут добавлены с каждой отдельной стороны изображения (в этой программе необх. именно 3)
            int kk = 2 * k;//итоговое кол-во пикселей, которые таким образом будут добавлены к высоте и ширине изображения

            Gx = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Gy = new int[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            bigPic = new double[bmp.Height + kk, bmp.Width + kk];// здесь будет храниться увеличенное изображение для избежания краевого эффекта
            respond = new double[bmp.Height + kk - 2, bmp.Width + kk - 2]; /*массив respond с каждой стороны изображения будет больше исходного изображения на 2 пикселя
                                                                 (эти пиксели в сформированные для показа Bitmap не войдут, но они нужны дальше в методе Non_Maximum_Suppression,
                                                                 чтобы, если есть, сохранить перепады на самых крайних пикселях изображения)*/
            atan = new double[bmp.Height + kk - 2, bmp.Width + kk - 2];
            bigPic = Form1.FormBigPic(bmp, k);

            int endFor1 = bmp.Height + kk - 2;
            int endFor2 = bmp.Width + kk - 2;
            //свертка
            for (int x = 0; x < endFor1; x++)
                for (int y = 0; y < endFor2; y++)
                {
                    respondX = 0.0;
                    respondY = 0.0;

                    for (int u = 0; u < 3; u++)
                        for (int v = 0; v < 3; v++)
                        {
                            respondX += Gx[u, v] * bigPic[u + x, v + y];
                            respondY += Gy[u, v] * bigPic[u + x, v + y];
                        }
                    //atan[x, y] = Math.Abs(Math.Atan2((respondY), (respondX))) * (180.00 / Math.PI);
                    atan[x, y] = Math.Atan2((respondY), (respondX)) * (180.00 / Math.PI);
                    if (atan[x, y] < 0) atan[x, y] = atan[x, y] + 180;
                    if ((atan[x, y] > 0 && atan[x, y] < 22.5) || (atan[x, y] > 157.5 && atan[x, y] <= 180))
                    {
                        atan[x, y] = 0;
                    }
                    else if (atan[x, y] > 22.5 && atan[x, y] < 67.5)
                    {
                        atan[x, y] = 45;
                    }
                    else if (atan[x, y] > 67.5 && atan[x, y] < 112.5)
                    {
                        atan[x, y] = 90;
                    }
                    else if (atan[x, y] > 112.5 && atan[x, y] < 157.5)
                    {
                        atan[x, y] = 135;
                    }
                    respond[x, y] = Math.Sqrt(respondX * respondX + respondY * respondY);//вычисляем градиент 
                }

            double maxRespond = Max(respond);

            for (int i = 0; i < respond.GetLength(0); i++)
                for (int j = 0; j < respond.GetLength(1); j++)
                    respond[i, j] = (respond[i, j] / maxRespond) * 255;//нормируем значение градиента от 0 до 255

            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                    bmp.SetPixel(j, i, Color.FromArgb(Convert.ToInt32(respond[i + 2, j + 2]), Convert.ToInt32(respond[i + 2, j + 2]), Convert.ToInt32(respond[i + 2, j + 2])));

            return bmp;
        }

        public static Bitmap Non_Maximum_Suppression(Bitmap bmp)
        {

            int[,] incomeImage = new int[bmp.Height, bmp.Width];//тестирую (можно удалить)
            for (int i = 0; i < bmp.Width; i++)//тестирую (можно удалить)
                for (int j = 0; j < bmp.Height; j++)//тестирую (можно удалить)
                    incomeImage[j, i] = bmp.GetPixel(i, j).R;//тестирую (можно удалить)


            suppressed = new double[bmp.Height + 2, bmp.Width + 2];
            for (int x = 1; x < respond.GetLength(0) - 1; x++)
                for (int y = 1; y < respond.GetLength(1) - 1; y++)/*цикл от 1 и до предпосл.элемента, т.к. respond - это увеличенное на 2 пикселя с каждой стороны изображение 
                                                            и при этом заполняется suppressed,кот. с каждой стороны на 1 пиксель меньше,чем respond. 
                                                            Избегается краевой эффект. */
                {
                    if (atan[x, y] == 0)
                        if (respond[x, y - 1] < respond[x, y] && respond[x, y + 1] < respond[x, y])
                            suppressed[x - 1, y - 1] = respond[x, y];
                        else
                        {
                            suppressed[x - 1, y - 1] = 0;
                            respond[x, y] = 0;
                        }
                    if (atan[x, y] == 45)
                        if (respond[x - 1, y + 1] < respond[x, y] && respond[x + 1, y - 1] < respond[x, y])
                            suppressed[x - 1, y - 1] = respond[x, y];
                        else
                        {
                            suppressed[x - 1, y - 1] = 0;
                            respond[x, y] = 0;
                        }
                    if (atan[x, y] == 90)
                        if (respond[x - 1, y] < respond[x, y] && respond[x + 1, y] < respond[x, y])
                            suppressed[x - 1, y - 1] = respond[x, y];
                        else
                        {
                            suppressed[x - 1, y - 1] = 0;
                            respond[x, y] = 0;
                        }
                    if (atan[x, y] == 135)
                        if (respond[x - 1, y - 1] < respond[x, y] && respond[x + 1, y + 1] < respond[x, y])
                            suppressed[x - 1, y - 1] = respond[x, y];
                        else {
                            suppressed[x - 1, y - 1] = 0;
                            respond[x, y] = 0;
                        }
                            //bmp.SetPixel(y-1, x-1, Color.FromArgb((int)suppressed[x-1, y-1], (int)suppressed[x-1, y-1], (int)suppressed[x-1, y-1]));
                        }
                    for (int x = 0; x < bmp.Height; x++)
                for (int y = 0; y < bmp.Width; y++)
                    bmp.SetPixel(y, x, Color.FromArgb(Convert.ToInt32(suppressed[x + 1, y + 1]), Convert.ToInt32(suppressed[x + 1, y + 1]), Convert.ToInt32(suppressed[x + 1, y + 1])));


            //for (int x = 0; x < bmp.Height; x += bmp.Height - 1)
            //    for (int y = 0; y < bmp.Width; y += bmp.Width - 1)
            //    {
            //        if (atan[x, y] == 0)
            //            if (respond[x, y - 1] < respond[x, y] && respond[x, y + 1] <= respond[x, y])
            //                suppressed[x, y] = respond[x, y];
            //            else suppressed[x, y] = 0;
            //        if (atan[x, y] == 45)
            //            if (respond[x - 1, y - 1] < respond[x, y] && respond[x + 1, y + 1] < respond[x, y])
            //                suppressed[x, y] = respond[x, y];
            //            else suppressed[x, y] = 0;
            //        if (atan[x, y] == 90)
            //            if (respond[x - 1, y] < respond[x, y] && respond[x + 1, y] < respond[x, y])
            //                suppressed[x, y] = respond[x, y];
            //            else suppressed[x, y] = 0;
            //        if (atan[x, y] == 135)
            //            if (respond[x - 1, y - 1] < respond[x, y] && respond[x + 1, y + 1] < respond[x, y])
            //                suppressed[x, y] = respond[x, y];
            //            else suppressed[x, y] = 0;
            //        bmp.SetPixel(y, x, Color.FromArgb((byte)suppressed[x, y], (byte)suppressed[x, y], (byte)suppressed[x, y]));
            //    }
            return bmp;

        }


        public static Bitmap Double_Threshold(Bitmap bmp, double bottomThreshold, double upperThreshold)
        {
            int[,] incomeImage = new int[bmp.Height, bmp.Width];//тестирую (можно удалить)
            for (int i = 0; i < bmp.Width; i++)//тестирую (можно удалить)
                for (int j = 0; j < bmp.Height; j++)//тестирую (можно удалить)
                    incomeImage[j, i] = bmp.GetPixel(i, j).R;//тестирую (можно удалить)

            for (int x = 1; x < suppressed.GetLength(0) - 1; x++)
                for (int y = 1; y < suppressed.GetLength(1) - 1; y++)/*Цикл от 1 и до предпосл.элемента, т.к. suppressed - это увеличенное на 1 пиксель с каждой стороны
                                                                     изображение и подавление немаксимумов для крайних пикселей не нужно (но при этом наличие этих крайних
                                                                     пикселей является обязательным для работоспособности метода Hysteresis_Thresholding)*/
                {
                    if (suppressed[x, y] <= bottomThreshold)
                    {
                        bmp.SetPixel(y - 1, x - 1, Color.Black);//y - 1, x - 1, т.к. исх.изображение, т.е. bmp, на 1 пиксель с кажд.стороны меньше, чем suppressed
                        suppressed[x, y] = 0;
                    }
                    else if (suppressed[x, y] >= upperThreshold)
                    {
                        bmp.SetPixel(y - 1, x - 1, Color.White);
                        suppressed[x, y] = 255;
                    }
                    else if (suppressed[x, y] > bottomThreshold && suppressed[x, y] < upperThreshold)
                    {
                        bmp.SetPixel(y - 1, x - 1, Color.YellowGreen);
                        suppressed[x, y] = 128;

                    }
                }
            return bmp;
        }

        public static Bitmap Hysteresis_Thresholding(Bitmap bmp, string whoCalles)
        {

            int[,] incomeImage = new int[bmp.Height, bmp.Width];//тестирую (можно удалить)
            for (int i = 0; i < bmp.Width; i++)//тестирую (можно удалить)
                for (int j = 0; j < bmp.Height; j++)//тестирую (можно удалить)
                    incomeImage[j, i] = bmp.GetPixel(i, j).R;//тестирую (можно удалить)

            methodResult = new double[bmp.Height, bmp.Width];
            for (int x = 1; x < suppressed.GetLength(0) - 1; x++)
                for (int y = 1; y < suppressed.GetLength(1) - 1; y++) /*Цикл от 1 и до предпосл.элемента, т.к.suppressed - это увеличенное на 1 пиксель с каждой стороны
                                                                      изображение, что позволяет корректно работать нижеприведенному условию if без краевого эффекта*/
                    if (suppressed[x, y] == 128)
                        if (suppressed[x, y + 1] == 255 || suppressed[x - 1, y + 1] == 255 || suppressed[x - 1, y] == 255 || suppressed[x - 1, y - 1] == 255 || suppressed[x, y - 1] == 255 || suppressed[x + 1, y - 1] == 255 || suppressed[x + 1, y] == 255 || suppressed[x + 1, y + 1] == 255)
                        {
                            suppressed[x, y] = 255;
                            bmp.SetPixel(y - 1, x - 1, Color.White);//y - 1, x - 1, т.к. исх.изображение, т.е. bmp, на 1 пиксель с кажд.стороны меньше, чем suppressed
                        }
                        else
                        {
                            suppressed[x, y] = 0;
                            bmp.SetPixel(y - 1, x - 1, Color.Black);
                        }


            if (whoCalles == "Canny")
                methodResult = suppressed;// = suppressed но уменьшенное с каждой стороны на 1 пиксель (создать тут массив methodResult, скопировав в него suppressed за искл.крайних пикселей)
            if (whoCalles == "Haar")
                methodResult = suppressed;
            bmp.Save("E:\\CannyResult.png", System.Drawing.Imaging.ImageFormat.Png);
            return bmp;
        }

        private void useHaarInCannyMethod_button_Click(object sender, EventArgs e)
        {
            Haar HaarForm = new Haar(filePath, Int32.Parse(waveletLength_textBox.Text), Convert.ToDouble(bottomThresholdHaar_textBox.Text), Convert.ToDouble(upperThresholdHaar_textBox.Text)); //создаем окно для вывода результатов метода Канни, передавая путь к выбранному файлу
            HaarForm.Show();
        }


        public static Bitmap HaarWavelet(Bitmap bmp, int filterLength)
        {
            int[] haarMatrix = new int[filterLength];
            for (int i = 0; i < filterLength / 2; i++)
                haarMatrix[i] = 1;
            for (int i = filterLength / 2; i < filterLength; i++)
                haarMatrix[i] = -1;


            double resX = 0;//будет сканировать значения в пределах строки, т.е. реагировать на вертикальные контуры
            double resY = 0;//будет сканировать значения в пределах столбца, т.е. реагировать на горизонтальные контуры
            respond = new double[bmp.Height + 4, bmp.Width + 4];
            atan = new double[bmp.Height + 4, bmp.Width + 4];


            double[,] bigPic = new double[respond.GetLength(0) + filterLength, respond.GetLength(1) + filterLength];
            int addToBmp = (bigPic.GetLength(1) - bmp.Width) / 2;
            bigPic = FormBigPic(bmp, addToBmp);
            //int y = 0;
            for (int x = 0; x < bmp.Height + 4; x++)
                for (int y = 0; y < bmp.Width + 4; y++)
                {
                    resX = 0;
                    resY = 0;
                    for (int u = 0; u < filterLength; u++)
                    {
                        resX += haarMatrix[u] * bigPic[x + filterLength / 2, y + u];//было resX += haarMatrix[u] * bigPic[x, y + u]; 22.11.2016
                        resY += haarMatrix[u] * bigPic[x + u, y + filterLength / 2];
                    }
                    //resX = Math.Abs(resX / filterLength);//усреднили
                    //resY = Math.Abs(resY / filterLength);//усреднили



                    atan[x, y] = (Math.Atan2(Math.Abs(resY), Math.Abs(resX))) * (180 / Math.PI);
                    //atan[x, y] = 0;
                    if (atan[x, y] < 0) atan[x, y] = atan[x, y] + 180;
                    if ((atan[x, y] > 0 && atan[x, y] < 22.5) || (atan[x, y] > 157.5 && atan[x, y] <= 180))
                    {
                        atan[x, y] = 0;
                    }
                    else if (atan[x, y] > 22.5 && atan[x, y] < 67.5)
                    {
                        atan[x, y] = 45;
                    }
                    else if (atan[x, y] > 67.5 && atan[x, y] < 112.5)
                    {
                        atan[x, y] = 90;
                    }
                    else if (atan[x, y] > 112.5 && atan[x, y] < 157.5)
                    {
                        atan[x, y] = 135;
                    }
                    respond[x, y] = Math.Sqrt(resX * resX + resY * resY);

                    // respond[x, y] = resX;

                    //if (y < (bmp.Width-1))
                    //    y++;
                }
            double maxResp = Max(respond);
            for (int i = 0; i < respond.GetLength(0); i++)
                for (int j = 0; j < respond.GetLength(1); j++)
                    respond[i, j] = (respond[i, j] / maxResp) * 255; //нормируем значение отклика от 0 до 255
            for (int x = 0; x < bmp.Height; x++)
                for (int y = 0; y < bmp.Width; y++)
                    bmp.SetPixel(y, x, Color.FromArgb(Convert.ToInt32(respond[x + 2, y + 2]), Convert.ToInt32(respond[x + 2, y + 2]), Convert.ToInt32(respond[x + 2, y + 2])));
            return bmp;
        }

        private static double Max(double[,] arr)
        {
            double maxValue = arr[0, 0];
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j] > maxValue)
                        maxValue = arr[i, j];
            return maxValue;
        }

private void analyze_button_Click(object sender, EventArgs e)
        {
            AnalysisOfMethods analyzeForm = new AnalysisOfMethods(Convert.ToDouble(analyze_sigma_textBox.Text), Convert.ToInt16(k_analyze_textBox.Text),
            Convert.ToDouble(bottomThresholdCanny_analyze_textBox.Text), Convert.ToDouble(upperThresholdCanny_analyze_textBox.Text), 
            Int32.Parse(waveletLength_analyze_textBox.Text), Convert.ToDouble(bottomThresholdHaar_analyze_textBox.Text), 
            Convert.ToDouble(upperThresholdHaar_analyze_textBox.Text), Int32.Parse(widthOfBrigtnessDiffer_textBox.Text), Convert.ToDouble(SNR_textBox.Text));


            analyzeForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
