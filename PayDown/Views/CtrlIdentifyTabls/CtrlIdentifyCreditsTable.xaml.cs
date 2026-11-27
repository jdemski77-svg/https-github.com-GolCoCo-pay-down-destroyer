using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CtrlIdentifyCreditsTable : UserControl
    {
        private DataTable mData;
        
        public CtrlIdentifyCreditsTable()
        {
            InitializeComponent();
            mData = new DataTable();
            DataColumn[] cols ={
                                  new DataColumn("MNo1",typeof(int)),
                                  new DataColumn("MSource1",typeof(string)),
                                  new DataColumn("MPayment1",typeof(double)),
                                  new DataColumn("MBalance1",typeof(double)),
                                  new DataColumn("MInterestRate1",typeof(double)),
                                  new DataColumn("MMark1",typeof(string)),
                              };
            mData.Columns.AddRange(cols);
            this.sfdatagrid.ItemsSource = mData;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //this.tbxRunName.Text = HardCode.HINT_NAME_DAN;
            //this.tbxRunContent.Text = HardCode.HINT_IDENTIFY_CREDIT;
        }

        public void UpdateTable()
        {
            WinMain._settingMain = SettingMain.Load<SettingMain>();
            if (WinMain._settingMain == null)
            {
                WinMain._settingMain = new SettingMain();
            }

            List<DebtsCreditCardModel> list = WinMain._DebtsCreditsTableData.GetDebtsCreditCards();

            mData.Clear();
            int index = 1;
            foreach (DebtsCreditCardModel model in list)
            {
                DataRow drow = mData.NewRow();
                drow["MNo1"] = Int32.Parse(model._MNo);
                drow["MSource1"] = model._MSource;
                drow["MPayment1"] = model._MPayment;
                drow["MBalance1"] = model._MBalance;
                drow["MInterestRate1"] = model._MInterestRate;
                if (WinMain._settingMain._CreditCardTableMarkIndex == index)
                {
                    drow["MMark1"] = "X";
                }
                else
                {
                    drow["MMark1"] = "";
                }
                mData.Rows.Add(drow);
                index++;
            }
        }

        private void sfdatagrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {

            DataTable data = this.sfdatagrid.ItemsSource as DataTable;
        }
        private void sfdatagrid_CurrentCellActivated(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellActivatedEventArgs e)
        {
            if (e.CurrentRowColumnIndex.ColumnIndex == 5)
            {

                DataTable dataTable = sfdatagrid.ItemsSource as DataTable;
                int rowIndex = e.CurrentRowColumnIndex.RowIndex;
                int irow = 1;
                WinMain._settingMain._CreditCardTableMarkIndex = rowIndex;
                WinMain._settingMain.Save();
                foreach (DataRow drow in dataTable.Rows)
                {
                    if (irow == rowIndex)
                    {
                        drow["MMark1"] = "X";
                    }
                    else
                    {
                        drow["MMark1"] = "";
                    }
                    irow = irow + 1;
                }
                sfdatagrid.View.Refresh();
            }
        }
    }
}
