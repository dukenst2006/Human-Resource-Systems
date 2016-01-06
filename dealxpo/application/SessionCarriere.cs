using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using com.levivoir.rh.dal;
using com.levivoir.rh.domaine;


namespace com.levivoir.rh.application
{
    public class SessionCarriere : BaseSession
    {



        public void InsererStatutCarriere(string code_emp, string code_dep, string code_grade, string code_poste, string debut, string motif)
        {

            try
            {
                StatutCarriere s = new StatutCarriere(code_dep, code_emp, int.Parse(code_grade), int.Parse(code_poste), DateTime.Parse(debut), motif);


                StatutCarriereDAL.AjouterStatut(s);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }


        public void Revoquer(string code_emp, string fin_statut)
        {

            try
            {
                StatutCarriereDAL.MettreFinACarriere(code_emp, DateTime.Parse(fin_statut));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

    }
}
