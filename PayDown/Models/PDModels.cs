using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Syncfusion.Windows.Shared;

namespace PayDown.Models
{
    // Income
    public class IncomeCheckingTable : SettingsBase
    {
        public string _Source1 { get; set; }
        public double _Amount1 { get; set; }
        public string _Source2 { get; set; }
        public double _Amount2 { get; set; }
        public string _Source3 { get; set; }
        public double _Amount3 { get; set; }
        public string _Source4 { get; set; }
        public double _Amount4 { get; set; }
        public string _Source5 { get; set; }
        public double _Amount5 { get; set; }
        public string _Source6 { get; set; }
        public double _Amount6 { get; set; }
        public string _Source7 { get; set; }
        public double _Amount7 { get; set; }
        public string _Source8 { get; set; }
        public double _Amount8 { get; set; }
        public string _Source9 { get; set; }
        public double _Amount9 { get; set; }
        public string _Source10 { get; set; }
        public double _Amount10 { get; set; }
        public string _Source11 { get; set; }
        public double _Amount11 { get; set; }
        public string _Source12 { get; set; }
        public double _Amount12 { get; set; }
        public IncomeCheckingTable()
        {
            _Source1 = "";
            _Amount1 = 0.0;
            _Source2 = "";
            _Amount2 = 0.0;
            _Source3 = "";
            _Amount3 = 0.0;
            _Source4 = "";
            _Amount4 = 0.0;
            _Source5 = "";
            _Amount5 = 0.0;
            _Source6 = "";
            _Amount6 = 0.0;
            _Source7 = "";
            _Amount7 = 0.0;
            _Source8 = "";
            _Amount8 = 0.0;
            _Source9 = "";
            _Amount9 = 0.0;
            _Source10 = "";
            _Amount10 = 0.0;
            _Source11 = "";
            _Amount11 = 0.0;
            _Source12 = "";
            _Amount12 = 0.0;
        }
        public double GetTotalAmount()
        {
            double total = 0;
            total = _Amount1
                + _Amount2
                + _Amount3
                + _Amount4
                + _Amount5
                + _Amount6
                + _Amount7
                + _Amount8
                + _Amount9
                + _Amount10
                + _Amount11
                + _Amount12;
            return total;
        }
    }

    public class IncomeGovermentalTable : SettingsBase
    {
        public string _Source1 { get; set; }
        public double _Amount1 { get; set; }
        public string _Source2 { get; set; }
        public double _Amount2 { get; set; }
        public string _Source3 { get; set; }
        public double _Amount3 { get; set; }
        public string _Source4 { get; set; }
        public double _Amount4 { get; set; }
        public string _Source5 { get; set; }
        public double _Amount5 { get; set; }
        public string _Source6 { get; set; }
        public double _Amount6 { get; set; }
        public string _Source7 { get; set; }
        public double _Amount7 { get; set; }
        public string _Source8 { get; set; }
        public double _Amount8 { get; set; }
        public string _Source9 { get; set; }
        public double _Amount9 { get; set; }
        public string _Source10 { get; set; }
        public double _Amount10 { get; set; }
        public string _Source11 { get; set; }
        public double _Amount11 { get; set; }
        public string _Source12 { get; set; }
        public double _Amount12 { get; set; }
        public IncomeGovermentalTable()
        {
            _Source1 = "";
            _Amount1 = 0.0;
            _Source2 = "";
            _Amount2 = 0.0;
            _Source3 = "";
            _Amount3 = 0.0;
            _Source4 = "";
            _Amount4 = 0.0;
            _Source5 = "";
            _Amount5 = 0.0;
            _Source6 = "";
            _Amount6 = 0.0;
            _Source7 = "";
            _Amount7 = 0.0;
            _Source8 = "";
            _Amount8 = 0.0;
            _Source9 = "";
            _Amount9 = 0.0;
            _Source10 = "";
            _Amount10 = 0.0;
            _Source11 = "";
            _Amount11 = 0.0;
            _Source12 = "";
            _Amount12 = 0.0;
        }
        public double GetTotalAmount()
        {
            double total = 0;
            total = _Amount1
                + _Amount2
                + _Amount3
                + _Amount4
                + _Amount5
                + _Amount6
                + _Amount7
                + _Amount8
                + _Amount9
                + _Amount10
                + _Amount11
                + _Amount12;
            return total;
        }
    }

    public class IncomePaychecksTable : SettingsBase
    {
        public string _Source1 { get; set; }
        public double _Amount1 { get; set; }
        public string _Source2 { get; set; }
        public double _Amount2 { get; set; }
        public string _Source3 { get; set; }
        public double _Amount3 { get; set; }
        public string _Source4 { get; set; }
        public double _Amount4 { get; set; }
        public string _Source5 { get; set; }
        public double _Amount5 { get; set; }
        public string _Source6 { get; set; }
        public double _Amount6 { get; set; }
        public string _Source7 { get; set; }
        public double _Amount7 { get; set; }
        public string _Source8 { get; set; }
        public double _Amount8 { get; set; }
        public string _Source9 { get; set; }
        public double _Amount9 { get; set; }
        public string _Source10 { get; set; }
        public double _Amount10 { get; set; }
        public string _Source11 { get; set; }
        public double _Amount11 { get; set; }
        public string _Source12 { get; set; }
        public double _Amount12 { get; set; }
        public IncomePaychecksTable()
        {
            _Source1 = "";
            _Amount1 = 0.0;
            _Source2 = "";
            _Amount2 = 0.0;
            _Source3 = "";
            _Amount3 = 0.0;
            _Source4 = "";
            _Amount4 = 0.0;
            _Source5 = "";
            _Amount5 = 0.0;
            _Source6 = "";
            _Amount6 = 0.0;
            _Source7 = "";
            _Amount7 = 0.0;
            _Source8 = "";
            _Amount8 = 0.0;
            _Source9 = "";
            _Amount9 = 0.0;
            _Source10 = "";
            _Amount10 = 0.0;
            _Source11 = "";
            _Amount11 = 0.0;
            _Source12 = "";
            _Amount12 = 0.0;
        }
        public double GetTotalAmount()
        {
            double total = 0;
            total = _Amount1
                + _Amount2
                + _Amount3
                + _Amount4
                + _Amount5
                + _Amount6
                + _Amount7
                + _Amount8
                + _Amount9
                + _Amount10
                + _Amount11
                + _Amount12;
            return total;
        }
    }

    public class IncomeRetirementTable : SettingsBase
    {
        public string _Source1 { get; set; }
        public double _Amount1 { get; set; }
        public string _Source2 { get; set; }
        public double _Amount2 { get; set; }
        public string _Source3 { get; set; }
        public double _Amount3 { get; set; }
        public string _Source4 { get; set; }
        public double _Amount4 { get; set; }
        public string _Source5 { get; set; }
        public double _Amount5 { get; set; }
        public string _Source6 { get; set; }
        public double _Amount6 { get; set; }
        public string _Source7 { get; set; }
        public double _Amount7 { get; set; }
        public string _Source8 { get; set; }
        public double _Amount8 { get; set; }
        public string _Source9 { get; set; }
        public double _Amount9 { get; set; }
        public string _Source10 { get; set; }
        public double _Amount10 { get; set; }
        public string _Source11 { get; set; }
        public double _Amount11 { get; set; }
        public string _Source12 { get; set; }
        public double _Amount12 { get; set; }
        public IncomeRetirementTable()
        {
            _Source1 = "";
            _Amount1 = 0.0;
            _Source2 = "";
            _Amount2 = 0.0;
            _Source3 = "";
            _Amount3 = 0.0;
            _Source4 = "";
            _Amount4 = 0.0;
            _Source5 = "";
            _Amount5 = 0.0;
            _Source6 = "";
            _Amount6 = 0.0;
            _Source7 = "";
            _Amount7 = 0.0;
            _Source8 = "";
            _Amount8 = 0.0;
            _Source9 = "";
            _Amount9 = 0.0;
            _Source10 = "";
            _Amount10 = 0.0;
            _Source11 = "";
            _Amount11 = 0.0;
            _Source12 = "";
            _Amount12 = 0.0;
        }

        public double GetTotalAmount()
        {
            double total = 0;
            total = _Amount1
                + _Amount2
                + _Amount3
                + _Amount4
                + _Amount5
                + _Amount6
                + _Amount7
                + _Amount8
                + _Amount9
                + _Amount10
                + _Amount11
                + _Amount12;
            return total;
        }
    }

    // Debts
    public class DebtsLivingExpTable : SettingsBase
    {
        public string _CYN1 { get; set; }
        public string _Source1 { get; set; }
        public double _Amount1 { get; set; }

        public string _CYN2 { get; set; }
        public string _Source2 { get; set; }
        public double _Amount2 { get; set; }

        public string _CYN3 { get; set; }
        public string _Source3 { get; set; }
        public double _Amount3 { get; set; }

        public string _CYN4 { get; set; }
        public string _Source4 { get; set; }
        public double _Amount4 { get; set; }

        public string _CYN5 { get; set; }
        public string _Source5 { get; set; }
        public double _Amount5 { get; set; }

        public string _CYN6 { get; set; }
        public string _Source6 { get; set; }
        public double _Amount6 { get; set; }

        public string _CYN7 { get; set; }
        public string _Source7 { get; set; }
        public double _Amount7 { get; set; }

        public string _CYN8 { get; set; }
        public string _Source8 { get; set; }
        public double _Amount8 { get; set; }

        public string _CYN9 { get; set; }
        public string _Source9 { get; set; }
        public double _Amount9 { get; set; }

        public string _CYN10 { get; set; }
        public string _Source10 { get; set; }
        public double _Amount10 { get; set; }

        public string _CYN11 { get; set; }
        public string _Source11 { get; set; }
        public double _Amount11 { get; set; }
        public string _CYN12 { get; set; }
        public string _Source12 { get; set; }
        public double _Amount12 { get; set; }

        public string _CYN13 { get; set; }
        public string _Source13 { get; set; }
        public double _Amount13 { get; set; }

        public string _CYN14 { get; set; }
        public string _Source14 { get; set; }
        public double _Amount14 { get; set; }

        public string _CYN15 { get; set; }
        public string _Source15 { get; set; }
        public double _Amount15 { get; set; }


        public DebtsLivingExpTable()
        {
            _CYN1 = "NO";
            _Source1 = "";
            _Amount1 = 0.0;

            _CYN2 = "NO";
            _Source2 = "";
            _Amount2 = 0.0;

            _CYN3 = "NO";
            _Source3 = "";
            _Amount3 = 0.0;

            _CYN4 = "NO";
            _Source4 = "";
            _Amount4 = 0.0;

            _CYN5 = "NO";
            _Source5 = "";
            _Amount5 = 0.0;

            _CYN6 = "NO";
            _Source6 = "";
            _Amount6 = 0.0;

            _CYN7 = "NO";
            _Source7 = "";
            _Amount7 = 0.0;

            _CYN8 = "NO";
            _Source8 = "";
            _Amount8 = 0.0;

            _CYN9 = "NO";
            _Source9 = "";
            _Amount9 = 0.0;

            _CYN10 = "NO";
            _Source10 = "";
            _Amount10 = 0.0;

            _CYN11 = "NO";
            _Source11 = "";
            _Amount11 = 0.0;

            _CYN12 = "NO";
            _Source12 = "";
            _Amount12 = 0.0;

            _CYN13 = "NO";
            _Source13 = "";
            _Amount13 = 0.0;

            _CYN14 = "NO";
            _Source14 = "";
            _Amount14 = 0.0;

            _CYN15 = "NO";
            _Source15 = "";
            _Amount15 = 0.0;
        }

        public double GetChargeableValue()
        {
            double total = 0.0;
            if (_CYN1 == "YES")
            {
                total = total + _Amount1;
            }
            if (_CYN2 == "YES")
            {
                total = total + _Amount2;
            }
            if (_CYN3 == "YES")
            {
                total = total + _Amount3;
            }
            if (_CYN4 == "YES")
            {
                total = total + _Amount4;
            }
            if (_CYN5 == "YES")
            {
                total = total + _Amount5;
            }
            if (_CYN6 == "YES")
            {
                total = total + _Amount6;
            }
            if (_CYN7 == "YES")
            {
                total = total + _Amount7;
            }
            if (_CYN8 == "YES")
            {
                total = total + _Amount8;
            }
            if (_CYN9 == "YES")
            {
                total = total + _Amount9;
            }
            if (_CYN10 == "YES")
            {
                total = total + _Amount10;
            }
            if (_CYN11 == "YES")
            {
                total = total + _Amount11;
            }
            if (_CYN12 == "YES")
            {
                total = total + _Amount12;
            }
            if (_CYN13 == "YES")
            {
                total = total + _Amount13;
            }
            if (_CYN14 == "YES")
            {
                total = total + _Amount14;
            }
            if (_CYN15 == "YES")
            {
                total = total + _Amount15;
            }
            return total;
        }

        public double GetNonChargeableValue()
        {
            double total = 0.0;
            if (_CYN1 != "YES")
            {
                total = total + _Amount1;
            }
            if (_CYN2 != "YES")
            {
                total = total + _Amount2;
            }
            if (_CYN3 != "YES")
            {
                total = total + _Amount3;
            }
            if (_CYN4 != "YES")
            {
                total = total + _Amount4;
            }
            if (_CYN5 != "YES")
            {
                total = total + _Amount5;
            }
            if (_CYN6 != "YES")
            {
                total = total + _Amount6;
            }
            if (_CYN7 != "YES")
            {
                total = total + _Amount7;
            }
            if (_CYN8 != "YES")
            {
                total = total + _Amount8;
            }
            if (_CYN9 != "YES")
            {
                total = total + _Amount9;
            }
            if (_CYN10 != "YES")
            {
                total = total + _Amount10;
            }
            if (_CYN11 != "YES")
            {
                total = total + _Amount11;
            }
            if (_CYN12 != "YES")
            {
                total = total + _Amount12;
            }
            if (_CYN13 != "YES")
            {
                total = total + _Amount13;
            }
            if (_CYN14 != "YES")
            {
                total = total + _Amount14;
            }
            if (_CYN15 != "YES")
            {
                total = total + _Amount15;
            }
            return total;
        }
    }

    public class DebtsMonthlyTable : SettingsBase
    {
        public string _CYN1 { get; set; }
        public string _Source1 { get; set; }
        public double _Amount1 { get; set; }

        public string _CYN2 { get; set; }
        public string _Source2 { get; set; }
        public double _Amount2 { get; set; }

        public string _CYN3 { get; set; }
        public string _Source3 { get; set; }
        public double _Amount3 { get; set; }

        public string _CYN4 { get; set; }
        public string _Source4 { get; set; }
        public double _Amount4 { get; set; }

        public string _CYN5 { get; set; }
        public string _Source5 { get; set; }
        public double _Amount5 { get; set; }

        public string _CYN6 { get; set; }
        public string _Source6 { get; set; }
        public double _Amount6 { get; set; }

        public string _CYN7 { get; set; }
        public string _Source7 { get; set; }
        public double _Amount7 { get; set; }

        public string _CYN8 { get; set; }
        public string _Source8 { get; set; }
        public double _Amount8 { get; set; }

        public string _CYN9 { get; set; }
        public string _Source9 { get; set; }
        public double _Amount9 { get; set; }

        public string _CYN10 { get; set; }
        public string _Source10 { get; set; }
        public double _Amount10 { get; set; }

        public string _CYN11 { get; set; }
        public string _Source11 { get; set; }
        public double _Amount11 { get; set; }
        public string _CYN12 { get; set; }
        public string _Source12 { get; set; }
        public double _Amount12 { get; set; }

        public string _CYN13 { get; set; }
        public string _Source13 { get; set; }
        public double _Amount13 { get; set; }

        public string _CYN14 { get; set; }
        public string _Source14 { get; set; }
        public double _Amount14 { get; set; }

        public string _CYN15 { get; set; }
        public string _Source15 { get; set; }
        public double _Amount15 { get; set; }


        public DebtsMonthlyTable()
        {
            _CYN1 = "NO";
            _Source1 = "";
            _Amount1 = 0.0;

            _CYN2 = "NO";
            _Source2 = "";
            _Amount2 = 0.0;

            _CYN3 = "NO";
            _Source3 = "";
            _Amount3 = 0.0;

            _CYN4 = "NO";
            _Source4 = "";
            _Amount4 = 0.0;

            _CYN5 = "NO";
            _Source5 = "";
            _Amount5 = 0.0;

            _CYN6 = "NO";
            _Source6 = "";
            _Amount6 = 0.0;

            _CYN7 = "NO";
            _Source7 = "";
            _Amount7 = 0.0;

            _CYN8 = "NO";
            _Source8 = "";
            _Amount8 = 0.0;

            _CYN9 = "NO";
            _Source9 = "";
            _Amount9 = 0.0;

            _CYN10 = "NO";
            _Source10 = "";
            _Amount10 = 0.0;

            _CYN11 = "NO";
            _Source11 = "";
            _Amount11 = 0.0;

            _CYN12 = "NO";
            _Source12 = "";
            _Amount12 = 0.0;

            _CYN13 = "NO";
            _Source13 = "";
            _Amount13 = 0.0;

            _CYN14 = "NO";
            _Source14 = "";
            _Amount14 = 0.0;

            _CYN15 = "NO";
            _Source15 = "";
            _Amount15 = 0.0;
        }

        public double GetChargeableValue()
        {
            double total = 0.0;
            if (_CYN1 == "YES")
            {
                total = total + _Amount1;
            }
            if (_CYN2 == "YES")
            {
                total = total + _Amount2;
            }
            if (_CYN3 == "YES")
            {
                total = total + _Amount3;
            }
            if (_CYN4 == "YES")
            {
                total = total + _Amount4;
            }
            if (_CYN5 == "YES")
            {
                total = total + _Amount5;
            }
            if (_CYN6 == "YES")
            {
                total = total + _Amount6;
            }
            if (_CYN7 == "YES")
            {
                total = total + _Amount7;
            }
            if (_CYN8 == "YES")
            {
                total = total + _Amount8;
            }
            if (_CYN9 == "YES")
            {
                total = total + _Amount9;
            }
            if (_CYN10 == "YES")
            {
                total = total + _Amount10;
            }
            if (_CYN11 == "YES")
            {
                total = total + _Amount11;
            }
            if (_CYN12 == "YES")
            {
                total = total + _Amount12;
            }
            if (_CYN13 == "YES")
            {
                total = total + _Amount13;
            }
            if (_CYN14 == "YES")
            {
                total = total + _Amount14;
            }
            if (_CYN15 == "YES")
            {
                total = total + _Amount15;
            }
            return total;
        }

        public double GetNonChargeableValue()
        {
            double total = 0.0;
            if (_CYN1 != "YES")
            {
                total = total + _Amount1;
            }
            if (_CYN2 != "YES")
            {
                total = total + _Amount2;
            }
            if (_CYN3 != "YES")
            {
                total = total + _Amount3;
            }
            if (_CYN4 != "YES")
            {
                total = total + _Amount4;
            }
            if (_CYN5 != "YES")
            {
                total = total + _Amount5;
            }
            if (_CYN6 != "YES")
            {
                total = total + _Amount6;
            }
            if (_CYN7 != "YES")
            {
                total = total + _Amount7;
            }
            if (_CYN8 != "YES")
            {
                total = total + _Amount8;
            }
            if (_CYN9 != "YES")
            {
                total = total + _Amount9;
            }
            if (_CYN10 != "YES")
            {
                total = total + _Amount10;
            }
            if (_CYN11 != "YES")
            {
                total = total + _Amount11;
            }
            if (_CYN12 != "YES")
            {
                total = total + _Amount12;
            }
            if (_CYN13 != "YES")
            {
                total = total + _Amount13;
            }
            if (_CYN14 != "YES")
            {
                total = total + _Amount14;
            }
            if (_CYN15 != "YES")
            {
                total = total + _Amount15;
            }

            return total;
        }
    }

    public class DebtsLoansTable : SettingsBase
    {
        public string _Source1 { get; set; }
        public double _Payment1 { get; set; }
        public double _Balance1 { get; set; }
        public double _InterestRate1 { get; set; }
        public double _OutstandInterest1 { get; set; }

        public string _Source2 { get; set; }
        public double _Payment2 { get; set; }
        public double _Balance2 { get; set; }
        public double _InterestRate2 { get; set; }
        public double _OutstandInterest2 { get; set; }

        public string _Source3 { get; set; }
        public double _Payment3 { get; set; }
        public double _Balance3 { get; set; }
        public double _InterestRate3 { get; set; }
        public double _OutstandInterest3 { get; set; }

        public string _Source4 { get; set; }
        public double _Payment4 { get; set; }
        public double _Balance4 { get; set; }
        public double _InterestRate4 { get; set; }
        public double _OutstandInterest4 { get; set; }

        public string _Source5 { get; set; }
        public double _Payment5 { get; set; }
        public double _Balance5 { get; set; }
        public double _InterestRate5 { get; set; }
        public double _OutstandInterest5 { get; set; }


        public string _Source6 { get; set; }
        public double _Payment6 { get; set; }
        public double _Balance6 { get; set; }
        public double _InterestRate6 { get; set; }
        public double _OutstandInterest6 { get; set; }

        public string _Source7 { get; set; }
        public double _Payment7 { get; set; }
        public double _Balance7 { get; set; }
        public double _InterestRate7 { get; set; }
        public double _OutstandInterest7 { get; set; }

        public string _Source8 { get; set; }
        public double _Payment8 { get; set; }
        public double _Balance8 { get; set; }
        public double _InterestRate8 { get; set; }
        public double _OutstandInterest8 { get; set; }


        public string _Source9 { get; set; }
        public double _Payment9 { get; set; }
        public double _Balance9 { get; set; }
        public double _InterestRate9 { get; set; }
        public double _OutstandInterest9 { get; set; }


        public string _Source10 { get; set; }
        public double _Payment10 { get; set; }
        public double _Balance10 { get; set; }
        public double _InterestRate10 { get; set; }
        public double _OutstandInterest10 { get; set; }

        public DebtsLoansTable()
        {
            _Source1 = "";
            _Payment1 = 0.0;
            _Balance1 = 0.0;
            _InterestRate1 = 0.0;
            _OutstandInterest1 = 0.0;

            _Source2 = "";
            _Payment2 = 0.0;
            _Balance2 = 0.0;
            _InterestRate2 = 0.0;
            _OutstandInterest2 = 0.0;

            _Source3 = "";
            _Payment3 = 0.0;
            _Balance3 = 0.0;
            _InterestRate3 = 0.0;
            _OutstandInterest3 = 0.0;

            _Source4 = "";
            _Payment4 = 0.0;
            _Balance4 = 0.0;
            _InterestRate4 = 0.0;
            _OutstandInterest4 = 0.0;

            _Source5 = "";
            _Payment5 = 0.0;
            _Balance5 = 0.0;
            _InterestRate5 = 0.0;
            _OutstandInterest5 = 0.0;

            _Source6 = "";
            _Payment6 = 0.0;
            _Balance6 = 0.0;
            _InterestRate6 = 0.0;
            _OutstandInterest6 = 0.0;

            _Source7 = "";
            _Payment7 = 0.0;
            _Balance7 = 0.0;
            _InterestRate7 = 0.0;
            _OutstandInterest7 = 0.0;

            _Source8 = "";
            _Payment8 = 0.0;
            _Balance8 = 0.0;
            _InterestRate8 = 0.0;
            _OutstandInterest8 = 0.0;

            _Source9 = "";
            _Payment9 = 0.0;
            _Balance9 = 0.0;
            _InterestRate9 = 0.0;
            _OutstandInterest9 = 0.0;

            _Source10 = "";
            _Payment10 = 0.0;
            _Balance10 = 0.0;
            _InterestRate10 = 0.0;
            _OutstandInterest10 = 0.0;
        }

        public List<DebtsLoanModel> GetDebtsLoans()
        {
            List<DebtsLoanModel> list = new List<DebtsLoanModel>();

            DebtsLoanModel icome1 = new DebtsLoanModel();
            icome1._MNo = "1";
            icome1._MSource = _Source1;
            icome1._MPayment = _Payment1;
            icome1._MBalance = _Balance1;
            icome1._MInterestRate = _InterestRate1;
            icome1._MOutInterest = _OutstandInterest1;
            list.Add(icome1);

            DebtsLoanModel icome2 = new DebtsLoanModel();
            icome2._MNo = "2";
            icome2._MSource = _Source2;
            icome2._MPayment = _Payment2;
            icome2._MBalance = _Balance2;
            icome2._MInterestRate = _InterestRate2;
            icome2._MOutInterest = _OutstandInterest2;
            list.Add(icome2);

            DebtsLoanModel icome3 = new DebtsLoanModel();
            icome3._MNo = "3";
            icome3._MSource = _Source3;
            icome3._MPayment = _Payment3;
            icome3._MBalance = _Balance3;
            icome3._MInterestRate = _InterestRate3;
            icome3._MOutInterest = _OutstandInterest3;
            list.Add(icome3);

            DebtsLoanModel icome4 = new DebtsLoanModel();
            icome4._MNo = "4";
            icome4._MSource = _Source4;
            icome4._MPayment = _Payment4;
            icome4._MBalance = _Balance4;
            icome4._MInterestRate = _InterestRate4;
            icome4._MOutInterest = _OutstandInterest4;
            list.Add(icome4);

            DebtsLoanModel icome5 = new DebtsLoanModel();
            icome5._MNo = "5";
            icome5._MSource = _Source5;
            icome5._MPayment = _Payment5;
            icome5._MBalance = _Balance5;
            icome5._MInterestRate = _InterestRate5;
            icome5._MOutInterest = _OutstandInterest5;
            list.Add(icome5);

            DebtsLoanModel icome6 = new DebtsLoanModel();
            icome6._MNo = "6";
            icome6._MSource = _Source6;
            icome6._MPayment = _Payment6;
            icome6._MBalance = _Balance6;
            icome6._MInterestRate = _InterestRate6;
            icome6._MOutInterest = _OutstandInterest6;
            list.Add(icome6);

            DebtsLoanModel icome7 = new DebtsLoanModel();
            icome7._MNo = "7";
            icome7._MSource = _Source7;
            icome7._MPayment = _Payment7;
            icome7._MBalance = _Balance7;
            icome7._MInterestRate = _InterestRate7;
            icome7._MOutInterest = _OutstandInterest7;
            list.Add(icome7);

            DebtsLoanModel icome8 = new DebtsLoanModel();
            icome8._MNo = "8";
            icome8._MSource = _Source8;
            icome8._MPayment = _Payment8;
            icome8._MBalance = _Balance8;
            icome8._MInterestRate = _InterestRate8;
            icome8._MOutInterest = _OutstandInterest8;
            list.Add(icome8);

            DebtsLoanModel icome9 = new DebtsLoanModel();
            icome9._MNo = "9";
            icome9._MSource = _Source9;
            icome9._MPayment = _Payment9;
            icome9._MBalance = _Balance9;
            icome9._MInterestRate = _InterestRate9;
            icome9._MOutInterest = _OutstandInterest9;
            list.Add(icome9);

            DebtsLoanModel icome10 = new DebtsLoanModel();
            icome10._MNo = "10";
            icome10._MSource = _Source10;
            icome10._MPayment = _Payment10;
            icome10._MBalance = _Balance10;
            icome10._MInterestRate = _InterestRate10;
            icome10._MOutInterest = _OutstandInterest10;
            list.Add(icome10);

            list.Sort(new DebtsDebtsLoanModelComparer());
            return list;
        }

        public double GetSubTotalPayment()
        {
            double total =
                _Payment1 +
                _Payment2 +
                _Payment3 +
                _Payment4 +
                _Payment5 +
                _Payment6 +
                _Payment7 +
                _Payment8 +
                _Payment9 +
                _Payment10;
            return total;
        }

        public double GetTotalBalance()
        {
            double total =
                _Balance1 +
                _Balance2 +
                _Balance3 +
                _Balance4 +
                _Balance5 +
                _Balance6 +
                _Balance7 +
                _Balance8 +
                _Balance9 +
                _Balance10;
            return total;
        }
    }

    public class DebtsCreditsTable : SettingsBase
    {
        public string _Source1 { get; set; }
        public double _Payment1 { get; set; }
        public double _Balance1 { get; set; }
        public double _InterestRate1 { get; set; }
        public double _Limit1 { get; set; }
        public string _TypeCCLOC1 { get; set; }

        public string _Source2 { get; set; }
        public double _Payment2 { get; set; }
        public double _Balance2 { get; set; }
        public double _InterestRate2 { get; set; }
        public double _Limit2 { get; set; }
        public string _TypeCCLOC2 { get; set; }

        public string _Source3 { get; set; }
        public double _Payment3 { get; set; }
        public double _Balance3 { get; set; }
        public double _InterestRate3 { get; set; }
        public double _Limit3 { get; set; }
        public string _TypeCCLOC3 { get; set; }

        public string _Source4 { get; set; }
        public double _Payment4 { get; set; }
        public double _Balance4 { get; set; }
        public double _InterestRate4 { get; set; }
        public double _Limit4 { get; set; }
        public string _TypeCCLOC4 { get; set; }

        public string _Source5 { get; set; }
        public double _Payment5 { get; set; }
        public double _Balance5 { get; set; }
        public double _InterestRate5 { get; set; }
        public double _Limit5 { get; set; }
        public string _TypeCCLOC5 { get; set; }

        public string _Source6 { get; set; }
        public double _Payment6 { get; set; }
        public double _Balance6 { get; set; }
        public double _InterestRate6 { get; set; }
        public double _Limit6 { get; set; }
        public string _TypeCCLOC6 { get; set; }

        public string _Source7 { get; set; }
        public double _Payment7 { get; set; }
        public double _Balance7 { get; set; }
        public double _InterestRate7 { get; set; }
        public double _Limit7 { get; set; }
        public string _TypeCCLOC7 { get; set; }

        public string _Source8 { get; set; }
        public double _Payment8 { get; set; }
        public double _Balance8 { get; set; }
        public double _InterestRate8 { get; set; }
        public double _Limit8 { get; set; }
        public string _TypeCCLOC8 { get; set; }


        public string _Source9 { get; set; }
        public double _Payment9 { get; set; }
        public double _Balance9 { get; set; }
        public double _InterestRate9 { get; set; }
        public double _Limit9 { get; set; }
        public string _TypeCCLOC9 { get; set; }


        public string _Source10 { get; set; }
        public double _Payment10 { get; set; }
        public double _Balance10 { get; set; }
        public double _InterestRate10 { get; set; }
        public double _Limit10 { get; set; }
        public string _TypeCCLOC10 { get; set; }

        public string _Source11 { get; set; }
        public double _Payment11 { get; set; }
        public double _Balance11 { get; set; }
        public double _InterestRate11 { get; set; }
        public double _Limit11 { get; set; }
        public string _TypeCCLOC11 { get; set; }
        public string _Source12 { get; set; }
        public double _Payment12 { get; set; }
        public double _Balance12 { get; set; }
        public double _InterestRate12 { get; set; }
        public double _Limit12 { get; set; }
        public string _TypeCCLOC12 { get; set; }

        public DebtsCreditsTable()
        {
            _Source1 = "";
            _Payment1 = 0.0;
            _Balance1 = 0.0;
            _InterestRate1 = 0.0;
            _Limit1 = 0.0;
            _TypeCCLOC1 = "CC";

            _Source2 = "";
            _Payment2 = 0.0;
            _Balance2 = 0.0;
            _InterestRate2 = 0.0;
            _Limit2 = 0.0;
            _TypeCCLOC2 = "CC";

            _Source3 = "";
            _Payment3 = 0.0;
            _Balance3 = 0.0;
            _InterestRate3 = 0.0;
            _Limit3 = 0.0;
            _TypeCCLOC3 = "CC";

            _Source4 = "";
            _Payment4 = 0.0;
            _Balance4 = 0.0;
            _InterestRate4 = 0.0;
            _Limit4 = 0.0;
            _TypeCCLOC4 = "CC";

            _Source5 = "";
            _Payment5 = 0.0;
            _Balance5 = 0.0;
            _InterestRate5 = 0.0;
            _Limit5 = 0.0;
            _TypeCCLOC5 = "CC";

            _Source6 = "";
            _Payment6 = 0.0;
            _Balance6 = 0.0;
            _InterestRate6 = 0.0;
            _Limit6 = 0.0;
            _TypeCCLOC6 = "CC";

            _Source7 = "";
            _Payment7 = 0.0;
            _Balance7 = 0.0;
            _InterestRate7 = 0.0;
            _Limit7 = 0.0;
            _TypeCCLOC7 = "CC";

            _Source8 = "";
            _Payment8 = 0.0;
            _Balance8 = 0.0;
            _InterestRate8 = 0.0;
            _Limit8 = 0.0;
            _TypeCCLOC8 = "CC";

            _Source9 = "";
            _Payment9 = 0.0;
            _Balance9 = 0.0;
            _InterestRate9 = 0.0;
            _Limit9 = 0.0;
            _TypeCCLOC9 = "CC";

            _Source10 = "";
            _Payment10 = 0.0;
            _Balance10 = 0.0;
            _InterestRate10 = 0.0;
            _Limit10 = 0.0;
            _TypeCCLOC10 = "CC";

            _Source11 = "";
            _Payment11 = 0.0;
            _Balance11 = 0.0;
            _InterestRate11 = 0.0;
            _Limit11 = 0.0;
            _TypeCCLOC11 = "CC";

            _Source12 = "";
            _Payment12 = 0.0;
            _Balance12 = 0.0;
            _InterestRate12 = 0.0;
            _Limit12 = 0.0;
            _TypeCCLOC12 = "CC";
        }

        public List<DebtsCreditCardModel> GetDebtsCreditCards()
        {

            List<DebtsCreditCardModel> list = new List<DebtsCreditCardModel>();

            DebtsCreditCardModel icome1 = new DebtsCreditCardModel();
            icome1._MNo = $"1";
            icome1._MSource = _Source1;
            icome1._MPayment = _Payment1;
            icome1._MBalance = _Balance1;
            icome1._MInterestRate = _InterestRate1;
            icome1._MLimit = _Limit1;
            icome1._MType = _TypeCCLOC1;
            list.Add(icome1);

            DebtsCreditCardModel icome2 = new DebtsCreditCardModel();
            icome2._MNo = $"2";
            icome2._MSource = _Source2;
            icome2._MPayment = _Payment2;
            icome2._MBalance = _Balance2;
            icome2._MInterestRate = _InterestRate2;
            icome2._MLimit = _Limit2;
            icome2._MType = _TypeCCLOC2;
            list.Add(icome2);

            DebtsCreditCardModel icome3 = new DebtsCreditCardModel();
            icome3._MNo = $"3";
            icome3._MSource = _Source3;
            icome3._MPayment = _Payment3;
            icome3._MBalance = _Balance3;
            icome3._MInterestRate = _InterestRate3;
            icome3._MLimit = _Limit3;
            icome3._MType = _TypeCCLOC3;
            list.Add(icome3);

            DebtsCreditCardModel icome4 = new DebtsCreditCardModel();
            icome4._MNo = $"4";
            icome4._MSource = _Source4;
            icome4._MPayment = _Payment4;
            icome4._MBalance = _Balance4;
            icome4._MInterestRate = _InterestRate4;
            icome4._MLimit = _Limit4;
            icome4._MType = _TypeCCLOC4;
            list.Add(icome4);

            DebtsCreditCardModel icome5 = new DebtsCreditCardModel();
            icome5._MNo = $"5";
            icome5._MSource = _Source5;
            icome5._MPayment = _Payment5;
            icome5._MBalance = _Balance5;
            icome5._MInterestRate = _InterestRate5;
            icome5._MLimit = _Limit5;
            icome5._MType = _TypeCCLOC5;
            list.Add(icome5);

            DebtsCreditCardModel icome6 = new DebtsCreditCardModel();
            icome6._MNo = $"6";
            icome6._MSource = _Source6;
            icome6._MPayment = _Payment6;
            icome6._MBalance = _Balance6;
            icome6._MInterestRate = _InterestRate6;
            icome6._MLimit = _Limit6;
            icome6._MType = _TypeCCLOC6;
            list.Add(icome6);

            DebtsCreditCardModel icome7 = new DebtsCreditCardModel();
            icome7._MNo = $"7";
            icome7._MSource = _Source7;
            icome7._MPayment = _Payment7;
            icome7._MBalance = _Balance7;
            icome7._MInterestRate = _InterestRate7;
            icome7._MLimit = _Limit7;
            icome7._MType = _TypeCCLOC7;
            list.Add(icome7);

            DebtsCreditCardModel icome8 = new DebtsCreditCardModel();
            icome8._MNo = $"8";
            icome8._MSource = _Source8;
            icome8._MPayment = _Payment8;
            icome8._MBalance = _Balance8;
            icome8._MInterestRate = _InterestRate8;
            icome8._MLimit = _Limit8;
            icome8._MType = _TypeCCLOC8;
            list.Add(icome8);

            DebtsCreditCardModel icome9 = new DebtsCreditCardModel();
            icome9._MNo = $"9";
            icome9._MSource = _Source9;
            icome9._MPayment = _Payment9;
            icome9._MBalance = _Balance9;
            icome9._MInterestRate = _InterestRate9;
            icome9._MLimit = _Limit9;
            icome9._MType = _TypeCCLOC9;
            list.Add(icome9);

            DebtsCreditCardModel icome10 = new DebtsCreditCardModel();
            icome10._MNo = $"10";
            icome10._MSource = _Source10;
            icome10._MPayment = _Payment10;
            icome10._MBalance = _Balance10;
            icome10._MInterestRate = _InterestRate10;
            icome10._MLimit = _Limit10;
            icome10._MType = _TypeCCLOC10;
            list.Add(icome10);

            DebtsCreditCardModel icome11 = new DebtsCreditCardModel();
            icome11._MNo = $"11";
            icome11._MSource = _Source11;
            icome11._MPayment = _Payment11;
            icome11._MBalance = _Balance11;
            icome11._MInterestRate = _InterestRate11;
            icome11._MLimit = _Limit11;
            icome11._MType = _TypeCCLOC11;
            list.Add(icome11);

            DebtsCreditCardModel icome12 = new DebtsCreditCardModel();
            icome12._MNo = $"12";
            icome12._MSource = _Source12;
            icome12._MPayment = _Payment12;
            icome12._MBalance = _Balance12;
            icome12._MInterestRate = _InterestRate12;
            icome12._MLimit = _Limit12;
            icome12._MType = _TypeCCLOC12;
            list.Add(icome12);
            //list.Sort(new DebtsCreditCardModelComparer());
            return list;
        }
        public double GetSubTotalPayment()
        {
            double total =
                _Payment1 +
                _Payment2 +
                _Payment3 +
                _Payment4 +
                _Payment5 +
                _Payment6 +
                _Payment7 +
                _Payment8 +
                _Payment9 +
                _Payment10 +
                _Payment11 +
                _Payment12;
            return total;
        }

        public double GetTotalBalance()
        {
            double total =
                _Balance1 +
                _Balance2 +
                _Balance3 +
                _Balance4 +
                _Balance5 +
                _Balance6 +
                _Balance7 +
                _Balance8 +
                _Balance9 +
                _Balance10 +
                _Balance11 +
                _Balance12;
            return total;
        }
    }

}
