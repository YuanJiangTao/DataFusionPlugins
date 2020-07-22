using DataFusionProtocal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Net;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class ProtocalMonitorViewModel : ViewModelBase,IDisposable
    {
        private DataTable _dataSource;
        private readonly int[] _primaryColumnIndexes;
        private ProtocalMonitor _pluginMonitor;
        public ProtocalMonitorViewModel(ProtocalMonitor pluginMonitor)
        {
            _pluginMonitor = pluginMonitor;
            _pluginMonitor.WatchEvent += _pluginMonitor_WatchEvent;
            _dataSource = new DataTable();
            LoadCommand = new RelayCommand<RoutedEventArgs>(DataGridLoad);
            AutoGeneratingColumnCommand = new RelayCommand<DataGridAutoGeneratingColumnEventArgs>(AutoGeneratingColumn);

            if (_pluginMonitor.Columns != null)
            {
                foreach (var column in _pluginMonitor.Columns)
                {
                    _dataSource.Columns.Add(new DataColumn(column));
                }
                if (_pluginMonitor.PrimaryKeys != null)
                {
                    if (!_pluginMonitor.PrimaryKeys.All(o => _pluginMonitor.Columns.Any(t => t == o)))
                        throw new ArgumentOutOfRangeException("主键应该在所有列明中间产生。");
                    _dataSource.PrimaryKey = _pluginMonitor.PrimaryKeys.Select(o => _dataSource.Columns[o]).ToArray();
                }
            }
            _primaryColumnIndexes = _dataSource.PrimaryKey.Select(o => o.Ordinal).ToArray();
        }

        private void AutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Guid")
                e.Cancel = true;
        }

        private bool scrollStop = false;
        private DateTime _scrollStopTime;
        private void DataGridLoad(RoutedEventArgs e)
        {
            var dataGrid = (System.Windows.Controls.DataGrid)e.Source;

            dataGrid.PreviewMouseWheel += (o, ea) =>
            {
                var delta = ea.Delta;

                var offset = 100;
                if (delta > offset)
                {
                    // 向上滚动.
                    scrollStop = true;
                    _scrollStopTime = DateTime.Now;
                }

                if (delta < -offset)
                {
                    // 向下滚动
                    scrollStop = false;
                }
            };
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;

            timer.Tick += (o, ea) =>
            {
                if (DateTime.Now.Subtract(_scrollStopTime).TotalSeconds > 60)
                {
                    scrollStop = false;
                }

                if (scrollStop) return;
                if (dataGrid.Items.Count == 0)
                    return;

                dataGrid.ScrollIntoView(dataGrid.Items[dataGrid.Items.Count - 1]);
            };
        }

        private void _pluginMonitor_WatchEvent(object sender, ProtocalMonitorEventArgs e)
        {
            Add(e.Fields);
        }

        public string Id
        {
            get
            {
                return _pluginMonitor.Id;
            }
        }

        public string Title
        {
            get => _pluginMonitor.Title;
            set
            {
                _pluginMonitor.Title = value;
                RaisePropertyChanged();
            }
        }
        internal void Add(params string[] fields)
        {
            Task.Factory.StartNew(() =>
            {
                if (_dataSource.Rows.Count > 1000)
                    for (var i = 0; i < 500; i++)
                        _dataSource.Rows.RemoveAt(i);
                var existRow = ContainDataRowInDataTable(_primaryColumnIndexes, _dataSource, fields);
                if (existRow != null)
                    for (var i = 0; i < _dataSource.Columns.Count; i++)
                    {
                        if (i < fields.Count())
                            existRow[i] = fields[i];
                    }
                else
                    _dataSource.Rows.Add(fields);
            }, CancellationToken.None, TaskCreationOptions.None, UIContext.Current);
        }
        private static DataRow ContainDataRowInDataTable(int[] primaryColumnIndexes, DataTable T, object[] values)
        {
            foreach (DataRow item in T.Rows)
                if (primaryColumnIndexes.Any())
                {
                    var rowPrimary = primaryColumnIndexes.Select(o => item[o] == null ? null : item[o].ToString())
                        .ToArray();
                    var valuePrimary = primaryColumnIndexes.Select(o => values[o] == null ? null : values[o].ToString())
                        .ToArray();
                    if (rowPrimary.SequenceEqual(valuePrimary))
                        return item;
                }
                else
                {
                    break;
                }
            return null;
        }

        public  void  Dispose()
        {
            try
            {
                _dataSource.Dispose();
                _pluginMonitor.WatchEvent -= _pluginMonitor_WatchEvent;
            }
            catch (Exception)
            {

            }
        }
        public DataView DataViewSource { get { return _dataSource.DefaultView; } }
        public ICommand LoadCommand { get; set; }

        public ICommand AutoGeneratingColumnCommand { get; set; }
    }
    public class UIContext
    {
        public static TaskScheduler Current { get; private set; }
        public static void Initialize()
        {
            if (Current != null)
                return;

            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            Current = TaskScheduler.FromCurrentSynchronizationContext();     //获得对同步上下文调度器的引用
        }
    }
}
