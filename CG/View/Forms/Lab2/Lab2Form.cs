using System.Collections.Generic;
using System.DirectoryServices;
using CG;
using CG.View.Forms;


namespace CG.View.Forms.Lab2
{
    public partial class Lab2Form : Form
    {
        Bitmap myBitmap; // объект Bitmap для вывода отрезка
        public Lab2Form()
        {
            InitializeComponent();

            // Создаёт битмап
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            AlgListBox.Items.Add("Отрезок по обычному ЦДА");
            AlgListBox.Items.Add("Отрезок по несимметричному ЦДА");
            AlgListBox.Items.Add("Отрезок по алгоритму Брезенхема");
            AlgListBox.Items.Add("Окружность по алгоритму Брезенхема");
            AlgListBox.Items.Add("Рекурсивная заливка");
            AlgListBox.Items.Add("Итеративная заливка");
            AlgListBox.Items.Add("Построчная заливка");
            AlgListBox.Items.Add("Режим создания многоугольника");
            AlgListBox.Items.Add("Режим создания прямоугольника");
            AlgListBox.Items.Add("Создать отрезки");

            // Задаёт стиль линии по умолчанию. 
            // Тонкая линия 
            Style = myBitmap.SetPixel;

        }

        /// <summary>
        /// Переменные, содержащие координаты и приращения
        /// </summary>
        public int xn, yn, xk, yk;




        Color CurrentLineColor = Color.Red; // текущий цвет отрезка
        Color CurrentFillColor = Color.Green; // Текущий цвет заливки


        /// <summary>
        /// Матрица оператора поворота на 90 градусов против часовой стрелки.
        /// </summary>
        double[,] Operator = { { 0, -1 }, { 1, 0 } };


        /// <summary>
        /// Пиксели многоугольника
        /// </summary>
        List<int[]> PolygonPixels = new List<int[]>();


        // Не понял, как трёхмерную коллекцию задать более красиво
        /// <summary>
        /// Пиксели отрезков. 
        /// Каждый элемент коллекции - коллекция из двух коллекций - координаты начала и координаты конца отрезка
        /// </summary>
        List<List<List<double>>> LinePixels = new List<List<List<double>>>();

        /// <summary>
        /// Делегат для функции стиля линии. (Толстая или тонкая)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="PixelColor"></param>
        public delegate void DrawStyle(int xc, int yc, Color PixelColor);



        /// <summary>
        /// Стиль линии. (Толстая или тонкая)
        /// </summary>
        DrawStyle Style;



        /// <summary>
        /// Умножение матриц. 
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public double[,] MatrixMultiply(double[,] m1, double[,] m2)
        {
            //double[,] result = new double[2, 1];
            double[,] result = new double[m1.GetLength(0), m2.GetLength(1)];

            for (int i = 0; i < m1.GetLength(0); i++)
            {
                for (int j = 0; j < m2.GetLength(1); j++)
                {
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        result[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Выполняется в момент нажатия кнопки мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            xn = e.X;
            yn = e.Y;

        }

        /// <summary>
        /// Выполняется в момент отпускания кнопки мыши. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            xk = e.X;
            yk = e.Y;

            //textBox1.Text = "x: " + xk.ToString() + " y: " + yk.ToString();

            // Выбор алгоритма рисования
            // 0 Отрезок по обычному ЦДА
            // 1 Отрезок по несимметричному цда
            // 2 Отрезок по Брезенхему
            // 3 Окружность по Брезенхему
            // 4 Рекурсивная заливка
            // 5 Итеративная заливка
            // 6 Построчная заливка (работает неправильно)
            // 7 Режим создания многоугольника
            // 8 Создание прямоугльника
            // 9 Создание списка с отрезками
            switch (AlgListBox.SelectedIndex)
            {
                case 0:

                    CDA(xn, yn, xk, yk, Style);

                    break;

                case 1:

                    AsimDDA(xn, yn, xk, yk, Style);

                    break;

                case 2:

                    BresLineAlg(xn, yn, xk, yk, Style);
                    break;

                case 3:

                    BresCircleAlg(xn, yn, Style);
                    break;

                case 4:

                    RecursiveFloodFill(xn, yn);
                    break;

                case 5:

                    IterativeFloodFill(xn, yn);
                    break;

                case 6:

                    SpanFloodFill(xn, yn);
                    break;

                case 7:

                    // Если выбран режим рисования многоугольника, по каждому клику добавляет пиксели
                    // Рисует по кнопке "Создать многоугольник".

                    //int[] a = { xn, yn };
                    //PolygonPixels.Add(a);

                    PolygonPixels.Add([e.X, e.Y]);
                    Style(e.X, e.Y, CurrentLineColor);

                    DrawRectangle(xn, yn, CurrentLineColor);

                    break;

                case 8:

                    PolygonPixels.Add([e.X, e.Y]);
                    Style(e.X, e.Y, CurrentLineColor);
                    DrawRectangle(xn, yn, CurrentLineColor);

                    break;

                case 9:

                    //AsimDDA(xn, yn, xk, yn, Style);
                    //AsimDDA(xk, yn, xk, yk, Style);
                    //AsimDDA(xk, yk, xn, yk, Style);
                    //AsimDDA(xn, yk, xn, yn, Style);

                    //BresLineAlg(xn, yn, xk, yn, Style);
                    //BresLineAlg(xk, yn, xk, yk, Style);
                    //BresLineAlg(xk, yk, xn, yk, Style);
                    //BresLineAlg(xn, yk, xn, yn, Style);

                    BresLineAlg(xn, yn, xk, yk, Style);
                    //List<List<int>> b = new List<List<int>>();
                    //b = [[xn, yn], [xk, yk]];
                    //LinePixels.Add(b);

                    LinePixels.Add([[xn, yn], [xk, yk]]);


                    break;


            }

            textBox1.Text = upperpixelsaddedcount.ToString();

            pictureBox1.Image = myBitmap;

            pictureBox1.Refresh();

        }


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
            LinePixels.Clear();
            PolygonPixels.Clear();


            // Метод обновления компонента PictureBox
            //pictureBox1.Image = null;
            //myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //Style = myBitmap.SetPixel;


        }

        /// <summary>
        /// Выбор цвета линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineColorButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                CurrentLineColor = colorDialog1.Color;
            }

        }

        /// <summary>
        /// Выбор цвета заливки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillColorButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                CurrentFillColor = colorDialog1.Color;
            }

        }

        /// <summary>
        /// Цифровой дифференциальный анализатор. 
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yEnd"></param>
        /// /// <param name="Style">Стиль линии</param>
        private void CDA(int xStart, int yStart, int xEnd, int yEnd, DrawStyle Style)
        {
            int index, numberNodes;
            double xOutput, yOutput, dx, dy;

            xn = xStart;
            yn = yStart;
            xk = xEnd;
            yk = yEnd;

            dx = xk - xn;
            dy = yk - yn;

            numberNodes = 1000;

            xOutput = xn;
            yOutput = yn;

            for (index = 1; index <= numberNodes; index++)
            {

                // Рисует линию выбранного стиля. 
                Style((int)xOutput, (int)yOutput, CurrentLineColor);

                //myBitmap.SetPixel((int)xOutput, (int)yOutput, Color.Red);

                xOutput = xOutput + dx / numberNodes;
                yOutput = yOutput + dy / numberNodes;
            }

        }


        /// <summary>
        /// Несимметричный цда
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yEnd"></param>
        /// <param name="Style"></param>
        private void AsimDDA(int xStart, int yStart, int xEnd, int yEnd, DrawStyle Style)
        {

            // см алгоритм Брезенхема, тут я сделал также. 

            double xOutput, yOutput, dx, dy;

            dx = xk - xn;
            dy = yk - yn;

            int temp;

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

            Style((int)xOutput, (int)yOutput, CurrentLineColor);


            // 1 Четверть
            if (dx >= dy && dy > 0)
            {
                while (xOutput < xk)
                {
                    xOutput++;
                    yOutput = yOutput + dy / dx;
                    Style((int)xOutput, (int)yOutput, CurrentLineColor);

                }
            }
            else if (dy >= dx && dx > 0)
            {
                while (yOutput < yk)
                {
                    yOutput++;
                    xOutput = xOutput + dx / dy;
                    Style((int)xOutput, (int)yOutput, CurrentLineColor);
                }
            }

            // 3 четверть. 
            else if (Math.Abs(dx) >= Math.Abs(dy) && dy < 0)
            {
                while (xOutput < xk)
                {
                    xOutput++;
                    yOutput = yOutput - Math.Abs(dy) / dx;
                    Style((int)xOutput, (int)yOutput, CurrentLineColor);
                }
            }
            else if (Math.Abs(dy) >= Math.Abs(dx) && dy < 0)
            {
                while (yOutput > yk)
                {
                    yOutput--;
                    xOutput = xOutput + dx / Math.Abs(dy);
                    Style((int)xOutput, (int)yOutput, CurrentLineColor);
                }
            }

        }

        /// <summary>
        /// Алгоритм Брезенхема для генерации отрезка
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="yn"></param>
        /// <param name="xk"></param>
        /// <param name="yk"></param>
        /// <param name="Style"></param>
        private void BresLineAlg(int xn, int yn, int xk, int yk, DrawStyle Style)
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

            Style((int)xOutput, (int)yOutput, CurrentLineColor);
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
                    Style((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */

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
                    Style((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */

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
                    Style((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */
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
                    Style((int)xOutput, (int)yOutput, CurrentLineColor); /* Очередная точка вектора */
                }
            }

        }


        /// <summary>
        /// Алгортим Брезенхема для геренрации окружности. 
        /// </summary>
        /// <param name="xn">x центра окружности</param>
        /// <param name="yn">y центра окружности</param>
        /// <param name="Style"></param>
        private void BresCircleAlg(double xc, double yc, DrawStyle Style)
        {
            // Суть алгортима
            // Алгоритм Брезенхема для окружности рисует её только в 1 четверти
            // Ведём новую систему координат с центром в точке xc,yc, чтобы определять точки относительно неё
            // Будем находить точки, которые надо нарисовать, переводить их в старую ск, и рисовать в ней
            // Чтобы нарисовать остальные - нужно повернуть точку на 90 градусов с помощью оператора поворота
            // Но он поворачивает только относительно начала координат
            // поврорачиваем относительно новой - переводим в старую и рисуем


            // Теория по преобразованию координат, слайд 5
            // https://moodle.math.tusur.ru/pluginfile.php/1187/mod_resource/content/1/Preobr_dsk.pdf
            // Теория по линейным операторам, слайд 11
            // https://moodle.math.tusur.ru/pluginfile.php/4097/mod_resource/content/1/Linejnyj%20operator.pdf


            int radius = int.Parse(RadiusTextBox.Text);


            // матрица-солбец с координатами точки, которую надо повернуть. 
            double[,] Coord = new double[2, 1];


            // координаты первой точки, которую надо нарисовать, в старой системе координат.
            // output потому, что рисовать будем именно их
            double xOutput = xc;
            double yOutput = yc + radius;



            // Координаты первой точки в новой ск 
            double xn = xOutput - xc; // = 0
            double yn = yOutput - yc; // = radius


            // рисуем первую точку
            //Style((int)xOutput, (int)yOutput, СurrentLineColor); 


            // эти коэффициенты также считаем относительно новой системы координат 
            double Dd = Math.Pow(xn + 1, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);
            double Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
            double Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);


            // пока y>0 т.е. в первой четверти
            while (yn >= 0)
            {
                if (Dd < 0)
                {
                    double di = Math.Abs(Dg) - Math.Abs(Dd);

                    if (di <= 0)
                    {
                        // Горизонтальный пиксель

                        // Меняем точку в новой системе кординат
                        xn++;

                        // Переводим из новой в старую
                        xOutput = xn + xc;
                        //yOutput = yOutput;

                        // рисуем в старой
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);


                        // Заношу координаты в новой ск в матрицу
                        Coord[0, 0] = xn;
                        Coord[1, 0] = yn;

                        // поворот на 90 градусов
                        Coord = MatrixMultiply(Operator, Coord);

                        // Перевожу координаты из новой ск в старую
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        ////Рисую координаты в старой ск
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        //// повторяю это ещё 2 раза
                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);


                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        // поворачиваю ещё раз, чтобы восстановить координаты точек 
                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        Dd = Dd + 2 * xn + 1;
                        Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
                        Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);
                    }
                    else
                    {
                        // Диагональный пиксель 

                        xn++;
                        yn--;

                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        // Заношу координаты в новой ск в матрицу
                        Coord[0, 0] = xn;
                        Coord[1, 0] = yn;

                        // поворот на 90 градусов
                        Coord = MatrixMultiply(Operator, Coord);


                        // Перевожу координаты из новой ск в старую
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        // Рисую координаты в старой ск
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        // повторяю это ещё 2 раза
                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);


                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        Dd = Dd + 2 * xn - 2 * yn + 2;
                        Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
                        Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);


                    }

                }
                else if (Dd > 0)
                {
                    double Si = Math.Abs(Dg) - Math.Abs(Dv);

                    if (Si <= 0)
                    {
                        // Диагональный пиксель 
                        xn++;
                        yn--;

                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        // Заношу координаты в новой ск в матрицу
                        Coord[0, 0] = xn;
                        Coord[1, 0] = yn;

                        // поворот на 90 градусов
                        Coord = MatrixMultiply(Operator, Coord);

                        // Перевожу координаты из новой ск в старую
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        // Рисую координаты в старой ск
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        // повторяю это ещё 2 раза
                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);


                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        Dd = Dd + 2 * xn - 2 * yn + 2;
                        Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
                        Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);


                    }
                    else
                    {
                        // Вертикальный пиксель

                        yn--;

                        yOutput = yn + yc;

                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        // Заношу координаты в новой ск в матрицу
                        Coord[0, 0] = xn;
                        Coord[1, 0] = yn;

                        // поворот на 90 градусов
                        Coord = MatrixMultiply(Operator, Coord);

                        // Перевожу координаты из новой ск в старую
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        // Рисую координаты в старой ск
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        // повторяю это ещё 2 раза
                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);


                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;
                        Style((int)xOutput, (int)yOutput, CurrentLineColor);

                        Coord = MatrixMultiply(Operator, Coord);
                        xn = Coord[0, 0];
                        yn = Coord[1, 0];
                        xOutput = xn + xc;
                        yOutput = yn + yc;

                        Dd = Dd - 2 * yn + 1;
                        Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
                        Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);

                    }
                }
                else
                {
                    // Диагональный пиксель 

                    xn++;
                    yn--;

                    xOutput = xn + xc;
                    yOutput = yn + yc;

                    Style((int)xOutput, (int)yOutput, CurrentLineColor);


                    // Заношу координаты в новой ск в матрицу
                    Coord[0, 0] = xn;
                    Coord[1, 0] = yn;

                    // поворот на 90 градусов
                    Coord = MatrixMultiply(Operator, Coord);

                    // Перевожу координаты из новой ск в старую
                    xn = Coord[0, 0];
                    yn = Coord[1, 0];
                    xOutput = xn + xc;
                    yOutput = yn + yc;

                    // Рисую координаты в старой ск
                    Style((int)xOutput, (int)yOutput, CurrentLineColor);

                    // повторяю это ещё 2 раза
                    Coord = MatrixMultiply(Operator, Coord);
                    xn = Coord[0, 0];
                    yn = Coord[1, 0];
                    xOutput = xn + xc;
                    yOutput = yn + yc;
                    Style((int)xOutput, (int)yOutput, CurrentLineColor);


                    Coord = MatrixMultiply(Operator, Coord);
                    xn = Coord[0, 0];
                    yn = Coord[1, 0];
                    xOutput = xn + xc;
                    yOutput = yn + yc;
                    Style((int)xOutput, (int)yOutput, CurrentLineColor);

                    Coord = MatrixMultiply(Operator, Coord);
                    xn = Coord[0, 0];
                    yn = Coord[1, 0];
                    xOutput = xn + xc;
                    yOutput = yn + yc;

                    Dd = Dd + 2 * xn - 2 * yn + 2;
                    Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
                    Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);
                }
            }



        }

        /// <summary>
        /// Рисует квадрат. Нужно для рисования толстой линии.
        /// Вместо одной точки, рисует квадрат вокруг неё. 
        /// </summary>
        /// <param name="x">Текущий x</param>
        /// <param name="y">Текущий y</param>
        private void DrawRectangle(int x, int y, Color PixelColor)
        {

            int height = 5;

            int xmax = x + height - 1;
            int ymax = y + height - 1;
            int xmin = x - height - 1;
            int ymin = y - height - 1;


            // Рисую линии квадрата
            for (int i = 1; i <= height * 2; i++)
            {
                // Верхняя линия
                myBitmap.SetPixel((int)xmin + i, (int)ymax, PixelColor);

                // Правая линия
                myBitmap.SetPixel((int)xmax, (int)ymax - i, PixelColor);

                // Нижняя линия
                myBitmap.SetPixel((int)xmin + i, (int)ymin, PixelColor);

                // Левая линия
                myBitmap.SetPixel((int)xmin, (int)ymin + i, PixelColor);
            }


        }


        /// <summary>
        /// Рисует толстую линию
        /// </summary>
        /// <param name="xStart">Начальный x</param>
        /// <param name="yStart">Начальный y</param>
        /// <param name="xEnd">Конечный x</param>
        /// <param name="yEnd">Конечный y</param>
        private void BoldLineDraw(int xStart, int yStart, int xEnd, int yEnd)
        {
            int index, numberNodes;
            double xOutput, yOutput, dx, dy;

            xn = xStart;
            yn = yStart;
            xk = xEnd;
            yk = yEnd;

            dx = xk - xn;
            dy = yk - yn;

            numberNodes = 200;
            xOutput = xn;
            yOutput = yn;


            for (index = 1; index <= numberNodes; index++)
            {
                // Сделал через рисование квадратов, вместо рисования пикселей
                DrawRectangle((int)xOutput, (int)yOutput, CurrentLineColor);

                xOutput = xOutput + dx / numberNodes;
                yOutput = yOutput + dy / numberNodes;

            }
        }


        /// <summary>
        /// Заливка с затравкой (рекурсивная)
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        private void RecursiveFloodFill(int x1, int y1)
        {

            Color oldPixelColor = myBitmap.GetPixel(x1, y1);

            // Тут он сравнивает цвет пикселя с текущим цветом линии

            // сравнение цветов происходит в формате RGB
            // для этого используем метод ToArgb объекта Color
            if ((oldPixelColor.ToArgb() != CurrentLineColor.ToArgb())
            && (oldPixelColor.ToArgb() != CurrentFillColor.ToArgb()))
            {

                //перекрашиваем пиксель
                myBitmap.SetPixel(x1, y1, CurrentFillColor);

                //pictureBox1.Image = myBitmap;

                //pictureBox1.Refresh();

                //вызываем метод для 4-х соседних пикселей
                RecursiveFloodFill(x1 + 1, y1);
                RecursiveFloodFill(x1 - 1, y1);
                RecursiveFloodFill(x1, y1 + 1);
                RecursiveFloodFill(x1, y1 - 1);
            }
            else
            {
                //выходим из метода
                return;
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


        int upperpixelsaddedcount;

        /// <summary>
        /// Построчная заливка
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        private void SpanFloodFill(int x1, int y1)
        {


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

            upperpixelsaddedcount = 0;

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

                    while ((myBitmap.GetPixel(xLeft - 1, yc).ToArgb() != CurrentLineColor.ToArgb())
                        && (myBitmap.GetPixel(xLeft - 1, yc).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        xLeft--;
                        myBitmap.SetPixel(xLeft, yc, CurrentFillColor);

                    }


                    while ((myBitmap.GetPixel(xRight + 1, yc).ToArgb() != CurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xRight + 1, yc).ToArgb() != CurrentFillColor.ToArgb()))
                    {
                        xRight++;
                        myBitmap.SetPixel(xRight, yc, CurrentFillColor);


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
            }

        }


        // Кнопка Выполнить
        private void ExecuteButton_Click(object sender, EventArgs e)
        {

            //отключаем кнопки
            ClearButton.Enabled = false;
            LineColorButton.Enabled = false;

            //создаем новый экземпляр Bitmap размером с элемент
            //pictureBox1 (myBitmap)
            //на нем выводим попиксельно отрезок
            //myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);


            if (BoldlineDrawButton.Checked == true)
            {
                //рисуем прямоугольник
                BoldLineDraw(10, 10, 10, 110);
                BoldLineDraw(10, 10, 110, 10);
                BoldLineDraw(10, 110, 110, 110);
                BoldLineDraw(110, 10, 110, 110);

                //рисуем треугольник
                BoldLineDraw(150, 10, 150, 200);
                BoldLineDraw(250, 50, 150, 200);
                BoldLineDraw(150, 10, 250, 150);
            }
            else
            {
                ////рисуем прямоугольник
                CDA(10, 10, 10, 110, Style);
                CDA(10, 10, 110, 10, Style);
                CDA(10, 110, 110, 110, Style);
                CDA(110, 10, 110, 110, Style);

                ////рисуем треугольник
                //CDA(150, 10, 150, 200);
                //CDA(250, 50, 150, 200);
                //CDA(150, 10, 250, 150);
            }



            //передаем полученный растр mybitmap в элемент pictureBox
            //pictureBox1.Image = myBitmap;


            // обновляем pictureBox и активируем кнопки
            pictureBox1.Image = myBitmap;

            pictureBox1.Refresh();

            ClearButton.Enabled = true;
            LineColorButton.Enabled = true;


        }


        // Убирает множественный выбор в чек листбоксе
        private void AlgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Текущий выбранный индекс
            int AlgIndex = AlgListBox.SelectedIndex;

            // Убирает выбор с других элементов
            for (int i = 0; i < AlgListBox.Items.Count; i++)
            {
                if (i != AlgIndex)
                    AlgListBox.SetItemCheckState(i, CheckState.Unchecked);

            }

            if (AlgIndex == 7)
            {

                // Если кнопка нажата, добавляет пиксели. 
                // Если кнопку отжать, потом нажать снова, пиксели сбрасываются
                // Нужно будет добавлять их заново. 

                PolygonPixels.Clear();
                Color newPixelColor = Color.White;
                for (int x = 0; x < myBitmap.Width; x++)
                {
                    for (int y = 0; y < myBitmap.Height; y++)
                    {
                        myBitmap.SetPixel(x, y, newPixelColor);
                    }
                }

                pictureBox1.Image = myBitmap;

                pictureBox1.Refresh();
            }

        }


        // Выбор стиля линии
        private void BoldlineDrawButton_CheckedChanged(object sender, EventArgs e)
        {

            if (BoldlineDrawButton.Checked == true)
            {
                Style = DrawRectangle;
            }
            else
            {
                Style = myBitmap.SetPixel;
            }
        }


        // Кнопка Создать многоугольник
        private void CreatePolygon_Click(object sender, EventArgs e)
        {


            if (AlgListBox.SelectedIndex == 7)
            {
                for (int i = 0; i < PolygonPixels.Count() - 1; i++)
                {
                    BresLineAlg(PolygonPixels[i][0], PolygonPixels[i][1], PolygonPixels[i + 1][0], PolygonPixels[i + 1][1], Style);
                }

                // Линия из последнего пикселя в первый
                BresLineAlg(PolygonPixels[PolygonPixels.Count() - 1][0], PolygonPixels[PolygonPixels.Count() - 1][1],
                            PolygonPixels[0][0], PolygonPixels[0][1], Style);

            }

            if (AlgListBox.SelectedIndex == 8 && PolygonPixels.Count == 2)
            {
                int x1 = PolygonPixels[0][0];
                int y1 = PolygonPixels[0][1];
                int x2 = PolygonPixels[1][0];
                int y2 = PolygonPixels[1][1];
                BresLineAlg(x1, y1, x2, y1, Style);
                BresLineAlg(x2, y1, x2, y2, Style);
                BresLineAlg(x2, y2, x1, y2, Style);
                BresLineAlg(x1, y2, x1, y1, Style);
            }
            // Рисует линии многоугольника

            pictureBox1.Image = myBitmap;

            pictureBox1.Refresh();
        }


        // Кнопка Простое отсечение отрезков 
        private void ClipLines1_Click(object sender, EventArgs e)
        {

            //for (int x = 0; x < myBitmap.Width; x++)
            //{
            //    for (int y = 0; y < myBitmap.Height; y++)
            //    {
            //        myBitmap.SetPixel(x, y, Color.White);
            //    }
            //}
            //pictureBox1.Image = myBitmap;


            Color TempColor = CurrentLineColor;
            CurrentLineColor = Color.Black;

            // Точки прямоугольника
            double x1 = PolygonPixels[0][0];
            double y1 = PolygonPixels[0][1];
            double x2 = PolygonPixels[1][0];
            double y2 = PolygonPixels[1][1];

            //BresLineAlg((int)x1, (int)y1, (int)x2, (int)y1, Style);
            //BresLineAlg((int)x2, (int)y1, (int)x2, (int)y2, Style);
            //BresLineAlg((int)x2, (int)y2, (int)x1, (int)y2, Style);
            //BresLineAlg((int)x1, (int)y2, (int)x1, (int)y1, Style);


            // Рёбра отсекающего окна
            double xl, xr, yn, yv;

            xl = Math.Min(x1, x2);
            xr = Math.Max(x1, x2);

            yn = Math.Min(y1, y2);
            yv = Math.Max(y1, y2);


            // цикл по отрезкам 
            for (int i = 0; i < LinePixels.Count(); i++)
            {

                // Точки отрезка
                x1 = LinePixels[i][0][0];
                y1 = LinePixels[i][0][1];
                x2 = LinePixels[i][1][0];
                y2 = LinePixels[i][1][1];


                // Мой код

                // лежит ли какая-либо точка снаружи окна
                // если не лежит - отрезок полностью внутри окна. 
                if ((x1 < xl || x1 > xr) || (x2 < xl || x2 > xr) || (y1 < yn || y1 > yv) || (y2 < yn || y2 > yv))
                {

                    // лежит ли вторая точка с этой же стороны окна.
                    // если да - отрезок невидим. Переходим к следующему 
                    if ((x1 < xl && x2 < xl) || (x1 > xr && x2 > xr) || (y1 > yv && y2 > yv) || (y1 < yn && y2 < yn))
                    {
                        continue;
                    }

                    // Если нет - отрезок частично виден. Определяем точки пересечения отрезка с окном. 
                    else
                    {
                        double ylp, ypp, xnp, xvp;

                        double dx = x2 - x1;
                        double dy = y2 - y1;
                        double tg = dy / dx;

                        // точки пересечения прямой, на которой лежит отрезок
                        // с продолжениями сторон прямоугольника
                        ylp = tg * (xl - x1) + y1;
                        ypp = tg * (xr - x1) + y1;
                        xvp = x1 + (yv - y1) / tg;
                        xnp = x1 + (yn - y1) / tg;

                        // проверка x
                        if (x1 < xl)
                        {
                            y1 = ylp;
                            x1 = xl;
                        }
                        if (x1 > xr)
                        {
                            y1 = ypp;
                            x1 = xr;
                        }
                        if (x2 > xr)
                        {
                            y2 = ypp;
                            x2 = xr;
                        }
                        if (x2 < xl)
                        {
                            y2 = ylp;
                            x2 = xl;
                        }

                        // проверка y
                        if (y1 > yv)
                        {
                            x1 = xvp;
                            y1 = yv;
                        }
                        if (y1 < yn)
                        {
                            x1 = xnp;
                            y1 = yn;
                        }

                        if (y2 < yn)
                        {
                            x2 = xnp;
                            y2 = yn;
                        }
                        if (y2 > yv)
                        {
                            x2 = xvp;
                            y2 = yv;
                        }

                        BresLineAlg((int)x1, (int)y1, (int)x2, (int)y2, Style);
                        continue;

                    }

                }

                else
                {
                    BresLineAlg((int)x1, (int)y1, (int)x2, (int)y2, Style);
                }


            }
            pictureBox1.Image = myBitmap;

            pictureBox1.Refresh();

            CurrentLineColor = TempColor;
            //LinePixels.Clear();
        }

        // Кнопка Отсечение Коэна-Сазерленда
        private void button1_Click(object sender, EventArgs e)
        {
            //for (int x = 0; x < myBitmap.Width; x++)
            //{
            //    for (int y = 0; y < myBitmap.Height; y++)
            //    {
            //        myBitmap.SetPixel(x, y, Color.White);
            //    }
            //}
            //pictureBox1.Image = myBitmap;

            Color TempColor = CurrentLineColor;
            CurrentLineColor = Color.Black;

            // Точки прямоугольника
            double x1 = PolygonPixels[0][0];
            double y1 = PolygonPixels[0][1];
            double x2 = PolygonPixels[1][0];
            double y2 = PolygonPixels[1][1];

            //BresLineAlg((int)x1, (int)y1, (int)x2, (int)y1, Style);
            //BresLineAlg((int)x2, (int)y1, (int)x2, (int)y2, Style);
            //BresLineAlg((int)x2, (int)y2, (int)x1, (int)y2, Style);
            //BresLineAlg((int)x1, (int)y2, (int)x1, (int)y1, Style);


            // Рёбра отсекающего окна
            double xl, xr, yn, yv;

            xl = Math.Min(x1, x2);
            xr = Math.Max(x1, x2);

            yn = Math.Min(y1, y2);
            yv = Math.Max(y1, y2);


            // цикл по отрезкам 
            for (int i = 0; i < LinePixels.Count(); i++)
            {

                // Коды начальной и конечной точек отрезка
                int FirstCode;
                int SecondCode;

                
                // Точки отрезка
                x1 = LinePixels[i][0][0];
                y1 = LinePixels[i][0][1];
                x2 = LinePixels[i][1][0];
                y2 = LinePixels[i][1][1];

                

                while (true)
                {

                    // Этот код должен быть двоичным, а не десятичным. 
                    FirstCode = 0000;
                    SecondCode = 0000;


                    // Код первой точки 
                    if (y1 < yn)
                    {
                        //FirstCode += 1000;
                        FirstCode += 8;
                    }
                    else if (y1 > yv)
                    {
                        //FirstCode += 100;
                        FirstCode += 4;
                    }

                    if (x1 > xr)
                    {
                        //FirstCode += 10;
                        FirstCode += 2;
                    }
                    else if (x1 < xl)
                    {
                        FirstCode += 1;
                    }

                    // Код второй точки 
                    if (y2 < yn)
                    {
                        //SecondCode += 1000;
                        SecondCode += 8;
                    }
                    else if (y2 > yv)
                    {
                        //SecondCode += 100;
                        SecondCode += 4;
                    }

                    if (x2 > xr)
                    {
                        //SecondCode += 10;
                        SecondCode += 2;
                    }
                    else if (x2 < xl)
                    {
                        SecondCode += 1;
                    }



                    // Проверка на видимость. 
                    // если коды обоих концов отрезка равны 0, то отрезок целиком внутри окна.
                    // Нарисовать его, выйти из этого цикла и перейти к следующему отрезку. 
                    if (FirstCode == 0 && SecondCode == 0)
                    {
                        BresLineAlg((int)x1, (int)y1, (int)x2, (int)y2, Style);
                        pictureBox1.Image = myBitmap;

                        pictureBox1.Refresh();
                        break;
                    }
                    // если логическое И(&) кодов обоих концов отрезка не равно нулю, то отрезок целиком вне окна.
                    // выйти из этого цикла и перейти к следующему отрезку. 
                    else if ((FirstCode & SecondCode) != 0)
                    {
                        break;
                    }

                    // отрезок подозрительный. Либо частично видимый, либо полностью невидимый. 
                    // Нужно его проверить. 
                    else
                    {
                        // Если начальная точка внутри окна 
                        // то она меняется местами с конечной точкой. 
                        if (FirstCode == 0)
                        {                 
                            double temp;

                            temp = x2;
                            x2 = x1;
                            x1 = temp;

                            temp = y2;
                            y2 = y1;
                            y1 = temp;
                        }

                        else if (SecondCode == 0)
                        {
                            double temp;
                            temp = x1;
                            x1 = x2;
                            x2 = temp;

                            temp = y1;
                            y1 = y2;
                            y2 = temp;

                        }

                        // Начальная точка лежит вне окна
                        //5.Анализируется код начальной точки для определения стороны окна,
                        //с которой есть пересечение, и выполняется расчет пересечения.При этом
                        //вычисленная точка пересечения заменяет начальную точку.

                        double ylp, ypp, xnp, xvp;

                        double dx = x2 - x1;
                        double dy = y2 - y1;
                        double tg = dy / dx;

                        // точки пересечения прямой, на которой лежит отрезок
                        // с продолжениями сторон прямоугольника
                        ylp = tg * (xl - x1) + y1;
                        ypp = tg * (xr - x1) + y1;
                        xvp = x1 + (yv - y1) / tg;
                        xnp = x1 + (yn - y1) / tg;

                        // проверка x
                        if (x1 < xl)
                        {
                            y1 = ylp;
                            x1 = xl;
                        }
                        if (x1 > xr)
                        {
                            y1 = ypp;
                            x1 = xr;
                        }
                        if (x2 > xr)
                        {
                            y2 = ypp;
                            x2 = xr;
                        }
                        if (x2 < xl)
                        {
                            y2 = ylp;
                            x2 = xl;
                        }

                        // проверка y
                        if (y1 > yv)
                        {
                            x1 = xvp;
                            y1 = yv;
                        }
                        if (y1 < yn)
                        {
                            x1 = xnp;
                            y1 = yn;
                        }

                        if (y2 < yn)
                        {
                            x2 = xnp;
                            y2 = yn;
                        }
                        if (y2 > yv)
                        {
                            x2 = xvp;
                            y2 = yv;
                        }


                        continue;


                    }

                    continue;
                }



            }
            CurrentLineColor = TempColor;
        }
    }
}
