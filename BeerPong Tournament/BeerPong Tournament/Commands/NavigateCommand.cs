using BeerPong_Tournament.Navigation;
using BeerPong_Tournament.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerPong_Tournament.Commands
{
    public class NavigateCommand<TMaster, TViewModel> : CommandBase where TViewModel : ViewModelBase where TMaster : ViewModelBase
    {
        private readonly NavigationService<TMaster, TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TMaster, TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
            base.Execute(true);
        }
    }
}
