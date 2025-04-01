using AllForLife.Entity;
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
    public MainWindow()
    {
        InitializeComponent();
        _db = new DataBase();
        LoadProducts();
    }


    private void LoadProducts()
    {
        var prodlst = _db.GetProducts();
        foreach(var prod in prodlst)
        {
            productsLB.Items.Add(prod);
        }
    }


}