using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.levivoir.rh.dal;

namespace com.levivoir.rh.domaine
{
    public class Employe
    {

		private string code_employe;
        private string prenom;
        private string nom;
        private string sexe;
        private DateTime date_naissance;
        private string telephone;
        private string email;
        private string adresse;
        private double surplus_salaire;

        private Departement departement;
        private List<Conge> conges;
        //private List<StatutCarriere> statut_carrieres;
        //private StatutCarriere statut_actuel;

        public Employe(string code, string nom, string prenom, string sexe, DateTime date_naissance,
                    string telephone, string email, string adresse, double surplus_salaire)
        {
            CodeEmploye = code;
            Nom = nom;
            Prenom = prenom;
            Sexe = sexe;
            DateNaissance = date_naissance;
            Telephone = telephone;
            Email = email;
            Adresse = adresse;
            SurplusSalaire = surplus_salaire;

            //this.Departement = new Departement();
            //this.Conges = new List<Conge>();
            //this.StatutCarrieres = new List<StatutCarriere>();
        }

        public Employe(string nom, string prenom, string sexe, DateTime date_naissance,
                    string telephone, string email, string adresse, double surplus_salaire)
        {
            Nom = nom;
            Prenom = prenom;
            Sexe = sexe;
            DateNaissance = date_naissance;
            Telephone = telephone;
            Email = email;
            Adresse = adresse;
            SurplusSalaire = surplus_salaire;

            //this.Departement = new Departement();
            //this.Conges = new List<Conge>();
            //this.StatutCarrieres = new List<StatutCarriere>();
        }

        
        
        public Employe()
        {
        }


        public string CodeEmploye
        {
            get { return code_employe.Replace(" ", ""); }
            set { if(value.Length >= 0 && value.Length <= 30) code_employe = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { if (value.Length >= 0 && value.Length <= 30) prenom = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { if (value.Length >= 0 && value.Length <= 40) nom = value; }
        }

        public string Sexe
        {
            get { return sexe; }
            set { if (value == new String('M', 1) | value == new string('F',1)) sexe = value; }
        }

        public DateTime DateNaissance
        {
            get { return date_naissance; }
            set { date_naissance = value; }
        }

        public string Telephone
        {
            get { return telephone; }
            set { if (value.Length >= 0 && value.Length <= 15) telephone = value; }
        }
        
        public string Email
        {
            get { return email; }
            set { if (value.Length >= 0 && value.Length <= 50) email = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { if (value != null && value.Length > 0 && value.Length <= 50) adresse = value; }
        }

        public Double SurplusSalaire
        {
            get { return surplus_salaire; }
            set { if (value >= 0 && value < 30000) surplus_salaire = value; }
        }

       
        public Departement getDepartement()
        {
            return this.departement;
        }

        public List<Conge> getConges()
        {
            return CongeDAL.getConges(this.CodeEmploye);
        }

        public bool dernierRetourEffectifValide()
        {
            return CongeDAL.RetourEffectifValide(this.CodeEmploye);
        }

        public Conge getCongeOuvert()
        {
            List<Conge> list = this.getConges();
            foreach (Conge c in list)
            {
                if (c.RetourEffectif < DateTime.Parse("1/1/2008"))
                    return c;
            }
            return null;
        }

        public List<StatutCarriere> getStatutCarrieres()
        {
            return StatutCarriereDAL.GetStatutCarrieres(this.CodeEmploye);
        }

        public StatutCarriere getStatutActuel()
        {
            return StatutCarriereDAL.GetStatutCarriereActuel(this.CodeEmploye);
        }

        public bool EstRevoque()
        {
            StatutCarriere stc = StatutCarriereDAL.GetStatutCarriereActuel(this.CodeEmploye);

            return (stc.Fin > DateTime.MinValue) ? true : false;
        }

    }
}
