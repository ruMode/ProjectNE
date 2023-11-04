using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.ViewModels
{
    public partial class LoginPageVM : ObservableObject
    {

        [ObservableProperty]
        public bool isPasswordRevealed = true;

        [RelayCommand]
        private void RevealPassword()
        {
            IsPasswordRevealed=false || IsPasswordRevealed ==false;
        }
        [RelayCommand]
        private void GotoMainPage()
        {
            Shell.Current.GoToAsync("///mainPage");
        }
    }
}
