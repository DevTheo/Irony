using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Irony.Avalonia.ViewModels;

namespace Irony.Avalonia
{
    public partial class SelectGrammars : Window
    {
        public SelectGrammars()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new SelectGrammarsViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
