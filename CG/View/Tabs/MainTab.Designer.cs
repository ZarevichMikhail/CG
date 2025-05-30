namespace CG.View
{
    partial class MainTab
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            Lab1Button = new Button();
            Lab2Button = new Button();
            Lab3Button = new Button();
            Lab4Button = new Button();
            DiagramFormButton = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(109, 42);
            label2.Name = "label2";
            label2.Size = new Size(279, 20);
            label2.TabIndex = 14;
            label2.Text = "Заревич, Лим, Тютюнников, Черкашин";
            // 
            // Lab1Button
            // 
            Lab1Button.Location = new Point(29, 106);
            Lab1Button.Name = "Lab1Button";
            Lab1Button.Size = new Size(164, 75);
            Lab1Button.TabIndex = 15;
            Lab1Button.Text = "Лабораторная работа №1";
            Lab1Button.UseVisualStyleBackColor = true;
            Lab1Button.Click += Lab1Button_Click;
            // 
            // Lab2Button
            // 
            Lab2Button.Location = new Point(215, 106);
            Lab2Button.Name = "Lab2Button";
            Lab2Button.Size = new Size(164, 75);
            Lab2Button.TabIndex = 16;
            Lab2Button.Text = "Лабораторная работа №2";
            Lab2Button.UseVisualStyleBackColor = true;
            Lab2Button.Click += Lab2Button_Click;
            // 
            // Lab3Button
            // 
            Lab3Button.Location = new Point(398, 106);
            Lab3Button.Name = "Lab3Button";
            Lab3Button.Size = new Size(164, 75);
            Lab3Button.TabIndex = 17;
            Lab3Button.Text = "Лабораторная работа №3";
            Lab3Button.UseVisualStyleBackColor = true;
            Lab3Button.Click += Lab3Button_Click;
            // 
            // Lab4Button
            // 
            Lab4Button.Location = new Point(584, 106);
            Lab4Button.Name = "Lab4Button";
            Lab4Button.Size = new Size(164, 75);
            Lab4Button.TabIndex = 18;
            Lab4Button.Text = "Лабораторная работа №4";
            Lab4Button.UseVisualStyleBackColor = true;
            Lab4Button.Click += Lab4Button_Click;
            // 
            // DiagramFormButton
            // 
            DiagramFormButton.Location = new Point(215, 198);
            DiagramFormButton.Name = "DiagramFormButton";
            DiagramFormButton.Size = new Size(164, 75);
            DiagramFormButton.TabIndex = 19;
            DiagramFormButton.Text = "Диаграмма";
            DiagramFormButton.UseVisualStyleBackColor = true;
            DiagramFormButton.Click += DiagramFormButton_Click;
            // 
            // MainTab
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(DiagramFormButton);
            Controls.Add(Lab4Button);
            Controls.Add(Lab3Button);
            Controls.Add(Lab2Button);
            Controls.Add(Lab1Button);
            Controls.Add(label2);
            Name = "MainTab";
            Size = new Size(961, 550);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Button Lab1Button;
        private Button Lab2Button;
        private Button Lab3Button;
        private Button Lab4Button;
        private Button DiagramFormButton;
    }
}
