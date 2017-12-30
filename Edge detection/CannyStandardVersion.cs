using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edge_detection
{
    class CannyStandardVersion : ImageProcessing
    {
        public static Bitmap GradUsingFirstDerOfGaus(Bitmap bmp, double sigma)
        {
            decimal respondX;
            decimal respondY;
            decimal[,] respondSmall = new decimal[bmp.Height, bmp.Width];
            atan = new decimal[bmp.Height, bmp.Width];

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


            decimal[,] dgau2D = new decimal[(width * 2) + 1, (width * 2) + 1];
            decimal[,] dgao2Dtransp = new decimal[(width * 2) + 1, (width * 2) + 1];
            for (int i = 0; i < (width * 2) + 1; i++)
                for (int j = 0; j < (width * 2) + 1; j++)
                {
                    dgau2D[i, j] = Convert.ToDecimal(-X[i, j] * Math.Exp(-(X[i, j] * X[i, j] + Y[i, j] * Y[i, j]) / (2 * ssq)) / (Math.PI * ssq));
                    dgao2Dtransp[j, i] = Convert.ToDecimal(-X[i, j] * Math.Exp(-(X[i, j] * X[i, j] + Y[i, j] * Y[i, j]) / (2 * ssq)) / (Math.PI * ssq));
                }

            decimal[,] BigPic = new decimal[bmp.Height + 2 * width, bmp.Width + 2 * width];// здесь будет храниться увеличенное изображение для избежания краевого эффекта
            BigPic = FormBigPic(bmp, width);


            //свертка
            for (int x = 0; x < bmp.Height; x++)
                for (int y = 0; y < bmp.Width; y++)
                {
                    respondX = 0;
                    respondY = 0;
                    for (int u = 0; u < (width * 2) + 1; u++)
                        for (int v = 0; v < (width * 2) + 1; v++)
                        {
                            respondX += dgau2D[u, v] * BigPic[x + u, y + v];
                            respondY += dgao2Dtransp[u, v] * BigPic[x + u, y + v];
                        }
                    atan[x, y] = Convert.ToDecimal((Math.Atan2(Convert.ToDouble(respondY), Convert.ToDouble(respondX)) * (180 / Math.PI)));
                    if (atan[x, y] < 0) atan[x, y] = atan[x, y] + 180;
                    if ((atan[x, y] > 0 && (Convert.ToDouble(atan[x, y]) < 22.5) || (Convert.ToDouble(atan[x, y]) > 157.5 && atan[x, y] <= 180)))
                    {
                        atan[x, y] = 0;
                    }
                    else if (Convert.ToDouble(atan[x, y]) > 22.5 && Convert.ToDouble(atan[x, y]) < 67.5)
                    {
                        atan[x, y] = 45;
                    }
                    else if (Convert.ToDouble(atan[x, y]) > 67.5 && Convert.ToDouble(atan[x, y]) < 112.5)
                    {
                        atan[x, y] = 90;
                    }
                    else if (Convert.ToDouble(atan[x, y]) > 112.5 && Convert.ToDouble(atan[x, y]) < 157.5)
                    {
                        atan[x, y] = 135;
                    }

                    respondSmall[x, y] = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(respondX * respondX + respondY * respondY)));
                }
            decimal maxrespondSmall = Max(respondSmall);

            for (int i = 0; i < respondSmall.GetLength(0); i++)
                for (int j = 0; j < respondSmall.GetLength(1); j++)
                    respondSmall[i, j] = (respondSmall[i, j] / maxrespondSmall) * 255;//нормируем значение градиента от 0 до 255

            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                    bmp.SetPixel(j, i, Color.FromArgb(Convert.ToInt32(respondSmall[i, j]), Convert.ToInt32(respondSmall[i, j]), Convert.ToInt32(respondSmall[i, j])));

            respond = FormBigPic(bmp, 2);/*массив respond (и массив atan) с каждой стороны изображения будет больше исходного изображения на 2 пикселя
                                                                 (эти пиксели в сформированные для показа Bitmap не войдут, но они нужны дальше в методе Non_Maximum_Suppression,
                                                                 чтобы, если есть, сохранить перепады на самых крайних пикселях изображения)*/
            atan = FormBigPic(atan, 2, 2);
            return bmp;
        }

        public static decimal[,] GradUsingFirstDerOfGaus(decimal[,] imageArray, double sigma)
        {
            decimal respondX;
            decimal respondY;
            decimal[,] respondSmall = new decimal[imageArray.GetLength(0), imageArray.GetLength(1)];
            atan = new decimal[imageArray.GetLength(0), imageArray.GetLength(1)];

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


            decimal[,] dgau2D = new decimal[(width * 2) + 1, (width * 2) + 1];
            decimal[,] dgao2Dtransp = new decimal[(width * 2) + 1, (width * 2) + 1];
            for (int i = 0; i < (width * 2) + 1; i++)
                for (int j = 0; j < (width * 2) + 1; j++)
                {
                    dgau2D[i, j] = Convert.ToDecimal(-X[i, j] * Math.Exp(-(X[i, j] * X[i, j] + Y[i, j] * Y[i, j]) / (2 * ssq)) / (Math.PI * ssq));
                    dgao2Dtransp[j, i] = Convert.ToDecimal(-X[i, j] * Math.Exp(-(X[i, j] * X[i, j] + Y[i, j] * Y[i, j]) / (2 * ssq)) / (Math.PI * ssq));
                }

            decimal[,] BigPic = new decimal[imageArray.GetLength(0) + 2 * width, imageArray.GetLength(1) + 2 * width];// здесь будет храниться увеличенное изображение для избежания краевого эффекта
            BigPic = FormBigPic(imageArray, width, width);


            //свертка
            for (int x = 0; x < imageArray.GetLength(0); x++)
                for (int y = 0; y < imageArray.GetLength(1); y++)
                {
                    respondX = 0;
                    respondY = 0;
                    for (int u = 0; u < (width * 2) + 1; u++)
                        for (int v = 0; v < (width * 2) + 1; v++)
                        {
                            respondX += dgau2D[u, v] * BigPic[x + u, y + v];
                            respondY += dgao2Dtransp[u, v] * BigPic[x + u, y + v];
                        }
                    atan[x, y] = Convert.ToDecimal((Math.Atan2(Convert.ToDouble(respondY), Convert.ToDouble(respondX)) * (180 / Math.PI)));
                    if (atan[x, y] < 0) atan[x, y] = atan[x, y] + 180;
                    if ((atan[x, y] > 0 && (Convert.ToDouble(atan[x, y]) < 22.5) || (Convert.ToDouble(atan[x, y]) > 157.5 && atan[x, y] <= 180)))
                    {
                        atan[x, y] = 0;
                    }
                    else if (Convert.ToDouble(atan[x, y]) > 22.5 && Convert.ToDouble(atan[x, y]) < 67.5)
                    {
                        atan[x, y] = 45;
                    }
                    else if (Convert.ToDouble(atan[x, y]) > 67.5 && Convert.ToDouble(atan[x, y]) < 112.5)
                    {
                        atan[x, y] = 90;
                    }
                    else if (Convert.ToDouble(atan[x, y]) > 112.5 && Convert.ToDouble(atan[x, y]) < 157.5)
                    {
                        atan[x, y] = 135;
                    }

                    respondSmall[x, y] = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(respondX * respondX + respondY * respondY)));
                }
            decimal maxrespondSmall = Max(respondSmall);

            for (int i = 0; i < respondSmall.GetLength(0); i++)
                for (int j = 0; j < respondSmall.GetLength(1); j++)
                    respondSmall[i, j] = (respondSmall[i, j] / maxrespondSmall) * 255;//нормируем значение градиента от 0 до 255

            for (int i = 0; i < imageArray.GetLength(0); i++)
                for (int j = 0; j < imageArray.GetLength(1); j++)
                    imageArray[i, j] = respondSmall[i, j];

            respond = FormBigPic(imageArray, 2, 2);/*массив respond (и массив atan) с каждой стороны изображения будет больше исходного изображения на 2 пикселя
                                                                 (эти пиксели в сформированные для показа Bitmap не войдут, но они нужны дальше в методе Non_Maximum_Suppression,
                                                                 чтобы, если есть, сохранить перепады на самых крайних пикселях изображения)*/
            atan = FormBigPic(atan, 2, 2);
            return imageArray;
        }


    }
}
