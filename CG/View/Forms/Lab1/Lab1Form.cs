using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG.View.Forms
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

        double[,] Vector1 = new double[MaxN, 1]; // Матрица-строка для первого вектора

        double[,] Vector2 = new double[MaxN, 1]; // Матрица-строка для второго вектора, чтобы вычислять их скалярное произведение. 

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
            for (int i = 0; i < MaxN; i++)
            {
                for (int j = 0; j < MaxN; j++)
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
            for (int i = 0; i < nstolb; i++)
            {
                for (int j = 0; j < nstr; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;

                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;

                    //MatrText[i, j].Text = (i+1).ToString() + "*" + (j+1).ToString();
                }
            }

            // 4. Корректировка размеров формы
            form2.Width = 10 + nstr * dx + 20;
            form2.Height = 10 + nstolb * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + nstr * dy + 10 + 30;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < nstolb; i++)
                    for (int j = 0; j < nstr; j++)
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
            for (int i = 0; i < nstolb; i++)
                for (int j = 0; j < nstr; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstolb + j + 1;
                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;
                }

            // 4. Корректировка размеров формы
            form2.Width = 10 + nstr * dx + 20;
            form2.Height = 10 + nstolb * dy + form2.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + nstr * dy + 10 + 30;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr2
                for (int i = 0; i < nstolb; i++)
                    for (int j = 0; j < nstr; j++)
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
                    Matr3[i, j] = +Matr1[i, j] + Matr2[i, j];
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
                    Matr3[i, j] = Matr1[i, j] - Matr2[i, j];
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

            for (int i = 0; i < nstolb; i++)
                for (int j = 0; j < nstr; j++)
                {
                    Matr3[i, j] = Matr1[i, j] * constant;

                }


            Clear_MatrText();

            // 3. Внесение данных в MatrText
            for (int i = 0; i < nstolb; i++)
                for (int j = 0; j < nstr; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                    MatrText[i, j].Visible = true;


                    //MatrText[i, j].Text = (i + 1).ToString() + "*" + (j + 1).ToString();
                    //MatrText[i,j].Text = Matr1[i, j].ToString() +"*"+ constant.ToString();
                }

            // 4. Вывод формы
            form2.ShowDialog();

        }

        private void CreateVector1Button_Click(object sender, EventArgs e)
        {


            if (VectorDimentionTextBox.Text == "") return;

            int dimention = int.Parse(VectorDimentionTextBox.Text);

            Clear_MatrText();

            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            // вектор - матрица строка 
            for (int i = 0; i < dimention; i++)
            {
                MatrText[i, 0].TabIndex = 0 * 0 + i + 1;
                // 3.2. Сделать ячейку видимой
                MatrText[i, 0].Visible = true;
            }

            // 4. Корректировка размеров формы
            form2.Width = 10 + dimention * dx + 20;
            form2.Height = 10 + 0 * dy + form2.button1.Height + 200;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + 0 * dy + 10 + 50;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < dimention; i++)
                {
                    if (MatrText[i, 0].Text != "")
                        Vector1[i, 0] = Double.Parse(MatrText[i, 0].Text);
                    else
                        Vector1[i, 0] = 0;
                }

            }


        }



        /// <summary>
        /// Считает модуль вектора. Сделал отдельной функцией,
        /// так как модуль вектора нам ещё понадобится в других функциях, а не только в кнопке
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <param name="dimention"></param>
        /// <returns></returns>
        public double CalculateVectorLength(double[,] Vector1, int dimention)
        {
            double length = 0;

            // 7. Перенос строк из формы Form2 в матрицу Matr1
            for (int i = 0; i < dimention; i++)
            {
                length += Math.Pow(Vector1[i, 0], 2);
            }

            length = Math.Sqrt(length);

            return length;

        }

        private void VectorLengthButton_Click(object sender, EventArgs e)
        {

            int dimention = int.Parse(VectorDimentionTextBox.Text);


            double length = CalculateVectorLength(Vector1, dimention);

            Clear_MatrText();
            MatrText[0, 0].Visible = true;

            MatrText[0, 0].TabIndex = 0;
            // 3.2. Перевести число в строку
            MatrText[0, 0].Text = length.ToString();



            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void CreateVector2Button_Click(object sender, EventArgs e)
        {
            if (VectorDimentionTextBox.Text == "") return;

            int dimention = int.Parse(VectorDimentionTextBox.Text);

            Clear_MatrText();

            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            // вектор - матрица строка 
            for (int i = 0; i < dimention; i++)
            {
                MatrText[i, 0].TabIndex = 0 * 0 + i + 1;
                // 3.2. Сделать ячейку видимой
                MatrText[i, 0].Visible = true;
            }

            // 4. Корректировка размеров формы
            form2.Width = 10 + dimention * dx + 20;
            form2.Height = 10 + 0 * dy + form2.button1.Height + 200;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + 0 * dy + 10 + 50;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < dimention; i++)
                {
                    if (MatrText[i, 0].Text != "")
                        Vector2[i, 0] = Double.Parse(MatrText[i, 0].Text);
                    else
                        Vector2[i, 0] = 0;
                }

                // 8. Данные в матрицу Matr1 внесены
                f1 = true;
                label3.Text = "true";

            }
        }


        /// <summary>
        /// Считает скалярное произведение - сумма произведений соответствующих координат.  
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <param name="dimention"></param>
        /// <returns></returns>
        public double CalculateScalarMultiplication(double[,] Vector1, double[,] Vector2, int dimention)
        {
            double result = 0;

            for (int i = 0; i < dimention; i++)
            {
                result += Vector1[i, 0] + Vector2[i, 0];
            }

            return result;

        }

        private void ScalarMultiplicationButton_Click(object sender, EventArgs e)
        {
            int dimention = int.Parse(VectorDimentionTextBox.Text);

            double result = CalculateScalarMultiplication(Vector1, Vector2, dimention);

            Clear_MatrText();
            MatrText[0, 0].Visible = true;

            MatrText[0, 0].TabIndex = 0;
            // 3.2. Перевести число в строку
            MatrText[0, 0].Text = result.ToString();


            // 4. Вывод формы
            form2.ShowDialog();

        }


        /// <summary>
        /// Считает модуль векторного произведения. 
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <param name="dimention"></param>
        /// <returns></returns>
        public double CalculateVectorMultiplicationModule(double[,] Vector1, double[,] Vector2, int dimention)
        {
            double result = 0;

            double length1 = CalculateVectorLength(Vector1, dimention);
            double length2 = CalculateVectorLength(Vector2, dimention);

            // Модуль векторного произведения 

            // Модуль векторного произведения считается как
            // [a,b] = |a|*|b|*Sin(alpha) = |a|*|b|*(1-Cos(alpha)^2)
            // Косинус можно найти из скалярного произведения
            // (a,b) = |a|*|b|*Cos(alpha) => Cos(alpha) = (a,b)/((a,b)|
            result = length1 * length2 * (1 - Math.Pow(CalculateScalarMultiplication(Vector1, Vector2, dimention) / (length1 * length2), 2));

            return result;
        }

        public double[,] CalculateVectorMultiplicationVector(double[,] Vector1, double[,] Vector2, int dimention)
        {

            

            double[,] result = new double[3, 1];

            result[0, 0] = Vector1[1, 0] * Vector2[2, 0] - Vector1[2, 0] * Vector2[1, 0];
            result[1, 0] = -(Vector1[0, 0] * Vector2[2, 0] - Vector1[2, 0] * Vector2[0, 0]);
            result[2, 0] = Vector1[0, 0] * Vector2[1, 0] - Vector1[1, 0] * Vector2[0, 0];
            

            return result;

        }

        private void VectorMultiplicationButton_Click(object sender, EventArgs e)
        {
            

            int dimention = int.Parse(VectorDimentionTextBox.Text);

            if (dimention != 3)
                return;

            double[,] vector = CalculateVectorMultiplicationVector(Vector1,Vector2, dimention);

            Clear_MatrText();
            for (int i = 0; i < 3; i++)
            {
                MatrText[i, 0].Visible = true;
                MatrText[i, 0].Text = vector[i, 0].ToString();
            }


            // 4. Вывод формы
            form2.ShowDialog();

        }

        /// <summary>
        /// Транспонирование матрицы. 
        /// </summary>
        /// <param name="Matrix"></param>
        /// <returns></returns>
        public double[,] MatrixTranspose(double[,] Matrix, int nstolb, int nstr)
        {

            double[,] result = new double[nstr, nstolb];
            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < nstolb; i++)
                for (int j = 0; j < nstr; j++)
                {
                    //result[j, i] = 0;
                    result[j, i] = Matrix[i, j];
                }
            return result;
        }

        private void Matrix1TransposeButton_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            //if (!(f1 == true)) return;

            //int dimention = int.Parse(VectorDimentionTextBox.Text);

            Matr3 = MatrixTranspose(Matr1, nstolb, nstr);


            Clear_MatrText();

            // 3. Внесение данных в MatrText
            for (int i = 0; i < nstr; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                    MatrText[i, j].Visible = true;
                }

            // 4. Вывод формы
            form2.ShowDialog();
        }


        /// <summary>
        /// Поскольку вектор это матрица, этот код почти такой же, как и в умножении матриц
        /// надо только проверки на размерность другие сделать и транспонировать вектор-строку. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Matrix1Vector1Multiplication_Click(object sender, EventArgs e)
        {

            if (!((f1 == true) && (VectorDimentionTextBox.Text != ""))) return;

            int dimention = int.Parse(VectorDimentionTextBox.Text);

            // проверка, можно ли умножать матрицы введённого размера. 
            if (nstr != dimention) return;

            // Транспонирование вектора-строки, так как матрицы можно умножать только на векторы столбцы
            Vector1 = MatrixTranspose(Vector1, dimention, 1);


            // Вычисление произведения
            for (int i = 0; i < nstolb; i++)
                for (int j = 0; j < 1; j++)
                {
                    //Matr3[i, j] = 0;
                    for (int k = 0; k < dimention; k++)
                        Matr3[j, i] = Matr3[j, i] + Matr1[k, i] * Vector1[j, k];
                    //Matr3[i, j] = Matr3[i, j] + Matr1[i, k] * Vector1[k, j];
                }

            Clear_MatrText();

            // 3. Внесение данных в MatrText
            for (int i = 0; i < 1; i++)
                for (int j = 0; j < nstolb; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * nstr + j + 1;
                    MatrText[i, j].Text = Matr3[i, j].ToString();

                    MatrText[i, j].Visible = true;
                }

            // 4. Вывод формы
            form2.ShowDialog();

        }

        private void Vector1ConstantMultiplicationButton_Click(object sender, EventArgs e)
        {
            int constant = int.Parse(ConstantTextBox.Text);
            int dimention = int.Parse(VectorDimentionTextBox.Text);

            for (int i = 0; i < dimention; i++)
                Vector1[i, 0] = Vector1[i, 0] * constant;


            Clear_MatrText();


            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            // вектор - матрица строка 
            for (int i = 0; i < dimention; i++)
            {
                MatrText[i, 0].TabIndex = 0 * 0 + i + 1;
                // 3.2. Сделать ячейку видимой
                MatrText[i, 0].Visible = true;
            }

            // 6. Вызов формы Form2

            // 7. Перенос строк из формы Form2 в матрицу Matr1
            for (int i = 0; i < dimention; i++)
            {
                MatrText[i, 0].TabIndex = i * nstr ;
                MatrText[i,0].Text = Vector1[i, 0].ToString();

                MatrText[i, 0].Visible = true;
               
            }
            // 4. Вывод формы
            form2.ShowDialog();

        }
    }
}
