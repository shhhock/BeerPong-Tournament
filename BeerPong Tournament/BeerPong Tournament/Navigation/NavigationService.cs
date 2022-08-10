using Tournaments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Navigation
{
    public class NavigationService<TMaster, TViewModel> where TViewModel : ViewModelBase where TMaster : ViewModelBase
    {
        private readonly NavigationStore<TMaster> _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore<TMaster> navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
