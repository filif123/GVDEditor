using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GVDEditor.Annotations;
using Microsoft.VisualBasic.CompilerServices;

namespace GVDEditor.Tools
{
    /// <summary>
    ///     Trieda sluziaca na logovanie do suborov
    /// </summary>
    public static class Log
    {
        /// <summary>
        ///     Nazov logovacieho suboru Info.log
        /// </summary>
        public const string LOG_APP_NAME = "Info.log";

        /// <summary>
        ///     Nazov logovacieho suboru Error.log
        /// </summary>
        public const string LOG_ERROR_NAME = "Error.log";

        /// <summary>
        ///     Cesta k priecinku s logmi
        /// </summary>
        public const string LogPath = "\\Log";

        /// <summary>
        ///     Cesta k priecinku s programom
        /// </summary>
        public static readonly string AppDirPath = Application.StartupPath;

        /// <summary>
        ///     Rob logy informacii
        /// </summary>
        public static bool DoAppLogs = true;

        /// <summary>
        ///     Rob logy chyb a vynimiek
        /// </summary>
        public static bool DoErrorLogs = true;

        private static LogFile logApp;
        private static LogFile logError;

        private static void LogString(LogFile logFile, string text)
        {
            logFile.LogString(text);
        }

        /// <summary>
        ///     Zapise chybu do logovacieho suboru
        /// </summary>
        /// <param name="s">text chybovej hlasky</param>
        public static void Error(string s)
        {
            if (DoErrorLogs)
            {
                logError ??= new LogFile(LOG_ERROR_NAME, LogFile.DateType.DATETIME);
                LogString(logError, s);
            }
        }

        /// <summary>
        ///     Zapise chybu do logovacieho suboru zo zadanymi parametrami
        /// </summary>
        /// <param name="sFmt">formát parametrov</param>
        /// <param name="aoPar">zadane parametre</param>
        public static void Error(string sFmt, params object[] aoPar)
        {
            if (DoErrorLogs) Error(string.Format(sFmt, aoPar));
        }

        /// <summary>
        ///     Zapise vynimku do logovacieho suburu
        /// </summary>
        /// <param name="e">vynimka</param>
        /// <param name="s">dobrovodna informacia o vynimke (dobrovolna)</param>
        public static void Exception(Exception e, string s = null)
        {
            if (e != null)
            {
                if (s == null)
                    s = e + "\r\n";
                else
                    s = s + "\r\n" + e + "\r\n";
            }

            Error(s);
        }

        /// <summary>
        ///     Zapise vynimku do logovacieho suburu
        /// </summary>
        /// <param name="e">vynimka</param>
        /// <param name="sFmt">format parametrov</param>
        /// <param name="aoPar">zadane parametre</param>
        public static void Exception(Exception e, string sFmt, params object[] aoPar)
        {
            if (DoErrorLogs) Exception(e, string.Format(sFmt, aoPar));
        }

        /// <summary>
        ///     Zapise informaciu do logovacieho suboru
        /// </summary>
        /// <param name="s">text informacie</param>
        public static void AppInfo(string s)
        {
            if (DoAppLogs)
            {
                logApp ??= new LogFile(LOG_APP_NAME, LogFile.DateType.DATETIME);
                LogString(logApp, s);
            }
        }

        internal class LogFile
        {
            public enum DateType
            {
                DATE,
                DATETIME,
                DATETIMEMS,
                TIME,
                TIMEMS,
                DATE_LAST
            }

            private readonly string DateSeparator;

            private readonly DateType DateTypeStamp;
            private readonly object Locker;
            private readonly int MaxSize;
            private DateTime LastDate;

            public LogFile([NotNull] string fileName, DateType dateType, string dateSeparator = "\t", int maxsize = 1000000)
            {
                FileName = fileName;
                DateTypeStamp = dateType;
                DateSeparator = dateSeparator;
                Locker = RuntimeHelpers.GetObjectValue(new object());

                FullFileDir = Utils.CombinePath(AppDirPath, LogPath);
                FullFilePath = Utils.CombinePath(AppDirPath, LogPath, fileName);

                if (!Directory.Exists(FullFileDir)) Directory.CreateDirectory(FullFileDir);

                MaxSize = maxsize;
            }

            private string FileName { get; }
            private string FullFilePath { get; }
            private string FullFileDir { get; }

            public void LogString(string text)
            {
                if (text == null) return;

                ObjectFlowControl.CheckForSyncLockOnValueType(Locker);
                lock (Locker)
                {
                    DateTime now = DateTime.Now;

                    if (MaxSize > 0)
                        if (File.Exists(FullFilePath))
                            if (MaxSize < new FileInfo(FullFilePath).Length)
                            {
                                var newFile = Path.GetFileNameWithoutExtension(FileName) + "_" + now.ToString("dd-MM") + ".log.bak";
                                File.Move(FullFilePath, Utils.CombinePath(FullFileDir, newFile));
                            }

                    switch (DateTypeStamp)
                    {
                        case DateType.DATE:
                            text = now.ToString("dd.MM.yyyy") + DateSeparator + text;
                            break;
                        case DateType.DATETIME:
                            text = now.ToString("dd.MM.yyyy HH:mm:ss") + DateSeparator + text;
                            break;
                        case DateType.DATETIMEMS:
                            text = now.ToString("dd.MM.yyyy HH:mm:ss.fff") + DateSeparator + text;
                            break;
                        case DateType.TIME:
                            text = now.ToString("HH:mm:ss") + DateSeparator + text;
                            break;
                        case DateType.TIMEMS:
                            text = now.ToString("HH:mm:ss.fff") + DateSeparator + text;
                            break;
                        case DateType.DATE_LAST:
                        {
                            if (LastDate.Equals(now.Date))
                                text = "--- " + now.ToString("dd.MM.yyyy") + " ---\r\n" + text;
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException("Neznámy parameter " + DateTypeStamp);
                    }

                    text += "\r\n";

                    LastDate = now.Date;

                    File.AppendAllText(FullFilePath, text);
                }
            }
        }
    }
}