using System.Windows;
using System.Windows.Threading;
namespace OpenFix;
public partial class App : System.Windows.Application
{
    public App()
    {
        Startup += (_, _) => new MainWindow().Show();
        DispatcherUnhandledException += (_, e) => { ShowStartupError(e.Exception); e.Handled = true; };
        AppDomain.CurrentDomain.UnhandledException += (_, e) => ShowStartupError(e.ExceptionObject as Exception ?? new Exception("Unknown startup error."));
    }
    private static void ShowStartupError(Exception exception) { try { System.Windows.MessageBox.Show($"OpenFix could not continue.\n\n{exception.Message}", "OpenFix startup error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error); } catch { } }
}
