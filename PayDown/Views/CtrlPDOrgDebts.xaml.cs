using System.Windows;
using System.Windows.Controls;
using PayDown.Views;

namespace PayDown
{
    /// <summary>
    /// Interaction logic for CtrlOrgIncome.xaml
    /// </summary>
    public partial class CtrlPDOrgDebts : UserControl
    {
        public CtrlDebtsMonthlyTable _monthBillsTable;
        public CtrlDebtsLivingExpTable _LivingExpenseTable;
        public CtrlDebtsLoansTable _loansTable;
        public CtrlDebtsCreditsTable _creditTable;
        private bool _Init = true;
        public CtrlPDOrgDebts()
        {
            InitializeComponent();
            this._monthBillsTable = new CtrlDebtsMonthlyTable(this);
            this._LivingExpenseTable = new CtrlDebtsLivingExpTable(this);
            this._loansTable = new CtrlDebtsLoansTable(this);
            this._creditTable = new CtrlDebtsCreditsTable(this);
            Grid.SetRow(this._monthBillsTable, 0);
            Grid.SetRow(this._LivingExpenseTable, 1);
            Grid.SetRow(this._loansTable, 2);
            Grid.SetRow(this._creditTable, 3);
            this.mainGrid.Children.Add(this._monthBillsTable);
            this.mainGrid.Children.Add(this._LivingExpenseTable);
            this.mainGrid.Children.Add(this._loansTable);
            this.mainGrid.Children.Add(this._creditTable);

            this.ctbxMortgage.Value = (decimal)WinMain._settingMain._MortgageRentPayment;
            this.ctbxTotalMonthlyExpense.Value = (decimal)WinMain._settingMain._TotalMonthlyExp;
            this.ctbxTotalDebt.Value = (decimal)WinMain._settingMain._TotalDebts;
            this.ctbxChargeExpsenses.Value = (decimal)WinMain._settingMain._ChargeableExpenses;
            this.ctbxNonChargeableExpense.Value = (decimal)WinMain._settingMain._NonChargeableExpenses;

            this.ntbxCreditScore1.Text = WinMain._settingMain._CreditScore1;
            this.ntbxCreditScore2.Text = WinMain._settingMain._CreditScore2;

            this._Init = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ctbxMortgage_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.ctbxTotalMonthlyExpense == null) return;
            if (this.ctbxMortgage == null) return;
            WinMain._settingMain._MortgageRentPayment = (double)this.ctbxMortgage.Value;
            this.ctbxTotalMonthlyExpense.Value = (decimal)WinMain._DebtsLoansTableData.GetSubTotalPayment()
                + (decimal)WinMain._DebtsCreditsTableData.GetSubTotalPayment()
                + (decimal)WinMain._settingMain._MortgageRentPayment
                + (decimal)(WinMain._DebtsMonthlyTableData.GetChargeableValue() + WinMain._DebtsMonthlyTableData.GetNonChargeableValue())
                + (decimal)(WinMain._DebtsLivingExpTableData.GetChargeableValue() + WinMain._DebtsLivingExpTableData.GetNonChargeableValue());


            WinMain._settingMain._TotalMonthlyExp = (double)this.ctbxTotalMonthlyExpense.Value;
            WinMain._settingMain.Save();
        }

        private void ctbxTotalDebt_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (this._Init) return;
            WinMain._settingMain._TotalDebts = (double)this.ctbxTotalDebt.Value;
            WinMain._settingMain.Save();
        }

        private void ctbxTotalMonthlyExpense_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (this._Init) return;
            WinMain._settingMain._TotalMonthlyExp = (double)this.ctbxTotalMonthlyExpense.Value;
            WinMain._settingMain.Save();
        }

        private void ctbxNonChargeableExpense_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (this._Init) return;
            WinMain._settingMain._NonChargeableExpenses = (double)this.ctbxNonChargeableExpense.Value;
            WinMain._settingMain.Save();
        }

        private void ctbxChargeExpsenses_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (this._Init) return;
            WinMain._settingMain._ChargeableExpenses = (double)this.ctbxChargeExpsenses.Value;
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

        private void ntbxCreditScore1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this._Init) return;
            WinMain._settingMain._CreditScore1 = this.ntbxCreditScore1.Text;
            WinMain._settingMain.Save();
        }

        private void ntbxCreditScore2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this._Init) return;
            WinMain._settingMain._CreditScore2 = this.ntbxCreditScore2.Text;
            WinMain._settingMain.Save();
        }
    }
}
