using AllForLife.Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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

namespace AllForLife.windows
{
    /// <summary>
    /// Логика взаимодействия для EditProduct_Window.xaml
    /// </summary>
    public partial class EditProduct_Window : Window
    {
        DataBase _db;
        private Products _prod;
        public EditProduct_Window(Products product)
        {
            InitializeComponent();
            _prod = product;
            _db = new();
            LoadBrandCB();
            LoadCatCB();
            LoadSupCB();
            DataContext = _prod;
            
            
        }


        private void LoadSupCB()
        {
            var supList = _db.GetSuppliers();
            supCB.ItemsSource = supList;

            supCB.DisplayMemberPath = "Name";  
            supCB.SelectedValuePath = "Id";    
            supCB.SelectedValue = _prod.SupplierId;
        }

        private void LoadCatCB()
        {
            var catList = _db.GetCategories();
            catCB.ItemsSource = catList;

            catCB.DisplayMemberPath = "Name";
            catCB.SelectedValuePath = "Id";
            catCB.SelectedValue = _prod.CategoryId;
        }

        private void LoadBrandCB()
        {
            var brandList = _db.GetBrands();
            brandCB.ItemsSource = brandList;

            brandCB.DisplayMemberPath = "Name";
            brandCB.SelectedValuePath = "Id";
            brandCB.SelectedValue = _prod.BrandId;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            return;
        }


        private void editBtn_Click(object sender, RoutedEventArgs e)
        {

            if(!int.TryParse(countTb.Text.Trim(), out int count))
            {
                countTb.Background = Brushes.DarkRed;
                return;
            }

            if (!int.TryParse(maxSaleTb.Text.Trim(), out int maxSale))
            {
                maxSaleTb.Background = Brushes.DarkRed;
                return;
            }

            if (!int.TryParse(currSaleTb.Text.Trim(), out int curSale))
            {
                currSaleTb.Background = Brushes.DarkRed;
                return;
            }
            string inputPrice = priceTB.Text.Trim().Replace('.', ',');

            if (!decimal.TryParse(inputPrice, out decimal price))
            {
                priceTB.Background = Brushes.DarkRed;
                return;
            }

            if (curSale > maxSale)
            {
                currSaleTb.Background = Brushes.DarkRed;
                currSaleTb.ToolTip = "скидка не должна превышать максимальную";
                return;
            }


            currSaleTb.ToolTip = Visibility.Collapsed;
            _prod.Name = nameTB.Text.Trim();
            _prod.Description = descTB.Text.Trim();
            _prod.Count = count;
            _prod.MaxSale = maxSale;
            _prod.CurrentSale = curSale;
            _prod.Price = price;

            _prod.CategoryId = catCB.SelectedValue is int catId ? catId : 0;
            _prod.SupplierId = supCB.SelectedValue is int supId ? supId : 0;
            _prod.BrandId = brandCB.SelectedValue is int brandId ? brandId : 0;


            bool upd = _db.UpdateProduct(_prod);

            if (upd)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("произошла ошибка");
                Close();
            }


        }

        private void updIMG_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _prod.ImageURL = openFileDialog.FileName;
            }

            prodIMG.Source = new BitmapImage(new Uri(_prod.ImageURL));
        }
    }
}
