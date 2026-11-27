using System.Windows;
using System.Windows.Controls;
using PayDown.Views;

namespace PayDown
{
    /// <summary>
    /// Interaction logic for CtrlOrgIncome.xaml
    /// </summary>
    public partial class CtrlPDOrgIncome : UserControl
    {
        public CtrlIncomePaychecksTable _payCheckTable;
        public CtrlIncomeRetirementTable _retirementTable;
        public CtrlIncomeGovernemtalTable _governmentalTable;
        public CtrlIncomeCheckingTable _checkingTable;
        private bool _Init = true;
        public CtrlPDOrgIncome()
        {
            InitializeComponent();
            this.tbxRunName.Text = HardCode.HINT_NAME_DAN;
            this.tbxRunContent.Text = HardCode.HINT_INCOME;
            this._payCheckTable = new CtrlIncomePaychecksTable(this);
            this._retirementTable = new CtrlIncomeRetirementTable(this);
            this._governmentalTable = new CtrlIncomeGovernemtalTable(this);
            this._checkingTable = new CtrlIncomeCheckingTable(this);

            Grid.SetRow(this._payCheckTable, 0);
            Grid.SetRow(this._retirementTable, 1);
            Grid.SetRow(this._governmentalTable, 2);
            Grid.SetRow(this._checkingTable, 3);
            this.mainGrid.Children.Add(this._payCheckTable);
            this.mainGrid.Children.Add(this._retirementTable);
            this.mainGrid.Children.Add(this._governmentalTable);
            this.mainGrid.Children.Add(this._checkingTable);
            this.ctbxIncomeTotal.Value = (decimal) WinMain._settingMain._IncomeTotal;
            this._Init = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ctbxSubTotal_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (this._Init) return;
            WinMain._settingMain._IncomeTotal = (double)this.ctbxIncomeTotal.Value;
            WinMain._settingMain.Save();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
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
