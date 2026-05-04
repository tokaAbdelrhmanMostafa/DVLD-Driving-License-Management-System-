using Buisness;
using Presentation.People.Controls;
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
    public partial class ctrlUserCard : UserControl
    {
        public clsUser _User;
        private int _UserID = -1;
        public int UserID { get { return _UserID; } }
        private void _ResetPersonInfo()
        {
            ID.Text= "???";
            IsActive.Text = "???";
         Name.Text="???";
        }
        private void _FillPersonInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            ID.Text = _User.UserID.ToString();
            
            Name.Text = _User.UserName;
            if (_User.IsActive) IsActive.Text = "Active";
            else IsActive.Text = "Not Active";
        }
        public void LoadPersonInfo(int PersonID)
        {
            _User = clsUser.FindByUserID(PersonID);
            if (_User == null) _ResetPersonInfo();
            else _FillPersonInfo();



        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
