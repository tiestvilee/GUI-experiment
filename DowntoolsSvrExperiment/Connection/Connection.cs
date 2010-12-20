using System.Data.SqlClient;
using DowntoolsSvrExperiment.VRPages.LocalServerPicker;

namespace DowntoolsSvrExperiment.Connection
{
    public class Connection
    {
        private readonly string m_DataSource;
        private readonly SecurityType m_SecurityType;
        private readonly string m_UserId;
        private readonly string m_Password;

        public Connection(string dataSource, SecurityType securityType, string userId, string password)
        {
            m_DataSource = dataSource;
            m_Password = password;
            m_UserId = userId;
            m_SecurityType = securityType;
        }

        public string UserId
        {
            get { return m_UserId; }
        }

        public string Password
        {
            get { return m_Password; }
        }

        public SecurityType SecurityType
        {
            get { return m_SecurityType; }
        }

        public string DataSource
        {
            get { return m_DataSource; }
        }

        public override string ToString()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.UserID = UserId;
            builder.Password = Password;
            builder.IntegratedSecurity = m_SecurityType == SecurityType.Integrated;
            builder.DataSource = m_DataSource;
            return builder.ToString();
        }

    }
}
