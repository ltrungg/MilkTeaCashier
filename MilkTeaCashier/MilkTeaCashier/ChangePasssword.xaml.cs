using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ChangePasssword.xaml
    /// </summary>
    public partial class ChangePasssword : Window
    {
        private UserAccountService _userAccountService= new();
        public Account AccountChangePassword { get; set; }
        public ChangePasssword()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(PasswordTextBox.Text) || string.IsNullOrEmpty(NewPasswordTextBox.Text) || string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
            {
                MessageBox.Show("Please fill in all flieds", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(NewPasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                MessageBox.Show("Confirm password do not match, please try again", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(PasswordTextBox.Text != AccountChangePassword.PassWord)
            {
                MessageBox.Show("Incorrect password","", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AccountChangePassword.PassWord = NewPasswordTextBox.Text;
            _userAccountService.UpdateUser(AccountChangePassword);
            MessageBox.Show("Sucessfully updated", "", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
