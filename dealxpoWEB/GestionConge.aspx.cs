using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.levivoir.rh.domaine;
using com.levivoir.rh.application;

namespace dealxpoWEB
{
    public partial class GestionConge : System.Web.UI.Page
    {
        private SessionConge sess_cong;
        private int code_conge;
        private string code_employe;


        protected void Page_Load(object sender, EventArgs e)
        {
            sess_cong = new SessionConge();

            this.lblFailure.Text = null;
            this.lblSuccess.Text = null;

            if (Request.QueryString["emp"] != null)
            {
                string code = Request.QueryString["emp"];

                if (Request.QueryString["op"] == "demande"
                        | Request.QueryString["op"] == "retour")
                {
                    string operation = Request.QueryString["op"];

                    try
                    {
                        SessionEmploye sess_cong_emp = new SessionEmploye();
                        Employe emp = sess_cong_emp.EmployePourModification(code);
                        code_employe = emp.CodeEmploye;

                        this.SetLeftMenu(emp);

                        //If the Page loaded for the first time
                        if (!Page.IsPostBack)
                        {
                            this.VerifierStatutEmploye(emp, operation);
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


        private void VerifierStatutEmploye(Employe emp, string op)
        {
            if (emp.EstRevoque())
            {
                this.lblFailure.Text = "L'Employé " + emp.Nom + " " + emp.Prenom + " est revoqué!";
            }
            else
            {
                this.InitialiserDDL();

                this.PreparerInfo(emp, op);
                this.RemplirEmploye(emp, op);
            }
        }

        private void InitialiserDDL()
        {
            //fill the DropDownList controls

            //Setup Motif
            ddlType.Items.Clear();
            ddlType.Items.Add(new ListItem("", null));
            ddlType.Items.Add(new ListItem("Annuel", "Annuel"));
            ddlType.Items.Add(new ListItem("Maladie", "Maladie"));
            ddlType.Items.Add(new ListItem("Maternite", "Maternite"));

        }

        private void PreparerInfo(Employe emp, string operation)
        {
            try
            {
                this.tbAnnee.Text = sess_cong.TrouverAnneeFiscaleActuelle().ToString();
                this.ddlType.SelectedValue = "Annuel";
                this.ddlType.Enabled = false;
                this.tbAnnee.Enabled = false;

                switch (operation)
                {
                    case "demande":
                        this.operation_title.InnerText = "Demande de Congé";

                        if (emp.getConges().Count > 0 && !emp.dernierRetourEffectifValide())
                        {
                            lblFailure.Text = "Le dernier Retour de Congé n'a pas été validé!";
                            this.ddlType.Enabled = false;
                            this.tbAnnee.Enabled = false;
                            this.tbDebut.Enabled = false;
                            this.tbRetourPrevu.Enabled = false;
                            this.tbRetourEffectif.Enabled = false;
                        }
                        else
                        {
                            this.lblSuccess.Text = "L'Employé peut prendre un Congé!";
                            this.tbRetourEffectif.Enabled = false;
                            this.btnValiderDemande.Style.Add("display", "block");
                        }
                        break;

                    case "retour":
                        this.operation_title.InnerText = "Retour Effectif d'un Congé";

                        if (emp.getCongeOuvert() == null)
                        {
                            this.lblFailure.Text = "L'Employé n'est pas actuellement en Congé!";
                            this.ddlType.Enabled = false;
                            this.tbAnnee.Enabled = false;
                            this.tbDebut.Enabled = false;
                            this.tbRetourPrevu.Enabled = false;
                            this.tbRetourEffectif.Enabled = false;
                        }
                        else
                        {
                            this.lblSuccess.Text = "L'Employé est bien en Congé!";
                            this.ddlType.Enabled = false;
                            this.tbAnnee.Enabled = false;
                            this.tbDebut.Enabled = false;
                            this.tbRetourPrevu.Enabled = false;
                            this.tbRetourEffectif.Enabled = true;

                            this.btnValiderRetour.Style.Add("display", "block");
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                this.lblFailure.Text = ex.Message;
            }
        }

        private void RemplirEmploye(Employe emp, string operation)
        {

            try
            {
                this.lbEspaceEmploye.Text = "Employé : (" + emp.CodeEmploye + ") " + emp.Prenom + " " + emp.Nom;

                Conge c = emp.getCongeOuvert();
                if (c != null)
                {
                    //Set code_conge of this Window so we can update RetourEffectif
                    code_conge = c.CodeConge;

                    this.ddlType.SelectedValue = c.Type;
                    this.tbAnnee.Text = c.AnneeFiscale.ToString();
                    this.tbDebut.Text = c.Debut.ToString();
                    this.tbRetourPrevu.Text = c.RetourPrevu.ToString();
                }

                this.dgConge.DataSource = emp.getConges();
                this.dgConge.DataBind();
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



        protected void btnValiderDemande_Click(object sender, EventArgs e)
        {
            try
            {
                sess_cong.Inserer(code_employe, (string)ddlType.SelectedValue.ToString(), int.Parse(tbAnnee.Text), tbDebut.Text,
                        tbRetourPrevu.Text);

                lblSuccess.Text = "Congé enregistré!";
                this.btnValiderDemande.Enabled = false;
            }
            catch (Exception ex)
            {
                lblSuccess.Text = null;
                lblFailure.Text = ex.Message;
            }
        }
        protected void btnValiderRetour_Click(object sender, EventArgs e)
        {
            try
            {
                sess_cong.ValiderRetourEffectif(this.code_conge, tbRetourEffectif.Text);

                lblSuccess.Text = "Retour de Congé validé!";
                this.btnValiderRetour.Enabled = false;
            }
            catch (Exception ex)
            {
                lblSuccess.Text = null;
                lblFailure.Text = ex.Message;
            }
        }
    }
}