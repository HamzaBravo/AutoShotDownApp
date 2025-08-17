namespace Sysstem32
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblTargetDateTime = new System.Windows.Forms.Label();
            this.dtpTargetDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTargetTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.nudDelayMinutes = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(13, 13);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentStatus.TabIndex = 0;
            this.lblCurrentStatus.Text = "label1";
            // 
            // lblTargetDateTime
            // 
            this.lblTargetDateTime.AutoSize = true;
            this.lblTargetDateTime.Location = new System.Drawing.Point(14, 36);
            this.lblTargetDateTime.Name = "lblTargetDateTime";
            this.lblTargetDateTime.Size = new System.Drawing.Size(35, 13);
            this.lblTargetDateTime.TabIndex = 0;
            this.lblTargetDateTime.Text = "label1";
            // 
            // dtpTargetDate
            // 
            this.dtpTargetDate.Location = new System.Drawing.Point(81, 72);
            this.dtpTargetDate.Name = "dtpTargetDate";
            this.dtpTargetDate.Size = new System.Drawing.Size(200, 20);
            this.dtpTargetDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hedef Tarih";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Hedef Saat";
            // 
            // dtpTargetTime
            // 
            this.dtpTargetTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTargetTime.Location = new System.Drawing.Point(81, 99);
            this.dtpTargetTime.Name = "dtpTargetTime";
            this.dtpTargetTime.Size = new System.Drawing.Size(200, 20);
            this.dtpTargetTime.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Gecikme";
            // 
            // nudDelayMinutes
            // 
            this.nudDelayMinutes.Location = new System.Drawing.Point(81, 143);
            this.nudDelayMinutes.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudDelayMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDelayMinutes.Name = "nudDelayMinutes";
            this.nudDelayMinutes.Size = new System.Drawing.Size(60, 20);
            this.nudDelayMinutes.TabIndex = 2;
            this.nudDelayMinutes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(113, 181);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(75, 23);
            this.btnDisable.TabIndex = 3;
            this.btnDisable.Text = "System İptal";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(206, 181);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Kapat";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 216);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.nudDelayMinutes);
            this.Controls.Add(this.dtpTargetTime);
            this.Controls.Add(this.dtpTargetDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTargetDateTime);
            this.Controls.Add(this.lblCurrentStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label lblTargetDateTime;
        private System.Windows.Forms.DateTimePicker dtpTargetDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTargetTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudDelayMinutes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Button btnClose;
    }
}