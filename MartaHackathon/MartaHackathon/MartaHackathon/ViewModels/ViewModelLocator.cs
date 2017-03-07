using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartaHackathon.ViewModels
{
    class ViewModelLocator
    {
        private MainViewModel _mainViewModel;
        public MainViewModel MainViewModel => _mainViewModel ?? (_mainViewModel = new MainViewModel());

        private MoreInfoViewModel _moreInfoViewModel;

        public MoreInfoViewModel MoreInfoViewModel
            => _moreInfoViewModel ?? (_moreInfoViewModel = new MoreInfoViewModel());
    }
}
