using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Irony.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Irony.Avalonia
{
    public class App : Application
    {
        public static ServiceProvider AppServices { get; private set; } = null!;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            RegisterAppServices();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void RegisterAppServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(new CommonData { IsDesignMode = Design.IsDesignMode });
            services.AddSingleton<GrammarExplorerViewModel, GrammarExplorerViewModel>();
            services.AddSingleton<SelectGrammarsViewModel, SelectGrammarsViewModel>();
            services.AddSingleton<ShowExceptionViewModel, ShowExceptionViewModel>();
            AppServices = services.BuildServiceProvider();
        }
    }
}