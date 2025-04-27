using AllForLife.Entity;
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
            supCB.SelectedItem = _prod.SupplierId;
        }

        private void LoadCatCB()
        {
            var catList = _db.GetCategories();
            catCB.ItemsSource = catList;

            catCB.DisplayMemberPath = "Name";
            catCB.SelectedValuePath = "Id";
            catCB.SelectedItem = _prod.CategoryId;
        }

        private void LoadBrandCB()
        {
            var brandList = _db.GetBrands();
            brandCB.ItemsSource = brandList;

            brandCB.DisplayMemberPath = "Name";
            brandCB.SelectedValuePath = "Id";
            brandCB.SelectedItem = _prod.BrandId;
        }






    }
}
