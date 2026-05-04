using Buisness;
using Buisness.GlobalClasses;
using Presentation.Global_Classes;
using Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Presentation.People.frmAddUpdatePerson;

namespace Presentation.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

      
        public event DataBackEventHandler DataBack;
        public enum enMode { Add = 0, Update = 1 }
        public enum enGendor { Male = 0, Female = 1 }
        private enMode _Mode=enMode.Add;
        private enGendor _Gendor;
        clsPerson _person;
        private int _PersonID = -1;
        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            comboBox1.DataSource = dtCountries;
            comboBox1.DisplayMember = "CountryName";   // اللي يظهر
            comboBox1.ValueMember = "CountryID";       // القيمة الحقيقية
        }
        private void _ResetDefaultValues()
        {
            _FillCountriesInComoboBox();
            if (_Mode == enMode.Add)
            {
                label1.Text = "Add new Person";
                _person = new clsPerson();

            }
            else { label1.Text = "Update person"; }
            //if (male.Checked) { pictureBox1.Image = Resources.Male_512; }
            //else pictureBox1.Image = Resources.Female_512;
            //LnRemove.Visible = (pictureBox1.Image != null);
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            comboBox1.SelectedIndex = comboBox1.FindString("Egypt");
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxSecondName.Text = "";
            textBoxThirdName.Text = "";
            textBoxNationalNumber.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
            TextPhone.Text = "";
            male.Checked = true;
        }


        private void _LoadData()
        {
            _person = clsPerson.Find(_PersonID);
            _person.Mode = clsPerson.enMode.UpdateMode;
            if (_person == null) {
                MessageBox.Show("No person Find");
                this.Close();
                return;
            }
            ID.Text = _person.PersonID.ToString();
            textBoxFirstName.Text = _person.FirstName;
            textBoxSecondName.Text = _person.SecondName;
            textBoxThirdName.Text = _person.ThirdName;
            textBoxLastName.Text = _person.LastName;
            textBoxNationalNumber.Text = _person.NationalNo.ToString();
            textBoxEmail.Text = _person.Email;
            textBoxAddress.Text = _person.Address;
            TextPhone.Text = _person.Phone;
           // comboBox1.SelectedIndex = comboBox1.FindString(_person.CountryInfo.CountryName);
            if (_person.Gendor == 0) male.Checked = true;
            else female.Checked = true;
            //if (_person.ImagePath != "") {
            //    pictureBox1.ImageLocation = _person.ImagePath; }
            //LnRemove.Visible = (_person.ImagePath != "");
            //if (comboBox1.SelectedItem == null)
            //{
            //    MessageBox.Show("Please select a country");
            //    return;
            //}
            comboBox1.SelectedValue = _person.NationalityCountryID;
            var country = clsCountry.Find(comboBox1.SelectedItem.ToString());

            //if (country == null)
            //{
            //    MessageBox.Show("Invalid country");
            //    return;
            //}

        }
        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.Add;

        }
        public frmAddUpdatePerson(int ID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            
            _PersonID = ID;

        }

        //private bool _HandlePersonImage()
        //{
        //    if (_person.ImagePath != pictureBox1.ImageLocation)
        //    {
        //        if (_person.ImagePath != "")
        //        {
        //            try {
        //                File.Delete(_person.ImagePath);
        //            }
        //            catch { }
        //        }

        //        //if (pictureBox1.ImageLocation != null)
        //        //{
        //        //    string SourceImageFile = pictureBox1.ImageLocation.ToString();
        //        //    if (Util.CopyImageToProjectImagesFolder(ref SourceImageFile))
        //        //    {
        //        //        pictureBox1.ImageLocation = SourceImageFile;
        //        //        return true;
        //        //    }
        //        //    else
        //        //    {
        //        //        //MessageBox.Show("Error");
        //        //        return false;
        //        //    }
        //        //}
        
            
        //    }
        //    return true;
        //}
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update) { _LoadData(); }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_person == null)
            {
                MessageBox.Show("No Person...");
                return;
            }

            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("No country selected");
                return;
            }

            int NationalCountryID = (int)comboBox1.SelectedValue;

            _person.FirstName = textBoxFirstName.Text.Trim();
            _person.SecondName = textBoxSecondName.Text.Trim();
            _person.ThirdName = textBoxThirdName.Text.Trim();
            _person.LastName = textBoxLastName.Text.Trim();
            _person.NationalNo = textBoxNationalNumber.Text.Trim();
            _person.Email = textBoxEmail.Text.Trim();
            _person.Phone = TextPhone.Text.Trim();
            _person.Address = textBoxAddress.Text.Trim();

            _person.Gendor = male.Checked ? (short)enGendor.Male : (short)enGendor.Female;

            _person.NationalityCountryID = NationalCountryID;

            //_person.ImagePath = pictureBox1.ImageLocation ?? "";

            if (_person.Save())
            {
                MessageBox.Show("Data Saved Successfully");
                btnSave.Enabled = false;
                DataBack?.Invoke(this, _person.PersonID);
            }
            else
            {
              MessageBox.Show("Error");
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.FilterIndex = 1;
            //openFileDialog1.RestoreDirectory = true;

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string selectedFilePath = openFileDialog1.FileName;
            //    pictureBox1.Load(selectedFilePath);
            //    LnRemove.Visible = true;

            //}
        }

        private void LnRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //pictureBox1.Image = null;
            //if (male.Checked) pictureBox1.Image = Resources.Male_512;
            //else pictureBox1.Image = Resources.Female_512;
            //LnRemove.Visible = false;
        }

        private void female_CheckedChanged(object sender, EventArgs e)
        {
          //  if (pictureBox1.Image == null) pictureBox1.Image = Resources.Female_512;

        }

        //private void LinkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
        //    openFileDialog1.FilterIndex = 1;
        //    openFileDialog1.RestoreDirectory = true;
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        string selectedFilePath = openFileDialog1.FileName;
        //      //  pictureBox1.Load(selectedFilePath);

        //    }
        //}

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNationalNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // _FillCountriesInComoboBox();
        }

        private void textBoxNationalNumber_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNationalNumber.Text.Trim()))
            {
                errorProvider1.SetError(textBoxNationalNumber, "This Failed Is Reuired");
                return;
            }
            else
            {
                errorProvider1.SetError(textBoxNationalNumber, null);
            }
            if (textBoxNationalNumber.Text.Trim() != _person.NationalNo && clsPerson.isPersonExist(textBoxNationalNumber.Text.Trim()))
            {
                errorProvider1.SetError(textBoxNationalNumber, "this number aleardy taken");
            }

        }

        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxEmail.Text.Trim() == "")
                return;
            if (!clsValidatoin.ValidateEmail(textBoxEmail.Text))
            {
                errorProvider1.SetError(textBoxEmail, "invliad");
            }
            else
            {
                errorProvider1.SetError(textBoxEmail, null);
            }
            ;
        }

        private void male_CheckedChanged(object sender, EventArgs e)
        {
            //if (pictureBox1.ImageLocation == null)
            //{
            //    pictureBox1.Image = Resources.Male_512;
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
