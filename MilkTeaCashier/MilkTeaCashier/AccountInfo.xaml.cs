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
    /// Interaction logic for AccountInfo.xaml
    /// </summary>
    public partial class AccountInfo : Window
    {

        UserAccountService _accountService= new();
        public AccountInfo()
        {
            InitializeComponent();
        }

        public Account SelectedAccount { get; set; } = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           LoadData();  
        }
        private void LoadData()
        {
            // remember to change this code
            Account accountInfo = _accountService.getUser("K18", "11");
            //accountInfo.UserName = "K18";
            //accountInfo.DisplayedName = "FPT";
            //accountInfo.PassWord = "1";
            UsernameTextBox.Text = accountInfo.UserName.ToString();
            DisplayNameTextBox.Text = accountInfo.DisplayedName.ToString();
            PasswordTextBox.Text = accountInfo.PassWord.ToString();
            UsernameTextBox.IsEnabled = false;
            PasswordTextBox.IsEnabled = false;

        }
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccount= _accountService.getUser("K18", "11");
            ChangePasssword changePasssword = new ChangePasssword();
            if (SelectedAccount == null)
            {
                return;
            }
            changePasssword.AccountChangePassword = SelectedAccount;
            changePasssword.Show();
            LoadData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = new();
            account.UserName= UsernameTextBox.Text;
            account.DisplayedName= DisplayNameTextBox.Text;
            account.PassWord= PasswordTextBox.Text;
            _accountService.UpdateUser(account);
             MessageBox.Show("Sucessfully updated", "", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
