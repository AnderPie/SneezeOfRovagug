using DnDGenerator.Models;
using DnDGenerator.ViewModels;
using System.Diagnostics;
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

namespace DnDGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        MainWindowViewModel vm = new();
        InitializeComponent();
        DataContext = vm;
        vm.GenerateRegion();
        vm.SelectedRegion = vm.Regions!.First();

    }

    private void Window_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
    {

    }

}