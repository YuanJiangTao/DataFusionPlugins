using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public interface ILogDogCollar : IDisposable
    {
        void Setup(string baseDir, string dir, string logName, string level = "ALL");
        ILogDog GetLogger();
    }
}
