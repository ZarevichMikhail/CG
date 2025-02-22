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
using KG.View.Forms;
using static System.Windows.Forms.DataFormats;


namespace KG.View
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
    }
}


