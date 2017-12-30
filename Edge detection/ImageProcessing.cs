using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edge_detection
{
    abstract class ImageProcessing
    {
        public string filePath;
        public static double[,] resultingImage;
        public static double[,] methodResult;
        public static decimal[,] respond;
        public static decimal[,] atan;
        public static decimal[,] suppressed;
        public static bool varyQ_radioButtonChecked;
        public static bool varyWidthOfDiffer_radioButtonChecked;
        public static decimal[,] bigPicIncomeInSobel;
        public static bool enableNoisingImage_checkBoxChecked;
        internal static int addToBmp;
        internal AnalyzeWithPlots analyzeWithPlotsForm;
        internal static bool mustMakePlotInSameWindow;

        public static Bitmap GrayScale(Bitmap bmp = null)
        {
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

        public static double[,] GrayScale(double[,] imageArray)
        {
            for (int x = 0; x < imageArray.GetLength(0); x++)
            {
                for (int y = 0; y < imageArray.GetLength(1); y++)
                {
                    double value = imageArray[x, y];
                    double colorGray = value * 0.299 + value * 0.587 + value * 0.114;
                    imageArray[x, y] = colorGray;
                }
            }
            return imageArray;
        }


        public static decimal[,] FormBigPic(Bitmap bmp, int k) //сформировать увеличенное (на k пикселей с каждой стороны) изображение
        {
            /*формирование вспомогательной матрицы Для правильного расчёта краевых точек.
              Поверхность увеличивается по ширине и высоте на 2k(исходя из того, что размер маски равен (2k+1)х(2k+1)).
              Значение точек в расширенной области заполняются значениями краевых точек.
             */
            Color cl;
            int kk = 2 * k;
            decimal[,] bigPic = new decimal[bmp.Height + kk, bmp.Width + kk];// здесь будет храниться увеличенное изображение для избежания краевого эффекта

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

                    bigPic[y, x] = Convert.ToDecimal(cl.R);
                }
            return bigPic;
        }
        public static decimal[,] FormBigPic(decimal[,] imageArray, int k) //сформировать увеличенное (на k пикселей с каждой стороны) изображение
        {
            /*формирование вспомогательной матрицы Для правильного расчёта краевых точек.
              Поверхность увеличивается по ширине и высоте на 2k(исходя из того, что размер маски равен (2k+1)х(2k+1)).
              Значение точек в расширенной области заполняются значениями краевых точек.
             */
            decimal value;
            int kk = 2 * k;
            decimal[,] bigPic = new decimal[imageArray.GetLength(0) + kk, imageArray.GetLength(1) + kk];// здесь будет храниться увеличенное изображение для избежания краевого эффекта

            for (int x = 0; x < imageArray.GetLength(0) + kk; x++)
                for (int y = 0; y < imageArray.GetLength(1) + kk; y++)
                {
                    if (y <= k)
                    {
                        if (x < k) value = imageArray[0, 0];
                        else if (x >= (imageArray.GetLength(1) + k)) value = imageArray[imageArray.GetLength(1) - 1, 0];
                        else value = imageArray[x - k, 0];
                    }

                    else if (y >= (imageArray.GetLength(0) + k))
                    {
                        if (x < k) value = imageArray[0, imageArray.GetLength(0) - 1];
                        else if (x >= (imageArray.GetLength(1) + k)) value = imageArray[imageArray.GetLength(1) - 1, imageArray.GetLength(0) - 1];
                        else value = imageArray[x - k, imageArray.GetLength(0) - 1];
                    }

                    else
                    {
                        if (x < k) value = imageArray[0, y - k];
                        else if (x >= imageArray.GetLength(1) + k) value = imageArray[imageArray.GetLength(1) - 1, y - k];
                        else value = imageArray[x - k, y - k];
                    }

                    bigPic[x, y] = value;
                }
            return bigPic;
        }

        public static decimal[,] FormBigPic(decimal[,] imageArray, int kForWidth, int kForHeight)
        {
            decimal value;
            int kkWidth = 2 * kForWidth;
            int kkHeight = 2 * kForHeight;
            decimal[,] bigPic = new decimal[imageArray.GetLength(0) + kkHeight, imageArray.GetLength(1) + kkWidth];// здесь будет храниться увеличенное изображение для избежания краевого эффекта

            for (int x = 0; x < imageArray.GetLength(0) + kkHeight; x++)
                for (int y = 0; y < imageArray.GetLength(1) + kkWidth; y++)
                {
                    if (y <= kForWidth)
                    {
                        if (x < kForHeight) value = imageArray[0, 0];
                        else if (x >= (imageArray.GetLength(0) + kForHeight)) value = imageArray[imageArray.GetLength(0) - 1, 0];
                        else value = imageArray[x - kForHeight, 0];
                    }

                    else if (y >= (imageArray.GetLength(1) + kForWidth))
                    {
                        if (x < kForHeight) value = imageArray[0, imageArray.GetLength(1) - 1];
                        else if (x >= (imageArray.GetLength(0) + kForHeight)) value = imageArray[imageArray.GetLength(0) - 1, imageArray.GetLength(1) - 1];
                        else value = imageArray[x - kForHeight, imageArray.GetLength(1) - 1];
                    }

                    else
                    {
                        if (x < kForHeight) value = imageArray[0, y - kForWidth];
                        else if (x >= imageArray.GetLength(0) + kForHeight) value = imageArray[imageArray.GetLength(0) - 1, y - kForWidth];
                        else value = imageArray[x - kForHeight, y - kForWidth];
                    }

                    bigPic[x, y] = value;
                }
            return bigPic;
        }


        public static Bitmap Non_Maximum_Suppression(Bitmap bmp = null, decimal[,] imageArray = null)
        {
            if (bmp != null)
                suppressed = new decimal[bmp.Height + 2, bmp.Width + 2];
            else
                suppressed = new decimal[imageArray.GetLength(0) + 2, imageArray.GetLength(1) + 2];

            for (int x = 1; x < respond.GetLength(0) - 1; x++)
            {

                int indexUntilWhichChecked_0 = 0;
                int indexUntilWhichChecked_90 = 0;




                for (int y = 1; y < respond.GetLength(1) - 1; y++)/*цикл от 1 и до предпосл.элемента, т.к. respond - это увеличенное на 2 пикселя с каждой стороны изображение 
                                                            и при этом заполняется suppressed,кот. с каждой стороны на 1 пиксель меньше,чем respond. 
                                                            Избегается краевой эффект. */
                {
                    if (atan[x, y] == 0)
                    {
                        if (respond[x, y] > 0)

                        {
                            if (respond[x, y - 1] < respond[x, y] && respond[x, y] > respond[x, y + 1])
                            {
                                suppressed[x - 1, y - 1] = respond[x, y];
                            }
                            else if (respond[x, y + 1] < respond[x, y])
                                continue; 
                            else if (respond[x, y + 1] == respond[x, y] && respond[x, y - 1] < respond[x, y])
                            {
                                int count = 1;
                                int yNew = y + 1; ;
                                while (atan[x, yNew] == 0 && respond[x, yNew] == respond[x, y])
                                {
                                    count++;
                                    if (yNew + 1 < respond.GetLength(1))
                                        yNew++;
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (respond[x, yNew] < respond[x, y])
                                {
                                    suppressed[x - 1, y + (int)Math.Truncate(Convert.ToDouble(count / 2)) - 1] = respond[x, y + (int)Math.Truncate(Convert.ToDouble(count / 2))];
                                    indexUntilWhichChecked_0 = y + count - 1;
                                }
                            }
                        }
                    }
                    if (atan[x, y] == 45)
                    {
                        if (respond[x, y] > 0)
                        {
                            if (respond[x - 1, y - 1] < respond[x, y] && respond[x, y] > respond[x + 1, y + 1])
                            {
                                suppressed[x - 1, y - 1] = respond[x, y];
                            }
                            else if (respond[x + 1, y + 1] < respond[x, y])
                                continue; 
                            else if (respond[x + 1, y + 1] == respond[x, y] && respond[x - 1, y - 1] < respond[x, y])
                            {
                                int count = 1;
                                int xNew = x + 1;
                                int yNew = y + 1; ;
                                while (atan[xNew, yNew] == 45 && respond[xNew, yNew] == respond[x, y])
                                {
                                    count++;
                                    if ((yNew + 1 < respond.GetLength(1)) && ((xNew + 1) < respond.GetLength(0)))
                                    {
                                        xNew++;
                                        yNew++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (respond[xNew, yNew] < respond[x, y])
                                {
                                    suppressed[x + (int)Math.Ceiling(Convert.ToDouble(count / 2)) - 1, y + (int)Math.Ceiling(Convert.ToDouble(count / 2)) - 1] = respond[x + (int)Math.Ceiling(Convert.ToDouble(count / 2)), y + (int)Math.Ceiling(Convert.ToDouble(count / 2))];
                                }
                            }
                        }
                    }
                    if (atan[x, y] == 90)
                    {
                        if (respond[x, y] > 0)                      
                        {

                            if (respond[x - 1, y] < respond[x, y] && respond[x, y] > respond[x + 1, y])
                            {
                                suppressed[x - 1, y - 1] = respond[x, y];
                            }
                            else if (respond[x + 1, y] < respond[x, y])
                                continue; 
                            else if (respond[x + 1, y] == respond[x, y] && respond[x - 1, y] < respond[x, y])
                            {
                                int count = 1;
                                int xNew = x + 1;
                                while (atan[xNew, y] == 90 && respond[xNew, y] == respond[x, y])
                                {
                                    count++;
                                    if (xNew + 1 < respond.GetLength(0))
                                        xNew++;
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (respond[xNew, y] < respond[x, y])
                                {
                                    suppressed[x + (int)Math.Ceiling(Convert.ToDouble(count / 2)) - 1, y - 1] = respond[x + (int)Math.Ceiling(Convert.ToDouble(count / 2)), y];
                                    indexUntilWhichChecked_90 = x + count - 1;
                                }
                            }
                        }

                    }

                    if (atan[x, y] == 135)
                        if (respond[x, y] > 0)
                        {
                            if (respond[x - 1, y + 1] < respond[x, y] && respond[x, y] > respond[x + 1, y - 1])
                            {
                                suppressed[x - 1, y - 1] = respond[x, y];
                            }
                            else if (respond[x + 1, y - 1] < respond[x, y])
                                continue; 
                            else if (respond[x + 1, y - 1] == respond[x, y] && respond[x - 1, y + 1] < respond[x, y])
                            {
                                int count = 1;
                                int xNew = x + 1;
                                int yNew = y - 1; ;
                                while (atan[xNew, yNew] == 135 && respond[xNew, yNew] == respond[x, y])
                                {
                                    count++;
                                    if ((yNew - 1) >= 0 && (yNew - 1) < respond.GetLength(1) && ((xNew + 1) < respond.GetLength(0)))
                                    {
                                        xNew++;
                                        yNew--;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (respond[xNew, yNew] < respond[x, y])
                                {
                                    suppressed[x + (int)Math.Ceiling(Convert.ToDouble(count / 2)) - 1, y - (int)Math.Ceiling(Convert.ToDouble(count / 2)) - 1] = respond[x + (int)Math.Ceiling(Convert.ToDouble(count / 2)), y - (int)Math.Ceiling(Convert.ToDouble(count / 2))];
                                }
                            }
                        }
                }
            }
            Bitmap bmp1;
            if (bmp == null)
                bmp1 = new Bitmap(imageArray.GetLength(0), imageArray.GetLength(1));
            else
                bmp1 = new Bitmap(bmp);
            for (int x = 0; x < bmp1.Height; x++)
                for (int y = 0; y < bmp1.Width; y++)
                    bmp1.SetPixel(y, x, Color.FromArgb(Convert.ToInt32(suppressed[x + 1, y + 1]), Convert.ToInt32(suppressed[x + 1, y + 1]), Convert.ToInt32(suppressed[x + 1, y + 1])));

            return bmp1;

        }
        public static Bitmap Double_Threshold(Bitmap bmp, double bottomThreshold, double upperThreshold)
        {

            for (int x = 1; x < suppressed.GetLength(0) - 1; x++)
                for (int y = 1; y < suppressed.GetLength(1) - 1; y++)/*Цикл от 1 и до предпосл.элемента, т.к. suppressed - это увеличенное на 1 пиксель с каждой стороны
                                                                     изображение и подавление немаксимумов для крайних пикселей не нужно (но при этом наличие этих крайних
                                                                     пикселей является обязательным для работоспособности метода Hysteresis_Thresholding)*/
                {
                    if (suppressed[x, y] <= Convert.ToDecimal(bottomThreshold))
                    {
                        bmp.SetPixel(y - 1, x - 1, Color.Black);
                        suppressed[x, y] = 0;
                    }
                    else if (suppressed[x, y] >= Convert.ToDecimal(upperThreshold))
                    {
                        bmp.SetPixel(y - 1, x - 1, Color.White);
                        suppressed[x, y] = 255;
                    }
                    else if (suppressed[x, y] > Convert.ToDecimal(bottomThreshold) && suppressed[x, y] < Convert.ToDecimal(upperThreshold))
                    {
                        bmp.SetPixel(y - 1, x - 1, Color.YellowGreen);
                        suppressed[x, y] = 128;

                    }
                }
            return bmp;
        }

        public static Bitmap Hysteresis_Thresholding(Bitmap bmp, string whoCalles)
        {

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
            return bmp;
        }

 
        protected static decimal Max(decimal[,] arr)
        {
            decimal maxValue = arr[0, 0];
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j] > maxValue)
                        maxValue = arr[i, j];
            return maxValue;
        }

    }
}
