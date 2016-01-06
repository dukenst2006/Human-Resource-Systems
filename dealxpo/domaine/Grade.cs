using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.levivoir.rh.domaine
{
    public class Grade
    {
        private int code_grade;
        private string lib_grade;
        private int pourcentage_salaire;


        public Grade(int code_grade, string code_employe, int pourcentage_salaire)
        {
            CodeGrade = code_grade;
            Name = lib_grade;
            PourcentageSalaire = pourcentage_salaire;
        }

        public Grade()
        {
        }

        public int CodeGrade
        {
            get { return code_grade; }
            set { if (value > 0) code_grade = value; }
        }

        public int PourcentageSalaire
        {
            get { return pourcentage_salaire; }
            set { if (value >= 0 && value <= 50) pourcentage_salaire = value; }
        }

        public string Name
        {
            get { return lib_grade; }
            set { if (value != null && value.Length > 0 && value.Length <= 30) lib_grade = value; }
        }

    }
}
