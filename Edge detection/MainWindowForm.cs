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
    public partial class MainWindowForm : Form
    {
        public string filePath;
        public static bool varyQ_radioButtonChecked;
        public static bool varyWidthOfDiffer_radioButtonChecked;
        public static bool enableNoisingImage_checkBoxChecked;
        internal AnalyzeWithPlots analyzeWithPlotsForm;
        internal static bool mustMakePlotInSameWindow;

        public MainWindowForm()
        {
            InitializeComponent();
            varyQ_radioButtonChecked = true;
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
            CannyForm CannyForm = new CannyForm(filePath, Convert.ToDouble(sigma_textBox.Text), Convert.ToDouble(bottomThreshold_textBox.Text), Convert.ToDouble(upperThreshold_textBox.Text)); //создаем окно для вывода результатов метода Канни, передавая путь к выбранному файлу
            CannyForm.Show();
        }


        private void useHaarInCannyMethod_button_Click(object sender, EventArgs e)
        {
            HaarForm HaarForm = new HaarForm(filePath, Int32.Parse(waveletLength_textBox.Text), Convert.ToDouble(bottomThresholdHaar_textBox.Text), Convert.ToDouble(upperThresholdHaar_textBox.Text)); //создаем окно для вывода результатов метода Канни, передавая путь к выбранному файлу
            HaarForm.Show();
        }
  

        private void analyze_button_Click(object sender, EventArgs e)
        {
            AnalysisOfMethods analyzeForm = new AnalysisOfMethods();
            if (enableNoisingImage_checkBox.Checked)
            analyzeForm = new AnalysisOfMethods(Convert.ToDouble(analyze_sigma_textBox.Text), 
            Convert.ToDouble(bottomThresholdCanny_analyze_textBox.Text), Convert.ToDouble(upperThresholdCanny_analyze_textBox.Text), 
            Int32.Parse(waveletLength_analyze_textBox.Text), Convert.ToDouble(bottomThresholdHaar_analyze_textBox.Text), 
            Convert.ToDouble(upperThresholdHaar_analyze_textBox.Text), Int32.Parse(widthOfBrigtnessDiffer_textBox.Text), Convert.ToDouble(SNR_textBox.Text));

            else
                if (enableNoisingImage_checkBox.Checked == false)
                analyzeForm = new AnalysisOfMethods(Convert.ToDouble(analyze_sigma_textBox.Text),
                Convert.ToDouble(bottomThresholdCanny_analyze_textBox.Text), Convert.ToDouble(upperThresholdCanny_analyze_textBox.Text),
                Int32.Parse(waveletLength_analyze_textBox.Text), Convert.ToDouble(bottomThresholdHaar_analyze_textBox.Text),
                Convert.ToDouble(upperThresholdHaar_analyze_textBox.Text), Int32.Parse(widthOfBrigtnessDiffer_textBox.Text));

                analyzeForm.Show();
        }

      
        private void varyQ_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (varyQ_radioButton.Checked)
            {
                varyParameter_label.Text = "Варьировать q";
                varyWidthOfDiffer_radioButtonChecked = false;
                varyQ_radioButtonChecked = true;
            }
        }

        private void varyWidthOfDiffer_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (varyWidthOfDiffer_radioButton.Checked)
            {
                varyParameter_label.Text = "Варьировать протяженность перепада";
                varyQ_radioButtonChecked = false;
                varyWidthOfDiffer_radioButtonChecked = true;
            }
        }

        private void analyzeWithPlots_button_Click(object sender, EventArgs e)
        {
            if (analyzeWithPlotsForm != null)
            {
                mustMakePlotInSameWindow = true;
                analyzeWithPlotsForm.MakePlotInSameWindow(Convert.ToDouble(analyzeWithPlots_sigma_textBox.Text), 
                Convert.ToDouble(bottomThresholdCanny_analyzeWithPlots_textBox.Text), Convert.ToDouble(upperThresholdCanny_analyzeWithPlots_textBox.Text),
                Int32.Parse(waveletLength_analyzeWithPlots_textBox.Text), Convert.ToDouble(bottomThresholdHaar_analyzeWithPlots_textBox.Text),
                Convert.ToDouble(upperThresholdHaar_analyzeWithPlots_textBox.Text), Int32.Parse(from_textBox.Text), Int32.Parse(to_textBox.Text));
            }
            if (analyzeWithPlotsForm == null)
            {
                analyzeWithPlotsForm = new AnalyzeWithPlots(Convert.ToDouble(analyzeWithPlots_sigma_textBox.Text),
                Convert.ToDouble(bottomThresholdCanny_analyzeWithPlots_textBox.Text), Convert.ToDouble(upperThresholdCanny_analyzeWithPlots_textBox.Text),
                Int32.Parse(waveletLength_analyzeWithPlots_textBox.Text), Convert.ToDouble(bottomThresholdHaar_analyzeWithPlots_textBox.Text),
                Convert.ToDouble(upperThresholdHaar_analyzeWithPlots_textBox.Text), Int32.Parse(from_textBox.Text), Int32.Parse(to_textBox.Text));
                mustMakePlotInSameWindow = false;
            }
               
            if (mustMakePlotInSameWindow)
                analyzeWithPlotsForm.Refresh();
            else analyzeWithPlotsForm.Show();
        }

        private void enableNoisingImage_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (enableNoisingImage_checkBox.Checked == false)
            {
                SNR_textBox.Text = "";
                SNR_textBox.Enabled = false;
                enableNoisingImage_checkBoxChecked = false;
            }
            else
            {
                SNR_textBox.Text = "100";
                SNR_textBox.Enabled = true;
                enableNoisingImage_checkBoxChecked = true;
            }
        }


    }
}
