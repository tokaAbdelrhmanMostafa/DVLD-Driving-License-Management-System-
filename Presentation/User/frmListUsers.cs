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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Presentation.User
{
  

    public partial class frmListUsers : Form
    {
        private static   DataTable _dtAllUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {

        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
           dataGridView1.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;

            dataGridView1.Columns[0].HeaderText = "User ID";
            dataGridView1.Columns[0].Width = 110;


            dataGridView1.Columns[1].HeaderText = "Person ID";
            dataGridView1.Columns[1].Width = 120;

            dataGridView1.Columns[2].HeaderText = "Full Name";
            dataGridView1.Columns[2].Width = 350;

            dataGridView1.Columns[3].HeaderText = "UserName";
            dataGridView1.Columns[3].Width = 120;

            dataGridView1.Columns[4].HeaderText = "Is Active";
            dataGridView1.Columns[4].Width = 120;
           

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text== "Is Active")
            {
              textBox1.Visible= false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
            }
            else
            {
                textBox1.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible=false;
                if (cbFilterBy.Text == "None")
                {
                    textBox1.Enabled=false;
                }
                else
                {
                    textBox1.Enabled = true;
                    textBox1.Text = "";
                }
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue= cbIsActive.Text;
            switch (FilterValue)
            {
                case "All":break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;

            }
            if (FilterValue == "All")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, FilterValue);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string TextFilter = "";
            switch (cbFilterBy.Text)
            {
                case "IsActive":
                    TextFilter = "IsActive";
                    break;
              
                case "UserName":
                    TextFilter = "UserName";
                    break;
                case "Person ID":
                    TextFilter = "PersonID";
                    break;
                case "User ID":
                    TextFilter = "UserID";
                    break;
                case "Full Name":
                    TextFilter = "FullName";break;
                default:
                    TextFilter = "None";
                    break;
            }
            if (textBox1.Text.Trim() == "" || TextFilter == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
              
                return;
            }


            if (TextFilter != "FullName" && TextFilter != "UserName")
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", TextFilter, textBox1.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", TextFilter, textBox1.Text.Trim());

            
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void AddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
