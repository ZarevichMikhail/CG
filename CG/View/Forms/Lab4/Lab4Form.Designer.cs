namespace CG.View.Forms.Lab4
{
    partial class Lab4Form
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
            DrawFigureButton = new Button();
            DrawAxisButton = new Button();
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
            splitContainer1.Panel2.Controls.Add(DrawFigureButton);
            splitContainer1.Panel2.Controls.Add(DrawAxisButton);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 552;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(552, 450);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // DrawFigureButton
            // 
            DrawFigureButton.Location = new Point(22, 123);
            DrawFigureButton.Name = "DrawFigureButton";
            DrawFigureButton.Size = new Size(171, 29);
            DrawFigureButton.TabIndex = 1;
            DrawFigureButton.Text = "Нарисовать тэтраэдр";
            DrawFigureButton.UseVisualStyleBackColor = true;
            DrawFigureButton.Click += DrawFigureButton_Click;
            // 
            // DrawAxisButton
            // 
            DrawAxisButton.Location = new Point(22, 52);
            DrawAxisButton.Name = "DrawAxisButton";
            DrawAxisButton.Size = new Size(148, 33);
            DrawAxisButton.TabIndex = 0;
            DrawAxisButton.Text = "Нарисовать оси";
            DrawAxisButton.UseVisualStyleBackColor = true;
            DrawAxisButton.Click += DrawAxisButton_Click;
            // 
            // Lab4Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "Lab4Form";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private PictureBox pictureBox1;
        private Button DrawFigureButton;
        private Button DrawAxisButton;
    }
}