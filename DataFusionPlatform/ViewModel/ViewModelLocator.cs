using BootstrapUI.TinyIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class ViewModelLocator
    {
        private readonly TinyIoCContainer _container;
        public ViewModelLocator()
        {
            _container = TinyIoCContainer.Current;
            _container.Register<MainViewModel>();
        }
        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
    }
}
