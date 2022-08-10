using Tournaments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace Tournaments.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler Executed;
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            if (parameter != null && (bool)parameter)
                Executed?.Invoke(this, new EventArgs());
        }

        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }

    public abstract class DisposableCommandBase<T> : CommandBase, IDisposable where T : INotifyPropertyChanged
    {
        protected readonly T ViewModel;
        public DisposableCommandBase(T viewModel)
        {
            ViewModel = viewModel;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        protected abstract void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e);

        public virtual void Dispose()
        {
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
    }
}
