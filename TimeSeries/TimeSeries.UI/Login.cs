using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeSeries.Core;
using TimeSeries.Core.Security;

namespace TimeSeries.UI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtUserName_Validated(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider.SetError(txtUserName, "Please enter your login");

            }
            else
            {
                errorProvider.SetError(txtUserName, "");
            }
        }

        private void txtPwd_Validated(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPwd.Text))
            {
                errorProvider.SetError(txtPwd, "Please enter your password");

            }
            else
            {
                errorProvider.SetError(txtPwd, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtUserName.Text) || String.IsNullOrEmpty(txtPwd.Text))
            {
                MessageBox.Show("Please enter your login&password before login");
            }
            else
            {
                string user = txtUserName.Text;
                string pwd = txtPwd.Text;

                LoginValidator validator = new LoginValidator(null, new Crypter());
                string error = validator.SignIn(user, pwd);
                if (String.IsNullOrEmpty(error))
                {
                    DataUpload dtUpload = new DataUpload(user);
                    dtUpload.ShowDialog();
                }
                else
                {
                    MessageBox.Show(error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateUser createUserForm = new CreateUser();
            createUserForm.ShowDialog();
        }
    }
}
