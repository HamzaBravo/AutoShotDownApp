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
        private const int MOD_ALT = 0x0001;
        private const int MOD_CONTROL = 0x0002;

        // Virtual Key Codes
        private const int VK_F12 = 0x7B;

        // Hotkey ID
        private const int HOTKEY_ID = 9000;

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
                // Ctrl+Alt+F12 kombinasyonunu kaydet
                return RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL | MOD_ALT, VK_F12);
            }
            catch{ return false;}
        }

        public void UnregisterHotkey()
        {
            try
            {
                UnregisterHotKey(_windowHandle, HOTKEY_ID);
            }
            catch { }
        }

        public void ProcessHotkey(Message message)
        {
            if (message.Msg == 0x0312) // WM_HOTKEY
            {
                int id = message.WParam.ToInt32();
                if (id == HOTKEY_ID)
                {
                    // Hotkey basıldı, event'i tetikle
                    HotkeyPressed?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
