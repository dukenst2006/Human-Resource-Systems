using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using com.levivoir.rh.dal;
using com.levivoir.rh.domaine;

namespace com.levivoir.rh.application
{
    public class SessionConge : BaseSession
    {



        public int TrouverAnneeFiscaleActuelle()
        {
            try
            {
                int annee = CongeDAL.AnneeFiscaleActuelle();
                return annee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }


        public void Inserer(string code_emp, string type, int annee, string debut, string retour_prevu)
        {
            try
            {
                Conge c = new Conge(code_emp, type, annee, DateTime.Parse(debut), DateTime.Parse(retour_prevu));

                CongeDAL.Insert(c);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public void ValiderRetourEffectif(int code_conge, string retour_effectif)
        {
            try
            {
                CongeDAL.ValiderRetour(code_conge, DateTime.Parse(retour_effectif));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        

    }
}
