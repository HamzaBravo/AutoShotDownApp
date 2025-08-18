using System;
using System.Windows.Forms;
using Sysstem32.Helpers;

namespace Sysstem32
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            LoadCurrentSettings();
            SetDefaultValues();
        }

        protected override void SetVisibleCore(bool value)
        {
            if (value)
            {
                // Form her görünür olduğunda verileri yenile
                LoadCurrentSettings();
            }
            base.SetVisibleCore(value);
        }

        private void LoadCurrentSettings()
        {
            try
            {
                // Gecikme süresini önce oku (scope çakışmasını önlemek için)
                int delayMinutes = ConfigManager.GetDelayMinutes();
                nudDelayMinutes.Value = delayMinutes;

                // Mevcut hedef tarihi oku
                DateTime? targetDate = DateTimeManager.GetTargetDate();

                if (targetDate.HasValue)
                {
                    lblTargetDateTime.Text = $"Hedef Tarih/Saat: {targetDate.Value:dd.MM.yyyy HH:mm}";

                    if (DateTimeManager.IsExpiredModeActive())
                    {
                        lblCurrentStatus.Text = "Mevcut Durum: SÜRELİ KAPATMA AKTİF!";
                        lblCurrentStatus.ForeColor = System.Drawing.Color.Red;

                        // Expired aktivasyon zamanını göster
                        DateTime? expTime = DateTimeManager.GetExpiredActivationTime();
                        if (expTime.HasValue)
                        {
                            DateTime shutdownTime = expTime.Value.AddMinutes(delayMinutes);
                            lblTargetDateTime.Text += $"\nKapatma Zamanı: {shutdownTime:dd.MM.yyyy HH:mm}";
                        }
                    }
                    else if (DateTimeManager.IsExpired())
                    {
                        lblCurrentStatus.Text = "Mevcut Durum: Süre doldu, kapatma beklemede...";
                        lblCurrentStatus.ForeColor = System.Drawing.Color.Orange;
                    }
                    else
                    {
                        lblCurrentStatus.Text = "Mevcut Durum: Aktif, hedef tarihi bekliyor";
                        lblCurrentStatus.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    lblCurrentStatus.Text = "Mevcut Durum: Ayarlanmamış";
                    lblTargetDateTime.Text = "Hedef Tarih/Saat: --";
                    lblCurrentStatus.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch
            {
                lblCurrentStatus.Text = "Mevcut Durum: Hata!";
            }
        }

        private void SetDefaultValues()
        {
            // Varsayılan olarak bugünden 1 hafta sonra
            dtpTargetDate.Value = DateTime.Now.AddDays(2);
            dtpTargetTime.Value = DateTime.Now.AddHours(1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Önce expired mode'u deaktif et (eğer aktifse)
                if (DateTimeManager.IsExpiredModeActive())
                {
                    DateTimeManager.DeactivateExpiredMode();
                }

                // Tarih ve saati birleştir
                DateTime targetDateTime = dtpTargetDate.Value.Date + dtpTargetTime.Value.TimeOfDay;

                // Geçmiş tarih kontrolü
                if (targetDateTime <= DateTime.Now)
                {
                    MessageBox.Show("Hedef tarih gelecekte olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ayarları kaydet
                DateTimeManager.SetTargetDate(targetDateTime);
                ConfigManager.SaveDelayMinutes((int)nudDelayMinutes.Value);

                MessageBox.Show("Ayarlar kaydedildi!\n\nSistem kapatma planlandı:\n" +
                               targetDateTime.ToString("dd.MM.yyyy HH:mm") +
                               "\n\nKısayol: Shift+Tab veya Shift+Home",
                               "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCurrentSettings(); // Görüntüyü güncelle
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Sistem kapatma planlamasını tamamen iptal etmek istediğinizden emin misiniz?",
                                           "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Tüm ayarları ve expired mode'u temizle
                    DateTimeManager.DeactivateExpiredMode();

                    MessageBox.Show("Sistem kapatma planlaması tamamen iptal edildi!", "İptal Edildi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadCurrentSettings(); // Görüntüyü güncelle
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İptal etme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Form kapatılmasını engelle, sadece gizle
            e.Cancel = true;
            this.Hide();
        }
    }
}