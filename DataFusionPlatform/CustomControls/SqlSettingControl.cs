using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataFusionPlatformPlugin.CustomControls
{
    public class SqlSettingControl : Control
    {
        static SqlSettingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SqlSettingControl), new FrameworkPropertyMetadata(typeof(SqlSettingControl)));
        }

        // 数据库密码输入框
        private PasswordBox _passwordBox;

        public SqlSettingControl()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _passwordBox = GetTemplateChild("TxbPassword") as PasswordBox;
            _passwordBox.Password = Password;
            _passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = _passwordBox.Password;
        }

        public string ServerName
        {
            get { return (string)GetValue(ServerNameProperty); }
            set
            {
                SetValue(ServerNameProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ServerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServerNameProperty =
            DependencyProperty.Register("ServerName", typeof(string), typeof(SqlSettingControl), new PropertyMetadata("127.0.0.1"));


        public string DatabaseName
        {
            get { return (string)GetValue(DatabaseNameProperty); }
            set { SetValue(DatabaseNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DatabaseName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatabaseNameProperty =
            DependencyProperty.Register("DatabaseName", typeof(string), typeof(SqlSettingControl), new PropertyMetadata("KJ835X"));




        public string UserId
        {
            get { return (string)GetValue(UserIdProperty); }
            set { SetValue(UserIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserIdProperty =
            DependencyProperty.Register("UserId", typeof(string), typeof(SqlSettingControl), new PropertyMetadata("sa"));



        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(SqlSettingControl), new FrameworkPropertyMetadata("gl", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public ICommand ConnectCommand
        {
            get { return (ICommand)GetValue(ConnectCommandProperty); }
            set { SetValue(ConnectCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConnectCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConnectCommandProperty =
            DependencyProperty.Register("ConnectCommand", typeof(ICommand), typeof(SqlSettingControl));


    }
}
