using Microsoft.Maui.Controls;
using ProjectNE.ViewModels;

namespace ProjectNE.Views;

public partial class WarehousePage : ContentPage
{
	public WarehousePage()
	{
		InitializeComponent();
        WarehousePageVM vm = new WarehousePageVM();
        BindingContext = vm;
		
		

    }
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        WarehousePageVM vm = new WarehousePageVM();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        WarehousePageVM vm = new WarehousePageVM();
        BindingContext = vm;
        base.OnAppearing();

    }
}