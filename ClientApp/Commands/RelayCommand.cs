using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientApp.Commands
{
    public interface IRelayCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }

    public class RelayCommand : IRelayCommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
            _canExecute = (o) => true;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand<T> : IRelayCommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<T> _execute;
        private Action _executeNoParams;
        private Predicate<T> _canExecute;
        private Func<bool> _canExecuteNoParams;

        #region Constructors
        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
            _canExecute = (o) => true;
        }

        public RelayCommand(Action execute)
        {
            _executeNoParams = execute;
            _canExecuteNoParams = () => true;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute) : this(execute)
        {
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Predicate<T> canExecute) : this(execute)
        {
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute) : this(execute)
        {
            _canExecuteNoParams = canExecute;
        }
        #endregion

        public bool CanExecute(object parameter)
        {
            var primitiveTypeCheck = (T)parameter;
            return _canExecute?.Invoke(primitiveTypeCheck) ?? _canExecuteNoParams.Invoke();
        }

        public void Execute(object parameter)
        {
            var primitiveTypeCheck = (T)parameter;

            _execute?.Invoke(primitiveTypeCheck);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
