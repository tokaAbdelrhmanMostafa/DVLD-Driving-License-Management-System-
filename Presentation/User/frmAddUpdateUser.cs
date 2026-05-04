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

namespace Presentation.User
{
    public partial class frmAddUpdateUser : Form
    {
        public enum enMode { AddNew=0, Update=1}
        private enMode _Mode;
        public int _UserID;
        clsUser _User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _UserID = UserID;
        }
        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew) {
                title.Text = "Add New User";
                this.Text= "Add New User";
                _User= new clsUser();
                tabPage2.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                title.Text = "Update User";
                this.Text = "Update User";
                tabPage2.Enabled = true;
                save.Enabled = true;
            }
            UserName.Text = "";
            Password.Text = "";
            ConformPassword.Text = "";
            checkBox1.Checked = true;
        }
        private void  _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = true;
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

              
            }
            UserId.Text = _User.UserID.ToString();
            UserName.Text = _User.UserName.ToString();
            Password.Text = _User.Password.ToString();
            ConformPassword.Text = _User.Password.ToString();
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if(_Mode== enMode.Update)
            {
                _LoadData();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            _User.PersonID=ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = UserName.Text;
            _User.Password= Password.Text;
            _User.IsActive= checkBox1.Checked;
            if (_User.Save())
            {
                UserId.Text = _User.UserID.ToString();
                title.Text = "Update User";
                this.Text = "Update User";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        
    }

        private void Next_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update) { 
                save.Enabled = true;
                tabPage2.Enabled = true;
                return;
            }
            if(ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.isUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user...");
                    ctrlPersonCardWithFilter1.FilterFocus();
                    return; 
                }
                else
                {
                    save.Enabled = true;
                    tabPage2.Enabled = true;

                }

            }
        }
    }
}
