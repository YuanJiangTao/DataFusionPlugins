using DataFusionProtocal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class DatabaseSg
    {
        public DatabaseSg()
        {

        }
        public string ServerName { get; set; } = "127.0.0.1";
        public string DatabaseName { get; set; } = "OIMS";
        public string UserId { get; set; } = "sa";
        public string Password { get; set; } = "gl";

        public bool IntegratedSecurity { get; set; } = false;

        internal string ConnectionString()
        {
            if (IntegratedSecurity)
                return $"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security=True";
            return
                $"Data Source={ServerName};Initial Catalog={DatabaseName};Persist Security Info=True;User ID={UserId};Password={Password};Application Name=DataFusion";
        }
        public DatabaseConfig ToDataBaseConfig()
        {
            return new DatabaseConfig()
            {
                ServerName = ServerName,
                DatabaseName = DatabaseName,
                UserId = UserId,
                Password = Password,
                IntegratedSecurity = IntegratedSecurity
            };
        }
    }
}
