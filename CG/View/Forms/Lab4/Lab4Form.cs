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

        //double[,] Cube;

        /// <summary>
        /// Матрица тела тэтраэдра
        /// </summary>
        List<double[,]> Cube = new List<double[,]>();


        /// <summary>
        /// Точки куба
        /// </summary>
        double[,] Dots = new double[8, 4];


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
        private bool RobertsAlg(double[,] face, double[] Center)
        {
            
            
            bool IsVisible;

            // Уравнение плоскости по двум направляющим векторам и точке. 
            double[] VecOne = new double[3];
            double[] VecTwo = new double[3];

            VecOne[0] = face[0, 0] - face[1, 0];
            VecOne[1] = face[0, 1] - face[1, 1];
            VecOne[2] = face[0, 2] - face[1, 2];

            VecTwo[0] = face[2, 0] - face[1, 0];
            VecTwo[1] = face[2, 1] - face[1, 1];
            VecTwo[2] = face[2, 2] - face[1, 2];

            // Коэффициенты уравнения плоскости
            double A;
            double B;
            double C;
            double D;

            A = VecOne[1] * VecTwo[2] - VecTwo[1] * VecOne[2];
            B = VecOne[2] * VecTwo[0] - VecTwo[2] * VecOne[0];
            C = VecOne[0] * VecTwo[1] - VecTwo[0] * VecOne[1];
            D = -(A * face[0,0] + B * face[0, 1] + C * face[0, 2]);

            //коэффициент, изменяющий знак плоскости True, если Sign = +, False иначе
            double sign;
            double[] E = new double[3];
            E = [1, 0, 0];

            sign = -(A * Center[0] + B * Center[1] + C * Center[2]);

            if (sign >= 0)
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
            double[] x1 = { 0, 0, 0 };
            double[] x2 = { 100, 0, 0 };
            double[] x3 = { 0, 100, 0 };
            double[] x4 = { 0, 0, 100 };

            double[] A = { 0, 0, 0, 1 };

            // Нижняя грань
            Dots[0, 0] = 0; Dots[0, 1] = 0; Dots[0, 2] = 0; Dots[0,3] = 1;       // A
            Dots[1, 0] = 100; Dots[1, 1] = 0; Dots[1, 2] = 0; Dots[1, 3] = 1;    // B
            Dots[2, 0] = 100; Dots[2, 1] = 100; Dots[2, 2] = 0; Dots[2, 3] = 1;  // C
            Dots[3, 0] = 0; Dots[3, 1] = 100; Dots[3, 2] = 0; Dots[3, 3] = 1;    // D

            // Верхняя грань
            Dots[4, 0] = 0; Dots[4, 1] = 0; Dots[4, 2] = 100; Dots[4, 3] = 1;    // E
            Dots[5, 0] = 100; Dots[5, 1] = 0; Dots[5, 2] = 100; Dots[5, 3] = 1;  // F
            Dots[6, 0] = 0; Dots[6, 1] = 100; Dots[6, 2] = 100; Dots[6, 3] = 1;  // G
            Dots[7, 0] = 100; Dots[7, 1] = 100; Dots[7, 2] = 100; Dots[3, 3] = 1;// H


            // Грань ABCD
            Cube.Add(new double[4, 4]);
            Cube[0][0, 0] = Dots[0, 0]; Cube[0][0, 1] = Dots[0, 1]; Cube[0][0, 2] = Dots[0, 2]; Cube[0][0, 3] = Dots[0, 3];
            Cube[0][1, 0] = Dots[1, 0]; Cube[0][1, 1] = Dots[1, 1]; Cube[0][1, 2] = Dots[1, 2]; Cube[0][1, 3] = Dots[1, 3];
            Cube[0][2, 0] = Dots[2, 0]; Cube[0][2, 1] = Dots[2, 1]; Cube[0][2, 2] = Dots[2, 2]; Cube[0][2, 3] = Dots[2, 3];
            Cube[0][3, 0] = Dots[3, 0]; Cube[0][3, 1] = Dots[3, 1]; Cube[0][3, 2] = Dots[3, 2]; Cube[0][2, 3] = Dots[3, 3];

            // Грань ABFE
            Cube.Add(new double[4, 4]);
            Cube[1][0, 0] = Dots[0, 0]; Cube[1][0, 1] = Dots[0, 1]; Cube[1][0, 2] = Dots[0, 2]; Cube[1][0, 3] = Dots[0, 3];
            Cube[1][1, 0] = Dots[1, 0]; Cube[1][1, 1] = Dots[1, 1]; Cube[1][1, 2] = Dots[1, 2]; Cube[1][1, 3] = Dots[1, 3];
            Cube[1][2, 0] = Dots[5, 0]; Cube[1][2, 1] = Dots[5, 1]; Cube[1][2, 2] = Dots[5, 2]; Cube[1][2, 3] = Dots[5, 3];
            Cube[1][3, 0] = Dots[4, 0]; Cube[1][3, 1] = Dots[4, 1]; Cube[1][3, 2] = Dots[4, 2]; Cube[1][3, 3] = Dots[4, 3];

            // Грань BCGF
            Cube.Add(new double[4, 4]);
            Cube[2][0, 0] = Dots[1, 0]; Cube[2][0, 1] = Dots[1, 1]; Cube[2][0, 2] = Dots[1, 2]; Cube[2][0, 3] = Dots[1, 3];
            Cube[2][1, 0] = Dots[2, 0]; Cube[2][1, 1] = Dots[2, 1]; Cube[2][1, 2] = Dots[2, 2]; Cube[2][1, 3] = Dots[2, 3];
            Cube[2][2, 0] = Dots[6, 0]; Cube[2][2, 1] = Dots[6, 1]; Cube[2][2, 2] = Dots[6, 2]; Cube[2][2, 3] = Dots[6, 3];
            Cube[2][3, 0] = Dots[5, 0]; Cube[2][3, 1] = Dots[5, 1]; Cube[2][3, 2] = Dots[5, 2]; Cube[2][3, 3] = Dots[5, 3];

            // Грань CDHG
            Cube.Add(new double[4, 4]);
            Cube[3][0, 0] = Dots[2, 0]; Cube[3][0, 1] = Dots[2, 1]; Cube[3][0, 2] = Dots[2, 2]; Cube[3][0, 3] = Dots[2, 3];
            Cube[3][1, 0] = Dots[3, 0]; Cube[3][1, 1] = Dots[3, 1]; Cube[3][1, 2] = Dots[3, 2]; Cube[3][1, 3] = Dots[3, 3];
            Cube[3][2, 0] = Dots[7, 0]; Cube[3][2, 1] = Dots[7, 1]; Cube[3][2, 2] = Dots[7, 2]; Cube[3][2, 3] = Dots[7, 3];
            Cube[3][3, 0] = Dots[6, 0]; Cube[3][3, 1] = Dots[6, 1]; Cube[3][3, 2] = Dots[6, 2]; Cube[3][3, 3] = Dots[6, 3];

            // Грань ADHE
            Cube.Add(new double[4, 4]);
            Cube[4][0, 0] = Dots[0, 0]; Cube[4][0, 1] = Dots[0, 1]; Cube[4][0, 2] = Dots[0, 2]; Cube[4][0, 3] = Dots[0, 3];
            Cube[4][1, 0] = Dots[3, 0]; Cube[4][1, 1] = Dots[3, 1]; Cube[4][1, 2] = Dots[3, 2]; Cube[4][1, 3] = Dots[3, 3];
            Cube[4][2, 0] = Dots[7, 0]; Cube[4][2, 1] = Dots[7, 1]; Cube[4][2, 2] = Dots[7, 2]; Cube[4][2, 3] = Dots[7, 3];
            Cube[4][3, 0] = Dots[4, 0]; Cube[4][3, 1] = Dots[4, 1]; Cube[4][3, 2] = Dots[4, 2]; Cube[4][3, 3] = Dots[4, 3];

            // Грань EFGH
            Cube.Add(new double[4, 4]);
            Cube[5][0, 0] = Dots[4, 0]; Cube[5][0, 1] = Dots[4, 1]; Cube[5][0, 2] = Dots[4, 2]; Cube[5][0, 3] = Dots[4, 3];
            Cube[5][1, 0] = Dots[5, 0]; Cube[5][1, 1] = Dots[5, 1]; Cube[5][1, 2] = Dots[5, 2]; Cube[5][1, 3] = Dots[5, 3];
            Cube[5][2, 0] = Dots[6, 0]; Cube[5][2, 1] = Dots[6, 1]; Cube[5][2, 2] = Dots[6, 2]; Cube[5][2, 3] = Dots[6, 3];
            Cube[5][3, 0] = Dots[7, 0]; Cube[5][3, 1] = Dots[7, 1]; Cube[5][3, 2] = Dots[7, 2]; Cube[5][3, 3] = Dots[7, 3];

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


        /// <summary>
        /// Рисует фигуру
        /// </summary>
        private void DrawFigure(List<double[,]> Figure)
        {

            // Перенос фигуры  в центр picture box 
            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;



            //double alpha = 21.208;
            //double betha = 19.705;

            double alpha = 45;
            double betha = 35.26;



            double x = (Cube[0][1, 0] + Cube[0][2, 0] + Cube[1][2, 0] + Cube[2][2, 0]) / 4;
            double y = (Cube[0][1, 1] + Cube[0][2, 1] + Cube[1][2, 1] + Cube[2][2, 1]) / 4;
            double z = (Cube[0][1, 2] + Cube[0][2, 2] + Cube[1][2, 2] + Cube[2][2, 2]) / 4;

            Center[0, 0] = x; Center[0, 1] = 0; Center[0, 2] = 0; Center[0, 3] = 1;
            Center[1, 0] = 0; Center[1, 1] = y; Center[1, 2] = 0; Center[1, 3] = 1;
            Center[2, 0] = 0; Center[2, 1] = 0; Center[2, 2] = z; Center[2, 3] = 1;



            for (int i = 0; i < Figure.Count; i++)
            {

                //Грань
                double[,] Face = Figure[i];


                //InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                //                            0, Math.Cos(betha), 0, 0,
                //                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                //                            0, 0, 0, 1);
                //Face = MatrixMultiplication(Face, CoordTransformMatrix);


                Center = MatrixMultiplication(Center, CoordTransformMatrix);



                double[] Center1 = new double[3];
                Center1[0] = 50;
                Center1[1] = 50;
                Center1[2] = 50;

                
                if (RobertsAlg(Face, Center1) == true)
                {
                    // Рисует фигуру относительно центра picture box
                    Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

                    //создаем новый объект Graphics (поверхность рисования) из pictureBox
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                    // Перенос в центр
                    InitCoordTransformMatrix(1, 0, 0, 0,
                                             0, 1, 0, 0,
                                             0, 0, 1, 0,
                                             m1, n1, 0, 1);
                    Face = MatrixMultiplication(Face, CoordTransformMatrix);


                    for (int j = 1; j < Face.GetLength(0); j++)
                    {
                        g.DrawLine(myPen, (int)Face[j - 1, 0], (int)Face[j - 1, 1], (int)Face[j, 0], (int)Face[j, 1]);

                    }


                    g.DrawLine(myPen, (int)Face[Face.GetLength(0) - 1, 0], (int)Face[Face.GetLength(0) - 1, 1], (int)Face[0, 0], (int)Face[0, 1]);
                    //// Первая линия
                    //g.DrawLine(myPen, (int)Face[0, 0], (int)Face[0, 1], (int)Face[1, 0], (int)Face[1, 1]);

                    //// Вторая линия
                    //g.DrawLine(myPen, (int)Face[1, 0], (int)Face[1, 1], (int)Face[2, 0], (int)Face[2, 1]);

                    //// Третья линия
                    //g.DrawLine(myPen, (int)Face[2, 0], (int)Face[2, 1], (int)Face[0, 0], (int)Face[0, 1]);

                    g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
                    myPen.Dispose(); //освобождвем ресурсы, связанные с Pen

                    // Перенос обратно в начало координат
                    InitCoordTransformMatrix(1, 0, 0, 0,
                                             0, 1, 0, 0,
                                             0, 0, 1, 0,
                                             -m1, -n1, 0, 1);

                    Face = MatrixMultiplication(Face, CoordTransformMatrix);
                    g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
                    myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
                }
                else
                {
                    // Рисует фигуру относительно центра picture box
                    Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

                    //создаем новый объект Graphics (поверхность рисования) из pictureBox
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

                    // Перенос в центр
                    InitCoordTransformMatrix(1,  0,  0, 0,
                                             0,  1,  0, 0,
                                             0,  0,  1, 0,
                                             m1, n1, 0, 1);
                    Face = MatrixMultiplication(Face, CoordTransformMatrix);

                    for (int j = 1; j < Face.GetLength(0); j++)
                    {
                        g.DrawLine(myPen, (int)Face[j - 1, 0], (int)Face[j - 1, 1], (int)Face[j, 0], (int)Face[j, 1]);

                    }


                    g.DrawLine(myPen, (int)Face[Face.GetLength(0) - 1, 0], (int)Face[Face.GetLength(0) - 1, 1], (int)Face[0, 0], (int)Face[0, 1]);

                    g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
                    myPen.Dispose(); //освобождвем ресурсы, связанные с Pen

                    // Перенос обратно в начало координат
                    InitCoordTransformMatrix(1, 0, 0, 0,
                                             0, 1, 0, 0,
                                             0, 0, 1, 0,
                                             -m1, -n1, 0, 1);

                    Face = MatrixMultiplication(Face, CoordTransformMatrix);
                    g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
                    myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
                }

                //for (int i = 1; i < Face.GetLength(0); i++)
                //{
                //    g.DrawLine(myPen, (int)Face[i - 1, 0], (int)Face[i - 1, 1], (int)Face[i, 0], (int)Face[i, 1]);

                //}



                // Первая линия
                //g.DrawLine(myPen, (int)Face[0, 0], (int)Face[0, 1], (int)Face[1, 0], (int)Face[1, 1]);

                // Вторая линия
                //g.DrawLine(myPen, (int)Face[1, 0], (int)Face[1, 1], (int)Face[2, 0], (int)Face[2, 1]);

                // Третья линия
                //g.DrawLine(myPen, (int)Face[2, 0], (int)Face[2, 1], (int)Face[0, 0], (int)Face[0, 1]);


            }


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


            double x = (Cube[0][1, 0] + Cube[0][2, 0] + Cube[1][2, 0] + Cube[2][2, 0])/4;
            double y = (Cube[0][1, 1] + Cube[0][2, 1] + Cube[1][2, 1] + Cube[2][2, 1]) / 4;
            double z = (Cube[0][1, 2] + Cube[0][2, 2] + Cube[1][2, 2] + Cube[2][2, 2]) / 4;
            
            Center[0, 0] = x; Center[0, 1] = 0; Center[0, 2] = 0; Center[0, 3] = 1;
            Center[1, 0] = 0; Center[1, 1] = y; Center[1, 2] = 0; Center[1, 3] = 1;
            Center[2, 0] = 0; Center[2, 1] = 0; Center[2, 2] = z; Center[2, 3] = 1;

            //Center[0] = -1; 
            //Center[1] = -1;
            //Center[2] = -1;


            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    Center[0] = Center[0] + Cube[i][0,0];
            //}


            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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


            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}



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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }

            DrawFigure(Cube);
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }

            DrawFigure(Cube);
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }

            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);

                //for (int j = 0; j < 3; j++)
                //{
                //    Cube[i][j, 0] = Cube[i][j, 0] / s;
                //    Cube[i][j, 1] = Cube[i][j, 1] / s;
                //    Cube[i][j, 2] = Cube[i][j, 1] / s;
                //    Cube[i][j, 3] = 1;
                //}

            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)

            //    DrawFigure(Cube[i]);
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i = 0; i < Cube.Count; i++)
            {
                Cube[i] = MatrixMultiplication(Cube[i], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i = 0; i < Cube.Count; i++)
            //{
            //    DrawFigure(Cube[i]);
            //}
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

            for (int i1 = 0; i1 < Cube.Count; i1++)
            {
                Cube[i1] = MatrixMultiplication(Cube[i1], CoordTransformMatrix);
            }
            DrawFigure(Cube);
            //for (int i2 = 0; i2 < Cube.Count; i2++)
            //{
            //    DrawFigure(Cube[i2]);
            //    continue;
            //}

        }
    }
}

