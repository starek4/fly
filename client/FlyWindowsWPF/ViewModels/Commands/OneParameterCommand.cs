

using System;
using System.Windows;
using System.Windows.Input;

namespace FlyWindowsWPF.ViewModels.Commands
{
    public class CommandHandler : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public CommandHandler(Action<object> execute, Predicate<object> canExecute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Refresh()
        {
            Application.Current.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
