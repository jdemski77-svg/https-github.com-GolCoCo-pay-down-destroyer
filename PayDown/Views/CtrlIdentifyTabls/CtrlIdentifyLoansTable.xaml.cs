using System;
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
    public partial class CtrlIdentifyLoansTable : UserControl
    {
        private DataTable mData;
        public CtrlIdentifyLoansTable()
        {
            InitializeComponent();

            mData = new DataTable();
            DataColumn[] cols ={
                                  new DataColumn("MNo1",typeof(int)),
                                  new DataColumn("MSource1",typeof(string)),
                                  new DataColumn("MPayment1",typeof(double)),
                                  new DataColumn("MBalance1",typeof(double)),
                                  new DataColumn("MOutstandInterest1",typeof(double)),

                              };
            mData.Columns.AddRange(cols);
            this.sfdatagrid.ItemsSource = mData;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        public void UpdateTable()
        {

            List<DebtsLoanModel> list = WinMain._DebtsLoansTableData.GetDebtsLoans();
            mData.Clear();
            foreach (DebtsLoanModel model in list)
            {
                DataRow drow = mData.NewRow();
                drow["MNo1"] = Int32.Parse(model._MNo);
                drow["MSource1"] = model._MSource;
                drow["MPayment1"] = model._MPayment;
                drow["MBalance1"] = model._MBalance;
                drow["MOutstandInterest1"] = model._MOutInterest;
                mData.Rows.Add(drow);
            }
        }

        private void sfdatagrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {

            DataTable data = this.sfdatagrid.ItemsSource as DataTable;
        }

        
    }
}
