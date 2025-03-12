namespace CG.View.Forms.Lab2
{
    public partial class Lab2Form : Form
    {
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

            // Задаёт стиль линии по умолчанию. 
            Style = myBitmap.SetPixel;

        }


        /// <summary>
        /// Переменные, содержащие координаты и приращения
        /// </summary>
        public int xn, yn, xk, yk;

        Bitmap myBitmap; // объект Bitmap для вывода отрезка


        Color СurrentLineColor = Color.Red; // текущий цвет отрезка
        Color CurrentFillCllor = Color.Green; // Текущий цвет заливки
        //currentBorderColor = Color.Red;

        /// <summary>
        /// Индекс выбранного алгоритма
        /// </summary>
        int AlgIndex;


        /// <summary>
        /// Матрица оператора поворота.
        /// </summary>
        double[,] Operator = { { 0, -1 }, { 1, 0 } };


        /// <summary>
        /// Делегат для функции стиля линии
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="PixelColor"></param>
        public delegate void DrawStyle(int x, int y, Color PixelColor);


        /// <summary>
        /// Стиль линии
        /// </summary>
        DrawStyle Style;




        public double[,] MatrixMultiply(double[,] m1, double[,] m2)
        {
            double[,] result = new double[2, 1];

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

            double xOutput, yOutput, dx, dy;

            xk = e.X;
            yk = e.Y;

            textBox1.Text = xk.ToString() + " " + yk.ToString();

            // Выбор алгоритма рисования
            // 0 Отрезок по обычному ЦДА
            // 1 Отрезок по несимметричному цда
            // 2 отрезок по Брезенхему
            // 3 окружность по Брезенхему
            // 4 Рекурсивная заливка
            // 5 Итеративная заливка
            // 6 Построчная заливка
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

            }

            pictureBox1.Image = myBitmap;

            // обновляем pictureBox и активируем кнопки
            pictureBox1.Refresh();

        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Все пиксели компонента PictureBox перекрасятся в светлый цвет

            //Bitmap myBitmap = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            ////Задаем цвет пикселя по схеме RGB (от 0 до 255 для каждого цвета)
            //Color newPixelColor = Color.FromArgb(247, 249, 239);
            //for (int x = 0; x < myBitmap.Width; x++)
            //{
            //    for (int y = 0; y < myBitmap.Height; y++)
            //    {
            //        myBitmap.SetPixel(x, y, newPixelColor);
            //    }
            //}
            //pictureBox1.Image = myBitmap;

            // Метод обновления компонента PictureBox
            pictureBox1.Image = null;


        }

        /// <summary>
        /// Выбор цвета линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineColorButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();

            if (dialogResult == DialogResult.OK && DDAAlgButton.Checked)
            {
                СurrentLineColor = colorDialog1.Color;
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

            if (dialogResult == DialogResult.OK && DDAAlgButton.Checked)
            {
                CurrentFillCllor = colorDialog1.Color;
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

            // Поменял количество узлов т.к. если их слишком мало, то заливка не будет работать
            // Будет ошибка. System.ArgumentOutOfRangeException: "Parameter must be positive and < Width. Arg_ParamName_Name"
            numberNodes = 1000;

            xOutput = xn;
            yOutput = yn;

            for (index = 1; index <= numberNodes; index++)
            {

                // Рисует линию выбранного стиля. 
                Style((int)xOutput, (int)yOutput, СurrentLineColor);

                // myBitmap.SetPixel((int)xOutput, (int)yOutput, СurrentLineColor);

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
            int count = 0;
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

            // 2 варианта: 1 и 4 четверть. 3 и 4 получаются, соответственно, отражениями координат
            // т.е. рисую не в x1,y1 в x2,y2, а наоборот. условия вверху нужны для этого. 
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
                    Style((int)xOutput, (int)yOutput, СurrentLineColor); /* Очередная точка вектора */

                }
            }
            else if (dy >= dx && dx >= 0)
            {
                E = 2 * dy - dx;
                for (double i = dy; (i - 1) >= 0; i--)
                {
                    if (E >= 0)
                    {
                        xOutput = xOutput + 1;
                        yOutput = yOutput + 1;
                        E = E + 2 * (dx - dy);
                    }
                    else
                    {
                        yOutput = yOutput + 1;
                        E = E + 2 * dx;
                    }
                    Style((int)xOutput, (int)yOutput, СurrentLineColor); /* Очередная точка вектора */

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
                    Style((int)xOutput, (int)yOutput, СurrentLineColor); /* Очередная точка вектора */
                }
            }
            else if (Math.Abs(dy) >= Math.Abs(dx) && dy <= 0)
            {

                E = 2 * Math.Abs(dy) - Math.Abs(dx);
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
                    count++;
                    Style((int)xOutput, (int)yOutput, СurrentLineColor); /* Очередная точка вектора */
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
            // Первую четверть будем рисовать относительно старой системы координат
            // Чтобы нарисовать остальные - нужно повернуть точку на 90 градусов с помощью оператора поворота
            // Но он поворачивает, только относительно начала координат
            // Ведём новую ск с центром в точке xc,yc, чтобы поворачивать относительно неё
            // потом будем переводить координаты полученной точки в старую ск и рисовать 

            // Теория по преобразованию координат, слайд 5
            // https://moodle.math.tusur.ru/pluginfile.php/1187/mod_resource/content/1/Preobr_dsk.pdf

            int radius = int.Parse(RadiusTextBox.Text);


            // координаты точки, которую надо повернуть. 
            double[,] Coord = new double[2, 1];


            // координаты точки, которую надо нарисовать, в старой системе координат.
            // output потому, что рисовать будем именно в ней
            double xOutput = xc;
            double yOutput = yc + radius;



            // Координаты точки в новой ск 
            double xn = xOutput - xc; // = 0
            double yn = yOutput - yc; // = radius



            Style((int)xOutput, (int)yOutput, СurrentLineColor); /* Очередная точка вектора */


            // Считаем относительно новой системы координат 
            double Dd = Math.Pow(xn + 1, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);
            double Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
            double Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);



            while (yn >= 0)
            {
                if (Dd < 0)
                {
                    double di = Math.Abs(Dg) - Math.Abs(Dd);

                    if (di <= 0)
                    {
                        // Горизонтальный пиксель
                        //xOutput = xOutput + 1;

                        // Меняем точку в новой системе кординат
                        xn++;

                        // Переводим из новой в старую
                        xOutput = xn + xc;
                        //yOutput = yOutput;

                        Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        // Заношу координаты в новой ск в матрицу
                        //Coord[0, 0] = xn;
                        //Coord[1, 0] = yn;

                        //// поворот на 90 градусов
                        //Coord = MatrixMultiply(Operator, Coord);

                        //// Перевожу координаты из новой ск в старую
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;

                        // Рисую координаты в старой ск
                        Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //// повторяю это ещё 2 раза
                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);


                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];

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

                        Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //// Заношу координаты в новой ск в матрицу
                        //Coord[0, 0] = xn;
                        //Coord[1, 0] = yn;

                        //// поворот на 90 градусов
                        //Coord = MatrixMultiply(Operator, Coord);
               

                        //// Перевожу координаты из новой ск в старую
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;

                        //// Рисую координаты в старой ск
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //// повторяю это ещё 2 раза
                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);


                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];

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

                        Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //// Заношу координаты в новой ск в матрицу
                        //Coord[0, 0] = xn;
                        //Coord[1, 0] = yn;

                        //// поворот на 90 градусов
                        //Coord = MatrixMultiply(Operator, Coord);

                        //// Перевожу координаты из новой ск в старую
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;

                        //// Рисую координаты в старой ск
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //// повторяю это ещё 2 раза
                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);


                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];

                        Dd = Dd + 2 * xn - 2 * yn + 2;
                        Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
                        Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);


                    }
                    else
                    {
                        // Вертикальный пиксель

                        yn--;

                        yOutput = yn + yc;

                        Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //// Заношу координаты в новой ск в матрицу
                        //Coord[0, 0] = xn;
                        //Coord[1, 0] = yn;

                        //// поворот на 90 градусов
                        //Coord = MatrixMultiply(Operator, Coord);

                        //// Перевожу координаты из новой ск в старую
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;

                        //// Рисую координаты в старой ск
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //// повторяю это ещё 2 раза
                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);


                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];
                        //xOutput = xn + xc;
                        //yOutput = yn + yc;
                        //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                        //Coord = MatrixMultiply(Operator, Coord);
                        //xn = Coord[0, 0];
                        //yn = Coord[1, 0];

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

                    Style((int)xOutput, (int)yOutput, СurrentLineColor);


                    //// Заношу координаты в новой ск в матрицу
                    //Coord[0, 0] = xn;
                    //Coord[1, 0] = yn;

                    //// поворот на 90 градусов
                    //Coord = MatrixMultiply(Operator, Coord);

                    //// Перевожу координаты из новой ск в старую
                    //xn = Coord[0, 0];
                    //yn = Coord[1, 0];
                    //xOutput = xn + xc;
                    //yOutput = yn + yc;

                    //// Рисую координаты в старой ск
                    //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                    //// повторяю это ещё 2 раза
                    //Coord = MatrixMultiply(Operator, Coord);
                    //xn = Coord[0, 0];
                    //yn = Coord[1, 0];
                    //xOutput = xn + xc;
                    //yOutput = yn + yc;
                    //Style((int)xOutput, (int)yOutput, СurrentLineColor);


                    //Coord = MatrixMultiply(Operator, Coord);
                    //xn = Coord[0, 0];
                    //yn = Coord[1, 0];
                    //xOutput = xn + xc;
                    //yOutput = yn + yc;
                    //Style((int)xOutput, (int)yOutput, СurrentLineColor);

                    //Coord = MatrixMultiply(Operator, Coord);
                    //xn = Coord[0, 0];
                    //yn = Coord[1, 0];

                    Dd = Dd + 2 * xn - 2 * yn + 2;
                    Dg = Math.Pow(xn + 1, 2) + Math.Pow(yn, 2) - Math.Pow(radius, 2);
                    Dv = Math.Pow(xn, 2) + Math.Pow(yn - 1, 2) - Math.Pow(radius, 2);
                }
            }



        }

        /// <summary>
        /// Рисует квадрат. 
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
            for (int i = 1; i <= height; i++)
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
                DrawRectangle((int)xOutput, (int)yOutput, СurrentLineColor);

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
            // Если линии разных цветов - ошибка System.ArgumentOutOfRangeException

            // сравнение цветов происходит в формате RGB
            // для этого используем метод ToArgb объекта Color
            if ((oldPixelColor.ToArgb() != СurrentLineColor.ToArgb())
            && (oldPixelColor.ToArgb() != CurrentFillCllor.ToArgb()))
            {
                //перекрашиваем пиксель

                myBitmap.SetPixel(x1, y1, CurrentFillCllor);

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
                if ((oldPixelColor.ToArgb() != СurrentLineColor.ToArgb())
                     && (oldPixelColor.ToArgb() != CurrentFillCllor.ToArgb()))
                {

                    myBitmap.SetPixel(xc, yc, CurrentFillCllor);


                    // Для всех четырех соседних пикселов проверить, является ли он граничным или уже перекрашен

                    if ((myBitmap.GetPixel(xc, yc + 1).ToArgb() != СurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc, yc + 1).ToArgb() != CurrentFillCllor.ToArgb()))
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

                    if ((myBitmap.GetPixel(xc + 1, yc).ToArgb() != СurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc + 1, yc).ToArgb() != CurrentFillCllor.ToArgb()))
                    {
                        int[] a = { xc + 1, yc };
                        Pixels.Push(a);
                    }

                    if ((myBitmap.GetPixel(xc, yc - 1).ToArgb() != СurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc, yc - 1).ToArgb() != CurrentFillCllor.ToArgb()))
                    {
                        int[] a = { xc, yc - 1 };
                        Pixels.Push(a);
                    }

                    if ((myBitmap.GetPixel(xc - 1, yc).ToArgb() != СurrentLineColor.ToArgb())
                            && (myBitmap.GetPixel(xc - 1, yc).ToArgb() != CurrentFillCllor.ToArgb()))
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

        /// <summary>
        /// Построчная заливка
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        private void SpanFloodFill(int x1, int y1)
        {

        }


        private void ExecuteButton_Click(object sender, EventArgs e)
        {

            //отключаем кнопки
            ClearButton.Enabled = false;
            LineColorButton.Enabled = false;

            //создаем новый экземпляр Bitmap размером с элемент
            //pictureBox1 (myBitmap)
            //на нем выводим попиксельно отрезок
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (Graphics g = Graphics.FromHwnd(pictureBox1.Handle))
            {
                if (DDAAlgButton.Checked == true)
                {
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
                        //CDA(10, 10, 10, 110);
                        //CDA(10, 10, 110, 10);
                        //CDA(10, 110, 110, 110);
                        //CDA(110, 10, 110, 110);

                        ////рисуем треугольник
                        //CDA(150, 10, 150, 200);
                        //CDA(250, 50, 150, 200);
                        //CDA(150, 10, 250, 150);
                    }

                }

                else
                {
                    if (FillButton.Checked == true)
                    {
                        //получаем растр созданного рисунка в mybitmap
                        myBitmap = pictureBox1.Image as Bitmap;

                        // задаем координаты затравки
                        xn = 160;
                        yn = 40;
                        // вызываем рекурсивную процедуру заливки с затравкой
                        RecursiveFloodFill(xn, yn);
                    }
                }

                //передаем полученный растр mybitmap в элемент pictureBox
                pictureBox1.Image = myBitmap;


                // обновляем pictureBox и активируем кнопки
                pictureBox1.Refresh();
                ClearButton.Enabled = true;
                LineColorButton.Enabled = true;
            }

        }


        // Убирает множественный выбор
        private void AlgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Текущий выбранный индекс
            AlgIndex = AlgListBox.SelectedIndex;

            // Убирает выбор с других элементов
            for (int i = 0; i < AlgListBox.Items.Count; i++)
            {
                if (i != AlgIndex)
                    AlgListBox.SetItemCheckState(i, CheckState.Unchecked);
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
    }
}
