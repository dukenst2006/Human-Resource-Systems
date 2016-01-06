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
    /// Interaction logic for FenEmploye.xaml
    /// </summary>
    public partial class FenEmploye : UserControl
    {
        private SessionEmploye sess_emp;
        private MdiContainer mdiContainer;
        private string code_employe;

        public FenEmploye(MdiContainer mct, string operation, Employe emp = null)
        {
            InitializeComponent();
            sess_emp = new SessionEmploye();
            this.mdiContainer = mct;

            //Avant meme de remplir les Cbo
            this.InitialiserCbo();

            this.PreparerInfo(operation);

            //Si c'est pour modifier
            if (emp != null)
            {
                this.code_employe = emp.CodeEmploye;
                this.RemplirEmploye(emp);
            }
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


        private void PreparerInfo(string operation)
        {
            switch (operation)
            {
                case "embaucher":
                    this.lbTitre.Content = "Embaucher un Employé";
                    this.tbCode.IsEnabled = true;
                    this.btnSave.Visibility = Visibility.Visible;
                    break;

                case "modifier":
                    this.lbTitre.Content = "Modifier un Employé";
                    this.btnUpdate.Visibility = Visibility.Visible;
                    break;
            }
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
            tbAdresse.AppendText(e.Adresse);
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


        private void ViderEmploye()
        {
            tbCode.Text = null;
            tbNom.Text =null;
            tbPrenom.Text = null;
            cboSexe.SelectedValue = -1;
            dpDateNaissance.SelectedDate = DateTime.Now;
            tbTelephone.Text = null;
            tbEmail.Text = null;
            tbAdresse.Text = null;
            tbSurplusSalaire.Text = null;

            cboDepartement.SelectedValue = -1;
            cboGrade.SelectedValue = -1;
            cboPoste.SelectedValue = -1;
            dpDebutStatut.SelectedDate = DateTime.Now;
            cboMotifStatut.SelectedValue = -1;

        }


        private void ViderErreurs()
        {
            this.lbCodeErr.Content = null;
            this.lbNomErr.Content = null;
            this.lbPrenomErr.Content = null;
            this.lbSexeErr.Content = null;
            this.lbDateNaissanceErr.Content = null;
            this.lbTelephoneErr.Content = null;
            this.lbEmailErr.Content = null;
            this.lbAdresseErr.Content = null;
            this.lbSurplusSalaireErr.Content = null;
            this.lbDepartementErr.Content = null;
            this.lbGradeErr.Content = null;
            this.lbPosteErr.Content = null;
            this.lbDebutStatutErr.Content = null;
            this.lbMotifStatutErr.Content = null;
        }

        public bool ValidationPassee()
        {
            this.ViderErreurs();

            int err = 0;

            if (string.IsNullOrEmpty(tbCode.Text))
            {
                this.lbCodeErr.Content = "obligatoire";
                err++;
            }
            else if(tbCode.Text.Length != 8)
            {
                this.lbCodeErr.Content = "longueur obligatoire: 8";
                err++;
            }

            if (string.IsNullOrEmpty(tbNom.Text))
            {
                this.lbNomErr.Content = "obligatoire";
                err++;
            }
            else if (tbNom.Text.Length > 30 | tbNom.Text.Length < 2)
            {
                this.lbNomErr.Content = "longueur- min: 2,max: 30";
                err++;
            }

            if (string.IsNullOrEmpty(tbPrenom.Text))
            {
                this.lbPrenomErr.Content = "obligatoire";
                err++;
            }
            else if (tbPrenom.Text.Length > 40 | tbPrenom.Text.Length < 2)
            {
                this.lbPrenomErr.Content = "longueur- min: 2,max: 40";
                err++;
            }

            if (cboSexe.SelectedIndex < 0)
            {
                this.lbSexeErr.Content = "obligatoire";
                err++;
            }

            if (tbTelephone.Text.Length > 15)
            {
                this.lbTelephoneErr.Content = "longueur- max: 15";
                err++;
            }

            if (!string.IsNullOrEmpty(tbEmail.Text))
            {
                if (tbEmail.Text.Length > 50)
                {
                    this.lbEmailErr.Content = "longueur- max: 50";
                    err++;
                }
                else if (tbEmail.Text.Contains(' '))
                {
                    this.lbEmailErr.Content = "sans espace";
                    err++;
                }
                else if (!tbEmail.Text.Contains('@') | !tbEmail.Text.Contains('.'))
                {
                    this.lbEmailErr.Content = "doit contenir : @ et .";
                    err++;
                }
            }

            //if (new TextRange(rtbAdresse.Document.ContentStart, rtbAdresse.Document.ContentEnd).Text.Length > 200)
            if (tbAdresse.Text.Length > 200)
            {
                this.lbAdresseErr.Content = "longueur- max: 200";
                err++;
            }

            if (!string.IsNullOrEmpty(tbSurplusSalaire.Text))
            {
                try { double.Parse(tbSurplusSalaire.Text); }
                catch(Exception ex)
                {
                    this.lbSurplusSalaireErr.Content = "doit etre un: chiffre";
                    err++;
                }

            }

            if (cboDepartement.SelectedIndex < 0)
            {
                this.lbDepartementErr.Content = "obligatoire";
                err++;
            }

            if (cboGrade.SelectedIndex < 0)
            {
                this.lbGradeErr.Content = "obligatoire";
                err++;
            }

            if (cboPoste.SelectedIndex < 0)
            {
                this.lbPosteErr.Content = "obligatoire";
                err++;
            }

            if (dpDebutStatut.ToString().Length <= 3)
            {
                this.lbDebutStatutErr.Content = "obligatoire";
                err++;
            }

            if (cboMotifStatut.SelectedIndex < 0)
            {
                this.lbMotifStatutErr.Content = "obligatoire";
                err++;
            }

            return (err < 1);
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!this.ValidationPassee()) return;

            string grade = null;
            if (cboGrade.SelectedIndex >= 0) grade = cboGrade.SelectedValue.ToString();

            string poste = null;
            if (cboPoste.SelectedIndex >= 0) poste = cboPoste.SelectedValue.ToString();

            try
            {
                sess_emp.Inserer(tbCode.Text, tbNom.Text, tbPrenom.Text, (string)cboSexe.SelectedValue, dpDateNaissance.ToString(),
                        tbTelephone.Text, tbEmail.Text,
                        tbAdresse.Text,
                        tbSurplusSalaire.Text, (string)cboDepartement.SelectedValue, grade, poste, dpDebutStatut.ToString(),
                        (string)cboMotifStatut.SelectedValue);

                MessageBox.Show("Employé enregistré!");
                this.ViderEmploye();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!this.ValidationPassee()) return;
            
            string grade = null;
            if (cboGrade.SelectedIndex >= 0) grade = cboGrade.SelectedValue.ToString();

            string poste = null;
            if (cboPoste.SelectedIndex >= 0) poste = cboPoste.SelectedValue.ToString();

            try
            {
                sess_emp.Modifier(tbCode.Text, tbNom.Text, tbPrenom.Text, (string)cboSexe.SelectedValue, dpDateNaissance.ToString(),
                        tbTelephone.Text, tbEmail.Text,
                        tbAdresse.Text,
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
            foreach (MdiChild mch in this.mdiContainer.Children)
            {
                if (mch.Content.Equals(this))
                {
                    mch.Close();
                    break;
                }
            }
        }

        private void GestionEmploye_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.CodeEmploye = this.code_employe;
        }

        private void GestionEmploye_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.CodeEmploye = "";
        }


        
    }
}
