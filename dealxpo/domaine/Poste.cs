using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.levivoir.rh.domaine
{
    public class Poste
    {
        private int code_poste;
        private string lib_poste;
        private double salaire;

       
        public Poste(int code_poste, string code_employe, double salaire)
        {
            CodePoste = code_poste;
            Name = lib_poste;
            Salaire = salaire;
        }

        public Poste()
        {
        }

        public int CodePoste
        {
            get { return code_poste; }
            set { if (value > 0) code_poste = value; }
        }

        public double Salaire
        {
            get { return salaire; }
            set { if (value > 0 && value < 500000) salaire = value; }
        }

        public string Name
        {
            get { return lib_poste; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) lib_poste = value; }
        }


    }
}
