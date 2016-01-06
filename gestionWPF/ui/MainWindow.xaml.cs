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
    
    public class MyGradientBrush
    {
        
        static MyGradientBrush()
        {

        }

        public static Brush getBackgroundBrush()
        {
            Color startColor = Color.FromRgb(218, 236, 250);
            Color endColor = Color.FromRgb(24, 106, 166);

            return new LinearGradientBrush(startColor, endColor, 90);
        }

        public static Brush getPanelBrush()
        {
            Color startColor = Color.FromRgb(50, 70, 140);
            Color endColor = Color.FromRgb(25, 35, 70);

            return new LinearGradientBrush(startColor, endColor, 90);

        }

    }
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static TextBox tbCodeEmploye;

        private SessionEmploye sess_emp;

        public MainWindow()
        {
            InitializeComponent();
            
            sess_emp = new SessionEmploye();

            MainWindow.tbCodeEmploye.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NotifierEmployeActuel);
            
        }

        static MainWindow()
        {
            MainWindow.tbCodeEmploye = new TextBox();
            
        }

        public static string CodeEmploye
        {
            get { return MainWindow.tbCodeEmploye.Text; }
            set { MainWindow.tbCodeEmploye.Text = value; }
        }
        
        private void AppWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //sess_emp.VerifierDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NotifierEmployeActuel(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.CodeEmploye.Length > 0)
                {
                    Employe emp = sess_emp.EmployePourModification(MainWindow.CodeEmploye);

                    this.RemplirEmploye(emp);
                }

                this.ToggleMenuEmploye();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemplirEmploye(Employe emp)
        {
            StatutCarriere stc = emp.getStatutActuel();

            this.lbCodeEmploye.Content = emp.CodeEmploye;
            this.lbNom.Content = emp.Nom;
            this.lbPrenom.Content = emp.Prenom;
            this.lbSexe.Content = emp.Sexe;

            this.lbDepartement.Content = stc.getDepartement().Name;
            this.lbPoste.Content = stc.getPoste().Name + " / " + stc.getGrade().Name;
        }

        private void ToggleMenuEmploye()
        {
            if (MainWindow.CodeEmploye.Length > 0)
            {
                this.GridEmploye.Visibility = Visibility.Visible;

                this.miModifierEmploye.IsEnabled = true;
                this.miPromotionEmploye.IsEnabled = true;
                this.miTransfertEmploye.IsEnabled = true;
                this.miRevocationEmploye.IsEnabled = true;
                this.miDemandeConge.IsEnabled = true;
                this.miRetourConge.IsEnabled = true;
            }
            else
            {
                this.GridEmploye.Visibility = Visibility.Collapsed;

                this.miModifierEmploye.IsEnabled = false;
                this.miPromotionEmploye.IsEnabled = false;
                this.miTransfertEmploye.IsEnabled = false;
                this.miRevocationEmploye.IsEnabled = false;
                this.miDemandeConge.IsEnabled = false;
                this.miRetourConge.IsEnabled = false;
            }
        }

        
        private void EmbaucherEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                mdiChild.Content = new FenEmploye(this.MainMdiContainer, "embaucher");
                mdiChild.Title = "Embaucher un Employé";
                mdiChild.Height = 630;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void miRechercheCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                mdiChild.Content = new FenRechercherEmploye(this, this.MainMdiContainer, TargetRecherche.Code);
                mdiChild.Title = "Recherche d'Employés";
                mdiChild.Height = 650;
                mdiChild.Width = 1000;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miRechercheNom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                mdiChild.Content = new FenRechercherEmploye(this, this.MainMdiContainer, TargetRecherche.Nom);
                mdiChild.Title = "Recherche d'Employés";
                mdiChild.Height = 650;
                mdiChild.Width = 1000;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void miEmployeStatut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                mdiChild.Content = 
                    new FenRapport(this.MainMdiContainer, "Liste des Employés avec leur Statut", sess_emp.EmployeAvecStatut());
                mdiChild.Title = "Rapport d'Employés";
                mdiChild.Height = 650;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miComptageEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                mdiChild.Content =
                    new FenRapport(this.MainMdiContainer, "Nombre d'Employés par Département", sess_emp.ComptageEmployeParDepartement());
                mdiChild.Title = "Rapport d'Employés";
                mdiChild.Height = 500;
                mdiChild.Width = 400;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miContactEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                mdiChild.Content =
                    new FenRapport(this.MainMdiContainer, "Contact des Employés", sess_emp.ContactEmploye());
                mdiChild.Title = "Rapport d'Employés";
                mdiChild.Height = 600;
                mdiChild.Width = 400;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miEmplois_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                mdiChild.Content =
                    new FenRapport(this.MainMdiContainer, "Emplois de l'Hôtel", sess_emp.Emplois());
                mdiChild.Title = "Rapport d'Employés";
                mdiChild.Height = 600;
                mdiChild.Width = 400;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miModifierEmploye_Click(object sender, RoutedEventArgs e)
        {
            this.ModifierEmploye(MainWindow.CodeEmploye);
        }

        public void ModifierEmploye(string code_emp)
        {
            try
            {
                Employe emp = sess_emp.EmployePourModification(code_emp);

                MdiChild mdiChild = new MdiChild();

                mdiChild.Content = new FenEmploye(this.MainMdiContainer, "modifier", emp);
                mdiChild.Title = "Modifier Employé";
                mdiChild.Height = 630;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miPromotionEmploye_Click(object sender, RoutedEventArgs e)
        {
            this.PromotionEmploye(MainWindow.CodeEmploye);
        }

        public void PromotionEmploye(string code_emp)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                Employe emp = sess_emp.EmployePourModification(code_emp);

                mdiChild.Content = new FenCarriere(this.MainMdiContainer, emp, CarriereMotif.Promotion);
                mdiChild.Title = "Promotion d'un Employé";
                mdiChild.Height = 600;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miTransfertEmploye_Click(object sender, RoutedEventArgs e)
        {
            this.TransfertEmploye(MainWindow.CodeEmploye);
        }

        public void TransfertEmploye(string code_emp)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                Employe emp = sess_emp.EmployePourModification(code_emp);

                mdiChild.Content = new FenCarriere(this.MainMdiContainer, emp, CarriereMotif.Transfert);
                mdiChild.Title = "Transfert d'un Employé";
                mdiChild.Height = 600;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miRevocationEmploye_Click(object sender, RoutedEventArgs e)
        {
            this.RevocationEmploye(MainWindow.CodeEmploye);
        }

        public void RevocationEmploye(string code_emp)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                Employe emp = sess_emp.EmployePourModification(code_emp);

                mdiChild.Content = new FenCarriere(this.MainMdiContainer, emp, CarriereMotif.Revocation);
                mdiChild.Title = "Révocation d'un Employé";
                mdiChild.Height = 600;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miDemandeConge_Click(object sender, RoutedEventArgs e)
        {
            this.DemandeConge(MainWindow.CodeEmploye);
        }

        public void DemandeConge(string code_emp)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                Employe emp = sess_emp.EmployePourModification(code_emp);

                mdiChild.Content = new FenConge(this.MainMdiContainer, emp, "Demande");
                mdiChild.Title = "Validation de Demande de Congé";
                mdiChild.Height = 600;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void miRetourConge_Click(object sender, RoutedEventArgs e)
        {
            this.RetourConge(MainWindow.CodeEmploye);
        }

        public void RetourConge(string code_emp)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                Employe emp = sess_emp.EmployePourModification(code_emp);

                mdiChild.Content = new FenConge(this.MainMdiContainer, emp, "Retour");
                mdiChild.Title = "Validation de Retour de Congé";
                mdiChild.Height = 600;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miRestaurerDatabase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

                dialog.DefaultExt = ".bak";
                dialog.Filter = "BAK Files (*.bak)|*.bak";

                Nullable<bool> result = dialog.ShowDialog();

                if (result == true)
                {
                    string fileName = dialog.FileName;
                    //MessageBox.Show(fileName);
                    sess_emp.RestaurerDatabase(fileName);
                    MessageBox.Show("La Base de Données a été restaurée!");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miBackupDatabase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sess_emp.BackupDatabase();
                MessageBox.Show("La Base de Données a été sauvergardée en copie!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void miCalculerPayroll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                Payroll pay = sess_emp.GetPayroll();
                mdiChild.Content = 
                    new FenRapport(this.MainMdiContainer, pay);
                mdiChild.Title = "Rapport d'Employés";
                mdiChild.Height = 650;
                mdiChild.Width = 900;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void miPresenceJour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                DateTime date = DateTime.Now;
                string dateString = date.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("fr-fr"));
                string titre = "Présence du " + dateString;
                DataTable dt = sess_emp.PresenceJournaliere(date);

                mdiChild.Content = new FenRapport(this.MainMdiContainer, titre, dt);
                mdiChild.Title = "Présence du Mois";
                mdiChild.Height = 650;
                mdiChild.Width = 700;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miPresenceMois_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                DateTime date = DateTime.Now;
                string dateString = date.ToString("MMMM yyyy", new System.Globalization.CultureInfo("fr-fr"));
                string titre = "Présence de " + dateString;
                DataTable dt = sess_emp.PresenceMensuelle(date);

                mdiChild.Content = new FenRapport(this.MainMdiContainer, titre, dt);
                mdiChild.Title = "Présence du Mois";
                mdiChild.Height = 650;
                mdiChild.Width = 700;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miPresenceAnnee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;
                
                // 1ere Fenetre - Absences Annuelles
                MdiChild mdiChild1 = new MdiChild();
                string titre = "Absences de l'Année " + date.Year;
                DataTable dt = sess_emp.AbsenceAnnuelle(date);
                mdiChild1.Content = new FenRapport(this.MainMdiContainer, titre, dt);
                mdiChild1.Title = "Absences de l'Année";
                mdiChild1.Position = new Point(0, 0);
                mdiChild1.Height = 650;
                mdiChild1.Width = 700;
                mdiChild1.Background = MyGradientBrush.getBackgroundBrush();
                this.MainMdiContainer.Children.Add(mdiChild1);
                

                // 2eme Fenetre - Retards Annuels
                MdiChild mdiChild2 = new MdiChild();
                titre = "Retards de l'Année " + date.Year;
                dt = sess_emp.RetardAnnuel(date);
                mdiChild2.Content = new FenRapport(this.MainMdiContainer, titre, dt);
                mdiChild2.Title = "Retards de l'Année";
                mdiChild2.Position = new Point(510, 0);
                mdiChild2.Height = 650;
                mdiChild2.Width = 700;
                mdiChild2.Background = MyGradientBrush.getBackgroundBrush();
                this.MainMdiContainer.Children.Add(mdiChild2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void miAnniversaire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MdiChild mdiChild = new MdiChild();

                DataTable dt = sess_emp.AnniversaireAVenir();
                dt.Columns.Add("Jour").DataType = ("string").GetType();
                dt.Columns["Jour"].SetOrdinal(0);

                foreach (DataRow dr in dt.Rows)
                {
                    DateTime date = DateTime.Parse(dr["DateNaissance"].ToString());
                    string dateString = date.ToString("dd MMMM", new System.Globalization.CultureInfo("fr-fr"));
                    dr["Jour"] = dateString;
                }

                dt.Columns.Remove("DateNaissance");

                mdiChild.Content =
                    new FenRapport(this.MainMdiContainer, "Anniversaires Proches", dt);
                mdiChild.Title = "Rapport d'Employés";
                mdiChild.Height = 400;
                mdiChild.Width = 600;
                mdiChild.Background = MyGradientBrush.getBackgroundBrush();

                this.MainMdiContainer.Children.Add(mdiChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

       
       
    }
}
