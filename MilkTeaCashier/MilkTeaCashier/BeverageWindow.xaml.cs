using Repositories.Entities;
using Services;
using System.Windows;


namespace MilkTeaCashier
{

    public partial class BeverageWindow : Window
    {
        private BeverageService _beverageService = new();
        private BeverageCategoryService _beverageCategoryService = new();
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
            BeverageDataGrid.SelectedValue = null;
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
            if (!InvalidInput()) return;

            Beverage b = new();
            Beverage selectedBeverage = BeverageDataGrid.SelectedValue as Beverage;
            b.Name = BeverageNameTextBox.Text;
            b.Price = double.Parse(PriceTextBox.Text);
            b.IdCategory = int.Parse(BeverageCategoryComboBox.SelectedValue.ToString());
            b.Status = "active";

            if (selectedBeverage != null)
            {
                b.Id = int.Parse(BeverageIDTextBox.Text);
                _beverageService.UpdateBeverage(b);
            }
            else
            {
                _beverageService.AddBeverage(b);
            }
            BeverageDetailsGrid.Visibility = Visibility.Collapsed;

            LoadDataGrid();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            BeverageDetailsGrid.Visibility = Visibility.Collapsed;
            ClearData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string beverageName = SearchTextBox.Text;
            List<Beverage> listBeverage = _beverageService.SearchBeverage(beverageName);

            if (listBeverage.Count == 0)
            {
                MessageBox.Show("No results found for your search.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            BeverageDataGrid.ItemsSource = null;
            BeverageDataGrid.ItemsSource = listBeverage;
        }

        private bool InvalidInput()
        {
            if (string.IsNullOrWhiteSpace(BeverageNameTextBox.Text) || string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                MessageBox.Show("All fields are required!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }


            if (!double.TryParse(PriceTextBox.Text, out double price))
            {
                MessageBox.Show("Please input Price as number!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (price <= 0 || price > 1_000_000)
            {
                MessageBox.Show("Price is greater than 0 and less than 1 000 000", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }
    }
}
