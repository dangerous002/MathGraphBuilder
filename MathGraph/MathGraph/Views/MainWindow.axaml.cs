using Avalonia.Controls;
using MathGraph.ViewModels;

namespace MathGraph.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        Avalonia.Markup.Xaml.AvaloniaXamlLoader.Load(this);
    }
}