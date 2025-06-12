using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// для работы с библиотекой OpenGL
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl
using Tao.Platform.Windows;




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
        /// Матрица тела тэтраэдра
        /// </summary>
        List<double[,]> Cube = new List<double[,]>();


        /// <summary>
        /// Точки куба
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
        /// Алгоритм Робертса для определения видимости грани
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        private bool RobertsAlg(int[] face, double[] Center)
        {
            
            
            bool IsVisible;

            // Уравнение плоскости по двум направляющим векторам и точке. 
            double[] VecOne = new double[3];
            double[] VecTwo = new double[3];


            VecOne[0] = CubeDots[face[0], 0] - CubeDots[face[1], 0];
            VecOne[1] = CubeDots[face[0], 1] - CubeDots[face[1], 1];
            VecOne[2] = CubeDots[face[0], 2] - CubeDots[face[1], 2];

            VecTwo[0] = CubeDots[face[2], 0] - CubeDots[face[1], 0];
            VecTwo[1] = CubeDots[face[2], 1] - CubeDots[face[1], 1];
            VecTwo[2] = CubeDots[face[2], 2] - CubeDots[face[1], 2];

            // Коэффициенты уравнения плоскости
            double A;
            double B;
            double C;
            double D;

            A = VecOne[1] * VecTwo[2] - VecTwo[1] * VecOne[2];
            B = VecOne[2] * VecTwo[0] - VecTwo[2] * VecOne[0];
            C = VecOne[0] * VecTwo[1] - VecTwo[0] * VecOne[1];
            D = -(A * CubeDots[face[0], 0] + B * CubeDots[face[0], 1] + C * CubeDots[face[0], 2]);

            //коэффициент, изменяющий знак плоскости True, если Sign = +, False иначе
            double sign;
            double[] E = new double[3];
            E = [1, -1, -1];

            sign = -(A * Center[0] + B * Center[1] + C * Center[2]);

            if (sign > 0)
            {
                if ((A * E[0] + B * E[1] + C * E[2]+D)>0)
                {
                    IsVisible = true;
                }
                else
                {
                    IsVisible = false;
                }
            }
            else
            {
                A = -A;
                B = -B;
                C = -C;
                D = -D;
                if ((A * E[0] + B * E[1] + C * E[2]) > 0)
                {
                    IsVisible = true;
                }
                else
                {
                    IsVisible = false;
                }
            }


            return IsVisible;
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
            CubeDots[0, 0] = 0;   CubeDots[0, 1] = 0;   CubeDots[0, 2] = 0;   CubeDots[0, 3] = 1;  // A
            CubeDots[1, 0] = -100; CubeDots[1, 1] = 0;   CubeDots[1, 2] = 0;   CubeDots[1, 3] = 1;  // B
            CubeDots[2, 0] = -100; CubeDots[2, 1] = 100; CubeDots[2, 2] = 0;   CubeDots[2, 3] = 1;  // C
            CubeDots[3, 0] = 0;   CubeDots[3, 1] = 100; CubeDots[3, 2] = 0;   CubeDots[3, 3] = 1;  // D

            // Координаты точек верхней грани
            CubeDots[4, 0] = 0;   CubeDots[4, 1] = 0;   CubeDots[4, 2] = 100; CubeDots[4, 3] = 1;  // E
            CubeDots[5, 0] = -100; CubeDots[5, 1] = 0;   CubeDots[5, 2] = 100; CubeDots[5, 3] = 1;  // F
            CubeDots[6, 0] = -100; CubeDots[6, 1] = 100; CubeDots[6, 2] = 100; CubeDots[6, 3] = 1;  // G
            CubeDots[7, 0] = 0;   CubeDots[7, 1] = 100; CubeDots[7, 2] = 100; CubeDots[7, 3] = 1;  // H

            // Индексы вершин из которых состоит грань
            CubeFaces[0, 0] = 0; CubeFaces[0, 1] = 1; CubeFaces[0, 2] = 2; CubeFaces[0, 3] = 3; // ABCD
            CubeFaces[1, 0] = 0; CubeFaces[1, 1] = 1; CubeFaces[1, 3] = 5; CubeFaces[1, 3] = 4; // ABFE
            CubeFaces[2, 0] = 1; CubeFaces[2, 1] = 2; CubeFaces[2, 2] = 6; CubeFaces[2, 3] = 5; // BCGF
            CubeFaces[3, 0] = 2; CubeFaces[3, 1] = 3; CubeFaces[3, 2] = 7; CubeFaces[3, 3] = 6; // CDHG
            CubeFaces[4, 0] = 0; CubeFaces[4, 1] = 3; CubeFaces[4, 2] = 7; CubeFaces[4, 3] = 4; // ADHE
            CubeFaces[5, 0] = 4; CubeFaces[5, 1] = 5; CubeFaces[5, 2] = 6; CubeFaces[5, 3] = 7; // EFGH




            //// Грань ABCD
            //Cube.Add(new double[4, 4]);
            //Cube[0][0, 0] = CubeDots[0, 0]; Cube[0][0, 1] = CubeDots[0, 1]; Cube[0][0, 2] = CubeDots[0, 2]; Cube[0][0, 3] = CubeDots[0, 3];
            //Cube[0][1, 0] = CubeDots[1, 0]; Cube[0][1, 1] = CubeDots[1, 1]; Cube[0][1, 2] = CubeDots[1, 2]; Cube[0][1, 3] = CubeDots[1, 3];
            //Cube[0][2, 0] = CubeDots[2, 0]; Cube[0][2, 1] = CubeDots[2, 1]; Cube[0][2, 2] = CubeDots[2, 2]; Cube[0][2, 3] = CubeDots[2, 3];
            //Cube[0][3, 0] = CubeDots[3, 0]; Cube[0][3, 1] = CubeDots[3, 1]; Cube[0][3, 2] = CubeDots[3, 2]; Cube[0][3, 3] = CubeDots[3, 3];

            //// Грань ABFE
            //Cube.Add(new double[4, 4]);
            //Cube[1][0, 0] = CubeDots[0, 0]; Cube[1][0, 1] = CubeDots[0, 1]; Cube[1][0, 2] = CubeDots[0, 2]; Cube[1][0, 3] = CubeDots[0, 3];
            //Cube[1][1, 0] = CubeDots[1, 0]; Cube[1][1, 1] = CubeDots[1, 1]; Cube[1][1, 2] = CubeDots[1, 2]; Cube[1][1, 3] = CubeDots[1, 3];
            //Cube[1][2, 0] = CubeDots[5, 0]; Cube[1][2, 1] = CubeDots[5, 1]; Cube[1][2, 2] = CubeDots[5, 2]; Cube[1][2, 3] = CubeDots[5, 3];
            //Cube[1][3, 0] = CubeDots[4, 0]; Cube[1][3, 1] = CubeDots[4, 1]; Cube[1][3, 2] = CubeDots[4, 2]; Cube[1][3, 3] = CubeDots[4, 3];

            //// Грань BCGF
            //Cube.Add(new double[4, 4]);
            //Cube[2][0, 0] = CubeDots[1, 0]; Cube[2][0, 1] = CubeDots[1, 1]; Cube[2][0, 2] = CubeDots[1, 2]; Cube[2][0, 3] = CubeDots[1, 3];
            //Cube[2][1, 0] = CubeDots[2, 0]; Cube[2][1, 1] = CubeDots[2, 1]; Cube[2][1, 2] = CubeDots[2, 2]; Cube[2][1, 3] = CubeDots[2, 3];
            //Cube[2][2, 0] = CubeDots[6, 0]; Cube[2][2, 1] = CubeDots[6, 1]; Cube[2][2, 2] = CubeDots[6, 2]; Cube[2][2, 3] = CubeDots[6, 3];
            //Cube[2][3, 0] = CubeDots[5, 0]; Cube[2][3, 1] = CubeDots[5, 1]; Cube[2][3, 2] = CubeDots[5, 2]; Cube[2][3, 3] = CubeDots[5, 3];

            //// Грань CDHG
            //Cube.Add(new double[4, 4]);
            //Cube[3][0, 0] = CubeDots[2, 0]; Cube[3][0, 1] = CubeDots[2, 1]; Cube[3][0, 2] = CubeDots[2, 2]; Cube[3][0, 3] = CubeDots[2, 3];
            //Cube[3][1, 0] = CubeDots[3, 0]; Cube[3][1, 1] = CubeDots[3, 1]; Cube[3][1, 2] = CubeDots[3, 2]; Cube[3][1, 3] = CubeDots[3, 3];
            //Cube[3][2, 0] = CubeDots[7, 0]; Cube[3][2, 1] = CubeDots[7, 1]; Cube[3][2, 2] = CubeDots[7, 2]; Cube[3][2, 3] = CubeDots[7, 3];
            //Cube[3][3, 0] = CubeDots[6, 0]; Cube[3][3, 1] = CubeDots[6, 1]; Cube[3][3, 2] = CubeDots[6, 2]; Cube[3][3, 3] = CubeDots[6, 3];

            //// Грань ADHE
            //Cube.Add(new double[4, 4]);
            //Cube[4][0, 0] = CubeDots[0, 0]; Cube[4][0, 1] = CubeDots[0, 1]; Cube[4][0, 2] = CubeDots[0, 2]; Cube[4][0, 3] = CubeDots[0, 3];
            //Cube[4][1, 0] = CubeDots[3, 0]; Cube[4][1, 1] = CubeDots[3, 1]; Cube[4][1, 2] = CubeDots[3, 2]; Cube[4][1, 3] = CubeDots[3, 3];
            //Cube[4][2, 0] = CubeDots[7, 0]; Cube[4][2, 1] = CubeDots[7, 1]; Cube[4][2, 2] = CubeDots[7, 2]; Cube[4][2, 3] = CubeDots[7, 3];
            //Cube[4][3, 0] = CubeDots[4, 0]; Cube[4][3, 1] = CubeDots[4, 1]; Cube[4][3, 2] = CubeDots[4, 2]; Cube[4][3, 3] = CubeDots[4, 3];

            //// Грань EFGH
            //Cube.Add(new double[4, 4]);
            //Cube[5][0, 0] = CubeDots[4, 0]; Cube[5][0, 1] = CubeDots[4, 1]; Cube[5][0, 2] = CubeDots[4, 2]; Cube[5][0, 3] = CubeDots[4, 3];
            //Cube[5][1, 0] = CubeDots[5, 0]; Cube[5][1, 1] = CubeDots[5, 1]; Cube[5][1, 2] = CubeDots[5, 2]; Cube[5][1, 3] = CubeDots[5, 3];
            //Cube[5][2, 0] = CubeDots[6, 0]; Cube[5][2, 1] = CubeDots[6, 1]; Cube[5][2, 2] = CubeDots[6, 2]; Cube[5][2, 3] = CubeDots[6, 3];
            //Cube[5][3, 0] = CubeDots[7, 0]; Cube[5][3, 1] = CubeDots[7, 1]; Cube[5][3, 2] = CubeDots[7, 2]; Cube[5][3, 3] = CubeDots[7, 3];

            //// Ещё одна точка
            //Cube.Add(new double[3, 4]);
            //Cube[4][0, 0] = -300; Cube[4][0, 1] = 300; Cube[4][0, 2] = 300; Cube[4][0, 3] = 1;
            //Cube[4][1, 0] = -100; Cube[4][1, 1] = 0; Cube[4][1, 2] = 0; Cube[4][1, 3] = 1;
            //Cube[4][2, 0] = -0; Cube[4][2, 1] = 100; Cube[4][2, 2] = 0; Cube[4][2, 3] = 1;

        }

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


            // Аксонометрическая проекция 
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



        private double[] FindCenter(double[,] cubedots)
        {
            // Координаты центра куба
            double[] Center = new double[3];

            for (int i = 0; i < 8; i++)
            {
                Center[0] += cubedots[i, 0];
                Center[1] += cubedots[i, 1];
                Center[2] += cubedots[i, 2];
            }

            Center[0] = Center[0] / 8;
            Center[1] = Center[1] / 8;
            Center[2] = Center[2] / 8;

            return Center;
        }

        /// <summary>
        /// Рисует фигуру
        /// </summary>
        private void DrawFigure(double[,] cubedots)
        {

            // Перенос фигуры  в центр picture box 
            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;

            //double alpha = 21.208;
            //double betha = 19.705;

            // Аксонометрическая проекция
            double alpha = 45;
            double betha = 35.26;

            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                        0, Math.Cos(betha), 0, 0,
                                 Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                        0, 0, 0, 1);
            cubedots = MatrixMultiplication(cubedots, CoordTransformMatrix);

            //Перенос в центр
            InitCoordTransformMatrix(1, 0, 0, 0,
                                    0, 1, 0, 0,
                                    0, 0, 1, 0,
                                    m1, n1, 0, 1);
            cubedots = MatrixMultiplication(cubedots, CoordTransformMatrix);

            // Координаты центра куба
            double[] Center = FindCenter(cubedots);

            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            for (int i = 0; i < CubeFaces.GetLength(0); i++)
            {
                // Координаты точек текущей грани
                int[] face = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    face[j] = CubeFaces[i, j];
                }

                if (RobertsAlg(face, Center) == true)
                {   
                    // Рисует линии от первой точки до последней
                    for (int k = 1; k < face.GetLength(0); k++)
                    {
                        g.DrawLine(myPen, (int)cubedots[face[k - 1], 0], (int)cubedots[face[k - 1], 1], (int)cubedots[face[k], 0], (int)cubedots[face[k], 1]);

                    }

                    // Линия из последней точки в первую
                    g.DrawLine(myPen, (int)cubedots[face[3], 0], (int)cubedots[face[3], 1], (int)cubedots[face[0], 0], (int)cubedots[face[0], 1]);
                }
                else
                {

                    //for (int k = 1; k < face.GetLength(0); k++)
                    //{
                    //    g.DrawLine(myPen, (int)cubedots[face[k - 1], 0], (int)cubedots[face[k - 1], 1], (int)cubedots[face[k], 0], (int)cubedots[face[k], 1]);

                    //}
                    //g.DrawLine(myPen, (int)cubedots[face[3], 0], (int)cubedots[face[3], 1], (int)cubedots[face[0], 0], (int)cubedots[face[0], 1]);
                }
            }
            InitCoordTransformMatrix(1, 0, 0, 0,
                                    0, 1, 0, 0,
                                    0, 0, 1, 0,
                                    -m1, -n1, 0, 1);

            cubedots = MatrixMultiplication(cubedots, CoordTransformMatrix);



            //    for (int i = 0; i < Figure.Count; i++)
            //{

            //    //Грань
            //    double[,] Face = Figure[i];


            //    //InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
            //    //                            0, Math.Cos(betha), 0, 0,
            //    //                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
            //    //                            0, 0, 0, 1);
            //    //Face = MatrixMultiplication(Face, CoordTransformMatrix);


            //    //Center = MatrixMultiplication(Center, CoordTransformMatrix);



            //    double[] Center1 = new double[3];
            //    Center1[0] = 50;
            //    Center1[1] = 50;
            //    Center1[2] = 50;


            //    if (RobertsAlg(face, Center1) == true)
            //    {
            //        // Рисует фигуру относительно центра picture box
            //        Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //        //создаем новый объект Graphics (поверхность рисования) из pictureBox
            //        Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            //        // Перенос в центр
            //        InitCoordTransformMatrix(1, 0, 0, 0,
            //                                 0, 1, 0, 0,
            //                                 0, 0, 1, 0,
            //                                 m1, n1, 0, 1);
            //        Face = MatrixMultiplication(Face, CoordTransformMatrix);


            //        for (int j = 1; j < Face.GetLength(0); j++)
            //        {
            //            g.DrawLine(myPen, (int)Face[j - 1, 0], (int)Face[j - 1, 1], (int)Face[j, 0], (int)Face[j, 1]);

            //        }


            //        g.DrawLine(myPen, (int)Face[Face.GetLength(0) - 1, 0], (int)Face[Face.GetLength(0) - 1, 1], (int)Face[0, 0], (int)Face[0, 1]);
            //        //// Первая линия
            //        //g.DrawLine(myPen, (int)Face[0, 0], (int)Face[0, 1], (int)Face[1, 0], (int)Face[1, 1]);

            //        //// Вторая линия
            //        //g.DrawLine(myPen, (int)Face[1, 0], (int)Face[1, 1], (int)Face[2, 0], (int)Face[2, 1]);

            //        //// Третья линия
            //        //g.DrawLine(myPen, (int)Face[2, 0], (int)Face[2, 1], (int)Face[0, 0], (int)Face[0, 1]);

            //        g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            //        myPen.Dispose(); //освобождвем ресурсы, связанные с Pen

            //        // Перенос обратно в начало координат
            //        InitCoordTransformMatrix(1, 0, 0, 0,
            //                                 0, 1, 0, 0,
            //                                 0, 0, 1, 0,
            //                                 -m1, -n1, 0, 1);

            //        Face = MatrixMultiplication(Face, CoordTransformMatrix);
            //        g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            //        myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
            //    }
            //    else
            //    {
            //        // Рисует фигуру относительно центра picture box
            //        Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //        //создаем новый объект Graphics (поверхность рисования) из pictureBox
            //        Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            //        // Перенос в центр
            //        InitCoordTransformMatrix(1,  0,  0, 0,
            //                                 0,  1,  0, 0,
            //                                 0,  0,  1, 0,
            //                                 m1, n1, 0, 1);
            //        Face = MatrixMultiplication(Face, CoordTransformMatrix);

            //        for (int j = 1; j < Face.GetLength(0); j++)
            //        {
            //            g.DrawLine(myPen, (int)Face[j - 1, 0], (int)Face[j - 1, 1], (int)Face[j, 0], (int)Face[j, 1]);

            //        }


            //        g.DrawLine(myPen, (int)Face[Face.GetLength(0) - 1, 0], (int)Face[Face.GetLength(0) - 1, 1], (int)Face[0, 0], (int)Face[0, 1]);

            //        g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            //        myPen.Dispose(); //освобождвем ресурсы, связанные с Pen

            //        // Перенос обратно в начало координат
            //        InitCoordTransformMatrix(1, 0, 0, 0,
            //                                 0, 1, 0, 0,
            //                                 0, 0, 1, 0,
            //                                 -m1, -n1, 0, 1);

            //        Face = MatrixMultiplication(Face, CoordTransformMatrix);
            //        g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            //        myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
            //    }

            //    //for (int i = 1; i < Face.GetLength(0); i++)
            //    //{
            //    //    g.DrawLine(myPen, (int)Face[i - 1, 0], (int)Face[i - 1, 1], (int)Face[i, 0], (int)Face[i, 1]);

            //    //}



            //    // Первая линия
            //    //g.DrawLine(myPen, (int)Face[0, 0], (int)Face[0, 1], (int)Face[1, 0], (int)Face[1, 1]);

            //    // Вторая линия
            //    //g.DrawLine(myPen, (int)Face[1, 0], (int)Face[1, 1], (int)Face[2, 0], (int)Face[2, 1]);

            //    // Третья линия
            //    //g.DrawLine(myPen, (int)Face[2, 0], (int)Face[2, 1], (int)Face[0, 0], (int)Face[0, 1]);


            //}


            //InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
            //                                0, Math.Cos(betha), 0, 0,
            //                         Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
            //                                0, 0, 0, 1);
            //Cube[1] = MatrixMultiplication(Cube[1], CoordTransformMatrix);

            //// Перенос в центр
            //InitCoordTransformMatrix(1, 0, 0, 0,
            //                         0, 1, 0, 0,
            //                         0, 0, 1, 0,
            //                         m1, n1, 0, 1);
            //Figure = MatrixMultiplication(Figure, CoordTransformMatrix);





            //// Поворот относительно оси Y
            //InitCoordTransformMatrix(Math.Cos(alpha), -Math.Sin(alpha), 0, 0,
            //                                0, 1, 0, 0,
            //                         Math.Sin(alpha), Math.Cos(alpha), 0, 0,
            //                                0, 0, 0, 1);

            //Figure = MatrixMultiplication(Figure, CoordTransformMatrix);

            //// Поворот относительно оси Z
            //InitCoordTransformMatrix(Math.Cos(betha), Math.Sin(betha), 0, 0,
            //                        -Math.Sin(betha), Math.Cos(betha), 0, 0,
            //                         0, 0, 1, 0,
            //                         0, 0, 0, 1);
            //Figure = MatrixMultiplication(Figure, CoordTransformMatrix);

            //// Параллельное проецирование на ось X
            //InitCoordTransformMatrix(0, 0, 0, 0,
            //                         0, 1, 0, 0,
            //                         0, 0, 1, 0,
            //                         0, 0, 0, 1);
            //Figure = MatrixMultiplication(Figure, CoordTransformMatrix);

            //InitCoordTransformMatrix(1, 0, 0, 0,
            //                         0, 1, 0, 0,
            //                         0, 0, 1, 0,
            //                         m1, n1, 0, 1);
            //Figure = MatrixMultiplication(Figure, CoordTransformMatrix);


        }

        private void DrawAxisButton_Click(object sender, EventArgs e)
        {
            InitAxis();
            DrawAxis();
        }

        private void DrawFigureButton_Click(object sender, EventArgs e)
        {
            InitCube();


            DrawFigure(CubeDots);

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
            
            DrawFigure(CubeDots);



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
            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }

            DrawFigure(CubeDots);
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
            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);

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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            DrawFigure(CubeDots);
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

            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha),  0, 0,
                                            0,         Math.Cos(betha),                   0, 0,
                                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                            m1, n1,                                       0, 1);

            double[,] CustomAxis1 = MatrixMultiplication(CustomAxis, CoordTransformMatrix);

            Pen myPen = new Pen(Color.Green, 1);// цвет линии и ширина
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            // рисуем ось ОХ
            g.DrawLine(myPen, (int)CustomAxis1[0, 0], (int)CustomAxis1[0, 1], (int)CustomAxis1[1, 0], (int)CustomAxis1[1, 1]);

            g.Dispose();
            myPen.Dispose();

        }

        // Это пока не сделал. 
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

            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     CustomAxis[0, 0], CustomAxis[0, 1], CustomAxis[0, 2], 1);

            double[,] CustomAxis1 = MatrixMultiplication(CustomAxis, CoordTransformMatrix);

            pictureBox1.Refresh();
            DrawAxis();
            // Перевод угла из градусов в радианы
            double Angle = double.Parse(AngleTextBox.Text);
            Angle = Angle * Math.PI / 180;


            double a = Math.Pow(n1, 2) + (1 - Math.Pow(n1, 2)) * Math.Cos(Angle);
            double b = n1 * n2 * (1 - Math.Cos(Angle))+n3* Math.Sin(Angle);
            double c = n1 * n3 * (1 - Math.Cos(Angle)) - n2 * Math.Sin(Angle);

            double d = n1 * n2 * (1 - Math.Cos(Angle)) - n3 * Math.Sin(Angle);
            double e1 = Math.Pow(n2, 2) + (1 - Math.Pow(n2, 2)) * Math.Cos(Angle); // не дал присвоить имя e из за ошибки CS0136 Локальная переменная или параметр с именем "e" нельзя объявить в данной области, так как это имя используется во включающей локальной области для определения локальной переменной или параметра
            double f = n2 * n3 * (1 - Math.Cos(Angle)) + n1 * Math.Sin(Angle);    // не знаю, из-за чего она возникла, такой переменной у меня больше нигде нет. 

            double h = n1 * n3 * (1 - Math.Cos(Angle)) + n2 * Math.Sin(Angle);
            double i = n2 * n3 * (1 - Math.Cos(Angle)) - n1 * Math.Sin(Angle);
            double j = Math.Pow(n3, 2) + (1 - Math.Pow(n3, 2)) * Math.Cos(Angle);


            InitCoordTransformMatrix(a, b, c, 0,
                                     d, e1, f, 0,
                                     h, i, j, 0,
                                     0, 0, 0, 1);

            CubeDots = MatrixMultiplication(CubeDots, CoordTransformMatrix);

            DrawFigure(CubeDots);

        }
    }
}

