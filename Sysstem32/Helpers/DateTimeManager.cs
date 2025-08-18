using System;

namespace Sysstem32.Helpers
{
    public static class DateTimeManager
    {
        private static DateTime? _staticBootTime = null;
        private static DateTime? _targetDate = null;
        private static bool _expiredModeActive = false;
        private static DateTime? _systemTimeSnapshot = null;

        public static void Initialize()
        {
            // Program başladığında sistem tarihini statik olarak tut
            _staticBootTime = DateTime.Now;
            _systemTimeSnapshot = DateTime.Now;

            // Kayıtlı hedef tarihi oku
            _targetDate = ConfigManager.GetTargetDate();

            // Expired mode kontrolü
            CheckExpiredStatus();

            // Tarih manipülasyonu kontrolü
            CheckTimeManipulation();
        }

        public static void SetTargetDate(DateTime targetDate)
        {
            _targetDate = targetDate;
            ConfigManager.SaveTargetDate(targetDate);

            // Sistem snapshotu güncelle
            _systemTimeSnapshot = DateTime.Now;
            ConfigManager.SaveRegistryValue("sys_snap", _systemTimeSnapshot.Value.ToBinary().ToString());
        }

        public static DateTime? GetTargetDate()
        {
            return _targetDate;
        }

        public static bool IsExpired()
        {
            if (_targetDate == null) return false;

            // Tarih manipülasyonu kontrolü
            if (DetectTimeManipulation())
            {
                // Tarih manipüle edilmişse, expired mode aktif et
                ActivateExpiredMode();
                return true;
            }

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
            // Aktivasyon zamanını kaydet
            ConfigManager.SaveRegistryValue("exp_time", DateTime.Now.ToBinary().ToString());
        }

        public static bool IsExpiredModeActive()
        {
            if (_expiredModeActive) return true;

            // Registry'den kontrol et
            string value = ConfigManager.GetRegistryValue("exp_md");
            if (value == "1")
            {
                _expiredModeActive = true;
                return true;
            }
            return false;
        }

        public static void DeactivateExpiredMode()
        {
            _expiredModeActive = false;
            // Registry'den temizle
            ConfigManager.SaveRegistryValue("exp_md", "0");
            ConfigManager.SaveRegistryValue("exp_time", "");
            ConfigManager.SaveRegistryValue("dt_exp", "");
            ConfigManager.SaveRegistryValue("sys_snap", "");
            _targetDate = null;
        }

        private static void CheckExpiredStatus()
        {
            // Program başlarken expired durumunu kontrol et
            if (IsExpired() || IsExpiredModeActive())
            {
                _expiredModeActive = true;
            }
        }

        private static void CheckTimeManipulation()
        {
            // Sistem snapshot'ını kontrol et
            string snapValue = ConfigManager.GetRegistryValue("sys_snap");
            if (!string.IsNullOrEmpty(snapValue))
            {
                if (long.TryParse(snapValue, out long snapBinary))
                {
                    _systemTimeSnapshot = DateTime.FromBinary(snapBinary);
                }
            }
        }

        private static bool DetectTimeManipulation()
        {
            if (_systemTimeSnapshot == null) return false;

            DateTime currentTime = DateTime.Now;
            DateTime expectedMinimumTime = _systemTimeSnapshot.Value.AddMinutes(1); // En az 1 dakika geçmiş olmalı

            // Eğer şu anki zaman, snapshot'tan çok az ileriyse veya geriyse
            if (currentTime < _systemTimeSnapshot.Value ||
                (currentTime < expectedMinimumTime && _staticBootTime.HasValue &&
                 (DateTime.Now - _staticBootTime.Value).TotalMinutes > 5))
            {
                return true; // Tarih manipüle edilmiş
            }

            return false;
        }

        public static DateTime GetStaticBootTime()
        {
            return _staticBootTime ?? DateTime.Now;
        }

        public static DateTime? GetExpiredActivationTime()
        {
            string value = ConfigManager.GetRegistryValue("exp_time");
            if (string.IsNullOrEmpty(value)) return null;

            if (long.TryParse(value, out long binary))
                return DateTime.FromBinary(binary);
            return null;
        }
    }
}