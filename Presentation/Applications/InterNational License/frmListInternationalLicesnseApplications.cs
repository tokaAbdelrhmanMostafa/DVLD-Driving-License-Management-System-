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

namespace Presentation.Applications.InterNational_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private DataTable _dtInternationalLicenseApplications;

        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = lsInternationalLicense.GetAllInternationalLicenses();
            

            dataGridView1.DataSource = _dtInternationalLicenseApplications;
            //lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Int.License ID";
                dataGridView1.Columns[0].Width = 160;

                dataGridView1.Columns[1].HeaderText = "Application ID";
                dataGridView1.Columns[1].Width = 150;

                dataGridView1.Columns[2].HeaderText = "Driver ID";
                dataGridView1.Columns[2].Width = 130;

                dataGridView1.Columns[3].HeaderText = "L.License ID";
                dataGridView1.Columns[3].Width = 130;

                dataGridView1.Columns[4].HeaderText = "Issue Date";
                dataGridView1.Columns[4].Width = 180;

                dataGridView1.Columns[5].HeaderText = "Expiration Date";
                dataGridView1.Columns[5].Width = 180;

                dataGridView1.Columns[6].HeaderText = "Is Active";
                dataGridView1.Columns[6].Width = 120;

            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
