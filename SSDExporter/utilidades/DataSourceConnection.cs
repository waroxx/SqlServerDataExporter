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
        public DataSourceConnection(string server, string dataBase,Credentials credentials)
        {
            ServerName = server;
            DataBaseName = dataBase;
            this.credentials = credentials;
            Connection = new SqlConnection();
        }
        public DataSourceConnection(string server, string dataBase)
        {
            ServerName = server;
            DataBaseName = dataBase;
            this.credentials = credentials;
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
                fetchTables();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

        private void generateConnectionString(bool securityType)
        {
            ConnectionString = "Server="+ServerName+
                ";Database="+DataBaseName+
                (securityType?";User Id="+credentials.UserName+";Password="+credentials.Password+";": ";Integrated Security=True;");
        }
        private void fetchTables()
        {
            SqlCommand command = new SqlCommand("SELECT table_name FROM information_schema.tables");
            SqlDataReader reader = command.ExecuteReader();
            try {
                reader.Read();
                DataTable dt = new DataTable();
                Connection.Open();
                dt.Load(reader);
                Tables = dt.AsEnumerable().Select(r => r.Field<string>("table_name")).ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw e;
            }finally
            {
                Connection.Close();
            }   
        }
    }
}
