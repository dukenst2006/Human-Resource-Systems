using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using com.levivoir.rh.application;
using com.levivoir.rh.domaine;

namespace dealxpoWEB
{
    public partial class GestionEmploye : System.Web.UI.Page
    {
        private SessionEmploye sess_emp;
        private SessionCarriere sess_car;
        private string code_employe;


        protected void Page_Load(object sender, EventArgs e)
        {
            sess_emp = new SessionEmploye();

            this.lblFailure.Text = null;
            this.lblSuccess.Text = null;

            if (Request.QueryString["emp"] != null)
            {
                string code = Request.QueryString["emp"];

                if (Request.QueryString["op"] == "modifier"
                        | Request.QueryString["op"] == "eliminer")
                {
                    string operation = Request.QueryString["op"];

                    try
                    {
                        Employe emp = sess_emp.EmployePourModification(code);
                        code_employe = emp.CodeEmploye;
                        this.SetLeftMenu(emp);

                        //If the Page loaded for the first time
                        if (!Page.IsPostBack)
                        {
                            this.InitialiserDDL();

                            this.PreparerInfo(emp, operation);
                            this.RemplirEmploye(emp);
                        }

                    }
                    catch (Exception ex)
                    {
                        this.lblFailure.Text = "Employé non trouvé!";
                        this.content_menu.Style.Add("display", "none");
                    }
                }
                else
                {
                    this.lblFailure.Text = "Opération inconnue!";
                    this.content_menu.Style.Add("display", "none");
                }
            }
            else
            {
                //Embaucher Employé
                if (!Page.IsPostBack)
                {
                    this.InitialiserDDL();
                }

                this.btnEmbaucher.Style.Add("display", "block");
            }

        }


        private void InitialiserDDL()
        {
            //fill the DropDownList controls

            //Setup Sexe
            ddlSexe.Items.Clear();
            ddlSexe.Items.Add(new ListItem("", null));
            ddlSexe.Items.Add(new ListItem("Masculin", "M"));
            ddlSexe.Items.Add(new ListItem("Feminin", "F"));

            //Setup Département
            ddlDepartement.Items.Clear();
            ddlDepartement.Items.Add(new ListItem("", null));
            foreach (DataRow dr in sess_emp.AllDepartements().Rows)
            {
                ddlDepartement.Items.Add(new ListItem(dr["libDepartement"].ToString(), dr["CodeDepartement"].ToString()));
            }


            //Setup Grade
            ddlGrade.Items.Clear();
            ddlGrade.Items.Add(new ListItem("", null));
            foreach (DataRow dr in sess_emp.AllGrades().Rows)
            {
                ddlGrade.Items.Add(new ListItem(dr["libGrade"].ToString(), dr["CodeGrade"].ToString()));
            }

            //Setup Poste
            ddlPoste.Items.Clear();
            ddlPoste.Items.Add(new ListItem("", null));
            foreach (DataRow dr in sess_emp.AllPostes().Rows)
            {
                ddlPoste.Items.Add(new ListItem(dr["libPoste"].ToString(), dr["CodePoste"].ToString()));
            }

            //Setup Motif
            ddlMotifStatut.Items.Clear();
            ddlMotifStatut.Items.Add(new ListItem("", null));
            ddlMotifStatut.Items.Add(new ListItem("Nomination", "Nomination"));
            ddlMotifStatut.Items.Add(new ListItem("Promotion", "Promotion"));
            ddlMotifStatut.Items.Add(new ListItem("Transfert", "Transfert"));

        }


        private void PreparerInfo(Employe emp, string operation)
        {
            switch (operation)
            {
                case "modifier":
                    this.operation_title.InnerText = "Modifier un Employé";

                    this.tbCode.Enabled = false;

                    this.btnModifier.Style.Add("display", "block");
                    break;

                case "eliminer":
                    this.operation_title.InnerText = "Eliminer un Employé";

                    this.tbCode.Enabled = false;
                    this.tbNom.Enabled = false;
                    this.tbPrenom.Enabled = false;
                    this.ddlSexe.Enabled = false;
                    this.tbDateNaissance.Enabled = false;
                    this.tbTelephone.Enabled = false;
                    this.tbEmail.Enabled = false;
                    this.tbAdresse.Enabled = false;
                    this.tbSurplusSalaire.Enabled = false;

                    this.ddlDepartement.Enabled = false;
                    this.ddlGrade.Enabled = false;
                    this.ddlPoste.Enabled = false;
                    this.tbDebutStatut.Enabled = false;
                    this.ddlMotifStatut.Enabled = false;

                    this.btnEliminer.Style.Add("display", "block"); ;
                    break;

            }
        }


        private void RemplirEmploye(Employe e)
        {
            StatutCarriere s = e.getStatutActuel();

            tbCode.Text = e.CodeEmploye;

            tbNom.Text = e.Nom;
            tbPrenom.Text = e.Prenom;
            ddlSexe.SelectedValue = e.Sexe;

            if (!e.DateNaissance.ToString().Equals(""))
            {
                tbDateNaissance.Text = e.DateNaissance.ToString();
            }

            tbTelephone.Text = e.Telephone;
            tbEmail.Text = e.Email;
            tbAdresse.Text = e.Adresse;
            tbSurplusSalaire.Text = e.SurplusSalaire.ToString();

            ddlDepartement.SelectedValue = s.CodeDepartement;
            ddlGrade.SelectedValue = s.CodeGrade.ToString();
            ddlPoste.SelectedValue = s.CodePoste.ToString();

            if (!s.Debut.ToString().Equals(""))
            {
                tbDebutStatut.Text = s.Debut.ToString();
            }

            ddlMotifStatut.SelectedValue = s.Motif;

        }

        private void SetLeftMenu(Employe e)
        {
            this.content_menu.Style.Add("display", "block");

            this.hlModifierEmploye.NavigateUrl = "GestionEmploye.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=modifier";
            this.hlDemandeConge.NavigateUrl = "GestionConge.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=demande";
            this.hlRetourConge.NavigateUrl = "GestionConge.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=retour";
            this.hlPromotion.NavigateUrl = "GestionCarriere.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=promotion";
            this.hlTransfert.NavigateUrl = "GestionCarriere.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=transfert";
            this.hlRevocation.NavigateUrl = "GestionCarriere.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=revocation";
            this.hlEliminerEmploye.NavigateUrl = "GestionEmploye.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=eliminer";
        }

        private void ViderEmploye()
        {
            tbCode.Text = null;
            tbNom.Text = null;
            tbPrenom.Text = null;
            ddlSexe.SelectedIndex = 0;
            tbDateNaissance.Text = null;
            tbTelephone.Text = null;
            tbEmail.Text = null;
            tbAdresse.Text = null;
            tbSurplusSalaire.Text = null;

            ddlDepartement.SelectedIndex = 0;
            ddlGrade.SelectedIndex = 0;
            ddlPoste.SelectedIndex = 0;
            tbDebutStatut.Text = null;
            ddlMotifStatut.SelectedIndex = 0;

        }

        private void ViderErreurs()
        {
            this.lbCodeErr.InnerText = null;
            this.lbNomErr.InnerText = null;
            this.lbPrenomErr.InnerText = null;
            this.lbSexeErr.InnerText = null;
            this.lbDateNaissanceErr.InnerText = null;
            this.lbTelephoneErr.InnerText = null;
            this.lbEmailErr.InnerText = null;
            this.lbAdresseErr.InnerText = null;
            this.lbSurplusSalaireErr.InnerText = null;
            this.lbDepartementErr.InnerText = null;
            this.lbGradeErr.InnerText = null;
            this.lbPosteErr.InnerText = null;
            this.lbDebutStatutErr.InnerText = null;
            this.lbMotifStatutErr.InnerText = null;
        }


        public bool ValidationPassee()
        {
            this.ViderErreurs();

            int err = 0;

            if (string.IsNullOrEmpty(tbCode.Text))
            {
                this.lbCodeErr.InnerText = "obligatoire";
                err++;
            }
            else if (tbCode.Text.Length != 8)
            {
                this.lbCodeErr.InnerText = "longueur obligatoire: 8";
                err++;
            }


            if (string.IsNullOrEmpty(tbNom.Text))
            {
                this.lbNomErr.InnerText = "obligatoire";
                err++;
            }
            else if (tbNom.Text.Length > 30 | tbNom.Text.Length < 2)
            {
                this.lbNomErr.InnerText = "longueur- min: 2,max: 30";
                err++;
            }

            if (string.IsNullOrEmpty(tbPrenom.Text))
            {
                this.lbPrenomErr.InnerText = "obligatoire";
                err++;
            }
            else if (tbPrenom.Text.Length > 40 | tbPrenom.Text.Length < 2)
            {
                this.lbPrenomErr.InnerText = "longueur- min: 2,max: 40";
                err++;
            }

            if (ddlSexe.SelectedIndex <= 0)
            {
                this.lbSexeErr.InnerText = "obligatoire";
                err++;
            }

            if (tbDateNaissance.Text.Length > 0)
            {
                try { DateTime.Parse(tbDateNaissance.Text); }
                catch (Exception ex)
                {
                    this.lbDateNaissanceErr.InnerText = "date non valide";
                    err++;
                }
            }

            if (tbTelephone.Text.Length > 15)
            {
                this.lbTelephoneErr.InnerText = "longueur- max: 15";
                err++;
            }


            if (!string.IsNullOrEmpty(tbEmail.Text))
            {
                if (tbEmail.Text.Length > 50)
                {
                    this.lbEmailErr.InnerText = "longueur- max: 50";
                    err++;
                }
                else if (tbEmail.Text.Contains(' '))
                {
                    this.lbEmailErr.InnerText = "sans espace";
                    err++;
                }
                else if (!tbEmail.Text.Contains('@') | !tbEmail.Text.Contains('.'))
                {
                    this.lbEmailErr.InnerText = "doit contenir : @ et .";
                    err++;
                }
            }

            if (tbAdresse.Text.Length > 200)
            {
                this.lbAdresseErr.InnerText = "longueur- max: 200";
                err++;
            }

            if (!string.IsNullOrEmpty(tbSurplusSalaire.Text))
            {
                try { double.Parse(tbSurplusSalaire.Text); }
                catch (Exception ex)
                {
                    this.lbSurplusSalaireErr.InnerText = "doit etre un: chiffre";
                    err++;
                }

            }

            if (ddlDepartement.SelectedIndex <= 0)
            {
                this.lbDepartementErr.InnerText = "obligatoire";
                err++;
            }

            if (ddlGrade.SelectedIndex <= 0)
            {
                this.lbGradeErr.InnerText = "obligatoire";
                err++;
            }

            if (ddlPoste.SelectedIndex <= 0)
            {
                this.lbPosteErr.InnerText = "obligatoire";
                err++;
            }

            if (tbDebutStatut.Text.Length <= 3)
            {
                this.lbDebutStatutErr.InnerText = "obligatoire";
                err++;
            }
            else
            {
                try { DateTime.Parse(tbDebutStatut.Text); }
                catch (Exception ex)
                {
                    this.lbDebutStatutErr.InnerText = "date non valide";
                    err++;
                }
            }

            if (ddlMotifStatut.SelectedIndex <= 0)
            {
                this.lbMotifStatutErr.InnerText = "obligatoire";
                err++;
            }

            return (err < 1);

        }

        protected void btnEmbaucher_Click(object sender, EventArgs e)
        {
            if (!this.ValidationPassee())
            {
                this.lblFailure.Text = "Erreur: Veuillez corriger le formulaire!";
                return;
            }

            string grade = null;
            if (ddlGrade.SelectedIndex >= 0) grade = ddlGrade.SelectedValue.ToString();

            string poste = null;
            if (ddlPoste.SelectedIndex >= 0) poste = ddlPoste.SelectedValue.ToString();

            try
            {
                sess_emp.Inserer(tbCode.Text, tbNom.Text, tbPrenom.Text, (string)ddlSexe.SelectedValue, tbDateNaissance.Text,
                        tbTelephone.Text, tbEmail.Text, tbAdresse.Text, tbSurplusSalaire.Text,
                        (string)ddlDepartement.SelectedValue, grade, poste, tbDebutStatut.Text,
                        (string)ddlMotifStatut.SelectedValue);

                //this.Alert("Employé enregistré!");
                this.lblSuccess.Text = "Employé enregistré!";

                this.ViderEmploye();
            }
            catch (Exception ex)
            {
                //this.Alert(ex.Message);
                this.lblFailure.Text = ex.Message;
            }
        }

        protected void btnModifier_Click(object sender, EventArgs e)
        {
            if (!this.ValidationPassee())
            {
                this.lblFailure.Text = "Erreur: Veuillez corriger le formulaire!";
                return;
            }

            string grade = null;
            if (ddlGrade.SelectedIndex >= 0) grade = ddlGrade.SelectedValue.ToString();

            string poste = null;
            if (ddlPoste.SelectedIndex >= 0) poste = ddlPoste.SelectedValue.ToString();

            try
            {
                sess_emp.Modifier(code_employe, tbNom.Text, tbPrenom.Text, (string)ddlSexe.SelectedValue, tbDateNaissance.Text,
                        tbTelephone.Text, tbEmail.Text, tbAdresse.Text, tbSurplusSalaire.Text,
                        (string)ddlDepartement.SelectedValue, grade, poste, tbDebutStatut.Text,
                        (string)ddlMotifStatut.SelectedValue);

                //this.Alert("Employé enregistré!");
                this.lblSuccess.Text = "Employé modifié!";

            }
            catch (Exception ex)
            {
                //this.Alert(ex.Message);
                this.lblFailure.Text = ex.Message;
            }
        }

        protected void btnEliminer_Click(object sender, EventArgs e)
        {
            try
            {
                sess_emp.Eliminer(tbCode.Text);
                Server.Transfer("RechercherEmploye.aspx", true);
            }
            catch (Exception ex)
            {
                //this.Alert(ex.Message);
                this.lblFailure.Text = ex.Message;
            }
        }
    }
}