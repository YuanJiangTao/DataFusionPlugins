using DataFusionProtocal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class ProtocalKVViewModel : ViewModelBase
    {
        public ProtocalKVViewModel(ProtocalKV pluginKV)
        {
            PluginKV = pluginKV;
            InitExecutor();
        }

        private void SelectFilePath()
        {
            try
            {
                var folderBrowserDialog = new FolderBrowserDialog();

                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Value = folderBrowserDialog.SelectedPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 上传文件备份目录
        /// </summary>
        private void BackupFilePath()
        {
            try
            {
                var folderBrowserDialog = new FolderBrowserDialog();

                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Value = folderBrowserDialog.SelectedPath;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string Key
        {
            get => PluginKV.Key;
            set
            {
                PluginKV.Key = value;
                RaisePropertyChanged();
            }
        }

        public string Value
        {
            get => PluginKV.Value;
            set
            {
                PluginKV.Value = value;
                RaisePropertyChanged();
            }
        }

        public KvType KvType
        {
            get => PluginKV.KvType;
            set
            {
                PluginKV.KvType = value;
                RaisePropertyChanged();


            }
        }

        public string[] ComboBoxItems
        {
            get => PluginKV.ComboBoxItems;
            set
            {
                PluginKV.ComboBoxItems = value;
                RaisePropertyChanged();
            }
        }

        public bool IsAdmin
        {
            get => PluginKV.IsAdmin;
            set
            {
                PluginKV.IsAdmin = value;
                RaisePropertyChanged();
            }
        }


        public string VisiableByKey
        {
            get
            {
                return PluginKV.VisiableByKey;
            }
            set
            {
                PluginKV.VisiableByKey = value;
                RaisePropertyChanged();
            }
        }

        public bool VisiablePredicate(string visiableByValue)
        {
            return PluginKV.VisiablePredicate(visiableByValue);
        }
        public ICommand SelectFilePathCommand { get; set; }
        public ICommand TestWebApiCommand { get; set; }
        public ICommand BackupFilePathCommand { get; set; }
        public string ExecutorName
        {
            get
            {
                return PluginKV.ExecutorName;
            }
            set
            {
                PluginKV.ExecutorName = value;
                RaisePropertyChanged();
            }
        }
        public void TestExecutor()
        {
            try
            {
                Tuple<string, string> item;
                if (Executor != null)
                {
                    item = Executor(PluginKV.Key, PluginKV.Value);
                }
                else
                {
                    item = PluginKV.Executor(PluginKV.Key, PluginKV.Value);
                }
                var title = item.Item1;
                var message = item.Item2;
                MessageBox.Show(message, "提示");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// 用作全局选项执行用.
        /// </summary>
        public Func<string, string, Tuple<string, string>> Executor { get; set; }

        public ProtocalKV PluginKV { get; }

        private void InitExecutor()
        {
            switch (PluginKV.KvType)
            {
                case KvType.String:
                    break;
                case KvType.Int:
                    break;
                case KvType.Float:
                    break;
                case KvType.File:
                    SelectFilePathCommand = new RelayCommand(SelectFilePath);
                    break;
                case KvType.Combobox:
                    break;
                case KvType.Bool:
                    break;
                case KvType.StringApi:
                    TestWebApiCommand = new RelayCommand(TestExecutor);
                    break;
                case KvType.BackupFile:
                    BackupFilePathCommand = new RelayCommand(BackupFilePath);
                    break;
            }
        }
    }
}
