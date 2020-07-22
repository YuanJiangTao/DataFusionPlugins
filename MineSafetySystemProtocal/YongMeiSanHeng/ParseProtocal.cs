using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MineSafetySystemProtocal.Repositories;
using DataFusionProtocal.Interfaces;
using System.IO;

namespace MineSafetySystemProtocal.YongMeiSanHeng
{
    class ParseProtocal : IParseProtocal
    {
        private DataRepo _repo;
        private Action<string> _log;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private Task _task;

        public ParseProtocal()
        {

        }

        public void Cancel()
        {
            try
            {
                _cts.Cancel();
            }
            catch
            {

            }
        }
        public IProtocalHostConfig HostConfig { get; private set; }

        public void Load(DataRepo repo, Action<string> log, IProtocalHostConfig hostConfig)
        {
            this._repo = repo;
            this._log = log;
            this.HostConfig = hostConfig;
            _task = Task.Factory.StartNew(TaskBody, _cts.Token);
        }
        private void TaskBody()
        {
            try
            {
                var dev = new Dev(this, _repo, _log);
                Base rtdata = new Rtdata(this, _repo, _log, dev);
                Base accdata = new Accdata(this, _repo, _log, dev);
                Base fzdata = new Fzdata(this, _repo, _log, dev);
                Base kgbhdata = new Kgbhdata(this, _repo, _log, dev);
                Base ycbjdata = new Ycbjdata(this, _repo, _log, dev);
                Base unknown = new Unknown(this, _repo, _log);
                dev.SetSuccessor(rtdata);
                rtdata.SetSuccessor(accdata);
                accdata.SetSuccessor(fzdata);
                fzdata.SetSuccessor(kgbhdata);
                kgbhdata.SetSuccessor(ycbjdata);
                ycbjdata.SetSuccessor(unknown);
                while (!_cts.IsCancellationRequested)
                {
                    _repo.HandleYearDapper();
                    var files = GetFilesFromDir(Config.MonitorFilePath, Config.FileExtension);
                    if (!files.Any())
                    {
                        Thread.Sleep(500);
                        continue;
                    }
                    try
                    {
                        foreach (var file in files)
                        {
                            _log($"处理文件 {file}");
                            // 处理接收到的文件
                            if (dev != null)
                            {
                                dev.HandleFile(file);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log($"处理文件出错:{ex}");
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                _log($"Load:{ex}");
            }
        }
        private string[] GetFilesFromDir(string path, string ext)
        {
            if (string.IsNullOrEmpty(ext))
                ext = "*";
            var files = Directory.GetFiles(path, ext, SearchOption.TopDirectoryOnly).Where(p => Path.GetExtension(p) != ".tmp")
                 .Select(o => new FileInfo(o))
                    .OrderByDescending(o => o.LastWriteTime)
                    .Take(20)
                    .Select(o => o.FullName)
                    .ToArray();
            return files;
        }

        public void Dispose()
        {
            try
            {
                _task.Dispose();
                _cts.Dispose();
            }
            catch
            {
            }
        }
    }
    public enum BakDirType
    {
        Back = 0,
        Error = 1,
        Unkown = 2,
    }
}
