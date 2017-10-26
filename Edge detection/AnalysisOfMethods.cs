using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Medallion;
using System.Runtime.InteropServices;

namespace Edge_detection
{
    public partial class AnalysisOfMethods : Form
    {
        //double PrettCriteria;
        //double correctness;
        //double efficiency;

        double cannyPrettCrit;
        double haarPrettCrit;
        double sigma;
        short k;
        double bottomThresholdCanny;
        double upperThresholdCanny;
        double bottomThresholdHaar;
        double upperThresholdHaar;
        int waveletLength;
        //bool noiseImage;
        int widthOfBrightnessDiffer;
        double snr;
        static Canny CannyForm;
        static Haar HaarForm;


        //enum prettCritt { cannyPrettCrit, haarPrettCrit };

        //public AnalysisOfMethods(double PrettCriteria, double correctness, double efficiency)
        public AnalysisOfMethods(double sigma, short k, double bottomThresholdCanny, double upperThresholdCanny, int waveletLength, double bottomThresholdHaar, double upperThresholdHaar, int widthOfBrightnessDiffer, double snr = -1)
        {
            InitializeComponent();
            this.sigma = sigma;
            this.k = k;
            this.bottomThresholdCanny = bottomThresholdCanny;
            this.upperThresholdCanny = upperThresholdCanny;
            this.waveletLength = waveletLength;
            this.bottomThresholdHaar = bottomThresholdHaar;
            this.upperThresholdHaar = upperThresholdHaar;
            //this.noiseImage = noiseImage;
            this.widthOfBrightnessDiffer = widthOfBrightnessDiffer;
            this.snr = snr;
            //this.PrettCriteria = PrettCriteria;
            //this.correctness = correctness;
            //this.efficiency = efficiency;
        }

        public AnalysisOfMethods()
        {
            InitializeComponent();
           
        }
        private void AnalysisOfMethods_Load(object sender, EventArgs e)
        {
            //bool enableNoisingImage = false;
            //if (Form1.enableNoisingImage_checkBoxChecked)
            //    enableNoisingImage = true;

            DoTheAnalysis(sigma, k, bottomThresholdCanny, upperThresholdCanny,
              waveletLength, bottomThresholdHaar, upperThresholdHaar, widthOfBrightnessDiffer, snr);
            CannyForm.Show();
            HaarForm.Show();
        }


        public static double[] DoTheAnalysis(double sigma, short k, double bottomThresholdCanny, double upperThresholdCanny,
            int waveletLength, double bottomThresholdHaar, double upperThresholdHaar, int widthOfBrightnessDiffer, double snr = -1)
        {
            double[,] origImage = MakeOriginImArr(widthOfBrightnessDiffer);
            double[,] origImageCopyForHaar = MakeOriginImArr(widthOfBrightnessDiffer);

            double[,] perfectEdgeImage = MakePerfectEdgeImArr();

            //float[][] tempImage;
            //tempImage = ToFloat<double>(origImage);
            //Bitmap res = ToBitmapNoLinear24BitGray(tempImage);
            ////res.Save(fileName, ImageFormat.Bmp);

            //Bitmap bmp = new Bitmap(origImage.GetLength(0), origImage.GetLength(1));
            //for (int i = 0; i < origImage.GetLength(0); i++)
            //    for (int j = 0; j < origImage.GetLength(1); j++)
            //        bmp.SetPixel(j, i, Color.FromArgb((byte)(origImage[i, j]), (byte)(origImage[i, j]), (byte)(origImage[i, j])));
            //if (noiseImage)
            //    bmp = AddGausNoise(bmp, snr);// зашумляем изображение

            if (snr > -1)
            {
                origImage = AddGausNoise(origImage, snr);// зашумляем изображение
                origImageCopyForHaar = AddGausNoise(origImageCopyForHaar, snr);
            }

            CannyForm = new Canny(sigma, k, bottomThresholdCanny, upperThresholdCanny, bmp: null, imageArray: origImage); //создаем окно для вывода результатов метода Канни, передавая путь к выбранному файлу
            //CannyForm.Show();
            // Bitmap cannyResBmp = // СЧИТАТЬ ИЗ ПИКЧЕРБОКСА 4 ИЗОБРАЖЕНИЕ
            double[,] cannyAlmostResult = Form1.suppressed;
            double[,] cannyResult = new double[cannyAlmostResult.GetLength(0) - 2, cannyAlmostResult.GetLength(1) - 2];
            for (int i = 0; i < cannyResult.GetLength(0); i++)
                for (int j = 0; j < cannyResult.GetLength(1); j++)
                {
                    cannyResult[i, j] = cannyAlmostResult[i + 1, j + 1];
                }
            HaarForm = new Haar (waveletLength, bottomThresholdHaar, upperThresholdHaar, bmp:null, imageArray: origImageCopyForHaar); //создаем окно для вывода результатов метода Хаара, передавая путь к выбранному файлу
            //HaarForm.Show();
            //double[,] haarAlmostResult = Form1.suppressed;
            //double[,] haarResult = new double[haarAlmostResult.GetLength(0) - 2, haarAlmostResult.GetLength(1) - 2];
            double[,] haarResult = new double[HaarForm.resultBmp.Height, HaarForm.resultBmp.Width];
            for (int i = 0; i < haarResult.GetLength(0); i++)
                for (int j = 0; j < haarResult.GetLength(1); j++)
                    haarResult[i, j] = HaarForm.resultBmp.GetPixel(j, i).R;
            //for (int i = 0; i < haarResult.GetLength(0); i++)
            //    for (int j = 0; j < haarResult.GetLength(1); j++)
            //    {
            //        haarResult[i, j] = haarAlmostResult[i + 1, j + 1];
            //    }

            double cannyPrettCrit = CountPrettCriteria(perfectEdgeImage, cannyResult);
            double haarPrettCrit = CountPrettCriteria(perfectEdgeImage, haarResult);
            //prettCritt.cannyPrettCrit = cannyPrettCrit;
            double[] prettCrit = new double[2];
            prettCrit[0] = cannyPrettCrit;
            prettCrit[1] = haarPrettCrit;

            return prettCrit;
            //textBox1.Text = cannyPrettCrit.ToString();
            //textBox2.Text = haarPrettCrit.ToString();
            //textBox3.Text = efficiency.ToString();
        }

        public static double[,] MakeOriginImArr(int widthOfBrightnessDiffer)
        {
            double[,] imageArr = new double[201, 201];
            double coeff = 255.00 / Convert.ToDouble(widthOfBrightnessDiffer);
            int x = 1; // будет, увеличиваясь, подставляться в уравнение y = -x + widthOfBrightnessDiffer?????????????????


            for (int i = 0; i < 201; i++)
                for (int j = 0; j < 101; j++)
                {
                    imageArr[i, j] = 255;
                }
            //строим левый перепад
            x = 1;
            //int iStart = 50 - widthOfBrightnessDiffer + 1;
            //int iEnd = 150 + widthOfBrightnessDiffer - 1;

            for (int j = 101 - widthOfBrightnessDiffer + 1; j < 50; j++)// было for (int j = 0; j < 200; j++)
            {
                for (int i = 0; i < 201; i++)// по смыслу то же, что и for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)
                {
                    imageArr[i, j] = x * (-coeff) + 255;
                }
                x++;
                //iStart++;
                //iEnd--;
            }

            //строим правый перепад
            x = 1;
            //iStart = 50 - widthOfBrightnessDiffer + 1;
            //iEnd = 150 + widthOfBrightnessDiffer - 1;

            for (int j = 101; j < 101 + widthOfBrightnessDiffer; j++)
            {
                for (int i = 0; i < 201; i++)
                {
                    imageArr[i, j] = x * (-coeff) + 255;
                }
                x++;
                //iStart++;
                //iEnd--;
            }
            //for (int i = 0; i < 201; i++)
            //    for (int j = 101; j < 201; j++)
            //    {
            //        imageArr[i, j] = 0;
            //    }

            return imageArr;
        }

        public static double[,] MakeOriginImArrSquareInSquare(int widthOfBrightnessDiffer)
        {
            double coeff = 255.00 / Convert.ToDouble(widthOfBrightnessDiffer);
            double[,] imageArr = new double[200, 200];
            int x = 1; // будет, увеличиваясь, подставляться в уравнение y = -x + widthOfBrightnessDiffer?????????????????


            //создаем массив изображения "квадрат в квадрате"

            // строим ВНЕШНИЙ КВАДРАТ
            for (int i = 0; i < 50 - widthOfBrightnessDiffer + 1; i++) // белая верхняя полоса
                for (int j = 0; j < 200; j++)
                {
                    imageArr[i, j] = 255;
                }

            for (int i = 150 + widthOfBrightnessDiffer; i < 200; i++) // белая нижняя полоса
                for (int j = 0; j < 200; j++)
                    imageArr[i, j] = 255;


            for (int i = 50 - widthOfBrightnessDiffer + 1; i < 150 + widthOfBrightnessDiffer; i++)
                for (int j = 0; j < 50 - widthOfBrightnessDiffer + 1; j++) // белая левая полоса
                {
                    imageArr[i, j] = 255;
                }

            for (int i = 50 - widthOfBrightnessDiffer + 1; i < 150 + widthOfBrightnessDiffer; i++)
                for (int j = 150 + widthOfBrightnessDiffer; j < 200; j++) // белая правая полоса
                {
                    imageArr[i, j] = 255;
                }

            //строим верхний перепад
            int jStart = 50 - widthOfBrightnessDiffer + 1;
            int jEnd = 150 + widthOfBrightnessDiffer - 1;
            for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)// по смыслу то же, что и for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)
            {
                for (int j = jStart; j < jEnd; j++)// было for (int j = 0; j < 200; j++)
                {
                    imageArr[i, j] = x * (-coeff) + 255;
                }
                x++;
                jStart++;
                jEnd--;
            }


            //строим нижний перепад
            x = 1;
            jStart = 50 - widthOfBrightnessDiffer + 1;
            jEnd = 150 + widthOfBrightnessDiffer;
            for (int i = 150 + widthOfBrightnessDiffer - 1; i > 150; i--)// по смыслу то же, что и for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)
            {
                for (int j = jStart; j < jEnd; j++)// было for (int j = 0; j < 200; j++)
                {
                    imageArr[i, j] = x * (-coeff) + 255;
                }
                x++;
                jStart++;
                jEnd--;
            }


            //строим левый перепад
            x = 1;
            int iStart = 50 - widthOfBrightnessDiffer + 1;
            int iEnd = 150 + widthOfBrightnessDiffer - 1;

            for (int j = 50 - widthOfBrightnessDiffer + 1; j < 50; j++)// было for (int j = 0; j < 200; j++)
            {
                for (int i = iStart; i < iEnd; i++)// по смыслу то же, что и for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)
                {
                    imageArr[i, j] = x * (-coeff) + 255;
                }
                x++;
                iStart++;
                iEnd--;
            }

            //строим правый перепад
            x = 1;
            iStart = 50 - widthOfBrightnessDiffer + 1;
            iEnd = 150 + widthOfBrightnessDiffer - 1;

            for (int j = 150 + widthOfBrightnessDiffer - 1; j > 150; j--)
            {
                for (int i = iStart; i < iEnd; i++)
                {
                    imageArr[i, j] = x * (-coeff) + 255;
                }
                x++;
                iStart++;
                iEnd--;
            }

            // строим ВНУТРЕННИЙ КВАДРАТ

            for (int i = 75; i < 125; i++)
                for (int j = 75; j < 125; j++)
                    imageArr[i, j] = 255;

            //строим верхний перепад
            x = 1;
            jStart = 75 - widthOfBrightnessDiffer + 1;
            jEnd = 125 + widthOfBrightnessDiffer - 1;
            for (int i = 75 - widthOfBrightnessDiffer + 1; i < 75; i++)// по смыслу то же, что и for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)
            {
                for (int j = jStart; j < jEnd; j++)// было for (int j = 0; j < 200; j++)
                {
                    imageArr[i, j] = x * (coeff);
                }
                x++;
                jStart++;
                jEnd--;
            }


            x = 1;
            //строим нижний перепад
            jStart = 75 - widthOfBrightnessDiffer + 1;
            jEnd = 125 + widthOfBrightnessDiffer;
            for (int i = 125 + widthOfBrightnessDiffer - 1; i >= 125; i--)// по смыслу то же, что и for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)
            {
                for (int j = jStart; j < jEnd; j++)// было for (int j = 0; j < 200; j++)
                {
                    imageArr[i, j] = x * (coeff);
                }
                x++;
                jStart++;
                jEnd--;
            }


            //строим левый перепад
            x = 1;
            iStart = 75 - widthOfBrightnessDiffer + 1;
            iEnd = 125 + widthOfBrightnessDiffer - 1;

            for (int j = 75 - widthOfBrightnessDiffer + 1; j < 75; j++)// было for (int j = 0; j < 200; j++)
            {
                for (int i = iStart; i < iEnd; i++)// по смыслу то же, что и for (int i = 50 - widthOfBrightnessDiffer + 1; i < 50; i++)
                {
                    imageArr[i, j] = x * (coeff);
                }
                x++;
                iStart++;
                iEnd--;
            }

            //строим правый перепад
            x = 1;
            iStart = 75 - widthOfBrightnessDiffer + 1;
            iEnd = 125 + widthOfBrightnessDiffer - 1;

            for (int j = 125 + widthOfBrightnessDiffer - 1; j >= 125; j--)
            {
                for (int i = iStart; i < iEnd; i++)
                {
                    imageArr[i, j] = x * (coeff);
                }
                x++;
                iStart++;
                iEnd--;
            }
            return imageArr;
        }

        public static double[,] MakePerfectEdgeImArr()
        {
            double[,] imageArr = new double[201, 201];

            //for (int i = 0; i < 200; i++) 
            //    

            for (int i = 0; i < 201; i++)
            {
                //for (int j = 0; j < 100; j++)
                    //    {
                    //        imageArr[i, j] = 0;
                    //    }

                    imageArr[i, 101] = 255;// вертикальная полоса (идеальный перепад)
                    //    for (int j = 102; j< 201; j++)
                    //    {
                    //        imageArr[i, j] = 0;
                    //    }
            }

            //for (int i = 0; i< 200; i++)

            return imageArr;
        }
    public static double [,] MakePerfectEdgeImArrSquareInSquare()
        {
            double[,] imageArr = new double[200, 200];

            for (int j = 50; j < 150; j++)
            {
                imageArr[50, j] = 255;
                imageArr[150, j] = 255;
            }

            for(int i = 50; i <150; i++)
            {
                imageArr[i, 50] = 255;
                imageArr[i, 151] = 255;
            }

            for (int j = 75; j < 125; j++)
            {
                imageArr[75, j] = 255;
                imageArr[125, j] = 255;
            }

            for (int i = 75; i < 125; i++)
            {
                imageArr[i, 75] = 255;
                imageArr[i, 126] = 255;
            }
            return imageArr;
        }

        public static double CountPrettCriteria(double[,] perfectResult, double[,] methodResult)
        {
            List<int>[] perfectEdgeIndecesPerRow = FindPerfectEdgeIndices(perfectResult);
            List<int>[] methodEdgeIndecesPerRow = FindMethodEdgeIndices(methodResult, perfectEdgeIndecesPerRow);
            double R = 0; // коэффициент Прэтта
            int iI = 0; //число точек перепада в идеальном контурном препарате
            int iA = 0; //число точек перепада в реальном контурном препарате

            for (int i = 0; i < perfectResult.GetLength(0); i++)
                for (int j = 0; j < perfectResult.GetLength(1); j++)
                {
                    if (perfectResult[i, j] == 255)
                    iI++;
                }
            for (int i = 0; i < methodResult.GetLength(0); i++)
                for(int j = 0; j < methodResult.GetLength(1); j++)
                {
                    if (methodResult[i, j] == 255)
                        iA++;
                }

            //вычисляем массив расстояний реального и идеального контуров для каждой строки
            double distanse = 0;
            //for (int i = 0; i < perfectEdgeIndecesPerRow.Count; i++)//двигаемся от одного списка к другому
            //    for (int j = 0; j < perfectEdgeIndecesPerRow[i].Count; j++)//перебираем элементы в пределах i-го списка
            //{
            //    distanse = methodEdgeIndecesPerRow[i][j] - perfectEdgeIndecesPerRow[i][j];
            //}
            int minCount = 0;
            int In = Math.Max(iI, iA);
            //for (int i = 0; i < iA; i++)
            for (int i = 0; i < perfectEdgeIndecesPerRow.GetLength(0); i++)//было for (int i = 0; i < methodEdgeIndecesPerRow.GetLength(0); i++)
            {
                //if (methodEdgeIndecesPerRow[i].Length < perfectEdgeIndecesPerRow[i].Count)
                //    minCount = methodEdgeIndecesPerRow[i].Length;
                //else minCount = perfectEdgeIndecesPerRow[i].Count;
                //for (int j = 0; j < minCount; j++)
                // for (int i = 0; i < methodResult.GetLength(0); i++)
                int iAInnerInRow = 0;
                for (int j = 0; j < methodResult.GetLength(1); j++)
                {
                    if (methodResult[i, j] == 255)
                        iAInnerInRow++;
                }
                //int indexOfColomnForPerfectEdge = perfectEdgeIndecesPerRow[i][j];
                int numberOfPixInARowInPerfectInd = perfectEdgeIndecesPerRow[i].Count;
                int k = 0;
                for (int j = 0; j < iAInnerInRow; j++)// было for (int j = 0; j < perfectEdgeIndecesPerRow[i].Count; j++)
                {
                    double distance = Convert.ToDouble(Math.Pow(methodEdgeIndecesPerRow[i][j] - perfectEdgeIndecesPerRow[i][k], 2));
                    R += 1.0 / (1.0 + (1.0 / 9.0) * distance);
                    if (k < numberOfPixInARowInPerfectInd - 1)
                        k++;
                }
            }
            R = R / In;
            return R;
        }

        //ЭТО МЕТОД ДЛЯ МНОГОМЕРНОГО СЛУЧАЯ, Т.Е. КОГДА ИДЕАЛЬНЫЙ КОНТУРНЫЙ ПРЕПАРАТ ЯВЛЯЕТСЯ НЕ ВЕРТИК.ЛИНИЕЙ, А ФИГУРОЙ,
        //ГДЕ В ОДНОЙ СТРОКЕ МОЖЕТ БЫТЬ НЕСКОЛЬКО КОНТУРНЫХ ТОЧЕК, А НЕ ОДНА, КАК В СЛУЧАЕ С ВЕРТИК.ЛИНИЕЙ
        public static List<int>[] FindPerfectEdgeIndices(double[,] perfectResult)
        {
            //int j = 0; // номер столбца в массиве
            //int k = 0; // номер найденного элемента в пределах строки
            //int[][] indecesPerRow = new int[][] {};
            int numberOfRows = perfectResult.GetLength(0);
            //List<int> [] indecesPerRow = new List<int>[numberOfRows]{ };// массив списков, где i-ый список содержит индексы столбцов с ненулевыми элементами в рамках i-ой строки
            List<int>[] indecesPerRow = new List<int>[numberOfRows];// массив списков, где i-ый список содержит индексы столбцов с ненулевыми элементами в рамках i-ой строки

            for (int i = 0; i < perfectResult.GetLength(0); i++)
            {
                //indecesPerRow.Add(new List<int>());// создали список indecesPerRow[i]
                indecesPerRow[i] = new List<int>();
                for (int j = 0; j < perfectResult.GetLength(1); j++)
                {
                    if (perfectResult[i, j] == 255)
                        indecesPerRow[i].Add(j);
                    // k++;
                }
            }
            return indecesPerRow;
        }

        public static List<int>[] FindMethodEdgeIndices(double[,] methodResult, List<int>[] perfect_IndecesPerRow)
        {
            int numberOfRows = methodResult.GetLength(0);
            //int numberOfCol = methodResult.
            List<int>[] indecesPerRow = new List<int>[numberOfRows];// массив списков, где i-ый список содержит индексы столбцов с ненулевыми элементами в рамках i-ой строки
            for (int i = 0; i < methodResult.GetLength(0); i++)
            {
                //indecesPerRow.Add(new List<int>());// создали список indecesPerRow[i]
                indecesPerRow[i] = new List<int>();
                //indecesPerRow[i] = new int[perfect_IndecesPerRow[i].Count];
                for (int j = 0; j < methodResult.GetLength(1); j++)// перебираем все элементы в i-ом списке
                {
                    
                        //добавляем в i-ый список (т.е. список для i-ой строки) индекс найденного элемента или индекс крайнего элемента в строке, если элемент отсутствует
                        if (methodResult[i, j] == 255)
                            indecesPerRow[i].Add(j);
                            //indecesPerRow[i][j] = kGoOneSide;
                }
            }
            return indecesPerRow;
        }



        //public static int [][] FindMethodEdgeIndices(double[,] methodResult, List<int>[] perfect_IndecesPerRow)
        //{
        //    bool foundEdgePix = false;
        //    bool notOutOfSequence = true;
        //    int numberOfRows = methodResult.GetLength(0);
        //    //int numberOfCol = methodResult.
        //    int [][] indecesPerRow = new int[numberOfRows][];// массив списков, где i-ый список содержит индексы столбцов с ненулевыми элементами в рамках i-ой строки
        //    for (int i = 0; i < methodResult.GetLength(0); i++)
        //    {
        //        //indecesPerRow.Add(new List<int>());// создали список indecesPerRow[i]
        //        //indecesPerRow[i] = new List<int>();
        //        indecesPerRow[i] = new int[perfect_IndecesPerRow[i].Count];
        //        for (int j = 0; j < perfect_IndecesPerRow[i].Count; j++)// перебираем все элементы в i-ом списке
        //        {
        //            int k = perfect_IndecesPerRow[i][j]; // индекс для передвижения в массиве methodResult = j-му элементу i-го списка
        //            int kGoOneSide = k;
        //            int kGoAnotherSide = k;

        //            if (perfect_IndecesPerRow[i].Count <= 4)// если список хранит элементы вертикальных контуров (сканируем влево и вправо от k)
        //            {
        //                while (foundEdgePix == false && kGoOneSide > 0 && kGoAnotherSide < (methodResult.GetLength(1) - 1) && methodResult[i, kGoOneSide] != 255 && methodResult[i, kGoAnotherSide] != 255)
        //                {
        //                    kGoOneSide--;
        //                    kGoAnotherSide++;
        //                }
        //                //добавляем в i-ый список (т.е. список для i-ой строки) индекс найденного элемента или индекс крайнего элемента в строке, если элемент отсутствует
        //                if ((methodResult[i, kGoOneSide] == 255) || kGoOneSide == 0)
        //                    //indecesPerRow[i].Add(kGoOneSide);
        //                    indecesPerRow[i][j]=kGoOneSide;
        //                else if ((methodResult[i, kGoAnotherSide] == 255)||(kGoAnotherSide == (methodResult.GetLength(1) - 1)))
        //                    //indecesPerRow[i].Add(kGoAnotherSide);
        //                    indecesPerRow[i][j] = kGoAnotherSide;
        //            }
        //            else// если список хранит элементы горизонтальных контуров (сканируем вверх и вниз от k)
        //            {

        //                while (kGoOneSide > 0 && kGoAnotherSide < (methodResult.GetLength(0) - 1) && methodResult[kGoOneSide, i] != 255 && methodResult[kGoAnotherSide, i] != 255)
        //                {
        //                    kGoOneSide--;
        //                    kGoAnotherSide++;
        //                }
        //                //добавляем в i-ый список (т.е. список для i-ой строки) индекс найденного элемента или индекс крайнего элемента в столбце, если элемент отсутствует
        //                if ((methodResult[kGoOneSide, i] == 255)||kGoOneSide == 0)
        //                    //indecesPerRow[i].Add(kGoOneSide);
        //                    indecesPerRow[i][j]= kGoOneSide;
        //                else if ((methodResult[kGoAnotherSide, i] == 255)||(kGoAnotherSide == (methodResult.GetLength(0) - 1)))
        //                    //indecesPerRow[i].Add(kGoAnotherSide);
        //                    indecesPerRow[i][j] = kGoAnotherSide;


        //                //while (kGoOneSide > 0 && kGoAnotherSide < (methodResult.GetLength(0) - 1) && methodResult[kGoOneSide, i] != 255 && methodResult[kGoAnotherSide, i] != 255)
        //                //{
        //                //    kGoOneSide--;
        //                //    kGoAnotherSide++;
        //                //}
        //                ////добавляем в i-ый список (т.е. список для i-ой строки) найденный элемент
        //                //if (methodResult[kGoOneSide, i] == 255)
        //                //    indecesPerRow[i].Add(kGoOneSide);
        //                //if (methodResult[kGoAnotherSide, i] == 255)
        //                //    indecesPerRow[i].Add(kGoAnotherSide);
        //            }
        //        }
        //    }
        //    return indecesPerRow;
        //}

        public static Bitmap AddGausNoise(Bitmap bmp, double snr)
        {
            var random = new Random();
            int height = bmp.Height;
            int width = bmp.Width;
            for(int i = 0; i < height; i++)
                for(int j = 0; j < width; j++)
                {
                    int q = random.Next(100);
                    double value = 255 * (1.00 / Math.Sqrt(snr)) * random.NextGaussian();// SNR - отношение сигнал/шум
                    //double x1 = 1 - random.NextDouble();
                    //double x2 = 1 - random.NextDouble();

                    //double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
                    //double value =  y1 * 1.00/Math.Sqrt(10.00)*255;
                    //if (q <= 40)
                    int color = bmp.GetPixel(j, i).R + Convert.ToInt32(value);
                    if (color > 255)
                        color = 255;
                    if (color < 0)
                        color = 0;
                    bmp.SetPixel(j, i, Color.FromArgb(Convert.ToInt32(color), Convert.ToInt32(color), Convert.ToInt32(color)));
                }

            return bmp;
        }

        public static double[,] AddGausNoise(double[,] origImage, double snr)
        {
            var random = new Random();
            int height = origImage.GetLength(0);
            int width = origImage.GetLength(1);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    int q = random.Next(100);
                    double value = 255 * (1.00 / Math.Sqrt(snr)) * random.NextGaussian();// SNR - отношение сигнал/шум
                    //double x1 = 1 - random.NextDouble();
                    //double x2 = 1 - random.NextDouble();

                    //double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
                    //double value =  y1 * 1.00/Math.Sqrt(10.00)*255;
                    //if (q <= 40)
                    double newColor = origImage[j, i] + value;
                    if (newColor > 255)
                        newColor = 255;
                    if (newColor < 0)
                        newColor = 0;
                    origImage[j, i]= newColor;
                }

            return origImage;
        }
    }
}
