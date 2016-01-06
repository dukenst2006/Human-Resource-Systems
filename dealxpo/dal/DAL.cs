using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Windows;

namespace com.levivoir.rh.dal
{
    public enum RapportGlobal
    {
        EmployeAvecStatut = 0, ComptageEmployeParDepartement = 1, ContactEmploye = 2, Emplois = 3,
        EmployesEnConge = 4, PresenceJour = 5, PresenceMois = 6, AbsenceAnnee = 7, RetardAnnee = 8,
        AnniversaireProche = 9
    }
    
    public abstract class DAL
    {
        protected static string path = "C:/Database/RH/";

        protected static string selectDepartements =
              "SELECT CodeDepartement, libDepartement FROM Departement;";

        protected static string selectOneDepartement =
               "SELECT CodeDepartement, libDepartement FROM Departement;";

        protected static string selectGrades =
              "SELECT CodeGrade, 'Grade ' + libGrade as libGrade FROM Grade;";

        protected static string selectPostes =
              "SELECT CodePoste, libPoste FROM Poste;";

        //Anniversaire dans les 2 mois (62 jours) qui suivent
        protected static string selectAnniversaireProche =
                "SELECT emp.DateNaissance, emp.CodeEmploye, emp.Nom, emp.Prenom, emp.Sexe, " +
                "    pos.libPoste as Poste, gra.libGrade as Grade, dep.libDepartement as Departement " +
                "FROM " +
                "Employe as emp " +
                "INNER JOIN StatutCarriere as sta ON emp.CodeEmploye = sta.CodeEmploye " +
                "INNER JOIN Departement as dep ON sta.CodeDepartement = dep.CodeDepartement " +
                "INNER JOIN Poste as pos ON sta.CodePoste = pos.CodePoste " +
                "INNER JOIN Grade as gra ON sta.CodeGrade = gra.CodeGrade " +
                "WHERE sta.FinStatut is null " +
                "AND ( " +
			    "       (((month(emp.DateNaissance)-1)*30.4 + day(emp.DateNaissance)) - ((month(getdate())-1)*30.4 + day(getdate())) >= 0 " +
			    "       AND ((month(emp.DateNaissance)-1)*30.4 + day(emp.DateNaissance)) - ((month(getdate())-1)*30.4 + day(getdate())) <= 62) " +
		        "   OR  " +
			    "       (((month(emp.DateNaissance)-1)*30.4 + day(emp.DateNaissance)) - ((month(getdate())-1)*30.4 + day(getdate())) > -365 " +
			    "       AND ((month(emp.DateNaissance)-1)*30.4 + day(emp.DateNaissance)) - ((month(getdate())-1)*30.4 + day(getdate())) <= -303) " +
		        "   ) " +
                "ORDER BY dep.CodeDepartement;";

        protected static string selectEmployeWithStatut =
                "SELECT emp.CodeEmploye, emp.Nom, emp.Prenom, emp.Sexe, " +
                "    pos.libPoste as Poste, gra.libGrade as Grade, dep.libDepartement as Departement " +
                "FROM " +
                "Employe as emp " +
                "INNER JOIN StatutCarriere as sta ON emp.CodeEmploye = sta.CodeEmploye " +
                "INNER JOIN Departement as dep ON sta.CodeDepartement = dep.CodeDepartement " +
                "INNER JOIN Poste as pos ON sta.CodePoste = pos.CodePoste " +
                "INNER JOIN Grade as gra ON sta.CodeGrade = gra.CodeGrade " +
                "WHERE sta.FinStatut is null " +
                "ORDER BY dep.CodeDepartement;";

        protected static string selectComptageEmployeParDepartement =
                "SELECT dep.LibDepartement as Département, count(emp.CodeEmploye) as [Nombre Employés] " +
                "FROM " +
                "Departement as dep " +
                "    LEFT JOIN StatutCarriere as sta ON dep.CodeDepartement = sta.CodeDepartement " +
                "    LEFT JOIN Employe as emp ON sta.CodeEmploye = emp.CodeEmploye " +
                "WHERE sta.FinStatut IS NULL " +
                "GROUP BY dep.libDepartement " +
                "ORDER BY dep.libDepartement;";

        protected static string selectContactEmploye =
                "SELECT Nom, Prenom, Sexe, Telephone, Email, Address FROM Employe;";

        protected static string selectEmplois =
                "select * FROM dbo.determinerEmplois();";

        protected static string selectEmployesEnConge =
                "SELECT DISTINCT emp.CodeEmploye, emp.Nom, emp.Prenom, emp.Sexe, " +
                "    dep.libDepartement as Département, con.Type, con.Debut, con.RetourPrevu " +
                "FROM " +
                "Employe as emp " +
                "INNER JOIN Conge as con ON emp.CodeEmploye = con.CodeEmploye " +
                "INNER JOIN StatutCarriere as sta ON emp.CodeEmploye = sta.CodeEmploye " +
                "INNER JOIN Departement as dep ON sta.CodeDepartement = dep.CodeDepartement " +
                "WHERE con.RetourEffectif is null " +
                "ORDER BY dep.libDepartement;";

        protected static string insertFeuillePresence =
                "EXEC dbo.creerFeuillePresence @date;";

        protected static string selectPresenceJournaliere =
                "EXEC dbo.afficherFeuillePresence @date;";

        protected static string selectPresenceMensuelle =
                "EXEC dbo.afficherPresenceMensuelle @mois, @annee;";
        
        protected static string selectAbsenceAnnuelle =
                "EXEC dbo.afficherAbsencesAnnuels @annee;";

        protected static string selectRetardAnnuel =
                "EXEC dbo.afficherRetardsAnnuels @annee;";


        public static Boolean DatabaseExists()
        {
            SqlConnection conn = new SqlConnection("Server=" + Connecteur.ServerName + ";Trusted_Connection=True");

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM sysdatabases WHERE name = 'RH';", conn);
   
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                return dr.HasRows;
            }
            catch(SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                conn.Close();
            }

        }

        public static void CreerDatabase()
        {
            SqlConnection conn = new SqlConnection("Server=" + Connecteur.ServerName + ";Trusted_Connection=True");
            
            try
            {
                /*string script = File.ReadAllText("../../Database_Script.sql");
                SqlCommand cmd = new SqlCommand(script, conn);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd.ExecuteNonQuery();*/
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                conn.Close();
            }
        }


        public static void BackupDatabase()
        {
            SqlConnection conn = new SqlConnection("Server=" + Connecteur.ServerName + ";Database=master;Trusted_Connection=True");

            try
            {
                //Créons le Dossier backup s'il n'existe pas
                if(!Directory.Exists(DAL.path + "backup"))
                {
                    Directory.CreateDirectory(DAL.path + "backup");
                }

                //Ajoutons la date dans le nom du fichier
                string dateString = DateTime.Now.ToString("dd-MMM-yyyy_HH-mm-ss");
                string fileName = "RH_" + dateString + ".bak";

                string script = "BACKUP DATABASE [RH] TO  DISK = '" + DAL.path + "backup/" + fileName +"';";

                SqlCommand cmd = new SqlCommand(script, conn);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                conn.Close();
            }
        }

        public static void RestoreDatabase(string backupFileName)
        {
            SqlConnection conn = new SqlConnection("Server=" + Connecteur.ServerName + ";Database=master;Trusted_Connection=True");

            try
            {
                string script = "DROP DATABASE [RH]; RESTORE DATABASE [RH] FROM DISK = '" + backupFileName + "';";

                SqlCommand cmd = new SqlCommand(script, conn);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                conn.Close();
            }
        }
        

        public static DataTable AllDepartements()
        {
            Connecteur ct = new Connecteur();
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter dap = new SqlDataAdapter(DAL.selectDepartements, ct.Connection);
                
                ct.Connection.Open();
                dap.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }

            return dt;
        }


        public static DataTable AllGrades()
        {
            Connecteur ct = new Connecteur();
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter dap = new SqlDataAdapter(DAL.selectGrades, ct.Connection);
                
                ct.Connection.Open();
                dap.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }

            return dt;
        }


        public static DataTable AllPostes()
        {
            Connecteur ct = new Connecteur();
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter dap = new SqlDataAdapter(DAL.selectPostes, ct.Connection);
                
                ct.Connection.Open();
                dap.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex.Message + " - Code: " + ex.Number + " - Couche(DAL)");
            }
            finally
            {
                ct.Connection.Close();
            }

            return dt;
        }


        public static DataTable Rapport(RapportGlobal rg)
        {
            DataTable dt = new DataTable();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(); ;
                if (rg == RapportGlobal.EmployeAvecStatut)
                    da = new SqlDataAdapter(EmployeDAL.selectEmployeWithStatut, ct.Connection);
                else if (rg == RapportGlobal.ComptageEmployeParDepartement)
                    da = new SqlDataAdapter(EmployeDAL.selectComptageEmployeParDepartement, ct.Connection);
                else if (rg == RapportGlobal.ContactEmploye)
                    da = new SqlDataAdapter(EmployeDAL.selectContactEmploye, ct.Connection);
                else if (rg == RapportGlobal.Emplois)
                    da = new SqlDataAdapter(EmployeDAL.selectEmplois, ct.Connection);
                else if (rg == RapportGlobal.EmployesEnConge)
                    da = new SqlDataAdapter(EmployeDAL.selectEmployesEnConge, ct.Connection);
                else if (rg == RapportGlobal.AnniversaireProche)
                    da = new SqlDataAdapter(EmployeDAL.selectAnniversaireProche, ct.Connection);
                
                dt = new DataTable();

                ct.Connection.Open();

                if (da.SelectCommand.CommandText.Length > 0)
                {
                    da.Fill(dt);
                }
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

        public static DataTable RapportPresence(RapportGlobal rg, int annee, int mois = 0, int jour = 0)
        {
            DataTable dt = new DataTable();
            Connecteur ct = new Connecteur();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(); ;

                if (rg == RapportGlobal.RetardAnnee)
                {
                    da = new SqlDataAdapter(EmployeDAL.selectRetardAnnuel, ct.Connection);
                    da.SelectCommand.Parameters.AddWithValue("@annee", annee);
                }

                if (rg == RapportGlobal.AbsenceAnnee)
                {
                    da = new SqlDataAdapter(EmployeDAL.selectAbsenceAnnuelle, ct.Connection);
                    da.SelectCommand.Parameters.AddWithValue("@annee", annee);
                }

                if (rg == RapportGlobal.PresenceMois & mois > 0)
                {
                    da = new SqlDataAdapter(EmployeDAL.selectPresenceMensuelle, ct.Connection);
                    da.SelectCommand.Parameters.AddWithValue("@annee", annee);
                    da.SelectCommand.Parameters.AddWithValue("@mois", mois);
                }

                if (rg == RapportGlobal.PresenceJour & jour > 0)
                {
                    da = new SqlDataAdapter(EmployeDAL.selectPresenceJournaliere, ct.Connection);
                    DateTime date = DateTime.Parse(mois + "-" + jour + "-" + annee);
                    
                    da.SelectCommand.Parameters.AddWithValue("@date", date);
                }

                dt = new DataTable();

                ct.Connection.Open();

                if (da.SelectCommand.CommandText.Length > 0)
                {
                    da.Fill(dt);
                }
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


    }
}
