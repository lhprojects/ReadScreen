﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadScreen
{
    public partial class MainForm : Form
    {
        IFitter fitter = new Fitter();
        ImageForm fImageForm;
        public MainForm()
        {
            InitializeComponent();
            postInit();
        }

        public void OnFitterChanged()
        {
            CalDataGrid.Rows.Clear();
            RetGridData.Rows.Clear();
            for (int i = 0; i < fitter.fCal.Count; ++i)
            {
                var cal = fitter.fCal.ElementAt(i);
                CalDataGrid.Rows.Add(
                    String.Format("C{0}", i),
                    String.Format("{0}", cal.Ps.X),
                    String.Format("{0}", cal.Ps.Y),
                    cal.fX,
                    cal.fY
                    );
            }
            for (int i = 0; i < fitter.fResults.Count; ++i)
            {
                var result = fitter.fResults.ElementAt(i);
                RetGridData.Rows.Add(
                    String.Format("R{0}", i),
                    String.Format("{0}", result.Ps.X),
                    String.Format("{0}", result.Ps.Y),
                    result.XValid ? String.Format("{0}", result.X) : null,
                    result.YValid ? String.Format("{0}", result.Y) : null
                    );
            }
        }

        void postInit()
        {
            XAxisType.SelectedIndex = 0;
            YAxisType.SelectedIndex = 0;
            XPixels.ReadOnly = true;
            YPixels.ReadOnly = true;
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn5.ReadOnly = true;
            CalDataGrid.UserDeletedRow += CalDataGrid_UserDeletedRow;
            CalDataGrid.UserDeletingRow += CalDataGrid_UserDeletingRow;
            CalDataGrid.CellValueChanged += CalDataGrid_CellValueChanged;
            RetGridData.UserDeletedRow += RetGridData_UserDeletedRow;
            RetGridData.UserDeletingRow += RetGridData_UserDeletingRow;            
        }

        int RetGridDataDeleting;
        private void RetGridData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            RetGridDataDeleting = e.Row.Index;
        }

        private void RetGridData_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            fitter.RmResIdx(RetGridDataDeleting);
            this.OnFitterChanged();
            fImageForm.OnFitterChanged();
        }

        private void CalDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            var value = view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            String newValue = value == null ? null : value.ToString();
 
            var t = fitter.fCal[e.RowIndex];
            if (e.ColumnIndex == 3)
            {
                t.fX = newValue;
            }
            else if (e.ColumnIndex == 4)
            {
                t.fY = newValue;
            }
        }

        int deletingIndex;
        private void CalDataGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            deletingIndex = e.Row.Index;
        }

        private void CalDataGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            fitter.RmCalIdx(deletingIndex);
            this.OnFitterChanged();
            fImageForm.OnFitterChanged();
        }

        void SetImagePath(String path)
        {
            if(fImageForm != null)
            {
                fImageForm.Close();
                fImageForm = null;
            }
            fImageForm =  new ImageForm();
            fImageForm.postInit(path, fitter, this);
            fImageForm.Show();

            TxtImagePath.Text = path;
        }

        private void BntOpen_Click(object sender, EventArgs e)
        {
            if (TxtImagePath.Text != null && !TxtImagePath.Text.Equals(""))
            {
                SetImagePath(TxtImagePath.Text);
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Title = "Open Image";
                dlg.Filter = "Image Files|*.bmp; *.png; *.jpeg|All Files|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SetImagePath(dlg.FileName);
                    BntOpen.Text = "Open Viewer";
                }
            }
        }

        private void XAxisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fitter.fLogX = ((ComboBox)sender).SelectedIndex == 0 ? false : true;
        }

        private void YAxisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fitter.fLogY = ((ComboBox)sender).SelectedIndex == 0 ? false : true;
        }
        
        private void BtnCal_Click(object sender, EventArgs e)
        {
            String str = fitter.Evaluate();
            TxtFormula.Text = str;
            TxtXYAngle.Text = String.Format("{0}", fitter.fXYAngle);
            if(str.Equals("Done"))
            {
                this.OnFitterChanged();
            }
        }

        private void Variables_Click(object sender, EventArgs e)
        {
            if(fitter != null)
            {
                var dialog = new VariablesDialog(fitter);
                dialog.ShowDialog(this);
            }
        }

        private void CalDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
