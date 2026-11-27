using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PayDown.Assets;
using PayDown.Models;
using Syncfusion.Windows.Controls.Notification;

namespace PayDown.Views
{
    /// <summary>
    /// Interaction logic for WinLogin.xaml
    /// </summary>
    public partial class WinLogin : ChromelessWindow
    {
        private WinLogin _form;
        private LoginInfo _loginInfo = null;
        private SupabaseManager _supabaseManager = null;
        public WinLogin()
        {
            InitializeComponent();
            _form = this;
            _supabaseManager = new SupabaseManager();
        }

        private void ChromelessWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _supabaseManager.IntializeClient();
                _loginInfo = LoginInfo.Load<LoginInfo>();
                if (_loginInfo == null)
                {
                    _loginInfo = new LoginInfo();
                }
                this.tbxUserName.Text = _loginInfo._username;
                this.psbPassword.Password = _loginInfo._password;
                this.cbxRemember.IsChecked = _loginInfo._rememeber;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (_loginInfo.IsEmpty())
            {
                UtilMethods.ShowMessageBox("");
                return;
            }
            busyIndicator.IsBusy = true;
            SupaDataModel model = new SupaDataModel();
            model.cname = _loginInfo._username;
            model.cpassword = _loginInfo._password;

            bool isValid = await _supabaseManager.CheckIfUserInTableAsync(model);

            if (isValid)
            {
                if (_loginInfo._rememeber)
                {
                    _loginInfo.Save();
                }

                RunWorkSpace();
            }
            else
            {
                UtilMethods.ShowMessageBox("Incorrect username or password. Please check again.");
                busyIndicator.IsBusy = false;
            }
        }

        private void RunWorkSpace()
        {
            _form.Hide();
            WinMain form = new WinMain();
            SfSkinManager.SetTheme(form, new Theme() { ThemeName = "Material3Light" });
            form.ShowDialog();
            _form.Close();
        }

        private void psbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginInfo._password = this.psbPassword.Password;
        }

        private void tbxUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _loginInfo._username = this.tbxUserName.Text;
        }

        private void cbxRemember_Checked(object sender, RoutedEventArgs e)
        {
            _loginInfo._rememeber = cbxRemember.IsChecked.Value;
        }

        private void cbxRemember_Unchecked(object sender, RoutedEventArgs e)
        {
            _loginInfo._rememeber = cbxRemember.IsChecked.Value;
        }
    }
}
