using Buisness;
using Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
      private clsPerson _person;
        private int _PersonID = -1;
        public int personID { 
            get { return _PersonID; } 
        } 
       
        public clsPerson SelectPersonInfo{  get { return _person; } }
        private void _loadImage()
        {
           // if (_person.Gendor == 0) { pictureBox1.Image = Resources.Male_512; }
            //if(_person.Gendor == 1) { pictureBox1.Image = Resources.Female_512; }
            string ImagePath= _person.ImagePath;
            //if (ImagePath != "")
            //{
            //    if (File.Exists(ImagePath))

            //        //pictureBox1.ImageLocation = ImagePath;
            //    else MessageBox.Show("could not find this image");
                
            //}
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            UserNmae.Text = "???";
            ID.Text = "???";
            NationalNo.Text = "???";
            Gender.Text = "???";    
            Email.Text = "???";
            Address.Text = "???";
            DateOfBirth.Text = "???";
            phone.Text = "???";
            Country.Text = "???";
           // pictureBox1.Image= Resources.Male_512;

        }

        public void FillPersonInfo()
        {
            ID.Text = _person.PersonID.ToString();
            UserNmae.Text = _person.FullName;
            NationalNo.Text = _person.NationalNo;
            Gender.Text = _person.Gendor == 0 ? "Male" : "Female";
            Email.Text = _person.Email;
            Address.Text = _person.Address;
            DateOfBirth.Text = _person.DateOfBith.ToString();
            phone.Text = _person.Phone.ToString();
            //  Country.Text = clsCountry.Find(_person.NationalityCountryID).CountryName;
            var country = clsCountry.Find(_person.NationalityCountryID);

            if (country != null)
            {
                Country.Text = country.CountryName;
            }
            else
            {
                Country.Text = "Unknown";
            }
            _loadImage();

        }
        public void LoadPersonInfo(int PersonID)
        {
            _PersonID = PersonID;
            _person = clsPerson.Find(PersonID);

            if (_person == null)
            {
                MessageBox.Show("NOT FOUND"); // 👈
                ResetPersonInfo();
                return;
            }

          //  MessageBox.Show("FOUND"); // 👈
            FillPersonInfo();
        }
        public void LoadPersonInfo(string nationalNo)
        {

            _person = clsPerson.Find(nationalNo);
            if (_person == null)
            {
                ResetPersonInfo();
                return;
            }
            FillPersonInfo();


        }



        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void EditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void ID_Click(object sender, EventArgs e)
        {

        }
    }
}
