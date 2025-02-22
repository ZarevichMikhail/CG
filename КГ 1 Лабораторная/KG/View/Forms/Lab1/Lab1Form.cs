using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG.View.Forms
{
    public partial class Lab1Form : Form
    {
        public Lab1Form()
        {
            InitializeComponent();
        }



        const int MaxN = 10; // максимально допустимая размерность матрицы

        //int n = 3; // текущая размерность матрицы

        // Переменные для строк и столбцов
        int nstr;
        int nstolb;


        TextBox[,] MatrText = null; // матрица элементов типа TextBox 

        double[,] Matr1 = new double[MaxN, MaxN]; // матрица 1 чисел с плавающей точкой

        double[,] Matr2 = new double[MaxN, MaxN]; // матрица 2 чисел с плавающей точкой

        double[,] Matr3 = new double[MaxN, MaxN]; // матрица результатов

        bool f1; // флажок, который указывает о вводе данных в матрицу Matr1

        bool f2; // флажок, который указывает о вводе данных в матрицу Matr2 

        int dx = 40, dy = 20; // ширина и высота ячейки в MatrText[,]

        CreateMatrixForm form2 = null; // экземпляр (объект) класса формы Form2



        /// <summary>
        /// Задание 1.1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelloButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Hello world.");
        }

        /// <summary>
        /// Задание 1.2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateFormButton_Click(object sender, EventArgs e)
        {
            var NewForm = new TestForm();
            //NewForm.Show();


            if (NewForm.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = "Result = OK";

            }

            else
            {
                textBox1.Text = "Result = Cancel";
            }
        }


        // Задание 2

        /// <summary>
        /// Пока не понимаю, что значит это событие. Переменные можно проинициализировать и без него. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lab1Form_Load(object sender, EventArgs e)
        {
            // І. Инициализация элементов управления и внутренних переменных
            textBox1.Text = "";

            f1 = f2 = false; // матрицы еще не заполнены
            label3.Text = "false";
            label4.Text = "false";

            // ІІ. Выделение памяти и настройка MatrText
            int i, j;

            // 1. Выделение памяти для формы Form2
            form2 = new CreateMatrixForm();

            // 2. Выделение памяти под саму матрицу
            MatrText = new TextBox[MaxN, MaxN];

            // 3. Выделение памяти для каждой ячейки матрицы и ее настройка
            for (i = 0; i < MaxN; i++)
                for (j = 0; j < MaxN; j++)
                {
                    // 3.1. Выделить память
                    MatrText[i, j] = new TextBox();

                    // 3.2. Обнулить эту ячейку
                    MatrText[i, j].Text = "0";

                    // 3.3. Установить позицию ячейки в форме Form2
                    MatrText[i, j].Location = new System.Drawing.Point(10 + i *
                    dx, 10 + j * dy);

                    // 3.4. Установить размер ячейки
                    MatrText[i, j].Size = new System.Drawing.Size(dx, dy);

                    // 3.5. Пока что спрятать ячейку
                    MatrText[i, j].Visible = false;

                    // 3.6. Добавить MatrText[i,j] в форму form2
                    form2.Controls.Add(MatrText[i, j]);

                }

        }




        private void Clear_MatrText()
        {
            // Обнуление ячеек MatrText
            for (int i = 0; i < nstr; i++)
            {
                for (int j = 0; j < nstolb; j++)
                {
                    MatrText[i, j].Text = "0";
                    MatrText[i, j].Visible = false;
                }
            }
        }

        /// <summary>
        /// Кнопка Ввод матрицы 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateMatrix1Button_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (nstrTextBox.Text == "") return;
            //n = int.Parse(nstrTextBox.Text);

            nstr = int.Parse(nstrTextBox.Text);
            nstolb = int.Parse(nstolbTextBox.Text);

            // 2. Обнуление ячейки MatrText
            Clear_MatrText();

            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            // Строки и столбцы расставлены правильно, но матрица получается неправильная. 
            for (int i = 0; i < nstr; i++)
            {
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;

                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;
                }
            }

            // 4. Корректировка размеров формы
            form2.Width = 10 + nstr * dx + 20;
            form2.Height = 10 + nstolb * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + nstr * dy + 10+30;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < nstr; i++)
                    for (int j = 0; j < nstolb; j++)
                        if (MatrText[i, j].Text != "")
                            Matr1[i, j] = Double.Parse(MatrText[i, j].Text);
                        else
                            Matr1[i, j] = 0;

                // 8. Данные в матрицу Matr1 внесены
                f1 = true;
                label3.Text = "true";

            }

        }

        private void CreateMatrix2Button_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (nstrTextBox.Text == "") return;
            //n = int.Parse(nstrTextBox.Text);

            nstr = int.Parse(nstrTextBox.Text);
            nstolb = int.Parse(nstolbTextBox.Text);

            // 2. Обнуление ячейки MatrText
            Clear_MatrText();

            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;
                }

            // 4. Корректировка размеров формы
            form2.Width = 10 + nstr * dx + 20;
            form2.Height = 10 + nstolb * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + nstr * dy + 10+30;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr2
                for (int i = 0; i < nstr; i++)
                    for (int j = 0; j < nstolb; j++)
                        if (MatrText[i, j].Text != "")
                            Matr2[i, j] = Double.Parse(MatrText[i, j].Text);
                        else
                            Matr2[i, j] = 0;

                // 8. Данные в матрицу Matr1 внесены
                f2 = true;
                label3.Text = "true";

            }

        }


        private void AdditionButton_Click(object sender, EventArgs e)
        {
            // 1.Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // 2. Вычисление сложения матриц. Результат в Matr3
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    //Matr3[i, j] = 0;
                    Matr3[i, j] = + Matr1[i, j] + Matr2[i, j];
                }
            // 3. Внесение данных в MatrText
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }
            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void SubstractionButton_Click(object sender, EventArgs e)
        {
            // 1.Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // 2. Вычисление вычитания матриц. Результат в Matr3
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    //Matr3[i, j] = 0;
                    Matr3[i, j] =  Matr1[i, j] - Matr2[i, j];
                }
            // 3. Внесение данных в MatrText
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }
            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void MultiplicationButton_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // проверка, можно ли умножать матрицы введённого размера. 
            if (nstr != nstolb) return;

            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    Matr3[j, i] = 0;
                    for (int k = 0; k < nstr; k++)
                        Matr3[j, i] = Matr3[j, i] + Matr1[k, i] * Matr2[j, k];
                }

            // 3. Внесение данных в MatrText
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }

            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //FileStream fw = null;
            //string msg;
            //byte[] msgByte = null; // байтовый массив

            //// 1. Открыть файл для записи
            //fw = new FileStream("Res_Matr.txt", FileMode.Create);

            //// 2. Запись матрицы результата в файл
            //// 2.1. Сначала записать число элементов матрицы Matr3
            //msg = n.ToString() + "\r\n";

            //// перевод строки msg в байтовый массив msgByte
            //msgByte = Encoding.Default.GetBytes(msg);

            //// запись массива msgByte в файл
            //fw.Write(msgByte, 0, msgByte.Length);

            //// 2.2. Теперь записать саму матрицу
            //msg = "";
            //for (int i = 0; i < n; i++)
            //{
            //    // формируем строку msg из элементов матрицы
            //    for (int j = 0; j < n; j++)
            //        msg = msg + Matr3[i, j].ToString() + " ";
            //    msg = msg + "\r\n";
            //    // добавить перевод строки
            //}
            //// 3. Перевод строки msg в байтовый массив msgByte
            //msgByte = Encoding.Default.GetBytes(msg);

            //// 4. запись строк матрицы в файл
            //fw.Write(msgByte, 0, msgByte.Length);

            //// 5. Закрыть файл
            //if (fw != null) fw.Close();
        }

        private void nstrTextBox_Leave(object sender, EventArgs e)
        {
            int nn;
            nn = Int16.Parse(nstrTextBox.Text);
            if (nn != nstr)
            {
                f1 = false;
                label3.Text = "false";
                label4.Text = "false";
            }
        }

        private void nstolbTextBox_Leave(object sender, EventArgs e)
        {
            int nn;
            nn = Int16.Parse(nstolbTextBox.Text);
            if (nn != nstolb)
            {
                f2 = false;
                label3.Text = "false";
                label4.Text = "false";
            }
        }

        private void Matrix1NumberMultiplicatoinButton_Click(object sender, EventArgs e)
        {

            int constant = int.Parse(ConstantTextBox.Text);

            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    Matr3[j, i] = Matr1[i, j] * constant;
                }
            // 3. Внесение данных в MatrText
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }

            // 4. Вывод формы
            form2.ShowDialog();

        }

        private void CreateVectorButton_Click(object sender, EventArgs e)
        {

            if (VectorDimentionTextBox.Text == "") return;

            Clear_MatrText();
            int dimention = int.Parse(VectorDimentionTextBox.Text);


            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            // вектор - матрица строка 
            for (int j = 0; j < dimention; j++)
            { 
                MatrText[0, j].TabIndex = 0 * 0 + j + 1;
                // 3.2. Сделать ячейку видимой
                MatrText[0, j].Visible = true;
            }

            // 4. Корректировка размеров формы
            form2.Width = 10 + dimention * dx + 20;
            form2.Height = 10 + 0 * dy + form2.button1.Height + 200;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + 0 * dy + 10+50;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int j = 0; j < dimention; j++)
                {
                    if (MatrText[0, j].Text != "")
                        Matr1[0, j] = Double.Parse(MatrText[0, j].Text);
                    else
                        Matr1[0, j] = 0;
                }

                // 8. Данные в матрицу Matr1 внесены
                f1 = true;
                label3.Text = "true";

            }


        }
    }
}
