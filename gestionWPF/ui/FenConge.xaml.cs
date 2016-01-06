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
    /// <summary>
    /// Interaction logic for FenConge.xaml
    /// </summary>
    public partial class FenConge : UserControl
    {
        private SessionConge sess_cong;
        private MdiContainer mdiContainer;
        private int code_conge;
        private string code_employe;
        
        public FenConge(MdiContainer mct, Employe emp, string operation)
        {
            InitializeComponent();
            sess_cong = new SessionConge();
            mdiContainer = mct;
            code_employe = emp.CodeEmploye;

            this.VerifierStatutEmploye(emp);
            this.InitialiserCbo();
            this.PreparerInfo(emp, operation);
            this.RemplirEmploye(emp, operation);
        }

        private void VerifierStatutEmploye(Employe emp)
        {
            if (emp.EstRevoque())
            {
                MessageBox.Show("Cet Employe est revoqué!");
                this.IsEnabled = false;
            }
        }

        private void InitialiserCbo()
        {
            this.cboType.ItemsSource = null;
            
            DataTable dt;

            //Setup Type
            dt = new DataTable("Type");
            dt.Columns.Add("Code");
            dt.Columns.Add("Expression");
            dt.Rows.Add("Annuel", "Annuel");
            dt.Rows.Add("Maladie", "Maladie");
            dt.Rows.Add("Maternite", "Maternite");

            this.cboType.ItemsSource = dt.DefaultView;
            this.cboType.SelectedValuePath = "Code";
            this.cboType.DisplayMemberPath = "Expression";
            this.cboType.SelectedItem = null;
        }

        private void PreparerInfo(Employe emp, string operation)
        {
            try
            {
                this.tbAnnee.Text = sess_cong.TrouverAnneeFiscaleActuelle().ToString();
                this.cboType.SelectedValue = "Annuel";

                switch (operation)
                {
                    case "Demande":
                        if (emp.getConges().Count > 0 && !emp.dernierRetourEffectifValide())
                        {
                            MessageBox.Show("Le dernier Retour de Congé n'a pas été validé!");
                            this.cboType.IsEnabled = false;
                            this.dpDebut.IsEnabled = false;
                            this.dpRetourPrevu.IsEnabled = false;
                            this.dpRetourEffectif.IsEnabled = false;
                        }
                        else
                        {
                            this.dpRetourEffectif.IsEnabled = false;
                            this.btnValiderDemande.Visibility = Visibility.Visible;
                        }
                        break;

                    case "Retour":
                        
                        if (emp.getCongeOuvert() == null)
                        {
                            MessageBox.Show("L'Employé n'est pas actuellement en Congé!");
                            this.cboType.IsEnabled = false;
                            this.dpDebut.IsEnabled = false;
                            this.dpRetourPrevu.IsEnabled = false;
                            this.dpRetourEffectif.IsEnabled = false;
                        }
                        else
                        {
                            this.cboType.IsEnabled = false;
                            this.dpDebut.IsEnabled = false;
                            this.dpRetourPrevu.IsEnabled = false;
                            this.dpRetourEffectif.Visibility = Visibility.Visible;

                            this.btnValiderRetour.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemplirEmploye(Employe emp, string operation)
        {
            this.lbEspaceEmploye.Content = operation + " de Congé de l'Employé : (" + emp.CodeEmploye + ") " + emp.Prenom + " " + emp.Nom;
            try
            {
                Conge c = emp.getCongeOuvert();
                if (c != null)
                {
                    //Set code_conge of this Window so we can update RetourEffectif
                    code_conge = c.CodeConge;

                    this.cboType.SelectedValue = c.Type;
                    this.tbAnnee.Text = c.AnneeFiscale.ToString();
                    this.dpDebut.SelectedDate = c.Debut;
                    this.dpRetourPrevu.SelectedDate = c.RetourPrevu;
                }

                this.dgConge.ItemsSource = emp.getConges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        


        private void btnValiderDemande_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sess_cong.Inserer(code_employe, (string)cboType.SelectedValue.ToString(), int.Parse(tbAnnee.Text), dpDebut.ToString(),
                        dpRetourPrevu.ToString());

                MessageBox.Show("Congé enregistré!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnValiderRetour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sess_cong.ValiderRetourEffectif(this.code_conge, dpRetourEffectif.ToString());

                MessageBox.Show("Retour de Congé validé!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
    }
}
