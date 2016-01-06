using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.levivoir.rh.domaine
{
    public class Paiement
    {
        private string code, nom, prenom;
        private double salmen, salan, abattement, salimp, iri, cfgdct, ona, deduction, salnet;


        public Paiement(string code, string nom, string prenom, double salmen)
        {
            this.code = code;
            this.nom = nom;
            this.prenom = prenom;

            this.salmen = salmen;
            this.salan = this.salmen * 12;
             
            this.abattement = this.salan * Payroll.TauxAbattement;
            this.salimp = this.salan * Payroll.TauxImpot;

            this.iri = Payroll.CalculerIri(this.salimp);

            this.cfgdct = Payroll.CalculerCFGDCT(this.salmen);

            this.ona = this.salmen * Payroll.TauxONA;

            this.deduction = this.iri + this.cfgdct + this.ona;

            this.salnet = this.salmen - this.deduction;
        }

        public string Code
        {
            get { return this.code; }
        }

        public string Nom
        {
            get { return this.nom; }
        }

        public string Prenom
        {
            get { return this.prenom; }
        }

        public double SalaireMensuel
        {
            get { return this.salmen; }
        }

        public double SalaireAnnuel
        {
            get { return this.salan; }
        }

        public double Abattement
        {
            get { return this.abattement; }
        }

        public double IRI
        {
            get { return this.iri; }
        }

        public double CFGDCT
        {
            get { return this.cfgdct; }
        }

        public double ONA
        {
            get { return this.ona; }
        }

        public double Deduction
        {
            get { return this.deduction; }
        }

        public double SalaireNET
        {
            get { return this.salnet; }
        }

    }
}
