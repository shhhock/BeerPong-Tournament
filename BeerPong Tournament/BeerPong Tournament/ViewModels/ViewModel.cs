using Tournaments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.ViewModels
{
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
