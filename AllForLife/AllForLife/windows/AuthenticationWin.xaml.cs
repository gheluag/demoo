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
using AllForLife.Entity;

namespace AllForLife.windows
{
    /// <summary>
    /// Логика взаимодействия для AuthenticationWin.xaml
    /// </summary>
    public partial class AuthenticationWin : Window
    {
        DataBase _db;
        public AuthenticationWin()
        {
            InitializeComponent();
            _db = new();
        }

        private void authBTN_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text.Trim();
            string password = passwordTB.Password.Trim();

            var (isauth, role) = _db.AuthenticationUser(username, password);


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            if (!isauth)
            {
                MessageBox.Show("неверное имя пользователя или пароль", "ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else
            {
                var user = _db.GetUserByUN(username);
                var mainWin = new MainWindow();
                mainWin.Show();
            }
            
        }
    }
}
