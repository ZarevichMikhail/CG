namespace CG.View.Forms.Diagram
{
    partial class DiagramForm
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
            CreatePieChartButton = new Button();
            ClearButton = new Button();
            CreateHistogramButton = new Button();
            EnterValuesButton = new Button();
            NumberTextBox = new TextBox();
            label1 = new Label();
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
            splitContainer1.Panel2.Controls.Add(CreatePieChartButton);
            splitContainer1.Panel2.Controls.Add(ClearButton);
            splitContainer1.Panel2.Controls.Add(CreateHistogramButton);
            splitContainer1.Panel2.Controls.Add(EnterValuesButton);
            splitContainer1.Panel2.Controls.Add(NumberTextBox);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 559;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(559, 450);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // CreatePieChartButton
            // 
            CreatePieChartButton.Location = new Point(14, 216);
            CreatePieChartButton.Name = "CreatePieChartButton";
            CreatePieChartButton.Size = new Size(141, 76);
            CreatePieChartButton.TabIndex = 4;
            CreatePieChartButton.Text = "Создать круговую диаграмму (пока не сделал)";
            CreatePieChartButton.UseVisualStyleBackColor = true;
            CreatePieChartButton.Click += CreatePieChartButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(14, 298);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(141, 77);
            ClearButton.TabIndex = 1;
            ClearButton.Text = "Сбросить значения";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // CreateHistogramButton
            // 
            CreateHistogramButton.Location = new Point(14, 134);
            CreateHistogramButton.Name = "CreateHistogramButton";
            CreateHistogramButton.Size = new Size(141, 76);
            CreateHistogramButton.TabIndex = 3;
            CreateHistogramButton.Text = "Создать столбчатую диаграмму";
            CreateHistogramButton.UseVisualStyleBackColor = true;
            CreateHistogramButton.Click += CreateHistogramButton_Click;
            // 
            // EnterValuesButton
            // 
            EnterValuesButton.Location = new Point(14, 52);
            EnterValuesButton.Name = "EnterValuesButton";
            EnterValuesButton.Size = new Size(141, 76);
            EnterValuesButton.TabIndex = 2;
            EnterValuesButton.Text = "Ввести значения";
            EnterValuesButton.UseVisualStyleBackColor = true;
            EnterValuesButton.Click += EnterValuesButton_Click;
            // 
            // NumberTextBox
            // 
            NumberTextBox.Location = new Point(176, 17);
            NumberTextBox.Name = "NumberTextBox";
            NumberTextBox.Size = new Size(49, 27);
            NumberTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 20);
            label1.Name = "label1";
            label1.Size = new Size(171, 20);
            label1.TabIndex = 0;
            label1.Text = "Количество элементов:";
            // 
            // DiagramForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "DiagramForm";
            Text = "DiagramForm";
            Load += DiagramForm_Load;
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
        private PictureBox pictureBox1;
        private Label label1;
        private Button CreateHistogramButton;
        private Button EnterValuesButton;
        private TextBox NumberTextBox;
        private Button CreatePieChartButton;
        private Button ClearButton;
    }
}