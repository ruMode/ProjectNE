
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectNE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectNE.ViewModels
{
    public partial class WarehousePageVM: ObservableObject
    {
        
        public WarehousePageVM() 
        { 
            WarehouseList= new ObservableCollection<Warehouse>();
            ItemsList = new ObservableCollection<Warehouse>();
            SearchList = new ObservableCollection<Warehouse>();

 

            if (App.database.GetWarehouseItemsAsync().Result != null)
            {
               WarehouseList = App.database.GetWarehouseItemsAsync().Result;
            }
            else return;
            SearchList = WarehouseList;
            Id = WarehouseList.Count;
        }

        [ObservableProperty]
        ObservableCollection<Warehouse> warehouseList;
        [ObservableProperty]
        ObservableCollection<Warehouse> searchList;
        [ObservableProperty]
        ObservableCollection<Warehouse> itemsList;

        #region props
        [ObservableProperty]
        private int id=1;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string quantity;
        [ObservableProperty]
        private string price;
        [ObservableProperty]
        private DateTime date = DateTime.Now;
        [ObservableProperty]
        private int sum;
        #endregion

        //#region props
        //[ObservableProperty]
        //private string hid ="ID";
        //[ObservableProperty]
        //private string hname = "НАИМЕНОВАНИЕ";
        //[ObservableProperty]
        //private string hquantity ="КОЛ-ВО";
        //[ObservableProperty]
        //private string hprice="ЦЕНА";
        //[ObservableProperty]
        //private string hsum="ИТОГО";
        //#endregion

        [ObservableProperty]
        private Warehouse selectedItem=null;

        [ObservableProperty]
        private bool isItemSelected = false;

        [ObservableProperty]
        public bool isAddFrameVisible = false;
        
        [ObservableProperty]
        public bool isEditFrameVisible = false;

        [ObservableProperty]
        private SelectionMode selectionMode = SelectionMode.Single;

        [ObservableProperty]
        private Microsoft.Maui.Graphics.Color selectedItemBackgroundColor = Colors.SkyBlue;


        [ObservableProperty]
        System.Drawing.Color border;

        [ObservableProperty]
        private string searchBarText;

        [RelayCommand]
        private async void Add() {

            if (!string.IsNullOrEmpty(Name))
            {
                try
                {
                    //Border = Microsoft.Maui.Graphics.Color.Parse("Transparent");
                    Warehouse newItem = new();
                    newItem.NAME = Name;
                    //newItem.PRICE = double.Parse(Price);
                    newItem.QUANTITY = int.Parse(Quantity);
                    newItem.DATE = DateTime.Now;

                    await App.database.SaveWarehouseItemAsync(newItem);

                    WarehouseList.Add(newItem);  

                    IsAddFrameVisible = false;
                    Name = Quantity = Price = null;
                    SearchList = WarehouseList;
                    Id=newItem.ID;
                }
                catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"); return; }

            }
            else return;
        }
        [RelayCommand]
        private async void Edit() 
        {
            SelectedItem.NAME = Name;
            //SelectedItem.PRICE = double.Parse(Price);
            SelectedItem.QUANTITY = int.Parse(Quantity);
            SelectedItem.DATE = DateTime.Now;


            await App.database.SaveWarehouseItemAsync(SelectedItem);
            
            SearchList = WarehouseList=App.database.GetWarehouseItemsAsync().Result;
            IsEditFrameVisible = false;
            await App.Current.MainPage.DisplayAlert("Success", "Успешно изменено", "Ok");
            
        }
        
        [RelayCommand]
        private async void Delete() {
           var isReallyDelete= await App.Current.MainPage.DisplayAlert("Внимание!", "Удаление элемента безвозвратно.\nВы уверенны?", "Да", "Нет");
            if (isReallyDelete)
            {
                try
                {
                    await App.database.DeleteWarehouseItemAsync(SelectedItem);
                    Id=SelectedItem.ID;
                    await App.Current.MainPage.DisplayAlert("Success", "Успешно удалено", "Ok");
                }
                catch(Exception ex) { await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"); return; }
            }
            WarehouseList.Remove(SelectedItem);
            IsItemSelected = false; SelectedItem = null;
        }

        [RelayCommand]
        private void ShowAddFrame()
        {
            IsAddFrameVisible = true;
            Name = Quantity = Price = null;
        }

        [RelayCommand]
        private void ShowEditFrame()
        {
            IsEditFrameVisible = true;
            Name = SelectedItem.NAME;
            Quantity = SelectedItem.QUANTITY.ToString();
            //Price = SelectedItem.PRICE.ToString();
        }

        [RelayCommand]
        private  void SelectionChanged() { 
            


            IsItemSelected = true;
            SelectedItemBackgroundColor = Colors.SkyBlue;
            if (SelectedItem != null)
            {
                //await App.Current.MainPage.DisplayAlert("Alert", $"u r selected: {SelectedItem.NAME}", "Ok");
            }
            //SelectedItem = null; 
        }

        [RelayCommand]
        private void SearchBarTextChanged()
        { 
            
            if (!string.IsNullOrWhiteSpace(SearchBarText))
            {
                ItemsList.Clear();
                for (int i = 0; i < WarehouseList.Count; i++)
                {
                    if (!WarehouseList[i].NAME.ToLower().Contains(SearchBarText.ToLower()))
                    {
                        continue;
                    }
                    else ItemsList.Add(WarehouseList[i]);
                    
                }
                SearchList = ItemsList;
            }
            else SearchList = WarehouseList;
        }

    
    
    
    }
}
