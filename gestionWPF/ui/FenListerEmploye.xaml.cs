using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using com.levivoir.rh.domaine;
using com.levivoir.rh.application;

namespace com.levivoir.rh.ui
{
    /// <summary>
    /// Interaction logic for FenListerEmploye.xaml
    /// </summary>
    public partial class FenListerEmploye : Window
    {
        private SessionEmploye sess;

        public FenListerEmploye()
        {
            InitializeComponent();
            sess = new SessionEmploye();
        }


        public void Lister()
        {
            try
            {
                //Set ItemsSource of Employe DataGrid to List<Employe>
                //this.dgEmploye
                this.dgEmploye.ItemsSource = sess.All();
                this.dgEmploye.Height = 350;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
