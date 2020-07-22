using MineSafetySystemProtocal.Helper;
using MineSafetySystemProtocal.Repositories;
using MineSafetySystemProtocal.YongMeiSanHeng.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.YongMeiSanHeng
{
    abstract class Base
    {
        protected const int MINECODE_LENGTH = 20;
        protected const char SEPARATOR = ';';
        protected const char ENDOFLINE = '~';



        protected ParseProtocal Parent;
        protected DataRepo DataRepo;
        protected Action<string> Log;

        protected string MineCode { get; private set; }
        public Base(ParseProtocal protocol, DataRepo dataRepo, Action<string> log)
        {
            Parent = protocol;
            DataRepo = dataRepo;
            Log = log;
            MineCode = Parent.HostConfig.MineProtocalConfig.MineCode;
        }
        public virtual DateTime ParserHeader(string[] segments)
        {
            if (FileName.ToUpper() != "DEV")
                if (segments.Length >= 1)
                    return segments[0].ToDateTime();
            return DateTime.Now;
        }

        public void HandleFile(string file)
        {
            try
            {
                Log($"开始解析文件:{file}");
                if (!IsFileValid(file))
                {
                    Move2BackDirectory(file, BakDirType.Unkown);
                    return;
                }
                ParseLines(file);
                Move2BackDirectory(file, BakDirType.Back);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Log($"解析文件{file}错误:{ex}");
                Move2BackDirectory(file, BakDirType.Error);
            }
        }
        public abstract string FileName { get; }
        public virtual void ParseLines(string file)
        {
            if (IsSatisfyCondition(file))
            {
                var lines = File.ReadLines(file, Encoding.GetEncoding(Config.FileEncoding)).ToList();
                if (lines.Any() && lines.Count >= 1)
                {
                    var header = GetLines(lines[0]);
                    var realTime = ParserHeader(header);
                    ParseContent(lines.Skip(1).ToList(), realTime);
                }
            }
            else if (Handler != null)
            {
                Handler.ParseLines(file);
            }
        }
        protected string[] GetLines(string content)
        {
            return content.TrimEnd(ENDOFLINE).Split(SEPARATOR);
        }
        protected abstract void ParseContent(List<string> lines, DateTime realTime);
        public virtual bool IsSatisfyCondition(string file)
        {
            return Path.GetFileName(file).ToUpper().Contains(FileName);
        }



        public virtual List<T> Read2Models<T>(string file)
        {
            return default;
        }



        protected Base Handler;
        public void SetSuccessor(Base handler)
        {
            this.Handler = handler;
        }
        private bool IsFileValid(string file)
        {
            if (!File.Exists(file))
            {
                Log($"文件{file} 不存在...");
                return false;
            }
            var fileName = Path.GetFileName(file);
            var mineCodeIsConform = IsMineCodeFit(fileName);
            if (!mineCodeIsConform)
            {
                Log($"文件{file} 编码不符合规范...");
                return false;
            }
            return true;
        }

        protected string ParseBackDirSection(BakDirType bakDirType)
        {
            switch (bakDirType)
            {
                case BakDirType.Back:
                    return "备份文件";
                case BakDirType.Error:
                    return "错误文件";
                case BakDirType.Unkown:
                    return "未知文件";
            }
            throw new Exception("未知的备份文件类型");
        }
        protected void Move2BackDirectory(string file, BakDirType bakDirType)
        {
            try
            {
                if (!File.Exists(file)) return;
                var destPath = Config.BackupFilePath;
                var fileName = Path.GetFileName(file);
                if (fileName != null)
                {
                    var backDirSection = ParseBackDirSection(bakDirType);
                    var secondDir = Path.Combine(destPath, backDirSection);
                    if (!Directory.Exists(secondDir))
                    {
                        Directory.CreateDirectory(secondDir);
                    }
                    var fileInfo = new FileInfo(file);
                    var writeTime = fileInfo.LastWriteTime;

                    var dayDir = Path.Combine(secondDir, $"{writeTime:yyyyMMdd}");
                    if (!Directory.Exists(dayDir))
                    {
                        Directory.CreateDirectory(dayDir);
                    }
                    var moveFile = Path.Combine(dayDir, fileName);
                    if (File.Exists(moveFile))
                        File.Delete(moveFile);
                    File.Move(file, moveFile);
                    Log($"文件 {file} 被移至到 {dayDir} 内");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log($"文件 {file} 移至到备份文件夹出错:" + e);
            }

        }

        /// <summary>
        /// 判断是否符合煤矿编码
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected bool IsMineCodeFit(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName.Length <= 20)
                return false;
            var mineCodeStr = fileName.Substring(0, MINECODE_LENGTH);
            return MineCode == mineCodeStr;
        }




    }


}
