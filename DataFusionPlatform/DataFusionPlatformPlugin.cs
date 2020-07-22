using BootstrapUI.TinyIoC;
using DataFusion.Interfaces;
using DataFusion.Interfaces.Utils;
using DataFusionPlatformPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Threading;

namespace DataFusionPlatformPlugin
{
    public class DataFusionPlatformPlugin : PluginBase
    {
        private readonly TinyIoCContainer _container;
        private ILogDog _log;
        private readonly IHostConfig _hostConfig;
        private MainViewModel _mainViewModel;
        private EventWaitHandle _waitHandle;
        public DataFusionPlatformPlugin(IPluginHost host) : base(host)
        {
            _hostConfig = host.GetService<IHostConfig>();
            _container = TinyIoCContainer.Current;
            _log = RegisterLogDog(_hostConfig.MinePluginConfig.MineName);
            _log.Info($"{_hostConfig.MinePluginConfig.MineName}开始载入...");
            _container.Register<IHostConfig>(_hostConfig);
            _container.Register<ILogDog>(_log);
            _container.Register<MainViewModel>().AsSingleton();
            _container.Register<Repo>().AsSingleton();
            _waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        }

        public override string Title => "协议解析";

        public override string Description => "用于系统融合协议解析";

        public override FrameworkElement CreateControl()
        {
            UIContext.Initialize(); // 程序一开始初始化.
            _waitHandle.WaitOne();
            _mainViewModel.IniMenuItems();
            _log.Info("开始生成UserControl...");
            return new MainUserControl() { DataContext = _mainViewModel };
        }

        protected override void Onload()
        {
            try
            {
                if (!Directory.Exists(BaseDirectory))
                    return;
                var assemblies = new List<Assembly>();
                var files = Directory.GetFiles(BaseDirectory, "*.dll", SearchOption.AllDirectories).Where(o => o.Contains("Protocal"));
                var moduleAssemblies = files.Select(Assembly.LoadFile);
                assemblies.AddRange(moduleAssemblies);
                _container.AutoRegister(assemblies, DuplicateImplementationActions.RegisterMultiple);
                _mainViewModel = _container.Resolve<MainViewModel>();
                _waitHandle.Set();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
        }
    }
}
