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
    public partial class CtrlDebtsLivingExpTable : UserControl
    {
        private DataTable mData;
        public double _subChargeable = 0;
        public double _subNonChargeable = 0;
        private CtrlPDOrgDebts _ctrlOrgDebts;
        public CtrlDebtsLivingExpTable(CtrlPDOrgDebts ctrlOrgDebts)
        {
            _ctrlOrgDebts = ctrlOrgDebts;
            WinMain._DebtsLivingExpTableData = DebtsLivingExpTable.Load<DebtsLivingExpTable>();
            if(WinMain._DebtsLivingExpTableData == null)
            {
                WinMain._DebtsLivingExpTableData = new DebtsLivingExpTable();
            }
            InitializeComponent();
            mData = new DataTable();
            DataColumn[] cols ={
                                  new DataColumn("MNo1",typeof(int)),
                                  new DataColumn("MCYN1",typeof(string)),
                                  new DataColumn("MSource1",typeof(string)),
                                  new DataColumn("MAmount1",typeof(double)),
                                  new DataColumn("MNo2",typeof(int)),
                                  new DataColumn("MCYN2",typeof(string)),
                                  new DataColumn("MSource2",typeof(string)),
                                  new DataColumn("MAmount2",typeof(double)),
                                  new DataColumn("MNo3",typeof(int)),
                                  new DataColumn("MCYN3",typeof(string)),
                                  new DataColumn("MSource3",typeof(string)),
                                  new DataColumn("MAmount3",typeof(double)),
                              };
            mData.Columns.AddRange(cols);

            for (int i = 1; i <= 5; i++)
            {
                DataRow drow = mData.NewRow();
                if (i == 1)
                {
                    drow["MNo1"] = i;
                    drow["MCYN1"] = WinMain._DebtsLivingExpTableData._CYN1;
                    drow["MSource1"] = WinMain._DebtsLivingExpTableData._Source1;
                    drow["MAmount1"] = WinMain._DebtsLivingExpTableData._Amount1;

                    drow["MNo2"] = i + 5;
                    drow["MCYN2"] = WinMain._DebtsLivingExpTableData._CYN6;
                    drow["MSource2"] = WinMain._DebtsLivingExpTableData._Source6;
                    drow["MAmount2"] = WinMain._DebtsLivingExpTableData._Amount6;

                    drow["MNo3"] = i + 10;
                    drow["MCYN3"] = WinMain._DebtsLivingExpTableData._CYN11;
                    drow["MSource3"] = WinMain._DebtsLivingExpTableData._Source11;
                    drow["MAmount3"] = WinMain._DebtsLivingExpTableData._Amount11;
                }
                else if (i == 2) {
                    drow["MNo1"] = i;
                    drow["MCYN1"] = WinMain._DebtsLivingExpTableData._CYN2;
                    drow["MSource1"] = WinMain._DebtsLivingExpTableData._Source2;
                    drow["MAmount1"] = WinMain._DebtsLivingExpTableData._Amount2;

                    drow["MNo2"] = i + 5;
                    drow["MCYN2"] = WinMain._DebtsLivingExpTableData._CYN7;
                    drow["MSource2"] = WinMain._DebtsLivingExpTableData._Source7;
                    drow["MAmount2"] = WinMain._DebtsLivingExpTableData._Amount7;

                    drow["MNo3"] = i + 10;
                    drow["MCYN3"] = WinMain._DebtsLivingExpTableData._CYN12;
                    drow["MSource3"] = WinMain._DebtsLivingExpTableData._Source12;
                    drow["MAmount3"] = WinMain._DebtsLivingExpTableData._Amount12;
                }
                else if (i == 3)
                {
                    drow["MNo1"] = i;
                    drow["MCYN1"] = WinMain._DebtsLivingExpTableData._CYN3;
                    drow["MSource1"] = WinMain._DebtsLivingExpTableData._Source3;
                    drow["MAmount1"] = WinMain._DebtsLivingExpTableData._Amount3;

                    drow["MNo2"] = i + 5;
                    drow["MCYN2"] = WinMain._DebtsLivingExpTableData._CYN8;
                    drow["MSource2"] = WinMain._DebtsLivingExpTableData._Source8;
                    drow["MAmount2"] = WinMain._DebtsLivingExpTableData._Amount8;

                    drow["MNo3"] = i + 10;
                    drow["MCYN3"] = WinMain._DebtsLivingExpTableData._CYN13;
                    drow["MSource3"] = WinMain._DebtsLivingExpTableData._Source13;
                    drow["MAmount3"] = WinMain._DebtsLivingExpTableData._Amount13;
                }
                else if (i == 4)
                {
                    drow["MNo1"] = i;
                    drow["MCYN1"] = WinMain._DebtsLivingExpTableData._CYN4;
                    drow["MSource1"] = WinMain._DebtsLivingExpTableData._Source4;
                    drow["MAmount1"] = WinMain._DebtsLivingExpTableData._Amount4;

                    drow["MNo2"] = i + 5;
                    drow["MCYN2"] = WinMain._DebtsLivingExpTableData._CYN9;
                    drow["MSource2"] = WinMain._DebtsLivingExpTableData._Source9;
                    drow["MAmount2"] = WinMain._DebtsLivingExpTableData._Amount9;

                    drow["MNo3"] = i + 10;
                    drow["MCYN3"] = WinMain._DebtsLivingExpTableData._CYN14;
                    drow["MSource3"] = WinMain._DebtsLivingExpTableData._Source14;
                    drow["MAmount3"] = WinMain._DebtsLivingExpTableData._Amount14;
                }
                else
                {
                    drow["MNo1"] = i;
                    drow["MCYN1"] = WinMain._DebtsLivingExpTableData._CYN5;
                    drow["MSource1"] = WinMain._DebtsLivingExpTableData._Source5;
                    drow["MAmount1"] = WinMain._DebtsLivingExpTableData._Amount5;

                    drow["MNo2"] = i + 5;
                    drow["MCYN2"] = WinMain._DebtsLivingExpTableData._CYN10;
                    drow["MSource2"] = WinMain._DebtsLivingExpTableData._Source10;
                    drow["MAmount2"] = WinMain._DebtsLivingExpTableData._Amount10;

                    drow["MNo3"] = i + 10;
                    drow["MCYN3"] = WinMain._DebtsLivingExpTableData._CYN15;
                    drow["MSource3"] = WinMain._DebtsLivingExpTableData._Source15;
                    drow["MAmount3"] = WinMain._DebtsLivingExpTableData._Amount15;
                }

                mData.Rows.Add(drow);
            }
            _subChargeable = WinMain._DebtsLivingExpTableData.GetChargeableValue();
            _subNonChargeable = WinMain._DebtsLivingExpTableData.GetNonChargeableValue();

            this.ctbxSubTotal.Value = (decimal)(_subChargeable + _subNonChargeable);
            this.sfdatagrid.ItemsSource = mData;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateOrgDebts()
        {
            SaveTableData();
            _subChargeable = WinMain._DebtsLivingExpTableData.GetChargeableValue();
            _subNonChargeable = WinMain._DebtsLivingExpTableData.GetNonChargeableValue();

            this.ctbxSubTotal.Value = (decimal)(_subChargeable + _subNonChargeable);
            this._ctrlOrgDebts.ctbxTotalMonthlyExpense.Value = (decimal)(
            (double)this._ctrlOrgDebts._loansTable.ctbxSubTotalPayment.Value +
            (double)this._ctrlOrgDebts._creditTable.ctbxSubTotalPayment.Value +
            (double)(this._ctrlOrgDebts.ctbxMortgage.Value) +
            (double)(this._ctrlOrgDebts._monthBillsTable.ctbxSubTotal.Value) +
            (double)(this._ctrlOrgDebts._LivingExpenseTable.ctbxSubTotal.Value)
            );

            this._ctrlOrgDebts.ctbxChargeExpsenses.Value = (decimal)_subChargeable + (decimal)WinMain._DebtsMonthlyTableData.GetChargeableValue();
            this._ctrlOrgDebts.ctbxNonChargeableExpense.Value = (decimal)_subNonChargeable +
                this._ctrlOrgDebts._loansTable.ctbxSubTotalPayment.Value +
                this._ctrlOrgDebts._creditTable.ctbxSubTotalPayment.Value +
                (decimal)WinMain._DebtsMonthlyTableData.GetNonChargeableValue() + _ctrlOrgDebts.ctbxMortgage.Value;
        }

        public void SaveTableData()
        {
            DataTable data = this.sfdatagrid.ItemsSource as DataTable;
            int index = 1;
            foreach (DataRow row in data.Rows)
            {
                if (index == 1)
                {
                    WinMain._DebtsLivingExpTableData._CYN1 = (string)row["MCYN1"];
                    WinMain._DebtsLivingExpTableData._Source1 = (string)row["MSource1"];
                    WinMain._DebtsLivingExpTableData._Amount1 = (double)row["MAmount1"];

                    WinMain._DebtsLivingExpTableData._CYN6 = (string)row["MCYN2"];
                    WinMain._DebtsLivingExpTableData._Source6 = (string)row["MSource2"];
                    WinMain._DebtsLivingExpTableData._Amount6 = (double)row["MAmount2"];

                    WinMain._DebtsLivingExpTableData._CYN11 = (string)row["MCYN3"];
                    WinMain._DebtsLivingExpTableData._Source11 = (string)row["MSource3"];
                    WinMain._DebtsLivingExpTableData._Amount11 = (double)row["MAmount3"];

                }
                else if (index == 2)
                {
                    WinMain._DebtsLivingExpTableData._CYN2 = (string)row["MCYN1"];
                    WinMain._DebtsLivingExpTableData._Source2 = (string)row["MSource1"];
                    WinMain._DebtsLivingExpTableData._Amount2 = (double)row["MAmount1"];

                    WinMain._DebtsLivingExpTableData._CYN7 = (string)row["MCYN2"];
                    WinMain._DebtsLivingExpTableData._Source7 = (string)row["MSource2"];
                    WinMain._DebtsLivingExpTableData._Amount7 = (double)row["MAmount2"];

                    WinMain._DebtsLivingExpTableData._CYN12 = (string)row["MCYN3"];
                    WinMain._DebtsLivingExpTableData._Source12 = (string)row["MSource3"];
                    WinMain._DebtsLivingExpTableData._Amount12 = (double)row["MAmount3"];
                }
                else if(index == 3) 
                {
                    WinMain._DebtsLivingExpTableData._CYN3 = (string)row["MCYN1"];
                    WinMain._DebtsLivingExpTableData._Source3 = (string)row["MSource1"];
                    WinMain._DebtsLivingExpTableData._Amount3 = (double)row["MAmount1"];

                    WinMain._DebtsLivingExpTableData._CYN8 = (string)row["MCYN2"];
                    WinMain._DebtsLivingExpTableData._Source8 = (string)row["MSource2"];
                    WinMain._DebtsLivingExpTableData._Amount8 = (double)row["MAmount2"];

                    WinMain._DebtsLivingExpTableData._CYN13 = (string)row["MCYN3"];
                    WinMain._DebtsLivingExpTableData._Source13 = (string)row["MSource3"];
                    WinMain._DebtsLivingExpTableData._Amount13 = (double)row["MAmount3"];
                }
                else if (index == 4)
                {
                    WinMain._DebtsLivingExpTableData._CYN4 = (string)row["MCYN1"];
                    WinMain._DebtsLivingExpTableData._Source4 = (string)row["MSource1"];
                    WinMain._DebtsLivingExpTableData._Amount4 = (double)row["MAmount1"];

                    WinMain._DebtsLivingExpTableData._CYN9 = (string)row["MCYN2"];
                    WinMain._DebtsLivingExpTableData._Source9 = (string)row["MSource2"];
                    WinMain._DebtsLivingExpTableData._Amount9 = (double)row["MAmount2"];

                    WinMain._DebtsLivingExpTableData._CYN14 = (string)row["MCYN3"];
                    WinMain._DebtsLivingExpTableData._Source14 = (string)row["MSource3"];
                    WinMain._DebtsLivingExpTableData._Amount14 = (double)row["MAmount3"];
                }
                else
                {
                    WinMain._DebtsLivingExpTableData._CYN5 = (string)row["MCYN1"];
                    WinMain._DebtsLivingExpTableData._Source5 = (string)row["MSource1"];
                    WinMain._DebtsLivingExpTableData._Amount5 = (double)row["MAmount1"];

                    WinMain._DebtsLivingExpTableData._CYN10 = (string)row["MCYN2"];
                    WinMain._DebtsLivingExpTableData._Source10 = (string)row["MSource2"];
                    WinMain._DebtsLivingExpTableData._Amount10 = (double)row["MAmount2"];

                    WinMain._DebtsLivingExpTableData._CYN15 = (string)row["MCYN3"];
                    WinMain._DebtsLivingExpTableData._Source15 = (string)row["MSource3"];
                    WinMain._DebtsLivingExpTableData._Amount15 = (double)row["MAmount3"];

                }
                index++;
            }
            WinMain._DebtsLivingExpTableData.Save();
        }

        private void GridCheckBoxColumn_Changed(object sender, System.EventArgs e)
        {
            SaveTableData();
            _subChargeable = WinMain._DebtsLivingExpTableData.GetChargeableValue();
            _subNonChargeable = WinMain._DebtsLivingExpTableData.GetNonChargeableValue();

            this._ctrlOrgDebts.ctbxChargeExpsenses.Value = (decimal)_subChargeable + (decimal)WinMain._DebtsMonthlyTableData.GetChargeableValue();
            this._ctrlOrgDebts.ctbxNonChargeableExpense.Value = (decimal)_subNonChargeable +
                this._ctrlOrgDebts._loansTable.ctbxSubTotalPayment.Value +
                this._ctrlOrgDebts._creditTable.ctbxSubTotalPayment.Value +
                (decimal)WinMain._DebtsMonthlyTableData.GetNonChargeableValue() + (decimal)WinMain._settingMain._MortgageRentPayment;
        }

        private void sfdatagrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            UpdateOrgDebts();
        }
    }
}
