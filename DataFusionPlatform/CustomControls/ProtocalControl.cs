using DataFusionPlatformPlugin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataFusionPlatformPlugin.CustomControls
{
    public class ProtocalControl:Control
    {
        static ProtocalControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProtocalControl), new FrameworkPropertyMetadata(typeof(ProtocalControl)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

    }
}
