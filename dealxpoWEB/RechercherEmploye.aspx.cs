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
    public enum TargetRecherche
    {
        Code = 0, Nom = 1
    }

    public partial class RechercherEmploye : System.Web.UI.Page
    {
        private SessionEmploye sess;

        protected void Page_Load(object sender, EventArgs e)
        {
            sess = new SessionEmploye();

            if (!Page.IsPostBack)
            {
                this.rbCode.Checked = true;
            }

            if (Request.QueryString["emp"] != null && Request.QueryString["op"] == "eliminer")
            {
                string codeEmploye = Request.QueryString["emp"];
                this.lblSuccess.Text = "L'Employé au code: (" + codeEmploye + ") a été enlevé du Système!";
            }

        }


        protected void gvEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbResultatRecherche.Text = "Selected Employe Changed";
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                this.lbResultatRecherche.Text = "Recherche en Cours...";
                TimeSpan ts = new TimeSpan();
                DateTime start = DateTime.Now;
                DateTime stop;

                DataTable dt = new DataTable();
                if (rbCode.Checked == true) dt = sess.Rechercher(tbMotsCles.Text, (int)TargetRecherche.Code);
                else if (rbNom.Checked == true) dt = sess.Rechercher(tbMotsCles.Text, (int)TargetRecherche.Nom);

                stop = DateTime.Now;
                ts = new TimeSpan(stop.Ticks - start.Ticks);
                this.lbResultatRecherche.Text = "Total Employés : " + dt.Rows.Count + ". Temps de recherche : " + (double)ts.Milliseconds / 1000 + "sec(s).";

                foreach (DataRow dr in dt.Rows)
                {
                    dr["CodeEmploye"] =
                    new HtmlString("<a href=\"GestionEmploye.aspx?emp=" + dr["CodeEmploye"].ToString().Replace(" ", null) + "&op=modifier\" >" + dr["CodeEmploye"].ToString() + "</a>");
                }


                this.dgEmploye.DataSource = dt.DefaultView;
                this.dgEmploye.DataBind();
                //this.dgEmploye.Height = 350;
            }
            catch (Exception ex)
            {
                this.lblFailure.Text = ex.Message;
            }
        }
    }
}