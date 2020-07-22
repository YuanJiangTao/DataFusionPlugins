using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFusionProtocal.Interfaces;
using System.Threading;
using MineSafetySystemProtocal.Repositories;

namespace MineSafetySystemProtocal
{
    public partial class MineSafetySystemProtocal : ProtocalBase
    {
        private ILogDog _log;
        public MineSafetySystemProtocal()
        {
            _log = RegisterLogDog(Title);
        }
        private ProtocalMonitor _logMonitor = new ProtocalMonitor()
        {
            Title = "安全系统融合",
            Columns = new[] { "时间", "日志" },
        };
        public override string Title => "安全监控协议";

        public override string Description => "安全监控协议解析";

        private void Log(string message)
        {
            Log(message, true);
        }
        private void Log(string message, bool isAddMonitor = true)
        {
            _log.Info(message);
            try
            {
                if (isAddMonitor)
                    Watch(0, DateTime.Now.ToString(), message);
            }
            catch (Exception)
            {
                // 防止关闭时候, 记录日志时候, Monitor已经消失.
            }
        }
        public override ObservableCollection<ProtocalKV> InitKvs()
        {
            return new ObservableCollection<ProtocalKV>()
            {
                _protocal,
                _monitorPathKV,
                _backupPathKV,
                _encodingKV,
                _fileExtensionKV
            };
        }

        public override ObservableCollection<ProtocalMonitor> InitMonitors()
        {
            return new ObservableCollection<ProtocalMonitor>() { _logMonitor };
        }
        private IParseProtocal _iparseProtocal;
        public override void OnLoad(IProtocalHostConfig hostConfig)
        {
            try
            {
                DataRepo repo = new DataRepo(hostConfig.DatabaseConfig);
                Config.Init(this);
                _log.Info("Protocal start...");
                _iparseProtocal = ProtocolFactory.Create(Config.SelectProtocal);
                if (_iparseProtocal == null)
                {
                    Log("请选择协议...");
                    return;
                }
                Log($"{Description}开始...");
                _iparseProtocal.Load(repo, Log, hostConfig);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public override void Stop()
        {
            _iparseProtocal?.Cancel();
        }
    }
}
