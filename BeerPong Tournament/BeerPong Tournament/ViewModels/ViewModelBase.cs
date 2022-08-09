using BeerPong_Tournament.Commands;
using BeerPong_Tournament.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BeerPong_Tournament.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual void Dispose() { }
    }

    public class ViewModel<T> : ViewModelBase where T : class, IDomain, new()
    {
        public ViewModel(T item)
        {
            Item = item;
        }

        protected override void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            Modified = true;
            base.OnPropertyChanged(name);
        }

        public T Item { get; }

        public bool Modified { get; set; }

        public virtual bool CanDelete()
        {
            return true;
        }
    }
}
