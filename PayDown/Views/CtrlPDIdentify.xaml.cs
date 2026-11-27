using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PayDown.Views
{
    /// <summary>
    /// Interaction logic for CtrlIdentify.xaml
    /// </summary>
    public partial class CtrlPDIdentify : UserControl
    {
        public CtrlIdentifyLoansTable _loanTable;
        public CtrlIdentifyCreditsTable _creditTable;
        public CtrlPDIdentify()
        {
            InitializeComponent();
            this._loanTable = new CtrlIdentifyLoansTable();
            this._creditTable = new CtrlIdentifyCreditsTable();

            Grid.SetRow(this._loanTable, 1);
            Grid.SetRow(this._creditTable, 0);

            this.mainGrid.Children.Add(this._loanTable);
            this.mainGrid.Children.Add(this._creditTable);
            UpdateAllTabls();
        }

        public void UpdateAllTabls()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                _creditTable.UpdateTable();
                _loanTable.UpdateTable();
            });
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                var scrollViewer = sender as ScrollViewer;

                if (scrollViewer != null)
                {
                    double scrollAmount = HardCode.SCROLL_SPEED;

                    if (e.Delta < 0)
                        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + scrollAmount);
                    else
                        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - scrollAmount);

                    e.Handled = true;
                }
            }
        }
    }
}
