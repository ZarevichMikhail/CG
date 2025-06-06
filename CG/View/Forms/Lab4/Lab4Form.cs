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

        //double[,] Tetraedr;

        /// <summary>
        /// Матрица тела тэтраэдра
        /// </summary>
        List<double[,]> Tetraedr = new List<double[,]>();

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
            Axis[2, 0] = 0; Axis[2, 1] = -0; Axis[2, 2] = 0; Axis[2, 3] = 1;
            Axis[3, 0] = 0; Axis[3, 1] = 200; Axis[3, 2] = 0; Axis[3, 3] = 1;


            // OZ
            Axis[4, 0] = 0; Axis[4, 1] = 0; Axis[4, 2] = -0; Axis[4, 3] = 1;
            Axis[5, 0] = 0; Axis[5, 1] = 0; Axis[5, 2] = 200; Axis[5, 3] = 1;


        }

        


        double[] T = new double[4];

        /// <summary>
        /// Инициализация тэтраэдра
        /// </summary>
        private void InitTet()
        {
            double[] x1 = { 0, 0, 0 };
            double[] x2 = { 100, 0, 0 };
            double[] x3 = { 0, 100, 0 };
            double[] x4 = { 0, 0, 100 };



            // Грань OAB
            Tetraedr.Add(new double[3, 4]);
            Tetraedr[0][0, 0] = 0; Tetraedr[0][0, 1] = 0; Tetraedr[0][0, 2] = 0; Tetraedr[0][0, 3] = 1;
            Tetraedr[0][1, 0] = -100; Tetraedr[0][1, 1] = 0; Tetraedr[0][1, 2] = 0; Tetraedr[0][1, 3] = 1;
            Tetraedr[0][2, 0] = 0; Tetraedr[0][2, 1] = 100; Tetraedr[0][2, 2] = 0; Tetraedr[0][2, 3] = 1;

            // Грань ABC
            Tetraedr.Add(new double[3, 4]);
            Tetraedr[1][0, 0] = -100; Tetraedr[1][0, 1] = 0; Tetraedr[1][0, 2] = 0; Tetraedr[1][0, 3] = 1;
            Tetraedr[1][1, 0] = 0; Tetraedr[1][1, 1] = 100; Tetraedr[1][1, 2] = 0; Tetraedr[1][1, 3] = 1;
            Tetraedr[1][2, 0] = 0; Tetraedr[1][2, 1] = 0; Tetraedr[1][2, 2] = 100; Tetraedr[1][2, 3] = 1;

            // Грань OBD
            Tetraedr.Add(new double[3, 4]);
            Tetraedr[2][0, 0] = 0; Tetraedr[2][0, 1] = 0; Tetraedr[2][0, 2] = 0; Tetraedr[2][0, 3] = 1;
            Tetraedr[2][1, 0] = 0; Tetraedr[2][1, 1] = 100; Tetraedr[2][1, 2] = 0; Tetraedr[2][1, 3] = 1;
            Tetraedr[2][2, 0] = 0; Tetraedr[2][2, 1] = 0; Tetraedr[2][2, 2] = 100; Tetraedr[2][2, 3] = 1;

            // Грань OAC
            Tetraedr.Add(new double[3, 4]);
            Tetraedr[3][0, 0] = 0; Tetraedr[3][0, 1] = 0; Tetraedr[3][0, 2] = 0; Tetraedr[3][0, 3] = 1;
            Tetraedr[3][1, 0] = -100; Tetraedr[3][1, 1] = 0; Tetraedr[3][1, 2] = 0; Tetraedr[3][1, 3] = 1;
            Tetraedr[3][2, 0] = 0; Tetraedr[3][2, 1] = 0; Tetraedr[3][2, 2] = 100; Tetraedr[3][2, 3] = 1;





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


            // Изометрическая проекция 
            double alpha = 45;
            double betha = 35.26;

            // Диметрическая проекция
            //double alpha = 22.208;
            //double betha = 20.705;


            //InitCoordTransformMatrix(Math.Cos(alpha),0, -Math.Sin(alpha) * Math.Sin(betha), 0,
            //                                0,              1,    Math.Cos(betha), 0,
            //                         Math.Sin(alpha), 0, Math.Cos(alpha) * Math.Sin(betha), 0,
            //                                m1,               n1,                         0, 1);

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
        /// Переносит уже преобразованную фигуру в центр picture box
        /// Рисует её
        /// Переносит обратно в начало системы координат
        /// </summary>
        private void DrawFigure(double[,] Figure)
        {

            // Перенос фигуры  в центр picture box 
            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;



            //double alpha = 21.208;
            //double betha = 19.705;

            double alpha = 45;
            double betha = 35.26;

            // Проекция
            //double alpha = 22.208;
            //double betha = 20.705;
            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                            0, Math.Cos(betha), 0, 0,
                                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                            0, 0, 0, 1);
            Figure = MatrixMultiplication(Figure, CoordTransformMatrix);

            // Перенос в центр
            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     m1, n1, 0, 1);
            Figure = MatrixMultiplication(Figure, CoordTransformMatrix);

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

            // Рисует фигуру относительно центра picture box
            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);



            //for (int i = 1; i < Figure.GetLength(0); i++)
            //{
            //    g.DrawLine(myPen, (int)Figure[i - 1, 0], (int)Figure[i - 1, 1], (int)Figure[i, 0], (int)Figure[i, 1]);

            //}

            // Первая линия
            g.DrawLine(myPen, (int)Figure[0, 0], (int)Figure[0, 1], (int)Figure[1, 0], (int)Figure[1, 1]);

            // Вторая линия
            g.DrawLine(myPen, (int)Figure[1, 0], (int)Figure[1, 1], (int)Figure[2, 0], (int)Figure[2, 1]);

            // Третья линия
            g.DrawLine(myPen, (int)Figure[2, 0], (int)Figure[2, 1], (int)Figure[0, 0], (int)Figure[0, 1]);






            //g.DrawLine(myPen, (int)Figure[Figure.GetLength(0) - 1, 0], (int)Figure[Figure.GetLength(0) - 1, 1], (int)Figure[0, 0], (int)Figure[0, 1]);

            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen


            // Перенос обратно в начало координат
            InitCoordTransformMatrix(1, 0, 0, 0,
                                     0, 1, 0, 0,
                                     0, 0, 1, 0,
                                     -m1, -n1, 0, 1);

            Figure = MatrixMultiplication(Figure, CoordTransformMatrix);
        }

        private void DrawAxisButton_Click(object sender, EventArgs e)
        {
            InitAxis();
            DrawAxis();
        }

        private void DrawFigureButton_Click(object sender, EventArgs e)
        {
            InitTet();
            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }



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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);

                //for (int j = 0; j < 3; j++)
                //{
                //    Tetraedr[i][j, 0] = Tetraedr[i][j, 0] / s;
                //    Tetraedr[i][j, 1] = Tetraedr[i][j, 1] / s;
                //    Tetraedr[i][j, 2] = Tetraedr[i][j, 1] / s;
                //    Tetraedr[i][j, 3] = 1;
                //}

            }

            for (int i = 0; i < Tetraedr.Count; i++)

                DrawFigure(Tetraedr[i]);
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                Tetraedr[i] = MatrixMultiplication(Tetraedr[i], CoordTransformMatrix);
            }

            for (int i = 0; i < Tetraedr.Count; i++)
            {
                DrawFigure(Tetraedr[i]);
            }
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

            InitCoordTransformMatrix(Math.Cos(alpha), Math.Sin(alpha) * Math.Sin(betha), 0, 0,
                                            0, Math.Cos(betha), 0, 0,
                                     Math.Sin(alpha), -Math.Cos(alpha) * Math.Sin(betha), 0, 0,
                                            m1, n1, 0, 1);

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

            for (int i1 = 0; i1 < Tetraedr.Count; i1++)
            {
                Tetraedr[i1] = MatrixMultiplication(Tetraedr[i1], CoordTransformMatrix);
            }

            for (int i2 = 0; i2 < Tetraedr.Count; i2++)
            {
                DrawFigure(Tetraedr[i2]);
                continue;
            }

        }
    }
}

