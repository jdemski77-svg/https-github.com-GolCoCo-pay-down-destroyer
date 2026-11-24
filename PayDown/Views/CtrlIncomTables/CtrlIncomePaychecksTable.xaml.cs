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
    public partial class CtrlIncomePaychecksTable : UserControl
    {
        private DataTable mData;
        private CtrlPDOrgIncome _ctrlOrgIncome;
        public CtrlIncomePaychecksTable(CtrlPDOrgIncome _ctrlOrgIncome)
        {
            InitializeComponent();
            this._ctrlOrgIncome = _ctrlOrgIncome;
            mData = new DataTable();
            DataColumn[] cols ={
                                  new DataColumn("MNo",typeof(int)),
                                  new DataColumn("MNo1",typeof(string)),
                                  new DataColumn("MSource1",typeof(string)),
                                  new DataColumn("MAmount1",typeof(double)),
                                  new DataColumn("MNo2",typeof(string)),
                                  new DataColumn("MSource2",typeof(string)),
                                  new DataColumn("MAmount2",typeof(double)),
                                  new DataColumn("MNo3",typeof(string)),
                                  new DataColumn("MSource3",typeof(string)),
                                  new DataColumn("MAmount3",typeof(double)),
                                  new DataColumn("MNo4",typeof(string)),
                                  new DataColumn("MSource4",typeof(string)),
                                  new DataColumn("MAmount4",typeof(double)),
                              };

            mData.Columns.AddRange(cols);

            for (int i = 1; i <= 3; i++)
            {
                DataRow drow = mData.NewRow();
                if (i == 1)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._IncomePaychecksTableData._Source1;
                    drow["MAmount1"] = WinMain._IncomePaychecksTableData._Amount1;
                    drow["MNo2"] = i + 3;
                    drow["MSource2"] = WinMain._IncomePaychecksTableData._Source4;
                    drow["MAmount2"] = WinMain._IncomePaychecksTableData._Amount4;
                    drow["MNo3"] = i + 6;
                    drow["MSource3"] = WinMain._IncomePaychecksTableData._Source7;
                    drow["MAmount3"] = WinMain._IncomePaychecksTableData._Amount7;
                    drow["MNo4"] = i + 9;
                    drow["MSource4"] = WinMain._IncomePaychecksTableData._Source10;
                    drow["MAmount4"] = WinMain._IncomePaychecksTableData._Amount10;
                }
                else if (i == 2)
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._IncomePaychecksTableData._Source2;
                    drow["MAmount1"] = WinMain._IncomePaychecksTableData._Amount2;
                    drow["MNo2"] = i + 3;
                    drow["MSource2"] = WinMain._IncomePaychecksTableData._Source5;
                    drow["MAmount2"] = WinMain._IncomePaychecksTableData._Amount5;
                    drow["MNo3"] = i + 6;
                    drow["MSource3"] = WinMain._IncomePaychecksTableData._Source8;
                    drow["MAmount3"] = WinMain._IncomePaychecksTableData._Amount8;
                    drow["MNo4"] = i + 9;
                    drow["MSource4"] = WinMain._IncomePaychecksTableData._Source11;
                    drow["MAmount4"] = WinMain._IncomePaychecksTableData._Amount11;
                }
                else
                {
                    drow["MNo1"] = i;
                    drow["MSource1"] = WinMain._IncomePaychecksTableData._Source3;
                    drow["MAmount1"] = WinMain._IncomePaychecksTableData._Amount3;
                    drow["MNo2"] = i + 3;
                    drow["MSource2"] = WinMain._IncomePaychecksTableData._Source6;
                    drow["MAmount2"] = WinMain._IncomePaychecksTableData._Amount6;
                    drow["MNo3"] = i + 6;
                    drow["MSource3"] = WinMain._IncomePaychecksTableData._Source9;
                    drow["MAmount3"] = WinMain._IncomePaychecksTableData._Amount9;
                    drow["MNo4"] = i + 9;
                    drow["MSource4"] = WinMain._IncomePaychecksTableData._Source12;
                    drow["MAmount4"] = WinMain._IncomePaychecksTableData._Amount12;
                }
                mData.Rows.Add(drow);
            }
            UpdateSubTotalPrice(mData);
            this.sfdatagrid.ItemsSource = mData;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbxRunName.Text = HardCode.HINT_NAME_DAN;
            //this.tbxRunContent.Text = HardCode.HINT_INCOME_PAYCHECK;
        }

        private void UpdateSubTotalPrice(DataTable data)
        {
            this.ctbxSubTotal.Value = (decimal)WinMain._IncomePaychecksTableData.GetTotalAmount();
            UpdateIncomeTotalPrice();
        }

        private void UpdateIncomeTotalPrice()
        {
            this._ctrlOrgIncome.ctbxIncomeTotal.Value = (decimal)WinMain._IncomePaychecksTableData.GetTotalAmount()
                + (decimal)WinMain._IncomeGovermentalTableData.GetTotalAmount()
                + (decimal)WinMain._IncomeRetirementTableData.GetTotalAmount() +
                +(decimal)WinMain._IncomeCheckingTableData.GetTotalAmount();
        }

        private List<int> SourceCols = new List<int>() { 2, 5, 8, 11 };

        private void sfdatagrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            DataTable data = this.sfdatagrid.ItemsSource as DataTable;


            int colIndex = e.RowColumnIndex.ColumnIndex + 1;
            int rowIndex = e.RowColumnIndex.RowIndex - 1;

            string source = "";
            double amount = 0;

            int Index = 0;

            if (SourceCols.Contains(colIndex))
            {
                Index = (colIndex / 3) * 3 + rowIndex + 1;
                source = (string)data.Rows[rowIndex][colIndex];
                amount = (double)data.Rows[rowIndex][colIndex + 1];
            }
            else
            {
                Index = ((colIndex - 1) / 3) * 3 + rowIndex + 1;
                source = (string)data.Rows[rowIndex][colIndex - 1];
                amount = (double)data.Rows[rowIndex][colIndex];
            }


            switch (Index)
            {
                case 1:
                    WinMain._IncomePaychecksTableData._Source1 = source;
                    WinMain._IncomePaychecksTableData._Amount1 = amount;
                    break;
                case 2:
                    WinMain._IncomePaychecksTableData._Source2 = source;
                    WinMain._IncomePaychecksTableData._Amount2 = amount;
                    break;
                case 3:
                    WinMain._IncomePaychecksTableData._Source3 = source;
                    WinMain._IncomePaychecksTableData._Amount3 = amount;
                    break;
                case 4:
                    WinMain._IncomePaychecksTableData._Source4 = source;
                    WinMain._IncomePaychecksTableData._Amount4 = amount;
                    break;
                case 5:
                    WinMain._IncomePaychecksTableData._Source5 = source;
                    WinMain._IncomePaychecksTableData._Amount5 = amount;
                    break;
                case 6:
                    WinMain._IncomePaychecksTableData._Source6 = source;
                    WinMain._IncomePaychecksTableData._Amount6 = amount;
                    break;
                case 7:
                    WinMain._IncomePaychecksTableData._Source7 = source;
                    WinMain._IncomePaychecksTableData._Amount7 = amount;
                    break;
                case 8:
                    WinMain._IncomePaychecksTableData._Source8 = source;
                    WinMain._IncomePaychecksTableData._Amount8 = amount;
                    break;
                case 9:
                    WinMain._IncomePaychecksTableData._Source9 = source;
                    WinMain._IncomePaychecksTableData._Amount9 = amount;
                    break;
                case 10:
                    WinMain._IncomePaychecksTableData._Source10 = source;
                    WinMain._IncomePaychecksTableData._Amount10 = amount;
                    break;
                case 11:
                    WinMain._IncomePaychecksTableData._Source11 = source;
                    WinMain._IncomePaychecksTableData._Amount11 = amount;
                    break;
                case 12:
                    WinMain._IncomePaychecksTableData._Source12 = source;
                    WinMain._IncomePaychecksTableData._Amount12 = amount;
                    break;
            }
            WinMain._IncomePaychecksTableData.Save();
            UpdateSubTotalPrice(data);
        }
    }
}
