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
    public partial class frmUserInfo : Form
    {
        private int _UserID;

        public frmUserInfo(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }

        private void ctrlUserCard1_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadPersonInfo(_UserID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
