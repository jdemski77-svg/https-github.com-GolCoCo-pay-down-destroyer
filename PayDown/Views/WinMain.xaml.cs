using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PayDown.Models;
using PayDown.SubViews;
using System.Threading;

namespace PayDown.Views
{
    /// <summary>
    /// Interaction logic for WinMain.xaml
    /// </summary>
    public partial class WinMain : ChromelessWindow
    {

        public static SettingMain _settingMain = null;

        // Income page
        public static IncomeRetirementTable _IncomeRetirementTableData;
        public static IncomePaychecksTable _IncomePaychecksTableData;
        public static IncomeGovermentalTable _IncomeGovermentalTableData;
        public static IncomeCheckingTable _IncomeCheckingTableData;

        // Org Debts
        public static DebtsMonthlyTable _DebtsMonthlyTableData;
        public static DebtsLivingExpTable _DebtsLivingExpTableData;
        public static DebtsLoansTable _DebtsLoansTableData;
        public static DebtsCreditsTable _DebtsCreditsTableData;


        //public static CtrlPDIdentify _ctrlIdentify;
        private CtrlPDOrgDebts _ctrlOrgDebts;
        private CtrlPDOrgIncome _ctrlOrgIncome;
        public static CtrlPDImplement _ctrlPayDown;
        private System.Timers.Timer _stimer;
        private int _InitUI = 0;

        public WinMain()
        {
            InitializeComponent();
        }

        private void LoadXMLFiles()
        {
            _settingMain = SettingMain.Load<SettingMain>();
            if (_settingMain == null)
            {
                _settingMain = new SettingMain();
            }

            // Income page
            _IncomeRetirementTableData = IncomeRetirementTable.Load<IncomeRetirementTable>();
            if (_IncomeRetirementTableData == null)
            {
                _IncomeRetirementTableData = new IncomeRetirementTable();
            }

            _IncomePaychecksTableData = IncomePaychecksTable.Load<IncomePaychecksTable>();
            if (_IncomePaychecksTableData == null)
            {
                _IncomePaychecksTableData = new IncomePaychecksTable();
            }

            _IncomeGovermentalTableData = IncomeGovermentalTable.Load<IncomeGovermentalTable>();
            if (_IncomeGovermentalTableData == null)
            {
                _IncomeGovermentalTableData = new IncomeGovermentalTable();
            }
            _IncomeCheckingTableData = IncomeCheckingTable.Load<IncomeCheckingTable>();
            if (_IncomeCheckingTableData == null)
            {
                _IncomeCheckingTableData = new IncomeCheckingTable();
            }

            // Org Debts
            _DebtsMonthlyTableData = DebtsMonthlyTable.Load<DebtsMonthlyTable>();
            if (_DebtsMonthlyTableData == null)
            {
                _DebtsMonthlyTableData = new DebtsMonthlyTable();
            }

            _DebtsLivingExpTableData = DebtsLivingExpTable.Load<DebtsLivingExpTable>();
            if (_DebtsLivingExpTableData == null)
            {
                _DebtsLivingExpTableData = new DebtsLivingExpTable();
            }

            _DebtsLoansTableData = DebtsLoansTable.Load<DebtsLoansTable>();
            if (_DebtsLoansTableData == null)
            {
                _DebtsLoansTableData = new DebtsLoansTable();
            }

            _DebtsCreditsTableData = DebtsCreditsTable.Load<DebtsCreditsTable>();
            if (_DebtsCreditsTableData == null)
            {
                _DebtsCreditsTableData = new DebtsCreditsTable();
            }
        }

        private void ChromelessWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadXMLFiles();
            RunTimerForPCDateTime();
            this.TabControl.Items.Add(getTabItemExtWithOrgIncome());
            this.TabControl.Items.Add(getTabItemExtWithOrganizeDebts());
            //this.TabControl.Items.Add(getTabItemExtWithIdentify());
            this.TabControl.Items.Add(getTabItemExtWithPayDown());
            this.TabControl.SelectedIndex = 0;
        }

        private void ChromelessWindow_Closed(object sender, EventArgs e)
        {

        }

        public void RunTimerForPCDateTime()
        {
            _stimer = new System.Timers.Timer();
            _stimer.Elapsed += new ElapsedEventHandler(OnPCTimeUpdate);
            _stimer.Interval = 1000;
            _stimer.Enabled = true;
            _stimer.Start();
        }

        private void OnPCTimeUpdate(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                this.lblPCDateTime.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            });
        }

        private TabItemExt getTabItemExtWithOrgIncome()
        {
            _ctrlOrgIncome = new CtrlPDOrgIncome();
            TabItemExt tabItem1 = new TabItemExt();
            tabItem1.FontWeight = FontWeights.Bold;
            tabItem1.FontSize = 14;
            tabItem1.HeaderMargin = new Thickness(24,4,4,4);
            tabItem1.Header = "ORGANIZE INCOME";
            tabItem1.Width = 220;
            tabItem1.Height = 36;
            tabItem1.HorizontalAlignment = HorizontalAlignment.Center;
            tabItem1.CloseButtonState = Visibility.Hidden;
            tabItem1.CanClose = false;
            tabItem1.Cursor = Cursors.Hand;
            tabItem1.Content = _ctrlOrgIncome;
            return tabItem1;
        }

        //private TabItemExt getTabItemExtWithIdentify()
        //{
        //    _ctrlIdentify = new CtrlPDIdentify();
        //    TabItemExt tabItem1 = new TabItemExt();
        //    tabItem1.Header = "IDENTIFY";
        //    tabItem1.Width = 220;
        //    tabItem1.HorizontalAlignment = HorizontalAlignment.Center;
        //    tabItem1.FontWeight = FontWeights.Bold;
        //    tabItem1.FontSize = 14;
        //    tabItem1.CloseButtonState = Visibility.Hidden;
        //    tabItem1.CanClose = false;
        //    tabItem1.AllowPin = true;
        //    tabItem1.AllowDrop = true;
        //    tabItem1.Cursor = Cursors.Hand;
        //    tabItem1.Content = _ctrlIdentify;
        //    return tabItem1;
        //}

        private TabItemExt getTabItemExtWithOrganizeDebts()
        {
            _ctrlOrgDebts = new CtrlPDOrgDebts();
            TabItemExt tabItem1 = new TabItemExt();
            tabItem1.FontWeight = FontWeights.Bold;
            tabItem1.FontSize = 14;
            tabItem1.HorizontalAlignment = HorizontalAlignment.Center;
            tabItem1.Header = "ORGANIZE DEBTS";
            tabItem1.Width = 220;
            tabItem1.Height = 36;
            tabItem1.CloseButtonState = Visibility.Hidden;
            tabItem1.CanClose = false;
            tabItem1.Cursor = Cursors.Hand;
            tabItem1.Content = _ctrlOrgDebts;
            return tabItem1;
        }

        private TabItemExt getTabItemExtWithPayDown()
        {
            _ctrlPayDown = new CtrlPDImplement();
            TabItemExt tabItem1 = new TabItemExt();
            tabItem1.Header = "IDENTIFY & IMPLEMENT";
            tabItem1.Width = 220;
            tabItem1.FontWeight = FontWeights.Bold;
            tabItem1.FontSize = 14;
            tabItem1.HorizontalAlignment = HorizontalAlignment.Center;
            tabItem1.CloseButtonState = Visibility.Hidden;
            tabItem1.CanClose = false;
            tabItem1.AllowPin = true;
            tabItem1.AllowDrop = true;
            tabItem1.Cursor = Cursors.Hand;
            tabItem1.Content = _ctrlPayDown;
            return tabItem1;
        }
        private void tbtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ttbxChartName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TabControl_SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (TabItemExt tab in TabControl.Items)
            {
                //string strHeader = tab.Header as string;
                //if (strHeader == "DEFAULT")
                //{
                //    tab.Background = Brushes.Magenta;
                //    tab.Foreground = HardCode._ForeColorActivatedTab;

                //}
                //else
                //{
                //    if (tab.IsSelected)
                //    {
                //        tab.Background = Brushes.DarkGoldenrod;
                //        tab.Foreground = HardCode._ForeColorActivatedTab;
                //    }
                //    else
                //    {
                //        tab.ClearValue(TabItemExt.BackgroundProperty);
                //        tab.ClearValue(TabItemExt.ForegroundProperty);
                //    }
                //}

                if (tab.IsSelected)
                {
                    string strHeader = tab.Header as string;
                    if (strHeader == "ORGANIZE DEBTS")
                    {
                        tab.Background = HardCode._DarkRedColorActivatedTab;


                    }
                    else if (strHeader == "IDENTIFY & IMPLEMENT")
                    {
                        tab.Background = HardCode._DarkMargentaColorActivatedTab;
                    }
                    else
                    {
                        tab.Background = HardCode._DarkGreenColorActivatedTab;
                    }
                    tab.Foreground = HardCode._ForeColorActivatedTab;
                }
                else
                {
                    tab.ClearValue(TabItemExt.BackgroundProperty);
                    tab.ClearValue(TabItemExt.ForegroundProperty);
                }

            }
        }

        private void ChromelessWindow_Closing(object sender, CancelEventArgs e)
        {
            _settingMain.Save();
            if (_stimer != null) _stimer.Stop();
        }
    }

}
