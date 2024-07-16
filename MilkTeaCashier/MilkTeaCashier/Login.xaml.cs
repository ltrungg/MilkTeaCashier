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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UserAccountService _service = new UserAccountService();
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // nhận vào username và password
            string userName = UsernameTextBox.Text;
            string password= Password.Password;
            Account account= _service.getUser(userName, password);
            if(account  == null)
            {
                MessageBox.Show("Invalid username or password", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
