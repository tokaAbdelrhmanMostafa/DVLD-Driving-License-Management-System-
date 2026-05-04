using Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Test.Test_Types
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            // _dtAllTestTypes = ;
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            dataGridView1.DataSource = _dtAllTestTypes;
          //  lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

           dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 120;

            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[1].Width = 200;

            dataGridView1.Columns[2].HeaderText = "Description";
            dataGridView1.Columns[2].Width = 400;

            dataGridView1.Columns[3].HeaderText = "Fees";
            dataGridView1.Columns[3].Width = 100;

        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditTestType_((clsTestType.enTestType)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
