using DataFusionProtocal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal
{
    public partial class MineSafetySystemProtocal
    {
        private readonly ProtocalKV _protocal = new ProtocalKV()
        {
            Key = ProtocalConst.SELECT_PROTOCAL,
            Value = "",
            ComboBoxItems =
            new[]
            {
                ProtocalConst.SANHENG_SAFETY_PROTOCAL
            },
            KvType = KvType.Combobox
        };

        private readonly ProtocalKV _monitorPathKV = new ProtocalKV()
        {
            Key = ProtocalConst.MONITOR_FILEPATH,
            Value = "",
            KvType = KvType.File,
            VisiableByKey = ProtocalConst.SELECT_PROTOCAL,
            VisiablePredicate = v =>
            {
                return v == ProtocalConst.SANHENG_SAFETY_PROTOCAL;
            }
        };
        private readonly ProtocalKV _backupPathKV = new ProtocalKV()
        {
            Key = ProtocalConst.BACKUP_FILEPATH,
            Value = "",
            KvType = KvType.File,
            VisiableByKey = ProtocalConst.SELECT_PROTOCAL,
            VisiablePredicate = v =>
            {
                return v == ProtocalConst.SANHENG_SAFETY_PROTOCAL;
            }
        };
        private readonly ProtocalKV _encodingKV = new ProtocalKV()
        {
            Key = ProtocalConst.ENCODING,
            Value = "GB2312",
            ComboBoxItems = new[] { "UTF-8", "GB2312" },
            KvType = KvType.Combobox,
            VisiableByKey = ProtocalConst.SELECT_PROTOCAL,
            VisiablePredicate = v =>
            {
                return v == ProtocalConst.SANHENG_SAFETY_PROTOCAL;
            }
        };
        private readonly ProtocalKV _fileExtensionKV = new ProtocalKV()
        {
            Key = ProtocalConst.FILE_EXTENSION,
            Value = "*.txt",
            KvType = KvType.String,
            VisiableByKey = ProtocalConst.SELECT_PROTOCAL,
            VisiablePredicate = v =>
            {
                return v == ProtocalConst.SANHENG_SAFETY_PROTOCAL;
            }
        };
    }
}
