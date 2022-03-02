using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Irony.Avalonia.ViewModels;

namespace Irony.Avalonia
{
    // AKA Grammar Explorer
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new GrammarExplorerViewModel();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}