namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            onnx = new CheckBox();
            ncnn = new CheckBox();
            button2 = new Button();
            button3 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            numericUpDown2 = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(507, 220);
            button1.Name = "button1";
            button1.Size = new Size(89, 23);
            button1.TabIndex = 0;
            button1.Text = "选择图片";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(482, 482);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(510, 454);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 2;
            label1.Text = "推理耗时";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.UseMnemonic = false;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(507, 181);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(89, 23);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // onnx
            // 
            onnx.AutoSize = true;
            onnx.Checked = true;
            onnx.CheckState = CheckState.Checked;
            onnx.Location = new Point(510, 12);
            onnx.Name = "onnx";
            onnx.Size = new Size(89, 21);
            onnx.TabIndex = 5;
            onnx.Text = "ONNX引擎";
            onnx.UseVisualStyleBackColor = true;
            onnx.CheckedChanged += Onnx_CheckedChanged;
            // 
            // ncnn
            // 
            ncnn.AutoSize = true;
            ncnn.Location = new Point(510, 46);
            ncnn.Name = "ncnn";
            ncnn.Size = new Size(89, 21);
            ncnn.TabIndex = 6;
            ncnn.Text = "NCNN引擎";
            ncnn.UseVisualStyleBackColor = true;
            ncnn.CheckedChanged += Ncnn_CheckedChanged;
            // 
            // button2
            // 
            button2.Location = new Point(507, 378);
            button2.Name = "button2";
            button2.Size = new Size(89, 23);
            button2.TabIndex = 7;
            button2.Text = "暂停识别";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(507, 349);
            button3.Name = "button3";
            button3.Size = new Size(89, 23);
            button3.TabIndex = 8;
            button3.Text = "屏幕识别";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // timer1
            // 
            timer1.Tick += Timer1_Tick;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpDown2.Location = new Point(507, 320);
            numericUpDown2.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(89, 23);
            numericUpDown2.TabIndex = 9;
            numericUpDown2.Value = new decimal(new int[] { 300, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Green;
            label2.Location = new Point(510, 475);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 10;
            label2.Text = "CPU";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.UseMnemonic = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(506, 298);
            label3.Name = "label3";
            label3.Size = new Size(91, 17);
            label3.TabIndex = 11;
            label3.Text = "截图间隔(毫秒):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(510, 156);
            label4.Name = "label4";
            label4.Size = new Size(83, 17);
            label4.TabIndex = 12;
            label4.Text = "图片推理次数:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownWidth = 200;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(506, 74);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(89, 25);
            comboBox1.TabIndex = 13;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 506);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(numericUpDown2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(ncnn);
            Controls.Add(onnx);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "YoloV8-GPU";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox pictureBox1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private CheckBox onnx;
        private CheckBox ncnn;
        private Button button2;
        private Button button3;
        private System.Windows.Forms.Timer timer1;
        private NumericUpDown numericUpDown2;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox comboBox1;
    }
}