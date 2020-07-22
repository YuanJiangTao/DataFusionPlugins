using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public interface IProtocal : IDisposable
    {
        ProtocalControlSetting GetProtocalSetting();

        void Load(IProtocalHostConfig hostConfig);
        void Stop();
    }
}
