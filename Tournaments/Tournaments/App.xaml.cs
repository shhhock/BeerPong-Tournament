using Tournaments.DbContexts;
using Tournaments.Navigation;
using Tournaments.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.HostBuilders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tournaments
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                        .AddViewModels()
                        .ConfigureServices((hostContext, services) =>
                        {
                            string connectionString = hostContext.Configuration.GetConnectionString("Default");
                            services.AddSingleton(new TournamentDbContextFactory(connectionString));

                            services.AddSingleton<NavigationStore<MainViewModel>>();

                            services.AddSingleton(s => new MainWindow()
                            {
                                DataContext = s.GetRequiredService<MainViewModel>()
                            });
                        })
                        .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            var prova = 1;
            TournamentDbContextFactory reservoomDbContextFactory = _host.Services.GetRequiredService<TournamentDbContextFactory>();
            using (TournamentDbContext dbContext = reservoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
            prova = prova +1;
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }

        #region onError
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMsg = "An application error occurred with the following information:";
            var eMessage = e.Exception.Message;
            var eStackTrace = new StackTrace(e.Exception, true);
            var eFrame = eStackTrace.GetFrame(0);
            var eClass = eFrame.GetFileName();
            var eMethod = eFrame.GetMethod();
            var eLine = eFrame.GetFileLineNumber();

            if (eClass != null)
            {
                var eC = eClass.Split("\\");
                errorMsg = $"{errorMsg}\r\n{eMessage}\r\n\r\nClass: {eC[eC.Length - 1]}\r\nMethod: {eMethod.Name}\r\nLine: {eLine}";
            }
            else errorMsg = $"{errorMsg}\r\n{eMessage}\r\n\r\nMethod: {eMethod.Name}";

            errorMsg = $"{errorMsg}\r\n\r\nYes = Shutdown | No = Restart | Cancel = Ignore";

            MessageBoxResult result = MessageBox.Show(errorMsg, "Unhandled Exception", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
                e.Handled = false;
            else if (result == MessageBoxResult.No)
            {
                e.Handled = true;
                Process.Start(Application.ResourceAssembly.Location);
                Current.Shutdown();
            }
            else if (result == MessageBoxResult.Cancel)
                e.Handled = true;

        }
        #endregion
    }
}
