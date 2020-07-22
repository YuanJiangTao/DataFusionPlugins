using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFusionProtocal.Interfaces;
using System.Collections.ObjectModel;
using BootstrapUI.TinyIoC;
using System.Collections.Specialized;
using System.ComponentModel;
using DataFusion.Interfaces;
using System.Threading;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class ProtocalViewModel : ViewModelBase, IDisposable
    {
        private readonly IProtocal _protocal;
        private readonly Repo _repo;
        private readonly string _protocalTitle;
        private readonly IHostConfig _hostConfig;

        private List<ProtocalKvSg> _kvsgs;


        public ProtocalViewModel()
        {
            _repo = TinyIoCContainer.Current.Resolve<Repo>();
            _hostConfig = TinyIoCContainer.Current.Resolve<IHostConfig>();
        }
        public ProtocalViewModel(IProtocal protocal) : this()
        {
            _protocal = protocal;
            _kvsgs = new List<ProtocalKvSg>();
            ProtocalKVViewModels = new ObservableCollection<ProtocalKVViewModel>();
            ProtocalMonitorViewModels = new ObservableCollection<ProtocalMonitorViewModel>();
            var protocalKvs = protocal.GetProtocalSetting().PluginKVs;
            var protocalMonitors = protocal.GetProtocalSetting().PluginMonitors;
            _protocalTitle = protocal.GetProtocalSetting().Title;
            var tempKvsgs = _repo.GetProtocalKvSgs(_hostConfig.MinePluginConfig.Id.ToString(), _protocalTitle);
            if (tempKvsgs != null)
                _kvsgs.AddRange(tempKvsgs);
            foreach (var item in protocalKvs)
            {
                if (_kvsgs.Exists(p => p.Key == item.Key))
                {
                    item.Value = _kvsgs.FirstOrDefault(p => p.Key == item.Key).Value;
                }
                //添加配置信息
                var kvm = new ProtocalKVViewModel(item);
                //Initialize ProtocalKVs
                ProtocalKVViewModels.Add(kvm);
                kvm.PropertyChanged += Kvm_PropertyChanged;
            }

            //Initialize Database
            DatabaseViewModel = new DatabaseViewModel(_protocalTitle);
            SelectedPluginMonitorVm = new ProtocalMonitorViewModel(protocal.GetProtocalSetting().PluginMonitors.FirstOrDefault());

            IsEnable = _repo.GetProtocalEnable(_hostConfig.MinePluginConfig.Id.ToString(), _protocalTitle);
            SendMessage();

        }

        private void Kvm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ProtocalKVViewModel kvm)
            {
                var kvsg = _kvsgs.FirstOrDefault(p => p.Key == kvm.Key);
                if (kvsg == null)
                {
                    _kvsgs.Add(new ProtocalKvSg()
                    {
                        Key = kvm.Key,
                        Value = kvm.Value
                    });
                }
                else
                {
                    kvsg.Value = kvm.Value;
                }
                _repo.SetProtocalKvSgs(_hostConfig.MinePluginConfig.Id.ToString(), _protocalTitle, _kvsgs);
            }
        }

        public void Dispose()
        {
            try
            {
                _protocal.Dispose();
            }
            catch
            {
            }
        }

        public DatabaseViewModel DatabaseViewModel { get; set; }

        public ProtocalMonitorViewModel SelectedPluginMonitorVm { get; set; }

        public ObservableCollection<ProtocalKVViewModel> ProtocalKVViewModels { get; set; }

        public ObservableCollection<ProtocalMonitorViewModel> ProtocalMonitorViewModels { get; set; }

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (Equals(_isEnable, value)) return;
                _isEnable = value;
                RaisePropertyChanged();
                SendMessage();
                HandlProtocal();
            }
        }

        private void SendMessage()
        {
            _repo.Publish<ProtocalEnableConfig>(_hostConfig.MinePluginConfig.Id.ToString(), new ProtocalEnableConfig() { ProtocalName = _protocalTitle, IsEnable = IsEnable });
        }

        private void HandlProtocal()
        {
            _repo.SetProtocalEnable(_hostConfig.MinePluginConfig.Id.ToString(), _protocalTitle, IsEnable);
            if (IsEnable)
            {
                ProtocalHostConfig protocalHostConfig = new ProtocalHostConfig()
                {
                    DatabaseConfig = DatabaseViewModel.DatabaseSg.ToDataBaseConfig(),
                    MineProtocalConfig = new MineProtocalConfig()
                    {
                        MineCode = _hostConfig.MinePluginConfig.MineCode,
                        MineName = _hostConfig.MinePluginConfig.MineName
                    }
                };
                _protocal.Load(protocalHostConfig);
            }
            else
            {
                _protocal.Stop();
            }

        }
    }
}
;