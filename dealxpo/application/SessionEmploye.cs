using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using com.levivoir.rh.dal;
using com.levivoir.rh.domaine;

namespace com.levivoir.rh.application
{
    public class SessionEmploye : BaseSession
    {
        

        public List<Employe> All()
        {
            try
            {
                List<Employe> eList = EmployeDAL.All();
                return eList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }

        }
        

        public DataTable Rechercher(string keyWords, int type)
        {
            try
            {
                DataTable dt = new DataTable();
                
                switch (type)
                {
                    case 0: dt = EmployeDAL.Trouver(keyWords);
                        break;
                    case 1: dt = EmployeDAL.RechercherParNom(keyWords);
                        break;
                    case 2: dt = EmployeDAL.RechercherParCodeEtNom(keyWords);
                        break;
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public Employe EmployePourModification(string code)
        {
            try
            {
                return EmployeDAL.TrouverEmploye(code);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public void Inserer(string code_emp, string nom, string prenom, string sexe, string date_naissance,
                    string telephone, string email, string adresse, string surplus_salaire,
                    string code_dep, string code_grade, string code_poste, string debut, string motif)
        {
            double surplus = 0.0;
            if (surplus_salaire.Length > 0) surplus = Double.Parse(surplus_salaire);

            DateTime naissance = DateTime.Parse("1/1/0001");
            if (date_naissance.Length > 0) naissance = DateTime.Parse(date_naissance);

            try
            {
                Employe e = new Employe(code_emp, nom, prenom, sexe, naissance, telephone, email, adresse, surplus);

                StatutCarriere s = new StatutCarriere(code_dep, code_emp, int.Parse(code_grade), int.Parse(code_poste), DateTime.Parse(debut), motif);

                EmployeDAL.Insert(e, s);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }


        public void Modifier(string code_emp, string nom, string prenom, string sexe, string date_naissance,
                    string telephone, string email, string adresse, string surplus_salaire,
                    string code_dep, string code_grade, string code_poste, string debut, string motif)
        {
            try
            {
                double surplus = 0.0;
                if (surplus_salaire.Length > 0) surplus = Double.Parse(surplus_salaire);

                DateTime naissance = DateTime.Parse("1/1/0001");
                if (date_naissance.Length > 0) naissance = DateTime.Parse(date_naissance);
                Employe e = new Employe(code_emp, nom, prenom, sexe, naissance, telephone, email, adresse, surplus);

                StatutCarriere s = new StatutCarriere(code_dep, code_emp, int.Parse(code_grade), int.Parse(code_poste), DateTime.Parse(debut), motif);

                EmployeDAL.Update(e, s);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }


        public void Eliminer(string code_emp)
        {
            try
            {
                EmployeDAL.Delete(code_emp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }


        public void InsererStatutCarriere(string code_emp, string code_dep, string code_grade, string code_poste, string debut, string motif)
        {
           
            try
            {
                StatutCarriere s = new StatutCarriere(code_dep, code_emp, int.Parse(code_grade), int.Parse(code_poste), DateTime.Parse(debut), motif);


                StatutCarriereDAL.AjouterStatut(s);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }


        public Payroll GetPayroll()
        {
            try
            {
                Payroll p = EmployeDAL.CalculerPayroll();
                return p;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable EmployesEnConge()
        {
            try
            {
                DataTable dt = EmployeDAL.Rapport(RapportGlobal.EmployesEnConge);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable AnniversaireAVenir()
        {
            try
            {
                DataTable dt = EmployeDAL.Rapport(RapportGlobal.AnniversaireProche);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }
    }
}
