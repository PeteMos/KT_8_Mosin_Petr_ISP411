using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterPol.Pages
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private readonly Data.PartnersImport _selectedPartner;
        public HistoryPage(Data.PartnersImport selectedPartner)
        {
            InitializeComponent();
            _selectedPartner = selectedPartner;
            LoadHistory();
        }
        private void LoadHistory()
        {
            var histories = Data.MasterFloorEntities.GetContext().PartnerProductsImport
                .Where(m => m.IdPartnerName == _selectedPartner.Id)
                .Select(m => new
                {   
                    Production = m.Production,
                    CountOfProduction = m.CountOfProduction,
                    DateOfSale = m.DateOfSale
                }).ToList();
            HistoryDataGrid.ItemsSource = histories;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.ListViewPage());
        }
    }
}