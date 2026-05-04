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

namespace Presentation.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action <int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action <int>  handler = OnPersonSelected;
            if(handler != null)
            {
                handler(PersonID);
            }
        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set { 
                _ShowAddPerson = value;
                AddNew.Visible = _ShowAddPerson;
            }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled=value;
                groupBox1.Enabled = _FilterEnabled;
            }
        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        private int _PersonID;
        public int PersonID
        {
            get { return ctrlPersonCard1.personID; }

        }
        public clsPerson SelectedPersonInfo { get { return ctrlPersonCard1.SelectPersonInfo; } }

        public void LoadPersonInfo(int PersonID)
        {
            comboBox1.SelectedIndex = 1;
            textBox1.Text= PersonID.ToString();
            FindNow();
        }
        private void FindNow()
        {
            switch (comboBox1.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(textBox1.Text)); 
                    break;
                case "National No.":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(textBox1.Text));
                    break;
                default:
                    break;


            }
            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrlPersonCard1.personID);
        }
        public void FilterFocus() { textBox1.Focus(); }
        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            FindNow();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            comboBox1.SelectedIndex = 1;
            textBox1.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        private void AddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13) { AddNew.PerformClick(); }
            if(comboBox1.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
