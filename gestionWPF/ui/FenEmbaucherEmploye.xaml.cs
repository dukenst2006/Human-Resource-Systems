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
using WPF.MDI;

namespace com.levivoir.rh.ui
{
    /// <summary>
    /// Interaction logic for FenEmbaucherEmploye.xaml
    /// </summary>
    public partial class FenEmbaucherEmploye : UserControl
    {
        private SessionEmploye sess;
        private MdiContainer mdiContainer;

        public FenEmbaucherEmploye(MdiContainer mct)
        {
            InitializeComponent();
            sess = new SessionEmploye();
            this.mdiContainer = mct;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string grade = null;
            if (cboGrade.SelectedIndex >= 0) grade = cboGrade.SelectedValue.ToString();

            string poste = null;
            if (cboPoste.SelectedIndex >= 0) poste = cboPoste.SelectedValue.ToString();
            
            try
            {
                sess.Inserer(tbCode.Text, tbNom.Text, tbPrenom.Text, (string)cboSexe.SelectedValue, dpDateNaissance.ToString(),
                        tbTelephone.Text, tbEmail.Text, 
                        new TextRange(rtbAdresse.Document.ContentStart, rtbAdresse.Document.ContentEnd).Text,
                        tbSurplusSalaire.Text, (string)cboDepartement.SelectedValue, grade, poste, dpDebutStatut.ToString(),
                        (string)cboMotifStatut.SelectedValue);

                MessageBox.Show("Employé enregistré!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            foreach(MdiChild mch in this.mdiContainer.Children)
            {
                if(mch.Content.Equals(this))
                {
                   mch.Close();
                    break;
                }
            }
        }

        private void EmbaucherEmploye_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitialiserCbo();
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
        
    }
}
