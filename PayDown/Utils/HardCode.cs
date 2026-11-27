using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Syncfusion.Windows.Controls.Grid;

namespace PayDown
{
    public class HardCode
    {
        // supabse infor
        public static string SUPA_URL = "https://xfospiidchxnhsjfjlpk.supabase.co";
        public static string SUPA_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Inhmb3NwaWlkY2h4bmhzamZqbHBrIiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTgxOTA0NDQsImV4cCI6MjAxMzc2NjQ0NH0.ypY0J59TW8MDXoztQ7I0k-2B6OVId-3Vo7x_xwDFglM";

        public static SolidColorBrush _ForeColorActivatedTab = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        public static SolidColorBrush _DarkGreenColorActivatedTab = new SolidColorBrush(Colors.DarkGreen);
        public static SolidColorBrush _DarkMargentaColorActivatedTab = new SolidColorBrush(Colors.DarkMagenta);
        public static SolidColorBrush _DarkRedColorActivatedTab = new SolidColorBrush(Colors.DarkRed);

        // Initial Status 0 Cascade, 1 Vertical 2, Horizontal Arrangment for Sri Chart
        public static int LayoutState = 2;
        public static int ROWS = 12;

        public static int SCROLL_SPEED = 60;
        public static string STR_SAVE_FOLDER_PATH = "C:\\Users";

        public static string HINT_NAME_DAN = "Dan: ";
        public static string HINT_NAME_JEN = "Jen: ";
        public static string HINT_INCOME = "If you run out of available entries in a section, combine two or more separate Source and Amount entries.The Source names do not have to be exact, the Source names are to help you keep everything organized after moving to Step 2: Identify. ";
        public static string HINT_INCOME_CHECK = "Only put long-term savings and checking balances that remain in your accounts each month, after bills and expenses are paid. In nearly every case, it saves you more money using your available cash to \'Pay Down\' your debt faster. We know the rules of thumb to keep so many months worth of bills saved in case of emergency. You will be able to compare the savings for yourself and decide what you are comfortable with choosing on the \'mplement\' sheet.";
        public static string HINT_INCOME_RETIRE = "Include any and all retirement and investment income that you receive each month or year. For a single annual disbursement, divide the after-tax total by 12 and enter that value here. If your investment income varies, you can decide to take a monthly average or make your own projection. Be conservative here to reduce the amount of adjustments you have to make later.";
        public static string HINT_INCOME_GOVER = "Include all of the government benefits you receive each month in this section - Social Security, SSI, Veteran Benefits, Unemployment, GAP etc.";
        public static string HINT_INCOME_PAYCHECK = "List any paychecks you receive on a consistent basis. The \'Pay Down\': strategy works on a monthly payment plan so you should list all of the paychecks you normally receive each month.";
        public static string HINT_RESULT = "";

        public static string HINT_DEBTS_MONTHLY = "Include every single monthly bill related to keeping and maintaining wherever you call home.\nRent (not Mortgage-that goes under the Loan section), Gas, Electric, Water, Garbage, Parking, Satelite, Cable, Cell Phones, Amazon Prime, Netfilx, HBO, Country Club, VFW/Eagles/Gym Membership, Professoinal Memberships, etc\n There are many more than the examples I listed here. Look over your monthly bank statements (online or print) and include them all here,-big and small. And I do mean...EVERY MONTHLY BILL.";
        public static string HINT_DEBTS_LIVING = "This section is for your monthly expenses that vary each month. Gas, Groceries, Movies, Dinners, Lunches, Coffees, Space Tourism, Bar Tab, Hot Yoga, Massages, Axe Throwing, Riding Lessons, Prescriptions, Kid's Sports/Activities, etc. Take at least a 3 month average of these expenses - 6 months would be bette\n. Look at your debit & credit card transactions online banking, or your monthly mailed statements.";
        public static string HINT_DEBTS = "This \'Organize Debts\' sheet is your worksheet. You will change or delete any credit cards, loans, or balances that you payoff entirely or partially on this sheet and it will autofill on the \'Implement\' sheet.";
        public static string HINT_DEBTS_LOANS = "Put all of your outstanding loans here: Student Loans, Car Loans, Personal Loans, Mortgages, etc. Only include loans from family or friends if they require monthly payments.";

        public static string HINT_IDENTIFY = "You want to free up the greatest possible cashflow to paydown your debts the fastest.\nWe recommend that you start by choosing the RLOC with the largest monthly payment as your \'Debt Destroyer\'. Then work the \'Pay Down\' strategy until your \'Debt Destroyer\' balance is low enough to pay off your next largest RLOC monthly payment until you have paid off all of your RLOC debt. \n\nIf you choose to use a new RLOC, PLOC, HELOC, or BLOC to \'Pay Down\' your debt, enter it on the \'Organize Debts\' sheet and select it here as your \'Debt Destroyer\'. \r\n\r\nThis is an important step for you to perform. But don't worry, we will walk you through it.\r\nUse the \'Organize Debts\' sheet as your worksheet to combine any RLOC balances where you are using a convenience check from another Credit Card, or another LOC, to pay off any of your other listed debts. This process, and all of the steps, are explained thoroughly and with examples in the course video explaining how to use the speadsheets.";
        public static string HINT_IDENTIFY_CREDIT = "There can be only one. Mark an 'X' for the \'Debt Destroyer\' of your choice. Your choice will automatically be entered into the \'Implement sheet\'.";
        public static string HINT_IDENTIFY_LOANS = "After you PayDown your higher interest LOC's you will then \'Pay Down\' any loan debts you have. \nYou want to save the most money on interest as you possibly can. We recommend you start with your higher interest loans with the most monthly payments left before pay-off.";

        // Table Names
        public static string TABLE_INCOME_PAYCHECK = "IncomePaycheck";
        public static string TABLE_INCOME_RETIRE = "IncomeRetirement";
        public static string TABLE_INCOME_GOVER = "IncomeGovermental";
        public static string TABLE_INCOME_CHECK= "IncomeChecking";

        public static string TABLE_DEBTS_MONTHLY_BILLS = "DebtsMonthyBills";
        public static string TABLE_DEBTS_LIVING_EXP = "DebtsLivingExpenses";
        public static string TABLE_DEBTS_LOANS= "DebtsLoans";
        public static string TABLE_DEBTS_CREDITCARD = "DebtsCreditCard";

        // Names
        public static string CTRL_INCOME_PAYCHECK = "Paychecks";
        public static string CTRL_INCOME_RETIRE = "Retirement And Investment Income";
        public static string CTRL_INCOME_GOVER = "Governmental Benefits";
        public static string CTRL_INCOME_CHECK = "Checking And Savings Account Balances";

        public static string CTRL_DEBTS_MONTHLY_BILLING= "Monthly Household Billls";
        public static string CTRL_DEBTS_LIVING_EXP = "Living Expenses";

        public static string CTRL_DEBTS_LOANS = "Loans";
        public static string CTRL_DEBTS_CREDITCARDS= "Credit Cards And Lines of Credit";
        
    }
}
