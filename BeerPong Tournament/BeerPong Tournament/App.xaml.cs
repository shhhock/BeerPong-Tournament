using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeerPong_Tournament
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainWindow mainWindow;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mainWindow = new();

            mainWindow.Show();
        }

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

            MessageBoxResult result = MessageBox.Show(errorMsg, eMessage, MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes) 
                e.Handled = false;
            else if (result == MessageBoxResult.No)
            {
                e.Handled = true;
                //Application.Restart();
                Current.Shutdown();
            }
            else if (result == MessageBoxResult.Cancel) 
                e.Handled = true;
            
        }
    }
}
