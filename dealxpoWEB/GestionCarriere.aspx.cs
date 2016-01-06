using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using com.levivoir.rh.domaine;
using com.levivoir.rh.application;

namespace dealxpoWEB
{
    public partial class GestionCarriere : System.Web.UI.Page
    {
        public enum CarriereMotif
        {
            Promotion = 0, Transfert = 1, Revocation = 2
        }

        private SessionEmploye sess_emp;
        private SessionCarriere sess_car;
        private string code_employe;


        protected void Page_Load(object sender, EventArgs e)
        {
            sess_emp = new SessionEmploye();
            sess_car = new SessionCarriere();

            this.lblFailure.Text = null;
            this.lblSuccess.Text = null;

            if (Request.QueryString["emp"] != null)
            {
                string code = Request.QueryString["emp"];

                if (Request.QueryString["op"] == "promotion"
                        | Request.QueryString["op"] == "transfert"
                        | Request.QueryString["op"] == "revocation")
                {
                    string operation = Request.QueryString["op"];
                    CarriereMotif cm = new CarriereMotif();

                    if (operation == "promotion") cm = CarriereMotif.Promotion;
                    if (operation == "transfert") cm = CarriereMotif.Transfert;
                    if (operation == "revocation") cm = CarriereMotif.Revocation;

                    try
                    {
                        SessionEmploye sess_emp_emp = new SessionEmploye();
                        Employe emp = sess_emp_emp.EmployePourModification(code);
                        code_employe = emp.CodeEmploye;
                        this.SetLeftMenu(emp);

                        //If the Page loaded for the first time
                        if (!Page.IsPostBack)
                        {
                            this.VerifierStatutEmploye(emp, cm);


                        }
                    }
                    catch (Exception ex)
                    {
                        this.lblFailure.Text = "Employé non trouvé! " + ex.Message;
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
                this.lblFailure.Text = "Aucun Employé à Gérer!";
                this.content_menu.Style.Add("display", "none");
            }


        }


        private void VerifierStatutEmploye(Employe emp, CarriereMotif cm)
        {
            if (emp.EstRevoque())
            {
                this.lblFailure.Text = "L'Employé " + emp.Nom + " " + emp.Prenom + " est revoqué!";
            }
            else
            {
                this.InitialiserDDL();

                this.PreparerInfo(emp, cm);
                this.RemplirEmploye(emp, cm);
            }
        }


        private void InitialiserDDL()
        {
            //fill the DropDownList controls
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



        private void PreparerInfo(Employe emp, CarriereMotif motif)
        {
            StatutCarriere stc;
            switch (motif)
            {
                case CarriereMotif.Promotion:
                    this.operation_title.InnerText = "Donner une Promotion";

                    this.ddlMotifStatut.SelectedValue = "Promotion";
                    this.ddlMotifStatut.Enabled = false;
                    this.btnValiderPromotion.Style.Add("display", "block");
                    break;

                case CarriereMotif.Transfert:
                    this.operation_title.InnerText = "Effectuer un Transfert";

                    stc = emp.getStatutActuel();
                    this.ddlMotifStatut.SelectedValue = "Transfert";
                    this.ddlMotifStatut.Enabled = false;
                    //Garde le meme Grade
                    this.ddlGrade.SelectedValue = stc.CodeGrade.ToString();
                    this.ddlGrade.Enabled = false;
                    //Garde le meme Poste
                    this.ddlPoste.SelectedValue = stc.CodePoste.ToString();
                    this.ddlPoste.Enabled = false;

                    this.btnValiderTransfert.Style.Add("display", "block"); ;
                    break;

                case CarriereMotif.Revocation:
                    this.operation_title.InnerText = "Effectuer une Revocation";

                    stc = emp.getStatutActuel();

                    this.ddlDepartement.Visible = false;
                    this.ddlMotifStatut.Visible = false;
                    this.ddlGrade.Visible = false;
                    this.ddlPoste.Visible = false;
                    this.tbDebutStatut.Visible = false;

                    this.revocation_input.Style.Add("display", "block");
                    this.btnValiderRevocation.Style.Add("display", "block");
                    break;

            }


        }

        private void RemplirEmploye(Employe emp, CarriereMotif motif)
        {
            this.lbEspaceEmploye.Text = motif + " de l'Employé : (" + emp.CodeEmploye + ") " + emp.Prenom + " " + emp.Nom;
            try
            {
                StatutCarriere stc = emp.getStatutActuel();

                this.lbDepartement.Text = stc.getDepartement().Name;
                this.lbGrade.Text = stc.getGrade().Name;
                this.lbPoste.Text = stc.getPoste().Name;
                this.lbDebut.Text = stc.Debut.ToString();
                this.lbMotif.Text = stc.Motif;

                this.dgCarriere.DataSource = emp.getStatutCarrieres();
                this.dgCarriere.DataBind();
            }
            catch (Exception ex)
            {
                this.lblFailure.Text = ex.Message;
            }
        }


        private void SetLeftMenu(Employe e)
        {
            this.hlModifierEmploye.NavigateUrl = "GestionEmploye.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=modifier";
            this.hlDemandeConge.NavigateUrl = "GestionConge.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=demande";
            this.hlRetourConge.NavigateUrl = "GestionConge.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=retour";
            this.hlPromotion.NavigateUrl = "GestionCarriere.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=promotion";
            this.hlTransfert.NavigateUrl = "GestionCarriere.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=transfert";
            this.hlRevocation.NavigateUrl = "GestionCarriere.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=revocation";
            this.hlEliminerEmploye.NavigateUrl = "GestionEmploye.aspx?emp=" + e.CodeEmploye.ToLower() + "&op=eliminer";
        }


        protected void btnValiderPromotion_Click(object sender, EventArgs e)
        {
            string grade = null;
            if (ddlGrade.SelectedIndex >= 0) grade = ddlGrade.SelectedValue.ToString();

            string poste = null;
            if (ddlPoste.SelectedIndex >= 0) poste = ddlPoste.SelectedValue.ToString();

            try
            {
                sess_car.InsererStatutCarriere(code_employe, (string)ddlDepartement.SelectedValue, grade, poste, tbDebutStatut.Text,
                        (string)ddlMotifStatut.SelectedValue);

                this.lblSuccess.Text = "Promotion enregistrée!";
                this.btnValiderPromotion.Enabled = false;
            }
            catch (Exception ex)
            {
                this.lblFailure.Text = ex.Message;
            }
        }

        protected void btnValiderTransfert_Click(object sender, EventArgs e)
        {
            string grade = null;
            if (ddlGrade.SelectedIndex >= 0) grade = ddlGrade.SelectedValue.ToString();

            string poste = null;
            if (ddlPoste.SelectedIndex >= 0) poste = ddlPoste.SelectedValue.ToString();

            try
            {
                sess_car.InsererStatutCarriere(code_employe, (string)ddlDepartement.SelectedValue, grade, poste, tbDebutStatut.Text,
                        (string)ddlMotifStatut.SelectedValue);

                this.lblSuccess.Text = "Transfert enregistré!";
                this.btnValiderTransfert.Enabled = false;
            }
            catch (Exception ex)
            {
                this.lblFailure.Text = ex.Message;
            }
        }

        protected void btnValiderRevocation_Click(object sender, EventArgs e)
        {
            try
            {
                sess_car.Revoquer(code_employe, tbFinStatut.Text);

                this.lblSuccess.Text = "Révocation enregistrée!";
                this.btnValiderRevocation.Enabled = false;
            }
            catch (Exception ex)
            {
                this.lblFailure.Text = ex.Message;
            }
        }
    }
}