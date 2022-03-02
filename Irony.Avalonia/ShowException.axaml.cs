using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Irony.Avalonia.ViewModels;

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
            DataContext = new ShowExceptionViewModel();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
