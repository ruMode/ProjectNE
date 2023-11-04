using CommunityToolkit.Maui.Core.Extensions;
using SQLite;
using System.Collections.ObjectModel;

namespace ProjectNE.Models
{
    public class AppDatabase
    {
        SQLiteAsyncConnection Database;
        public AppDatabase() {}

        public async Task Init()
        {
            if (Database is not null)
                return;
            
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            
            await Database.CreateTablesAsync<Warehouse, Contragents, Orders>();

        }

        //CRUD 

        //CREATE | UPDATE
        public async Task<int> SaveWarehouseItemAsync(Warehouse item)
        {
            await Init();

            if (item.ID != 0)
                return await Database.UpdateAsync(item);
                
            else
                return await Database.InsertAsync(item);
            
        }
        public async Task<int> SaveManyWarehouseItemsAsync(ObservableCollection<Warehouse> items)
        {
            await Init();

            //ObservableCollection<Warehouse> warehouses = this.GetWarehouseItemsAsync().Result;

            //for (int i = 0; i < items.Count; i++)
            //{
            //    if (warehouses.Contains(items[i]))
            //    {
            //        int index = warehouses.IndexOf(items[i]);

            //        if(!isSale) items[i].QUANTITY += warehouses[index].QUANTITY;
            //        else items[i].QUANTITY = warehouses[index].QUANTITY-items[i].QUANTITY;

            //        await Database.UpdateAsync(items[i]);
            //    }
            //    else await Database.InsertAsync(items[i]);
            //}

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID != 0)
                     await Database.UpdateAsync(items[i]);

                else
                     await Database.InsertAsync(items[i]);


            }
            return items.Count;
        }
        public async Task<int> SaveSupplierAsync(Contragents item)
        {
            await Init();

            if (item.ID != 0)
                return await Database.UpdateAsync(item);
                
            else
                return await Database.InsertAsync(item);
            
        }
        public async Task<int> SaveOrdersAsync(Orders item)
        {
            await Init();
            
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
                
            else
                return await Database.InsertAsync(item);
            
        }


        //READ
        public async Task<ObservableCollection<Warehouse>> GetWarehouseItemsAsync()
        {
            await Init();
            List<Warehouse> items = new ();
            items =Database.Table<Warehouse>().ToListAsync().Result;
            ObservableCollection<Warehouse> itemsOC = new ();
            for (int i = 0; i < items.Count; i++)
            {
                itemsOC.Add(items[i]);
            }
            return itemsOC;
        }
        public async Task<ObservableCollection<Contragents>> GetSuppliersAsync()
        {
            await Init();
            List<Contragents> items = Database.Table<Contragents>().ToListAsync().Result;
            //ObservableCollection<Contragents> itemsOC =items.ToObservableCollection();
            //for (int i = 0; i < items.Count; i++)
            //{
            //    itemsOC.Add(items[i]);
            //}
            return items.ToObservableCollection();
        }
        public async Task<ObservableCollection<Orders>> GetOrdersAsync()
        {
            await Init();
            
            List<Orders> items = Database.Table<Orders>().ToListAsync().Result;
            
            ObservableCollection<Orders> itemsOC = new ();
            for (int i = 0; i < items.Count; i++)
            {
                itemsOC.Add(items[i]);
            }
            return itemsOC;
        }

        //DELETE
        public async Task<int> DeleteWarehouseItemAsync (Warehouse item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
        public async Task<int> DeleteContragentAsync (Contragents item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
        public async Task<int> DeleteOrderAsync (Orders item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }




















































        public async void DeleteTables()
        {
           await Database.DropTableAsync<Orders>();
           await Database.DropTableAsync<Warehouse>();
           await Database.DropTableAsync<Contragents>();
            await Database.CloseAsync();
        }
    }
}
