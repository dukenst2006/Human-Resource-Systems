using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using com.levivoir.rh.domaine;
using WPF.MDI;

namespace com.levivoir.rh.ui
{
    /// <summary>
    /// Interaction logic for FenRapport.xaml
    /// </summary>
    public partial class FenRapport : UserControl
    {
        private MdiContainer mdiContainer;

        public FenRapport(MdiContainer mct, string title, DataTable dt)
        {
            InitializeComponent();
            this.mdiContainer = mct;
            this.lbTitle.Content = title;

            this.dgRapport.ItemsSource = dt.DefaultView;

        }

        public FenRapport(MdiContainer mct, Payroll payroll)
        {
            InitializeComponent();
            this.mdiContainer = mct;

            this.lbTitle.Content = "Payroll de : " + payroll.Periode;

            this.dgRapport.ItemsSource = payroll.Paiements;
        }

        


    }
}
