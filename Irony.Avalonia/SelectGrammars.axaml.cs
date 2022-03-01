using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
