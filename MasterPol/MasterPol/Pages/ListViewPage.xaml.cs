using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MasterPol.Pages
{
    public partial class ListViewPage : Page
    {
        public ListViewPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            var par = Data.MasterFloorEntities.GetContext().PartnersImport.ToList();
            var partDiscounts = (from partner in par
                                 join product in Data.MasterFloorEntities.GetContext().PartnerProductsImport
                                 on partner.Id equals product.IdPartnerName
                                 group product by partner
                                 into g
                                 select new
                                 {
                                     Partner = g.Key,
                                     Discount = CalculateDiscount(g.Sum(p => p.CountOfProduction))
                                 }).ToList();
            MasterListView.ItemsSource = partDiscounts;
        }
        private int CalculateDiscount(int count)
        {
            if (count < 10000) 
                return 0;
            if (count >= 10000 && count < 50000) 
                return 5;
            if (count >= 50000 && count < 300000)
                return 10;
            return 15;
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button edit = sender as Button;
            var Data = edit.DataContext;
            if (Data != null)
            {
                var partner = (Data as dynamic).Partner;
                Classes.Manager.MainFrame.Navigate(new Pages.AddEditPage(partner));
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.AddEditPage(null));
        }
        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Button history = sender as Button;
            var Data = history.DataContext;
            if (Data != null)
            {
                var partner = (Data as dynamic).Partner;
                Classes.Manager.MainFrame.Navigate(new HistoryPage(partner));
            }
        }
    }
}