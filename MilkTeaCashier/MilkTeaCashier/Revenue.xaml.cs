using Repositories;
using Repositories.Entities;
using Services;
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
using System.Windows.Shapes;

namespace MilkTeaCashier
{
    /// <summary>
    /// Interaction logic for Revenue.xaml
    /// </summary>
    public partial class Revenue : Window
    {
        private RevenueService _service = new();
        public Revenue()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            MilkteaListData.ItemsSource = null;
            MilkteaListData.ItemsSource = _service.GetAllBills();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            BillInfo? selected = MilkteaListData.SelectedItem as BillInfo;

            if (selected == null)
            {
                MessageBox.Show("Please select a row before deleting", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult confirm = MessageBox.Show("Do you really want to delete?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.No)
            {
                return;
            }

            _service.RemoveBill(selected);
            FillDataGrid();
        }
    }
}
