using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using PayDown.Models;
using PayDown.Views;

namespace PayDown
{
    /// <summary>
    /// Interaction logic for CtrlIncomTable.xaml
    /// </summary>
    public partial class CtrlDebtsLoansTable : UserControl
    {
        private DataTable mData;
        private CtrlPDOrgDebts _ctrlOrgDebts;
        public CtrlDebtsLoansTable(CtrlPDOrgDebts ctrlOrgDebts)
        {
            _ctrlOrgDebts = ctrlOrgDebts;
            InitializeComponent();
            mData = new DataTable();
            DataColumn[] cols ={
                                  new DataColumn("MNo1",typeof(int)),
                                  new DataColumn("MSource1",typeof(string)),
                                  new DataColumn("MPayment1",typeof(double)),
                                  new DataColumn("MBalance1",typeof(double)),
                                  new DataColumn("MInterestRate1",typeof(double)),
                                  new DataColumn("MOutstandInterest1",typeof(double)),

                                  new DataColumn("MNo2",typeof(int)),
                                  new DataColumn("MSource2",typeof(string)),
                                  new DataColumn("MPayment2",typeof(double)),
                                  new DataColumn("MBalance2",typeof(double)),
                                  new DataColumn("MInterestRate2",typeof(double)),
                                  new DataColumn("MOutstandInterest2",typeof(double)),

                              };
            mData.Columns.AddRange(cols);

            for (int i = 1; i <= 5; i++)
            {
                DataRow drow = mData.NewRow();
                if (i == 1)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsLoansTableData._Source1;
                    drow["MPayment1"] = WinMain._DebtsLoansTableData._Payment1;
                    drow["MBalance1"] = WinMain._DebtsLoansTableData._Balance1;
                    drow["MInterestRate1"] = WinMain._DebtsLoansTableData._InterestRate1;
                    drow["MOutstandInterest1"] = WinMain._DebtsLoansTableData._OutstandInterest1;

                    drow["MNo2"] = i + 5;
                    drow["MSource2"] = WinMain._DebtsLoansTableData._Source6;
                    drow["MPayment2"] = WinMain._DebtsLoansTableData._Payment6;
                    drow["MBalance2"] = WinMain._DebtsLoansTableData._Balance6;
                    drow["MInterestRate2"] = WinMain._DebtsLoansTableData._InterestRate6;
                    drow["MOutstandInterest2"] = WinMain._DebtsLoansTableData._OutstandInterest6;
                }
                else if (i == 2)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsLoansTableData._Source2;
                    drow["MPayment1"] = WinMain._DebtsLoansTableData._Payment2;
                    drow["MBalance1"] = WinMain._DebtsLoansTableData._Balance2;
                    drow["MInterestRate1"] = WinMain._DebtsLoansTableData._InterestRate2;
                    drow["MOutstandInterest1"] = WinMain._DebtsLoansTableData._OutstandInterest2;

                    drow["MNo2"] = i + 5;
                    drow["MSource2"] = WinMain._DebtsLoansTableData._Source7;
                    drow["MPayment2"] = WinMain._DebtsLoansTableData._Payment7;
                    drow["MBalance2"] = WinMain._DebtsLoansTableData._Balance7;
                    drow["MInterestRate2"] = WinMain._DebtsLoansTableData._InterestRate7;
                    drow["MOutstandInterest2"] = WinMain._DebtsLoansTableData._OutstandInterest7;
                }
                else if (i == 3)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsLoansTableData._Source3;
                    drow["MPayment1"] = WinMain._DebtsLoansTableData._Payment3;
                    drow["MBalance1"] = WinMain._DebtsLoansTableData._Balance3;
                    drow["MInterestRate1"] = WinMain._DebtsLoansTableData._InterestRate3;
                    drow["MOutstandInterest1"] = WinMain._DebtsLoansTableData._OutstandInterest3;

                    drow["MNo2"] = i + 5;
                    drow["MSource2"] = WinMain._DebtsLoansTableData._Source8;
                    drow["MPayment2"] = WinMain._DebtsLoansTableData._Payment8;
                    drow["MBalance2"] = WinMain._DebtsLoansTableData._Balance8;
                    drow["MInterestRate2"] = WinMain._DebtsLoansTableData._InterestRate8;
                    drow["MOutstandInterest2"] = WinMain._DebtsLoansTableData._OutstandInterest8;
                }
                else if (i == 4)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsLoansTableData._Source4;
                    drow["MPayment1"] = WinMain._DebtsLoansTableData._Payment4;
                    drow["MBalance1"] = WinMain._DebtsLoansTableData._Balance4;
                    drow["MInterestRate1"] = WinMain._DebtsLoansTableData._InterestRate4;
                    drow["MOutstandInterest1"] = WinMain._DebtsLoansTableData._OutstandInterest4;

                    drow["MNo2"] = i + 5;
                    drow["MSource2"] = WinMain._DebtsLoansTableData._Source9;
                    drow["MPayment2"] = WinMain._DebtsLoansTableData._Payment9;
                    drow["MBalance2"] = WinMain._DebtsLoansTableData._Balance9;
                    drow["MInterestRate2"] = WinMain._DebtsLoansTableData._InterestRate9;
                    drow["MOutstandInterest2"] = WinMain._DebtsLoansTableData._OutstandInterest9;
                }
                else
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsLoansTableData._Source5;
                    drow["MPayment1"] = WinMain._DebtsLoansTableData._Payment5;
                    drow["MBalance1"] = WinMain._DebtsLoansTableData._Balance5;
                    drow["MInterestRate1"] = WinMain._DebtsLoansTableData._InterestRate5;
                    drow["MOutstandInterest1"] = WinMain._DebtsLoansTableData._OutstandInterest5;

                    drow["MNo2"] = i + 5;
                    drow["MSource2"] = WinMain._DebtsLoansTableData._Source10;
                    drow["MPayment2"] = WinMain._DebtsLoansTableData._Payment10;
                    drow["MBalance2"] = WinMain._DebtsLoansTableData._Balance10;
                    drow["MInterestRate2"] = WinMain._DebtsLoansTableData._InterestRate10;
                    drow["MOutstandInterest2"] = WinMain._DebtsLoansTableData._OutstandInterest10;
                }

                mData.Rows.Add(drow);
            }
            UpdateSubTotal();
            this.sfdatagrid.ItemsSource = mData;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbxRunName.Text = HardCode.HINT_NAME_JEN;
            this.tbxRunContent.Text = HardCode.HINT_DEBTS_LOANS;
        }

        private void UpdateSubTotal()
        {
            double subtotalp = WinMain._DebtsLoansTableData.GetSubTotalPayment();
            double subtotalb = WinMain._DebtsLoansTableData.GetTotalBalance();

            this.ctbxSubTotalPayment.Value = (decimal)subtotalp;
            this.ctbxSubTotalBalance.Value = (decimal)subtotalb;

            this._ctrlOrgDebts.ctbxTotalDebt.Value = (decimal)((double)WinMain._DebtsCreditsTableData.GetTotalBalance() + subtotalb);
            this._ctrlOrgDebts.ctbxTotalMonthlyExpense.Value = (decimal)((double)WinMain._DebtsCreditsTableData.GetSubTotalPayment() + subtotalp
                + (double)(WinMain._DebtsMonthlyTableData.GetChargeableValue() + WinMain._DebtsMonthlyTableData.GetNonChargeableValue()) + (double)(WinMain._DebtsLivingExpTableData.GetChargeableValue() + WinMain._DebtsLivingExpTableData.GetNonChargeableValue()) + (double)WinMain._settingMain._MortgageRentPayment);

            this._ctrlOrgDebts.ctbxNonChargeableExpense.Value = (decimal)WinMain._DebtsLivingExpTableData.GetNonChargeableValue() +
            (decimal)subtotalp +
            (decimal)WinMain._DebtsCreditsTableData.GetSubTotalPayment() +
            (decimal)WinMain._DebtsMonthlyTableData.GetNonChargeableValue() + (decimal)WinMain._settingMain._MortgageRentPayment;
        }

        public void SaveTableData()
        {
            DataTable data = this.sfdatagrid.ItemsSource as DataTable;
            int index = 1;
            foreach (DataRow row in data.Rows)
            {
                if (index == 1)
                {
                    WinMain._DebtsLoansTableData._Source1  = (string) row["MSource1"];
                    WinMain._DebtsLoansTableData._Payment1 = (double) row["MPayment1"];
                    WinMain._DebtsLoansTableData._Balance1 = (double)row["MBalance1"];
                    WinMain._DebtsLoansTableData._InterestRate1 = (double)row["MInterestRate1"];
                    WinMain._DebtsLoansTableData._OutstandInterest1 = (double)row["MOutstandInterest1"];

                    WinMain._DebtsLoansTableData._Source6 = (string)row["MSource2"];
                    WinMain._DebtsLoansTableData._Payment6 = (double)row["MPayment2"];
                    WinMain._DebtsLoansTableData._Balance6 = (double)row["MBalance2"];
                    WinMain._DebtsLoansTableData._InterestRate6 = (double)row["MInterestRate2"];
                    WinMain._DebtsLoansTableData._OutstandInterest6 = (double)row["MOutstandInterest2"];
                }
                else if (index == 2)
                {
                    WinMain._DebtsLoansTableData._Source2 = (string)row["MSource1"];
                    WinMain._DebtsLoansTableData._Payment2 = (double)row["MPayment1"];
                    WinMain._DebtsLoansTableData._Balance2 = (double)row["MBalance1"];
                    WinMain._DebtsLoansTableData._InterestRate2 = (double)row["MInterestRate1"];
                    WinMain._DebtsLoansTableData._OutstandInterest2 = (double)row["MOutstandInterest1"];

                    WinMain._DebtsLoansTableData._Source7 = (string)row["MSource2"];
                    WinMain._DebtsLoansTableData._Payment7 = (double)row["MPayment2"];
                    WinMain._DebtsLoansTableData._Balance7 = (double)row["MBalance2"];
                    WinMain._DebtsLoansTableData._InterestRate7 = (double)row["MInterestRate2"];
                    WinMain._DebtsLoansTableData._OutstandInterest7 = (double)row["MOutstandInterest2"];
                }
                else if (index == 3)
                {
                    WinMain._DebtsLoansTableData._Source3 = (string)row["MSource1"];
                    WinMain._DebtsLoansTableData._Payment3 = (double)row["MPayment1"];
                    WinMain._DebtsLoansTableData._Balance3 = (double)row["MBalance1"];
                    WinMain._DebtsLoansTableData._InterestRate3 = (double)row["MInterestRate1"];
                    WinMain._DebtsLoansTableData._OutstandInterest3 = (double)row["MOutstandInterest1"];

                    WinMain._DebtsLoansTableData._Source8 = (string)row["MSource2"];
                    WinMain._DebtsLoansTableData._Payment8 = (double)row["MPayment2"];
                    WinMain._DebtsLoansTableData._Balance8 = (double)row["MBalance2"];
                    WinMain._DebtsLoansTableData._InterestRate8 = (double)row["MInterestRate2"];
                    WinMain._DebtsLoansTableData._OutstandInterest8 = (double)row["MOutstandInterest2"];
                }
                else if (index == 4)
                {
                    WinMain._DebtsLoansTableData._Source4 = (string)row["MSource1"];
                    WinMain._DebtsLoansTableData._Payment4 = (double)row["MPayment1"];
                    WinMain._DebtsLoansTableData._Balance4 = (double)row["MBalance1"];
                    WinMain._DebtsLoansTableData._InterestRate4 = (double)row["MInterestRate1"];
                    WinMain._DebtsLoansTableData._OutstandInterest4 = (double)row["MOutstandInterest1"];

                    WinMain._DebtsLoansTableData._Source9 = (string)row["MSource2"];
                    WinMain._DebtsLoansTableData._Payment9 = (double)row["MPayment2"];
                    WinMain._DebtsLoansTableData._Balance9 = (double)row["MBalance2"];
                    WinMain._DebtsLoansTableData._InterestRate9 = (double)row["MInterestRate2"];
                    WinMain._DebtsLoansTableData._OutstandInterest9 = (double)row["MOutstandInterest2"];
                }
                else
                {
                    WinMain._DebtsLoansTableData._Source5 = (string)row["MSource1"];
                    WinMain._DebtsLoansTableData._Payment5 = (double)row["MPayment1"];
                    WinMain._DebtsLoansTableData._Balance5 = (double)row["MBalance1"];
                    WinMain._DebtsLoansTableData._InterestRate5 = (double)row["MInterestRate1"];
                    WinMain._DebtsLoansTableData._OutstandInterest5 = (double)row["MOutstandInterest1"];

                    WinMain._DebtsLoansTableData._Source10 = (string)row["MSource2"];
                    WinMain._DebtsLoansTableData._Payment10 = (double)row["MPayment2"];
                    WinMain._DebtsLoansTableData._Balance10 = (double)row["MBalance2"];
                    WinMain._DebtsLoansTableData._InterestRate10 = (double)row["MInterestRate2"];
                    WinMain._DebtsLoansTableData._OutstandInterest10 = (double)row["MOutstandInterest2"];

                }
                index++;
            }
            WinMain._DebtsLoansTableData.Save();
        }

        private void sfdatagrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            DataTable data = this.sfdatagrid.ItemsSource as DataTable;
            SaveTableData();
            UpdateSubTotal();
        }
    }
}
