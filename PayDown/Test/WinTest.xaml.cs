using System;
using System.Collections.Generic;
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

namespace PayDown
{
    /// <summary>
    /// Interaction logic for WinTest.xaml
    /// </summary>
    public partial class WinTest : Window
    {
        public WinTest()
        {
            InitializeComponent();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                var scrollViewer = sender as ScrollViewer;

                if (scrollViewer != null)
                {
                    // Pass the mouse wheel event to the parent ScrollViewer
                    if (e.Delta < 0)
                        scrollViewer.LineDown();
                    else
                        scrollViewer.LineUp();

                    e.Handled = true; // Mark event as handled
                }
            }
        }
    }
}
