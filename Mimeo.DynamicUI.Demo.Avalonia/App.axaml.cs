using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Mimeo.DynamicUI.Avalonia.Services;
using System;

namespace Mimeo.DynamicUI.Demo.Avalonia
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);


            var serviceProvider = BuildServiceProvider();
            this.Resources[typeof(IServiceProvider)] = serviceProvider;
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel((IServiceProvider)this.Resources[typeof(IServiceProvider)]!)
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddLocalization();
            services.AddSingleton<IStringLocalizer, StringLocalizer<Language>>();
            services.AddSingleton<IDateTimeConverter, DateTimeConverter>();

            return services.BuildServiceProvider();
        }
    }
}