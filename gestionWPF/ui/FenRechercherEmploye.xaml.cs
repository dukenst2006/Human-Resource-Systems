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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using com.levivoir.rh.application;
using com.levivoir.rh.domaine;
using WPF.MDI;

namespace com.levivoir.rh.ui
{
    public enum TargetRecherche
    {
        Code = 0, Nom = 1, LesDeux = 2
    }
    

    /// <summary>
    /// Interaction logic for FenRechercherEmploye.xaml
    /// </summary>
    public partial class FenRechercherEmploye : UserControl
    {
        private SessionEmploye sess_emp;
        private MdiContainer mdiContainer;
        private MainWindow mainWindow;

        public FenRechercherEmploye(MainWindow mwin, MdiContainer mct, TargetRecherche target)
        {
            InitializeComponent();
            sess_emp = new SessionEmploye();
            mainWindow = mwin;
            mdiContainer = mwin.MainMdiContainer;

            if (target == TargetRecherche.Code) rbCode.IsChecked = true;
            if (target == TargetRecherche.Nom) rbNom.IsChecked = true;
            if (target == TargetRecherche.LesDeux) rbLesDeux.IsChecked = true;
        }

        private DataRow getSelectedDataRow()
        {
            //Retrieve the selectected DGV Cell
            DataGridCellInfo dgci = this.dgEmploye.SelectedCells[0];

            //Retrieve the the DGV Row
            DataRowView drv = (DataRowView) dgEmploye.SelectedItem;

            return drv.Row;
            
        }

        private void btnRechercher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.lbResultatRecherche.Content = "Recherche en Cours...";

                TimeSpan ts = new TimeSpan();
                DateTime start = DateTime.Now;
                DateTime stop;
                
                DataTable dt = new DataTable();
                if(rbCode.IsChecked == true) dt = sess_emp.Rechercher(tbMotsCles.Text, (int)TargetRecherche.Code);
                else if (rbNom.IsChecked == true) dt = sess_emp.Rechercher(tbMotsCles.Text, (int)TargetRecherche.Nom);
                else if (rbLesDeux.IsChecked == true) dt = sess_emp.Rechercher(tbMotsCles.Text, (int)TargetRecherche.LesDeux);

                stop = DateTime.Now;
                ts = new TimeSpan(stop.Ticks - start.Ticks);
                this.lbResultatRecherche.Content = "Total Employés : " + dt.Rows.Count + ". Temps de recherche : " + (double)ts.Milliseconds/1000 + "sec(s).";

                this.dgEmploye.ItemsSource = dt.DefaultView;
                this.dgEmploye.Height = 350;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedCells.Count > 0)
            {
                //Retrieve the the DG Row
                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;
                DataRow dr = drv.Row;

                this.mainWindow.ModifierEmploye(dr["CodeEmploye"].ToString());
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Employé!");
            }
        }

        private void btnTransfert_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedCells.Count > 0)
            {
                MdiChild mc = new MdiChild();

                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;
                DataRow dr = drv.Row;

                this.mainWindow.TransfertEmploye(dr["CodeEmploye"].ToString());
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Employé!");
            }
        }

        private void btnPromotion_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedCells.Count > 0)
            {
                MdiChild mc = new MdiChild();

                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;
                DataRow dr = drv.Row;

                this.mainWindow.PromotionEmploye(dr["CodeEmploye"].ToString());
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Employé!");
            }
        }

        private void btnDemandeConge_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedCells.Count > 0)
            {
                MdiChild mc = new MdiChild();

                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;
                DataRow dr = drv.Row;

                this.mainWindow.DemandeConge(dr["CodeEmploye"].ToString());
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Employé!");
            }
        }

        private void btnRetourConge_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedCells.Count > 0)
            {
                MdiChild mc = new MdiChild();

                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;
                DataRow dr = drv.Row;

                this.mainWindow.RetourConge(dr["CodeEmploye"].ToString());
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Employé!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            foreach (MdiChild mch in this.mdiContainer.Children)
            {
                if (mch.Content.Equals(this))
                {
                    mch.Close();
                    break;
                }
            }
        }

        private void dgEmploye_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                MdiChild mc = new MdiChild();

                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;
                //DataRow dr = (DataRow)dgEmploye.SelectedItem;
                
                MainWindow.CodeEmploye = drv["CodeEmploye"].ToString();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedCells.Count > 0)
            {
                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;

                if (MessageBox.Show("Voulez-vous vraiment éliminer toute trace de l'Employé?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        sess_emp.Eliminer(drv["CodeEmploye"].ToString());
                        MessageBox.Show("l'Employé a été enlevé du Système");

                        //Relancer la Recherche
                        //btnRechercher_Click(sender, e);
                        //Enlever la ligne de l'Employé
                        drv.Row.Delete();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Employé!");
            }
        }

        private void btnRevocation_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmploye.SelectedCells.Count > 0)
            {
                MdiChild mc = new MdiChild();

                DataRowView drv = (DataRowView)dgEmploye.SelectedItem;

                DataRow dr = drv.Row;

                Employe emp = sess_emp.EmployePourModification(dr["CodeEmploye"].ToString());

                mc.Content = new FenCarriere(mdiContainer, emp, CarriereMotif.Revocation);
                mc.Title = "Révocation d'un Employé";
                mc.Height = 600;
                mc.Width = 900;

                mdiContainer.Children.Add(mc);
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un Employé!");
            }
        }

        private void dgEmploye_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnEdit_Click(btnEdit, new RoutedEventArgs());
        }

        private void tbMotsCles_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.btnRechercher_Click(sender, new RoutedEventArgs());
        }

        
       
    }
}
