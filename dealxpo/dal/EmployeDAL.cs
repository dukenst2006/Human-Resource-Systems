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

    public class EmployeDAL : DAL
    {

        private static string selectAllEmploye =
                "SELECT * FROM Employe;";

        private static string selectOneEmploye =
                "SELECT * FROM Employe " +
                "WHERE CodeEmploye = @CodeEmploye;";

        private static string selectEmployeByNom =
                "SELECT * FROM Employe " +
                "WHERE Nom like @Name OR Prenom like @Name;";

        private static string selectEmployeByCodeAndNom =
                "SELECT * FROM Employe " +
                "WHERE CodeEmploye like @Name OR Nom like @Name OR Prenom like @Name;";

        private static string insertEmploye =
                "INSERT INTO " +
                "Employe(CodeEmploye, Nom, Prenom, Sexe, DateNaissance, Telephone, Email, Address, SurplusSalaire) " +
                "VALUES(@CodeEmploye, @Nom, @Prenom, @Sexe, @DateNaissance, @Telephone, @Email, @Adresse, @SurplusSalaire);";

        private static string updateEmploye =
               "UPDATE Employe " +
               "SET Nom = @Nom, Prenom = @Prenom, Sexe = @Sexe, DateNaissance = @DateNaissance, " +
               "Telephone = @Telephone, Email = @Email, Address = @Adresse, SurplusSalaire = @SurplusSalaire " +
               "WHERE CodeEmploye = @CodeEmploye;";

        private static string deleteEmploye =
                "DELETE FROM Employe " +
                "WHERE CodeEmploye = @CodeEmploye;";

        private static string selectEmployeSalaire =
                "SELECT emp.CodeEmploye, emp.Nom, emp.Prenom, " +
                "   (pos.SalaireBase + emp.SurplusSalaire + (pos.SalaireBase * gra.PourcentageSalaire)/100 ) as SalaireMensuelle " +
                "FROM Employe as emp " +
                "   INNER JOIN StatutCarriere as stc ON emp.CodeEmploye = stc.CodeEmploye " +
                "   INNER JOIN Poste as pos ON stc.CodePoste = pos.CodePoste " +
                "   INNER JOIN Grade as gra ON stc.CodeGrade = gra.CodeGrade " +
                "WHERE stc.FinStatut IS NULL " +
                "ORDER BY emp.CodeEmploye;";


          
        public EmployeDAL()
        {

        }

        public static List<Employe> All()
        {
            List<Employe> employeList = new List<Employe>();
            Connecteur ct = new Connecteur();

            try
            {
                SqlDataAdapter dap = new SqlDataAdapter(EmployeDAL.selectAllEmploye, ct.Connection);
                
                DataTable dt = new DataTable("Employe");

                ct.Connection.Open();
                dap.Fill(dt);

                Employe e;
                foreach (DataRow dr in dt.Rows)
                {
                    e = new Employe();
                    EmployeDAL.Hydrate(e, dr);
                    employeList.Add(e);
                }

                return employeList;
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


        public static Employe TrouverEmploye(string code)
        {
            Employe e = new Employe();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(EmployeDAL.selectOneEmploye, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@CodeEmploye", code);

                
                DataTable dt = new DataTable("Employe");

                ct.Connection.Open();
                dae.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    EmployeDAL.Hydrate(e, dr);
                }

                return e;

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


        public static DataTable Trouver(string code)
        {
            DataTable dt = new DataTable("Employe");
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(EmployeDAL.selectOneEmploye, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@CodeEmploye", code);

                dt = new DataTable();

                ct.Connection.Open();
                dae.Fill(dt);

                return dt;

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

        public static DataTable RechercherParNom(string keyWords)
        {
            List<Employe> employeList = new List<Employe>();
            DataTable dt = new DataTable("Employe");

            Connecteur ct = new Connecteur();

            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(EmployeDAL.selectEmployeByNom, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@Name", "%"+keyWords+"%");

                ct.Connection.Open();
                dae.Fill(dt);
                
                return dt;
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


        public static DataTable RechercherParCodeEtNom(string keyWords)
        {
            DataTable dt = new DataTable("Employe");
            Connecteur ct = new Connecteur();

            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(EmployeDAL.selectEmployeByCodeAndNom, ct.Connection);
                dae.SelectCommand.Parameters.AddWithValue("@Name", "%" + keyWords + "%");

                ct.Connection.Open();
                dae.Fill(dt);

                return dt;
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

        public static Payroll CalculerPayroll()
        {
            DataTable dt = new DataTable("EmployeSalaire");
            Connecteur ct = new Connecteur();

            try
            {
                SqlDataAdapter dae = new SqlDataAdapter(EmployeDAL.selectEmployeSalaire, ct.Connection);

                ct.Connection.Open();
                dae.Fill(dt);

                Payroll payroll = new Payroll();

                foreach (DataRow dr in dt.Rows)
                {
                    payroll.AjouterPaiement(dr["CodeEmploye"].ToString(), dr["Nom"].ToString(),
                                    dr["Prenom"].ToString(), double.Parse(dr["SalaireMensuelle"].ToString()));
                }

                return payroll;
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
        

        public static void Insert(Employe emp, StatutCarriere stc)
        {
            Connecteur ct = new Connecteur();

            
            if (ct.Connection.State == ConnectionState.Closed)
                ct.Connection.Open();
                
            //----Begin Transaction---
            SqlTransaction trans = ct.Connection.BeginTransaction();

            try
            {
                //Proceed Database Command-------------------------------------
                SqlCommand employeCmd =
                    new SqlCommand(EmployeDAL.insertEmploye, ct.Connection, trans);

                SqlCommand carriereCmd =
                    new SqlCommand(StatutCarriereDAL.InsertStatutCarriere, ct.Connection, trans);

                //1 - INSERT Employe
                //Employe Params - Insert
                EmployeDAL.setEmployeParameters(employeCmd.Parameters, emp);
                employeCmd.ExecuteNonQuery();

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


        public static void Update(Employe emp, StatutCarriere stc)
        {
            Connecteur ct = new Connecteur();
            
            if (ct.Connection.State == ConnectionState.Closed)
                ct.Connection.Open();

            //----Begin Transaction---
            SqlTransaction trans = ct.Connection.BeginTransaction();

            try
            {
                //Proceed Database Command-------------------------------------
                SqlCommand employeCmd =
                    new SqlCommand(EmployeDAL.updateEmploye, ct.Connection, trans);

                SqlCommand carriereCmd =
                    new SqlCommand(StatutCarriereDAL.UpdateStatutCarriere, ct.Connection, trans);
                
                //1 - Update Employe
                //Employe Params
                EmployeDAL.setEmployeParameters(employeCmd.Parameters, emp);
                employeCmd.ExecuteNonQuery();

                //2 - Update StatutCarriere
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


        public static void Delete(string code_employe)
        {
            Connecteur ct = new Connecteur();

            try
            {
                //Proceed Database Command-------------------------------------
                if (ct.Connection.State == ConnectionState.Closed)
                    ct.Connection.Open();

                SqlCommand employeCmd =
                    new SqlCommand(EmployeDAL.deleteEmploye, ct.Connection);
                
                employeCmd.Parameters.AddWithValue("@CodeEmploye", code_employe);

                employeCmd.ExecuteNonQuery();

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


        public static void Hydrate(Employe e, DataRow dr)
        {
            //Employe
            e.CodeEmploye = dr["CodeEmploye"].ToString();

            e.Prenom = dr["Prenom"].ToString();
            e.Nom = dr["Nom"].ToString();
            e.Sexe = dr["Sexe"].ToString();

            if (!dr["DateNaissance"].Equals(null))
            e.DateNaissance = (DateTime)dr["DateNaissance"];

            e.Telephone = dr["Telephone"].ToString();
            e.Email = dr["Email"].ToString();
            e.Adresse = dr["Address"].ToString();
            e.SurplusSalaire = Double.Parse(dr["SurplusSalaire"].ToString());
        }


        public static void setEmployeParameters(SqlParameterCollection pc, Employe e)
        {
            pc.AddWithValue("@CodeEmploye", e.CodeEmploye);
            pc.AddWithValue("@Nom", e.Nom);
            pc.AddWithValue("@Prenom", e.Prenom);
            pc.AddWithValue("@Sexe", e.Sexe);
            pc.AddWithValue("@DateNaissance", e.DateNaissance);

            pc.AddWithValue("@Telephone", (e.Telephone == null) ? "" : e.Telephone);
            pc.AddWithValue("@Email", (e.Email == null) ? "" : e.Email);
            pc.AddWithValue("@Adresse", (e.Adresse == null)? "" : e.Adresse);
            pc.AddWithValue("@SurplusSalaire", e.SurplusSalaire);

        }


        

    }
}
