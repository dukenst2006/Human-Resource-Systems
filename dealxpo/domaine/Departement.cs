using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.levivoir.rh.domaine
{
    public class Departement
    {
        private string code_departement;
        private string code_employe;
        private string lib_departement;

        public Departement(string code_departement, string code_employe, string lib)
        {
            CodeDepartement = code_departement;
            CodeEmploye = code_employe;
            Name = lib;
        }

        public Departement()
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

        public string Name
        {
            get { return lib_departement; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) lib_departement = value; }
        }
        

    }
}
