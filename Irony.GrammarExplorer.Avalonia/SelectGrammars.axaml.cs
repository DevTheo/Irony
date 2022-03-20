using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Irony.UI.ViewModels;

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
            DataContext = Design.IsDesignMode ?
                new SelectGrammarsViewModel(new CommonData { IsDesignMode = Design.IsDesignMode }) :
                App.AppServices!.GetService(typeof(SelectGrammarsViewModel));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
