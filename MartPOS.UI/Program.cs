using MartPOS.Core.Interfaces;
using MartPOS.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MartPOS.UI;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.ThreadException += (_, e) => Console.Error.WriteLine(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => Console.Error.WriteLine(e.ExceptionObject);

        var services = new ServiceCollection();
        services.AddSingleton<ISalesService, SalesService>();
        services.AddTransient<Forms.SalesForm>();
        var provider = services.BuildServiceProvider();

        ApplicationConfiguration.Initialize();
        Application.Run(provider.GetRequiredService<Forms.SalesForm>());
    }
}
