using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.levivoir.rh.domaine
{
    public class Manager : Employe
    {
        private string code_departement;

        private Departement departement_dirige;

        public Manager(string code_departement)
        {
            CodeDepartement = code_departement;

            DepartementDirige = new Departement();
        }

        public string CodeDepartement
        {
            get { return code_departement; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) code_departement = value; }
        }

        public Departement DepartementDirige
        {
            get { return departement_dirige; }
            set { if (value != null) departement_dirige = value; }
        }
    }
}
