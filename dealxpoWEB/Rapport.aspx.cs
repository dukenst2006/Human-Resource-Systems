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
    public partial class Rapport : System.Web.UI.Page
    {
        private SessionEmploye sess_emp;

        protected void Page_Load(object sender, EventArgs e)
        {
            sess_emp = new SessionEmploye();

            if (Request.Params["r"] != null)
            {
                DataTable dt = new DataTable();

                string rapport = Request.Params["r"];

                //Pour la partie DataTable
                if (rapport == "statut") dt = sess_emp.EmployeAvecStatut();
                if (rapport == "effectif") dt = sess_emp.ComptageEmployeParDepartement();
                if (rapport == "contact") dt = sess_emp.ContactEmploye();
                if (rapport == "emploi") dt = sess_emp.Emplois();

                //Pour la partie Presence
                if (rapport == "presence_jour")
                {
                    DateTime date = DateTime.Now;
                    string dateString = date.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("fr-fr"));
                    this.operation_title.InnerText = "Présence du " + dateString;
                    dt = sess_emp.PresenceJournaliere(date);
                }

                if (rapport == "presence_mois")
                {
                    DateTime date = DateTime.Now;
                    string dateString = date.ToString("MMMM yyyy", new System.Globalization.CultureInfo("fr-fr"));
                    this.operation_title.InnerText = "Présence de " + dateString;
                    dt = sess_emp.PresenceMensuelle(date);
                }

                if (rapport == "presence_annee")
                {
                    DateTime date = DateTime.Now;
                    this.operation_title.InnerText = "Présence (Retards + Absences) de l'Année " + date.Year;
                    dt = sess_emp.RetardAnnuel(date);

                    DataTable dt2 = sess_emp.AbsenceAnnuelle(date);
                    gvRapport2.DataSource = dt2;
                    gvRapport2.DataBind();
                }

                if (rapport == "conge")
                {
                    //Employe en congé
                    this.operation_title.InnerText = "Employés en Congé";
                    dt = sess_emp.EmployesEnConge();
                }

                this.gvRapport.DataSource = dt;

                //Pour la partie Payroll
                if (rapport == "payroll")
                {
                    Payroll pay = sess_emp.GetPayroll();
                    this.operation_title.InnerText = "Payroll de : " + pay.Periode;
                    this.gvRapport.DataSource = pay.Paiements;
                    //this.dgRapport.CssClass = "grid_table";
                }

                this.gvRapport.DataBind();
            }

        }


    }
}