using Buisness;
using Presentation.Global_Classes;
using Presentation.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplication _Application;
        private int _ApplicationID;
        public int ApplicationID { get { return _ApplicationID; } }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        public void ResetApplicationInfo()
        {
            _ApplicationID=-1;
            id.Text = "???";
            status.Text = "???";
            fees.Text = "???";
            type.Text = "???";
            application.Text = "???";
            date.Text = "???";
            StatusDate.Text = "???";
            CreatedBy.Text = "???";

        }
        private void _FillApplicationInfo()
{
            _ApplicationID = _Application.ApplicationID;
            id.Text = _ApplicationID.ToString();
            status.Text = _Application.StatusText.ToString();
            fees.Text =_Application.PaidFees.ToString();
          type.Text = _Application.ApplicationTypeInfo.Title;
            application.Text = _Application.ApplicantFullName;
           date.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            StatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            CreatedBy.Text = _Application.CreatedByUserInfo.UserName;
        }


        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.FindBaseApplication(ApplicationID);
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                _FillApplicationInfo();

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowPersonInfo(_Application.ApplicationID);

            frm.ShowDialog();
            LoadApplicationInfo(_ApplicationID);
        }
    }
}
