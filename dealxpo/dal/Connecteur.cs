using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace com.levivoir.rh.dal
{
    public class Connecteur
    {
        private static string server_name = "localhost";
        private SqlConnection conn;
        

        public Connecteur()
        {
            this.conn = new SqlConnection("Server=" + Connecteur.ServerName + ";Database=SGRH;Trusted_Connection=True");
        }

        public SqlConnection Connection
        {
            get { return this.conn; }
            set { this.conn = value; }
        }

        public static string ServerName
        {
            get { return Connecteur.server_name; }
        }


    }
}
