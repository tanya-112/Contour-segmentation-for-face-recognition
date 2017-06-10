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
    public partial class AnalysisOfMethods : Form
    {
        double PrettCriteria;
        double correctness;
        double efficiency;
        public AnalysisOfMethods(double PrettCriteria, double correctness, double efficiency)
        {
            InitializeComponent();
            this.PrettCriteria = PrettCriteria;
            this.correctness = correctness;
            this.efficiency = efficiency;
        }

        private void AnalysisOfMethods_Load(object sender, EventArgs e)
        {
            textBox1.Text = PrettCriteria.ToString();
            textBox2.Text = correctness.ToString();
            textBox3.Text = efficiency.ToString();
        }
    }
}
