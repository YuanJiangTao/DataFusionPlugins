using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public class DatabaseConfig
    {
        public string ServerName { get; set; } = "127.0.0.1";
        public string DatabaseName { get; set; } = "OIMS";
        public string UserId { get; set; } = "sa";
        public string Password { get; set; } = "gl";
        public bool IntegratedSecurity { get; set; } = false;
        public string ConnectionString()
        {
            if (IntegratedSecurity)
                return $"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security=True;MultipleActiveResultSets=true";
            return
                $"Data Source={ServerName};Initial Catalog={DatabaseName};Persist Security Info=True;User ID={UserId};Password={Password};MultipleActiveResultSets=true;Application Name=Anthill";

        }
        public override string ToString()
        {
            return $"{ConnectionString() }";
        }
        public string ToYearString(DateTime now)
        {
            if (IntegratedSecurity)
                return $"Data Source={ServerName};Initial Catalog={DatabaseName}{now:yyyy};Integrated Security=True;MultipleActiveResultSets=true";
            return
                $"Data Source={ServerName};Initial Catalog={DatabaseName}{now:yyyy};Persist Security Info=True;User ID={UserId};Password={Password};MultipleActiveResultSets=true;Application Name=Anthill";
        }
    }
}
