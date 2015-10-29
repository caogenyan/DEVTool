namespace DevTool
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bdsDomain = new System.Windows.Forms.BindingSource(this.components);
            this.tabIIS = new System.Windows.Forms.TabPage();
            this.tabWebConfig = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabHost = new System.Windows.Forms.TabPage();
            this.dgvDomain = new System.Windows.Forms.DataGridView();
            this.Domain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QA1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.QA2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.QA3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.LOC = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bdsDomain)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDomain)).BeginInit();
            this.SuspendLayout();
            // 
            // tabIIS
            // 
            this.tabIIS.Location = new System.Drawing.Point(4, 29);
            this.tabIIS.Name = "tabIIS";
            this.tabIIS.Padding = new System.Windows.Forms.Padding(3);
            this.tabIIS.Size = new System.Drawing.Size(619, 367);
            this.tabIIS.TabIndex = 3;
            this.tabIIS.Text = "IIS";
            this.tabIIS.UseVisualStyleBackColor = true;
            // 
            // tabWebConfig
            // 
            this.tabWebConfig.Location = new System.Drawing.Point(4, 29);
            this.tabWebConfig.Name = "tabWebConfig";
            this.tabWebConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabWebConfig.Size = new System.Drawing.Size(619, 367);
            this.tabWebConfig.TabIndex = 2;
            this.tabWebConfig.Text = "WebConfig";
            this.tabWebConfig.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabHost);
            this.tabControl1.Controls.Add(this.tabWebConfig);
            this.tabControl1.Controls.Add(this.tabIIS);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 25);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 399);
            this.tabControl1.TabIndex = 0;
            // 
            // tabHost
            // 
            this.tabHost.Controls.Add(this.dgvDomain);
            this.tabHost.Location = new System.Drawing.Point(4, 29);
            this.tabHost.Name = "tabHost";
            this.tabHost.Padding = new System.Windows.Forms.Padding(3);
            this.tabHost.Size = new System.Drawing.Size(619, 366);
            this.tabHost.TabIndex = 0;
            this.tabHost.Text = "Host";
            this.tabHost.UseVisualStyleBackColor = true;
            // 
            // dgvDomain
            // 
            this.dgvDomain.AllowUserToAddRows = false;
            this.dgvDomain.AllowUserToDeleteRows = false;
            this.dgvDomain.AllowUserToOrderColumns = true;
            this.dgvDomain.AllowUserToResizeRows = false;
            this.dgvDomain.AutoGenerateColumns = false;
            this.dgvDomain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDomain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Domain,
            this.IP,
            this.Remark,
            this.QA1,
            this.QA2,
            this.QA3,
            this.LOC});
            this.dgvDomain.DataSource = this.bdsDomain;
            this.dgvDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDomain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvDomain.Location = new System.Drawing.Point(3, 3);
            this.dgvDomain.MultiSelect = false;
            this.dgvDomain.Name = "dgvDomain";
            this.dgvDomain.RowHeadersWidth = 25;
            this.dgvDomain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDomain.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDomain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDomain.Size = new System.Drawing.Size(613, 360);
            this.dgvDomain.TabIndex = 0;
            this.dgvDomain.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDomain_CellContentDoubleClick);
            this.dgvDomain.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDomain_CellMouseDown);
            this.dgvDomain.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDomain_CellPainting);
            this.dgvDomain.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDomain_CellValidating);
            this.dgvDomain.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDomain_DataBindingComplete);
            this.dgvDomain.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            // 
            // Domain
            // 
            this.Domain.DataPropertyName = "Domain";
            this.Domain.HeaderText = "Domain";
            this.Domain.Name = "Domain";
            this.Domain.Width = 150;
            // 
            // IP
            // 
            this.IP.DataPropertyName = "IP";
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.Width = 95;
            // 
            // Remark
            // 
            this.Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "Remark";
            this.Remark.Name = "Remark";
            // 
            // QA1
            // 
            this.QA1.DataPropertyName = "QA1";
            this.QA1.HeaderText = "QA1";
            this.QA1.Name = "QA1";
            this.QA1.ReadOnly = true;
            this.QA1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.QA1.Width = 35;
            // 
            // QA2
            // 
            this.QA2.DataPropertyName = "QA2";
            this.QA2.HeaderText = "QA2";
            this.QA2.Name = "QA2";
            this.QA2.ReadOnly = true;
            this.QA2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.QA2.Width = 35;
            // 
            // QA3
            // 
            this.QA3.DataPropertyName = "QA3";
            this.QA3.HeaderText = "QA3";
            this.QA3.Name = "QA3";
            this.QA3.ReadOnly = true;
            this.QA3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.QA3.Width = 35;
            // 
            // LOC
            // 
            this.LOC.DataPropertyName = "LOC";
            this.LOC.HeaderText = "LOC";
            this.LOC.Name = "LOC";
            this.LOC.ReadOnly = true;
            this.LOC.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LOC.Width = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(627, 399);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(643, 438);
            this.Name = "Form1";
            this.Text = "DevTool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsDomain)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDomain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bdsDomain;
        private System.Windows.Forms.TabPage tabIIS;
        private System.Windows.Forms.TabPage tabWebConfig;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridView dgvDomain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Domain;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.DataGridViewImageColumn QA1;
        private System.Windows.Forms.DataGridViewImageColumn QA2;
        private System.Windows.Forms.DataGridViewImageColumn QA3;
        private System.Windows.Forms.DataGridViewImageColumn LOC;
        private System.Windows.Forms.TabPage tabHost;
    }
}

