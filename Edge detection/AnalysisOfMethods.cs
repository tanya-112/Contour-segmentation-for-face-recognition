using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Medallion;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Edge_detection
{
    public partial class AnalysisOfMethods : Form
    {
        double sigma;
        double bottomThresholdCanny;
        double upperThresholdCanny;
        double bottomThresholdHaar;
        double upperThresholdHaar;
        int waveletLength;
        int widthOfBrightnessDiffer;
        double snr;
        static CannyForm CannyForm;
        static HaarForm HaarForm;



        public AnalysisOfMethods(double sigma,  double bottomThresholdCanny, double upperThresholdCanny, int waveletLength, double bottomThresholdHaar, double upperThresholdHaar, int widthOfBrightnessDiffer, double snr = -1)
        {
            InitializeComponent();
            this.sigma = sigma;
            this.bottomThresholdCanny = bottomThresholdCanny;
            this.upperThresholdCanny = upperThresholdCanny;
            this.waveletLength = waveletLength;
            this.bottomThresholdHaar = bottomThresholdHaar;
            this.upperThresholdHaar = upperThresholdHaar;
            this.widthOfBrightnessDiffer = widthOfBrightnessDiffer;
            this.snr = snr;
            double[] prettCrit = DoTheAnalysis(sigma, bottomThresholdCanny, upperThresholdCanny,
            waveletLength, bottomThresholdHaar, upperThresholdHaar, widthOfBrightnessDiffer, snr);
            this.Canny_label.Text = prettCrit[0].ToString();
            this.Haar_label.Text = prettCrit[1].ToString();
            CannyForm.Show();
            HaarForm.Show();

        }

        public AnalysisOfMethods()
        {
            InitializeComponent();          
        }

        public static double[] DoTheAnalysis(double sigma, double bottomThresholdCanny, double upperThresholdCanny,
            int waveletLength, double bottomThresholdHaar, double upperThresholdHaar, int widthOfBrightnessDiffer, double snr = -1)
        {
            decimal[,] origImage = MakeOriginImArr(widthOfBrightnessDiffer);
            decimal[,] origImageCopyForHaar = MakeOriginImArr(widthOfBrightnessDiffer);

            double[,] perfectEdgeImage = MakePerfectEdgeImArr();

            Bitmap bmpToSaveForTest = new Bitmap(origImage.GetLength(1), origImage.GetLength(0));
            for (int i = 0; i < origImage.GetLength(0); i++)
                for (int j = 0; j < origImage.GetLength(1); j++)
                    bmpToSaveForTest.SetPixel(j, i, Color.FromArgb((int)Math.Ceiling(origImage[i, j]), (int)Math.Ceiling(origImage[i, j]), (int)Math.Ceiling(origImage[i, j])));

            if (snr > -1)
            {
                origImage = AddGausNoise(origImage, snr);// зашумляем изображение
                origImageCopyForHaar = AddGausNoise(origImageCopyForHaar, snr);
            }

            CannyForm = new CannyForm(sigma, bottomThresholdCanny, upperThresholdCanny, bmp: null, imageArray: origImage); //создаем окно для вывода результатов метода Канни, передавая путь к выбранному файлу
            
            double[,] cannyResult = new double[CannyForm.cannyResult.Height, CannyForm.cannyResult.Width];
            for (int i = 0; i < cannyResult.GetLength(0); i++)
                for (int j = 0; j < cannyResult.GetLength(1); j++)
                    cannyResult[i, j] = CannyForm.cannyResult.GetPixel(j, i).R;
            HaarForm = new HaarForm (waveletLength, bottomThresholdHaar, upperThresholdHaar, bmp:null, imageArray: origImageCopyForHaar); //создаем окно для вывода результатов метода Хаара, передавая путь к выбранному файлу
            
            double[,] haarResult = new double[HaarForm.resultBmp.Height, HaarForm.resultBmp.Width];
            for (int i = 0; i < haarResult.GetLength(0); i++)
                for (int j = 0; j < haarResult.GetLength(1); j++)
                    haarResult[i, j] = HaarForm.resultBmp.GetPixel(j, i).R;

            double cannyPrettCrit = CountPrettCriteriaWithoutBorderPixels(perfectEdgeImage, cannyResult, waveletLength);
            double haarPrettCrit = CountPrettCriteriaWithoutBorderPixels(perfectEdgeImage, haarResult, waveletLength);

            double[] prettCrit = new double[2];
            prettCrit[0] = cannyPrettCrit;
            prettCrit[1] = haarPrettCrit;

            return prettCrit;
        }

        public static decimal[,] MakeOriginImArr(int widthOfBrightnessDiffer)
        {
            decimal[,] imageArr = new decimal[201, 201];
            decimal coeff = 255m / ((decimal)widthOfBrightnessDiffer);
            decimal x = 1; 

            for (int i = 0; i < 201; i++)
                for (int j = 0; j < 101; j++)
                {
                    imageArr[i, j] = 255;
                }          

            int j1 = 101 - (int)Math.Ceiling(Convert.ToDouble(widthOfBrightnessDiffer) / 2) + 1;
            for (int i = 0; i < 201; i++)
            {
                x = 1;
                j1 = 101 - (int)Math.Ceiling(Convert.ToDouble(widthOfBrightnessDiffer) / 2) + 1;
                for (int z = 0; z < widthOfBrightnessDiffer; z++)
                {
                    imageArr[i, j1] = x * (decimal)(-coeff) + (decimal)255;
                    x++;
                    j1++;
                }
            }
          
            return imageArr;
        }

        public static double[,] MakePerfectEdgeImArr()
        {
            double[,] imageArr = new double[201, 201];

            for (int i = 0; i < 201; i++)
            {
                    imageArr[i, 101] = 255;// вертикальная полоса (идеальный перепад)

            }

            return imageArr;
        }
 
        public static double CountPrettCriteriaWithoutBorderPixels(double[,] perfectResult, double[,] methodResult, int widthOfIgnoringFromSide)
        {
            List<int>[] perfectEdgeIndecesPerRow = FindPerfectEdgeIndices(perfectResult);
            List<int>[] methodEdgeIndecesPerRow = FindMethodEdgeIndices(methodResult, perfectEdgeIndecesPerRow);
            double R = 0; // коэффициент Прэтта
            int iI = 0; //число точек перепада в идеальном контурном препарате
            int iA = 0; //число точек перепада в реальном контурном препарате

            for (int i = widthOfIgnoringFromSide; i < perfectResult.GetLength(0) - widthOfIgnoringFromSide; i++)
                for (int j = widthOfIgnoringFromSide; j < perfectResult.GetLength(1)- widthOfIgnoringFromSide; j++)
                {
                    if (perfectResult[i, j] == 255)
                        iI++;
                }
            for (int i = widthOfIgnoringFromSide; i < methodResult.GetLength(0) - widthOfIgnoringFromSide; i++)
                for (int j = widthOfIgnoringFromSide; j < methodResult.GetLength(1)- widthOfIgnoringFromSide; j++)
                {
                    if (methodResult[i, j] == 255)
                        iA++;
                }

            int In = Math.Max(iI, iA);

            for (int i = widthOfIgnoringFromSide; i < perfectEdgeIndecesPerRow.GetLength(0)- widthOfIgnoringFromSide; i++)//было for (int i = 0; i < methodEdgeIndecesPerRow.GetLength(0); i++)
            {
                int iAInnerInRow = 0;
                for (int j = widthOfIgnoringFromSide; j < methodResult.GetLength(1)- widthOfIgnoringFromSide; j++) // -1????????
                {
                    if (methodResult[i, j] == 255)
                        iAInnerInRow++;
                }
    
                int numberOfPixInARowInPerfectInd = 1;
                int k = 0;
                for (int j = 0; j < iAInnerInRow; j++)
                {
                    double distance = Convert.ToDouble(Math.Pow(methodEdgeIndecesPerRow[i][j] - perfectEdgeIndecesPerRow[i][k], 2));
                    R += 1.0 / (1.0 + (1.0 / 9.0) * distance);
                    if (k < numberOfPixInARowInPerfectInd - 1)
                        k++;
                }
            }
            R = R / Convert.ToDouble(In);
            return R;
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

            int In = Math.Max(iI, iA);

            for (int i = 0; i < perfectEdgeIndecesPerRow.GetLength(0); i++)
            {              
                int iAInnerInRow = 0;
                for (int j = 0; j < methodResult.GetLength(1); j++)
                {
                    if (methodResult[i, j] == 255)
                        iAInnerInRow++;
                }

                int numberOfPixInARowInPerfectInd = perfectEdgeIndecesPerRow[i].Count;
                int k = 0;
                for (int j = 0; j < iAInnerInRow; j++)
                {
                    double distance = Convert.ToDouble(Math.Pow(methodEdgeIndecesPerRow[i][j] - perfectEdgeIndecesPerRow[i][k], 2));
                    R += 1.0 / (1.0 + (1.0 / 9.0) * distance);
                    if (k < numberOfPixInARowInPerfectInd - 1)
                        k++;
                }
            }
            R = R / Convert.ToDouble(In);
            return R;
        }


        public static List<int>[] FindPerfectEdgeIndices(double[,] perfectResult)
        {            
            int numberOfRows = perfectResult.GetLength(0);
            
            List<int>[] indecesPerRow = new List<int>[numberOfRows];// массив списков, где i-ый список содержит индексы столбцов с ненулевыми элементами в рамках i-ой строки

            for (int i = 0; i < perfectResult.GetLength(0); i++)
            {
                indecesPerRow[i] = new List<int>();
                for (int j = 0; j < perfectResult.GetLength(1); j++)
                {
                    if (perfectResult[i, j] == 255)
                        indecesPerRow[i].Add(j);
                }
            }
            return indecesPerRow;
        }

        public static List<int>[] FindMethodEdgeIndices(double[,] methodResult, List<int>[] perfect_IndecesPerRow)
        {
            int numberOfRows = methodResult.GetLength(0);         
            List<int>[] indecesPerRow = new List<int>[numberOfRows];// массив списков, где i-ый список содержит индексы столбцов с ненулевыми элементами в рамках i-ой строки
            for (int i = 0; i < methodResult.GetLength(0); i++)
            {
                indecesPerRow[i] = new List<int>();
                for (int j = 0; j < methodResult.GetLength(1); j++)// перебираем все элементы в i-ом списке
                {                    
                        //добавляем в i-ый список (т.е. список для i-ой строки) индекс найденного элемента или индекс крайнего элемента в строке, если элемент отсутствует
                        if (methodResult[i, j] == 255)
                            indecesPerRow[i].Add(j);
                }
            }
            return indecesPerRow;
        }


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
                    int color = bmp.GetPixel(j, i).R + Convert.ToInt32(value);
                    if (color > 255)
                        color = 255;
                    if (color < 0)
                        color = 0;
                    bmp.SetPixel(j, i, Color.FromArgb(Convert.ToInt32(color), Convert.ToInt32(color), Convert.ToInt32(color)));
                }

            return bmp;
        }

        public static decimal[,] AddGausNoise(decimal[,] origImage, double snr)
        {
            var random = new Random();
            int height = origImage.GetLength(0);
            int width = origImage.GetLength(1);
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    double noiseValue = random.NextGaussian();
                    double value = 255 * (1 / Math.Sqrt(snr)) * noiseValue;// SNR - отношение сигнал/шум
                    decimal newColor = origImage[j, i] + Convert.ToDecimal(value);
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
