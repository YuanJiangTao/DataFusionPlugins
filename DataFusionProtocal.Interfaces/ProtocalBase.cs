using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public abstract class ProtocalBase : IProtocal
    {
        protected ILogDogCollar LogDogCollar;
        protected ILogDog LogDogRoot;
        private Thread _loadThread;

        public ProtocalBase()
        {
            CultureInfoHelper.SetDateTimeFormat();
            LogDogCollar = new LogDogCollar();
            LogDogRoot = RegisterLogDog(Title);
            _pluginMonitors = InitMonitors();
            _pluginKVs = InitKvs();
            BaseDirectory = Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).Location);
        }
        protected ILogDog RegisterLogDog(string logName, string level = "ALL")
        {
            var currentDir = Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).Location);
            var logDir = Directory.GetParent(Directory.GetParent(currentDir).FullName).FullName;
            LogDogCollar.Setup(logDir, Title, logName, level);
            return LogDogCollar.GetLogger();
        }
        public string BaseDirectory { get; protected set; }

        private ObservableCollection<ProtocalMonitor> _pluginMonitors;
        private ObservableCollection<ProtocalKV> _pluginKVs;

        public abstract ObservableCollection<ProtocalMonitor> InitMonitors();

        public abstract ObservableCollection<ProtocalKV> InitKvs();

        public ProtocalControlSetting GetProtocalSetting()
        {
            if (_pluginKVs != null)
            {
                foreach (var group in _pluginKVs.GroupBy(o => o.Key))
                {
                    if (group.Count() > 1)
                    {
                        throw new ArgumentException($"{group.Key} 设置了带个相同的键值的选项.");
                    }
                }
            }
            return new ProtocalControlSetting()
            {
                Title = Title,
                Description = Description,
                PluginMonitors = _pluginMonitors,
                PluginKVs = _pluginKVs
            };
        }

        public abstract void OnLoad(IProtocalHostConfig hostConfig);

        public void Load(IProtocalHostConfig hostConfig)
        {
            try
            {
                _loadThread = new Thread(() =>
                  {
                      OnLoad(hostConfig);
                  });
                _loadThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void Watch(int index, params string[] fields)
        {
            if (index < 0 || index >= _pluginMonitors.Count)
            {
                throw new IndexOutOfRangeException("超出定义的监视器");
            }
            var monitor = _pluginMonitors[index];
            monitor.OnWatch(monitor.Id, fields);
        }

        public virtual void Dispose()
        {
            _loadThread.Join(3000);
            if (_pluginMonitors != null)
            {
                _pluginMonitors.Clear();
            }
            if (_pluginKVs != null)
            {
                _pluginKVs.Clear();
            }
        }

        public abstract string Title { get; }

        public abstract string Description { get; }

        public abstract void Stop();

        public string this[string index]
        {
            get
            {
                if (_pluginKVs == null) return null;

                var kv = _pluginKVs.FirstOrDefault(o => o.Key == index);
                if (kv != null)
                {
                    return kv.Value;
                }
                return null;
            }
            set
            {
                var kv = _pluginKVs.FirstOrDefault(o => o.Key == index);
                if (kv != null)
                {
                    kv.Value = value;
                }
                else
                {
                    // 增加通用配置.
                    kv = new ProtocalKV();
                    kv.Key = index;
                    kv.Value = value;
                    _pluginKVs.Add(kv);

                    Console.WriteLine($"增加通用配置{kv.Key}-{kv.Value}");
                }
            }
        }
    }
}
