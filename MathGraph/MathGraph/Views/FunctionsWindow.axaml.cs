using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MathGraph.ViewModels;

namespace MathGraph.Views;

public partial class FunctionsWindow : Window
{
    public FunctionsWindow()
    {
        InitializeComponent();
        DataContext = new FunctionsViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}