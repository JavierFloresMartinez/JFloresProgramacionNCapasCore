using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string GetConexcionString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new ();
            connectionStringBuilder.DataSource = ".";
            connectionStringBuilder.InitialCatalog = "JFloresProgramacionNCapas";
            connectionStringBuilder.PersistSecurityInfo = true;
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "pass@word1";
            var conexcionString = connectionStringBuilder.ConnectionString;
            return conexcionString;
        }
    }
}
