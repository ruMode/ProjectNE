using ProjectNE.ViewModels;

namespace ProjectNE.Views;

public partial class OrdersPage : ContentPage
{   
    public static OrdersPageVM vm = new OrdersPageVM();
	public OrdersPage()
	{
		InitializeComponent();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        BindingContext = vm;
        
    }

}