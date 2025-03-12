namespace CG.View.Forms.Lab2
{
    partial class Lab2Form
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
            splitContainer1 = new SplitContainer();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            RadiusTextBox = new TextBox();
            AlgListBox = new CheckedListBox();
            BresAlgButton = new RadioButton();
            textBox1 = new TextBox();
            FillColorButton = new Button();
            BoldlineDrawButton = new CheckBox();
            ExecuteButton = new Button();
            LineColorButton = new Button();
            FillButton = new RadioButton();
            ClearButton = new Button();
            DDAAlgButton = new RadioButton();
            label1 = new Label();
            colorDialog1 = new ColorDialog();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(RadiusTextBox);
            splitContainer1.Panel2.Controls.Add(AlgListBox);
            splitContainer1.Panel2.Controls.Add(BresAlgButton);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(FillColorButton);
            splitContainer1.Panel2.Controls.Add(BoldlineDrawButton);
            splitContainer1.Panel2.Controls.Add(ExecuteButton);
            splitContainer1.Panel2.Controls.Add(LineColorButton);
            splitContainer1.Panel2.Controls.Add(FillButton);
            splitContainer1.Panel2.Controls.Add(ClearButton);
            splitContainer1.Panel2.Controls.Add(DDAAlgButton);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Size = new Size(954, 542);
            splitContainer1.SplitterDistance = 668;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(668, 542);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 191);
            label2.Name = "label2";
            label2.Size = new Size(143, 20);
            label2.TabIndex = 14;
            label2.Text = "Радиус окружности";
            // 
            // RadiusTextBox
            // 
            RadiusTextBox.Location = new Point(14, 188);
            RadiusTextBox.Name = "RadiusTextBox";
            RadiusTextBox.Size = new Size(39, 27);
            RadiusTextBox.TabIndex = 13;
            RadiusTextBox.Text = "80";
            // 
            // AlgListBox
            // 
            AlgListBox.CheckOnClick = true;
            AlgListBox.FormattingEnabled = true;
            AlgListBox.Location = new Point(14, 47);
            AlgListBox.Name = "AlgListBox";
            AlgListBox.Size = new Size(244, 114);
            AlgListBox.TabIndex = 12;
            AlgListBox.SelectedIndexChanged += AlgListBox_SelectedIndexChanged;
            // 
            // BresAlgButton
            // 
            BresAlgButton.AutoSize = true;
            BresAlgButton.Location = new Point(16, 419);
            BresAlgButton.Name = "BresAlgButton";
            BresAlgButton.Size = new Size(186, 24);
            BresAlgButton.TabIndex = 11;
            BresAlgButton.TabStop = true;
            BresAlgButton.Text = "Алгоритм Брезенхема";
            BresAlgButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 221);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(166, 73);
            textBox1.TabIndex = 10;
            // 
            // FillColorButton
            // 
            FillColorButton.Location = new Point(124, 335);
            FillColorButton.Name = "FillColorButton";
            FillColorButton.Size = new Size(112, 29);
            FillColorButton.TabIndex = 8;
            FillColorButton.Text = "Цвет заливки";
            FillColorButton.UseVisualStyleBackColor = true;
            FillColorButton.Click += FillColorButton_Click;
            // 
            // BoldlineDrawButton
            // 
            BoldlineDrawButton.AutoSize = true;
            BoldlineDrawButton.Location = new Point(17, 167);
            BoldlineDrawButton.Name = "BoldlineDrawButton";
            BoldlineDrawButton.Size = new Size(132, 24);
            BoldlineDrawButton.TabIndex = 7;
            BoldlineDrawButton.Text = "Толстая линия";
            BoldlineDrawButton.UseVisualStyleBackColor = true;
            BoldlineDrawButton.CheckedChanged += BoldlineDrawButton_CheckedChanged;
            // 
            // ExecuteButton
            // 
            ExecuteButton.Location = new Point(14, 300);
            ExecuteButton.Name = "ExecuteButton";
            ExecuteButton.Size = new Size(104, 29);
            ExecuteButton.TabIndex = 5;
            ExecuteButton.Text = "Выполнить";
            ExecuteButton.UseVisualStyleBackColor = true;
            ExecuteButton.Click += ExecuteButton_Click;
            // 
            // LineColorButton
            // 
            LineColorButton.Location = new Point(123, 300);
            LineColorButton.Name = "LineColorButton";
            LineColorButton.Size = new Size(105, 29);
            LineColorButton.TabIndex = 4;
            LineColorButton.Text = "Цвет линии";
            LineColorButton.UseVisualStyleBackColor = true;
            LineColorButton.Click += LineColorButton_Click;
            // 
            // FillButton
            // 
            FillButton.AutoSize = true;
            FillButton.Location = new Point(16, 449);
            FillButton.Name = "FillButton";
            FillButton.Size = new Size(86, 24);
            FillButton.TabIndex = 3;
            FillButton.TabStop = true;
            FillButton.Text = "Заливка";
            FillButton.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(14, 335);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(94, 29);
            ClearButton.TabIndex = 2;
            ClearButton.Text = "Очистить";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // DDAAlgButton
            // 
            DDAAlgButton.AutoSize = true;
            DDAAlgButton.Location = new Point(16, 389);
            DDAAlgButton.Name = "DDAAlgButton";
            DDAAlgButton.Size = new Size(133, 24);
            DDAAlgButton.TabIndex = 1;
            DDAAlgButton.TabStop = true;
            DDAAlgButton.Text = "Обычный ЦДА";
            DDAAlgButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 8);
            label1.Name = "label1";
            label1.Size = new Size(134, 20);
            label1.TabIndex = 0;
            label1.Text = "Выбор алгоритма";
            // 
            // Lab2Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(954, 542);
            Controls.Add(splitContainer1);
            Name = "Lab2Form";
            Text = "Растровые алгоритмы.";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private RadioButton DDAAlgButton;
        private Label label1;
        private Button ClearButton;
        private PictureBox pictureBox1;
        private RadioButton FillButton;
        public Button LineColorButton;
        private ColorDialog colorDialog1;
        private Button ExecuteButton;
        private CheckBox BoldlineDrawButton;
        private Button FillColorButton;
        private TextBox textBox1;
        private RadioButton BresAlgButton;
        private CheckedListBox AlgListBox;
        private Label label2;
        private TextBox RadiusTextBox;
    }
}