using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using ProjectNE.Models;
using ProjectNE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.ViewModels
{
    public partial class ReportsPageVM : ObservableObject
    {
        public ReportsPageVM()
        {

        }

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;
        [ObservableProperty]
        private DateTime endDate = DateTime.Now;

        //[ObservableProperty]
        //private string name;

        //[ObservableProperty]
        //private int quantity;

        [ObservableProperty]
        private double buySum;

        [ObservableProperty]
        private double sellSum;

        [ObservableProperty]
        private double income;

        [ObservableProperty]
        private bool isReportListVisible =false;



        [ObservableProperty]
        ObservableCollection<Orders> ordersList = new();
        [ObservableProperty]
        ObservableCollection<OrderItems> orderBuyItemsList = new();
        [ObservableProperty]
        ObservableCollection<OrderItems> orderSellItemsList = new();

        [RelayCommand]
        private async void DateChanged()
        {
            if(EndDate <  StartDate) { await App.Current.MainPage.DisplayAlert("Внимание!", "Конец периода не может быть раньше начала!", "Ок"); EndDate = StartDate; }
        }
        
        [RelayCommand]
        private void CreateReport()
        {
            OrderSellItemsList.Clear();
            OrderBuyItemsList.Clear();
            BuySum = SellSum = Income = 0;
            IsReportListVisible = true;
            //creating report


            //get orders
            OrdersList = App.database.GetOrdersAsync().Result.Where(order => (order.DATE >=StartDate && order.DATE<=EndDate)).ToObservableCollection();
            ObservableCollection<Orders> incomeOrders = OrdersList.Where(i => i.INVOICE_TYPE == "Приход").ToObservableCollection();
            ObservableCollection<Orders> outcomeOrders = OrdersList.Where(i => i.INVOICE_TYPE == "Расход").ToObservableCollection();
            ObservableCollection<OrderItems> orderItems = new();


            for (int i = 0; i < incomeOrders.Count; i++) 
            {
                orderItems = JsonConvert.DeserializeObject<ObservableCollection<OrderItems>>(incomeOrders[i].ITEMS_JSON);
                for (int j = 0; j < orderItems.Count; j++)
                {
                    OrderBuyItemsList.Add(orderItems[j]);
                }
            }


            for (int i = 0; i < outcomeOrders.Count; i++) 
            {
                orderItems = JsonConvert.DeserializeObject<ObservableCollection<OrderItems>>(outcomeOrders[i].ITEMS_JSON);
                for (int j = 0; j < orderItems.Count; j++)
                {
                    OrderSellItemsList.Add(orderItems[j]);
                }
            }

            BuySum = OrderBuyItemsList.Sum(i => i.SUM);
            SellSum = OrderSellItemsList.Sum(i => i.SUM);
            Income = SellSum - BuySum;

            GmailSMTP.SendMail(GmailSMTP.CreateMail($"Company name. Отчет за {StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}", 
                $"Сумма продаж: {SellSum};<br>Сумма закупок: {BuySum};<br>Выручка: {Income}"));
        }

        
    }
}
