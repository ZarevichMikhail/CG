namespace CG.View.Forms.Lab3
{
    partial class Lab3Form
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            pictureBox1 = new PictureBox();
            DrawVeloButton = new Button();
            label5 = new Label();
            AlgCheckListBox = new CheckedListBox();
            ScalingParameterTextBox = new TextBox();
            label4 = new Label();
            ScalingXYButton = new Button();
            ReflectionXButton = new Button();
            ReflectionYButton = new Button();
            MoveDistanceTextBox = new TextBox();
            ScaleDistanceTextBox = new TextBox();
            label3 = new Label();
            ScaleOYButton = new Button();
            ScaleOXButton = new Button();
            AngleTextBox = new TextBox();
            RotateButton = new Button();
            label2 = new Label();
            StartButton = new Button();
            MoveYDown = new Button();
            MoveYUp = new Button();
            MoveXLeft = new Button();
            MoveXRight = new Button();
            label1 = new Label();
            ClearButton = new Button();
            DrawFigureButton = new Button();
            DrawAxisButton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            MoveVeloButton = new Button();
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
            splitContainer1.Panel2.Controls.Add(MoveVeloButton);
            splitContainer1.Panel2.Controls.Add(DrawVeloButton);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(AlgCheckListBox);
            splitContainer1.Panel2.Controls.Add(ScalingParameterTextBox);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(ScalingXYButton);
            splitContainer1.Panel2.Controls.Add(ReflectionXButton);
            splitContainer1.Panel2.Controls.Add(ReflectionYButton);
            splitContainer1.Panel2.Controls.Add(MoveDistanceTextBox);
            splitContainer1.Panel2.Controls.Add(ScaleDistanceTextBox);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(ScaleOYButton);
            splitContainer1.Panel2.Controls.Add(ScaleOXButton);
            splitContainer1.Panel2.Controls.Add(AngleTextBox);
            splitContainer1.Panel2.Controls.Add(RotateButton);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(StartButton);
            splitContainer1.Panel2.Controls.Add(MoveYDown);
            splitContainer1.Panel2.Controls.Add(MoveYUp);
            splitContainer1.Panel2.Controls.Add(MoveXLeft);
            splitContainer1.Panel2.Controls.Add(MoveXRight);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(ClearButton);
            splitContainer1.Panel2.Controls.Add(DrawFigureButton);
            splitContainer1.Panel2.Controls.Add(DrawAxisButton);
            splitContainer1.Size = new Size(1482, 617);
            splitContainer1.SplitterDistance = 641;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(641, 617);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // DrawVeloButton
            // 
            DrawVeloButton.Location = new Point(193, 296);
            DrawVeloButton.Name = "DrawVeloButton";
            DrawVeloButton.Size = new Size(140, 54);
            DrawVeloButton.TabIndex = 23;
            DrawVeloButton.Text = "Нарисовать велосипед";
            DrawVeloButton.UseVisualStyleBackColor = true;
            DrawVeloButton.Click += DrawVeloButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 356);
            label5.Name = "label5";
            label5.Size = new Size(153, 20);
            label5.TabIndex = 22;
            label5.Text = "Параметры таймера";
            // 
            // AlgCheckListBox
            // 
            AlgCheckListBox.CheckOnClick = true;
            AlgCheckListBox.FormattingEnabled = true;
            AlgCheckListBox.Location = new Point(12, 390);
            AlgCheckListBox.Name = "AlgCheckListBox";
            AlgCheckListBox.Size = new Size(236, 224);
            AlgCheckListBox.TabIndex = 21;
            AlgCheckListBox.SelectedIndexChanged += AlgCheckListBox_SelectedIndexChanged;
            // 
            // ScalingParameterTextBox
            // 
            ScalingParameterTextBox.Location = new Point(652, 165);
            ScalingParameterTextBox.Name = "ScalingParameterTextBox";
            ScalingParameterTextBox.Size = new Size(125, 27);
            ScalingParameterTextBox.TabIndex = 20;
            ScalingParameterTextBox.Text = "1,1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(422, 165);
            label4.Name = "label4";
            label4.Size = new Size(224, 20);
            label4.TabIndex = 19;
            label4.Text = "Множитель масштабирования";
            // 
            // ScalingXYButton
            // 
            ScalingXYButton.Location = new Point(422, 191);
            ScalingXYButton.Name = "ScalingXYButton";
            ScalingXYButton.Size = new Size(199, 29);
            ScalingXYButton.TabIndex = 18;
            ScalingXYButton.Text = "Масштабирование";
            ScalingXYButton.UseVisualStyleBackColor = true;
            ScalingXYButton.Click += ScalingXYButton_Click;
            // 
            // ReflectionXButton
            // 
            ReflectionXButton.AllowDrop = true;
            ReflectionXButton.Location = new Point(193, 226);
            ReflectionXButton.Name = "ReflectionXButton";
            ReflectionXButton.Size = new Size(223, 29);
            ReflectionXButton.TabIndex = 17;
            ReflectionXButton.Text = "Отражение относительно ОХ";
            ReflectionXButton.UseVisualStyleBackColor = true;
            ReflectionXButton.Click += ReflectionXButton_Click;
            // 
            // ReflectionYButton
            // 
            ReflectionYButton.Location = new Point(193, 191);
            ReflectionYButton.Name = "ReflectionYButton";
            ReflectionYButton.Size = new Size(223, 29);
            ReflectionYButton.TabIndex = 16;
            ReflectionYButton.Text = "Отражение относительно ОУ";
            ReflectionYButton.UseVisualStyleBackColor = true;
            ReflectionYButton.Click += ReflectionYButton_Click;
            // 
            // MoveDistanceTextBox
            // 
            MoveDistanceTextBox.Location = new Point(172, 149);
            MoveDistanceTextBox.Name = "MoveDistanceTextBox";
            MoveDistanceTextBox.Size = new Size(56, 27);
            MoveDistanceTextBox.TabIndex = 15;
            MoveDistanceTextBox.Text = "5";
            // 
            // ScaleDistanceTextBox
            // 
            ScaleDistanceTextBox.Location = new Point(514, 27);
            ScaleDistanceTextBox.Name = "ScaleDistanceTextBox";
            ScaleDistanceTextBox.Size = new Size(107, 27);
            ScaleDistanceTextBox.TabIndex = 14;
            ScaleDistanceTextBox.Text = "1,1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(422, 29);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 13;
            label3.Text = "Расстояние:  ";
            // 
            // ScaleOYButton
            // 
            ScaleOYButton.Location = new Point(422, 104);
            ScaleOYButton.Name = "ScaleOYButton";
            ScaleOYButton.Size = new Size(199, 29);
            ScaleOYButton.TabIndex = 12;
            ScaleOYButton.Text = "Смещение по оси ОY";
            ScaleOYButton.UseVisualStyleBackColor = true;
            ScaleOYButton.Click += ScaleOYButton_Click;
            // 
            // ScaleOXButton
            // 
            ScaleOXButton.Location = new Point(422, 69);
            ScaleOXButton.Name = "ScaleOXButton";
            ScaleOXButton.Size = new Size(199, 29);
            ScaleOXButton.TabIndex = 11;
            ScaleOXButton.Text = "Смещение по оси ОХ";
            ScaleOXButton.UseVisualStyleBackColor = true;
            ScaleOXButton.Click += ScaleOXButton_Click;
            // 
            // AngleTextBox
            // 
            AngleTextBox.Location = new Point(313, 26);
            AngleTextBox.Name = "AngleTextBox";
            AngleTextBox.Size = new Size(103, 27);
            AngleTextBox.TabIndex = 10;
            AngleTextBox.Text = "30";
            // 
            // RotateButton
            // 
            RotateButton.Location = new Point(193, 69);
            RotateButton.Name = "RotateButton";
            RotateButton.Size = new Size(223, 29);
            RotateButton.TabIndex = 9;
            RotateButton.Text = "Поворот на заданный угол";
            RotateButton.UseVisualStyleBackColor = true;
            RotateButton.Click += RotateButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(193, 27);
            label2.Name = "label2";
            label2.Size = new Size(124, 20);
            label2.TabIndex = 1;
            label2.Text = "Угол в градусах: ";
            // 
            // StartButton
            // 
            StartButton.Location = new Point(278, 390);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(94, 29);
            StartButton.TabIndex = 8;
            StartButton.Text = "Старт";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // MoveYDown
            // 
            MoveYDown.Location = new Point(12, 296);
            MoveYDown.Name = "MoveYDown";
            MoveYDown.Size = new Size(159, 29);
            MoveYDown.TabIndex = 7;
            MoveYDown.Text = "По оси ОУ вниз";
            MoveYDown.UseVisualStyleBackColor = true;
            MoveYDown.Click += MoveYDown_Click;
            // 
            // MoveYUp
            // 
            MoveYUp.Location = new Point(12, 261);
            MoveYUp.Name = "MoveYUp";
            MoveYUp.Size = new Size(159, 29);
            MoveYUp.TabIndex = 6;
            MoveYUp.Text = "По оси ОУ вверх";
            MoveYUp.UseVisualStyleBackColor = true;
            MoveYUp.Click += MoveYUp_Click;
            // 
            // MoveXLeft
            // 
            MoveXLeft.Location = new Point(12, 226);
            MoveXLeft.Name = "MoveXLeft";
            MoveXLeft.Size = new Size(159, 29);
            MoveXLeft.TabIndex = 5;
            MoveXLeft.Text = "По оси ОХ влево";
            MoveXLeft.UseVisualStyleBackColor = true;
            MoveXLeft.Click += MoveXLeft_Click;
            // 
            // MoveXRight
            // 
            MoveXRight.Location = new Point(12, 191);
            MoveXRight.Name = "MoveXRight";
            MoveXRight.Size = new Size(159, 29);
            MoveXRight.TabIndex = 4;
            MoveXRight.Text = "По оси ОХ вправо";
            MoveXRight.UseVisualStyleBackColor = true;
            MoveXRight.Click += MoveXRight_Click;
            // 
            // label1
            // 
            label1.AccessibleRole = AccessibleRole.None;
            label1.AutoSize = true;
            label1.Location = new Point(15, 152);
            label1.Name = "label1";
            label1.Size = new Size(161, 20);
            label1.TabIndex = 3;
            label1.Text = "Сдвиг на расстояние: ";
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(12, 95);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(159, 29);
            ClearButton.TabIndex = 2;
            ClearButton.Text = "Очистить";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // DrawFigureButton
            // 
            DrawFigureButton.Location = new Point(12, 60);
            DrawFigureButton.Name = "DrawFigureButton";
            DrawFigureButton.Size = new Size(159, 29);
            DrawFigureButton.TabIndex = 1;
            DrawFigureButton.Text = "Нарисовать фигуру";
            DrawFigureButton.UseVisualStyleBackColor = true;
            DrawFigureButton.Click += DrawFigureButton_Click;
            // 
            // DrawAxisButton
            // 
            DrawAxisButton.Location = new Point(12, 25);
            DrawAxisButton.Name = "DrawAxisButton";
            DrawAxisButton.Size = new Size(159, 29);
            DrawAxisButton.TabIndex = 0;
            DrawAxisButton.Text = "Нарисовать оси";
            DrawAxisButton.UseVisualStyleBackColor = true;
            DrawAxisButton.Click += DrawAxisButton_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // MoveVeloButton
            // 
            MoveVeloButton.Location = new Point(339, 296);
            MoveVeloButton.Name = "MoveVeloButton";
            MoveVeloButton.Size = new Size(124, 54);
            MoveVeloButton.TabIndex = 24;
            MoveVeloButton.Text = "Двигать велосипед";
            MoveVeloButton.UseVisualStyleBackColor = true;
            MoveVeloButton.Click += MoveVeloButton_Click;
            // 
            // Lab3Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1482, 617);
            Controls.Add(splitContainer1);
            Name = "Lab3Form";
            Text = "Lab3Form";
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
        private Button MoveXRight;
        private Label label1;
        private Button ClearButton;
        private Button DrawFigureButton;
        private Button DrawAxisButton;
        private Button MoveYDown;
        private Button MoveYUp;
        private Button MoveXLeft;
        private Button StartButton;
        private System.Windows.Forms.Timer timer1;
        private Button RotateButton;
        private Label label2;
        private TextBox AngleTextBox;
        private Button ScaleOXButton;
        private Button ScaleOYButton;
        private TextBox ScaleDistanceTextBox;
        private Label label3;
        private TextBox MoveDistanceTextBox;
        private Button ReflectionXButton;
        private Button ReflectionYButton;
        private Button ScalingXYButton;
        private Label label4;
        private TextBox ScalingParameterTextBox;
        private CheckedListBox AlgCheckListBox;
        private Label label5;
        private Button button2;
        private Button DrawVeloButton;
        private Button MoveVeloButton;
    }
}