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
            CreatePolygon = new Button();
            label2 = new Label();
            RadiusTextBox = new TextBox();
            AlgListBox = new CheckedListBox();
            textBox1 = new TextBox();
            FillColorButton = new Button();
            BoldlineDrawButton = new CheckBox();
            ExecuteButton = new Button();
            LineColorButton = new Button();
            ClearButton = new Button();
            label1 = new Label();
            colorDialog1 = new ColorDialog();
            ClipLines1 = new Button();
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
            splitContainer1.Panel2.Controls.Add(ClipLines1);
            splitContainer1.Panel2.Controls.Add(CreatePolygon);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(RadiusTextBox);
            splitContainer1.Panel2.Controls.Add(AlgListBox);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(FillColorButton);
            splitContainer1.Panel2.Controls.Add(BoldlineDrawButton);
            splitContainer1.Panel2.Controls.Add(ExecuteButton);
            splitContainer1.Panel2.Controls.Add(LineColorButton);
            splitContainer1.Panel2.Controls.Add(ClearButton);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Size = new Size(1018, 616);
            splitContainer1.SplitterDistance = 598;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(598, 616);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // CreatePolygon
            // 
            CreatePolygon.Location = new Point(12, 331);
            CreatePolygon.Name = "CreatePolygon";
            CreatePolygon.Size = new Size(190, 29);
            CreatePolygon.TabIndex = 16;
            CreatePolygon.Text = "Создать многоугольник";
            CreatePolygon.UseVisualStyleBackColor = true;
            CreatePolygon.Click += CreatePolygon_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 301);
            label2.Name = "label2";
            label2.Size = new Size(143, 20);
            label2.TabIndex = 14;
            label2.Text = "Радиус окружности";
            // 
            // RadiusTextBox
            // 
            RadiusTextBox.Location = new Point(14, 298);
            RadiusTextBox.Name = "RadiusTextBox";
            RadiusTextBox.Size = new Size(39, 27);
            RadiusTextBox.TabIndex = 13;
            RadiusTextBox.Text = "80";
            // 
            // AlgListBox
            // 
            AlgListBox.CheckOnClick = true;
            AlgListBox.FormattingEnabled = true;
            AlgListBox.Location = new Point(17, 30);
            AlgListBox.Name = "AlgListBox";
            AlgListBox.Size = new Size(361, 224);
            AlgListBox.TabIndex = 12;
            AlgListBox.SelectedIndexChanged += AlgListBox_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 362);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(166, 40);
            textBox1.TabIndex = 10;
            // 
            // FillColorButton
            // 
            FillColorButton.Location = new Point(124, 443);
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
            BoldlineDrawButton.Location = new Point(17, 268);
            BoldlineDrawButton.Name = "BoldlineDrawButton";
            BoldlineDrawButton.Size = new Size(132, 24);
            BoldlineDrawButton.TabIndex = 7;
            BoldlineDrawButton.Text = "Толстая линия";
            BoldlineDrawButton.UseVisualStyleBackColor = true;
            BoldlineDrawButton.CheckedChanged += BoldlineDrawButton_CheckedChanged;
            // 
            // ExecuteButton
            // 
            ExecuteButton.Location = new Point(14, 408);
            ExecuteButton.Name = "ExecuteButton";
            ExecuteButton.Size = new Size(104, 29);
            ExecuteButton.TabIndex = 5;
            ExecuteButton.Text = "Выполнить";
            ExecuteButton.UseVisualStyleBackColor = true;
            ExecuteButton.Click += ExecuteButton_Click;
            // 
            // LineColorButton
            // 
            LineColorButton.Location = new Point(123, 408);
            LineColorButton.Name = "LineColorButton";
            LineColorButton.Size = new Size(105, 29);
            LineColorButton.TabIndex = 4;
            LineColorButton.Text = "Цвет линии";
            LineColorButton.UseVisualStyleBackColor = true;
            LineColorButton.Click += LineColorButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(14, 443);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(94, 29);
            ClearButton.TabIndex = 2;
            ClearButton.Text = "Очистить";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
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
            // ClipLines1
            // 
            ClipLines1.Location = new Point(208, 331);
            ClipLines1.Name = "ClipLines1";
            ClipLines1.Size = new Size(170, 29);
            ClipLines1.TabIndex = 17;
            ClipLines1.Text = "Отсечение отрезков";
            ClipLines1.UseVisualStyleBackColor = true;
            ClipLines1.Click += ClipLines1_Click;
            // 
            // Lab2Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1018, 616);
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
        private Label label1;
        private Button ClearButton;
        private PictureBox pictureBox1;
        public Button LineColorButton;
        private ColorDialog colorDialog1;
        private Button ExecuteButton;
        private CheckBox BoldlineDrawButton;
        private Button FillColorButton;
        private TextBox textBox1;
        private CheckedListBox AlgListBox;
        private Label label2;
        private TextBox RadiusTextBox;
        internal Button CreatePolygon;
        internal Button ClipLines1;
    }
}