using ProjectNE.ViewModels;

namespace ProjectNE.Views;

public partial class ReportsPage : ContentPage
{
	public ReportsPage()
	{
		InitializeComponent();
        ReportsPageVM reportsVM = new();
		BindingContext = reportsVM;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ReportsPageVM reportsVM = new();
        BindingContext = reportsVM;

    }

}
