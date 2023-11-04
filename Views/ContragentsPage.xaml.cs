using ProjectNE.ViewModels;

namespace ProjectNE.Views;

public partial class ContragentsPage : ContentPage
{
	public ContragentsPage()
	{
		InitializeComponent();
        ContragentsPageVM vm = new ContragentsPageVM();
        BindingContext = vm;

    }
    protected override void OnAppearing()
    {
        ContragentsPageVM vm = new ContragentsPageVM();
        BindingContext = vm;

        base.OnAppearing();
    }
}