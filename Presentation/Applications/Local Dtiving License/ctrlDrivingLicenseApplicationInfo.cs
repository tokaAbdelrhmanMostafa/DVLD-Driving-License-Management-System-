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

namespace Presentation.Applications.Local_Dtiving_License
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
private int _LicenseID;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            //     _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            //       linkLabel1.Enabled = _LicenseID != -1;
            //       //  lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            //       lblLocalDrivingLicenseApplicationID.Text =
            // _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            //       //lblAppliedFor.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplicationID).ClassName;
            //       lblAppliedFor.Text = clsLicenseClass
            //.Find(_LocalDrivingLicenseApplication.LicenseClassID)
            //.ClassName;
            //       PaasesTest.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString()+"/3";
            //       ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            linkLabel1.Enabled = (_LicenseID != -1);

            lblLocalDrivingLicenseApplicationID.Text =
                _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

            lblAppliedFor.Text =
                clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;

            PaasesTest.Text =
                _LocalDrivingLicenseApplication.GetPassedTestCount().ToString() + "/3";

            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);


        }
        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "???";
            lblAppliedFor.Text = "???";//
        }
        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            //_LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(LocalDrivingLicenseApplicationID);
            //if( _LocalDrivingLicenseApplication != null) {
            //    MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
               // MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID);
                return;
            }

            _FillLocalDrivingLicenseApplicationInfo();
        }
        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null) {
                _ResetLocalDrivingLicenseApplicationInfo();


              //  MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();


        }
        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            { _ResetLocalDrivingLicenseApplicationInfo();
              //  MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillLocalDrivingLicenseApplicationInfo();

        }
        private void lblAppliedFor_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }
    }
}
