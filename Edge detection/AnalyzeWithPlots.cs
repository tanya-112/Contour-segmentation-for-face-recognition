using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Edge_detection
{
    public partial class AnalyzeWithPlots : Form
    {
        double cannyPrettCrit;
        double haarPrettCrit;
        double sigma;
        short k;
        double bottomThresholdCanny;
        double upperThresholdCanny;
        double bottomThresholdHaar;
        double upperThresholdHaar;
        int waveletLength;
        int from;
        int to;
        int widthOfBrightnessDiffer;
        double snr;

        public AnalyzeWithPlots(double sigma, short k, double bottomThresholdCanny, double upperThresholdCanny, 
            int waveletLength, double bottomThresholdHaar, double upperThresholdHaar, int from, int to)
        {
            InitializeComponent();
            this.sigma = sigma;
            this.k = k;
            this.bottomThresholdCanny = bottomThresholdCanny;
            this.upperThresholdCanny = upperThresholdCanny;
            this.waveletLength = waveletLength;
            this.bottomThresholdHaar = bottomThresholdHaar;
            this.upperThresholdHaar = upperThresholdHaar;
            this.from = from;
            this.to = to;
            //this.widthOfBrightnessDiffer = widthOfBrightnessDiffer;
            //this.snr = snr;
        }

        private void AnalysisWithPlots_Load(object sender, EventArgs e)
        {
            if (Form1.varyQ_radioButtonChecked == true)
            {
                double[,] R = new double[2, to - from + 1];// в 0й строке будут все значения крит.Прэтта для Канни, в 1й - для Хаара
                int qStep = 1;
                // Получим панель для рисования
                GraphPane pane = zedGraphControl1.GraphPane;

                // Создадим списки точек
                PointPairList listForCanny = new PointPairList();
                PointPairList listForHaar = new PointPairList();


                int i = 0;
                for (int q = from; q <= to; q+= qStep)
                {

                    double [] prettCritt = AnalysisOfMethods.DoTheAnalysis(sigma, k, bottomThresholdCanny, upperThresholdCanny,
                      waveletLength, bottomThresholdHaar, upperThresholdHaar, 1, q);
                    R[0, i] = prettCritt[0];// знач.критерия Прэтта для метода Канни при отношении сигнал/шум = q
                    R[1, i] = prettCritt[1];// знач.критерия Прэтта для метода Хаара при отношении сигнал/шум = q
                
                    // добавим в список полученные точки для дальнейшего отображения на графике
                    listForCanny.Add(q, R[0, i]);
                    listForHaar.Add(q, R[1, i]);
                    i++;
                }
                //Color color = Color.Black;
                // Создадим кривую с названием "F", 
                LineItem curveForCanny = pane.AddCurve("Canny method Prett Criteria", listForCanny, Color.BlueViolet, SymbolType.Circle);
                LineItem curveForHaar = pane.AddCurve("Haar method Prett Criteria", listForHaar, Color.Green, SymbolType.Circle);
                // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                // В противном случае на рисунке будет показана только часть графика, 
                // которая умещается в интервалы по осям, установленные по умолчанию
                zedGraphControl1.AxisChange();

                // Обновляем график
                zedGraphControl1.Invalidate();
            }
            if (Form1.varyWidthOfDiffer_radioButtonChecked == true)
            {
                double[,] R = new double[2, to - from + 1];// в 0й строке будут все значения крит.Прэтта для Канни, в 1й - для Хаара
                int widthStep = 1;
                // Получим панель для рисования
                GraphPane pane = zedGraphControl1.GraphPane;

                // Создадим списки точек
                PointPairList listForCanny = new PointPairList();
                PointPairList listForHaar = new PointPairList();

                int i = 0;
                for (int width = from; width <= to; width += widthStep)
                {

                    double[] prettCritt = AnalysisOfMethods.DoTheAnalysis(sigma, k, bottomThresholdCanny, upperThresholdCanny,
                      waveletLength, bottomThresholdHaar, upperThresholdHaar, width);
                    R[0, i] = prettCritt[0];// знач.критерия Прэтта для метода Канни при отношении сигнал/шум = q
                    R[1, i] = prettCritt[1];// знач.критерия Прэтта для метода Хаара при отношении сигнал/шум = q

                    // добавим в список полученные точки для дальнейшего отображения на графике
                    listForCanny.Add(width, R[0, i]);
                    listForHaar.Add(width, R[1, i]);
                    i++;
                }
                //Color color = Color.Black;
                // Создадим кривую с названием "F", 
                LineItem curveForCanny = pane.AddCurve("Canny method Prett Criteria", listForCanny, Color.BlueViolet, SymbolType.Circle);
                LineItem curveForHaar = pane.AddCurve("Haar method Prett Criteria", listForHaar, Color.Green, SymbolType.Circle);
                // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                // В противном случае на рисунке будет показана только часть графика, 
                // которая умещается в интервалы по осям, установленные по умолчанию
                zedGraphControl1.AxisChange();

                // Обновляем график
                zedGraphControl1.Invalidate();
            }
        }
    }
}
