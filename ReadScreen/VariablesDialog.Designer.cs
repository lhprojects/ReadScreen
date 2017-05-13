namespace ReadScreen
{
    partial class VariablesDialog
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
            this.VariablesNameValue = new System.Windows.Forms.DataGridView();
            this.VariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.VariablesNameValue)).BeginInit();
            this.SuspendLayout();
            // 
            // VariablesNameValue
            // 
            this.VariablesNameValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VariablesNameValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VariableName,
            this.VariableValue});
            this.VariablesNameValue.Location = new System.Drawing.Point(12, 12);
            this.VariablesNameValue.Name = "VariablesNameValue";
            this.VariablesNameValue.RowTemplate.Height = 27;
            this.VariablesNameValue.Size = new System.Drawing.Size(320, 248);
            this.VariablesNameValue.TabIndex = 0;
            this.VariablesNameValue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VariablesNameValue_CellContentClick);
            this.VariablesNameValue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.VariablesNameValue_CellValueChanged);
            this.VariablesNameValue.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.VariablesNameValue_UserAddedRow);
            this.VariablesNameValue.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.VariablesNameValue_UserDeletedRow);
            // 
            // VariableName
            // 
            this.VariableName.FillWeight = 120F;
            this.VariableName.HeaderText = "Name";
            this.VariableName.Name = "VariableName";
            this.VariableName.Width = 120;
            // 
            // VariableValue
            // 
            this.VariableValue.FillWeight = 150F;
            this.VariableValue.HeaderText = "Initial Value";
            this.VariableValue.Name = "VariableValue";
            this.VariableValue.Width = 150;
            // 
            // VariablesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 272);
            this.Controls.Add(this.VariablesNameValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VariablesDialog";
            this.Text = "Variables";
            this.Load += new System.EventHandler(this.VariablesDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VariablesNameValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView VariablesNameValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableValue;
    }
}