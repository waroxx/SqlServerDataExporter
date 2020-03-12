using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDExporter.utilidades
{
    class Credentials{
        public Credentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    class DataSourceConnection
    {
        public string ServerName { get; set; }
        public string DataBaseName { get; set; }
        public string TableName { get; set; }
        public List<string> Tables { get; set; }
        public Credentials credentials { get; set; }
        private SqlConnection Connection { get; set; }
        private string ConnectionString { get; set; }
        public DataTable data { get; set; }

        //constructores
        public DataSourceConnection(string server, string dataBase)
        {
            ServerName = server;
            DataBaseName = dataBase;
            Connection = new SqlConnection();
        }
        public DataSourceConnection()
        {
        }

        //metodos publicos
        public bool testConnection()
        {
            Connection.ConnectionString = ConnectionString;
            try
            {
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

        private generateConnectionString()
        {
            ConnectionString = "Server="+ServerName+
                ";Database="+DataBaseName+
                (credentials!=null?";User Id="+credentials.UserName+";Password="+credentials.Password+";":"");
        }
        private void fetchTables()
        {
            Connection
        }
    }
}
