using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.levivoir.rh.dal;

namespace com.levivoir.rh.domaine
{
    public class StatutCarriere
    {
        private string code_departement;
        private string code_employe;
        private int code_grade;
        private int code_poste;
        private DateTime debut_statut;
        private DateTime fin_statut;
        private string motif_statut;
        

        public StatutCarriere(string code_departement, string code_employe, int code_grade, int code_poste,
                            DateTime debut, DateTime fin, string motif)
        {
            CodeDepartement = code_departement;
            CodeEmploye = code_employe;
            CodeGrade = code_grade;
            CodePoste = code_poste;
            Debut = debut;
            Fin = fin;
            Motif = motif;

        }

        public StatutCarriere(string code_departement, string code_employe, int code_grade, int code_poste,
                            DateTime debut, string motif)
        {
            CodeDepartement = code_departement;
            CodeEmploye = code_employe;
            CodeGrade = code_grade;
            CodePoste = code_poste;
            Debut = debut;
            Motif = motif;

        }

        public StatutCarriere()
        {

        }
        

        public string CodeDepartement
        {
            get { return code_departement; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) code_departement = value; }
        }

        public string CodeEmploye
        {
            get { return code_employe; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) code_employe = value; }
        }

        public int CodeGrade
        {
            get { return code_grade; }
            set { if (value > 0) code_grade = value; }
        }

        public int CodePoste
        {
            get { return code_poste; }
            set { if (value > 0) code_poste = value; }
        }

        public DateTime Debut
        {
            get { return debut_statut; }
            set { debut_statut = value; }
        }

        public DateTime Fin
        {
            get { return fin_statut; }
            set { fin_statut = value; }
        }

        public String Motif
        {
            get { return motif_statut; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) motif_statut = value; }
        }

        public Departement getDepartement()
        {
            return StatutCarriereDAL.GetDepartement(this.CodeDepartement);
        }

        public Employe getEmploye()
        {
            return null; //A implementer si necessaire
        }

        public Grade getGrade()
        {
            return StatutCarriereDAL.GetGrade(this.CodeGrade);
        }

        public Poste getPoste()
        {
            return StatutCarriereDAL.GetPoste(this.CodePoste);
        }


    }
}
