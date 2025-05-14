namespace PBL3_CARPARKINGMANAGEMENT
{
    partial class Parking_StatusForm
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
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.cartypecbB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // cartypecbB
            // 
            this.cartypecbB.BackColor = System.Drawing.Color.Transparent;
            this.cartypecbB.BorderRadius = 10;
            this.cartypecbB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cartypecbB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cartypecbB.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cartypecbB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cartypecbB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cartypecbB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cartypecbB.ItemHeight = 30;
            this.cartypecbB.Location = new System.Drawing.Point(27, 24);
            this.cartypecbB.Name = "cartypecbB";
            this.cartypecbB.Size = new System.Drawing.Size(196, 36);
            this.cartypecbB.TabIndex = 0;
            this.cartypecbB.SelectedIndexChanged += new System.EventHandler(this.cartypecbB_SelectedIndexChanged);
            // 
            // contentPanel
            // 
            this.contentPanel.BorderRadius = 10;
            this.contentPanel.FillColor = System.Drawing.Color.White;
            this.contentPanel.Location = new System.Drawing.Point(25, 97);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(605, 400);
            this.contentPanel.TabIndex = 1;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(56)))), ((int)(((byte)(41)))));
            this.guna2Button1.Font = new System.Drawing.Font("Calibri", 13.75F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(502, 23);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(127, 36);
            this.guna2Button1.TabIndex = 2;
            this.guna2Button1.Text = "Close";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // Parking_StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(77)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(659, 546);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.cartypecbB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Parking_StatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parking_StatusForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2ComboBox cartypecbB;
        private Guna.UI2.WinForms.Guna2Panel contentPanel;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}