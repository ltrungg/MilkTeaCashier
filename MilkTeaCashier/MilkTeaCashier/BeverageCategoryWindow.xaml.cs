using Repositories.Entities;
using Services;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace MilkTeaCashier
{
    public partial class BeverageCategoryWindow : Window
    {
        private BeverageCategoryServices _service = new();
        public BeverageCategoryWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            BevarageCategoryDataGrid.ItemsSource = null;

            BevarageCategoryDataGrid.ItemsSource = _service.GetCategories();
        }
        private void ClearData()
        {
            BeverageIdTextBox.Text = string.Empty;
            CategoryNameTextBox.Text = string.Empty;
            BevarageCategoryDataGrid.SelectedValue = null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            CategoryDetailsGrid.Visibility = Visibility.Visible;
            BeverageIdTextBox.IsEnabled = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            BeverageCategory selectedCategory = BevarageCategoryDataGrid.SelectedValue as BeverageCategory;
            if (selectedCategory == null)
            {
                MessageBox.Show("Please select a category to delete!", "Select category", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult answer = MessageBox.Show("Do you want to delete this category?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
            {
                return;
            }

            _service.DeleteCategory(selectedCategory);
            LoadDataGrid();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            BeverageCategory selectedCategory = BevarageCategoryDataGrid.SelectedValue as BeverageCategory;
            BeverageIdTextBox.IsEnabled = false;

            if (selectedCategory == null)
            {
                MessageBox.Show("Please select a beverage to update!", "Select one beverage", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CategoryDetailsGrid.Visibility = Visibility.Visible;

            if (selectedCategory != null)
            {
                BeverageIdTextBox.Text = selectedCategory.Id.ToString();
                CategoryNameTextBox.Text = selectedCategory.Name.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InvalidInput()) return;

            BeverageCategory c = new();
            BeverageCategory selectedBeverage = BevarageCategoryDataGrid.SelectedValue as BeverageCategory;
            c.Name = CategoryNameTextBox.Text;
            c.Status = "active";

            if (selectedBeverage != null)
            {
                c.Id = int.Parse(BeverageIdTextBox.Text);
                _service.UpdateCategory(c);
            }
            else
            {
                _service.AddCategory(c);
            }
            CategoryDetailsGrid.Visibility = Visibility.Collapsed;

            LoadDataGrid();
        }
        private bool InvalidInput()
        {
            if (string.IsNullOrWhiteSpace(CategoryNameTextBox.Text))
            {
                MessageBox.Show("Please fill in the category name!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryDetailsGrid.Visibility = Visibility.Collapsed;
            ClearData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = SearchTextBox.Text;
            List<BeverageCategory> categoryList = _service.SearchByCategoryName(categoryName);
            if (categoryList.Count == 0)
            {
                MessageBox.Show("No results found! ", "Not Found", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            BevarageCategoryDataGrid.ItemsSource = null;
            BevarageCategoryDataGrid.ItemsSource = categoryList;
        }
    }
}