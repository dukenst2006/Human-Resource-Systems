using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using com.levivoir.rh.domaine;


namespace com.levivoir.rh.dal
{
    public class StatutCarriereDAL : DAL
    {

        private static string selectAllStatutCarriere =
                "SELECT * FROM StatutCarriere;";

        private static string selectEmployeStatutCarrieres =
                "SELECT * FROM StatutCarriere " +
                "WHERE CodeEmploye = @CodeEmploye;";

        private static string selectActualEmployeStatutCarriere =
                "SELECT * FROM StatutCarriere " +
                "WHERE CodeEmploye = @CodeEmploye " +
                "ORDER BY DebutStatut DESC;";

        private static string insertStatutCarriere =
                "INSERT INTO " +
                "StatutCarriere(CodeDepartement, CodeEmploye, CodeGrade, CodePoste, DebutStatut, MotifStatut) " +
                "VALUES(@CodeDepartement, @CodeEmploye, @CodeGrade, @CodePoste, @DebutStatut, @MotifStatut)";

        private static string updateStatutCarriere =
                "UPDATE StatutCarriere " +
                "SET CodeDepartement = @CodeDepartement, CodeGrade = @CodeGrade, " +
                "CodePoste = @CodePoste, DebutStatut = @DebutStatut, MotifStatut = @MotifStatut " +
                "WHERE CodeEmploye = @CodeEmploye AND FinStatut IS NULL;";

        private static string updateAncienStatut =
                "UPDATE StatutCarriere " +
                "SET FinStatut = @FinStatut " +
                "WHERE CodeEmploye = @CodeEmploye AND FinStatut IS NULL;";
               
        private static string deleteStatutCarriere =
              "DELETE FROM StatutCarriere " +
              "WHERE DebutStatut = @DebutStatut AND FinStatut IS NULL;";

        private static string selectDepartement =
                "SELECT * FROM Departement " +
                "WHERE CodeDepartement = @CodeDepartement;";

        private static string selectGrade =
                "SELECT * FROM Grade " +
                "WHERE CodeGrade = @CodeGrade;";

        private static string selectPoste =
                "SELECT * FROM Poste " +
                "WHERE CodePoste = @CodePoste;";



        public StatutCarriereDAL()
        {

        }


        public static string InsertStatutCarriere
        {
            get { return StatutCarriereDAL.insertStatutCarriere; }
        }

        public static string UpdateStatutCarriere
        {
            get { return StatutCarriereDAL.updateStatutCarriere; }
        }


        public static void AjouterStatut(StatutCarriere stc)
        {
            Connecteur ct = new Connecteur();

            //Proceed Database Command-------------------------------------
            if (ct.Connection.State == ConnectionState.Closed)
                ct.Connection.Open();

            //----Begin Transaction---
            SqlTransaction trans = ct.Connection.BeginTransaction();

            SqlCommand ancienCarriereCmd =
                new SqlCommand(StatutCarriereDAL.updateAncienStatut, ct.Connection, trans);

            SqlCommand carriereCmd =
                new SqlCommand(StatutCarriereDAL.insertStatutCarriere, ct.Connection, trans);

            try
            {
                //1 - Update FinStatut de l'Ancien Statut
                //Employe Params - Insert
                ancienCarriereCmd.Parameters.AddWithValue("@CodeEmploye", stc.CodeEmploye);
                ancienCarriereCmd.Parameters.AddWithValue("@FinStatut", stc.Debut);
                ancienCarriereCmd.ExecuteNonQuery();

                //2 - INSERT StatutCarriere
                //StatutCarriere Params  - Insert
                StatutCarriereDAL.setStatutCarriereParameters(carriereCmd.Parameters, stc);
                carriereCmd.ExecuteNonQuery();

                //Commit Transaction
                trans.Commit();

                //-------------------------------------------------------------
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }

        }


        public static void MettreFinACarriere(string code_emp, DateTime fin_statut)
        {
            Connecteur ct = new Connecteur();

            //Proceed Database Command-------------------------------------
            if (ct.Connection.State == ConnectionState.Closed)
                ct.Connection.Open();

            SqlCommand ancienCarriereCmd =
                new SqlCommand(StatutCarriereDAL.updateAncienStatut, ct.Connection);

            try
            {
                //1 - Update FinStatut de l'Ancien Statut
                //Employe Params - Insert
                ancienCarriereCmd.Parameters.AddWithValue("@CodeEmploye", code_emp);
                ancienCarriereCmd.Parameters.AddWithValue("@FinStatut", fin_statut);
                ancienCarriereCmd.ExecuteNonQuery();

                //-------------------------------------------------------------
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


        public static List<StatutCarriere> GetStatutCarrieres(string code)
        {
            List<StatutCarriere> lCarriere = new List<StatutCarriere>();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(StatutCarriereDAL.selectEmployeStatutCarrieres, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@CodeEmploye", code);

                DataTable dt = new DataTable("StatutCarriere");

                ct.Connection.Open();
                dae.Fill(dt);

                StatutCarriere s;

                foreach(DataRow dr in dt.Rows)
                {
                    s = new StatutCarriere();
                    StatutCarriereDAL.Hydrate(s, dr);
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


        public static StatutCarriere GetStatutCarriereActuel(string code)
        {
            StatutCarriere s = new StatutCarriere();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(StatutCarriereDAL.selectActualEmployeStatutCarriere, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@CodeEmploye", code);

                DataTable dt = new DataTable("StatutCarriere");

                ct.Connection.Open();
                dae.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    StatutCarriereDAL.Hydrate(s, dr);
                }

                return s;
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
        

        public static Departement GetDepartement(string code_departement)
        {
            Departement d = new Departement();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(StatutCarriereDAL.selectDepartement, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@CodeDepartement", code_departement);

                DataTable dt = new DataTable("Departement");

                ct.Connection.Open();
                dae.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    StatutCarriereDAL.Hydrate(d, dr);
                }

                return d;
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


        public static Grade GetGrade(int code_grade)
        {
            Grade g = new Grade();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(StatutCarriereDAL.selectGrade, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@CodeGrade", code_grade);

                DataTable dt = new DataTable("Grade");

                ct.Connection.Open();
                dae.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    StatutCarriereDAL.Hydrate(g, dr);
                }

                return g;
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


        public static Poste GetPoste(int code_poste)
        {
            Poste d = new Poste();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(StatutCarriereDAL.selectPoste, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@CodePoste", code_poste);

                DataTable dt = new DataTable("Poste");

                ct.Connection.Open();
                dae.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    StatutCarriereDAL.Hydrate(d, dr);
                }

                return d;
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


        public static void Hydrate(StatutCarriere s, DataRow dr)
        {
            //Statut Carriere
            s.CodeEmploye = dr["CodeEmploye"].ToString();
            s.CodeDepartement = dr["CodeDepartement"].ToString();
            s.CodeGrade = (int)dr["CodeGrade"];
            s.CodePoste = (int)dr["CodePoste"];

            if (!dr["DebutStatut"].ToString().Equals(""))
                s.Debut = (DateTime)dr["DebutStatut"];

            if (!dr["FinStatut"].ToString().Equals(""))
                s.Fin = (DateTime)dr["FinStatut"];

            s.Motif = dr["MotifStatut"].ToString();
        }

        public static void Hydrate(Departement d, DataRow dr)
        {
            //Departement
            d.CodeDepartement = dr["CodeDepartement"].ToString();
            d.CodeEmploye = dr["CodeEmploye"].ToString();
            d.Name = dr["libDepartement"].ToString();
        }

        public static void Hydrate(Grade g, DataRow dr)
        {
            //Grade
            g.CodeGrade = (int)dr["CodeGrade"];
            g.Name = dr["libGrade"].ToString();
            g.PourcentageSalaire = (int)dr["PourcentageSalaire"];
        }

        public static void Hydrate(Poste p, DataRow dr)
        {
            //Poste
            p.CodePoste = (int)dr["CodePoste"];
            p.Name = dr["libPoste"].ToString();
            p.Salaire = double.Parse(dr["SalaireBase"].ToString());
        }

        public static void setStatutCarriereParameters(SqlParameterCollection pc, StatutCarriere s)
        {
            pc.AddWithValue("@CodeDepartement", s.CodeDepartement);
            pc.AddWithValue("@CodeEmploye", s.CodeEmploye);
            pc.AddWithValue("@CodeGrade", s.CodeGrade);
            pc.AddWithValue("@CodePoste", s.CodePoste);
            pc.AddWithValue("@DebutStatut", s.Debut);

            pc.AddWithValue("@MotifStatut", s.Motif);
        }

        

    }
}
