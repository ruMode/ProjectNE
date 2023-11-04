using Microsoft.Maui.Controls.Handlers.Items;
using ProjectNE.Models;

namespace ProjectNE
{
    public partial class App : Application
    {
        public static AppDatabase database = new AppDatabase();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            database.Init();
            CollectionViewHandler.Mapper.AppendToMapping("HeaderAndFooterFix", (_, collectionView) =>
            {
                collectionView.AddLogicalChild(collectionView.Header as Element);
                collectionView.AddLogicalChild(collectionView.Footer as Element);
            });
        }

        
    }
}