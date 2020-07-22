using BootstrapUI.TinyIoC;
using DataFusion.Interfaces;
using DataFusionProtocal.Interfaces.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class DatabaseViewModel : ViewModelBase
    {
        private readonly Repo _repo;
        private readonly string _protocalTitle;
        private readonly IHostConfig _hostConfig;
        public DatabaseViewModel()
        {
            _hostConfig = TinyIoCContainer.Current.Resolve<IHostConfig>();
            _repo = TinyIoCContainer.Current.Resolve<Repo>();
        }
        public DatabaseViewModel(string protocalTitle) : this()
        {
            _protocalTitle = protocalTitle;
            var databaseSg = _repo.GetDatabaseSg(_hostConfig.MinePluginConfig.Id.ToString(), protocalTitle);
            DatabaseSg = databaseSg ?? new DatabaseSg();
            ConnectCommand = new RelayCommand(Connect);
        }
        public ICommand ConnectCommand { get; }

        public DatabaseSg DatabaseSg { get; }

        public string ServerName
        {
            get => DatabaseSg.ServerName;
            set
            {
                DatabaseSg.ServerName = value;
                RaisePropertyChanged();
            }
        }
        public string DatabaseName
        {
            get => DatabaseSg.DatabaseName;
            set { DatabaseSg.DatabaseName = value; RaisePropertyChanged(); }
        }
        public string UserId
        {
            get => DatabaseSg.UserId;
            set { DatabaseSg.UserId = value; RaisePropertyChanged(); }
        }
        public string Password
        {
            get => DatabaseSg.Password;
            set { DatabaseSg.Password = value; RaisePropertyChanged(); }
        }
        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
            _repo.SetDatabaseSg(_hostConfig.MinePluginConfig.Id.ToString(), _protocalTitle, DatabaseSg);
        }

        private void Connect()
        {
            _repo.SetDatabaseSg(_hostConfig.MinePluginConfig.Id.ToString(), _protocalTitle, DatabaseSg);
            if (ConnectUtil.TryConnect(DatabaseSg.ConnectionString()))
                MessageBox.Show("连接成功");
            else
                MessageBox.Show("连接失败!");
        }

    }
}
