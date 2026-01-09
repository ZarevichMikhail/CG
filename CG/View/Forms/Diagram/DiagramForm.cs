using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CG.View.Forms.Lab2.Lab2Form;

namespace CG.View.Forms.Diagram
{
    public partial class DiagramForm : Form
    {
        public DiagramForm()
        {
            InitializeComponent();
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

        }



        Bitmap myBitmap;
        int NumberOfValues;


        TextBox[,] MatrText = null;
        System.Windows.Forms.Label[,] LabelMatrix = null;

        //System.Windows.Forms.Label[,] MatrLabel = new System.Windows.Forms.Label[2, 5];

        int[,] DiagramData = new int[2, 5];

        Color CurrentLineColor = Color.Red; // текущий цвет отрезка
        Color CurrentFillColor = Color.Green; // Текущий цвет заливки


        CreateMatrixForm form2 = null;

        int dx = 40, dy = 20; // ширина и высота ячейки в MatrText[,]


        // Событие происходит при каждой загрузке данного элемента пользователем. 
        private void DiagramForm_Load(object sender, EventArgs e)
        {


            int i, j;

            // 1. Выделение памяти для формы Form2
            form2 = new CreateMatrixForm();

            // 2. Выделение памяти под саму матрицу
            MatrText = new TextBox[2, 5];

            LabelMatrix = new System.Windows.Forms.Label[2, 5];

            // 3. Выделение памяти для каждой ячейки матрицы и ее настройка
            for (i = 0; i < 2; i++)
                for (j = 0; j < 5; j++)
                {
                    LabelMatrix[i,j] = new System.Windows.Forms.Label();

                    // 3.1. Выделить память
                    MatrText[i, j] = new TextBox();

                    // 3.2. Обнулить эту ячейку
                    MatrText[i, j].Text = "0";

                    // 3.3. Установить позицию ячейки в форме Form2
                    MatrText[i, j].Location = new System.Drawing.Point(10 + j * dx, 10 + i * dy);

                    // 3.4. Установить размер ячейки
                    MatrText[i, j].Size = new System.Drawing.Size(dx, dy);

                    // 3.5. Пока что спрятать ячейку
                    MatrText[i, j].Visible = false;

                    // 3.6. Добавить MatrText[i,j] в форму form2
                    form2.Controls.Add(MatrText[i, j]);

                    this.Controls.Add(LabelMatrix[i, j]);


                }
        }

        
        // Кнопка Ввести значения
        private void EnterValuesButton_Click(object sender, EventArgs e)
        {

            int nstolb = int.Parse(NumberTextBox.Text);



            for (int j = 0; j < nstolb; j++)
            {
                // 3.1. Порядок табуляции
                MatrText[0, j].TabIndex = 1 * nstolb + j + 1;
                MatrText[0, j].Text = (j + 1).ToString();

                // 3.2. Сделать ячейку видимой
                MatrText[0, j].Visible = true;

                MatrText[1, j].TabIndex = 1 * nstolb + j + 1;
                MatrText[1, j].Visible = true;

            }



            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            // Строки и столбцы расставлены правильно, но матрица получается неправильная. 
            //for (int i = 0; i < 2; i++)
            //{
            //    for (int j = 0; j < nstolb; j++)
            //    {
            //        // 3.1. Порядок табуляции
            //        MatrText[i, j].TabIndex = i * nstolb + j + 1;

            //        // 3.2. Сделать ячейку видимой
            //        MatrText[i, j].Visible = true;


            //    }
            //}

            // 4. Корректировка размеров формы
            form2.Width = 10 + 5 * dx + 20;
            form2.Height = 10 + nstolb * dy + form2.button1.Height + 80;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 60;
            form2.button1.Width = form2.Width - 30;

            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу DiagramData
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < nstolb; j++)
                        if (MatrText[i, j].Text != "")
                        {
                            DiagramData[i, j] = int.Parse(MatrText[i, j].Text);
                            LabelMatrix[i, j].Text = MatrText[i, j].Text;
                        }
                        else
                            DiagramData[i, j] = 0;

            }

        }



        // Кнопка Создать столбчатую диаграмму
        private void CreateHistogramButton_Click(object sender, EventArgs e)
        {

        //try
        //{

            NumberOfValues = int.Parse(NumberTextBox.Text);
            int Height = pictureBox1.Height;
            int Width = pictureBox1.Width;


            // Стартовая точка 
            //int[] StartingPoint = { 40, Height - 40 };

            // Начальная точка О имеет координаты (40, Height - 40)
            // Самая верхняя - (Width - 40, 40)

            // Оси диаграммы 
            BresLineAlg(40, 40, 40, Height - 40);
            BresLineAlg(40, Height - 40, Width - 40, Height - 40);

            pictureBox1.Image = myBitmap;

            pictureBox1.Refresh();

            // Список с координатами x вертикальных линий
            int[] VerticalCoordinates = new int[NumberOfValues];

            // Расстояние, между вертикальными линиями
            // Ширина, доступная для рисования, делённое на количество элементов
            int dx = (Width - 80) / NumberOfValues;

            // Рисует вертикальные линии
            for (int i = 0; i < VerticalCoordinates.Length; i++)
            {
                // +1 нужно, чтобы не рисовалась первая вертикальная линия,
                // которая совпадает с осью координат. 
                VerticalCoordinates[i] = 40 + (i + 1) * dx;
                //BresLineAlg(VerticalCoordinates[i], 40, VerticalCoordinates[i], Height - 100);
            }

            // Ищет максимальное значение
            int max = DiagramData[0, 0];
            for (int i = 0; i < NumberOfValues; i++)
            {
                if (max < DiagramData[1, i])
                {
                    max = DiagramData[1, i];
                }
            }

            // Делит размер окна на максимальное значение, 
            // чтобы нормировать остальные значения относительно него
            int step = (Height - 80) / max;

            // Рисует диаграмму
            for (int i = 0; i < NumberOfValues; i++)
            {
                // +1 нужно, чтобы не рисовалась первая вертикальная линия,
                // которая совпадает с осью координат. 

                int h = (Height - 40) - step * DiagramData[1, i];

                BresLineAlg(VerticalCoordinates[i] - dx, h, VerticalCoordinates[i], h);

                // Вертикальные линии
                BresLineAlg(VerticalCoordinates[i] - dx, h, VerticalCoordinates[i] - dx, Height - 40);
                BresLineAlg(VerticalCoordinates[i], h, VerticalCoordinates[i], Height - 40);


                LabelMatrix[0, i].Top = (Height - 40) + 10;
                LabelMatrix[0, i].Left = VerticalCoordinates[i] - dx / 2;
                LabelMatrix[0, i].Visible = true;
                //Устанавливает элемент управления поверх других.
                LabelMatrix[0, i].BringToFront();
                LabelMatrix[0, i].Size = new Size(30, 20);





                LabelMatrix[1, i].Top = h;
                LabelMatrix[1, i].Left = 10;
                LabelMatrix[1, i].Visible = true;
                LabelMatrix[1, i].BringToFront();
                LabelMatrix[1, i].Size = new Size(39, 30);
                //LabelMatrix[1, i].TextAlign = ContentAlignment.TopRight;

                //SpanFloodFill(VerticalCoordinates[i] - dx + 1, Height - 41);
                IterativeFloodFill(VerticalCoordinates[i] - dx + 1, Height - 41);
            }

            // Список с координатами y горизонтальных линий
            int[] HorizontalCoordinates = new int[NumberOfValues];


            pictureBox1.Image = myBitmap;

            pictureBox1.Refresh();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }



        // Кнопка Создать круговую диаграмму
        private void CreatePieChartButton_Click(object sender, EventArgs e)
        {

        }





        /// <summary>
        /// Алгоритм Брезенхема для генерации отрезка
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="yn"></param>
        /// <param name="xk"></param>
        /// <param name="yk"></param>
        /// <param name="myBitmap.SetPixel"></param>
        private void BresLineAlg(int xn, int yn, int xk, int yk)
        {
            double xOutput, yOutput, dx, dy, E;

            // Переменная для замены координат
            int temp;

            // Приращения x и y
            dx = xk - xn;
            dy = yk - yn;


            // Условие для 3 четверти, dx и dy оба отрицательные
            // Менят начальные и кончные координаты, и инвертирует их приращение
            // т.о. отрезок будет рисоваться не из x1,y1 в x2,y2, а наоборот
            if (dx <= 0 && dy <= 0)
            {

                temp = xn;
                xn = xk;
                xk = temp;

                temp = yn;
                yn = yk;
                yk = temp;

                dx = -dx;
                dy = -dy;
            }

            // Условие для 2 четверти, dx<0, dy>0.
            // т.к. уже есть функция для 4 четверти, где dx>0, dy<0, тут надо поменять координаты.
            // Менят начальные и кончные координаты, и инвертирует их приращение
            if (dx <= 0 && dy >= 0)
            {
                temp = xn;
                xn = xk;
                xk = temp;

                temp = yn;
                yn = yk;
                yk = temp;

                dx = -dx;
                dy = -dy;
            }


            xOutput = xn;
            yOutput = yn;

            myBitmap.SetPixel((int)xOutput, (int)yOutput, CurrentLineColor);
            // 2 варианта: 1 и 4 четверть. 3 и 4 получаются, соответственно, отражениями координат


            // 1 четверть
            if (dx >= dy && dy >= 0)
            {
                E = 2 * dy - dx;
                for (double i = dx; (i - 1) >= 0; i--)
                {
                    if (E >= 0)
                    {
                        xOutput = xOutput + 1;
                        yOutput = yOutput + 1;
                        E = E + 2 * (dy - dx);
                    }
                    else
                    {
                        xOutput = xOutput + 1;
                        E = E + 2 * dy;
                    }
                    myBitmap.SetPixel((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */

                }
            }
            else if (dy >= dx && dx >= 0)
            {
                E = 2 * dx - dy;
                for (double i = dy; (i - 1) >= 0; i--)
                {
                    if (E >= 0)
                    {
                        xOutput = xOutput + 1;
                        yOutput = yOutput + 1;
                        //E = E + 2 * dx;
                        E = E + 2 * (dx - dy);
                    }
                    else
                    {
                        yOutput = yOutput + 1;
                        //E = E + 2 * (dx - dy);
                        E = E + 2 * dx;
                    }
                    myBitmap.SetPixel((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */

                }
            }

            // 4 четверть 
            else if (Math.Abs(dx) >= Math.Abs(dy) && dy <= 0)
            {

                E = 2 * Math.Abs(dy) - Math.Abs(dx);
                for (double i = dx; (i - 1) >= 0; i--)
                {
                    if (E >= 0)
                    {
                        xOutput = xOutput + 1;
                        yOutput = yOutput - 1;
                        E = E + 2 * (Math.Abs(dy) - Math.Abs(dx));
                    }
                    else
                    {
                        xOutput = xOutput + 1;
                        E = E + 2 * Math.Abs(dy);
                    }
                    myBitmap.SetPixel((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */
                }
            }
            else if (Math.Abs(dy) >= Math.Abs(dx) && dy <= 0)
            {

                E = 2 * Math.Abs(dx) - Math.Abs(dy);
                for (double i = Math.Abs(dy); (i - 1) >= 0; i--)
                {
                    if (E >= 0)
                    {
                        xOutput = xOutput + 1;
                        yOutput = yOutput - 1;
                        E = E + 2 * (Math.Abs(dx) - Math.Abs(dy));
                    }
                    else
                    {
                        yOutput = yOutput - 1;
                        E = E + 2 * Math.Abs(dx);
                    }
                    myBitmap.SetPixel((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */
                }
            }

        }



        /// <summary>
        /// Построчная заливка
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        private void SpanFloodFill(int x1, int y1)
        {
            //Color CurrentLineColor = Color.Blue;

            // Переменные, чтобы указывать, какой пиксель заносить в стек верхний или нижний
            // Изначально нужно занести оба
            // Нужны, чтобы решить проблему неправильной заливки
            /*Проблема:
            Причина проблемы, которую я долго не мог решить заключалась в следующем:
            После каждой новой закрашенной строки в стек заносился и верхний и нижний пиксели
            Т.е. после первой строки занеслись верхний и нижний. Достал нижний, строка закрасилась
            затем занеслись верхний(предыдущий первый) и нижний пиксели.
            Таким образом в стеке были пиксели, которые находились на закрашенных строках. 
            После достижения нижней границы начали доставаться верхние пиксели, но, так как они находились уже на закрашенных строках,
            Алгортим записывал, что верхняя граница уже достигнута. Доставал все верхние пиксели, дошёл до самого первого верхнего.
            Закрасил его, но вверх больше не шёл. 

            Это объясняет то, почему алгоритм работал, если я закрашивал либо только верх, либо только низ,
            В этом случае в стеке не было лишних пикселей. И то почему, если я менял местами порядок добавления пикселей
            (Нижний добавлялся первым, затем верхний. Т.о. из стека сначала доставались верхние пиксели), заливка шла вверх, а вниз - нет.
            Почему заливка иногда протекала так и не понял, но, видимо, проблема тоже была где - то тут.
            */
            /* Решение проблемы:
            Сначала нужно занести оба пикселя - верхний и нижний.
            Первым достанется нижний. Нужно дойти до самого низа, но заносить при этом только нижние пиксели, а не верхние.
            Когда дошёл до нижней границы, в стеке останется 1 пиксель - верхний. Нужно достать его и идти вверх,
            занося только верхние пиксели.
            Реализацию см ниже. строка 1359
             */
            bool CanGoUp = true;
            bool CanGoDown = true;


            // стек из массивов. в каждом массиве по 2 элемента - координаты пикселя.
            Stack<int[]> Pixels = new Stack<int[]>();

            // координаты первого пикселя
            int[] CurrentPixel = { x1, y1 };

            // Добавляет первый пиксель в стек
            Pixels.Push(CurrentPixel);

            // Достигнута или нет верхняя граница
            bool UpperBoundReached = false;
            bool LowerBoundReached = false;

            // Пока стек не пуст
            while (Pixels.Count != 0)
            {
                // Получает пиксель из стека
                CurrentPixel = Pixels.Pop();

                // координаты x и y текущего пикселя
                // занёс их в отдельные переменные, чтобы они занимали меньше текста
                int xc = CurrentPixel[0];
                int yc = CurrentPixel[1];

                // левый и правый пиксели, которые надо закрашивать 
                int xLeft = xc;
                int xRight = xc;


                // если текущий пиксель не граничный и не закрашен, закрашивает всё слева и справа от него. 
                if ((myBitmap.GetPixel(xc, yc).ToArgb() != CurrentLineColor.ToArgb())
                       && (myBitmap.GetPixel(xc, yc).ToArgb() != CurrentFillColor.ToArgb()))
                {

                    myBitmap.SetPixel(xc, yc, CurrentFillColor);
                    pictureBox1.Image = myBitmap;

                    pictureBox1.Refresh();
                    while ((myBitmap.GetPixel(xLeft - 1, yc).ToArgb() != CurrentLineColor.ToArgb())
                        && (myBitmap.GetPixel(xLeft - 1, yc).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        xLeft--;
                        myBitmap.SetPixel(xLeft, yc, CurrentFillColor);
                        pictureBox1.Image = myBitmap;

                        pictureBox1.Refresh();
                    }


                    while ((myBitmap.GetPixel(xRight + 1, yc).ToArgb() != CurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xRight + 1, yc).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        xRight++;
                        myBitmap.SetPixel(xRight, yc, CurrentFillColor);

                        pictureBox1.Image = myBitmap;

                        pictureBox1.Refresh();
                    }
                    pictureBox1.Image = myBitmap;

                    pictureBox1.Refresh();
                }



                /*при поиске верхнего правого незакрашенного пикселя могут быть три ситуации. 
                1) верхний пиксель граничный.
                Нам не надо проверять, будет ли следующий пиксель граничным, так как неизвестно, насколько длинная граница
                все граничные пиксели надо пропустить.

                Поиск крайнего правого граничного пикселя сделал в другом цикле

                2) верхний пиксель самый верхний в фигуре. 
                надо дополнить условие из 1
                надо дойти до самого крайнего граничного пикселя в верхней строке, запомнить координату x
                и посмотреть, является ли пиксель на текущей строке с этим же или следующим x граничным. 
                если да - это будет означать, что граница идёт вниз, значит сверху больше нет пикселей, принадлежащих фигуре
                если нет - внизу границы нет, значит там фигура, выход из цикла

                3) верхний пиксель не граничный, т.е.принадлежит фигуре
                проверять, граничный ли пиксель, больше не надо.
                Нужно выйти из цикла и искать следующий граничный пиксель. 
                */

                // координата x проверяемого пикселя
                int i1 = xLeft;


                // Если верхняя граница не достигнута и можно идти вверх.
                // ищет, где заканчивается верхняя граница
                if (UpperBoundReached == false && CanGoUp == true)
                {
                    while (true)
                    {
                        // Проверка условия 1
                        // если пиксель граничный, пропускает его
                        if (myBitmap.GetPixel(i1 + 1, yc + 1).ToArgb() == CurrentLineColor.ToArgb())
                        {
                            i1++;
                            continue;
                        }

                        // проверка 2
                        // Проверяет пиксель на текущей строке и следующий,
                        // если какой-либо из них цвета границы, значит наш пиксель уже за фигурой.
                        // и переходить на него не надо. достигли верхней границы
                        else if (myBitmap.GetPixel(i1, yc).ToArgb() == CurrentLineColor.ToArgb() ||
                           myBitmap.GetPixel(i1 + 1, yc).ToArgb() == CurrentLineColor.ToArgb())
                        {
                            // Дошёл до верхней границы, можно идти вниз.
                            UpperBoundReached = true;
                            CanGoDown = true;
                            //int[] a = { i1 - 2, yc + 1 };

                            // Добавляет верхний пиксель в стек
                            //Pixels.Push(a);
                            //upperpixelsaddedcount++;
                            break;
                        }
                        else
                        {

                            break;
                        }

                    }
                }



                // если верхняя граница не достигнута и можно идти вверх, ищем пиксель. 

                int it1 = i1;
                if (UpperBoundReached == false && CanGoUp == true)
                {
                    while (true)
                    {
                        // Если пиксель граничный заносит в стек и выходит из цикла
                        if (myBitmap.GetPixel(it1 + 1, yc + 1).ToArgb() == CurrentLineColor.ToArgb())
                        {
                            int[] b = { it1, yc + 1 }; ;
                            // Добавляет верхний пиксель в стек
                            Pixels.Push(b);
                            //upperpixelsaddedcount++;

                            break;
                        }
                        else
                        {
                            it1++;
                        }
                    }
                }


                // то же самое для нижнего пикселя. 

                // если нижняя граница не достигнута и можно идти вниз, ищем пиксель. 
                int i2 = xLeft;

                if (LowerBoundReached == false && CanGoDown == true)
                {
                    while (true)
                    {
                        // Проверка 1
                        if (myBitmap.GetPixel(i2 + 1, yc - 1).ToArgb() == CurrentLineColor.ToArgb())
                        {
                            i2++;
                            continue;
                        }

                        // Проверка 2
                        else if (myBitmap.GetPixel(i2, yc).ToArgb() == CurrentLineColor.ToArgb() ||
                               myBitmap.GetPixel(i2 + 1, yc).ToArgb() == CurrentLineColor.ToArgb())
                        {
                            // Нижняя граница достигнута. Дальше можно заносить верхние пиксели. Вниз больше идти нельзя. 
                            LowerBoundReached = true;
                            CanGoDown = false;
                            CanGoUp = true;
                            break;
                        }
                        else
                        {
                            // Если нижнняя граница не достигнута, запрещает заносить верхние пиксели.
                            CanGoUp = false;
                            break;
                        }

                    }
                }


                int it2 = i2;

                if (LowerBoundReached == false && CanGoDown == true)
                {
                    while (true)
                    {
                        //Если пиксель граничный или закрашен заносит в стек и выходит из цикла
                        if (myBitmap.GetPixel(it2 + 1, yc - 1).ToArgb() == CurrentLineColor.ToArgb())
                        {
                            //координаты нижнего левого пикселя
                            int[] a = { it2, yc - 1 };

                            //Добавляет нижний пиксель в стек
                            Pixels.Push(a);

                            break;
                        }
                        else
                        {
                            it2++;
                        }
                    }
                }

                pictureBox1.Image = myBitmap;

                pictureBox1.Refresh();
            }

        }

        /// <summary>
        /// Итеративная заливка.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        private void IterativeFloodFill(int x1, int y1)
        {

            // теория про массивы в с#
            // https://metanit.com/sharp/tutorial/2.4.php
            // теория про стеки в c#
            // https://metanit.com/sharp/tutorial/4.8.php

            // Теория про стек. Кормен Алгоритмы 2 издание, с. 260
            // Первым из стека(stack) удаляется элемент, который был помещен туда последним:
            // в стеке реализуется стратегия “последним вошел — первым вышел” (last -in, first -out — LIFO).

            // ru.wikipedia.org/wiki/Стек

            // сд, в которую можно помещать объекты, но достать можно только последний элемент, который после взятия удаляется их стека



            // стек из массивов. в каждом массиве по 2 элемента - координаты пикселя.
            Stack<int[]> Pixels = new Stack<int[]>();

            // координаты первого пикселя
            int[] CurrentPixel = { x1, y1 };

            // Добавляет первый пиксель в стек
            Pixels.Push(CurrentPixel);

            // Пока стек не пуст
            while (Pixels.Count != 0)
            {
                // Получает пиксель из стека
                CurrentPixel = Pixels.Pop();

                // координаты x и y текущего пикселя
                // занёс их в отдельные переменные, чтобы они занимали меньше текста
                int xc = CurrentPixel[0];
                int yc = CurrentPixel[1];

                Color oldPixelColor = myBitmap.GetPixel(xc, yc);

                // проверяет его цвет
                if ((oldPixelColor.ToArgb() != CurrentLineColor.ToArgb())
                     && (oldPixelColor.ToArgb() != CurrentFillColor.ToArgb()))
                {

                    myBitmap.SetPixel(xc, yc, CurrentFillColor);
                    //pictureBox1.Image = myBitmap;

                    //pictureBox1.Refresh();

                    // Для всех четырех соседних пикселов проверить, является ли он граничным или уже перекрашен

                    if ((myBitmap.GetPixel(xc, yc + 1).ToArgb() != CurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc, yc + 1).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        // это не работает, значения будут передаваться по ссылке,
                        // поэтому у всех элементов стека координаты будут одни и те же
                        //CurrentPixel = [xc, yc + 1];
                        //CurrentPixel[0] = xc;
                        //CurrentPixel[1] = yc+1;

                        // надо выделить память под новый массив, и заносить его. 
                        int[] a = { xc, yc + 1 };
                        Pixels.Push(a);
                    }

                    if ((myBitmap.GetPixel(xc + 1, yc).ToArgb() != CurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc + 1, yc).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        int[] a = { xc + 1, yc };
                        Pixels.Push(a);
                    }

                    if ((myBitmap.GetPixel(xc, yc - 1).ToArgb() != CurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc, yc - 1).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        int[] a = { xc, yc - 1 };
                        Pixels.Push(a);
                    }

                    if ((myBitmap.GetPixel(xc - 1, yc).ToArgb() != CurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc - 1, yc).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        int[] a = { xc - 1, yc };
                        Pixels.Push(a);
                    }


                    // решил сделать более красиво, через двумерный массив
                    // но не разобрался как итерироваться по его подмассивам
                    // foreach идёт не по ним, а по элементам
                    //  соседние пиксели
                    //int[,] NeighborPixels =  { {x1,yc+1},{xc+1,yc}, {xc,yc-1}, { xc, yc-1 } };

                    //foreach (int pixel in NeighborPixels)
                    //{
                    //    if ((myBitmap.GetPixel(pixel[0], pixel[1]).ToArgb() != СurrentLineColor.ToArgb())
                    //        && (myBitmap.GetPixel(xc, yc + 1).ToArgb() != CurrentFillCllor.ToArgb()))
                    //    {
                    //        CurrentPixel = [xc, yc + 1];
                    //        Pixels.Push(CurrentPixel);
                    //    }

                    //}

                }

            }

        }


        // Кнопка Сбросить значения. 
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Все пиксели компонента PictureBox перекрасятся в светлый цвет

            //Bitmap myBitmap = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            //Задаем цвет пикселя по схеме RGB (от 0 до 255 для каждого цвета)
            Color newPixelColor = Color.White;
            for (int x = 0; x < myBitmap.Width; x++)
            {
                for (int y = 0; y < myBitmap.Height; y++)
                {
                    myBitmap.SetPixel(x, y, newPixelColor);
                }
            }
            pictureBox1.Image = myBitmap;


            // Обнуление ячеек MatrText
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    MatrText[i, j].Text = "0";
                    MatrText[i, j].Visible = false;

                    DiagramData[i, j] = 0;
                }
            }


        }

        
    }
}
