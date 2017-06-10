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
            this.useCannyMethod_button = new System.Windows.Forms.Button();
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
            this.label9 = new System.Windows.Forms.Label();
            this.upperThresholdHaar_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bottomThresholdHaar_textBox = new System.Windows.Forms.TextBox();
            this.waveletLength_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.useHaarInCannyMethod_button = new System.Windows.Forms.Button();
            this.chooseFile_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(12, 48);
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
            this.splitContainer1.Size = new System.Drawing.Size(663, 271);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.TabIndex = 1;
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
            // chooseFile_button
            // 
            this.chooseFile_button.Location = new System.Drawing.Point(273, 19);
            this.chooseFile_button.Name = "chooseFile_button";
            this.chooseFile_button.Size = new System.Drawing.Size(131, 23);
            this.chooseFile_button.TabIndex = 1;
            this.chooseFile_button.Text = "Выбрать изображение";
            this.chooseFile_button.UseVisualStyleBackColor = true;
            this.chooseFile_button.Click += new System.EventHandler(this.chooseFile_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 331);
            this.Controls.Add(this.chooseFile_button);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Edge detection";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button useCannyMethod_button;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button useHaarInCannyMethod_button;
        private System.Windows.Forms.TextBox sigma_textBox;
        private System.Windows.Forms.TextBox k_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bottomThresholdHaar_textBox;
        private System.Windows.Forms.TextBox waveletLength_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button chooseFile_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox upperThreshold_textBox;
        private System.Windows.Forms.TextBox bottomThreshold_textBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox upperThresholdHaar_textBox;
        private System.Windows.Forms.Label label7;
    }
}

