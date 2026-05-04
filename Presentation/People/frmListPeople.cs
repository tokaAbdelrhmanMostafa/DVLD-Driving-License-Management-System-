using Buisness;
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

namespace Presentation.People
{
    public partial class frmListPeople : Form
    {
       private static DataTable _dtAllPeople= clsPerson.GetAllPeople();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable
          (false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "GendorCaption", "DateOfBirth", "CountryName",  "Phone", "Email");
     
        private void _RefreshPeoplList()
        {
             _dtAllPeople = clsPerson.GetAllPeople();
        _dtPeople = _dtAllPeople.DefaultView.ToTable
            (false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "GendorCaption", "DateOfBirth", "CountryName", "Phone", "Email");

            dataGridView1.DataSource = _dtPeople;
        }
        public frmListPeople()
        {
            InitializeComponent();
        }
        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource= _dtPeople;
            comboBox1.SelectedIndex = 0;
            if (dataGridView1.Rows.Count > 0) {
                dataGridView1.Columns[0].HeaderText = "PersonID";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[1].HeaderText = "NationalNo";
                dataGridView1.Columns[1].Width = 110;
                dataGridView1.Columns[2].HeaderText = "FirstName";
                dataGridView1.Columns[2].Width = 110;
                dataGridView1.Columns[3].HeaderText = "SecondName";
                dataGridView1.Columns[3].Width = 110;
                dataGridView1.Columns[4].HeaderText = "ThirdName";
                dataGridView1.Columns[4].Width = 110;
                dataGridView1.Columns[5].HeaderText = "LastName";
                dataGridView1.Columns[5].Width = 110;
                dataGridView1.Columns[6].HeaderText = "GendorCaption";
                dataGridView1.Columns[6].Width = 110;
                dataGridView1.Columns[7].HeaderText = "DateOfBirth";
                dataGridView1.Columns[7].Width = 110;
                dataGridView1.Columns[8].HeaderText = "CountryName";
                dataGridView1.Columns[8].Width = 110;
                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[9].Width = 110;
                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[10].Width = 170;

            }        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource= _dtPeople;
            comboBox1.SelectedIndex = 0;
            if (dataGridView1.Rows.Count > 0) {
                dataGridView1.Columns[0].HeaderText = "PersonID";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "NationalNo";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "FirstName";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "SecondName";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "ThirdName";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "LastName";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "GendorCaption";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "DateOfBirth";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "CountryName";
                dataGridView1.Columns[0].Width = 140;
                dataGridView1.Columns[0].HeaderText = "phone";
                dataGridView1.Columns[0].Width = 110;
                dataGridView1.Columns[0].HeaderText = "Email";
                dataGridView1.Columns[0].Width = 170;
               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (comboBox1.Text)
            {
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;
                case "DateOfBirth":
                    FilterColumn = "DateOfBirth";
                    break;
                case "CountryName":
                    FilterColumn = "CountryName";

                    break;



                case "PersonID":
                    FilterColumn = "PersonID";
                    break;
                case "NationalNo":
                    FilterColumn = "NationalNo";
                    break;
                case "FirstName":
                    FilterColumn = "FirstName";
                    break;
                case "SecondName":
                    FilterColumn = "SecondName";
                    break;
                case "ThirdName":
                    FilterColumn = "ThirdName";
                    break;
                case "LastName":
                    FilterColumn = "LastName";
                    break;
                default:
                    FilterColumn = "None";
                    break;


            }

            if(textBox1.Text.Trim()=="" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                return;
            }
            if (FilterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = String.Format("[{0}]={1}",FilterColumn,textBox1.Text.Trim());
             
            }else
                _dtPeople.DefaultView.RowFilter = String.Format("[{0}] LIKE '{1}%'", FilterColumn, textBox1.Text.Trim());


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = (comboBox1.Text !="None");
            if (textBox1.Visible)
            {
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
       frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure ?? ","conform",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                if (clsPerson.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Done");
                    _RefreshPeoplList();
                }
                else
                    MessageBox.Show("can't do that");

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }
    }
}
