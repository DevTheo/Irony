using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}