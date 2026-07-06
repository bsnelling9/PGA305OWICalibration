using PGA305OWICalibration.UIControls;

namespace PGA305OWICalibration
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            TabMainPage = new TabPage();
            label2 = new Label();
            btnStart = new ATPButton();
            btnConfigI2C = new ATPButton();
            btnSettings = new ATPButton();
            btnDebug = new ATPButton();
            hardwareTab = new TabPage();
            deviceTab = new TabPage();
            tabPage4 = new TabPage();
            tabControl1.SuspendLayout();
            TabMainPage.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(TabMainPage);
            tabControl1.Controls.Add(hardwareTab);
            tabControl1.Controls.Add(deviceTab);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1238, 738);
            tabControl1.TabIndex = 21;
            // 
            // TabMainPage
            // 
            TabMainPage.Controls.Add(label2);
            TabMainPage.Controls.Add(btnStart);
            TabMainPage.Controls.Add(btnConfigI2C);
            TabMainPage.Controls.Add(btnSettings);
            TabMainPage.Controls.Add(btnDebug);
            TabMainPage.ForeColor = SystemColors.ControlText;
            TabMainPage.Location = new Point(4, 24);
            TabMainPage.Name = "TabMainPage";
            TabMainPage.Padding = new Padding(3);
            TabMainPage.Size = new Size(1230, 710);
            TabMainPage.TabIndex = 0;
            TabMainPage.Text = "Main";
            TabMainPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(8, 13);
            label2.Name = "label2";
            label2.Size = new Size(653, 221);
            label2.TabIndex = 44;
            label2.Text = "APT10 Final Output Configuration";
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.White;
            btnStart.BorderColor = Color.Black;
            btnStart.BorderSize = 2;
            btnStart.CornerRadius = 10;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 10F);
            btnStart.ForeColor = Color.Black;
            btnStart.Location = new Point(1034, 418);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(150, 50);
            btnStart.TabIndex = 43;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnConfigI2C
            // 
            btnConfigI2C.BackColor = Color.White;
            btnConfigI2C.BorderColor = Color.Black;
            btnConfigI2C.BorderSize = 2;
            btnConfigI2C.CornerRadius = 10;
            btnConfigI2C.FlatStyle = FlatStyle.Flat;
            btnConfigI2C.Font = new Font("Segoe UI", 10F);
            btnConfigI2C.ForeColor = Color.Black;
            btnConfigI2C.Location = new Point(1034, 490);
            btnConfigI2C.Name = "btnConfigI2C";
            btnConfigI2C.Size = new Size(150, 50);
            btnConfigI2C.TabIndex = 42;
            btnConfigI2C.Text = "Configure using I2C";
            btnConfigI2C.UseVisualStyleBackColor = true;
            btnConfigI2C.Click += btnConfigI2C_Click;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.White;
            btnSettings.BorderColor = Color.Black;
            btnSettings.BorderSize = 2;
            btnSettings.CornerRadius = 10;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 10F);
            btnSettings.ForeColor = Color.Black;
            btnSettings.Location = new Point(1034, 631);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(150, 50);
            btnSettings.TabIndex = 41;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnDebug
            // 
            btnDebug.BackColor = Color.White;
            btnDebug.BorderColor = Color.Black;
            btnDebug.BorderSize = 2;
            btnDebug.CornerRadius = 10;
            btnDebug.FlatStyle = FlatStyle.Flat;
            btnDebug.Font = new Font("Segoe UI", 10F);
            btnDebug.ForeColor = Color.Black;
            btnDebug.Location = new Point(1034, 564);
            btnDebug.Name = "btnDebug";
            btnDebug.Size = new Size(150, 50);
            btnDebug.TabIndex = 40;
            btnDebug.Text = "Configure using EVM";
            btnDebug.UseVisualStyleBackColor = true;
            btnDebug.Click += btnDebug_Click;
            // 
            // hardwareTab
            // 
            hardwareTab.Location = new Point(4, 24);
            hardwareTab.Name = "hardwareTab";
            hardwareTab.Padding = new Padding(3);
            hardwareTab.Size = new Size(1230, 710);
            hardwareTab.TabIndex = 1;
            hardwareTab.Text = "Hardware";
            hardwareTab.UseVisualStyleBackColor = true;
            // 
            // deviceTab
            // 
            deviceTab.Location = new Point(4, 24);
            deviceTab.Name = "deviceTab";
            deviceTab.Padding = new Padding(3);
            deviceTab.Size = new Size(1230, 710);
            deviceTab.TabIndex = 2;
            deviceTab.Text = "Device";
            deviceTab.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1230, 710);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1238, 738);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "PGA305 OWI Calibration";
            tabControl1.ResumeLayout(false);
            TabMainPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TabControl tabControl1;
        private TabPage TabMainPage;
        private TabPage hardwareTab;
        private TabPage deviceTab;
        private TabPage tabPage4;
        private ATPButton btnDebug;
        private ATPButton btnSettings;
        private ATPButton btnConfigI2C;
        private ATPButton btnStart;
        private Label label2;
    }
}
