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
            DrawCustomAxisButton = new Button();
            Z2TextBox = new TextBox();
            Z1TextBox = new TextBox();
            Y2TextBox = new TextBox();
            Y1TextBox = new TextBox();
            X2TextBox = new TextBox();
            X1TextBox = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            ReflextionOZButton = new Button();
            ReflectionXButton = new Button();
            ReflectionYButton = new Button();
            ScaleOZButton = new Button();
            ScalingParameterTextBox = new TextBox();
            label4 = new Label();
            ScalingXYButton = new Button();
            ScaleDistanceTextBox = new TextBox();
            label3 = new Label();
            ScaleOYButton = new Button();
            ScaleOXButton = new Button();
            MoveZBackwards = new Button();
            MoveZForwards = new Button();
            DistanceTextBox = new TextBox();
            MoveYDown = new Button();
            MoveYUp = new Button();
            MoveXLeft = new Button();
            MoveXRight = new Button();
            label2 = new Label();
            RotateZButton = new Button();
            RotateXButton = new Button();
            RotateYButton = new Button();
            AngleTextBox = new TextBox();
            label1 = new Label();
            DrawFigureButton = new Button();
            DrawAxisButton = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            RotateCustomAxisButton = new Button();
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
            splitContainer1.Panel2.Controls.Add(RotateCustomAxisButton);
            splitContainer1.Panel2.Controls.Add(DrawCustomAxisButton);
            splitContainer1.Panel2.Controls.Add(Z2TextBox);
            splitContainer1.Panel2.Controls.Add(Z1TextBox);
            splitContainer1.Panel2.Controls.Add(Y2TextBox);
            splitContainer1.Panel2.Controls.Add(Y1TextBox);
            splitContainer1.Panel2.Controls.Add(X2TextBox);
            splitContainer1.Panel2.Controls.Add(X1TextBox);
            splitContainer1.Panel2.Controls.Add(label8);
            splitContainer1.Panel2.Controls.Add(label7);
            splitContainer1.Panel2.Controls.Add(label6);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(ReflextionOZButton);
            splitContainer1.Panel2.Controls.Add(ReflectionXButton);
            splitContainer1.Panel2.Controls.Add(ReflectionYButton);
            splitContainer1.Panel2.Controls.Add(ScaleOZButton);
            splitContainer1.Panel2.Controls.Add(ScalingParameterTextBox);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(ScalingXYButton);
            splitContainer1.Panel2.Controls.Add(ScaleDistanceTextBox);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(ScaleOYButton);
            splitContainer1.Panel2.Controls.Add(ScaleOXButton);
            splitContainer1.Panel2.Controls.Add(MoveZBackwards);
            splitContainer1.Panel2.Controls.Add(MoveZForwards);
            splitContainer1.Panel2.Controls.Add(DistanceTextBox);
            splitContainer1.Panel2.Controls.Add(MoveYDown);
            splitContainer1.Panel2.Controls.Add(MoveYUp);
            splitContainer1.Panel2.Controls.Add(MoveXLeft);
            splitContainer1.Panel2.Controls.Add(MoveXRight);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(RotateZButton);
            splitContainer1.Panel2.Controls.Add(RotateXButton);
            splitContainer1.Panel2.Controls.Add(RotateYButton);
            splitContainer1.Panel2.Controls.Add(AngleTextBox);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(DrawFigureButton);
            splitContainer1.Panel2.Controls.Add(DrawAxisButton);
            splitContainer1.Size = new Size(1665, 640);
            splitContainer1.SplitterDistance = 725;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(725, 640);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // DrawCustomAxisButton
            // 
            DrawCustomAxisButton.Location = new Point(578, 136);
            DrawCustomAxisButton.Name = "DrawCustomAxisButton";
            DrawCustomAxisButton.Size = new Size(157, 29);
            DrawCustomAxisButton.TabIndex = 42;
            DrawCustomAxisButton.Text = "Нарисовать ось";
            DrawCustomAxisButton.UseVisualStyleBackColor = true;
            DrawCustomAxisButton.Click += DrawCustomAxisButton_Click;
            // 
            // Z2TextBox
            // 
            Z2TextBox.Location = new Point(768, 86);
            Z2TextBox.Name = "Z2TextBox";
            Z2TextBox.Size = new Size(49, 27);
            Z2TextBox.TabIndex = 41;
            Z2TextBox.Text = "-20";
            // 
            // Z1TextBox
            // 
            Z1TextBox.Location = new Point(768, 52);
            Z1TextBox.Name = "Z1TextBox";
            Z1TextBox.Size = new Size(49, 27);
            Z1TextBox.TabIndex = 40;
            Z1TextBox.Text = "20";
            // 
            // Y2TextBox
            // 
            Y2TextBox.Location = new Point(686, 86);
            Y2TextBox.Name = "Y2TextBox";
            Y2TextBox.Size = new Size(49, 27);
            Y2TextBox.TabIndex = 39;
            Y2TextBox.Text = "100";
            // 
            // Y1TextBox
            // 
            Y1TextBox.Location = new Point(686, 51);
            Y1TextBox.Name = "Y1TextBox";
            Y1TextBox.Size = new Size(49, 27);
            Y1TextBox.TabIndex = 38;
            Y1TextBox.Text = "400";
            // 
            // X2TextBox
            // 
            X2TextBox.Location = new Point(605, 86);
            X2TextBox.Name = "X2TextBox";
            X2TextBox.Size = new Size(49, 27);
            X2TextBox.TabIndex = 37;
            X2TextBox.Text = "-200";
            // 
            // X1TextBox
            // 
            X1TextBox.Location = new Point(605, 53);
            X1TextBox.Name = "X1TextBox";
            X1TextBox.Size = new Size(49, 27);
            X1TextBox.TabIndex = 36;
            X1TextBox.Text = "200";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(741, 55);
            label8.Name = "label8";
            label8.Size = new Size(21, 20);
            label8.TabIndex = 35;
            label8.Text = "Z:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(660, 56);
            label7.Name = "label7";
            label7.Size = new Size(24, 20);
            label7.TabIndex = 34;
            label7.Text = "Y: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(578, 55);
            label6.Name = "label6";
            label6.Size = new Size(21, 20);
            label6.TabIndex = 33;
            label6.Text = "X:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(578, 25);
            label5.Name = "label5";
            label5.Size = new Size(242, 20);
            label5.TabIndex = 32;
            label5.Text = "Координаты точек оси вращения";
            // 
            // ReflextionOZButton
            // 
            ReflextionOZButton.Location = new Point(18, 433);
            ReflextionOZButton.Name = "ReflextionOZButton";
            ReflextionOZButton.Size = new Size(223, 29);
            ReflextionOZButton.TabIndex = 31;
            ReflextionOZButton.Text = "Отражение относительно OZ";
            ReflextionOZButton.UseVisualStyleBackColor = true;
            ReflextionOZButton.Click += ReflextionOZButton_Click;
            // 
            // ReflectionXButton
            // 
            ReflectionXButton.AllowDrop = true;
            ReflectionXButton.Location = new Point(18, 363);
            ReflectionXButton.Name = "ReflectionXButton";
            ReflectionXButton.Size = new Size(223, 29);
            ReflectionXButton.TabIndex = 30;
            ReflectionXButton.Text = "Отражение относительно ОХ";
            ReflectionXButton.UseVisualStyleBackColor = true;
            ReflectionXButton.Click += ReflectionXButton_Click;
            // 
            // ReflectionYButton
            // 
            ReflectionYButton.Location = new Point(18, 398);
            ReflectionYButton.Name = "ReflectionYButton";
            ReflectionYButton.Size = new Size(223, 29);
            ReflectionYButton.TabIndex = 29;
            ReflectionYButton.Text = "Отражение относительно ОУ";
            ReflectionYButton.UseVisualStyleBackColor = true;
            ReflectionYButton.Click += ReflectionYButton_Click;
            // 
            // ScaleOZButton
            // 
            ScaleOZButton.Location = new Point(260, 270);
            ScaleOZButton.Name = "ScaleOZButton";
            ScaleOZButton.Size = new Size(197, 29);
            ScaleOZButton.TabIndex = 28;
            ScaleOZButton.Text = "Смещение по оси OZ";
            ScaleOZButton.UseVisualStyleBackColor = true;
            ScaleOZButton.Click += ScaleOZButton_Click;
            // 
            // ScalingParameterTextBox
            // 
            ScalingParameterTextBox.Location = new Point(489, 323);
            ScalingParameterTextBox.Name = "ScalingParameterTextBox";
            ScalingParameterTextBox.Size = new Size(43, 27);
            ScalingParameterTextBox.TabIndex = 27;
            ScalingParameterTextBox.Text = "1,1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(259, 326);
            label4.Name = "label4";
            label4.Size = new Size(224, 20);
            label4.TabIndex = 26;
            label4.Text = "Множитель масштабирования";
            // 
            // ScalingXYButton
            // 
            ScalingXYButton.Location = new Point(259, 363);
            ScalingXYButton.Name = "ScalingXYButton";
            ScalingXYButton.Size = new Size(199, 29);
            ScalingXYButton.TabIndex = 25;
            ScalingXYButton.Text = "Масштабирование";
            ScalingXYButton.UseVisualStyleBackColor = true;
            ScalingXYButton.Click += ScalingXYButton_Click;
            // 
            // ScaleDistanceTextBox
            // 
            ScaleDistanceTextBox.Location = new Point(350, 158);
            ScaleDistanceTextBox.Name = "ScaleDistanceTextBox";
            ScaleDistanceTextBox.Size = new Size(107, 27);
            ScaleDistanceTextBox.TabIndex = 24;
            ScaleDistanceTextBox.Text = "1,1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(258, 160);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 23;
            label3.Text = "Расстояние:  ";
            // 
            // ScaleOYButton
            // 
            ScaleOYButton.Location = new Point(258, 235);
            ScaleOYButton.Name = "ScaleOYButton";
            ScaleOYButton.Size = new Size(199, 29);
            ScaleOYButton.TabIndex = 22;
            ScaleOYButton.Text = "Смещение по оси ОY";
            ScaleOYButton.UseVisualStyleBackColor = true;
            ScaleOYButton.Click += ScaleOYButton_Click;
            // 
            // ScaleOXButton
            // 
            ScaleOXButton.Location = new Point(258, 200);
            ScaleOXButton.Name = "ScaleOXButton";
            ScaleOXButton.Size = new Size(199, 29);
            ScaleOXButton.TabIndex = 21;
            ScaleOXButton.Text = "Смещение по оси ОХ";
            ScaleOXButton.UseVisualStyleBackColor = true;
            ScaleOXButton.Click += ScaleOXButton_Click;
            // 
            // MoveZBackwards
            // 
            MoveZBackwards.Location = new Point(21, 296);
            MoveZBackwards.Name = "MoveZBackwards";
            MoveZBackwards.Size = new Size(159, 29);
            MoveZBackwards.TabIndex = 14;
            MoveZBackwards.Text = "По оси OZ назад";
            MoveZBackwards.UseVisualStyleBackColor = true;
            MoveZBackwards.Click += MoveZBackwards_Click;
            // 
            // MoveZForwards
            // 
            MoveZForwards.Location = new Point(21, 261);
            MoveZForwards.Name = "MoveZForwards";
            MoveZForwards.Size = new Size(159, 29);
            MoveZForwards.TabIndex = 13;
            MoveZForwards.Text = "По оси OZ вперёд";
            MoveZForwards.UseVisualStyleBackColor = true;
            MoveZForwards.Click += MoveZForwards_Click;
            // 
            // DistanceTextBox
            // 
            DistanceTextBox.Location = new Point(184, 88);
            DistanceTextBox.Name = "DistanceTextBox";
            DistanceTextBox.Size = new Size(39, 27);
            DistanceTextBox.TabIndex = 12;
            DistanceTextBox.Text = "20";
            // 
            // MoveYDown
            // 
            MoveYDown.Location = new Point(21, 226);
            MoveYDown.Name = "MoveYDown";
            MoveYDown.Size = new Size(159, 29);
            MoveYDown.TabIndex = 11;
            MoveYDown.Text = "По оси ОУ вниз";
            MoveYDown.UseVisualStyleBackColor = true;
            MoveYDown.Click += MoveYDown_Click;
            // 
            // MoveYUp
            // 
            MoveYUp.Location = new Point(21, 191);
            MoveYUp.Name = "MoveYUp";
            MoveYUp.Size = new Size(159, 29);
            MoveYUp.TabIndex = 10;
            MoveYUp.Text = "По оси ОУ вверх";
            MoveYUp.UseVisualStyleBackColor = true;
            MoveYUp.Click += MoveYUp_Click;
            // 
            // MoveXLeft
            // 
            MoveXLeft.Location = new Point(21, 156);
            MoveXLeft.Name = "MoveXLeft";
            MoveXLeft.Size = new Size(159, 29);
            MoveXLeft.TabIndex = 9;
            MoveXLeft.Text = "По оси ОХ влево";
            MoveXLeft.UseVisualStyleBackColor = true;
            MoveXLeft.Click += MoveXLeft_Click;
            // 
            // MoveXRight
            // 
            MoveXRight.Location = new Point(21, 121);
            MoveXRight.Name = "MoveXRight";
            MoveXRight.Size = new Size(159, 29);
            MoveXRight.TabIndex = 8;
            MoveXRight.Text = "По оси ОХ вправо";
            MoveXRight.UseVisualStyleBackColor = true;
            MoveXRight.Click += MoveXRight_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 91);
            label2.Name = "label2";
            label2.Size = new Size(157, 20);
            label2.TabIndex = 7;
            label2.Text = "Сдвиг на расстояние:";
            // 
            // RotateZButton
            // 
            RotateZButton.Location = new Point(260, 121);
            RotateZButton.Name = "RotateZButton";
            RotateZButton.Size = new Size(261, 29);
            RotateZButton.TabIndex = 6;
            RotateZButton.Text = "Поворот относительно оси OZ";
            RotateZButton.UseVisualStyleBackColor = true;
            RotateZButton.Click += RotateZButton_Click;
            // 
            // RotateXButton
            // 
            RotateXButton.Location = new Point(260, 51);
            RotateXButton.Name = "RotateXButton";
            RotateXButton.Size = new Size(261, 29);
            RotateXButton.TabIndex = 5;
            RotateXButton.Text = "Поворот относительно оси OX";
            RotateXButton.UseVisualStyleBackColor = true;
            RotateXButton.Click += RotateXButton_Click;
            // 
            // RotateYButton
            // 
            RotateYButton.Location = new Point(260, 86);
            RotateYButton.Name = "RotateYButton";
            RotateYButton.Size = new Size(261, 29);
            RotateYButton.TabIndex = 4;
            RotateYButton.Text = "Поворот относительно оси OY";
            RotateYButton.UseVisualStyleBackColor = true;
            RotateYButton.Click += RotateYButton_Click;
            // 
            // AngleTextBox
            // 
            AngleTextBox.Location = new Point(386, 12);
            AngleTextBox.Name = "AngleTextBox";
            AngleTextBox.Size = new Size(125, 27);
            AngleTextBox.TabIndex = 3;
            AngleTextBox.Text = "30";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(260, 16);
            label1.Name = "label1";
            label1.Size = new Size(120, 20);
            label1.TabIndex = 2;
            label1.Text = "Угол в градусах:";
            // 
            // DrawFigureButton
            // 
            DrawFigureButton.Location = new Point(18, 51);
            DrawFigureButton.Name = "DrawFigureButton";
            DrawFigureButton.Size = new Size(171, 29);
            DrawFigureButton.TabIndex = 1;
            DrawFigureButton.Text = "Нарисовать тэтраэдр";
            DrawFigureButton.UseVisualStyleBackColor = true;
            DrawFigureButton.Click += DrawFigureButton_Click;
            // 
            // DrawAxisButton
            // 
            DrawAxisButton.Location = new Point(18, 16);
            DrawAxisButton.Name = "DrawAxisButton";
            DrawAxisButton.Size = new Size(148, 29);
            DrawAxisButton.TabIndex = 0;
            DrawAxisButton.Text = "Нарисовать оси";
            DrawAxisButton.UseVisualStyleBackColor = true;
            DrawAxisButton.Click += DrawAxisButton_Click;
            // 
            // RotateCustomAxisButton
            // 
            RotateCustomAxisButton.Location = new Point(519, 171);
            RotateCustomAxisButton.Name = "RotateCustomAxisButton";
            RotateCustomAxisButton.Size = new Size(267, 29);
            RotateCustomAxisButton.TabIndex = 43;
            RotateCustomAxisButton.Text = "Поворот относительно новой оси";
            RotateCustomAxisButton.UseVisualStyleBackColor = true;
            RotateCustomAxisButton.Click += RotateCustomAxisButton_Click;
            // 
            // Lab4Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1665, 640);
            Controls.Add(splitContainer1);
            Name = "Lab4Form";
            Text = "Form1";
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
        private Button DrawFigureButton;
        private Button DrawAxisButton;
        private TextBox AngleTextBox;
        private Label label1;
        private Button RotateYButton;
        private Button RotateXButton;
        private Button RotateZButton;
        private Label label2;
        private Button MoveYDown;
        private Button MoveYUp;
        private Button MoveXLeft;
        private Button MoveXRight;
        private TextBox DistanceTextBox;
        private Button MoveZForwards;
        private Button MoveZBackwards;
        private TextBox ScalingParameterTextBox;
        private Label label4;
        private Button ScalingXYButton;
        private TextBox ScaleDistanceTextBox;
        private Label label3;
        private Button ScaleOYButton;
        private Button ScaleOXButton;
        private Button ScaleOZButton;
        private Button ReflextionOZButton;
        private Button ReflectionXButton;
        private Button ReflectionYButton;
        private Label label6;
        private Label label5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox Z2TextBox;
        private TextBox Z1TextBox;
        private TextBox Y2TextBox;
        private TextBox Y1TextBox;
        private TextBox X2TextBox;
        private TextBox X1TextBox;
        private Label label8;
        private Label label7;
        private Button DrawCustomAxisButton;
        private Button RotateCustomAxisButton;
    }
}