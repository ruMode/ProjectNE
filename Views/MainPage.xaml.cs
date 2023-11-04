
using ProjectNE.ViewModels;

namespace ProjectNE.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageVM vm = new MainPageVM();
        public MainPage()
        {
            InitializeComponent();
            
            
            BindingContext =vm;
            this.UpdateChildrenLayout();
            this.SizeChanged += MainPage_SizeChanged;
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
           vm.ButtonWidth= this.Width /3;
        }
    }
}