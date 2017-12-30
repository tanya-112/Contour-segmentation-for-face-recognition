using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edge_detection
{
    class CannyWithHaarWavelet : ImageProcessing
    {
        public static Bitmap HaarWaveletHorisontal(Bitmap bmp, int filterLength)
        {
            int[] haarMatrix = new int[filterLength];
            for (int i = 0; i < filterLength / 2; i++)
                haarMatrix[i] = 1;
            for (int i = filterLength / 2; i < filterLength; i++)
                haarMatrix[i] = -1;


            decimal resX = 0;//будет сканировать значения в пределах строки, т.е. реагировать на вертикальные контуры
            decimal[,] respondX = new decimal[bmp.Height + 4, bmp.Width + 4];
            decimal[,] respondY = new decimal[bmp.Height + 4, bmp.Width + 4];
            respond = new decimal[bmp.Height + 4, bmp.Width + 4];
            atan = new decimal[bmp.Height + 4, bmp.Width + 4];

            decimal[,] bigPic = new decimal[respond.GetLength(0) + filterLength, respond.GetLength(1) + filterLength];
            addToBmp = (bigPic.GetLength(1) - bmp.Width) / 2;
            bigPic = FormBigPic(bmp, addToBmp);
            for (int x = 0; x < bmp.Height + 4; x++)
                for (int y = 0; y < bmp.Width + 4; y++)
                {
                    resX = 0;
                    for (int u = 0; u < filterLength; u++)
                    {
                        resX += haarMatrix[u] * bigPic[x + filterLength / 2, y + u];
                    }
                    
                    atan[x, y] = 0;                    
                    respondX[x, y] = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(resX * resX)));                  
                    respond[x, y] = respondX[x, y];
                }

            decimal maxResp = Max(respond);
            for (int i = 0; i < respond.GetLength(0); i++)
                for (int j = 0; j < respond.GetLength(1); j++)
                    respond[i, j] = (respond[i, j] / maxResp) * 255; //нормируем значение отклика от 0 до 255


            decimal maxRespond = Max(respond);

            for (int i = 0; i < respond.GetLength(0); i++)
                for (int j = 0; j < respond.GetLength(1); j++)
                    respond[i, j] = (respond[i, j] / maxRespond) * 255;//нормируем значение градиента от 0 до 255


            for (int x = 0; x < bmp.Height; x++)
                for (int y = 0; y < bmp.Width; y++)
                    bmp.SetPixel(y, x, Color.FromArgb(Convert.ToInt32(respond[x + 2, y + 2]), Convert.ToInt32(respond[x + 2, y + 2]), Convert.ToInt32(respond[x + 2, y + 2])));
            return bmp;
        }

        public static Bitmap HaarWaveletVertical(Bitmap bmp, int filterLength)
        {
            int[] haarMatrix = new int[filterLength];
            for (int i = 0; i < filterLength / 2; i++)
                haarMatrix[i] = 1;
            for (int i = filterLength / 2; i < filterLength; i++)
                haarMatrix[i] = -1;

            decimal resY = 0;//будет сканировать значения в пределах столбца, т.е. реагировать на горизонтальные контуры
            double[,] respondY = new double[bmp.Height + 4, bmp.Width + 4];
            respond = new decimal[bmp.Height + 4, bmp.Width + 4];
            atan = new decimal[bmp.Height + 4, bmp.Width + 4];


            decimal[,] bigPic = new decimal[respond.GetLength(0) + filterLength, respond.GetLength(1) + filterLength];
            int addToBmp = (bigPic.GetLength(1) - bmp.Width) / 2;
            bigPic = FormBigPic(bmp, addToBmp);

            for (int x = 0; x < bmp.Height + 4; x++)
                for (int y = 0; y < bmp.Width + 4; y++)
                {
                    resY = 0;
                    for (int u = 0; u < filterLength; u++)
                    {                       
                        resY += haarMatrix[u] * bigPic[x + u, y + filterLength / 2];
                    }

                    atan[x, y] = 90;                   
                    respondY[x, y] = Math.Sqrt(Convert.ToDouble(resY * resY));                   
                    respond[x, y] = Convert.ToDecimal(respondY[x, y]);
                }

            decimal maxResp = Max(respond);
            if (maxResp != 0)
            {
                for (int i = 0; i < respond.GetLength(0); i++)
                    for (int j = 0; j < respond.GetLength(1); j++)
                        respond[i, j] = (respond[i, j] / maxResp) * 255; //нормируем значение отклика от 0 до 255
            }
            for (int x = 0; x < bmp.Height; x++)
                for (int y = 0; y < bmp.Width; y++)
                    bmp.SetPixel(y, x, Color.FromArgb(Convert.ToInt32(respond[x + 2, y + 2]), Convert.ToInt32(respond[x + 2, y + 2]), Convert.ToInt32(respond[x + 2, y + 2])));
            return bmp;
        }


        public static Bitmap SumHorisAndVerticHaarResults(Bitmap HorisontalHaarResult, Bitmap VerticalHaarResult)
        {
            Bitmap summedBmp = new Bitmap(HorisontalHaarResult.Width, HorisontalHaarResult.Height);
            for (int x = 0; x < HorisontalHaarResult.Height; x++)
                for (int y = 0; y < HorisontalHaarResult.Width; y++)
                {
                    if (HorisontalHaarResult.GetPixel(y, x).R == 255 || VerticalHaarResult.GetPixel(y, x).R == 255)
                        summedBmp.SetPixel(y, x, Color.FromArgb(255, 255, 255));
                    else
                        summedBmp.SetPixel(y, x, Color.FromArgb(0, 0, 0));

                }
            return summedBmp;
        }


        public static decimal[,] HaarWaveletVertical(decimal[,] imageArray, int filterLength)
        {
            int[] haarMatrix = new int[filterLength];
            for (int i = 0; i < filterLength / 2; i++)
                haarMatrix[i] = 1;
            for (int i = filterLength / 2; i < filterLength; i++)
                haarMatrix[i] = -1;

            decimal resY = 0;//будет сканировать значения в пределах столбца, т.е. реагировать на горизонтальные контуры
            double[,] respondY = new double[imageArray.GetLength(0) + 4, imageArray.GetLength(1) + 4];
            respond = new decimal[imageArray.GetLength(0) + 4, imageArray.GetLength(1) + 4];
            atan = new decimal[imageArray.GetLength(0) + 4, imageArray.GetLength(1) + 4];


            decimal[,] bigPic = new decimal[respond.GetLength(0) + filterLength, respond.GetLength(1) + filterLength];
            int addToBmp = (bigPic.GetLength(1) - imageArray.GetLength(1)) / 2;
            bigPic = FormBigPic(imageArray, addToBmp, addToBmp);
            for (int x = 0; x < imageArray.GetLength(0) + 4; x++)
                for (int y = 0; y < imageArray.GetLength(1) + 4; y++)
                {
                    resY = 0;
                    for (int u = 0; u < filterLength; u++)
                    {
                        resY += haarMatrix[u] * bigPic[x + u, y + filterLength / 2];
                    }
                   
                    atan[x, y] = 90;
                   
                    respondY[x, y] = Math.Sqrt(Convert.ToDouble(resY * resY));

                    respond[x, y] = Convert.ToDecimal(respondY[x, y]);

                }
            decimal maxResp = Max(respond);
            if (maxResp != 0)
            {
                for (int i = 0; i < respond.GetLength(0); i++)
                    for (int j = 0; j < respond.GetLength(1); j++)
                        respond[i, j] = (respond[i, j] / maxResp) * 255; //нормируем значение отклика от 0 до 255
            }
            for (int x = 0; x < imageArray.GetLength(0); x++)
                for (int y = 0; y < imageArray.GetLength(1); y++)
                    imageArray[x, y] = respond[x + 2, y + 2];
            return imageArray;
        }


        public static decimal[,] HaarWaveletHorisontal(decimal[,] imageArray, int filterLength)
        {
            int[] haarMatrix = new int[filterLength];
            for (int i = 0; i < filterLength / 2; i++)
                haarMatrix[i] = 1;
            for (int i = filterLength / 2; i < filterLength; i++)
                haarMatrix[i] = -1;

            decimal resX = 0;//будет сканировать значения в пределах столбца, т.е. реагировать на горизонтальные контуры

            respond = new decimal[imageArray.GetLength(0) + 4, imageArray.GetLength(1) + 4];
            atan = new decimal[imageArray.GetLength(0) + 4, imageArray.GetLength(1) + 4];

            decimal[,] bigPic = new decimal[respond.GetLength(0) + filterLength, respond.GetLength(1) + filterLength];
            int addToBmp = (bigPic.GetLength(1) - imageArray.GetLength(1)) / 2;
            bigPic = FormBigPic(imageArray, addToBmp);

            Bitmap bigPicBmp = new Bitmap(bigPic.GetLength(1), bigPic.GetLength(0));
            for (int i = 0; i < bigPic.GetLength(0); i++)
                for (int j = 0; j < bigPic.GetLength(1); j++)
                {
                    bigPicBmp.SetPixel(j, i, Color.FromArgb((int)bigPic[i, j], (int)bigPic[i, j], (int)bigPic[i, j]));
                }

            for (int x = 0; x < respond.GetLength(0); x++)
                for (int y = 0; y < respond.GetLength(1); y++)
                {
                    resX = 0;
                    for (int u = 0; u < filterLength; u++)
                    {
                        resX += haarMatrix[u] * bigPic[x + filterLength / 2, y + u]; 
                    }

                    atan[x, y] = 0;
                   
                    respond[x, y]= Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(resX * resX)));
                }
            decimal maxResp = Max(respond);
            for (int i = 0; i < respond.GetLength(0); i++)
                for (int j = 0; j < respond.GetLength(1); j++)
                    respond[i, j] = (respond[i, j] / maxResp) * 255; //нормируем значение отклика от 0 до 255
            Bitmap saveRespondBmp = new Bitmap(respond.GetLength(1), respond.GetLength(0));
            for (int i = 0; i < respond.GetLength(0); i++)
                for (int j = 0; j < respond.GetLength(1); j++)
                {
                    saveRespondBmp.SetPixel(j, i, Color.FromArgb((int)respond[i, j], (int)respond[i, j], (int)respond[i, j]));
                }

            for (int x = 0; x < imageArray.GetLength(0); x++)
                for (int y = 0; y < imageArray.GetLength(1); y++)
                    imageArray[x, y] = respond[x + 2, y + 2];


            return imageArray;
        }

        public static Bitmap FindEdgesInsideGivenEdge(Bitmap bmpWidthGivenEdge, decimal[,]origImageArray)
        {
            List<int> contourIndicesX = new List<int>();
            List<int> contourIndicesY = new List<int>();


            decimal[,] imageArray = new decimal[bmpWidthGivenEdge.Height, bmpWidthGivenEdge.Width];
            for (int i = 0; i < bmpWidthGivenEdge.Width; i++)
                for (int j = 0; j < bmpWidthGivenEdge.Height; j++)
                    imageArray[j, i] = bmpWidthGivenEdge.GetPixel(i, j).R;

            bool followedTheEdge = false;
            for (int i = 0; i < imageArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageArray.GetLength(1); j++)
                {
                    if (imageArray[i, j] == 255)
                    {
                        contourIndicesX.Add(i);
                        int rememberXIndexIfNeedTiRemove = contourIndicesX.Count;
                        contourIndicesY.Add(j);
                        int rememberYIndexIfNeedTiRemove = contourIndicesY.Count;
                        recursiveFindNeighborContourIndices(imageArray, contourIndicesX, contourIndicesY, i, j);
                        followedTheEdge = true;
                        break;
                    }
                }
                if (followedTheEdge)
                    break;
            }
            //удаляем по последнему индексу, т.к. они совпадают с началом прослеженного контура, который уже занесен в список
            contourIndicesX.RemoveAt(contourIndicesX.Count - 1);
            contourIndicesY.RemoveAt(contourIndicesY.Count - 1);

            //определяем часть изображения, попавшую внутрь прослеженного контура и где надо, увеличиваем изображение, чтоб в итоге у него была постоянная ширина и пост.высота
            int minFromcontourIndicesX = contourIndicesX[0];
            int maxFromcontourIndicesX = contourIndicesX[0];
            int minFromcontourIndicesY = contourIndicesY[0];
            int maxFromcontourIndicesY = contourIndicesY[0];


            for (int i = 0; i < contourIndicesX.Count; i++)
            {
                if (contourIndicesX[i] < minFromcontourIndicesX)
                    minFromcontourIndicesX = contourIndicesX[i];
                if (contourIndicesY[i] < minFromcontourIndicesY)
                    minFromcontourIndicesY = contourIndicesY[i];
                if (contourIndicesX[i] > maxFromcontourIndicesX)
                    maxFromcontourIndicesX = contourIndicesX[i];
                if (contourIndicesY[i] > maxFromcontourIndicesY)
                    maxFromcontourIndicesY = contourIndicesY[i];
            }

            int minFromcontourIndicesYTemp = minFromcontourIndicesY + 7;
            int maxFromcontourIndicesYTemp = maxFromcontourIndicesY - 7;
            int minFromcontourIndicesXTemp = minFromcontourIndicesX + 35;
            int maxFromcontourIndicesXTemp = maxFromcontourIndicesX - 15;

            // создаем и заполняем обрезанный массив изображения
            decimal[,] newImageArrayTemp = new decimal[maxFromcontourIndicesXTemp - minFromcontourIndicesXTemp + 1, maxFromcontourIndicesYTemp - minFromcontourIndicesYTemp + 1];
            if (imageArray != null)
            {
                int iNew = 0;
                int jNew = 0;
                for (int i = minFromcontourIndicesXTemp; i <= maxFromcontourIndicesXTemp; i++)
                {


                    int minFromcontourIndicesXInCurColumn;
                    int maxFromcontourIndicesXInCurColumn;
                    int minFromcontourIndicesYInCurRow;
                    int maxFromcontourIndicesYInCurRow;
                    jNew = 0;
                    findMinAndMaxInRow(i, out minFromcontourIndicesYInCurRow, out maxFromcontourIndicesYInCurRow, contourIndicesX, contourIndicesY);

                    for (int j = minFromcontourIndicesYTemp; j <= maxFromcontourIndicesYTemp; j++)
                    {
                        findMinAndMaxInColumn(j, out minFromcontourIndicesXInCurColumn, out maxFromcontourIndicesXInCurColumn, contourIndicesX, contourIndicesY);
                        if (j <= minFromcontourIndicesYInCurRow + 1)
                            newImageArrayTemp[iNew, jNew] = origImageArray[i, minFromcontourIndicesYInCurRow + 8];
                        else if (i <= minFromcontourIndicesXInCurColumn + 1)
                            newImageArrayTemp[iNew, jNew] = origImageArray[minFromcontourIndicesXInCurColumn + 15, j];
                        else if (j >= maxFromcontourIndicesYInCurRow - 1)
                            newImageArrayTemp[iNew, jNew] = origImageArray[i, maxFromcontourIndicesYInCurRow - 8];
                        else if (i >= maxFromcontourIndicesXInCurColumn - 1)
                            newImageArrayTemp[iNew, jNew] = origImageArray[maxFromcontourIndicesXInCurColumn - 8, j];
                        else
                            newImageArrayTemp[iNew, jNew] = origImageArray[i, j];
                        jNew++;
                    }
                    iNew++;
                }
            }

            for (int i = 0; i < contourIndicesX.Count; i++)
            {
                if (contourIndicesX[i] < minFromcontourIndicesX)
                    minFromcontourIndicesX = contourIndicesX[i];
                if (contourIndicesY[i] < minFromcontourIndicesY)
                    minFromcontourIndicesY = contourIndicesY[i];
                if (contourIndicesX[i] > maxFromcontourIndicesX)
                    maxFromcontourIndicesX = contourIndicesX[i];
                if (contourIndicesY[i] > maxFromcontourIndicesY)
                    maxFromcontourIndicesY = contourIndicesY[i];
            }


            Bitmap bmpInsideTheContour = new Bitmap(newImageArrayTemp.GetLength(1), newImageArrayTemp.GetLength(0));

            for (int i = 0; i < newImageArrayTemp.GetLength(0); i++)
                for (int j = 0; j < newImageArrayTemp.GetLength(1); j++)
                {
                    bmpInsideTheContour.SetPixel(j, i, Color.FromArgb((int)newImageArrayTemp[i, j], (int)newImageArrayTemp[i, j], (int)newImageArrayTemp[i, j]));
                }

            return bmpInsideTheContour;

        }

        private static void recursiveFindNeighborContourIndices(decimal[,] imageArray, List<int> contourIndicesX, List<int> contourIndicesY,
            int startPositionX, int startPositionY)
        {
            int i = startPositionX;
            int j = startPositionY;

            FindNeighbor(imageArray, ref contourIndicesX, ref contourIndicesY);

            while (contourIndicesX[contourIndicesX.Count - 1] != contourIndicesX[0] ||
                contourIndicesY[contourIndicesY.Count - 1] != contourIndicesY[0]) //  если не вернулись начала
                recursiveFindNeighborContourIndices(imageArray, contourIndicesX, contourIndicesY, contourIndicesX[contourIndicesX.Count - 1],
                    contourIndicesY[contourIndicesY.Count - 1]);

        }
        private static bool CheckIfNotALoop(decimal[,] imageArray, int iCheck, int jCheck, List<int> listToCheckInX, List<int> listToCheckInY)
        {
            bool isNotALoop = true;
            for (int i = 0; i < listToCheckInX.Count; i++)
            {
                if (iCheck == listToCheckInX[i] &&
                                jCheck == listToCheckInY[i] && (iCheck != listToCheckInX[0] || jCheck != listToCheckInY[0]))
                {
                    isNotALoop = false;
                    break; 
                }
            }
            return isNotALoop;
        }

        private static void FindNeighbor(decimal[,] imageArray, ref List<int> contourIndicesX, ref List<int> contourIndicesY)
        {
            bool foundNeighbor = false;
            int i = contourIndicesX[contourIndicesX.Count - 1];
            int j = contourIndicesY[contourIndicesY.Count - 1];
            int radius = 1;
            while (!foundNeighbor)
            {

                if (imageArray[i - radius, j] == 255 &&
                     CheckIfNotALoop(imageArray, i - radius, j, contourIndicesX, contourIndicesY))
                {
                    i -= radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }

                else if (imageArray[i - radius, j + radius] == 255 &&
                     CheckIfNotALoop(imageArray, i - radius, j + radius, contourIndicesX, contourIndicesY))
                {
                    i -= radius;
                    j += radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }
                else if (imageArray[i, j + radius] == 255 &&
                            CheckIfNotALoop(imageArray, i, j + radius, contourIndicesX, contourIndicesY))
                {
                    j += radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }
                else if (imageArray[i + radius, j + radius] == 255 &&
                     CheckIfNotALoop(imageArray, i + radius, j + radius, contourIndicesX, contourIndicesY))
                {
                    i += radius;
                    j += radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }
                else if (imageArray[i + radius, j] == 255 &&
                     CheckIfNotALoop(imageArray, i + radius, j, contourIndicesX, contourIndicesY))
                {
                    i += radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }

                else if (imageArray[i + radius, j - radius] == 255 &&
                     CheckIfNotALoop(imageArray, i + radius, j - radius, contourIndicesX, contourIndicesY))
                {
                    i += radius;
                    j -= radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }
                else if (imageArray[i, j - radius] == 255 &&
                     CheckIfNotALoop(imageArray, i, j - radius, contourIndicesX, contourIndicesY))
                {
                    j -= radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }
                else if (imageArray[i - radius, j - radius] == 255 &&
                    CheckIfNotALoop(imageArray, i - radius, j - radius, contourIndicesX, contourIndicesY))
                {
                    i -= radius;
                    j -= radius;
                    contourIndicesX.Add(i);
                    contourIndicesY.Add(j);
                    foundNeighbor = true;
                }
                else
                    radius++;
            }

        }

        private static void findMinAndMaxInRow(int rowNumber, out int minFromcontourIndicesY, out int maxFromcontourIndicesY, List<int> contourIndicesX, List<int> contourIndicesY)
        {
            minFromcontourIndicesY = contourIndicesY[0];
            maxFromcontourIndicesY = contourIndicesY[0];

            List<int> indeces = new List<int>();
            for (int i = 0; i < contourIndicesX.Count; i++)
            {
                if (contourIndicesX[i] == rowNumber)
                    indeces.Add(i);
            }
            foreach (int index in indeces)
            {
                if (contourIndicesY[index] < minFromcontourIndicesY)
                    minFromcontourIndicesY = contourIndicesY[index];
                if (contourIndicesY[index] > maxFromcontourIndicesY)
                    maxFromcontourIndicesY = contourIndicesY[index];
            }
        }

        private static void findMinAndMaxInColumn(int colNumber, out int minFromcontourIndicesX, out int maxFromcontourIndicesX, List<int> contourIndicesX, List<int> contourIndicesY)
        {
            minFromcontourIndicesX = contourIndicesX[0];
            maxFromcontourIndicesX = contourIndicesX[0];

            List<int> indeces = new List<int>();
            for (int i = 0; i < contourIndicesY.Count; i++)
            {
                if (contourIndicesY[i] == colNumber)
                    indeces.Add(i);
            }
            foreach (int index in indeces)
            {
                if (contourIndicesX[index] < minFromcontourIndicesX)
                    minFromcontourIndicesX = contourIndicesX[index];
                if (contourIndicesX[index] > maxFromcontourIndicesX)
                    maxFromcontourIndicesX = contourIndicesX[index];
            }
        }

    }
}
