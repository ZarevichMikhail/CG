using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace CG.View.Forms.Lab4
{
    public partial class Lab4Form : Form
    {
        public Lab4Form()
        {
            InitializeComponent();
        }


        // <summary>
        /// Оси ОХ, OY, OZ
        /// </summary>
        double[,] Axis = new double[6, 4];

        /// <summary>
        /// Матрица тела произвольной оси
        /// </summary>
        double[,] CustomAxis = new double[2, 4];


        /// <summary>
        /// Матрица тела тэтраэдра.
        /// Эта переменная у меня нигде не используется.
        /// Нужен рефракторинг.
        /// </summary>
        List<double[,]> Cube = new List<double[,]>();


        /// <summary>
        /// Координаты точек куба.
        /// По сути это матрица тела, а Cube можно убрать. 
        /// </summary>
        double[,] CubeDots = new double[8, 4];


        /// <summary>
        /// Грани куба.
        /// Каждая грань записывается в виде списка с индексами вершин. 
        /// </summary>
        int[,] CubeFaces = new int[6, 4];



        double[,] Center = new double[3,4];


        /// <summary>
        /// Матрица преобразвания координат.
        /// </summary>
        double[,] CoordTransformMatrix = new double[4, 4];



        /// <summary>
        /// Умножение матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double[,] MatrixMultiplication(double[,] a, double[,] b)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            double[,] result = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < m; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return result;
        }



        /// <summary>
        /// Инициализация матрицы осей
        /// </summary>
        private void InitAxis()
        {
            // OX
            Axis[0, 0] = 0; Axis[0, 1] = 0; Axis[0, 2] = 0; Axis[0, 3] = 1;
            Axis[1, 0] = -200; Axis[1, 1] = 0; Axis[1, 2] = 0; Axis[1, 3] = 1;

            // OY
            Axis[2, 0] = 0; Axis[2, 1] = 0; Axis[2, 2] = 0; Axis[2, 3] = 1;
            Axis[3, 0] = 0; Axis[3, 1] = 200; Axis[3, 2] = 0; Axis[3, 3] = 1;


            // OZ
            Axis[4, 0] = 0; Axis[4, 1] = 0; Axis[4, 2] = 0; Axis[4, 3] = 1;
            Axis[5, 0] = 0; Axis[5, 1] = 0; Axis[5, 2] = 200; Axis[5, 3] = 1;


        }



        /// <summary>
        /// Инициализация тэтраэдра
        /// </summary>
        private void InitCube()
        {

            // Координаты точек нижней грани
            CubeDots[0, 0] = 0;    CubeDots[0, 1] = 0;   CubeDots[0, 2] = 0;   CubeDots[0, 3] = 1;  // A
            CubeDots[1, 0] = -100; CubeDots[1, 1] = 0;   CubeDots[1, 2] = 0;   CubeDots[1, 3] = 1;  // B
            CubeDots[2, 0] = -100; CubeDots[2, 1] = 100; CubeDots[2, 2] = 0;   CubeDots[2, 3] = 1;  // C
            CubeDots[3, 0] = 0;    CubeDots[3, 1] = 100; CubeDots[3, 2] = 0;   CubeDots[3, 3] = 1;  // D

            // Координаты точек верхней грани
            CubeDots[4, 0] = 0;    CubeDots[4, 1] = 0;   CubeDots[4, 2] = 100; CubeDots[4, 3] = 1;  // E
            CubeDots[5, 0] = -100; CubeDots[5, 1] = 0;   CubeDots[5, 2] = 100; CubeDots[5, 3] = 1;  // F
            CubeDots[6, 0] = -100; CubeDots[6, 1] = 100; CubeDots[6, 2] = 100; CubeDots[6, 3] = 1;  // G
            CubeDots[7, 0] = 0;    CubeDots[7, 1] = 100; CubeDots[7, 2] = 100; CubeDots[7, 3] = 1;  // H

            //// Индексы вершин из которых состоит грань
            CubeFaces[0, 0] = 0; CubeFaces[0, 1] = 1; CubeFaces[0, 2] = 2; CubeFaces[0, 3] = 3; // ABCD
            CubeFaces[1, 0] = 0; CubeFaces[1, 1] = 1; CubeFaces[1, 2] = 5; CubeFaces[1, 3] = 4; // ABFE
            CubeFaces[2, 0] = 1; CubeFaces[2, 1] = 2; CubeFaces[2, 2] = 6; CubeFaces[2, 3] = 5; // BCGF
            CubeFaces[3, 0] = 2; CubeFaces[3, 1] = 3; CubeFaces[3, 2] = 7; CubeFaces[3, 3] = 6; // CDHG
            CubeFaces[4, 0] = 0; CubeFaces[4, 1] = 3; CubeFaces[4, 2] = 7; CubeFaces[4, 3] = 4; // ADHE
            CubeFaces[5, 0] = 4; CubeFaces[5, 1] = 5; CubeFaces[5, 2] = 6; CubeFaces[5, 3] = 7; // EFGH

            //// 0: ABCD (Нижняя) - смотрим снизу, обход против часовой
            //CubeFaces[0, 0] = 0; CubeFaces[0, 1] = 3; CubeFaces[0, 2] = 2; CubeFaces[0, 3] = 1;
            //// 1: ABFE (Передняя)
            //CubeFaces[1, 0] = 0; CubeFaces[1, 1] = 1; CubeFaces[1, 2] = 5; CubeFaces[1, 3] = 4;
            //// 2: BCGF (Правая)
            //CubeFaces[2, 0] = 1; CubeFaces[2, 1] = 2; CubeFaces[2, 2] = 6; CubeFaces[2, 3] = 5;
            //// 3: CDHG (Задняя)
            //CubeFaces[3, 0] = 2; CubeFaces[3, 1] = 3; CubeFaces[3, 2] = 7; CubeFaces[3, 3] = 6;
            //// 4: ADHE (Левая)
            //CubeFaces[4, 0] = 3; CubeFaces[4, 1] = 0; CubeFaces[4, 2] = 4; CubeFaces[4, 3] = 7;
            //// 5: EFGH (Верхняя)
            //CubeFaces[5, 0] = 4; CubeFaces[5, 1] = 5; CubeFaces[5, 2] = 6; CubeFaces[5, 3] = 7;

        }


        /// <summary>
        /// Инициализация матрицы преобразования
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="q"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="r"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="s"></param>
        private void InitCoordTransformMatrix(double a, double b, double c, double p,
                                              double d, double e, double f, double q,
                                              double h, double i, double j, double r,
                                              double l, double m, double n, double s)
        {
            CoordTransformMatrix[0, 0] = a; CoordTransformMatrix[0, 1] = b; CoordTransformMatrix[0, 2] = c; CoordTransformMatrix[0, 3] = p;
            CoordTransformMatrix[1, 0] = d; CoordTransformMatrix[1, 1] = e; CoordTransformMatrix[1, 2] = f; CoordTransformMatrix[1, 3] = q;
            CoordTransformMatrix[2, 0] = h; CoordTransformMatrix[2, 1] = i; CoordTransformMatrix[2, 2] = j; CoordTransformMatrix[2, 3] = r;
            CoordTransformMatrix[3, 0] = l; CoordTransformMatrix[3, 1] = m; CoordTransformMatrix[3, 2] = n; CoordTransformMatrix[3, 3] = s;
        }

        /// <summary>
        /// Вывод осей на экран
        /// </summary>
        private void DrawAxis()
        {
            //Инициализация матрицы сдвига с заданными параметрами
            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;
            //InitCoordTransformMatrix(1, 0, 0, 0,
            //                         0, 1, 0, 0,
            //                         0, 0, 1, 0,
            //                         m1, n1, 0, 1);

            //Перенос осей в центр picture box
            //double[,] osi1 = MatrixMultiplication(Axis, CoordTransformMatrix);
            //double alpha = 21.208;
            //double betha = 19.705;


            // Изометрическая проекция 
            double alpha = 45;
            double betha = 35.26;

            // Диметрическая проекция
            //double alpha = 22.208;
            //double betha = 20.705;


            //InitCoordTransformMatrix(Math.Cos(alpha), 0, -Math.Sin(alpha) * Math.Sin(betha), 0,
            //                                0, 1, Math.Cos(betha), 0,
            //                         Math.Sin(alpha), 0, Math.Cos(alpha) * Math.Sin(betha), 0,
            //                                m1, n1, 0, 1);

            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                            0, Math.Cos(betha), 0, 0,
                                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                            m1, n1, 0, 1);

            double[,] osi1 = MatrixMultiplication(Axis, CoordTransformMatrix);

            //double[,] osi1 =Axis;

            //Перенос осей в центр picture box
            //osi1 = MatrixMultiplication(Axis, CoordTransformMatrix);
            Pen myPen = new Pen(Color.Red, 1);// цвет линии и ширина
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            // рисуем ось ОХ
            g.DrawLine(myPen, (int)osi1[0, 0], (int)osi1[0, 1], (int)osi1[1, 0], (int)osi1[1, 1]);

            // рисуем ось ОУ
            g.DrawLine(myPen, (int)osi1[2, 0], (int)osi1[2, 1], (int)osi1[3, 0], (int)osi1[3, 1]);

            // рисуем OZ
            g.DrawLine(myPen, (int)osi1[4, 0], (int)osi1[4, 1], (int)osi1[5, 0], (int)osi1[5, 1]);

            g.Dispose();
            myPen.Dispose();
        }


        /// <summary>
        /// Поиск центра фигуры.
        /// </summary>
        /// <param name="cube_dots">Фигура</param>
        /// <returns>Массив с x y z координатами центра фигуры</returns>
        private double[] FindCenter(double[,] cube_dots)
        {
            // Координаты центра куба
            double[] Center = new double[3];

            for (int i = 0; i < 8; i++)
            {
                Center[0] += cube_dots[i, 0];
                Center[1] += cube_dots[i, 1];
                Center[2] += cube_dots[i, 2];
            }

            Center[0] = Center[0] / 8;
            Center[1] = Center[1] / 8;
            Center[2] = Center[2] / 8;

            return Center;
        }





        // Моя фукнция
        /// <summary>
        /// Алгоритм Робертса для определения видимости грани
        /// </summary>
        /// <param name="face">Грань, которую нужно проверить на видимость</param>
        /// <returns> IsVisible</returns>
        private bool RobertsAlg(int[] face, double[] FigureCenter, double[,] FigureDots )
        {

            bool IsVisible;

            // Уравнение плоскости по двум направляющим векторам и точке. 
            double[] VecOne = new double[3];
            double[] VecTwo = new double[3];

            // Вектор от 0-й вершины к 1-й
            VecOne[0] = FigureDots[face[1], 0] - FigureDots[face[0], 0];
            VecOne[1] = FigureDots[face[1], 1] - FigureDots[face[0], 1];
            VecOne[2] = FigureDots[face[1], 2] - FigureDots[face[0], 2];

            // Вектор от 0-й вершины к 3-й 
            VecTwo[0] = FigureDots[face[3], 0] - FigureDots[face[0], 0];
            VecTwo[1] = FigureDots[face[3], 1] - FigureDots[face[0], 1];
            VecTwo[2] = FigureDots[face[3], 2] - FigureDots[face[0], 2];

            // Коэффициенты уравнения плоскости, т.е. координаты вектора нормали. 
            double A;
            double B;
            double C;
            double D;

            A = VecOne[1] * VecTwo[2] - VecTwo[1] * VecOne[2];
            B = VecOne[2] * VecTwo[0] - VecTwo[2] * VecOne[0];
            C = VecOne[0] * VecTwo[1] - VecTwo[0] * VecOne[1];

            D = -(A * FigureDots[face[0], 0] + B * FigureDots[face[0], 1] + C * FigureDots[face[0], 2]);

            // Коэффициент, изменяющий знак плоскости 
            // Нужно определить, с какой стороны от плоскости грани находится центр фигуры. 
            // sign - это уравнение плоскости. Подставив в него координаты центра узнаем
            // с какой стороны он находится. 0 - на плоскости, + выше, - ниже. 
            // В алгоритме нужно, чтобы положительное направление было внутрь фигуры
            double sign;
            sign = -(A * FigureCenter[0] + B * FigureCenter[1] + C * FigureCenter[2] + D);


            // вектор направления взгляда
            // взгляд направлен из точки [-1,1,1] в противоположную сторону. 
            double[] E = new double[3];
            E = [ -1, 1, 1 ];

            // Если грань ориентирована правильно. 
            if (sign > 0)
            {
                
            }
            // Если плоскость ориентирована неправильно,
            // меняем направление вектора нормали
            else
            {
                A = -A;
                B = -B;
                C = -C;
                D = -D;
              
            }

            // Скалярное произведение вектора нормали и ветора взгляда 
            // Если оно положительно, значит взгляд и нормаль направлены в одну сторону
            // Если отрицательно, то взгляд и нормаль направлены в противоположные стороны
            // И это значит, что между этой гранью и взглядом есть другая грань, которая её закрывает. 
            // То есть рисовать её не надо. 
            if ((A * E[0] + B * E[1] + C * E[2]) > 0)
            {
                IsVisible = true;
            }
            else
            {
                IsVisible = false;
            }


            return IsVisible;
        }


        /// <summary>
        /// Делает проекцию фигуры и переносит её в центр экрана
        /// </summary>
        /// <param name="Figure">Матрица тела фигуры</param>
        /// <returns>Массив с экранными координатами фигуры</returns>
        private double[,] FigureProjection(double[,] Figure)
        {

            // Явное копирование массива figure_dots
            // Чтобы тут работать с копией, а не с оригиналом
            double[,] WorkingDots = (double[,])Figure.Clone();
            // Перенос фигуры  в центр picture box 
            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;

            //double alpha = 21.208;
            //double betha = 19.705;

            // Изометрическая проекция
            double alpha = 45;
            double betha = 35.26;


            // Проекция куба
            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                          0, Math.Cos(betha), 0, 0,
                                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                          0, 0, 0, 1);
            WorkingDots = MatrixMultiplication(WorkingDots, CoordTransformMatrix);



            //Перенос в центр
            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     m1, n1, 0, 1);
            WorkingDots = MatrixMultiplication(WorkingDots, CoordTransformMatrix);

            return WorkingDots;

        }


        /// <summary>
        // Рисует фигуру
        // </summary>
        private void DrawFigure(double[,] initial_figure_dots, int[,] initial_figure_faces)
        {
            // Явное копирование массива figure_dots
            // Чтобы тут работать с копией, а не с оригиналом
            double[,] WorkingDots = (double[,]) initial_figure_dots.Clone();

            // Количество граней в фигуре
            int FacesNumber = initial_figure_faces.GetLength(0);

            double[] FigureCenter = FindCenter(initial_figure_dots);

            WorkingDots = FigureProjection(WorkingDots);

            // Координаты центра куба
            // double[] CubeCenter = FindCenter(WorkingDots);



            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            for (int i = 0; i < FacesNumber; i++)
            {
                // Координаты точек текущей грани
                int[] face = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    face[j] = initial_figure_faces[i, j];
                }

                
                if (RobertsAlg(face, FigureCenter, initial_figure_dots) == true)
                {   
                    // Рисует линии от первой точки до последней
                    //for (int k = 1; k < face.GetLength(0); k++)
                    for (int k = 0; k < face.GetLength(0); k++)
                    {
                        int next = (k + 1) % 4; // Линии 0-1, 1-2, 2-3, 3-0
                        g.DrawLine(myPen, 
                            (int)WorkingDots[face[k], 0],    (int)WorkingDots[face[k], 1], 
                            (int)WorkingDots[face[next], 0], (int)WorkingDots[face[next], 1]);

                        //g.DrawLine(myPen, (int)WorkingDots[face[k - 1], 0], (int)WorkingDots[face[k - 1], 1], (int)WorkingDots[face[k], 0], (int)WorkingDots[face[k], 1]);

                    } 

                    //Линия из последней точки в первую
                    //g.DrawLine(myPen, (int)WorkingDots[face[3], 0], (int)WorkingDots[face[3], 1], (int)WorkingDots[face[0], 0], (int)WorkingDots[face[0], 1]);

                }
                else
                {

                    // //for (int k = 1; k < face.GetLength(0); k++)
                    //for (int k = 0; k < face.GetLength(0); k++)
                    //{
                    //    g.DrawLine(myPen, (int)WorkingDots[face[k - 1], 0], (int)WorkingDots[face[k - 1], 1], (int)WorkingDots[face[k], 0], (int)WorkingDots[face[k], 1]);
                    //int next = (k + 1) % 4; // Соединяем 0-1, 1-2, 2-3, 3-0
                    //g.DrawLine(myPen, (int)WorkingDots[face[k], 0], (int)WorkingDots[face[k], 1], (int)WorkingDots[face[next], 0], (int)WorkingDots[face[next], 1]);
                    //}
                    //g.DrawLine(myPen, (int)WorkingDots[face[3], 0], (int)WorkingDots[face[3], 1], (int)WorkingDots[face[0], 0], (int)WorkingDots[face[0], 1]);
                }
            }

            g.Dispose();
            myPen.Dispose();
        }


        private void DrawAxisButton_Click(object sender, EventArgs e)
        {
            InitAxis();
            DrawAxis();
        }


        private void DrawFigureButton_Click(object sender, EventArgs e)
        {
            InitCube();

            DrawFigure(CubeDots, CubeFaces);

        }


        /// <summary>
        /// Поворот относительно оси OY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateYButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            DrawAxis();
            // Перевод угла из градусов в радианы
            double Angle = double.Parse(AngleTextBox.Text);
            Angle = Angle * Math.PI / 180;


            InitCoordTransformMatrix(Math.Cos(Angle), 0, -Math.Sin(Angle), 0,
                                            0, 1, 0, 0,
                                     Math.Sin(Angle), 0, Math.Cos(Angle), 0,
                                            0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);
            
            DrawFigure(CubeDots, CubeFaces);



        }

        // Поворот относительно оси OX
        private void RotateXButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            DrawAxis();
            // Перевод угла из градусов в радианы
            double Angle = double.Parse(AngleTextBox.Text);
            Angle = Angle * Math.PI / 180;

            
            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, Math.Cos(Angle), Math.Sin(Angle), 0,
                                     0, -Math.Sin(Angle), Math.Cos(Angle), 0,
                                     0, 0, 0, 1);
            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            //}

            DrawFigure(CubeDots, CubeFaces);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
        }

        private void RotateZButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            DrawAxis();
            // Перевод угла из градусов в радианы
            double Angle = double.Parse(AngleTextBox.Text);
            Angle = Angle * Math.PI / 180;


            InitCoordTransformMatrix(Math.Cos(Angle), Math.Sin(Angle), 0, 0,
                                     -Math.Sin(Angle), Math.Cos(Angle), 0, 0,
                                     0, 0, 1, 0,
                                     0, 0, 0, 1);
            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            //}

            DrawFigure(CubeDots, CubeFaces);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
        }

        private void MoveXRight_Click(object sender, EventArgs e)
        {

            double Distance = double.Parse(DistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     Distance, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void MoveXLeft_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(DistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     -Distance, 0, 0, 1);
            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void MoveYUp_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(DistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     0, Distance, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void MoveYDown_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(DistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     0, -Distance, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void MoveZForwards_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(DistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     0, 0, Distance, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void MoveZBackwards_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(DistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     0, 0, -Distance, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void ScaleOXButton_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(Distance, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void ScaleOYButton_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, Distance, 0, 0,
                                     0, 0, 1, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void ScaleOZButton_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, Distance, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void ScalingXYButton_Click(object sender, EventArgs e)
        {
            double s = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1 / s, 0, 0, 0,
                                     0, 1 / s, 0, 0,
                                     0, 0, 1 / s, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);

        }

        private void ReflectionXButton_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(-1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void ReflectionYButton_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, -1, 0, 0,
                                     0, 0, 1, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void ReflextionOZButton_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            DrawAxis();


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, -1, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots, CubeFaces);
        }

        private void DrawCustomAxisButton_Click(object sender, EventArgs e)
        {
            double X1 = double.Parse(X1TextBox.Text);
            double X2 = double.Parse(X2TextBox.Text);

            double Y1 = double.Parse(Y1TextBox.Text);
            double Y2 = double.Parse(Y2TextBox.Text);

            double Z1 = double.Parse(Z1TextBox.Text);
            double Z2 = double.Parse(Z2TextBox.Text);

            CustomAxis[0, 0] = X1; CustomAxis[0, 1] = Y1; CustomAxis[0, 2] = Z1; CustomAxis[0, 3] = 1;
            CustomAxis[1, 0] = X2; CustomAxis[1, 1] = Y2; CustomAxis[1, 2] = Z2; CustomAxis[1, 3] = 1;

            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;

            double alpha = 45;
            double betha = 35.26;

            InitCoordTransformMatrix(Math.Cos(alpha),  Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                            0,         Math.Cos(betha),                   0, 0,
                                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                            m1,                 n1,                       0, 1);

            double[,] CustomAxis1 = MatrixMultiplication(CustomAxis, CoordTransformMatrix);

            Pen myPen = new Pen(Color.Green, 1);// цвет линии и ширина
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.DrawLine(myPen, (int)CustomAxis1[0, 0], (int)CustomAxis1[0, 1], (int)CustomAxis1[1, 0], (int)CustomAxis1[1, 1]);

            g.Dispose();
            myPen.Dispose();

        }

        
        /// <summary>
        /// Поворот вокруг произвольной оси.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateCustomAxisButton_Click(object sender, EventArgs e)
        {

            // Координаты двух точек, через которые проходит ось
            double X1 = double.Parse(X1TextBox.Text);
            double X2 = double.Parse(X2TextBox.Text);

            double Y1 = double.Parse(Y1TextBox.Text);
            double Y2 = double.Parse(Y2TextBox.Text);

            double Z1 = double.Parse(Z1TextBox.Text);
            double Z2 = double.Parse(Z2TextBox.Text);

            // Модуль направляющего вектора оси
            double module = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2));

            // Направляющие косинусы для осей x y z 
            double n1 = (X2- X1) / module;
            double n2 = (Y2- Y1) / module;
            double n3 = (Z2- Z1) / module;

            // Перенос то
            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     -CustomAxis[0, 0], -CustomAxis[0, 1], -CustomAxis[0, 2], 1);

            //double[,] CustomAxis1 = MatrixMultiplication(CustomAxis, CoordTransformMatrix);
            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            pictureBox1.Refresh();
            DrawAxis();
            // Перевод угла из градусов в радианы
            double Angle = double.Parse(AngleTextBox.Text);
            Angle = Angle * Math.PI / 180;


            double a = Math.Pow(n1, 2) + (1 - Math.Pow(n1, 2)) * Math.Cos(Angle);
            double b = n1 * n2 * (1 - Math.Cos(Angle))+n3* Math.Sin(Angle);
            double c = n1 * n3 * (1 - Math.Cos(Angle)) - n2 * Math.Sin(Angle);

            double d = n1 * n2 * (1 - Math.Cos(Angle)) - n3 * Math.Sin(Angle);
            double e1 = Math.Pow(n2, 2) + (1 - Math.Pow(n2, 2)) * Math.Cos(Angle); 
            double f = n2 * n3 * (1 - Math.Cos(Angle)) + n1 * Math.Sin(Angle);    

            double h = n1 * n3 * (1 - Math.Cos(Angle)) + n2 * Math.Sin(Angle);
            double i = n2 * n3 * (1 - Math.Cos(Angle)) - n1 * Math.Sin(Angle);
            double j = Math.Pow(n3, 2) + (1 - Math.Pow(n3, 2)) * Math.Cos(Angle);


            InitCoordTransformMatrix(a, b, c, 0,
                                     d, e1, f, 0,
                                     h, i, j, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);


            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     CustomAxis[0, 0], CustomAxis[0, 1], CustomAxis[0, 2], 1);


            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);


            DrawFigure(CubeDots, CubeFaces);

            Pen myPen = new Pen(Color.Green, 1);// цвет линии и ширина
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            double alpha = 45;
            double betha = 35.26;

            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                           0, Math.Cos(betha), 0, 0,
                                    Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                           pictureBox1.Width / 2, pictureBox1.Height / 2, 0, 1);

            double[,] CustomAxis1 = MatrixMultiplication(CustomAxis, CoordTransformMatrix);

            // Рисуем ось
            g.DrawLine(myPen, (int)CustomAxis1[0, 0], (int)CustomAxis1[0, 1], (int)CustomAxis1[1, 0], (int)CustomAxis1[1, 1]);
            DrawAxis();
            g.Dispose();
            myPen.Dispose();

        }
    }
}

