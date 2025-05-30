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
using CG.View.Forms;
using CG.View.Forms.Diagram;
using CG.View.Forms.Lab2;
using CG.View.Forms.Lab3;
using CG.View.Forms.Lab4;
using static System.Windows.Forms.DataFormats;


namespace CG.View
{

    /// <summary>
    /// Главная вкладка
    /// </summary>
    public partial class MainTab : UserControl
    {


        public MainTab()
        {
            InitializeComponent();
        }



        private void Lab1Button_Click(object sender, EventArgs e)
        {
            var NewForm = new Lab1Form();
            //NewForm.Show();


            if (NewForm.ShowDialog() != DialogResult.OK)
            {
                return;

            }
        }

        private void Lab2Button_Click(object sender, EventArgs e)
        {
            var NewForm = new Lab2Form();

            if (NewForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }


        private void Lab3Button_Click(object sender, EventArgs e)
        {
            var NewForm = new Lab3Form();

            if (NewForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }


        private void Lab4Button_Click(object sender, EventArgs e)
        {

            var NewForm = new Lab4Form();

            if (NewForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

        }


        private void DiagramFormButton_Click(object sender, EventArgs e)
        {
            var NewForm = new DiagramForm();

            if (NewForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }
    }
}


