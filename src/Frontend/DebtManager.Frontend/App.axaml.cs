using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DebtManager.Frontend.Api;
using DebtManager.Frontend.ViewModels;
using DebtManager.Frontend.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace DebtManager.Frontend;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var builder = Host.CreateApplicationBuilder();

        // 1. Configurar Refit Client apuntando al Backend
        builder
            .Services.AddRefitClient<IDebtManagerApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7113"));

        // 2. Registrar dependencias MVVM
        builder.Services.AddTransient<MainViewModel>();

        AppHost = builder.Build();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                // Inyectar el ViewModel usando el contenedor DI
                DataContext = AppHost.Services.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
