using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.ViewModels
{
    internal partial class MainPageVM:ObservableObject
    {
        //[RelayCommand]
        //private void GoToSuppliersPage() => Shell.Current.GoToAsync("///suppliers");
        [RelayCommand]
        private void GoToWarehousePage() => Shell.Current.GoToAsync("///warehouse");
        [RelayCommand]
        private void GoToContragentsPage() => Shell.Current.GoToAsync("///contragents");
        [RelayCommand]
        private void GoToReportsPage() => Shell.Current.GoToAsync("///reports");
        [RelayCommand]
        private void GoToOrdersPage() => Shell.Current.GoToAsync("///orders");


        [ObservableProperty]
        public double buttonWidth = App.Current.MainPage.Window.Width/2;
    }
}
