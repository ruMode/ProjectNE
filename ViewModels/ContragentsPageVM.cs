using CommunityToolkit.Mvvm.ComponentModel;
using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ProjectNE.Models;

namespace ProjectNE.ViewModels
{
    internal partial class ContragentsPageVM : ObservableObject
    {
        public ContragentsPageVM()
        {
            SuppliersList = App.database.GetSuppliersAsync().Result;
            ItemsList = new ObservableCollection<Contragents>();
            SearchList = new ObservableCollection<Contragents>();

            SearchList = SuppliersList;
            Id = SuppliersList.Count;
        }

        [ObservableProperty]
        ObservableCollection<Contragents> suppliersList;
        [ObservableProperty]
        ObservableCollection<Contragents> searchList;
        [ObservableProperty]
        ObservableCollection<Contragents> itemsList;

        #region props
        [ObservableProperty]
        private int id = 1;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string address;
        [ObservableProperty]
        private DateTime date = DateTime.Now;
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
        private Contragents selectedItem = null;

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
        private async void Add()
        {

            if (!string.IsNullOrEmpty(Name))
            {
                try
                {
                    //Border = Microsoft.Maui.Graphics.Color.Parse("Transparent");
                    Contragents newItem = new();
                    newItem.NAME = Name;
                    newItem.ADDRESS = Address;
                    newItem.DATE = DateTime.Now;

                    await App.database.SaveSupplierAsync(newItem);

                    SuppliersList.Add(newItem);

                    IsAddFrameVisible = false;
                    Name = Address = null;
                    SearchList = SuppliersList;
                    Id = newItem.ID;
                }
                catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"); return; }

            }
            else return;
        }
        [RelayCommand]
        private async void Edit()
        {
            SelectedItem.NAME = Name;
            SelectedItem.ADDRESS = Address;
            SelectedItem.DATE = DateTime.Now;


            await App.database.SaveSupplierAsync(SelectedItem);

            SearchList = SuppliersList = App.database.GetSuppliersAsync().Result;
            IsEditFrameVisible = false;
            await App.Current.MainPage.DisplayAlert("Success", "Успешно изменено", "Ok");

        }

        [RelayCommand]
        private async void Delete()
        {
            var isReallyDelete = await App.Current.MainPage.DisplayAlert("Внимание!", "Удаление элемента безвозвратно.\nВы уверенны?", "Да", "Нет");
            if (isReallyDelete)
            {
                try
                {
                    await App.database.DeleteContragentAsync(SelectedItem);
                    Id = SelectedItem.ID;
                    await App.Current.MainPage.DisplayAlert("Success", "Успешно удалено", "Ok");
                }
                catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"); return; }
            }
            SuppliersList.Remove(SelectedItem);
            IsItemSelected = false; SelectedItem = null;
        }

        [RelayCommand]
        private void ShowAddFrame()
        {
            IsAddFrameVisible = true;
            Name = Address  = null;
        }

        [RelayCommand]
        private void ShowEditFrame()
        {
            IsEditFrameVisible = true;
            Name = SelectedItem.NAME;
            Address = SelectedItem.ADDRESS;
        }

        [RelayCommand]
        private void SelectionChanged()
        {



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
                for (int i = 0; i < SuppliersList.Count; i++)
                {
                    if (!SuppliersList[i].NAME.ToLower().Contains(SearchBarText.ToLower()))
                    {
                        continue;
                    }
                    else ItemsList.Add(SuppliersList[i]);

                }
                SearchList = ItemsList;
            }
            else SearchList = SuppliersList;
        }


    }
}
