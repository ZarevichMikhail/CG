namespace KG.View.Forms
{
    partial class Lab1Form
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
            AdditionButton = new Button();
            SubstractionButton = new Button();
            label4 = new Label();
            label3 = new Label();
            SaveButton = new Button();
            MultiplicationButton = new Button();
            CreateMatrix2Button = new Button();
            CreateMatrix1Button = new Button();
            nstrTextBox = new TextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            CreateFormButton = new Button();
            HelloButton = new Button();
            nstolbTextBox = new TextBox();
            label2 = new Label();
            label5 = new Label();
            ConstantTextBox = new TextBox();
            Matrix1NumberMultiplicatoinButton = new Button();
            label6 = new Label();
            VectorDimentionTextBox = new TextBox();
            CreateVectorButton = new Button();
            SuspendLayout();
            // 
            // AdditionButton
            // 
            AdditionButton.Location = new Point(238, 206);
            AdditionButton.Name = "AdditionButton";
            AdditionButton.Size = new Size(257, 29);
            AdditionButton.TabIndex = 26;
            AdditionButton.Text = "Сложение";
            AdditionButton.UseVisualStyleBackColor = true;
            AdditionButton.Click += AdditionButton_Click;
            // 
            // SubstractionButton
            // 
            SubstractionButton.Location = new Point(238, 241);
            SubstractionButton.Name = "SubstractionButton";
            SubstractionButton.Size = new Size(257, 29);
            SubstractionButton.TabIndex = 25;
            SubstractionButton.Text = "Вычитание";
            SubstractionButton.UseVisualStyleBackColor = true;
            SubstractionButton.Click += SubstractionButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(391, 165);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 24;
            label4.Text = "label4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(391, 129);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 23;
            label3.Text = "label3";
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(238, 311);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(257, 29);
            SaveButton.TabIndex = 22;
            SaveButton.Text = "Сохранить в файле \"Res_Matr.txt\"";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // MultiplicationButton
            // 
            MultiplicationButton.Location = new Point(238, 276);
            MultiplicationButton.Name = "MultiplicationButton";
            MultiplicationButton.Size = new Size(257, 29);
            MultiplicationButton.TabIndex = 21;
            MultiplicationButton.Text = "Умножение";
            MultiplicationButton.UseVisualStyleBackColor = true;
            MultiplicationButton.Click += MultiplicationButton_Click;
            // 
            // CreateMatrix2Button
            // 
            CreateMatrix2Button.Location = new Point(238, 161);
            CreateMatrix2Button.Name = "CreateMatrix2Button";
            CreateMatrix2Button.Size = new Size(138, 29);
            CreateMatrix2Button.TabIndex = 20;
            CreateMatrix2Button.Text = "Ввод матрицы 2";
            CreateMatrix2Button.UseVisualStyleBackColor = true;
            CreateMatrix2Button.Click += CreateMatrix2Button_Click;
            // 
            // CreateMatrix1Button
            // 
            CreateMatrix1Button.Location = new Point(238, 124);
            CreateMatrix1Button.Name = "CreateMatrix1Button";
            CreateMatrix1Button.Size = new Size(138, 31);
            CreateMatrix1Button.TabIndex = 19;
            CreateMatrix1Button.Text = "Ввод матрицы 1";
            CreateMatrix1Button.UseVisualStyleBackColor = true;
            CreateMatrix1Button.Click += CreateMatrix1Button_Click;
            // 
            // nstrTextBox
            // 
            nstrTextBox.Location = new Point(370, 52);
            nstrTextBox.Name = "nstrTextBox";
            nstrTextBox.RightToLeft = RightToLeft.No;
            nstrTextBox.Size = new Size(125, 27);
            nstrTextBox.TabIndex = 18;
            nstrTextBox.Leave += nstrTextBox_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(238, 55);
            label1.Name = "label1";
            label1.Size = new Size(108, 20);
            label1.TabIndex = 17;
            label1.Text = "Число строк =";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(46, 256);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(146, 27);
            textBox1.TabIndex = 16;
            // 
            // CreateFormButton
            // 
            CreateFormButton.Location = new Point(12, 127);
            CreateFormButton.Name = "CreateFormButton";
            CreateFormButton.Size = new Size(200, 66);
            CreateFormButton.TabIndex = 15;
            CreateFormButton.Text = "Create new form";
            CreateFormButton.UseVisualStyleBackColor = true;
            CreateFormButton.Click += CreateFormButton_Click;
            // 
            // HelloButton
            // 
            HelloButton.AccessibleRole = AccessibleRole.None;
            HelloButton.Location = new Point(12, 52);
            HelloButton.Name = "HelloButton";
            HelloButton.Size = new Size(200, 66);
            HelloButton.TabIndex = 14;
            HelloButton.Text = "Hello world Button";
            HelloButton.UseVisualStyleBackColor = true;
            HelloButton.Click += HelloButton_Click;
            // 
            // nstolbTextBox
            // 
            nstolbTextBox.Location = new Point(370, 85);
            nstolbTextBox.Name = "nstolbTextBox";
            nstolbTextBox.Size = new Size(125, 27);
            nstolbTextBox.TabIndex = 28;
            nstolbTextBox.Leave += nstolbTextBox_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(238, 88);
            label2.Name = "label2";
            label2.Size = new Size(135, 20);
            label2.TabIndex = 27;
            label2.Text = "Число столбцов =";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(542, 52);
            label5.Name = "label5";
            label5.Size = new Size(98, 20);
            label5.TabIndex = 29;
            label5.Text = "Константа = ";
            // 
            // ConstantTextBox
            // 
            ConstantTextBox.Location = new Point(646, 49);
            ConstantTextBox.Name = "ConstantTextBox";
            ConstantTextBox.Size = new Size(125, 27);
            ConstantTextBox.TabIndex = 30;
            // 
            // Matrix1NumberMultiplicatoinButton
            // 
            Matrix1NumberMultiplicatoinButton.Location = new Point(524, 206);
            Matrix1NumberMultiplicatoinButton.Name = "Matrix1NumberMultiplicatoinButton";
            Matrix1NumberMultiplicatoinButton.Size = new Size(272, 29);
            Matrix1NumberMultiplicatoinButton.TabIndex = 31;
            Matrix1NumberMultiplicatoinButton.Text = "Умножение матрицы 1 на константу";
            Matrix1NumberMultiplicatoinButton.UseVisualStyleBackColor = true;
            Matrix1NumberMultiplicatoinButton.Click += Matrix1NumberMultiplicatoinButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(542, 85);
            label6.Name = "label6";
            label6.Size = new Size(176, 20);
            label6.TabIndex = 32;
            label6.Text = "Размерность вектора = ";
            // 
            // VectorDimentionTextBox
            // 
            VectorDimentionTextBox.Location = new Point(711, 82);
            VectorDimentionTextBox.Name = "VectorDimentionTextBox";
            VectorDimentionTextBox.Size = new Size(125, 27);
            VectorDimentionTextBox.TabIndex = 33;
            // 
            // CreateVectorButton
            // 
            CreateVectorButton.Location = new Point(524, 241);
            CreateVectorButton.Name = "CreateVectorButton";
            CreateVectorButton.Size = new Size(145, 29);
            CreateVectorButton.TabIndex = 34;
            CreateVectorButton.Text = "Создание вектора";
            CreateVectorButton.UseVisualStyleBackColor = true;
            CreateVectorButton.Click += CreateVectorButton_Click;
            // 
            // Lab1Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1076, 450);
            Controls.Add(CreateVectorButton);
            Controls.Add(VectorDimentionTextBox);
            Controls.Add(label6);
            Controls.Add(Matrix1NumberMultiplicatoinButton);
            Controls.Add(ConstantTextBox);
            Controls.Add(label5);
            Controls.Add(nstolbTextBox);
            Controls.Add(label2);
            Controls.Add(AdditionButton);
            Controls.Add(SubstractionButton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(SaveButton);
            Controls.Add(MultiplicationButton);
            Controls.Add(CreateMatrix2Button);
            Controls.Add(CreateMatrix1Button);
            Controls.Add(nstrTextBox);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(CreateFormButton);
            Controls.Add(HelloButton);
            Name = "Lab1Form";
            Text = "Lab1Form";
            Load += Lab1Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button AdditionButton;
        private Button SubstractionButton;
        private Label label4;
        private Label label3;
        private Button SaveButton;
        private Button MultiplicationButton;
        private Button CreateMatrix2Button;
        private Button CreateMatrix1Button;
        private TextBox nstrTextBox;
        private Label label1;
        public TextBox textBox1;
        private Button CreateFormButton;
        private Button HelloButton;
        private TextBox nstolbTextBox;
        private Label label2;
        private Label label5;
        private TextBox ConstantTextBox;
        private Button Matrix1NumberMultiplicatoinButton;
        private Label label6;
        private TextBox VectorDimentionTextBox;
        private Button CreateVectorButton;
    }
}