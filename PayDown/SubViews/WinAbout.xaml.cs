using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayDown.SubViews
{
    /// <summary>
    /// Interaction logic for WinNewWorkSpace.xaml
    /// </summary>
    public partial class WinAbout : ChromelessWindow
    {
        private WinAbout _form;
        public WinAbout()
        {
            InitializeComponent();
            _form = this;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            _form.Close();
        }
    }
}
