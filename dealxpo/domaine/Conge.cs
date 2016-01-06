using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.levivoir.rh.domaine
{
    public class Conge
    {
        private int code_conge;
        private string code_employe;
        private string type;
        private int annee;
        private DateTime debut;
        private DateTime retour_prevu;
        private DateTime retour_effectif;

        private Employe employe;


        public Conge(int code_conge, string code_employe, string type, int annee,
                            DateTime debut, DateTime retour_prevu, DateTime retour_effectif)
        {
            CodeConge = code_conge;
            CodeEmploye = code_employe;
            Type = type;
            AnneeFiscale = annee;
            Debut = debut;
            RetourPrevu = retour_prevu;
            RetourEffectif = retour_effectif;
        }


        public Conge(string code_employe, string type, int annee,
                            DateTime debut, DateTime retour_prevu)
        {
            CodeEmploye = code_employe;
            Type = type;
            AnneeFiscale = annee;
            Debut = debut;
            RetourPrevu = retour_prevu;
        }
        
        public Conge()
        {
        }


        public int CodeConge
        {
            get { return code_conge; }
            set { if (value > 0) code_conge = value; }
        }

        public string CodeEmploye
        {
            get { return code_employe; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) code_employe = value; }
        }

        public string Type
        {
            get { return type; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) type = value; }
        }

        public int AnneeFiscale
        {
            get { return annee; }
            set { if (value > 0) annee = value; }
        }


        public DateTime Debut
        {
            get { return debut; }
            set { debut = value; }
        }

        public DateTime RetourPrevu
        {
            get { return retour_prevu; }
            set { retour_prevu = value; }
        }

        public DateTime RetourEffectif
        {
            get { return retour_effectif; }
            set { retour_effectif = value; }
        }
        
        public Employe Employe
        {
            get { return this.employe; }
            set { if (value != null) this.employe = value; }
        }


    }
}
