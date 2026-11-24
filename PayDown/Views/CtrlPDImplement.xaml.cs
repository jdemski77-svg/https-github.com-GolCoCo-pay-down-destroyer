using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using PayDown.Models;

namespace PayDown.Views
{
    /// <summary>
    /// Interaction logic for CtrlPDImplement.xaml
    /// </summary>
    public partial class CtrlPDImplement : UserControl
    {
        private DataTable _mData;
        private List<ResultModel> _retmodels;
        private double _TotalInterestedPaid = 0.0;
        private int _MonthsToPay = 0;
        private int _Initial = 0;
        private int _SelectedIndex = 0;
        private int _monthsToPayCurrent = 0;
        private double _totalInterestedPaidCurrent = 0;
        public CtrlPDImplement()
        {
            InitializeComponent();
            _mData = new DataTable();

            DataColumn[] cols ={
                                  new DataColumn("MMonthsToPayoff",typeof(int)),
                                  new DataColumn("MMonthlyBalance",typeof(double)),
                                  new DataColumn("MAvailableIncome",typeof(double)),
                                  new DataColumn("MNewBalance",typeof(double)),
                                  new DataColumn("MChargeableExpenses",typeof(double)),
                                  new DataColumn("MInterest",typeof(double)),
                                  new DataColumn("MMonthlyEndBalance",typeof(double)),
                              };

            _mData.Columns.AddRange(cols);
            this.sfdatagrid.ItemsSource = _mData;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(_Initial < 2)
            {
                _Initial++;
                return;
            }
            UpdateIdentifyCreditCardsTable();
            //bool IsHeloc = (bool) chbHELOPLOCBLOC.IsChecked;
            //if (IsHeloc) {

            //    UpdateFormChecked();
            //}
            //else
            //{
            //    UpdateFormUnChecked();
            //}

        }

        private void UpdateIdentifyCreditCardsTable()
        {
            var items = new ObservableCollection<DebtsCreditCardModel>(
                                WinMain._DebtsCreditsTableData.GetDebtsCreditCards()
                            );

            lstIdentifyItems.ItemsSource = items;
            lstIdentifyItems.SelectedIndex = _SelectedIndex;
        }

        private void lstIdentifyItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstIdentifyItems.SelectedItem is DebtsCreditCardModel selectedItem)
                {
                    _TotalInterestedPaid = 0.0;
                    _MonthsToPay = 0;
                    _totalInterestedPaidCurrent = 0.0;
                    _monthsToPayCurrent = 0;
                    double selectedBalance = selectedItem._MBalance;
                    double selectedPayment = selectedItem._MPayment;
                    double selectedRate = selectedItem._MInterestRate;
                    string selectedType = selectedItem._MType;
                    _SelectedIndex = lstIdentifyItems.SelectedIndex;

                    double ai = 0;
                    double ce = 0;
                    double ib = 0;
                    if (selectedType == "CC")
                    {
                        ai = (double)(WinMain._settingMain._IncomeTotal - WinMain._settingMain._NonChargeableExpenses + selectedPayment);
                        ce = (double)WinMain._settingMain._ChargeableExpenses;
                        ib = (double)selectedBalance;
                    }
                    else
                    {
                        ai = (double)(WinMain._settingMain._IncomeTotal);
                        ce = (double)(WinMain._settingMain._TotalMonthlyExp - selectedPayment);
                        ib = (double)selectedBalance;
                    }
                    ctbxAvailableIncome.Value = (decimal)ai;
                    ctbxChargeableExpenses.Value = (decimal)ce;
                    ctbxInitialBalance.Value = (decimal)ib;
                    ptbxInterestRate.PercentValue = selectedRate;
                    UpdateSavingsResultTable(selectedBalance, selectedPayment, selectedRate/(double)100);
                    _retmodels = GetResultModels();

                    lblTradMonths.Content = $"{_monthsToPayCurrent} Months";
                    lblPayDownMonths.Content = $"{_MonthsToPay} Months";
                    lblSavingMonths.Content = $"{_monthsToPayCurrent - _MonthsToPay} Months";
                    ctbxSTraditional.Value = (decimal)_totalInterestedPaidCurrent;
                    ctbxSPayDown.Value = (decimal)_TotalInterestedPaid;
                    ctbxSSavings.Value = (decimal)(_totalInterestedPaidCurrent - _TotalInterestedPaid);
                }
            }
            catch (Exception ex) { 
                ex.ToString();
            }

        }

        public void UpdateSavingsResultTable(double balance, double payment, double rate)
        {
            ClearSavingsResultTable();
            if (balance <= 0 || payment <= 0 || rate <= 0)
            {
                return;
            }
            double totalinterest = 0;
            double interest = balance * rate;
            double principal = payment - interest;
            if (principal <= 0)
            {
                return;
            }
            totalinterest += interest;
            balance -= principal;
            int months = 0;

            if (payment >= balance)
            {
                _monthsToPayCurrent = 1;
                _totalInterestedPaidCurrent = interest;
                return;
            }

            int maxIterations = 1200;
            while (payment < balance && months < maxIterations)
            {
                interest = balance * rate;
                principal = payment - interest;

                if (principal <= 0)
                {
                    _monthsToPayCurrent = -1;
                    _totalInterestedPaidCurrent = -1;
                    return;
                }

                balance -= principal;
                totalinterest += interest;
                months++;
            }

            if (months >= maxIterations)
            {
                _monthsToPayCurrent = -1;
                _totalInterestedPaidCurrent = -1;
                return;
            }

            interest = balance * rate;
            totalinterest += interest;
            _monthsToPayCurrent = months + 2;
            _totalInterestedPaidCurrent = Math.Round(totalinterest,2);
        }

        public void ClearSavingsResultTable()
        {
            lblTradMonths.Content = "0 Months";
            lblPayDownMonths.Content = "0 Months";
            lblSavingMonths.Content = "0 Months";
            ctbxSTraditional.Value = 0;
            ctbxSPayDown.Value = 0;
            ctbxSSavings.Value = 0;
        }

        public List<ResultModel> GetResultModels()
        {
            _TotalInterestedPaid = 0.0;
            _MonthsToPay = 0;
            double sb = (double)ctbxInitialBalance.Value;
            double ai = (double)ctbxAvailableIncome.Value;
            double ir = (double)ptbxInterestRate.PercentValue / 100;
            double ce = (double)ctbxChargeableExpenses.Value;
            double totalInterested = 0;
            int row = 1;
            double interest = 0;
            List<ResultModel> retmodels = new List<ResultModel>();

            while (sb >= ai)
            {
                ResultModel resultModel = new ResultModel();
                double sb1 = sb - ai;
                interest = Math.Floor(((sb - ai) + sb) / 2 * ir / 12) + 1;

                if (sb < (sb1 + ce + interest))
                {
                    return retmodels;
                }
                resultModel._MonthlyStartingBalance = sb;
                resultModel._MonthsToPayoff = row;
                resultModel._AvailableIncome = ai;
                resultModel._NewBalance = sb1;
                resultModel._ChargeableExp = ce;
                resultModel._Interest = interest;
                if (row > 0)
                {
                    retmodels.Add(resultModel);
                    totalInterested += interest;
                }
                sb = sb1 + ce + interest;
                resultModel._MonthlyEndingBalance = sb;
                row++;
            }
            _TotalInterestedPaid = totalInterested;
            _MonthsToPay = retmodels.Count;
            return retmodels;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                busyIndicator.IsBusy = true;
                UpdateTable(_retmodels);
            });
        }

        public void UpdateTable(List<ResultModel> retmodels)
        {
            _retmodels = retmodels;
            _mData.Clear();
            if (_retmodels.Count == 0)
            {
                UtilMethods.ShowMessageBox("Your selections lead to increasing debt. Consult the instruction video for alternatives.");
            }
            _mData.BeginLoadData();
            foreach (ResultModel resultmodel in retmodels)
            {
                DataRow row = _mData.NewRow();
                row["MMonthsToPayoff"] = resultmodel._MonthsToPayoff;
                row["MMonthlyBalance"] = resultmodel._MonthlyStartingBalance;
                row["MAvailableIncome"] = resultmodel._AvailableIncome;
                row["MNewBalance"] = resultmodel._NewBalance;
                row["MChargeableExpenses"] = resultmodel._ChargeableExp;
                row["MInterest"] = resultmodel._Interest;
                row["MMonthlyEndBalance"] = resultmodel._MonthlyEndingBalance;
                _mData.Rows.Add(row);
            }
            _mData.EndLoadData();
            busyIndicator.IsBusy = false;
        }
    }
}
