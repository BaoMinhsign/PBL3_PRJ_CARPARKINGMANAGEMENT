﻿namespace PBL3_CARPARKINGMANAGEMENT
{
    partial class ParkingLotUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.employeeTableAdapter = new PBL3_CARPARKINGMANAGEMENT.CARPARKINGMANAGEMENTDataSetTableAdapters.EmployeeTableAdapter();
            this.cARPARKINGMANAGEMENTDataSet1 = new PBL3_CARPARKINGMANAGEMENT.CARPARKINGMANAGEMENTDataSet1();
            this.cARPARKINGMANAGEMENTDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.payment_btn = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.ButtonDelete = new Guna.UI2.WinForms.Guna2Button();
            this.ButtonAdd = new Guna.UI2.WinForms.Guna2Button();
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cARPARKINGMANAGEMENTDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cARPARKINGMANAGEMENTDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cARPARKINGMANAGEMENTDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cARPARKINGMANAGEMENTDataSetBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ComboBox1
            // 
            this.guna2ComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox1.BorderRadius = 5;
            this.guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ComboBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2ComboBox1.ItemHeight = 30;
            this.guna2ComboBox1.Location = new System.Drawing.Point(38, 24);
            this.guna2ComboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2ComboBox1.Name = "guna2ComboBox1";
            this.guna2ComboBox1.Size = new System.Drawing.Size(226, 36);
            this.guna2ComboBox1.TabIndex = 3;
            this.guna2ComboBox1.SelectedIndexChanged += new System.EventHandler(this.guna2ComboBox1_SelectedIndexChanged);
            // 
            // employeeTableAdapter
            // 
            this.employeeTableAdapter.ClearBeforeFill = true;
            // 
            // cARPARKINGMANAGEMENTDataSet1
            // 
            this.cARPARKINGMANAGEMENTDataSet1.DataSetName = "CARPARKINGMANAGEMENTDataSet1";
            this.cARPARKINGMANAGEMENTDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cARPARKINGMANAGEMENTDataSet1BindingSource
            // 
            this.cARPARKINGMANAGEMENTDataSet1BindingSource.DataSource = this.cARPARKINGMANAGEMENTDataSet1;
            this.cARPARKINGMANAGEMENTDataSet1BindingSource.Position = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.LightGray;
            this.dataGridView1.Location = new System.Drawing.Point(38, 81);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(660, 325);
            this.dataGridView1.TabIndex = 4;
            // 
            // payment_btn
            // 
            this.payment_btn.BorderRadius = 5;
            this.payment_btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.payment_btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.payment_btn.ForeColor = System.Drawing.Color.White;
            this.payment_btn.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(185)))), ((int)(((byte)(232)))));
            this.payment_btn.Image = global::PBL3_CARPARKINGMANAGEMENT.Properties.Resources.icon_Giaodich;
            this.payment_btn.Location = new System.Drawing.Point(38, 427);
            this.payment_btn.Margin = new System.Windows.Forms.Padding(2);
            this.payment_btn.Name = "payment_btn";
            this.payment_btn.Size = new System.Drawing.Size(172, 29);
            this.payment_btn.TabIndex = 6;
            this.payment_btn.Text = "Thanh toán";
            this.payment_btn.Click += new System.EventHandler(this.payment_btn_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 5;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(238)))), ((int)(((byte)(181)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(185)))), ((int)(((byte)(232)))));
            this.guna2Button1.Image = global::PBL3_CARPARKINGMANAGEMENT.Properties.Resources.icon_Phuongtien;
            this.guna2Button1.Location = new System.Drawing.Point(526, 24);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(172, 29);
            this.guna2Button1.TabIndex = 5;
            this.guna2Button1.Text = "Trạng thái bãi đỗ";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.BorderRadius = 5;
            this.ButtonDelete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.ButtonDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ButtonDelete.ForeColor = System.Drawing.Color.White;
            this.ButtonDelete.Image = global::PBL3_CARPARKINGMANAGEMENT.Properties.Resources.remove_icon;
            this.ButtonDelete.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ButtonDelete.Location = new System.Drawing.Point(412, 24);
            this.ButtonDelete.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(90, 29);
            this.ButtonDelete.TabIndex = 2;
            this.ButtonDelete.Text = "Xoá ";
            this.ButtonDelete.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.BorderRadius = 5;
            this.ButtonAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.ButtonAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ButtonAdd.ForeColor = System.Drawing.Color.White;
            this.ButtonAdd.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(185)))), ((int)(((byte)(232)))));
            this.ButtonAdd.Image = global::PBL3_CARPARKINGMANAGEMENT.Properties.Resources.add_icon1;
            this.ButtonAdd.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ButtonAdd.Location = new System.Drawing.Point(300, 24);
            this.ButtonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(90, 29);
            this.ButtonAdd.TabIndex = 1;
            this.ButtonAdd.Text = "Thêm";
            this.ButtonAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ParkingLotUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(9)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.payment_btn);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.guna2ComboBox1);
            this.Controls.Add(this.ButtonDelete);
            this.Controls.Add(this.ButtonAdd);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ParkingLotUC";
            this.Size = new System.Drawing.Size(739, 488);
            ((System.ComponentModel.ISupportInitialize)(this.cARPARKINGMANAGEMENTDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cARPARKINGMANAGEMENTDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cARPARKINGMANAGEMENTDataSetBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button ButtonAdd;
        private Guna.UI2.WinForms.Guna2Button ButtonDelete;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox1;
        private System.Windows.Forms.BindingSource cARPARKINGMANAGEMENTDataSet1BindingSource;
        private CARPARKINGMANAGEMENTDataSet1 cARPARKINGMANAGEMENTDataSet1;
        private System.Windows.Forms.BindingSource employeeBindingSource;
        private CARPARKINGMANAGEMENTDataSetTableAdapters.EmployeeTableAdapter employeeTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource cARPARKINGMANAGEMENTDataSetBindingSource1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button payment_btn;
    }
}
