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
            _productsCollection.Add(prod);
        }

        productsLB.ItemsSource = _productsCollection;
    }


    private void LoadProducts()
    {
        var prodlst = _db.GetProducts();

        string imageFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
        string defaultImage = System.IO.Path.Combine(imageFolder, "picture.png");

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
    }


}