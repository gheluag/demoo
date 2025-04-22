using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
using AllForLife.Entity;

namespace AllForLife.windows
{
    /// <summary>
    /// Логика взаимодействия для AuthenticationWin.xaml
    /// </summary>
    public partial class AuthenticationWin : Window
    {
        DataBase _db;
        public Users CurrentUser { get; set; }
        public AuthenticationWin()
        {
            InitializeComponent();
            _db = new();
        }

        private void authBTN_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text.Trim();
            string password = passwordTB.Password.Trim();

            CurrentUser = _db.AuthenticateUser(username, password);

            if(CurrentUser != null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
