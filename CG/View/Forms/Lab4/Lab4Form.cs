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



        //double[,] Tetraedr;


        List<double[]> Tetraedr = new List<double[]>();

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
            Axis[0, 0] = -200; Axis[0, 1] = 0; Axis[0, 2] = 0; Axis[0, 3] = 1;
            Axis[1, 0] = 200; Axis[1, 1] = 0; Axis[1, 2] = 0; Axis[1, 3] = 1;

            Axis[2, 0] = 0; Axis[2, 1] = -200; Axis[2, 2] = 0; Axis[2, 3] = 1;
            Axis[3, 0] = 0; Axis[3, 1] = 200; Axis[3, 2] = 0; Axis[3, 3] = 1;

            Axis[4, 0] = 0; Axis[4, 1] = 0; Axis[4, 2] = -200; Axis[4, 3] = 1;
            Axis[5, 0] = 0; Axis[5, 1] = 0; Axis[5, 2] = 200; Axis[5, 3] = 1;


        }


        /// <summary>
        /// Инициализация тэтраэдра
        /// </summary>
        private void InitTet()
        {
            double[] x1 = { 0, 0, 0 };
            double[] x2 = { 50, 0, 0 };
            double[] x3 = { 0, 50, 0 };
            double[] x4 = { 0, 0, 50 };


            Tetraedr.Add(new double[1]);
            // Нижняя грань
            //Tetraedr[0, 0] = 0; Tetraedr[0, 1] = 0; Tetraedr[0, 2] = 0;
            //Tetraedr[0][0] = x1;

        }

        private void InitCoordTransformMatrix(double a, double b, double c, double p,
                                              double d, double e, double f, double q,
                                              double h, double i, double j, double r,
                                              double l, double m, double n, double s)
        {
            CoordTransformMatrix[0, 0] = a; CoordTransformMatrix[0, 1] = b; CoordTransformMatrix[0, 2] = c; CoordTransformMatrix[0, 3] = p;
            CoordTransformMatrix[1, 0] = d; CoordTransformMatrix[1, 1] = e; CoordTransformMatrix[1, 2] = f; CoordTransformMatrix[1, 3] = q;
            CoordTransformMatrix[2, 0] = m; CoordTransformMatrix[2, 1] = i; CoordTransformMatrix[2, 2] = j; CoordTransformMatrix[2, 3] = r;
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
            //InitCoordTransformMatrix(1,  0,  0,  0,
            //                         0,  1,  0,  0,
            //                         0,  0,  1,  0,
            //                         0, 0, 0, 1);

            //Перенос осей в центр picture box
            //double[,] osi1 = MatrixMultiplication(Axis, CoordTransformMatrix);
            double alpha = 21.208;
            double betha = 20.705;
            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                            0, Math.Cos(betha), 0, 0,
                                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                     m1, n1, 0, 1);

            double[,] osi1 = MatrixMultiplication(Axis, CoordTransformMatrix);


            //Перенос осей в центр picture box
            //osi1 = MatrixMultiplication(Axis, CoordTransformMatrix);
            Pen myPen = new Pen(Color.Red, 1);// цвет линии и ширина
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            //рисуем ось ОХ
            g.DrawLine(myPen, (int)osi1[0, 0], (int)osi1[0, 1], (int)osi1[1, 0], (int)osi1[1, 1]);
            //рисуем ось ОУ
            g.DrawLine(myPen, (int)osi1[2, 0], (int)osi1[2, 1], (int)osi1[3, 0], (int)osi1[3, 1]);

            g.DrawLine(myPen, (int)osi1[4, 0], (int)osi1[4, 1], (int)osi1[5, 0], (int)osi1[5, 1]);
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
            InitTet();
        }
    }
}
