using PayDown.Views;
using Syncfusion.Windows.Shared;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace PayDown.SubViews
{
    /// <summary>
    /// Interaction logic for WinMessageBox.xaml
    /// </summary>
    public partial class WinSavingBox : ChromelessWindow
    {
        private WinSavingBox _form;
        public WinSavingBox()
        {
            InitializeComponent();
            _form = this;
        }
    }
}
