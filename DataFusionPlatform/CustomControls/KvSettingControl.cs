using DataFusionPlatformPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataFusionPlatformPlugin.CustomControls
{
    public class KvSettingControl : Control
    {
        static KvSettingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KvSettingControl), new FrameworkPropertyMetadata(typeof(KvSettingControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            foreach (var item in ProtocalKVVms)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var triggerKvvm = sender as ProtocalKVViewModel;
            if (triggerKvvm == null) return;

            foreach (var item in ProtocalKVVms)
            {
                if (item.VisiableByKey == triggerKvvm.Key)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                    item.RaisePropertyChanged("Value");
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
        }

        public ObservableCollection<ProtocalKVViewModel> ProtocalKVVms
        {
            get { return (ObservableCollection<ProtocalKVViewModel>)GetValue(ProtocalKVVmsProperty); }
            set { SetValue(ProtocalKVVmsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PluginKVVms.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProtocalKVVmsProperty =
            DependencyProperty.Register("ProtocalKVVms", typeof(ObservableCollection<ProtocalKVViewModel>), typeof(KvSettingControl), new PropertyMetadata(null));


        public bool AdminMode
        {
            get { return (bool)GetValue(AdminModeProperty); }
            set { SetValue(AdminModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdminMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdminModeProperty =
            DependencyProperty.Register("AdminMode", typeof(bool), typeof(KvSettingControl), new PropertyMetadata(false));
    }
}
