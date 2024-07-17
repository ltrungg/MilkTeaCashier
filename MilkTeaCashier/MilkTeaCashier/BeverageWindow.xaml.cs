using Repositories.Entities;
using Services;
using System.Windows;


namespace MilkTeaCashier
{

    public partial class BeverageWindow : Window
    {
        private BeverageService _beverageService = new ();
        private BeverageCategoryService _beverageCategoryService = new ();
        public BeverageWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }


        private void ClearData()
        {
            BeverageIDTextBox.Text = string.Empty;
            BeverageNameTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            BeverageCategoryComboBox.SelectedValue = 1;
        }
        

        private void LoadDataGrid()
        {
            BeverageDataGrid.ItemsSource = null;
            BeverageDataGrid.ItemsSource = _beverageService.GetBeverages();

            BeverageCategoryComboBox.ItemsSource = null;
            BeverageCategoryComboBox.ItemsSource = _beverageCategoryService.GetCategories();
            BeverageCategoryComboBox.DisplayMemberPath = "Name";
            BeverageCategoryComboBox.SelectedValuePath = "Id";

            ClearData();
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Beverage selectedBeverage = BeverageDataGrid.SelectedValue as Beverage;


            if (selectedBeverage == null)
            {
                MessageBox.Show("Please select a beverage to update!", "Select one beverage", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            BeverageDetailsGrid.Visibility = Visibility.Visible;

            if (selectedBeverage != null)
            {
                BeverageIDTextBox.Text = selectedBeverage.Id.ToString();
                BeverageNameTextBox.Text = selectedBeverage.Name.ToString();
                PriceTextBox.Text = selectedBeverage.Price.ToString();
                BeverageCategoryComboBox.SelectedValue = selectedBeverage.IdCategory.ToString();
            }

        }

        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ClearData();

            BeverageDetailsGrid.Visibility = Visibility.Visible;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Beverage selectedBeverage = BeverageDataGrid.SelectedValue as Beverage;
            if (selectedBeverage == null)
            {
                MessageBox.Show("Please select a beverage to delete!", "Select one beverage", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you want to delete this beverage?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }

            _beverageService.DeleteBeverage(selectedBeverage);
            LoadDataGrid();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Beverage updateBeverage = new();
            updateBeverage.Id = int.Parse(BeverageIDTextBox.Text);
            updateBeverage.Name = BeverageNameTextBox.Text;
            updateBeverage.Price = double.Parse(PriceTextBox.Text);
            updateBeverage.IdCategory = int.Parse(BeverageCategoryComboBox.SelectedValue.ToString());

            _beverageService.UpdateBeverage(updateBeverage);
            BeverageDetailsGrid.Visibility = Visibility.Collapsed;

            LoadDataGrid();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            BeverageDetailsGrid.Visibility = Visibility.Collapsed;
        }
    }
}
