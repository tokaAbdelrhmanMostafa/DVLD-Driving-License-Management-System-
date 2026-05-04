using Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Applications.Applications_Type
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            if (_ApplicationType != null) {
                id.Text = _ApplicationType.ID.ToString();
                title.Text = _ApplicationType.Title.ToString();
                fees.Text = _ApplicationType.Fees.ToString();

            }

        }

        private void save_Click(object sender, EventArgs e)
        {
            _ApplicationType.Title = title.Text.Trim();
            _ApplicationType.Fees = Convert.ToSingle(fees.Text.Trim());

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Done");
            }
            else { MessageBox.Show("Error"); }
        }

        private void title_TextChanged(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
