using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public enum KvType
    {
        String = 0,
        Int = 1,
        Float = 2,
        File = 3,
        Combobox = 4,
        Bool = 5,
        StringApi = 6,
        Filter = 7,
        BackupFile = 8,
    }
    public enum ProtocalControlType
    {
        Monitor = 0x00,
        KeyValue = 0x01
    }
}
