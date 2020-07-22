using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataFusionPlatformPlugin.ViewModel
{
    public class RelayCommand<T> : ICommand
        where T : class
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;


        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute((T)parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;

        /// <summary>
        ///     Constructor not using canExecute.
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action execute) : this(execute, null)
        {
        }

        /// <summary>
        ///     Constructor using both execute and canExecute.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        ///     This method is called from XAML to evaluate if the command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute();

            return true;
        }

        /// <summary>
        ///     This method is called from XAML to execute the command.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute();
        }

        /// <summary>
        ///     This event notify XAML controls using the command to reevaluate the CanExecute of it.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     This method allow us to force the execution of CanExecute method to reevaluate execution.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Func<object, Task> _execute;

        private long _isExecuting;

        public AsyncRelayCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (o => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (Interlocked.Read(ref _isExecuting) != 0)
                return false;

            return _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            Interlocked.Exchange(ref _isExecuting, 1);
            RaiseCanExecuteChanged();

            try
            {
                await _execute(parameter);
            }
            catch (Exception)
            {

            }
            finally
            {
                Interlocked.Exchange(ref _isExecuting, 0);
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
