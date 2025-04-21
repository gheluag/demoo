using AllForLife.Entity;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AllForLife;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DataBase _db;
    private ObservableCollection<Products> _productsCollection { get; set; }

    static string imageFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
    string defaultImage = System.IO.Path.Combine(imageFolder, "picture.png");

    private Users _currentUser;
    public MainWindow()
    {
        InitializeComponent();
        _db = new DataBase();
        _productsCollection = new ObservableCollection<Products>();
        LoadProducts();
        LoadSaleComboBox();
        
    }


    private void LoadSaleComboBox()
    {
        List<string> saleList = new List<string>
        {
            "Все диапазоны",
            "3-5%",
            "6-8%"
        };

        SaleCB.ItemsSource = saleList;
        SaleCB.SelectedIndex = 0;
    }


    private void SaleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SaleCB.SelectedItem == null)
            return;

       

        if (SaleCB.SelectedIndex == 0)
        {
            LoadProducts(); 
            return;
        }

        int minSale = 0, maxSale = 100; 

        switch (SaleCB.SelectedIndex)
        {
            case 1:
                minSale = 3;
                maxSale = 5;
                break;
            case 2:
                minSale = 6;
                maxSale = 8;
                break;
        }

        var filteredProducts = _db.GetSaleProducts(minSale, maxSale);

        _productsCollection.Clear();
        foreach (var prod in filteredProducts)
        {
            if (string.IsNullOrWhiteSpace(prod.ImageURL))
            {
                prod.ImageURL = defaultImage;
            }
            else
            {
                string imagePath = System.IO.Path.Combine(imageFolder, prod.ImageURL);

                if (!System.IO.File.Exists(imagePath))
                {
                    prod.ImageURL = defaultImage;
                }
                else
                {
                    prod.ImageURL = imagePath;
                }
            }

            _productsCollection.Add(prod);
        }

        productsLB.ItemsSource = _productsCollection;
        countItems.Text = $"Показано товаров: {productsLB.Items.Count}";
    }


    private void LoadProducts()
    {
        List<Products> prodlst;
        string txt = searchTB.Text.Trim();

        if (string.IsNullOrEmpty(txt))
        {
            _productsCollection.Clear();
            prodlst = _db.GetProducts();

        }
        else
        {
            _productsCollection.Clear();
            prodlst = _db.SearchResults(txt);
        }

        foreach (var prod in prodlst)
        {
            if (string.IsNullOrWhiteSpace(prod.ImageURL))
            {
                prod.ImageURL = defaultImage;
            }
            else
            {
                string imagePath = System.IO.Path.Combine(imageFolder, prod.ImageURL);

                if (!System.IO.File.Exists(imagePath))
                {
                    prod.ImageURL = defaultImage;
                }
                else
                {
                    prod.ImageURL = imagePath;
                }
            }

            _productsCollection.Add(prod);
        }




        productsLB.ItemsSource = _productsCollection;
        countItems.Text = $"Показано товаров: {productsLB.Items.Count}";
    }

    private void searchTB_SelectionChanged(object sender, RoutedEventArgs e)
    {
        LoadProducts();
    }

    private void authBTN_Click(object sender, RoutedEventArgs e)
    {
        windows.AuthenticationWin authenticationWin = new();
        authenticationWin.ShowDialog();
    }
}