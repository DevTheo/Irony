using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Irony.UI.ViewModels;

namespace Irony.Avalonia
{
    public partial class ShowException : Window
    {
        public ShowException()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new ShowExceptionViewModel(Design.IsDesignMode);

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
