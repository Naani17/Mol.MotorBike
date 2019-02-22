using System;
using System.Windows.Input;

namespace MotorBikeView.Bindable
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<bool> _func;

        public RelayCommand(Action<object> action, Func<bool> func = null)
        {
            _action = action;
            _func = func;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter) => _func == null || _func();

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
