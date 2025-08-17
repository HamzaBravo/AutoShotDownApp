using System;

namespace Sysstem32.Helpers
{
    public static class DateTimeManager
    {
        private static DateTime? _staticBootTime = null;
        private static DateTime? _targetDate = null;
        private static bool _expiredModeActive = false;

        public static void Initialize()
        {
            // Program başladığında sistem tarihini statik olarak tut
            _staticBootTime = DateTime.Now;

            // Kayıtlı hedef tarihi oku
            _targetDate = ConfigManager.GetTargetDate();

            // Expired mode kontrolü
            CheckExpiredStatus();
        }

        public static void SetTargetDate(DateTime targetDate)
        {
            _targetDate = targetDate;
            ConfigManager.SaveTargetDate(targetDate);
        }

        public static DateTime? GetTargetDate()
        {
            return _targetDate;
        }

        public static bool IsExpired()
        {
            if (_targetDate == null) return false;

            // Statik boot time kullanarak kontrol et
            if (_staticBootTime.HasValue)
            {
                return _staticBootTime.Value >= _targetDate.Value;
            }

            return false;
        }

        public static void ActivateExpiredMode()
        {
            _expiredModeActive = true;
            // Registry'de expired durumunu kaydet
            ConfigManager.SaveRegistryValue("exp_md", "1");
        }

        public static bool IsExpiredModeActive()
        {
            if (_expiredModeActive) return true;

            // Registry'den kontrol et
            string value = ConfigManager.GetRegistryValue("exp_md");
            return value == "1";
        }

        private static void CheckExpiredStatus()
        {
            // Program başlarken expired durumunu kontrol et
            if (IsExpired() || IsExpiredModeActive())
            {
                _expiredModeActive = true;
            }
        }

        public static DateTime GetStaticBootTime()
        {
            return _staticBootTime ?? DateTime.Now;
        }
    }
}
