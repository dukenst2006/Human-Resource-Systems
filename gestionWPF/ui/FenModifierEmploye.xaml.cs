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

namespace com.levivoir.rh.ui
{
    /// <summary>
    /// Interaction logic for FenModifierEmploye.xaml
    /// </summary>
    public partial class FenModifierEmploye : UserControl
    {
        private SessionEmploye sess;

        public FenModifierEmploye(Employe e)
        {
            InitializeComponent();
            sess = new SessionEmploye();
            this.InitialiserCbo();

            this.RemplirEmploye(e);
        }


        private void InitialiserCbo()
        {
            this.cboSexe.ItemsSource = null;
            this.cboDepartement.ItemsSource = null;
            this.cboGrade.ItemsSource = null;
            this.cboPoste.ItemsSource = null;
            this.cboMotifStatut.ItemsSource = null;

            //Setup Sexe
            DataTable dt = new DataTable("Sexe");
            dt.Columns.Add("Code");
            dt.Columns.Add("Expression");
            dt.Rows.Add("M", "Masculin");
            dt.Rows.Add("F", "Feminin");

            this.cboSexe.ItemsSource = dt.DefaultView;
            this.cboSexe.SelectedValuePath = "Code";
            this.cboSexe.DisplayMemberPath = "Expression";
            this.cboSexe.SelectedItem = null;

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
            dt = sess.AllDepartements();
            this.cboDepartement.ItemsSource = dt.DefaultView;
            this.cboDepartement.SelectedValuePath = "CodeDepartement";
            this.cboDepartement.DisplayMemberPath = "libDepartement";
            this.cboDepartement.SelectedIndex = -1;

            //Setup Grade
            dt = sess.AllGrades();
            this.cboGrade.ItemsSource = dt.DefaultView;
            this.cboGrade.SelectedValuePath = "CodeGrade";
            this.cboGrade.DisplayMemberPath = "libGrade";
            this.cboGrade.SelectedIndex = -1;

            //Setup Poste
            dt = sess.AllPostes();
            this.cboPoste.ItemsSource = dt.DefaultView;
            this.cboPoste.SelectedValuePath = "CodePoste";
            this.cboPoste.DisplayMemberPath = "libPoste";
            this.cboPoste.SelectedIndex = -1;
        }

        private void RemplirEmploye_bak(DataRow dr)
        {
            tbCode.Text = dr["CodeEmploye"].ToString();

            tbNom.Text = dr["Nom"].ToString();
            tbPrenom.Text = dr["Prenom"].ToString();
            cboSexe.SelectedValue = dr["Sexe"].ToString();

            if (!dr["DateNaissance"].Equals(null))
                dpDateNaissance.DisplayDate = (DateTime)dr["DateNaissance"];

            tbTelephone.Text = dr["Telephone"].ToString();
            tbEmail.Text = dr["Email"].ToString();
            rtbAdresse.AppendText(dr["Address"].ToString());
            tbSurplusSalaire.Text = dr["SurplusSalaire"].ToString();
        }

        private void RemplirEmploye(Employe e)
        {
            StatutCarriere s = e.getStatutActuel();

            tbCode.Text = e.CodeEmploye;

            tbNom.Text = e.Nom;
            tbPrenom.Text = e.Prenom;
            cboSexe.SelectedValue = e.Sexe;

            if (!e.DateNaissance.ToString().Equals(""))
            {
                dpDateNaissance.SelectedDate = e.DateNaissance;
                dpDateNaissance.DisplayDate = e.DateNaissance;
            }

            tbTelephone.Text = e.Telephone;
            tbEmail.Text = e.Email;
            rtbAdresse.AppendText(e.Adresse);
            tbSurplusSalaire.Text = e.SurplusSalaire.ToString();

            cboDepartement.SelectedValue = s.CodeDepartement;
            cboGrade.SelectedValue = s.CodeGrade;
            cboPoste.SelectedValue = s.CodePoste;

            if (!s.Debut.ToString().Equals(""))
            {
                dpDebutStatut.SelectedDate = s.Debut;
            }

            cboMotifStatut.SelectedValue = s.Motif;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string grade = null;
            if (cboGrade.SelectedIndex >= 0) grade = cboGrade.SelectedValue.ToString();

            string poste = null;
            if (cboPoste.SelectedIndex >= 0) poste = cboPoste.SelectedValue.ToString();

            try
            {
                sess.Modifier(tbCode.Text, tbNom.Text, tbPrenom.Text, (string)cboSexe.SelectedValue, dpDateNaissance.ToString(),
                        tbTelephone.Text, tbEmail.Text,
                        new TextRange(rtbAdresse.Document.ContentStart, rtbAdresse.Document.ContentEnd).Text,
                        tbSurplusSalaire.Text, (string)cboDepartement.SelectedValue, grade, poste, dpDebutStatut.ToString(),
                        (string)cboMotifStatut.SelectedValue);

                MessageBox.Show("Employé modifié avec succès!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifierEmploye_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}
