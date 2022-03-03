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
            DataContext = Design.IsDesignMode ?
                new ShowExceptionViewModel(new CommonData { IsDesignMode = Design.IsDesignMode }) :
                App.AppServices!.GetService(typeof(ShowExceptionViewModel));

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
