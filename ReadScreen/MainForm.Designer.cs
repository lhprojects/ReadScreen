namespace ReadScreen
{
    partial class MainForm
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
            this.BntOpen = new System.Windows.Forms.Button();
            this.TxtImagePath = new System.Windows.Forms.TextBox();
            this.GrpCal = new System.Windows.Forms.GroupBox();
            this.Variables = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtFormula = new System.Windows.Forms.TextBox();
            this.TxtXYAngle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnCal = new System.Windows.Forms.Button();
            this.CalDataGrid = new System.Windows.Forms.DataGridView();
            this.Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XPixels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YPixels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XAxisType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.YAxisType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RetGridData = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrpCal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalDataGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RetGridData)).BeginInit();
            this.SuspendLayout();
            // 
            // BntOpen
            // 
            this.BntOpen.Location = new System.Drawing.Point(31, 5);
            this.BntOpen.Name = "BntOpen";
            this.BntOpen.Size = new System.Drawing.Size(169, 27);
            this.BntOpen.TabIndex = 0;
            this.BntOpen.Text = "Open File";
            this.BntOpen.UseVisualStyleBackColor = true;
            this.BntOpen.Click += new System.EventHandler(this.BntOpen_Click);
            // 
            // TxtImagePath
            // 
            this.TxtImagePath.Location = new System.Drawing.Point(206, 5);
            this.TxtImagePath.Name = "TxtImagePath";
            this.TxtImagePath.ReadOnly = true;
            this.TxtImagePath.Size = new System.Drawing.Size(410, 27);
            this.TxtImagePath.TabIndex = 1;
            // 
            // GrpCal
            // 
            this.GrpCal.Controls.Add(this.Variables);
            this.GrpCal.Controls.Add(this.label6);
            this.GrpCal.Controls.Add(this.TxtFormula);
            this.GrpCal.Controls.Add(this.TxtXYAngle);
            this.GrpCal.Controls.Add(this.label5);
            this.GrpCal.Controls.Add(this.BtnCal);
            this.GrpCal.Controls.Add(this.CalDataGrid);
            this.GrpCal.Location = new System.Drawing.Point(23, 109);
            this.GrpCal.Name = "GrpCal";
            this.GrpCal.Size = new System.Drawing.Size(593, 320);
            this.GrpCal.TabIndex = 2;
            this.GrpCal.TabStop = false;
            this.GrpCal.Text = "Calibration";
            // 
            // Variables
            // 
            this.Variables.Location = new System.Drawing.Point(134, 273);
            this.Variables.Name = "Variables";
            this.Variables.Size = new System.Drawing.Size(88, 34);
            this.Variables.TabIndex = 8;
            this.Variables.Text = "Variables";
            this.Variables.UseVisualStyleBackColor = true;
            this.Variables.Click += new System.EventHandler(this.Variables_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(407, 283);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Result";
            // 
            // TxtFormula
            // 
            this.TxtFormula.Location = new System.Drawing.Point(470, 280);
            this.TxtFormula.Name = "TxtFormula";
            this.TxtFormula.ReadOnly = true;
            this.TxtFormula.Size = new System.Drawing.Size(99, 27);
            this.TxtFormula.TabIndex = 6;
            // 
            // TxtXYAngle
            // 
            this.TxtXYAngle.Location = new System.Drawing.Point(316, 280);
            this.TxtXYAngle.Name = "TxtXYAngle";
            this.TxtXYAngle.ReadOnly = true;
            this.TxtXYAngle.Size = new System.Drawing.Size(81, 27);
            this.TxtXYAngle.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "XYAngle";
            // 
            // BtnCal
            // 
            this.BtnCal.Location = new System.Drawing.Point(17, 273);
            this.BtnCal.Name = "BtnCal";
            this.BtnCal.Size = new System.Drawing.Size(101, 34);
            this.BtnCal.TabIndex = 1;
            this.BtnCal.Text = "Calibrate";
            this.BtnCal.UseVisualStyleBackColor = true;
            this.BtnCal.Click += new System.EventHandler(this.BtnCal_Click);
            // 
            // CalDataGrid
            // 
            this.CalDataGrid.AllowUserToAddRows = false;
            this.CalDataGrid.AllowUserToResizeColumns = false;
            this.CalDataGrid.AllowUserToResizeRows = false;
            this.CalDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Point,
            this.XPixels,
            this.YPixels,
            this.X,
            this.Y});
            this.CalDataGrid.Location = new System.Drawing.Point(17, 42);
            this.CalDataGrid.Name = "CalDataGrid";
            this.CalDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CalDataGrid.RowTemplate.Height = 27;
            this.CalDataGrid.Size = new System.Drawing.Size(552, 217);
            this.CalDataGrid.TabIndex = 0;
            // 
            // Point
            // 
            this.Point.HeaderText = "Point";
            this.Point.Name = "Point";
            this.Point.ReadOnly = true;
            // 
            // XPixels
            // 
            this.XPixels.HeaderText = "X(Pixels)";
            this.XPixels.Name = "XPixels";
            // 
            // YPixels
            // 
            this.YPixels.HeaderText = "Y(Pixels)";
            this.YPixels.Name = "YPixels";
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // XAxisType
            // 
            this.XAxisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.XAxisType.FormattingEnabled = true;
            this.XAxisType.Items.AddRange(new object[] {
            "liner",
            "logarithmic"});
            this.XAxisType.Location = new System.Drawing.Point(124, 57);
            this.XAxisType.Name = "XAxisType";
            this.XAxisType.Size = new System.Drawing.Size(121, 28);
            this.XAxisType.TabIndex = 3;
            this.XAxisType.SelectedIndexChanged += new System.EventHandler(this.XAxisType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "X Axis";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y Axis";
            // 
            // YAxisType
            // 
            this.YAxisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.YAxisType.FormattingEnabled = true;
            this.YAxisType.Items.AddRange(new object[] {
            "liner",
            "logarithmic"});
            this.YAxisType.Location = new System.Drawing.Point(372, 58);
            this.YAxisType.Name = "YAxisType";
            this.YAxisType.Size = new System.Drawing.Size(121, 28);
            this.YAxisType.TabIndex = 6;
            this.YAxisType.SelectedIndexChanged += new System.EventHandler(this.YAxisType_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RetGridData);
            this.groupBox2.Location = new System.Drawing.Point(23, 435);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(593, 265);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // RetGridData
            // 
            this.RetGridData.AllowUserToAddRows = false;
            this.RetGridData.AllowUserToResizeColumns = false;
            this.RetGridData.AllowUserToResizeRows = false;
            this.RetGridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RetGridData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.RetGridData.Location = new System.Drawing.Point(17, 24);
            this.RetGridData.Name = "RetGridData";
            this.RetGridData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.RetGridData.RowTemplate.Height = 27;
            this.RetGridData.Size = new System.Drawing.Size(552, 235);
            this.RetGridData.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Point";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "X(Pixels)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Y(Pixels)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "X";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Y";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(638, 712);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.YAxisType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.XAxisType);
            this.Controls.Add(this.GrpCal);
            this.Controls.Add(this.TxtImagePath);
            this.Controls.Add(this.BntOpen);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ReadScreen";
            this.GrpCal.ResumeLayout(false);
            this.GrpCal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalDataGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RetGridData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BntOpen;
        private System.Windows.Forms.TextBox TxtImagePath;
        private System.Windows.Forms.GroupBox GrpCal;
        private System.Windows.Forms.ComboBox XAxisType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox YAxisType;
        private System.Windows.Forms.DataGridView CalDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Point;
        private System.Windows.Forms.DataGridViewTextBoxColumn XPixels;
        private System.Windows.Forms.DataGridViewTextBoxColumn YPixels;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtFormula;
        private System.Windows.Forms.TextBox TxtXYAngle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnCal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView RetGridData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button Variables;
    }
}

