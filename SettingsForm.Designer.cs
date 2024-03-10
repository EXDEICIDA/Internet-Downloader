namespace TestBunifu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            this.label1 = new System.Windows.Forms.Label();
            this.btnToggleMode = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(280, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(590, 163);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnToggleMode
            // 
            this.btnToggleMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleMode.BackColor = System.Drawing.Color.Transparent;
            this.btnToggleMode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToggleMode.BackgroundImage")));
            this.btnToggleMode.ButtonText = "Mode";
            this.btnToggleMode.ButtonTextMarginLeft = 0;
            this.btnToggleMode.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnToggleMode.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnToggleMode.DisabledForecolor = System.Drawing.Color.White;
            this.btnToggleMode.ForeColor = System.Drawing.Color.White;
            this.btnToggleMode.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleMode.IconPadding = 10;
            this.btnToggleMode.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnToggleMode.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnToggleMode.IdleBorderRadius = 35;
            this.btnToggleMode.IdleBorderThickness = 0;
            this.btnToggleMode.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnToggleMode.IdleIconLeftImage = null;
            this.btnToggleMode.IdleIconRightImage = null;
            this.btnToggleMode.Location = new System.Drawing.Point(12, 197);
            this.btnToggleMode.Name = "btnToggleMode";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.BorderRadius = 1;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.btnToggleMode.onHoverState = stateProperties1;
            this.btnToggleMode.Size = new System.Drawing.Size(1084, 55);
            this.btnToggleMode.TabIndex = 3;
            this.btnToggleMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnToggleMode.Click += new System.EventHandler(this.btnToggleMode_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 949);
            this.Controls.Add(this.btnToggleMode);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnToggleMode;
    }
}