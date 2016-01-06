using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.levivoir.rh.domaine
{
    public class Payroll
    {
        private string mois;
        private int annee;
        private string periode;

        public const double TauxAbattement = 0.1; //10%
        public const double TauxImpot = 0.1; //10%
        public const double TauxONA = 0.06; //6%
        
        List<Paiement> paiements;

        public Payroll()
        {
            this.paiements = new List<Paiement>();

            DateTime dt = DateTime.Now;
            System.Globalization.CultureInfo cult = new System.Globalization.CultureInfo("fr-FR");
            this.periode = dt.ToString("MMMM yyyy", cult);
            this.mois = dt.ToString("MMMM", cult);
            this.annee = dt.Year;
        }

        
        public String Mois
        {
            get { return this.mois; }
        }

        public int Annee
        {
            get { return this.annee; }
        }

        public String Periode
        {
            get { return this.periode; }
        }

        public void AjouterPaiement(string code, string nom, string prenom, double salmen)
        {
            Paiement p = new Paiement(code, nom, prenom, salmen);
            this.Paiements.Add(p);
        }

        public static double CalculerIri(double salaireImposable)
        {
            double iri = 0;
            double s = salaireImposable;

            if (s <= 60000)
            {
                iri = 0;
            }
            else if (s > 60000 && s <= 240000)
            {
                iri = (s - 60000) * 0.1;
            }
            else if (s > 240000 && s <= 480000)
            {
                iri = (s - 240000) * 0.15 + 18000;
            }
            else if (s > 480000 && s <= 10000000)
            {
                iri = (s - 480000) * 0.25 + 18000 + 36000;
            }
            else if (s > 1000000)
            {
                iri = (s - 1000000) * 0.25 + 18000 + 36000 + 130000;
            }

            iri /= 12;

            return iri;
        }

        public static double CalculerCFGDCT(double salaireMensuelle)
        {
            double s = salaireMensuelle;

            if (s >= 5000) return s * 0.01;
            else return 0.0;
        }

        public List<Paiement> Paiements
        {
            get { return this.paiements; }

        }

    }
}
