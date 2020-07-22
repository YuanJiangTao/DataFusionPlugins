using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public interface IProtocalHostConfig
    {
        DatabaseConfig DatabaseConfig { get; set; }
        MineProtocalConfig MineProtocalConfig { get; set; }
    }

    public class ProtocalHostConfig : IProtocalHostConfig
    {
        public ProtocalHostConfig()
        {

        }
        public DatabaseConfig DatabaseConfig { get; set; }
        public MineProtocalConfig MineProtocalConfig { get; set; }
    }
}
