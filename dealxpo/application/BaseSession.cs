using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using com.levivoir.rh.dal;
using com.levivoir.rh.domaine;


namespace com.levivoir.rh.application
{
    public abstract class BaseSession
    {

        public void VerifierDatabase()
        {
            try
            {
                bool ok = DAL.DatabaseExists();

                //Si la Base de Donnees n'existe pas, alors creer le
                if (!ok)
                {
                    DAL.CreerDatabase();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public void BackupDatabase()
        {
            try
            {
                bool ok = DAL.DatabaseExists();

                //Si la Base de Donnees existe, alors faire le backup
                if (ok)
                {
                    DAL.BackupDatabase();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public void RestaurerDatabase(string backupFileName)
        {
            try
            {
                bool ok = DAL.DatabaseExists();

                //Si la Base de Donnees existe, alors faire le backup
                if (ok)
                {
                    DAL.RestoreDatabase(backupFileName);
                }
                else
                {
                    throw new Exception("La Base de Données n'existe pas! - Couche(APP)");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable AllDepartements()
        {
            try
            {
                DataTable dt = DAL.AllDepartements();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable AllGrades()
        {
            try
            {
                DataTable dt = DAL.AllGrades();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable AllPostes()
        {
            try
            {
                DataTable dt = DAL.AllPostes();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }


        public DataTable EmployeAvecStatut()
        {
            try
            {
                DataTable dt = EmployeDAL.Rapport(RapportGlobal.EmployeAvecStatut);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable ComptageEmployeParDepartement()
        {
            try
            {
                DataTable dt = EmployeDAL.Rapport(RapportGlobal.ComptageEmployeParDepartement);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable ContactEmploye()
        {
            try
            {
                DataTable dt = EmployeDAL.Rapport(RapportGlobal.ContactEmploye);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable Emplois()
        {
            try
            {
                DataTable dt = EmployeDAL.Rapport(RapportGlobal.Emplois);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable PresenceJournaliere(DateTime date)
        {
            try
            {
                DataTable dt = EmployeDAL.RapportPresence(RapportGlobal.PresenceJour, date.Year, date.Month, date.Day);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable PresenceMensuelle(DateTime date)
        {
            try
            {
                DataTable dt = EmployeDAL.RapportPresence(RapportGlobal.PresenceMois, date.Year, date.Month);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable RetardAnnuel(DateTime date)
        {
            try
            {
                DataTable dt = EmployeDAL.RapportPresence(RapportGlobal.RetardAnnee, date.Year);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }

        public DataTable AbsenceAnnuelle(DateTime date)
        {
            try
            {
                DataTable dt = EmployeDAL.RapportPresence(RapportGlobal.AbsenceAnnee, date.Year);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - Couche(APP)");
            }
        }
        
    }
}
