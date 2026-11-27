
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayDown.SubViews;
using System.Windows;
using System.IO;
using Syncfusion.Licensing;
using Syncfusion.SfSkinManager;

namespace PayDown
{
    public static class LicenseKeyLocator
    {
        public static void FindandRegisterLicenseKey()
        {
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWH1fcHVXRWJYVkd1XkY=");
        }
    }
    public class UtilMethods
    {
        public static string GetTableNameByFormName(string ctrlName)
        {
            if (ctrlName == HardCode.CTRL_INCOME_PAYCHECK)
            {
                return HardCode.TABLE_INCOME_PAYCHECK;
            }
            else if (ctrlName == HardCode.CTRL_INCOME_RETIRE) {

                return HardCode.TABLE_INCOME_RETIRE;

            }else if(ctrlName == HardCode.CTRL_INCOME_GOVER)
            {
                return HardCode.TABLE_INCOME_GOVER;
            }
            else
            {
                return HardCode.TABLE_INCOME_CHECK;
            }
        }

        public static string GetTableNameDebtsByFormName(string ctrlName)
        {
            if (ctrlName == HardCode.CTRL_DEBTS_MONTHLY_BILLING)
            {
                return HardCode.TABLE_DEBTS_MONTHLY_BILLS;
            }
            else if (ctrlName == HardCode.CTRL_DEBTS_LIVING_EXP)
            {

                return HardCode.TABLE_DEBTS_LIVING_EXP;

            }
            else if (ctrlName == HardCode.CTRL_DEBTS_LOANS)
            {
                return HardCode.TABLE_DEBTS_LOANS;
            }
            else
            {
                return HardCode.TABLE_DEBTS_CREDITCARD;
            }
        }
        public static void ShowMessageBox(string content)
        {
            WinMessageBox form = new WinMessageBox(content);
            SfSkinManager.SetTheme(form, new Theme() { ThemeName = "Material3Light" });
            form.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            form.ShowDialog();
        }

        public static List<string> GetSaveFolderPath()
        {
            List<string> strings = new List<string>();
            string documentpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PayDown";
            if (!Directory.Exists(@documentpath))
            {
                Directory.CreateDirectory(@documentpath);
            }
            string documentSubpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PayDown\\WorkSpace";
            if (!Directory.Exists(@documentSubpath))
            {
                Directory.CreateDirectory(@documentSubpath);
            }
            string documentWatchListSubpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PayDown\\WatchList";
            if (!Directory.Exists(@documentWatchListSubpath))
            {
                Directory.CreateDirectory(@documentWatchListSubpath);
            }
            strings.Add(documentpath);
            strings.Add(documentSubpath);
            strings.Add(documentWatchListSubpath);
            return strings;
        }
    }
}
