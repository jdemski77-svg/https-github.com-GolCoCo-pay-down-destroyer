using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PayDown.Models
{
    [Table("easyseat-user-paydown")]
    public class SupaDataModel : BaseModel
    {
        [PrimaryKey("id", false)]
        public int id { get; set; }

        [Column("cname")]
        public string cname { get; set; }

        [Column("cpassword")]
        public string cpassword { get; set; }

        public bool ModelCompare(SupaDataModel model)
        {
            bool ret = false;
            if (cname == model.cname && cpassword == model.cpassword)
            {
                ret = true;
            }
            return ret;
        }
    }
    public class ResultModel
    {
        public int _MonthsToPayoff {  get; set; }
        public double _MonthlyStartingBalance {  get; set; }
        public double _AvailableIncome {  get; set; }
        public double _NewBalance {  get; set; }
        public double _ChargeableExp {  get; set; }
        public double _Interest {  get; set; }
        public double _MonthlyEndingBalance {  get; set; }

        public ResultModel() {
            _MonthsToPayoff = 0;
            _MonthlyStartingBalance = 0.0;
            _AvailableIncome = 0.0;
            _NewBalance = 0.0;
            _ChargeableExp = 0.0;
            _Interest = 0.0;
            _MonthlyEndingBalance = 0.0;
        }
    }
    public class SettingMain : SettingsBase
    {
        public double _TotalInterestedPaid {  get; set; }
        public double? _IncomeTotal { get; set; }
        public double _MonthsToPay {  get; set; }
        public int _CreditCardTableMarkIndex {  get; set; }
        public double _TotalDebts {  get; set; }
        public double _MortgageRentPayment {  get; set; }
        public double _ChargeableExpenses {  get; set; }
        public double _NonChargeableExpenses { get; set; }
        public double _TotalMonthlyExp {  get; set; }

        public string _CreditScore1 { get; set; }

        public string _CreditScore2 { get; set; }

        public bool _IsHeloc{ get; set; }

        public SettingMain()
        {
            _CreditCardTableMarkIndex = 1;
            _TotalMonthlyExp = 0.0;
            _TotalDebts = 0.0;
            _MortgageRentPayment = 0.0;
            _ChargeableExpenses= 0.0;
            _NonChargeableExpenses = 0.0;
            _IncomeTotal = 0.0;
            _MonthsToPay = 0.0;
            _TotalInterestedPaid = 0.0;
            _CreditScore1 = "";
            _CreditScore2 = "";
            _IsHeloc = false;
        }
    }
    public class DebtsModel
    {
        public string _MNo { get; set; }
        public string _MSource { get; set; }
        public string _MAmount { get; set; }
        public string _MChargebale { get; set; }

        public DebtsModel()
        {
            _MNo = "";
            _MSource = "";
            _MAmount = "";
            _MChargebale = "NO";
        }
    }

    public class DebtsLoanModel
    {
        public string _MNo { get; set; }
        public string _MSource { get; set; }
        public double _MPayment { get; set; }
        public double _MBalance { get; set; }
        public double _MInterestRate { get; set; }
        public double _MOutInterest { get; set; }

        public DebtsLoanModel()
        {
            _MNo = "";
            _MSource = "";
            _MPayment = 0.0;
            _MBalance = 0.0;
            _MInterestRate = 0.0;
            _MOutInterest = 0.0;
        }
    }

    public class DebtsDebtsLoanModelComparer : IComparer<DebtsLoanModel>
    {
        private int SortOrder = -1;

        public DebtsDebtsLoanModelComparer()
        {
        }

        public int Compare(DebtsLoanModel x, DebtsLoanModel y)
        {
            if (x._MPayment > y._MPayment)
            {
                return 1 * SortOrder;
            }
            else if (x._MPayment < y._MPayment)
            {
                return -1 * SortOrder;
            }
            else
            {
                if (x._MInterestRate < y._MInterestRate)
                {
                    return 1 * SortOrder;
                }
                else if (x._MInterestRate > y._MInterestRate)
                {
                    return -1 * SortOrder;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class DebtsCreditCardModelComparer : IComparer<DebtsCreditCardModel>
    {
        private int SortOrder = -1;

        public DebtsCreditCardModelComparer()
        {
        }

        public int Compare(DebtsCreditCardModel x, DebtsCreditCardModel y)
        {
            if (x._MPayment > y._MPayment)
            {
                return 1 * SortOrder;
            }
            else if (x._MPayment < y._MPayment)
            {
                return -1 * SortOrder;
            }
            else
            {
                if (x._MInterestRate < y._MInterestRate)
                {
                    return 1 * SortOrder;
                }
                else if (x._MInterestRate > y._MInterestRate)
                {
                    return -1 * SortOrder;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class DebtsCreditCardModel
    {
        public string _MNo { get; set; }
        public string _MSource { get; set; }
        public double _MPayment { get; set; }
        public double _MBalance { get; set; }
        public double _MInterestRate { get; set; }
        public double _MLimit { get; set; }
        public string _MType { get; set; }

        public DebtsCreditCardModel()
        {
            _MNo = "";
            _MSource = "";
            _MPayment = 0.0;
            _MBalance = 0.0;
            _MInterestRate = 0.0;
            _MLimit = 0.0;
            _MType = "";
        }
    }
    public class IncomeModel
    {
        public string _MNo { get; set; }
        public string _MSource { get; set; }
        public string _MAmount { get; set; }

        public IncomeModel()
        {
            _MNo = "";
            _MSource = "";
            _MAmount = "";
        }
    }
    public class LoginInfo : SettingsBase
    {
        public string _username { get; set; }
        public string _password { get; set; }
        public bool _rememeber { get; set; }

        public LoginInfo()
        {
            _username = "";
            _password = "";
            _rememeber = false;
        }

        public bool IsEmpty()
        {
            if (_username == "" || _password == "")
            {
                return true;
            }
            return false;
        }
    }

    public class SettingsBase
    {
        public void Save()
        {

            using (StreamWriter writer = new StreamWriter(this.GetType().Name + ".xml"))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(this.GetType());
                xmlSerializer.Serialize(writer, this);
            }
        }

        public static T Load<T>() where T : SettingsBase
        {
            T result = null;
            if (File.Exists(typeof(T).Name + ".xml"))
            {
                using (StreamReader reader = new StreamReader(typeof(T).Name + ".xml"))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    result = (T)xmlSerializer.Deserialize(reader);
                }
            }
            return result;
        }
    }
}
