using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sysstem32.Helpers
{
    public class HotkeyManager
    {
        // Windows API fonksiyonları
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Modifier keys
        private const int MOD_SHIFT = 0x0004;

        // Virtual Key Codes
        private const int VK_TAB = 0x09;
        private const int VK_HOME = 0x24;

        // Hotkey ID
        private const int HOTKEY_ID_1 = 9001;
        private const int HOTKEY_ID_2 = 9002;

        private IntPtr _windowHandle;
        public event EventHandler HotkeyPressed;

        public HotkeyManager(IntPtr windowHandle)
        {
            _windowHandle = windowHandle;
        }

        public bool RegisterHotkey()
        {
            try
            {
                // Shift+Tab kombinasyonunu kaydet
                bool result1 = RegisterHotKey(_windowHandle, HOTKEY_ID_1, MOD_SHIFT, VK_TAB);
                // Shift+Home kombinasyonunu da kaydet (alternatif)
                bool result2 = RegisterHotKey(_windowHandle, HOTKEY_ID_2, MOD_SHIFT, VK_HOME);

                return result1 || result2; // En az birisi başarılı olsun
            }
            catch { return false; }
        }

        public void UnregisterHotkey()
        {
            try
            {
                UnregisterHotKey(_windowHandle, HOTKEY_ID_1);
                UnregisterHotKey(_windowHandle, HOTKEY_ID_2);
            }
            catch { }
        }

        public void ProcessHotkey(Message message)
        {
            if (message.Msg == 0x0312) // WM_HOTKEY
            {
                int id = message.WParam.ToInt32();
                if (id == HOTKEY_ID_1 || id == HOTKEY_ID_2)
                {
                    // Hotkey basıldı, event'i tetikle
                    HotkeyPressed?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
