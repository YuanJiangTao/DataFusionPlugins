using DataFusion.Interfaces;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using DataFusionProtocal.Interfaces;
using hostlog = DataFusion.Interfaces;
using DataFusion.Interfaces.Utils;
using System.Threading;
using System.Windows.Threading;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly hostlog.ILogDog _log;
        private readonly IHostConfig _hostConfig;
        private List<IProtocal> _protocals;
        private RedisHelper _repo;
        public MainViewModel(hostlog.ILogDog log, IHostConfig hostConfig, List<IProtocal> protocals)
        {
            _repo = new RedisHelper(0);
            _hostConfig = hostConfig;
            _protocals = protocals;
            _log = log;
            MenuItems = new ObservableCollection<HamburgerMenuItem>();

        }
        /// <summary>
        /// 初始化信息
        /// </summary>
        public void IniMenuItems()
        {
            try
            {
                foreach (var protocal in _protocals)
                {
                    MenuItems.Add(new HamburgerMenuItem()
                    {
                        Label = protocal.GetProtocalSetting().Title,
                        Tag = new CustomControls.ProtocalControl() { DataContext = new ProtocalViewModel(protocal) }
                    });
                }
            }
            catch (Exception ex)
            {
                _log.Error($"IniMenuItems:{ex.ToString()}");
            }

        }
        public ObservableCollection<HamburgerMenuItem> MenuItems { get; set; }





    }
}
