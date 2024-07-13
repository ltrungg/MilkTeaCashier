using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly AccountService _account;
        public LoginWindow()
        {
            _account = new AccountService();
            InitializeComponent();
        }



        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to quit?", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }

        }

        private void ClearTextBox()
        {
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = _account.GetAll().Where(a => a.UserName == UserNameTextBox.Text && a.PassWord == PasswordTextBox.Password).FirstOrDefault();
            if (account == null)
            {
                MessageBoxResult result = MessageBox.Show("Invalid username or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AccountWindow accountWindow = new AccountWindow(account);
            accountWindow.Show();
            this.Close();
        }
    }
}
