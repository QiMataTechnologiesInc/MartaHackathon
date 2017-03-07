using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartaHackathon.ViewModels;
using Xamarin.Forms;

namespace MartaHackathon
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var vm = BindingContext as MainViewModel;

            if (vm == null)
            {
                return;
            }

            vm.Navigation = Navigation;
        }
    }
}
