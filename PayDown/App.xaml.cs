using Syncfusion.SfSkinManager;
using System.Windows;
using PayDown.Views;

namespace PayDown
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            LicenseKeyLocator.FindandRegisterLicenseKey();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var form = new WinLogin();
            SfSkinManager.SetTheme(form, new Theme() { ThemeName = "Material3Light" });
            form.ShowDialog();
            base.OnStartup(e);
        }
    }
}
