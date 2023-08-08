using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;
using BookStore.Services.BookService;
using BookStore.Data;
using BookStore.Models;
using System;
using BookStore.Services.SettingsService;

namespace BookStoreWinApp
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Application.Run(new FormSearch());

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Application.Run(ServiceProvider.GetRequiredService<FormMain>());
            // Application.Run(ServiceProvider.GetRequiredService<FormSearch>());
            // Application.Run(ServiceProvider.GetRequiredService<FormSettings>());
            // Application.Run(new FormSettings());
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddTransient<IBookService, BookService>();
                services.AddTransient<ISettingsService, SettingsService>();
                services.AddDbContext<SqliteContext>();
                services.AddSingleton<FormMain>();
                services.AddSingleton<FormSearch>();
                services.AddSingleton<FormSettings>();
                services.AddTransient<FormEntry>();
            });
        } 

    }
}