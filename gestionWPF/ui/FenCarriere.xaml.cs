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
    public enum CarriereMotif
    {
        Promotion = 0, Transfert = 1, Revocation = 2
    }

    /// <summary>
    /// Interaction logic for FenCarriere.xaml
    /// </summary>
    public partial class FenCarriere : UserControl
    {
        private SessionEmploye sess_emp;
        private SessionCarriere sess_car;
        private string code_emp;
        private MdiContainer mdiContainer;

        public FenCarriere(MdiContainer mct, Employe emp, CarriereMotif motif)
        {
            InitializeComponent();
            sess_emp = new SessionEmploye();
            sess_car = new SessionCarriere();
            mdiContainer = mct;
            code_emp = emp.CodeEmploye;

            this.VerifierStatutEmploye(emp);
            this.InitialiserCbo();
            this.PreparerInfo(emp, motif);
            this.RemplirEmploye(emp, motif);
            
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
            this.cboDepartement.ItemsSource = null;
            this.cboGrade.ItemsSource = null;
            this.cboPoste.ItemsSource = null;
            this.cboMotifStatut.ItemsSource = null;

            DataTable dt;

            //Setup MotifStatut
            dt = new DataTable("MotifStatut");
            dt.Columns.Add("Code");
            dt.Columns.Add("Expression");
            dt.Rows.Add("Nomination", "Nomination");
            dt.Rows.Add("Promotion", "Promotion");
            dt.Rows.Add("Transfert", "Transfert");

            this.cboMotifStatut.ItemsSource = dt.DefaultView;
            this.cboMotifStatut.SelectedValuePath = "Code";
            this.cboMotifStatut.DisplayMemberPath = "Expression";
            this.cboMotifStatut.SelectedItem = null;

            //Setup Departement
            dt = sess_emp.AllDepartements();
            this.cboDepartement.ItemsSource = dt.DefaultView;
            this.cboDepartement.SelectedValuePath = "CodeDepartement";
            this.cboDepartement.DisplayMemberPath = "libDepartement";
            this.cboDepartement.SelectedIndex = -1;

            //Setup Grade
            dt = sess_emp.AllGrades();
            this.cboGrade.ItemsSource = dt.DefaultView;
            this.cboGrade.SelectedValuePath = "CodeGrade";
            this.cboGrade.DisplayMemberPath = "libGrade";
            this.cboGrade.SelectedIndex = -1;

            //Setup Poste
            dt = sess_emp.AllPostes();
            this.cboPoste.ItemsSource = dt.DefaultView;
            this.cboPoste.SelectedValuePath = "CodePoste";
            this.cboPoste.DisplayMemberPath = "libPoste";
            this.cboPoste.SelectedIndex = -1;
        }


        private void PreparerInfo(Employe emp, CarriereMotif motif)
        {
            switch (motif)
            {
                case CarriereMotif.Promotion :
                    this.cboMotifStatut.SelectedValue = "Promotion";
                    this.cboMotifStatut.IsEnabled = false;
                    this.btnValiderPromotion.Visibility = Visibility.Visible;
                    break;
                    
                case CarriereMotif.Transfert:
                    StatutCarriere stc = emp.getStatutActuel();
                    this.cboMotifStatut.SelectedValue = "Transfert";
                    this.cboMotifStatut.IsEnabled = false;
                    //Garde le meme Grade
                    this.cboGrade.SelectedValue = stc.CodeGrade;
                    this.cboGrade.IsEnabled = false;
                    //Garde le meme Poste
                    this.cboPoste.SelectedValue = stc.CodePoste;
                    this.cboPoste.IsEnabled = false;

                    this.btnValiderTransfert.Visibility = Visibility.Visible;
                    break;

                case CarriereMotif.Revocation:
                    this.lbFinStatut.Visibility = Visibility.Visible;
                    this.dpFinStatut.Visibility = Visibility.Visible;

                    this.cboDepartement.IsEnabled = false;
                    this.cboGrade.IsEnabled = false;
                    this.cboPoste.IsEnabled = false;
                    this.dpDebutStatut.IsEnabled = false;
                    this.cboMotifStatut.IsEnabled = false;

                    this.btnValiderRevocation.Visibility = Visibility.Visible;
                    break;
            }


        }

        private void RemplirEmploye(Employe emp, CarriereMotif motif)
        {
            this.lbEspaceEmploye.Content = motif + " de l'Employé : (" + emp.CodeEmploye + ") " + emp.Prenom + " " + emp.Nom;
            try
            {
                StatutCarriere stc = emp.getStatutActuel();

                this.lbDepartement.Content = stc.getDepartement().Name;
                this.lbGrade.Content = stc.getGrade().Name;
                this.lbPoste.Content = stc.getPoste().Name;
                this.lbDebut.Content = stc.Debut;
                this.lbMotif.Content = stc.Motif;

                this.dgCarriere.ItemsSource = emp.getStatutCarrieres();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        


        private void btnValiderPromotion_Click(object sender, RoutedEventArgs e)
        {
            string grade = null;
            if (cboGrade.SelectedIndex >= 0) grade = cboGrade.SelectedValue.ToString();

            string poste = null;
            if (cboPoste.SelectedIndex >= 0) poste = cboPoste.SelectedValue.ToString();

            try
            {
                sess_car.InsererStatutCarriere(code_emp, (string)cboDepartement.SelectedValue, grade, poste, dpDebutStatut.ToString(),
                        (string)cboMotifStatut.SelectedValue);

                MessageBox.Show("Promotion enregistrée!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnValiderTransfert_Click(object sender, RoutedEventArgs e)
        {
            string grade = null;
            if (cboGrade.SelectedIndex >= 0) grade = cboGrade.SelectedValue.ToString();

            string poste = null;
            if (cboPoste.SelectedIndex >= 0) poste = cboPoste.SelectedValue.ToString();

            try
            {
                sess_car.InsererStatutCarriere(code_emp, (string)cboDepartement.SelectedValue, grade, poste, dpDebutStatut.ToString(),
                        (string)cboMotifStatut.SelectedValue);

                MessageBox.Show("Transfert enregistré!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnValiderRevocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sess_car.Revoquer(this.code_emp, dpFinStatut.ToString());

                MessageBox.Show("Employé Revoqué!");
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
