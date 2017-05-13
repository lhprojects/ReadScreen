using System;
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
    public partial class VariablesDialog : Form
    {
        public IFitter fFitter;
        static bool initing;
        public VariablesDialog(IFitter fitter)
        {
            initing = true;
            fFitter = fitter;
            InitializeComponent();


            DataGridView view = VariablesNameValue;
            view.Rows.Clear();
            foreach (var x in fFitter.fInitialValues)
            {
                view.Rows.Add(x.Key, x.Value.ToString());
            }
            initing = false;

        }
        private void OnCellChanged(object sender)
        {
            if (initing) return;
            DataGridView view = (DataGridView)sender;

            var d = new Dictionary<String, double>();
            for (int i = 0; i < view.RowCount; ++i)
            {
                try
                {
                    var n = view.Rows[i].Cells[0].Value;
                    if (n != null)
                    {
                        var name = n == null ? null : n.ToString();
                        var v = view.Rows[i].Cells[1].Value;
                        double value = Convert.ToDouble(v);
                        d.Add(name, value);
                    }

                }
                catch (Exception)
                {
                }
            }
            fFitter.fInitialValues = d;
        }

        private void VariablesNameValue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            OnCellChanged(sender);
        }

        private void VariablesNameValue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void VariablesNameValue_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void VariablesNameValue_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            OnCellChanged(sender);
        }

        private void VariablesDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
