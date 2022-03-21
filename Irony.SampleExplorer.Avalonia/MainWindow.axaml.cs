using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Irony.SampleExplorer.Avalonia.ViewModels;

namespace Irony.SampleExplorer.Avalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new SampleExplorerViewModel(new CommonUiSettings());

            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}