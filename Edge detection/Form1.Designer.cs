namespace Edge_detection
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chooseFile_button = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label8 = new System.Windows.Forms.Label();
            this.upperThreshold_textBox = new System.Windows.Forms.TextBox();
            this.bottomThreshold_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sigma_textBox = new System.Windows.Forms.TextBox();
            this.k_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.useCannyMethod_button = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.upperThresholdHaar_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bottomThresholdHaar_textBox = new System.Windows.Forms.TextBox();
            this.waveletLength_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.useHaarInCannyMethod_button = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SNR_textBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.widthOfBrigtnessDiffer_textBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.analyze_button = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.upperThresholdCanny_analyze_textBox = new System.Windows.Forms.TextBox();
            this.bottomThresholdCanny_analyze_textBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.analyze_sigma_textBox = new System.Windows.Forms.TextBox();
            this.k_analyze_textBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.upperThresholdHaar_analyze_textBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.bottomThresholdHaar_analyze_textBox = new System.Windows.Forms.TextBox();
            this.waveletLength_analyze_textBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(806, 331);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.chooseFile_button);
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(798, 305);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основной режим";
            // 
            // chooseFile_button
            // 
            this.chooseFile_button.Location = new System.Drawing.Point(329, 3);
            this.chooseFile_button.Name = "chooseFile_button";
            this.chooseFile_button.Size = new System.Drawing.Size(131, 23);
            this.chooseFile_button.TabIndex = 3;
            this.chooseFile_button.Text = "Выбрать изображение";
            this.chooseFile_button.UseVisualStyleBackColor = true;
            this.chooseFile_button.Click += new System.EventHandler(this.chooseFile_button_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(7, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.upperThreshold_textBox);
            this.splitContainer1.Panel1.Controls.Add(this.bottomThreshold_textBox);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.sigma_textBox);
            this.splitContainer1.Panel1.Controls.Add(this.k_textBox);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.useCannyMethod_button);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.upperThresholdHaar_textBox);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.bottomThresholdHaar_textBox);
            this.splitContainer1.Panel2.Controls.Add(this.waveletLength_textBox);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.useHaarInCannyMethod_button);
            this.splitContainer1.Size = new System.Drawing.Size(785, 271);
            this.splitContainer1.SplitterDistance = 385;
            this.splitContainer1.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(329, 40);
            this.label8.TabIndex = 9;
            this.label8.Text = "Стандартный метод Канни (используется маска Собеля)";
            // 
            // upperThreshold_textBox
            // 
            this.upperThreshold_textBox.Location = new System.Drawing.Point(139, 175);
            this.upperThreshold_textBox.Name = "upperThreshold_textBox";
            this.upperThreshold_textBox.Size = new System.Drawing.Size(56, 20);
            this.upperThreshold_textBox.TabIndex = 8;
            this.upperThreshold_textBox.Text = "40";
            // 
            // bottomThreshold_textBox
            // 
            this.bottomThreshold_textBox.Location = new System.Drawing.Point(139, 143);
            this.bottomThreshold_textBox.Name = "bottomThreshold_textBox";
            this.bottomThreshold_textBox.Size = new System.Drawing.Size(56, 20);
            this.bottomThreshold_textBox.TabIndex = 7;
            this.bottomThreshold_textBox.Text = "30";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Верхний порог";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Нижний порог";
            // 
            // sigma_textBox
            // 
            this.sigma_textBox.Location = new System.Drawing.Point(139, 62);
            this.sigma_textBox.Name = "sigma_textBox";
            this.sigma_textBox.Size = new System.Drawing.Size(56, 20);
            this.sigma_textBox.TabIndex = 4;
            this.sigma_textBox.Text = "1";
            // 
            // k_textBox
            // 
            this.k_textBox.Location = new System.Drawing.Point(139, 98);
            this.k_textBox.Name = "k_textBox";
            this.k_textBox.Size = new System.Drawing.Size(56, 20);
            this.k_textBox.TabIndex = 3;
            this.k_textBox.Text = "9";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sigma";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(43, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "k для размытия Гауссовским фильтром ";
            // 
            // useCannyMethod_button
            // 
            this.useCannyMethod_button.Enabled = false;
            this.useCannyMethod_button.Location = new System.Drawing.Point(46, 233);
            this.useCannyMethod_button.Name = "useCannyMethod_button";
            this.useCannyMethod_button.Size = new System.Drawing.Size(114, 23);
            this.useCannyMethod_button.TabIndex = 0;
            this.useCannyMethod_button.Text = "Выделить контуры";
            this.useCannyMethod_button.UseVisualStyleBackColor = true;
            this.useCannyMethod_button.Click += new System.EventHandler(this.useCannyMethod_button_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(40, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(255, 40);
            this.label9.TabIndex = 7;
            this.label9.Text = "Модифицированный метод Канни (используется вейвлет-преобразование Хаара)";
            // 
            // upperThresholdHaar_textBox
            // 
            this.upperThresholdHaar_textBox.Location = new System.Drawing.Point(192, 179);
            this.upperThresholdHaar_textBox.Name = "upperThresholdHaar_textBox";
            this.upperThresholdHaar_textBox.Size = new System.Drawing.Size(52, 20);
            this.upperThresholdHaar_textBox.TabIndex = 6;
            this.upperThresholdHaar_textBox.Text = "50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Верхний порог";
            // 
            // bottomThresholdHaar_textBox
            // 
            this.bottomThresholdHaar_textBox.Location = new System.Drawing.Point(192, 137);
            this.bottomThresholdHaar_textBox.Name = "bottomThresholdHaar_textBox";
            this.bottomThresholdHaar_textBox.Size = new System.Drawing.Size(52, 20);
            this.bottomThresholdHaar_textBox.TabIndex = 4;
            this.bottomThresholdHaar_textBox.Text = "20";
            // 
            // waveletLength_textBox
            // 
            this.waveletLength_textBox.Location = new System.Drawing.Point(192, 95);
            this.waveletLength_textBox.Name = "waveletLength_textBox";
            this.waveletLength_textBox.Size = new System.Drawing.Size(52, 20);
            this.waveletLength_textBox.TabIndex = 3;
            this.waveletLength_textBox.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Нижний порог";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(40, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 45);
            this.label3.TabIndex = 1;
            this.label3.Text = "Длина носителя для вейвлет-преобразования";
            // 
            // useHaarInCannyMethod_button
            // 
            this.useHaarInCannyMethod_button.Enabled = false;
            this.useHaarInCannyMethod_button.Location = new System.Drawing.Point(96, 233);
            this.useHaarInCannyMethod_button.Name = "useHaarInCannyMethod_button";
            this.useHaarInCannyMethod_button.Size = new System.Drawing.Size(110, 23);
            this.useHaarInCannyMethod_button.TabIndex = 0;
            this.useHaarInCannyMethod_button.Text = "Выделить контуры";
            this.useHaarInCannyMethod_button.UseVisualStyleBackColor = true;
            this.useHaarInCannyMethod_button.Click += new System.EventHandler(this.useHaarInCannyMethod_button_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.analyze_button);
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(798, 305);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Анализ методов";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SNR_textBox);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.widthOfBrigtnessDiffer_textBox);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Location = new System.Drawing.Point(624, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 271);
            this.panel1.TabIndex = 8;
            // 
            // SNR_textBox
            // 
            this.SNR_textBox.Location = new System.Drawing.Point(107, 143);
            this.SNR_textBox.Name = "SNR_textBox";
            this.SNR_textBox.Size = new System.Drawing.Size(40, 20);
            this.SNR_textBox.TabIndex = 4;
            this.SNR_textBox.Text = "100";
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(6, 139);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 34);
            this.label21.TabIndex = 3;
            this.label21.Text = "Отношение сигнал/шум";
            // 
            // widthOfBrigtnessDiffer_textBox
            // 
            this.widthOfBrigtnessDiffer_textBox.Location = new System.Drawing.Point(107, 92);
            this.widthOfBrigtnessDiffer_textBox.Name = "widthOfBrigtnessDiffer_textBox";
            this.widthOfBrigtnessDiffer_textBox.Size = new System.Drawing.Size(40, 20);
            this.widthOfBrigtnessDiffer_textBox.TabIndex = 2;
            this.widthOfBrigtnessDiffer_textBox.Text = "1";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 95);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Ширина перепада";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(14, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(148, 40);
            this.label19.TabIndex = 0;
            this.label19.Text = "Дополнительные параметры для анализа";
            // 
            // analyze_button
            // 
            this.analyze_button.BackColor = System.Drawing.Color.Transparent;
            this.analyze_button.Location = new System.Drawing.Point(355, 3);
            this.analyze_button.Name = "analyze_button";
            this.analyze_button.Size = new System.Drawing.Size(75, 23);
            this.analyze_button.TabIndex = 5;
            this.analyze_button.Text = "Анализ";
            this.analyze_button.UseVisualStyleBackColor = false;
            this.analyze_button.Click += new System.EventHandler(this.analyze_button_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(7, 31);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label10);
            this.splitContainer2.Panel1.Controls.Add(this.upperThresholdCanny_analyze_textBox);
            this.splitContainer2.Panel1.Controls.Add(this.bottomThresholdCanny_analyze_textBox);
            this.splitContainer2.Panel1.Controls.Add(this.label11);
            this.splitContainer2.Panel1.Controls.Add(this.label12);
            this.splitContainer2.Panel1.Controls.Add(this.analyze_sigma_textBox);
            this.splitContainer2.Panel1.Controls.Add(this.k_analyze_textBox);
            this.splitContainer2.Panel1.Controls.Add(this.label13);
            this.splitContainer2.Panel1.Controls.Add(this.label14);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label15);
            this.splitContainer2.Panel2.Controls.Add(this.upperThresholdHaar_analyze_textBox);
            this.splitContainer2.Panel2.Controls.Add(this.label16);
            this.splitContainer2.Panel2.Controls.Add(this.bottomThresholdHaar_analyze_textBox);
            this.splitContainer2.Panel2.Controls.Add(this.waveletLength_analyze_textBox);
            this.splitContainer2.Panel2.Controls.Add(this.label17);
            this.splitContainer2.Panel2.Controls.Add(this.label18);
            this.splitContainer2.Size = new System.Drawing.Size(611, 271);
            this.splitContainer2.SplitterDistance = 300;
            this.splitContainer2.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(329, 40);
            this.label10.TabIndex = 9;
            this.label10.Text = "Стандартный метод Канни (используется маска Собеля)";
            // 
            // upperThresholdCanny_analyze_textBox
            // 
            this.upperThresholdCanny_analyze_textBox.Location = new System.Drawing.Point(139, 175);
            this.upperThresholdCanny_analyze_textBox.Name = "upperThresholdCanny_analyze_textBox";
            this.upperThresholdCanny_analyze_textBox.Size = new System.Drawing.Size(56, 20);
            this.upperThresholdCanny_analyze_textBox.TabIndex = 8;
            this.upperThresholdCanny_analyze_textBox.Text = "160";
            // 
            // bottomThresholdCanny_analyze_textBox
            // 
            this.bottomThresholdCanny_analyze_textBox.Location = new System.Drawing.Point(139, 143);
            this.bottomThresholdCanny_analyze_textBox.Name = "bottomThresholdCanny_analyze_textBox";
            this.bottomThresholdCanny_analyze_textBox.Size = new System.Drawing.Size(56, 20);
            this.bottomThresholdCanny_analyze_textBox.TabIndex = 7;
            this.bottomThresholdCanny_analyze_textBox.Text = "140";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(43, 183);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Верхний порог";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(43, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Нижний порог";
            // 
            // analyze_sigma_textBox
            // 
            this.analyze_sigma_textBox.Location = new System.Drawing.Point(139, 62);
            this.analyze_sigma_textBox.Name = "analyze_sigma_textBox";
            this.analyze_sigma_textBox.Size = new System.Drawing.Size(56, 20);
            this.analyze_sigma_textBox.TabIndex = 4;
            this.analyze_sigma_textBox.Text = "1";
            // 
            // k_analyze_textBox
            // 
            this.k_analyze_textBox.Location = new System.Drawing.Point(139, 98);
            this.k_analyze_textBox.Name = "k_analyze_textBox";
            this.k_analyze_textBox.Size = new System.Drawing.Size(56, 20);
            this.k_analyze_textBox.TabIndex = 3;
            this.k_analyze_textBox.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(43, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Sigma";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(43, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 42);
            this.label14.TabIndex = 1;
            this.label14.Text = "k для размытия Гауссовским фильтром ";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(40, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(255, 40);
            this.label15.TabIndex = 7;
            this.label15.Text = "Модифицированный метод Канни (используется вейвлет-преобразование Хаара)";
            // 
            // upperThresholdHaar_analyze_textBox
            // 
            this.upperThresholdHaar_analyze_textBox.Location = new System.Drawing.Point(192, 179);
            this.upperThresholdHaar_analyze_textBox.Name = "upperThresholdHaar_analyze_textBox";
            this.upperThresholdHaar_analyze_textBox.Size = new System.Drawing.Size(52, 20);
            this.upperThresholdHaar_analyze_textBox.TabIndex = 6;
            this.upperThresholdHaar_analyze_textBox.Text = "150";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(43, 179);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Верхний порог";
            // 
            // bottomThresholdHaar_analyze_textBox
            // 
            this.bottomThresholdHaar_analyze_textBox.Location = new System.Drawing.Point(192, 137);
            this.bottomThresholdHaar_analyze_textBox.Name = "bottomThresholdHaar_analyze_textBox";
            this.bottomThresholdHaar_analyze_textBox.Size = new System.Drawing.Size(52, 20);
            this.bottomThresholdHaar_analyze_textBox.TabIndex = 4;
            this.bottomThresholdHaar_analyze_textBox.Text = "140";
            // 
            // waveletLength_analyze_textBox
            // 
            this.waveletLength_analyze_textBox.Location = new System.Drawing.Point(192, 95);
            this.waveletLength_analyze_textBox.Name = "waveletLength_analyze_textBox";
            this.waveletLength_analyze_textBox.Size = new System.Drawing.Size(52, 20);
            this.waveletLength_analyze_textBox.TabIndex = 3;
            this.waveletLength_analyze_textBox.Text = "8";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(40, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Нижний порог";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(40, 95);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(146, 45);
            this.label18.TabIndex = 1;
            this.label18.Text = "Длина носителя для вейвлет-преобразования";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 355);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Edge detection";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button chooseFile_button;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox upperThreshold_textBox;
        private System.Windows.Forms.TextBox bottomThreshold_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sigma_textBox;
        private System.Windows.Forms.TextBox k_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button useCannyMethod_button;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox upperThresholdHaar_textBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bottomThresholdHaar_textBox;
        private System.Windows.Forms.TextBox waveletLength_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button useHaarInCannyMethod_button;
        private System.Windows.Forms.Button analyze_button;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox upperThresholdCanny_analyze_textBox;
        private System.Windows.Forms.TextBox bottomThresholdCanny_analyze_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox analyze_sigma_textBox;
        private System.Windows.Forms.TextBox k_analyze_textBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox upperThresholdHaar_analyze_textBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox bottomThresholdHaar_analyze_textBox;
        private System.Windows.Forms.TextBox waveletLength_analyze_textBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox widthOfBrigtnessDiffer_textBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox SNR_textBox;
    }
}

