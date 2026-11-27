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
    public partial class CtrlDebtsCreditsTable : UserControl
    {
        private DataTable mData;
        private CtrlPDOrgDebts _ctrlOrgDebts;
        public CtrlDebtsCreditsTable(CtrlPDOrgDebts ctrlOrgDebts)
        {
            _ctrlOrgDebts = ctrlOrgDebts;
            InitializeComponent();
            mData = new DataTable();
            DataColumn[] cols ={
                                  new DataColumn("MNo1",typeof(int)),
                                  new DataColumn("MSource1",typeof(string)),
                                  new DataColumn("MCType1",typeof(string)),
                                  new DataColumn("MPayment1",typeof(double)),
                                  new DataColumn("MBalance1",typeof(double)),
                                  new DataColumn("MLimit1",typeof(double)),
                                  new DataColumn("MInterestRate1",typeof(double)),


                                  new DataColumn("MNo2",typeof(int)),
                                  new DataColumn("MSource2",typeof(string)),
                                  new DataColumn("MCType2",typeof(string)),
                                  new DataColumn("MPayment2",typeof(double)),
                                  new DataColumn("MBalance2",typeof(double)),
                                  new DataColumn("MLimit2",typeof(double)),
                                  new DataColumn("MInterestRate2",typeof(double)),
                              };
            mData.Columns.AddRange(cols);

            for (int i = 1; i <= 6; i++)
            {
                DataRow drow = mData.NewRow();
                if (i == 1)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsCreditsTableData._Source1;
                    drow["MCType1"] = WinMain._DebtsCreditsTableData._TypeCCLOC1;
                    drow["MPayment1"] = WinMain._DebtsCreditsTableData._Payment1;
                    drow["MBalance1"] = WinMain._DebtsCreditsTableData._Balance1;
                    drow["MInterestRate1"] = WinMain._DebtsCreditsTableData._InterestRate1;
                    drow["MLimit1"] = WinMain._DebtsCreditsTableData._Limit1;

                    drow["MNo2"] = i + 6;
                    drow["MSource2"] = WinMain._DebtsCreditsTableData._Source7;
                    drow["MCType2"] = WinMain._DebtsCreditsTableData._TypeCCLOC7;
                    drow["MPayment2"] = WinMain._DebtsCreditsTableData._Payment7;
                    drow["MBalance2"] = WinMain._DebtsCreditsTableData._Balance7;
                    drow["MInterestRate2"] = WinMain._DebtsCreditsTableData._InterestRate7;
                    drow["MLimit2"] = WinMain._DebtsCreditsTableData._Limit7;
                }
                else if (i == 2)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsCreditsTableData._Source2;
                    drow["MCType1"] = WinMain._DebtsCreditsTableData._TypeCCLOC2;
                    drow["MPayment1"] = WinMain._DebtsCreditsTableData._Payment2;
                    drow["MBalance1"] = WinMain._DebtsCreditsTableData._Balance2;
                    drow["MInterestRate1"] = WinMain._DebtsCreditsTableData._InterestRate2;
                    drow["MLimit1"] = WinMain._DebtsCreditsTableData._Limit2;

                    drow["MNo2"] = i + 6;
                    drow["MSource2"] = WinMain._DebtsCreditsTableData._Source8;
                    drow["MCType2"] = WinMain._DebtsCreditsTableData._TypeCCLOC8;
                    drow["MPayment2"] = WinMain._DebtsCreditsTableData._Payment8;
                    drow["MBalance2"] = WinMain._DebtsCreditsTableData._Balance8;
                    drow["MInterestRate2"] = WinMain._DebtsCreditsTableData._InterestRate8;
                    drow["MLimit2"] = WinMain._DebtsCreditsTableData._Limit8;
                }
                else if (i == 3)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsCreditsTableData._Source3;
                    drow["MCType1"] = WinMain._DebtsCreditsTableData._TypeCCLOC3;
                    drow["MPayment1"] = WinMain._DebtsCreditsTableData._Payment3;
                    drow["MBalance1"] = WinMain._DebtsCreditsTableData._Balance3;
                    drow["MInterestRate1"] = WinMain._DebtsCreditsTableData._InterestRate3;
                    drow["MLimit1"] = WinMain._DebtsCreditsTableData._Limit3;

                    drow["MNo2"] = i + 6;
                    drow["MSource2"] = WinMain._DebtsCreditsTableData._Source9;
                    drow["MCType2"] = WinMain._DebtsCreditsTableData._TypeCCLOC9;
                    drow["MPayment2"] = WinMain._DebtsCreditsTableData._Payment9;
                    drow["MBalance2"] = WinMain._DebtsCreditsTableData._Balance9;
                    drow["MInterestRate2"] = WinMain._DebtsCreditsTableData._InterestRate9;
                    drow["MLimit2"] = WinMain._DebtsCreditsTableData._Limit9;
                }
                else if (i == 4)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsCreditsTableData._Source4;
                    drow["MCType1"] = WinMain._DebtsCreditsTableData._TypeCCLOC4;
                    drow["MPayment1"] = WinMain._DebtsCreditsTableData._Payment4;
                    drow["MBalance1"] = WinMain._DebtsCreditsTableData._Balance4;
                    drow["MInterestRate1"] = WinMain._DebtsCreditsTableData._InterestRate4;
                    drow["MLimit1"] = WinMain._DebtsCreditsTableData._Limit4;

                    drow["MNo2"] = i + 6;
                    drow["MSource2"] = WinMain._DebtsCreditsTableData._Source10;
                    drow["MCType2"] = WinMain._DebtsCreditsTableData._TypeCCLOC10;
                    drow["MPayment2"] = WinMain._DebtsCreditsTableData._Payment10;
                    drow["MBalance2"] = WinMain._DebtsCreditsTableData._Balance10;
                    drow["MInterestRate2"] = WinMain._DebtsCreditsTableData._InterestRate10;
                    drow["MLimit2"] = WinMain._DebtsCreditsTableData._Limit10;
                }

                else if (i == 5)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsCreditsTableData._Source5;
                    drow["MCType1"] = WinMain._DebtsCreditsTableData._TypeCCLOC5;
                    drow["MPayment1"] = WinMain._DebtsCreditsTableData._Payment5;
                    drow["MBalance1"] = WinMain._DebtsCreditsTableData._Balance5;
                    drow["MInterestRate1"] = WinMain._DebtsCreditsTableData._InterestRate5;
                    drow["MLimit1"] = WinMain._DebtsCreditsTableData._Limit5;

                    drow["MNo2"] = i + 6;
                    drow["MSource2"] = WinMain._DebtsCreditsTableData._Source11;
                    drow["MCType2"] = WinMain._DebtsCreditsTableData._TypeCCLOC10;
                    drow["MPayment2"] = WinMain._DebtsCreditsTableData._Payment11;
                    drow["MBalance2"] = WinMain._DebtsCreditsTableData._Balance11;
                    drow["MInterestRate2"] = WinMain._DebtsCreditsTableData._InterestRate11;
                    drow["MLimit2"] = WinMain._DebtsCreditsTableData._Limit11;
                }

                else
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._DebtsCreditsTableData._Source6;
                    drow["MCType1"] = WinMain._DebtsCreditsTableData._TypeCCLOC6;
                    drow["MPayment1"] = WinMain._DebtsCreditsTableData._Payment6;
                    drow["MBalance1"] = WinMain._DebtsCreditsTableData._Balance6;
                    drow["MInterestRate1"] = WinMain._DebtsCreditsTableData._InterestRate6;
                    drow["MLimit1"] = WinMain._DebtsCreditsTableData._Limit6;

                    drow["MNo2"] = i + 6;
                    drow["MSource2"] = WinMain._DebtsCreditsTableData._Source12;
                    drow["MCType2"] = WinMain._DebtsCreditsTableData._TypeCCLOC12;
                    drow["MPayment2"] = WinMain._DebtsCreditsTableData._Payment12;
                    drow["MBalance2"] = WinMain._DebtsCreditsTableData._Balance12;
                    drow["MInterestRate2"] = WinMain._DebtsCreditsTableData._InterestRate12;
                    drow["MLimit2"] = WinMain._DebtsCreditsTableData._Limit12;
                }

                mData.Rows.Add(drow);
            }
            UpdateSubTotal();
            this.sfdatagrid.ItemsSource = mData;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbxRunName.Text = HardCode.HINT_NAME_DAN;
            this.tbxRunContent.Text = "Your Revolving Lines of Credit include - Personal Line of Credit, Home Equity Line of Credit, Business Line of Credit (if applicable), and First Lien Home Equity Line Of Credit.";
        }

        private void UpdateSubTotal()
        {
            double subtotalp = WinMain._DebtsCreditsTableData.GetSubTotalPayment();
            double subtotalb = WinMain._DebtsCreditsTableData.GetTotalBalance();

            this.ctbxSubTotalPayment.Value = (decimal)subtotalp;
            this.ctbxSubTotalBalance.Value = (decimal)subtotalb;

            if (this._ctrlOrgDebts._loansTable == null) return;

            this._ctrlOrgDebts.ctbxTotalDebt.Value = (decimal)((double)WinMain._DebtsLoansTableData.GetTotalBalance() + subtotalb);

            this._ctrlOrgDebts.ctbxTotalMonthlyExpense.Value = (decimal)((double)WinMain._DebtsLoansTableData.GetSubTotalPayment() + subtotalp
                + (double)(WinMain._DebtsMonthlyTableData.GetChargeableValue() + WinMain._DebtsMonthlyTableData.GetNonChargeableValue()) + (double)(WinMain._DebtsLivingExpTableData.GetChargeableValue() + WinMain._DebtsLivingExpTableData.GetNonChargeableValue()) + (double)WinMain._settingMain._MortgageRentPayment);

            this._ctrlOrgDebts.ctbxNonChargeableExpense.Value = (decimal)WinMain._DebtsLivingExpTableData.GetNonChargeableValue() +
                    (decimal)subtotalp +
                    (decimal)WinMain._DebtsLoansTableData.GetSubTotalPayment() +
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
                    WinMain._DebtsCreditsTableData._Source1  = (string) row["MSource1"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC1 = (string)row["MCType1"];
                    WinMain._DebtsCreditsTableData._Payment1 = (double) row["MPayment1"];
                    WinMain._DebtsCreditsTableData._Balance1 = (double)row["MBalance1"];
                    WinMain._DebtsCreditsTableData._InterestRate1 = (double)row["MInterestRate1"];
                    WinMain._DebtsCreditsTableData._Limit1 = (double)row["MLimit1"];

                    WinMain._DebtsCreditsTableData._Source7 = (string)row["MSource2"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC7 = (string)row["MCType2"];
                    WinMain._DebtsCreditsTableData._Payment7 = (double)row["MPayment2"];
                    WinMain._DebtsCreditsTableData._Balance7 = (double)row["MBalance2"];
                    WinMain._DebtsCreditsTableData._InterestRate7 = (double)row["MInterestRate2"];
                    WinMain._DebtsCreditsTableData._Limit7 = (double)row["MLimit2"];
                }
                else if (index == 2)
                {
                    WinMain._DebtsCreditsTableData._Source2 = (string)row["MSource1"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC2 = (string)row["MCType1"];
                    WinMain._DebtsCreditsTableData._Payment2 = (double)row["MPayment1"];
                    WinMain._DebtsCreditsTableData._Balance2 = (double)row["MBalance1"];
                    WinMain._DebtsCreditsTableData._InterestRate2 = (double)row["MInterestRate1"];
                    WinMain._DebtsCreditsTableData._Limit2 = (double)row["MLimit1"];

                    WinMain._DebtsCreditsTableData._Source8 = (string)row["MSource2"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC8 = (string)row["MCType2"];
                    WinMain._DebtsCreditsTableData._Payment8 = (double)row["MPayment2"];
                    WinMain._DebtsCreditsTableData._Balance8 = (double)row["MBalance2"];
                    WinMain._DebtsCreditsTableData._InterestRate8 = (double)row["MInterestRate2"];
                    WinMain._DebtsCreditsTableData._Limit8 = (double)row["MLimit2"];
                }
                else if (index == 3)
                {
                    WinMain._DebtsCreditsTableData._Source3 = (string)row["MSource1"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC3 = (string)row["MCType1"];
                    WinMain._DebtsCreditsTableData._Payment3 = (double)row["MPayment1"];
                    WinMain._DebtsCreditsTableData._Balance3 = (double)row["MBalance1"];
                    WinMain._DebtsCreditsTableData._InterestRate3 = (double)row["MInterestRate1"];
                    WinMain._DebtsCreditsTableData._Limit3 = (double)row["MLimit1"];

                    WinMain._DebtsCreditsTableData._Source9 = (string)row["MSource2"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC9 = (string)row["MCType2"];
                    WinMain._DebtsCreditsTableData._Payment9 = (double)row["MPayment2"];
                    WinMain._DebtsCreditsTableData._Balance9 = (double)row["MBalance2"];
                    WinMain._DebtsCreditsTableData._InterestRate9 = (double)row["MInterestRate2"];
                    WinMain._DebtsCreditsTableData._Limit9 = (double)row["MLimit2"];
                }
                else if (index == 4)
                {
                    WinMain._DebtsCreditsTableData._Source4 = (string)row["MSource1"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC4 = (string)row["MCType1"];
                    WinMain._DebtsCreditsTableData._Payment4 = (double)row["MPayment1"];
                    WinMain._DebtsCreditsTableData._Balance4 = (double)row["MBalance1"];
                    WinMain._DebtsCreditsTableData._InterestRate4 = (double)row["MInterestRate1"];
                    WinMain._DebtsCreditsTableData._Limit4 = (double)row["MLimit1"];

                    WinMain._DebtsCreditsTableData._Source10 = (string)row["MSource2"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC10 = (string)row["MCType2"];
                    WinMain._DebtsCreditsTableData._Payment10 = (double)row["MPayment2"];
                    WinMain._DebtsCreditsTableData._Balance10 = (double)row["MBalance2"];
                    WinMain._DebtsCreditsTableData._InterestRate10 = (double)row["MInterestRate2"];
                    WinMain._DebtsCreditsTableData._Limit10 = (double)row["MLimit2"];
                }
                else if (index == 5)
                {
                    WinMain._DebtsCreditsTableData._Source5 = (string)row["MSource1"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC5 = (string)row["MCType1"];
                    WinMain._DebtsCreditsTableData._Payment5 = (double)row["MPayment1"];
                    WinMain._DebtsCreditsTableData._Balance5 = (double)row["MBalance1"];
                    WinMain._DebtsCreditsTableData._InterestRate5 = (double)row["MInterestRate1"];
                    WinMain._DebtsCreditsTableData._Limit5 = (double)row["MLimit1"];

                    WinMain._DebtsCreditsTableData._Source11 = (string)row["MSource2"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC11 = (string)row["MCType2"];
                    WinMain._DebtsCreditsTableData._Payment11 = (double)row["MPayment2"];
                    WinMain._DebtsCreditsTableData._Balance11 = (double)row["MBalance2"];
                    WinMain._DebtsCreditsTableData._InterestRate11 = (double)row["MInterestRate2"];
                    WinMain._DebtsCreditsTableData._Limit11 = (double)row["MLimit2"];
                }
                else
                {
                    WinMain._DebtsCreditsTableData._Source6 = (string)row["MSource1"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC6 = (string)row["MCType1"];
                    WinMain._DebtsCreditsTableData._Payment6 = (double)row["MPayment1"];
                    WinMain._DebtsCreditsTableData._Balance6 = (double)row["MBalance1"];
                    WinMain._DebtsCreditsTableData._InterestRate6 = (double)row["MInterestRate1"];
                    WinMain._DebtsCreditsTableData._Limit6 = (double)row["MLimit1"];

                    WinMain._DebtsCreditsTableData._Source12 = (string)row["MSource2"];
                    WinMain._DebtsCreditsTableData._TypeCCLOC12 = (string)row["MCType2"];
                    WinMain._DebtsCreditsTableData._Payment12 = (double)row["MPayment2"];
                    WinMain._DebtsCreditsTableData._Balance12 = (double)row["MBalance2"];
                    WinMain._DebtsCreditsTableData._InterestRate12 = (double)row["MInterestRate2"];
                    WinMain._DebtsCreditsTableData._Limit12 = (double)row["MLimit2"];

                }
                index++;
            }
            WinMain._DebtsCreditsTableData.Save();
        }
        private void sfdatagrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            SaveTableData();
            UpdateSubTotal();
            //WinMain._ctrlIdentify.UpdateAllTabls();
        }
    }
}
