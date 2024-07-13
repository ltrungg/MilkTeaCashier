using Microsoft.Identity.Client.NativeInterop;
using Repositories.Entities;
using Services;
using System.Windows;
using Account = Repositories.Entities.Account;

namespace MilkTeaCashier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        private readonly Repositories.Entities.Account _account;
        private readonly AccountService _accountService;
        public AccountWindow(Repositories.Entities.Account account)
        {
            _account = account;
            _accountService = new AccountService();
            InitializeComponent();
        }

        private void ClearTextBox()
        {
            UserNameTextBox.Text = string.Empty;
            DisplayedNameTextBox.Text = string.Empty;
            AccountTypeTextBox.Text = string.Empty;
            PasswordAccountTextBox.Text = string.Empty;
        }

        private bool IsAdmin() => _account.Type == 1;
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin())
            {
                MessageBox.Show("Okay good!");
            }
            else
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void AddButtonAccount_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputFields())
            {
                var existingAccount = _accountService.GetByName(UserNameTextBox.Text);
                if (existingAccount != null)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Account account = new Account()
                {
                    UserName = UserNameTextBox.Text,
                    DisplayedName = UserNameTextBox.Text,
                    Type = int.Parse(AccountTypeTextBox.Text),
                    PassWord = PasswordAccountTextBox.Text,
                };
                _accountService.Add(account);
                MessageBox.Show("Account added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadList();
                ClearTextBox();
            }

        }

        private void LoadList()
        {
            AccountListDataGrid.ItemsSource = _accountService.GetAll();
        }

        private void AccountListDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadList();
        }

        private void AccountListDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AccountListDataGrid.SelectedItem != null)
            {
                Repositories.Entities.Account account = (Repositories.Entities.Account)AccountListDataGrid.SelectedItem;
                UserNameTextBox.Text = account.UserName;
                DisplayedNameTextBox.Text = account.DisplayedName;
                AccountTypeTextBox.Text = account.Type.ToString();
                PasswordAccountTextBox.Text = account.PassWord;
                UserNameTextBox.IsReadOnly = true;
            }
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountListDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose account to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this account?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;
            string acountUsername = UserNameTextBox.Text;
            Account? account = _accountService.GetByName(acountUsername);
            _accountService.Delete(account);
            MessageBox.Show("Account deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadList();
            ClearTextBox();
        }

        private bool ValidateInputFields()
        {
            if (string.IsNullOrWhiteSpace(UserNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(DisplayedNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(AccountTypeTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordAccountTextBox.Text))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(AccountTypeTextBox.Text, out int accountType) || (accountType != 0 && accountType != 1))
            {
                MessageBox.Show("Type must be either 0 or 1.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void UpdateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountListDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose account to update!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ValidateInputFields())
            {
                string accountUserName = UserNameTextBox.Text;
                Account? account = _accountService.GetByName(accountUserName);
                if (account == null)
                {
                    MessageBox.Show("Please choose account to update!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                UserNameTextBox.IsReadOnly = true;
                account.DisplayedName = DisplayedNameTextBox.Text;
                account.Type = int.Parse(AccountTypeTextBox.Text);
                account.PassWord = PasswordAccountTextBox.Text;
                _accountService.Update(account);
                MessageBox.Show("Account updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadList();
                ClearTextBox();
            }
        }

        private void SearchAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = SearchAccountTextBox.Text.ToLower();
            bool isNumeric = int.TryParse(SearchAccountTextBox.Text, out int searchType);
            var searchResult = _accountService.Search(a =>
                a.UserName.ToLower().Contains(searchText) ||
                a.DisplayedName.ToLower().Contains(searchText) ||
                (isNumeric && a.Type == searchType)
            ).ToList();
            AccountListDataGrid.ItemsSource = searchResult;
        }

    }
}