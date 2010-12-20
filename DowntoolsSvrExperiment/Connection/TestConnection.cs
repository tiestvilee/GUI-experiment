using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DowntoolsSvrExperiment.VRPages.LocalServerPicker;
using RedGate.Shared.SQL.Server;

namespace DowntoolsSvrExperiment.Connection
{
    public class TestConnection
    {

        public virtual ConnectionStatus Test(Connection connection)
        {
            bool connected;
            string errorMessage;
            string dataPath;
            var databases = new List<DbObjectName>();

            try
            {
                foreach (var n in SQLServer.GetDatabases(connection.DataSource, connection.SecurityType == SecurityType.Integrated, connection.UserId, connection.Password, true))
                {
                    databases.Add(new DbObjectName(n));
                }
                connected = true;
                errorMessage = String.Empty;
            }
            catch (Exception e)
            {
                connected = false;
                errorMessage = e.Message;
                databases = null;
            }

            try
            {
                dataPath = GetDataPath(connection.ToString());
            }
            catch (Exception e)
            {
                /* no error message stored */
                // Logger.LogInformation("UI", e, "Could not get sql data path");
                dataPath = String.Empty;
            }
            return new ConnectionStatus(connected, errorMessage, databases, dataPath);
        }

        private static string GetDataPath(string connectionSring)
        {
            using (var conn = new SqlConnection(connectionSring))
            {
                conn.Open();
                using (var cmd = new SqlCommand("master.dbo.xp_instance_regread", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var root = cmd.Parameters.Add("", SqlDbType.Text);
                    var key = cmd.Parameters.Add("", SqlDbType.Text);
                    var value = cmd.Parameters.Add("", SqlDbType.Text);
                    var output = cmd.Parameters.Add("", SqlDbType.Variant);

                    root.Value = @"HKEY_LOCAL_MACHINE";
                    key.Value = @"SOFTWARE\Microsoft\MSSQLServer\Setup";
                    value.Value = @"SQLDataRoot";
                    output.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    var basePath = output.Value as String;

                    return basePath != null ? Path.Combine(basePath, "Data") : null;
                }
            }
        }
    }



    public class ConnectionStatus
    {
        private bool m_Connected;
        private string m_ErrorMessage;
        private List<DbObjectName> m_Databases = new List<DbObjectName>();
        private string m_DataPath;

        public ConnectionStatus(bool connected, string errorMessage, List<DbObjectName> databases, string dataPath)
        {
            m_Connected = connected;
            m_DataPath = dataPath;
            m_Databases = databases;
            m_ErrorMessage = errorMessage;
        }

        public string DataPath
        {
            get { return m_DataPath; }
        }

        public List<DbObjectName> Databases
        {
            get { return m_Databases; }
        }

        public string ErrorMessage
        {
            get { return m_ErrorMessage; }
        }

        public bool Connected
        {
            get { return m_Connected; }
        }
    }
}
