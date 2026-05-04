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
    public partial class frmEditTestType_ : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;
        public frmEditTestType_(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void frmEditTestType__Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);
            if (_TestType != null) { 
                id.Text= ((int)_TestTypeID).ToString();
                title.Text = _TestType.Title;
                description.Text = _TestType.Description;
                fees.Text = _TestType.Fees.ToString();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            _TestType.Title = title.Text.Trim();
            _TestType.Description = description.Text.Trim();
            _TestType.Fees = Convert.ToSingle(fees.Text.Trim());


            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

    }
    }


