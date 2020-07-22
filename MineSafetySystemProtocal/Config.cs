using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal
{
    class Config
    {
        private static MineSafetySystemProtocal _protocal;
        public static void Init(MineSafetySystemProtocal protocal)
        {
            _protocal = protocal;
        }

        /// <summary>
        /// 选择的协议.
        /// </summary>
        public static string  SelectProtocal
        {
            get
            {
                return _protocal[ProtocalConst.SELECT_PROTOCAL].ToString();
            }
        }
        /// <summary>
        /// 监听文件夹
        /// </summary>
        public static string MonitorFilePath 
        {
            get
            {
                return _protocal[ProtocalConst.MONITOR_FILEPATH].ToString();
            }
        }
        /// <summary>
        /// 备份文件夹
        /// </summary>
        public static string BackupFilePath
        {
            get
            {
                return _protocal[ProtocalConst.BACKUP_FILEPATH].ToString();
            }
        }
        /// <summary>
        /// 监听文件夹扩展名
        /// </summary>
        public static string FileExtension
        {
            get
            {
                return _protocal[ProtocalConst.FILE_EXTENSION].ToString();
            }
        }
        /// <summary>
        /// 文件编码格式
        /// </summary>
        public static string FileEncoding
        {
            get
            {
                return _protocal[ProtocalConst.ENCODING].ToString();
            }
        }
    }
}
