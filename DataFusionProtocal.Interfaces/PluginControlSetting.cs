using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionProtocal.Interfaces
{
    public class ProtocalControlSetting
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ObservableCollection<ProtocalMonitor> PluginMonitors { get; set; }
        public ObservableCollection<ProtocalKV> PluginKVs { get; set; }
    }
    public class ProtocalMonitor 
    {
        public void OnWatch(string monitorId, params string[] fields)
        {
            WatchEvent?.Invoke(this, new ProtocalMonitorEventArgs(monitorId, fields));
        }
        public event EventHandler<ProtocalMonitorEventArgs> WatchEvent;

        public ProtocalMonitor()
        {
        }
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Title { get; set; }

        public string[] Columns { get; set; }

        public string[] PrimaryKeys { get; set; }
    }

    public class ProtocalKV : INotifyPropertyChanged
    {
        public ProtocalKV()
        {

        }
        private string key;
        public string Key
        {
            get => key;
            set
            {
                if (Equals(key, value)) return;
                key = value;
                OnPropertyChanged();
            }
        }
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value)) return;
                _value = value;
                OnPropertyChanged();
            }
        }
        private KvType kvType;
        public KvType KvType
        {
            get => kvType;
            set
            {
                if (Equals(kvType, value)) return;
                kvType = value;
                OnPropertyChanged();
            }
        }
        private string[] comboBoxItems;
        public string[] ComboBoxItems
        {
            get => comboBoxItems;
            set
            {
                comboBoxItems = value;
                OnPropertyChanged();
            }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                if (Equals(isAdmin, value)) return;
                isAdmin = value;
                OnPropertyChanged();
            }
        }
        private string visiableByKey;

        public string VisiableByKey
        {
            get => visiableByKey;
            set
            {
                if (Equals(visiableByKey, value)) return;
                visiableByKey = value;
                OnPropertyChanged();
            }
        }


        public Func<string, bool> VisiablePredicate { get; set; }
        public Func<string, string, Tuple<string, string>> Executor { get; set; }
        public string ExecutorName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProtocalMonitorEventArgs
    {
        public string Id { get; private set; }
        public string[] Fields { get; private set; }

        public ProtocalMonitorEventArgs(string id, string[] fields)
        {
            Id = id;
            Fields = fields;
        }
    }
}
