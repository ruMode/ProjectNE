using ProjectNE.ViewModels;

namespace ProjectNE.Views;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage()
	{
		InitializeComponent();
        
        BindingContext = OrdersPage.vm;

    }
}