using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using com.levivoir.rh.domaine;


namespace com.levivoir.rh.dal
{
    public class CongeDAL : DAL
    {
        
        private static string selectEmployeConges =
                "SELECT * FROM Conge " +
                "WHERE CodeEmploye = @CodeEmploye;";

        private static string selectActualEmployeConge =
                "SELECT * FROM Conge " +
                "WHERE CodeEmploye = @CodeEmploye AND RetourEffectif IS NULL;";

        private static string insertConge =
                "INSERT INTO " +
                "Conge(CodeEmploye, Type, Annee, Debut, RetourPrevu) " +
                "VALUES( @CodeEmploye, @Type, @Annee, @Debut, @RetourPrevu)";

        private static string updateConge =
                "UPDATE Conge " +
                "SET Type = @Type, Annee = @Annee, " +
                "Debut = @Debut, RetourPrevu = @RetourPrevu " +
                "WHERE CodeConge = @CodeConge;";

        private static string updateRetourEffectifConge =
                "UPDATE Conge " +
                "SET RetourEffectif = @RetourEffectif " +
                "WHERE CodeConge = @CodeConge;";


        private static string deleteConge =
                "DELETE FROM Conge " +
                "WHERE CodeConge = @CodeConge;";

        //Procedures et Fonctions Database
        private static string anneFiscaleActuelle =
                "SELECT dbo.anneeFiscaleActuelle();";

        private static string retourEffectifValide =
                "SELECT dbo.retourEffectifDernierConge(0, @codeEmploye);";




        public static int AnneeFiscaleActuelle()
        {
            Connecteur ct = new Connecteur();

            try
            {
                ct.Connection.Open();
                SqlDataAdapter dap = new SqlDataAdapter(CongeDAL.anneFiscaleActuelle, ct.Connection);
                int annee = int.Parse(dap.SelectCommand.ExecuteScalar().ToString());
                return annee;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }
        }


        public static void ValiderRetour(int code, DateTime retour_effectif)
        {
            Connecteur ct = new Connecteur();

            //Proceed Database Command-------------------------------------
            if (ct.Connection.State == ConnectionState.Closed)
                ct.Connection.Open();

            SqlCommand cmd =
                new SqlCommand(CongeDAL.updateRetourEffectifConge, ct.Connection);
            
            try
            {
                //1 - Update Conge (RetourEffectif)
                //Conge Params - Update
                cmd.Parameters.AddWithValue("@CodeConge", code);
                cmd.Parameters.AddWithValue("@RetourEffectif", retour_effectif);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }

        }


        public static void Insert(Conge c)
        {
            Connecteur ct = new Connecteur();

            //Proceed Database Command-------------------------------------
            if (ct.Connection.State == ConnectionState.Closed)
                ct.Connection.Open();

            SqlCommand cmd =
                new SqlCommand(CongeDAL.insertConge, ct.Connection);

            try
            {
                //1 - INSERT Conge
                //Conge Params - Insert
                CongeDAL.setCongeParameters(cmd.Parameters, c);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }

        }
        


        public static List<Conge> getConges(string code_employe)
        {
            List<Conge> lCarriere = new List<Conge>();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(CongeDAL.selectEmployeConges, ct.Connection);
                da.SelectCommand.Parameters.AddWithValue("@CodeEmploye", code_employe);

                DataTable dt = new DataTable("Conge");

                ct.Connection.Open();
                da.Fill(dt);

                Conge s;

                foreach (DataRow dr in dt.Rows)
                {
                    s = new Conge();
                    CongeDAL.Hydrate(s, dr);
                    lCarriere.Add(s);
                }

                return lCarriere;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }
        }

        public static bool RetourEffectifValide(string code)
        {
            Connecteur ct = new Connecteur();

            try
            {
                ct.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(CongeDAL.retourEffectifValide, ct.Connection);
                da.SelectCommand.Parameters.AddWithValue("@CodeEmploye", code);

                string dateString = da.SelectCommand.ExecuteScalar().ToString();

                if (dateString == "") return false;

                DateTime date = DateTime.Parse(dateString);

                return date > DateTime.Parse("01/01/2008") ? true : false;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }
        }

        public static void Hydrate(Conge c, DataRow dr)
        {
            //Statut Carriere
            c.CodeConge = (int)dr["CodeConge"];
            c.CodeEmploye = dr["CodeEmploye"].ToString();
            c.Type = dr["Type"].ToString();
            c.AnneeFiscale = (int)dr["Annee"];

            if (!dr["Debut"].ToString().Equals(""))
                c.Debut = (DateTime)dr["Debut"];

            if (!dr["RetourPrevu"].ToString().Equals(""))
                c.RetourPrevu = (DateTime)dr["RetourPrevu"];

            if (!dr["RetourEffectif"].ToString().Equals(""))
                c.RetourEffectif = (DateTime)dr["RetourEffectif"];
        }

        public static void setCongeParameters(SqlParameterCollection pc, Conge c)
        {
            pc.AddWithValue("@CodeEmploye", c.CodeEmploye);
            pc.AddWithValue("@Type", c.Type);
            pc.AddWithValue("@Annee", c.AnneeFiscale);
            pc.AddWithValue("@Debut", c.Debut);
            pc.AddWithValue("@RetourPrevu", c.RetourPrevu);
        }




    }
}
