using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using GVDEditor.Forms;
using GVDEditor.Properties;
using GVDEditor.Tools;
using GVDEditor.XML;

namespace GVDEditor
{
    internal static class Program
    {
        private static readonly string appGuid = ((GuidAttribute) Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using var mutex = new Mutex(false, "Global\\" + appGuid);

            try
            {
                GlobData.Config = Config.ReadData(Utils.CombinePath(Application.StartupPath, FileConsts.FILE_CONFIG));
            }
            catch (Exception e)
            {
                Log.Error($"Chyba pri načítaní konfiguračného súboru: {e.Message}");
                return;
            }

            Log.DoAppLogs = GlobData.Config.LoggingInfo;
            Log.DoErrorLogs = GlobData.Config.LoggingError;

            try
            {
                GlobData.Styles = Styles.ReadData(Utils.CombinePath(Application.StartupPath, FileConsts.FILE_STYLES));
                GlobData.UsingStyle = GlobData.Styles[GlobData.Styles.UsingStyleID];
            }
            catch (Exception e)
            {
                Log.Error($"Chyba pri načítaní súboru so štýlmi: {e.Message}");
                return;
            }

            if (!mutex.WaitOne(0, false) && !GlobData.Config.MoreInstance)
            {
                Log.AppInfo("Duplicitná inštancia ukončená");
                return;
            }

            if (GlobData.Config.ClassicGUI)
            {
                Application.SetCompatibleTextRenderingDefault(true);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }

            var culture = CultureInfo.CreateSpecificCulture(GlobData.Config.Language == Config.AppLanguage.CZ ? "cs" : "sk");

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            if (args.Length == 1 && (args[0] == "-unlock" || args[0] == "/unlock")) GlobData.PrivateFeatures = true;

            MessageBoxManager.OK = Resources.Global_OK;
            MessageBoxManager.Cancel = Resources.Global_Cancel;
            MessageBoxManager.Retry = Resources.Global_Retry;
            MessageBoxManager.Ignore = Resources.Global_Ignore;
            MessageBoxManager.Abort = Resources.Global_Abort;
            MessageBoxManager.Yes = Resources.Global_Yes;
            MessageBoxManager.No = Resources.Global_No;

            MessageBoxManager.Register();

            foreach (Style style in GlobData.Styles.StyleList)
            {
                SettingsNaming.NameColorSettings(style);
            }

            SettingsNaming.NameAppFontSetting(GlobData.Config.Fonts);
            SettingsNaming.NameDesktopColsSetting(GlobData.Config.DesktopCols);
            SettingsNaming.NameShortcutCommands(GlobData.Config.Shortcuts);

            Log.AppInfo($"Program spustený - v.{Application.ProductVersion} - \"{Application.ExecutablePath}\"");

            DateRem.Loc = GlobData.Config.DateRemLocate == Config.AppLanguage.CZ ? DateRem.LOCALE.CZECH : DateRem.LOCALE.SLOVAK;

            Application.Run(new FMain());

            Log.AppInfo("Program sa ukončuje\r\n");
        }
    }
}