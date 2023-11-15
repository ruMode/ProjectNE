using Microsoft.Maui.Controls;
using System.Drawing;

namespace ProjectNE
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

        }

        private void ChangeAppTheme_Clicked(object sender, EventArgs e)
        {
            if (App.Current.UserAppTheme == AppTheme.Dark) App.Current.UserAppTheme = AppTheme.Light;
            else App.Current.UserAppTheme = AppTheme.Dark;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.database.DeleteTables();
        }

    }
}