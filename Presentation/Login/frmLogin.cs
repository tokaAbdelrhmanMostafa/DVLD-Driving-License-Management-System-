using Buisness;
using Presentation.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", password = "";
            if (clsGlobal.GetStoredCredential(ref UserName, ref password))
            {
                UserName1.Text = UserName;
                Password1.Text = password;
                checkBox1.Checked= true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void login_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(
                UserName1.Text.Trim(), Password1.Text.Trim());

            if (user != null) {
                if (checkBox1.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(UserName1.Text.Trim(), Password1.Text.Trim());

                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }
                if (!user.IsActive)
                {
                    UserName1.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                UserName1.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
