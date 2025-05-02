using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using static CG.View.Forms.Lab2.Lab2Form;
using Microsoft.VisualBasic.ApplicationServices;


namespace CG.View.Forms.Lab3
{
    public partial class Lab3Form : Form
    {
        public Lab3Form()
        {
            InitializeComponent();

            AlgCheckListBox.Items.Add("Сдвиг по оси ОХ вправо");
            AlgCheckListBox.Items.Add("Сдвиг по оси ОХ влево");
            AlgCheckListBox.Items.Add("Сдвиг по оси ОУ вверх");
            AlgCheckListBox.Items.Add("Сдвиг по оси ОУ вниз");
            AlgCheckListBox.Items.Add("Поворот по часовой стрелке");
            AlgCheckListBox.Items.Add("Смещение по оси ОХ");
            AlgCheckListBox.Items.Add("Смещение по оси ОУ");
            AlgCheckListBox.Items.Add("Масштабирование");
            AlgCheckListBox.Items.Add("Двигать велосипед");

        }



        /// <summary>
        /// Делегат для методов преобразования.
        /// Метод преобразования выбирается в чеклистбоксе 
        /// Потом делегат передаётся в таймер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void TransformStyle(Object sender, EventArgs e);



        /// <summary>
        /// Метод преобразования
        /// Метод преобразования выбирается в чеклистбоксе 
        /// Потом делегат передаётся в таймер
        /// </summary>
        public TransformStyle Style;



        /// <summary>
        /// Квадрат.
        /// Точки представлены в виде матриц-строк
        /// 4 точки, имеющие 3 однородные координаты
        /// </summary>
        double[,] kv = new double[4, 3];

        /// <summary>
        /// Моя фигура
        /// Точки представлены в виде матриц-строк
        /// </summary>
        double[,] MyFigure = new double[5, 3];


        /// <summary>
        /// Велосипед. Коллекция массивов. 
        /// Состоит из 4 фигур. 
        /// 0, 1 - левое и правое колесо.
        /// 2 - корпус.
        /// 3 - педали. 
        /// </summary>
        //double[,,] Velo = new double[4, 4, 3];

        //List<List<List<double>>> Velo = new List<List<List<double>>>();

        List<double[,]> Velo = new List<double[,]>();



        /// <summary>
        /// Оси ОХ и ОУ
        /// </summary>
        double[,] osi = new double[4, 3];


        /// <summary>
        /// Матрица сдвига 
        /// </summary>
        double[,] MoveMatrix = new double[3, 3];

        double k, l; // элементы матрицы сдвига

        /// <summary>
        /// Матрица поворота
        /// </summary>
        double[,] RotationMatrix = new double[3, 3];

        /// <summary>
        /// Матрица преобразвания координат.
        /// </summary>
        double[,] CoordTransformMatrix = new double[3, 3];


        /// <summary>
        /// Инициализация матрицы преобразования координат.
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <param name="p">p</param>
        /// <param name="c">c</param>
        /// <param name="d">d</param>
        /// <param name="q">q</param>
        /// <param name="m">m</param>
        /// <param name="n">n</param>
        /// <param name="s">s</param>
        private void InitCoordTransformMatrix(double a, double b, double p,
                                              double c, double d, double q,
                                              double m, double n, double s)
        {
            CoordTransformMatrix[0, 0] = a; CoordTransformMatrix[0, 1] = b; CoordTransformMatrix[0, 2] = q;
            CoordTransformMatrix[1, 0] = c; CoordTransformMatrix[1, 1] = d; CoordTransformMatrix[1, 2] = p;
            CoordTransformMatrix[2, 0] = m; CoordTransformMatrix[2, 1] = n; CoordTransformMatrix[2, 2] = s;
        }

        /// <summary>
        /// Инициализация матрицы тела квадрата.
        /// Центр в начале координат. 
        /// </summary>
        private void Init_kvadrat()
        {
            kv[0, 0] = -50; kv[0, 1] = 0; kv[0, 2] = 1;
            kv[1, 0] = 0; kv[1, 1] = 50; kv[1, 2] = 1;
            kv[2, 0] = 50; kv[2, 1] = 0; kv[2, 2] = 1;
            kv[3, 0] = 0; kv[3, 1] = -50; kv[3, 2] = 1;
        }


        /// <summary>
        /// Инициализация матрицы тела моей фигуры.
        /// Центр в начале координат. 
        /// </summary>
        private void InitMyFigure()
        {
            MyFigure[0, 0] = -80; MyFigure[0, 1] = -80; MyFigure[0, 2] = 1;
            MyFigure[1, 0] = -40; MyFigure[1, 1] = -80; MyFigure[1, 2] = 1;
            MyFigure[2, 0] = 80; MyFigure[2, 1] = 80; MyFigure[2, 2] = 1;
            MyFigure[3, 0] = 80; MyFigure[3, 1] = 20; MyFigure[3, 2] = 1;
            MyFigure[4, 0] = -80; MyFigure[4, 1] = 80; MyFigure[4, 2] = 1;
        }


        /// <summary>
        /// Инициализация велосипеда в начале координат. 
        /// </summary>
        private void InitVelo()
        {


            // Левое колесо
            Velo.Add(new double[4, 3]);

            Velo[0][0, 0] = -100; Velo[0][0, 1] = -60; Velo[0][0, 2] = 1;
            Velo[0][1, 0] = -40; Velo[0][1, 1] = -60; Velo[0][1, 2] = 1;
            Velo[0][2, 0] = -40; Velo[0][2, 1] = 0; Velo[0][2, 2] = 1;
            Velo[0][3, 0] = -100; Velo[0][3, 1] = 0; Velo[0][3, 2] = 1;



            // Правое колесо
            Velo.Add(new double[4, 3]);

            Velo[1][0, 0] = 40; Velo[1][0, 1] = -60; Velo[1][0, 2] = 1;
            Velo[1][1, 0] = 100; Velo[1][1, 1] = -60; Velo[1][1, 2] = 1;
            Velo[1][2, 0] = 100; Velo[1][2, 1] = 0; Velo[1][2, 2] = 1;
            Velo[1][3, 0] = 40; Velo[1][3, 1] = 0; Velo[1][3, 2] = 1;

            // Корпус 
            Velo.Add(new double[4, 3]);

            Velo[2][0, 0] = -70; Velo[2][0, 1] = -30; Velo[2][0, 2] = 1;
            Velo[2][1, 0] = -70; Velo[2][1, 1] = -100; Velo[2][1, 2] = 1;
            Velo[2][2, 0] = 70; Velo[2][2, 1] = -100; Velo[2][2, 2] = 1;
            Velo[2][3, 0] = 70; Velo[2][3, 1] = -30; Velo[2][3, 2] = 1;


            // Педали 
            Velo.Add(new double[8, 3]);


            Velo[3][0, 0] = 0; Velo[3][0, 1] = -15; Velo[3][0, 2] = 1;

            Velo[3][0, 0] = 15; Velo[3][0, 1] = -45; Velo[3][0, 2] = 1;
            Velo[3][1, 0] = -15; Velo[3][1, 1] = -45; Velo[3][1, 2] = 1;

            Velo[3][2, 0] = 0; Velo[3][2, 1] = -45; Velo[3][2, 2] = 1;
            Velo[3][3, 0] = 0; Velo[3][3, 1] = -15; Velo[3][3, 2] = 1;

            Velo[3][4, 0] = -15; Velo[3][4, 1] = -15; Velo[3][4, 2] = 1;
            Velo[3][5, 0] = 15; Velo[3][5, 1] = -15; Velo[3][5, 2] = 1;

            Velo[3][6, 0] = 0; Velo[3][6, 1] = -15; Velo[3][6, 2] = 1;
            Velo[3][7, 0] = 0; Velo[3][7, 1] = -45; Velo[3][7, 2] = 1;




            //Velo[3][5, 0] = 0; Velo[3][5, 1] = -45; Velo[3][5, 2] = 1;
            //Velo[3][2, 0] = -15; Velo[3][2, 1] = -15; Velo[3][2, 2] = 1;

            ////Velo[3][2, 0] = 0; Velo[3][2, 1] = -45; Velo[3][2, 2] = 1;

            //Velo[3][2, 0] = -15; Velo[3][2, 1] = -15; Velo[3][2, 2] = 1;
            //Velo[3][3, 0] = 30; Velo[3][3, 1] = -15; Velo[3][3, 2] = 1;
            //Velo[3][4, 0] = -15; Velo[3][4, 1] = -45; Velo[3][4, 2] = 1;
            //Velo[3][5, 0] = 30; Velo[3][5, 1] = -45; Velo[3][5, 2] = 1;



            //Velo[3][0, 0] = 0; Velo[3][0, 1] = -15; Velo[3][0, 2] = 1;
            //Velo[3][1, 0] = 0; Velo[3][1, 1] = -45; Velo[3][1, 2] = 1;
            //Velo[3][2, 0] = -15; Velo[3][2, 1] = -15; Velo[3][2, 2] = 1;
            //Velo[3][3, 0] = 30; Velo[3][3, 1] = -15; Velo[3][3, 2] = 1;
            //Velo[3][4, 0] = -15; Velo[3][4, 1] = -45; Velo[3][4, 2] = 1;
            //Velo[3][5, 0] = 30; Velo[3][5, 1] = -45; Velo[3][5, 2] = 1;


        }


        ///// <summary>
        ///// Инициализация матрицы сдвига
        ///// </summary>
        ///// <param name="k1">Сдвиг по оси ОХ</param>
        ///// <param name="l1">Сдвиг по оси ОУ</param>
        //private void InitMoveMatrix(double k1, double l1)
        //{
        //    MoveMatrix[0, 0] = 1; MoveMatrix[0, 1] = 0; MoveMatrix[0, 2] = 0;
        //    MoveMatrix[1, 0] = 0; MoveMatrix[1, 1] = 1; MoveMatrix[1, 2] = 0;
        //    MoveMatrix[2, 0] = k1; MoveMatrix[2, 1] = l1; MoveMatrix[2, 2] = 1;
        //}

        /// <summary>
        /// Инициализация матрицы осей
        /// </summary>
        private void Init_osi()
        {
            osi[0, 0] = -200; osi[0, 1] = 0; osi[0, 2] = 1;
            osi[1, 0] = 200; osi[1, 1] = 0; osi[1, 2] = 1;
            osi[2, 0] = 0; osi[2, 1] = 200; osi[2, 2] = 1;
            osi[3, 0] = 0; osi[3, 1] = -200; osi[3, 2] = 1;

        }


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
        /// Вывод квадрата на экран
        /// </summary>
        private void Draw_Kv()
        {

            Init_kvadrat(); //инициализация матрицы тела
            //InitMoveMatrix(k, l); //инициализация матрицы преобразования
            double[,] kv1 = MatrixMultiplication(kv, MoveMatrix); //перемножение матриц

            //int[,] kv1 = Array.ConvertAll(kv2, new Converter<double, int>(kv2));
            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина
            
            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            
            // рисуем 1 сторону квадрата
            g.DrawLine(myPen, (int)kv1[0, 0], (int)kv1[0, 1], (int)kv1[1, 0], (int)kv1[1, 1]);
            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, (int)kv1[1, 0], (int)kv1[1, 1], (int)kv1[2, 0], (int)kv1[2, 1]);
            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, (int)kv1[2, 0], (int)kv1[2, 1], (int)kv1[3, 0], (int)kv1[3, 1]);
            // рисуем 4 сторону квадрата
            g.DrawLine(myPen, (int)kv1[3, 0], (int)kv1[3, 1], (int)kv1[0, 0], (int)kv1[0, 1]);
            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen

        }


        /// <summary>
        /// Рисует фигуру
        /// Переносит уже преобразованную фигуру в центр picture box
        /// Рисует её
        /// Переносит обратно в начало системы координат
        /// </summary>
        private void DrawFigure(double[,] Figure)
        {

            // Перенос на нужное расстояние относительно начала координат
            //InitMoveMatrix(k, l); //инициализация матрицы преобразования
            //MyFigure = MatrixMultiplication(MyFigure, MoveMatrix); //перемножение матриц

            // Перенос фигуры  в центр picture box
            //InitMoveMatrix(pictureBox1.Width / 2, pictureBox1.Height / 2);
            //MyFigure = MatrixMultiplication(MyFigure, MoveMatrix);

            // Оба действия можно реализовать одной матрицей 
            //InitMoveMatrix(pictureBox1.Width / 2 +k, pictureBox1.Height / 2 +l);
            //MyFigure = MatrixMultiplication(MyFigure, MoveMatrix);


            // Перенос фигуры  в центр picture box 
            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;


            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                     m1, n1, 1);

            Figure = MatrixMultiplication(Figure, CoordTransformMatrix);


            // Рисует фигуру относительно центра picture box

            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            for (int i = 1; i < Figure.GetLength(0); i++)
            {
                g.DrawLine(myPen, (int)Figure[i - 1, 0], (int)Figure[i - 1, 1], (int)Figure[i, 0], (int)Figure[i, 1]);

            }


            g.DrawLine(myPen, (int)Figure[Figure.GetLength(0) - 1, 0], (int)Figure[Figure.GetLength(0) - 1, 1], (int)Figure[0, 0], (int)Figure[0, 1]);

            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen


            // Перенос обратно в начало координат
            //InitMoveMatrix(-pictureBox1.Width / 2, -pictureBox1.Height / 2);
            //MyFigure = MatrixMultiplication(MyFigure, MoveMatrix);


            m1 = -pictureBox1.Width / 2;
            n1 = -pictureBox1.Height / 2;
            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                     m1, n1, 1);
            Figure = MatrixMultiplication(Figure, CoordTransformMatrix);

        }

        /// <summary>
        /// Вывод осей на экран
        /// </summary>
        private void Draw_osi()
        {
            // Инициализация матрицы сдвига с заданными параметрами
            int m1 = pictureBox1.Width / 2;
            int n1 = pictureBox1.Height / 2;
            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                     m1, n1, 1);

            // Перенос осей в центр picture box
            double[,] osi1 = MatrixMultiplication(osi, CoordTransformMatrix);

            Pen myPen = new Pen(Color.Red, 1);// цвет линии и ширина
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем ось ОХ
            g.DrawLine(myPen, (int)osi1[0, 0], (int)osi1[0, 1], (int)osi1[1, 0], (int)osi1[1, 1]);
            // рисуем ось ОУ
            g.DrawLine(myPen, (int)osi1[2, 0], (int)osi1[2, 1], (int)osi1[3, 0], (int)osi1[3, 1]);
            g.Dispose();
            myPen.Dispose();
        }



        // Кнопка Нарисовать оси
        private void DrawAxisButton_Click(object sender, EventArgs e)
        {
            Init_osi();
            Draw_osi();
        }


        // Кнопка Нарисовать фигуру
        private void DrawFigureButton_Click(object sender, EventArgs e)
        {

            InitMyFigure();
            DrawFigure(MyFigure);

        }


        // Кнопка Сдвиг по оси ОХ вправо
        private void MoveXRight_Click(object sender, EventArgs e)
        {

            double Distance = double.Parse(MoveDistanceTextBox.Text);

            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                     Distance, 0, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);

        }


        // Кнопка Сдвиг по оси ОХ влево
        private void MoveXLeft_Click(object sender, EventArgs e)
        {

            double Distance = double.Parse(MoveDistanceTextBox.Text);

            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                    -Distance, 0, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);

        }


        // Кнопка Сдвиг по оси ОУ вверх
        private void MoveYUp_Click(object sender, EventArgs e)
        {

            double Distance = double.Parse(MoveDistanceTextBox.Text);

            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                     0, Distance, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);
        }


        // Кнопка Сдвиг по оси ОУ вниз
        private void MoveYDown_Click(object sender, EventArgs e)
        {

            double Distance = double.Parse(MoveDistanceTextBox.Text);

            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                     0, -Distance, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);
        }


        // Кнопка Поворот на заданный угол
        private void RotateButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Draw_osi();

            // Перевод угла из градусов в радианы для передачи в Math.Sin
            double Angle = double.Parse(AngleTextBox.Text);
            Angle = Angle * Math.PI / 180;

            // Поворот относительно начала координат
            //InitRotationMatrix(Angle);
            //MyFigure = MatrixMultiplication(MyFigure, RotationMatrix)

            InitCoordTransformMatrix(Math.Cos(Angle), Math.Sin(Angle), 0,
                                     -Math.Sin(Angle), Math.Cos(Angle), 0,
                                            0, 0, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);

        }


        // Кнопка Смещение по оси ОХ
        private void ScaleOXButton_Click(object sender, EventArgs e)
        {

            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(Distance, 0, 0,
                                     0, 1, 0,
                                     0, 0, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);

        }


        // Кнопка Смещение по оси ОУ
        private void ScaleOYButton_Click(object sender, EventArgs e)
        {
            double Distance = double.Parse(ScaleDistanceTextBox.Text);

            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(1, 0, 0,
                                     0, Distance, 0,
                                     0, 0, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);
        }


        // Кнопка Отражение относительно ОУ
        private void ReflectionYButton_Click(object sender, EventArgs e)
        {


            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(-1, 0, 0,
                                     0, 1, 0,
                                     0, 0, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);
        }


        // Кнопка Отражение относительно ОХ
        private void ReflectionXButton_Click(object sender, EventArgs e)
        {


            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(1, 0, 0,
                                     0, -1, 0,
                                     0, 0, 1);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            //DrawMyFigure();
            DrawFigure(MyFigure);


        }


        //Кнопка Масштабирование
        private void ScalingXYButton_Click(object sender, EventArgs e)
        {

            double s = double.Parse(ScalingParameterTextBox.Text);
            pictureBox1.Refresh();
            Draw_osi();

            InitCoordTransformMatrix(1, 0, 0,
                                     0, 1, 0,
                                     0, 0, s);

            MyFigure = MatrixMultiplication(MyFigure, CoordTransformMatrix);

            for (int i = 0; i < MyFigure.GetLength(0); i++)
            {
                MyFigure[i, 0] = MyFigure[i, 0] / s;
                MyFigure[i, 1] = MyFigure[i, 1] / s;
                MyFigure[i, 2] = 1;
            }

            //DrawMyFigure();
            DrawFigure(MyFigure);
        }


        // Кнопка Очистить
        private void ClearButton_Click(object sender, EventArgs e)
        {

            pictureBox1.Refresh();

        }


        // Событие, происходящее при тике таймера
        private void timer1_Tick(object sender, EventArgs e)
        {

            // Не разобрался с событиями.
            // Не понимаю, какие значения тут принимает sender и e и почему событие клика можно вызывать именно так.
            Style(sender, e);

            // это не работает. 
            //MoveXRight.Click?.Invoke();
            //DrawMyFigure();

            Thread.Sleep(100);
        }


        /// <summary>
        /// Переменная для старта и остановки таймера
        /// </summary>
        bool f = true;

        // Кнопка Старт
        private void StartButton_Click(object sender, EventArgs e)
        {

            // Время в милисекундах между тиками таймера
            timer1.Interval = 100;

            StartButton.Text = "Стоп";
            if (f == true)
                timer1.Start();
            else
            {
                timer1.Stop();
                StartButton.Text = "Старт";
            }
            f = !f;


        }


        private void AlgCheckListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Убирает множественный выбор

            // Текущий выбранный индекс
            int AlgIndex = AlgCheckListBox.SelectedIndex;

            // Убирает выбор с других элементов
            for (int i = 0; i < AlgCheckListBox.Items.Count; i++)
            {
                if (i != AlgIndex)
                    AlgCheckListBox.SetItemCheckState(i, CheckState.Unchecked);

            }


            // Передаёт в делегат текущий выбранный алгоритм
            switch (AlgCheckListBox.SelectedIndex)
            {
                case 0:

                    Style = MoveXRight_Click;
                    break;

                case 1:

                    Style = MoveXLeft_Click;
                    break;

                case 2:

                    Style = MoveYUp_Click;
                    break;

                case 3:

                    Style = MoveYDown_Click;
                    break;

                case 4:

                    Style = RotateButton_Click;
                    break;

                case 5:

                    Style = ScaleOXButton_Click;
                    break;

                case 6:
                    Style = ScaleOYButton_Click;
                    break;

                case 7:

                    Style = ScalingXYButton_Click;
                    break;

                case 8:
                    Style = MoveVeloButton_Click;
                    break;

            }


        }

        private void DrawVeloButton_Click(object sender, EventArgs e)
        {
            InitVelo();         

            for (int i = 0; i < Velo.Count; i++)
            {
                DrawFigure(Velo[i]);
            }

            // центр левого колеса
            //Velo[2][0, 0] = -70;

            // центр правого колеса 
            //Velo[2][3, 0] = 70;

            // x - The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.
            // y - The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.

            // width
            // Width of the bounding rectangle that defines the ellipse.

            // height
            // Height of the bounding rectangle that defines the ellipse.

            float x = pictureBox1.Width / 2 + float.Parse(Velo[2][0, 0].ToString())-40;
            float y = pictureBox1.Height / 2 + float.Parse(Velo[2][0, 1].ToString())-40;

            // Высота колеса = 40. т.е. от центра надо отступать 20. 

            //int Top = Velo[2].Max();

            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            
            g.DrawEllipse(myPen, x, y, 80, 80);


            x = pictureBox1.Width / 2 + float.Parse(Velo[2][3, 0].ToString()) - 40;
            y = pictureBox1.Height / 2 + float.Parse(Velo[2][3, 1].ToString()) - 40;

            g.DrawEllipse(myPen, x, y, 80, 80);


            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }

        double a = 0;

        private void MoveVeloButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Draw_osi();

            // 0 1 колёса. Вращать и двигать
            // 2 корпус двигать
            // 3 педали вращать и двигать

            double Angle = 30;
            Angle = Angle * Math.PI / 180;



            // Правое колесо

            // Центр правого колеса
            double[] RightWheelXCoord = { Velo[0][0, 0], Velo[0][1, 0], Velo[0][2, 0], Velo[0][3, 0] };
            double center1 = RightWheelXCoord.Max() - RightWheelXCoord.Min();

            // должно быть именно в таком порядке: перенос, поворот
            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    5, 0, 1);
            a += 5;
            Velo[0] = MatrixMultiplication(Velo[0], CoordTransformMatrix);


            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    70-a, 30, 1);
            Velo[0] = MatrixMultiplication(Velo[0], CoordTransformMatrix);

            InitCoordTransformMatrix(Math.Cos(Angle), Math.Sin(Angle), 0,
                                     -Math.Sin(Angle), Math.Cos(Angle), 0,
                                            0, 0, 1);
            Velo[0] = MatrixMultiplication(Velo[0], CoordTransformMatrix);

            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    -70+a, -30, 1);
            Velo[0] = MatrixMultiplication(Velo[0], CoordTransformMatrix);



            // Левое колесо

            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    5, 0, 1);

            Velo[1] = MatrixMultiplication(Velo[1], CoordTransformMatrix);

            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    -70-a, 30, 1);
            Velo[1] = MatrixMultiplication(Velo[1], CoordTransformMatrix);

            InitCoordTransformMatrix(Math.Cos(Angle), Math.Sin(Angle), 0,
                                     -Math.Sin(Angle), Math.Cos(Angle), 0,
                                            0, 0, 1);
            Velo[1] = MatrixMultiplication(Velo[1], CoordTransformMatrix);

            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    70+a, -30, 1);
            Velo[1] = MatrixMultiplication(Velo[1], CoordTransformMatrix);




            // Педали

            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    5, 0, 1);

            Velo[3] = MatrixMultiplication(Velo[3], CoordTransformMatrix);



            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    0-a, 30, 1);
            Velo[3] = MatrixMultiplication(Velo[3], CoordTransformMatrix);

            InitCoordTransformMatrix(Math.Cos(Angle), Math.Sin(Angle), 0,
                                     -Math.Sin(Angle), Math.Cos(Angle), 0,
                                            0, 0, 1);
            Velo[3] = MatrixMultiplication(Velo[3], CoordTransformMatrix);

            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    0+a, -30, 1);
            Velo[3] = MatrixMultiplication(Velo[3], CoordTransformMatrix);


            InitCoordTransformMatrix(1, 0, 0,
                                    0, 1, 0,
                                    5, 0, 1);
            Velo[2] = MatrixMultiplication(Velo[2], CoordTransformMatrix);




            for (int i = 0; i < Velo.Count; i++)
            {
                DrawFigure(Velo[i]);
            }

            float x = pictureBox1.Width / 2 + float.Parse(Velo[2][0, 0].ToString()) - 40;
            float y = pictureBox1.Height / 2 + float.Parse(Velo[2][0, 1].ToString()) - 40;

            // Высота колеса = 40. т.е. от центра надо отступать 20. 

            //int Top = Velo[2].Max();

            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawEllipse(myPen, x, y, 80, 80);


            x = pictureBox1.Width / 2 + float.Parse(Velo[2][3, 0].ToString()) - 40;
            y = pictureBox1.Height / 2 + float.Parse(Velo[2][3, 1].ToString()) - 40;

            g.DrawEllipse(myPen, x, y, 80, 80);


            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen



        }




    }
}
