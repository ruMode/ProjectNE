using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ProjectNE.Models;
using ProjectNE.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.ViewModels
{
    public partial class OrdersPageVM: ObservableObject
    {
        public OrdersPageVM()
        {
            OrdersList = new ObservableCollection<Orders>();
            ItemsList = new ObservableCollection<Orders>();
            SearchList = new ObservableCollection<Orders>();
            StockItemsList = new ObservableCollection<Warehouse>();
            WarehouseItemsList = App.database.GetWarehouseItemsAsync().Result;
            OrderItemsList = new ObservableCollection<OrderItems>();
            Date = DateTime.Now;

            if (App.database.GetOrdersAsync().Result != null)
            {
                OrdersList = App.database.GetOrdersAsync().Result;
            }
            else return;
           // CustomersList = App.database.GetContragentsAsync().Result;
            SearchList = OrdersList;
            Id = OrdersList.Count;
            
        }

        #region collections
        [ObservableProperty]
        ObservableCollection<Orders> ordersList;
        [ObservableProperty]
        ObservableCollection<Orders> searchList;
        [ObservableProperty]
        ObservableCollection<Orders> itemsList;
        [ObservableProperty]
        ObservableCollection<Contragents> customersList;
        [ObservableProperty]
        ObservableCollection<Warehouse> stockItemsList;
        [ObservableProperty]
        ObservableCollection<Warehouse> warehouseItemsList;
        [ObservableProperty]
        ObservableCollection<OrderItems> orderItemsList;
        
        #endregion
        
        #region props
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string title;
        [ObservableProperty]
        private string price;
        [ObservableProperty]
        private string quantity;
        [ObservableProperty]
        private string customer_name;
        [ObservableProperty]
        private double sum ;
        [ObservableProperty]
        private DateTime date;
        [ObservableProperty]
        private List<OrderItems> items = new();
        
        #endregion


        [ObservableProperty]
        private Orders selectedItem;

        [ObservableProperty]
        private bool isItemSelected = false;

        [ObservableProperty]
        public bool isAddFrameVisible = false;

        [ObservableProperty]
        public bool isEditFrameVisible = false;

        [ObservableProperty]
        public bool isShowInfoButtonEnabled = false;

        [ObservableProperty]
        public bool isProductFieldsAreFilled;

        [ObservableProperty]
        public bool isOrderFieldsAreFilled;

        [ObservableProperty]
        public bool isSale ;

        [ObservableProperty]
        public ObservableCollection<string> typesOfInvoice = new() { "Приход", "Расход"};

        [ObservableProperty]
        private string selectedInvoiceType="Приход";
        
        [ObservableProperty]
        private SelectionMode selectionMode = SelectionMode.Single;

        [RelayCommand]
        private void DateChanged()
        {
            
        }


        [ObservableProperty]
        private string searchBarText;

        [RelayCommand]
        private void TextChanged()
        {
            IsOrderFieldsAreFilled = true? !string.IsNullOrEmpty(Title)&&!string.IsNullOrEmpty(Customer_name)&&OrderItemsList.Count!=0:false;
            IsProductFieldsAreFilled = true? !string.IsNullOrEmpty(Product)&&!string.IsNullOrEmpty(Quantity)&& !string.IsNullOrEmpty(Price) : false;

        }


        [RelayCommand]
        private async void Add()
        {

            if (IsOrderFieldsAreFilled)
            {
                try
                {
                    Id++;
                    Orders newItem = new();

                    ObservableCollection<Contragents> ContragentsList = await App.database.GetSuppliersAsync();
                    if(!ContragentsList.Where(i=> i.NAME == Customer_name).Any()) {
                       await App.database.SaveSupplierAsync(new Contragents()
                        {
                            NAME = Customer_name,
                            DATE = DateTime.Now
                        });
                    }

                    newItem.TITLE = Title;
                    newItem.CUSTOMER_NAME = Customer_name;
                    newItem.DATE = Date;
                    newItem.ITEMS_JSON = JsonConvert.SerializeObject(OrderItemsList);
                    newItem.SUM = Sum;
                    newItem.INVOICE_TYPE = SelectedInvoiceType;
                    await App.database.SaveOrdersAsync(newItem);


                    await App.database.SaveManyWarehouseItemsAsync(StockItemsList);
                    OrdersList.Add(newItem);

                    IsAddFrameVisible = false;
                    
                    
                    SearchList = OrdersList;
                    StockItemsList.Clear();
                    OrderItemsList.Clear();
                }
                catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"); return; }

            }
            else return;
           
        }
        //[RelayCommand]
        //private async void Edit()
        //{
        //    SelectedItem.TITLE = Title;
        //    SelectedItem.CUSTOMER_NAME = Customer_name;
        //    SelectedItem.DATE = DateTime.Now;


        //    await App.database.SaveOrdersAsync(SelectedItem);

        //    SearchList = OrdersList = App.database.GetOrdersAsync().Result;
        //    IsEditFrameVisible = false;
        //    await App.Current.MainPage.DisplayAlert("Success", "Успешно изменено", "Ok");

        //}

        [RelayCommand]
        private async void Delete()
        {
            var isReallyDelete = await App.Current.MainPage.DisplayAlert("Внимание!", "Удаление элемента безвозвратно.\nВы уверенны?", "Да", "Нет");
            if (isReallyDelete)
            {
                try
                {
                    await App.database.DeleteOrderAsync(SelectedItem);
                    Id = SelectedItem.ID;
                    
                }
                catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"); return; }
            }
            OrdersList.Remove(SelectedItem);
            await App.Current.MainPage.DisplayAlert("Success", "Успешно удалено", "Ok");
            IsItemSelected = false; SelectedItem = null;
        }

        [RelayCommand]
        private void ShowAddFrame()
        {
            Date = DateTime.Now;
            Title = Customer_name = Price = Quantity = Product = null; Sum = 0;
            IsAddFrameVisible = true;
            StockItemsList.Clear();
            OrderItemsList.Clear();

        }

        //[RelayCommand]
        //private void ShowEditFrame()
        //{
        //    IsEditFrameVisible = true;
        //    Title = SelectedItem.TITLE;
        //    Customer_name = SelectedItem.CUSTOMER_NAME;
        //}


        [RelayCommand]
        private async void ShowOrderInfo()
        {
            
            await Shell.Current.GoToAsync("///orderDetails", true);
        }

        [RelayCommand]
        private void SelectionChanged()
        {
            IsShowInfoButtonEnabled = true;
            
            IsItemSelected = true;
            if (SelectedItem != null)
            {
                Title = SelectedItem.TITLE;
                Customer_name = SelectedItem.CUSTOMER_NAME;
                
                OrderItemsList = JsonConvert.DeserializeObject<ObservableCollection<OrderItems>>(SelectedItem.ITEMS_JSON);


                Sum = SelectedItem.SUM;
            }
            //SelectedItem = null; 
        }


        //реалтайм поиск
        [RelayCommand]
        private void SearchBarTextChanged()
        {

            if (!string.IsNullOrWhiteSpace(SearchBarText)) //если поле поиска пустое то отображаем весь список
            {
                ItemsList.Clear();
                for (int i = 0; i < OrdersList.Count; i++)
                {
                    if (!OrdersList[i].TITLE.ToLower().Contains(SearchBarText.ToLower()))
                    {
                        continue;
                    }
                    else ItemsList.Add(OrdersList[i]);

                }
                SearchList = ItemsList;
            }
            else SearchList = OrdersList;
        }



        [ObservableProperty]
        public string product; 

        [RelayCommand]
        private async void AddProduct()
        {
            WarehouseItemsList = App.database.GetWarehouseItemsAsync().Result;
            IsSale = true ? SelectedInvoiceType == "Расход" : false;
            //List<string> products = WarehouseItemsList.Where(i=>i.NAME==Product).ToString();
            try
            {
                OrderItems order = new();

                order.QUANTITY = int.Parse(Quantity);
                order.PRICE =  double.Parse(Price);
                order.NAME = Product;

                Warehouse wrhI = new();
                wrhI.QUANTITY = int.Parse(Quantity);
                wrhI.NAME = Product;
                wrhI.DATE = DateTime.Now;

                Warehouse _temp = WarehouseItemsList.Where(i => i.NAME.ToLower() == wrhI.NAME.ToLower()).FirstOrDefault();

                if (WarehouseItemsList.Contains(_temp))
                {
                    int index = WarehouseItemsList.IndexOf(WarehouseItemsList.Where(i => i.NAME.ToLower() == wrhI.NAME.ToLower()).FirstOrDefault());

                    wrhI.ID =_temp.ID ;
                    if (IsSale)
                    {
                        if (WarehouseItemsList[index].QUANTITY - wrhI.QUANTITY < 0)
                        {
                            await App.Current.MainPage.DisplayAlert("Ошибка", $"Вы пытаетесь продать больше чем есть на складе! \n Доступно {_temp.QUANTITY}", "Ok");
                            return;
                        }
                        else
                        {
                            wrhI.QUANTITY = WarehouseItemsList[index].QUANTITY - wrhI.QUANTITY;
                            wrhI.ID = _temp.ID;
                            StockItemsList.Add(wrhI);
                        }

                    }
                    else
                        wrhI.QUANTITY += WarehouseItemsList[index].QUANTITY;
                        StockItemsList.Add(wrhI);

                }
                else StockItemsList.Add(wrhI);

                //order.QUANTITY = wrhI.QUANTITY;
                OrderItemsList.Add(order);
                Sum += double.Parse(Price) * int.Parse(Quantity);
                Product=Price = Quantity =  null;
                
            }
            catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"); return; }
           
            
        }

        [RelayCommand]
        private async void GoBack() => await Shell.Current.GoToAsync("///orders");


    }
}
