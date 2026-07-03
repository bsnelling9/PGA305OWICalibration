namespace PGA305OWICalibration
{
    partial class SettingsForm
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
            txtAPIURL = new TextBox();
            lblAPIURL = new Label();
            btnClose = new PGA305OWICalibration.UIControls.ATPButton();
            btnSave = new PGA305OWICalibration.UIControls.ATPButton();
            SuspendLayout();
            // 
            // txtAPIURL
            // 
            txtAPIURL.Location = new Point(96, 48);
            txtAPIURL.Name = "txtAPIURL";
            txtAPIURL.Size = new Size(173, 23);
            txtAPIURL.TabIndex = 0;
            txtAPIURL.TextChanged += txtAPIURL_TextChanged;
            // 
            // lblAPIURL
            // 
            lblAPIURL.AutoSize = true;
            lblAPIURL.Location = new Point(17, 51);
            lblAPIURL.Name = "lblAPIURL";
            lblAPIURL.Size = new Size(52, 15);
            lblAPIURL.TabIndex = 1;
            lblAPIURL.Text = "API URL:";
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.BorderColor = Color.Black;
            btnClose.BorderSize = 2;
            btnClose.CornerRadius = 10;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F);
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point(696, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(92, 40);
            btnClose.TabIndex = 41;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.White;
            btnSave.BorderColor = Color.Black;
            btnSave.BorderSize = 2;
            btnSave.CornerRadius = 10;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.ForeColor = Color.Black;
            btnSave.Location = new Point(28, 398);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(92, 40);
            btnSave.TabIndex = 42;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(btnClose);
            Controls.Add(lblAPIURL);
            Controls.Add(txtAPIURL);
            Name = "SettingsForm";
            Text = "SettingsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAPIURL;
        private Label lblAPIURL;
        private UIControls.ATPButton btnClose;
        private UIControls.ATPButton btnSave;
    }
}