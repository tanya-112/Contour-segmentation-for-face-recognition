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
        double sigma;
        //short k;
        double bottomThresholdCanny;
        double upperThresholdCanny;
        double bottomThresholdHaar;
        double upperThresholdHaar;
        int waveletLength;
        int from;
        int to;
        static GraphPane pane1;


        public AnalyzeWithPlots(double sigma, double bottomThresholdCanny, double upperThresholdCanny, 
            int waveletLength, double bottomThresholdHaar, double upperThresholdHaar, int from, int to)
        {
            InitializeComponent();
            this.sigma = sigma;
            this.bottomThresholdCanny = bottomThresholdCanny;
            this.upperThresholdCanny = upperThresholdCanny;
            this.waveletLength = waveletLength;
            this.bottomThresholdHaar = bottomThresholdHaar;
            this.upperThresholdHaar = upperThresholdHaar;
            this.from = from;
            this.to = to;
        }
        public void MakePlotInSameWindow(double sigma, double bottomThresholdCanny, double upperThresholdCanny,
            int waveletLength, double bottomThresholdHaar, double upperThresholdHaar, int from, int to)
            {
            if (MainWindowForm.varyQ_radioButtonChecked == true)
            {
                int quantityOfExperimentsWithOneQ = 15; // столько раз будем проводить один и тот же эксперимент, а затем усредним рез-т, просуммировав и разделив на это число
                double[][,] R = new double[quantityOfExperimentsWithOneQ][,];// в 0й строке будут все значения крит.Прэтта для Канни, в 1й - для Хаара

                for (int z = 0; z < quantityOfExperimentsWithOneQ; z++)
                    R[z] = new double[2, (to - from + 1)];

                int qStep = 1;
                // Получим панель для рисования
                if (MainWindowForm.mustMakePlotInSameWindow == false)
                    pane1 = zedGraphControl1.GraphPane;

                pane1.XAxis.Title.Text = "q";
                pane1.YAxis.Title.Text = "R";

                // Создадим списки точек

                PointPairList listForCanny = new PointPairList();
                PointPairList listForHaar = new PointPairList();
                
                int i = 0;
                for (int q = from; q <= to; q += qStep)
                {
                    double[] prettCritt;
                    for (int numberOfExperimentToAverage = 0; numberOfExperimentToAverage < quantityOfExperimentsWithOneQ; numberOfExperimentToAverage++)
                    {
                        prettCritt = AnalysisOfMethods.DoTheAnalysis(sigma, bottomThresholdCanny, upperThresholdCanny,
                      waveletLength, bottomThresholdHaar, upperThresholdHaar, 1, q);
                        R[numberOfExperimentToAverage][0, i] = prettCritt[0];// знач.критерия Прэтта для метода Канни при отношении сигнал/шум = q
                        R[numberOfExperimentToAverage][1, i] = prettCritt[1];// знач.критерия Прэтта для метода Хаара при отношении сигнал/шум = q
                    }
                    double[,] averageOfExperimentsForOneQ = new double[2, 1];
                    averageOfExperimentsForOneQ[0, 0] = 0;
                    averageOfExperimentsForOneQ[1, 0] = 0;

                    for (int z = 0; z < quantityOfExperimentsWithOneQ; z++)
                    {
                        averageOfExperimentsForOneQ[0, 0] += R[z][0, i];
                        averageOfExperimentsForOneQ[1, 0] += R[z][1, i];
                    }

                    averageOfExperimentsForOneQ[0, 0] /= quantityOfExperimentsWithOneQ;//получили усредненный результат с экспериментом для опр-го q для м.Канни
                    averageOfExperimentsForOneQ[1, 0] /= quantityOfExperimentsWithOneQ;//получили усредненный результат с экспериментом для опр-го q для м.Хаара
                                                                                       // добавим в список полученные точки для дальнейшего отображения на графике
                    if (MainWindowForm.mustMakePlotInSameWindow == false)
                        listForCanny.Add(q, averageOfExperimentsForOneQ[0, 0]);
                    listForHaar.Add(q, averageOfExperimentsForOneQ[1, 0]);
                    i++;
                }
                LineItem curveForCanny;
                // Создадим кривую
                if (MainWindowForm.mustMakePlotInSameWindow == false)
                     curveForCanny = pane1.AddCurve("Canny method Prett Criteria", listForCanny, Color.BlueViolet, SymbolType.None);
                LineItem curveForHaar = pane1.AddCurve("Haar method Prett Criteria", listForHaar, Color.Green, SymbolType.None);
                // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                // В противном случае на рисунке будет показана только часть графика, 
                // которая умещается в интервалы по осям, установленные по умолчанию
                zedGraphControl1.AxisChange();

                // Обновляем график
                zedGraphControl1.Invalidate();

            }
            if (MainWindowForm.varyWidthOfDiffer_radioButtonChecked == true)
            {
                double[,] R = new double[2, to - from + 1];// в 0й строке будут все значения крит.Прэтта для Канни, в 1й - для Хаара
                int widthStep = 1;
                // Получим панель для рисования
                if(MainWindowForm.mustMakePlotInSameWindow==false)
                pane1 = zedGraphControl1.GraphPane;

                pane1.XAxis.Title.Text = "Ширина перепаду";
                pane1.YAxis.Title.Text = "R";

                // Создадим списки точек
                PointPairList listForCanny = new PointPairList();
                PointPairList listForHaar = new PointPairList();

                int i = 0;
                for (int width = from; width <= to; width += widthStep)
                {

                    double[] prettCritt = AnalysisOfMethods.DoTheAnalysis(sigma, bottomThresholdCanny, upperThresholdCanny,
                      waveletLength, bottomThresholdHaar, upperThresholdHaar, width);
                    R[0, i] = prettCritt[0];// знач.критерия Прэтта для метода Канни при отношении сигнал/шум = q
                    R[1, i] = prettCritt[1];// знач.критерия Прэтта для метода Хаара при отношении сигнал/шум = q

                    // добавим в список полученные точки для дальнейшего отображения на графике
                    if (MainWindowForm.mustMakePlotInSameWindow == false)
                        listForCanny.Add(width, R[0, i]);
                    listForHaar.Add(width, R[1, i]);
                    i++;
                }
                LineItem curveForCanny;
                // Создадим кривую 
                if (MainWindowForm.mustMakePlotInSameWindow == false)
                    curveForCanny = pane1.AddCurve("Canny method Prett Criteria", listForCanny, Color.BlueViolet, SymbolType.None);
                LineItem curveForHaar = pane1.AddCurve("Haar method Prett Criteria", listForHaar, Color.Green, SymbolType.None);
                // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                // В противном случае на рисунке будет показана только часть графика, 
                // которая умещается в интервалы по осям, установленные по умолчанию
                zedGraphControl1.AxisChange();

                // Обновляем график
                zedGraphControl1.Invalidate();
            }        
    }
        private void AnalysisWithPlots_Load(object sender, EventArgs e)
        {
            MakePlotInSameWindow( sigma, bottomThresholdCanny,  upperThresholdCanny,
             waveletLength,  bottomThresholdHaar,  upperThresholdHaar,  from, to);
        }
    }
}
