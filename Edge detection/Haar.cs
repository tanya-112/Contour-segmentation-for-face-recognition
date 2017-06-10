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
    public partial class Haar : Form
    {
        string filePath;
        double bottomThreshold;
        double upperThreshold;
        int waveletLength;
        public Haar(string filePath, int waveletLength, double bottomThreshold, double upperThreshold)
        {
            InitializeComponent();
            this.filePath = filePath;
            this.waveletLength = waveletLength;
            this.bottomThreshold = bottomThreshold;
            this.upperThreshold = upperThreshold;
        }

        private void Haar_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(filePath);
            Bitmap a = new Bitmap(pictureBox1.Image);

            Form1.GrayScale(a);
            Form1.HaarWavelet(a, waveletLength);
            pictureBox2.Image = a;
            Bitmap b = new Bitmap(a);
            Form1.Non_Maximum_Suppression(b);
            pictureBox3.Image = Form1.Double_Threshold(b, bottomThreshold, upperThreshold);
            Bitmap c = new Bitmap(b);
            pictureBox4.Image = Form1.Hysteresis_Thresholding(c, "Haar");
        }
    }
}
