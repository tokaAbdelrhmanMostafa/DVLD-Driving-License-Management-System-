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
    public partial class frmChangePassword : Form
    {
        private int _IserID;
        private clsUser _User;

        public frmChangePassword(int IserID)
        {
            InitializeComponent();
            _IserID = IserID;
        }
        private void _ResetDefualtValues()
        {
            CurrentPaasword.Text = "";
            NewPaasword.Text = "";
            ConformPassword.Text = "";
            CurrentPaasword.Focus();
        }

        private void ctrlUserCard1_Load(object sender, EventArgs e)
        {

        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUser.FindByUserID(_IserID);
            if(_User == null)
            {
                MessageBox.Show("No User Found");
                return;
            }
            ctrlUserCard1.LoadPersonInfo(_IserID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CurrentPaasword_TextChanged(object sender, EventArgs e)
        {

           
        }

        private void NewPaasword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewPaasword.Text.Trim()))
            {
                errorProvider1.SetError(NewPaasword, "Error");
            }
        }

        private void ConformPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPaasword.Text.Trim()))
            {
                errorProvider1.SetError(CurrentPaasword, "Error");
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = NewPaasword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                   "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
