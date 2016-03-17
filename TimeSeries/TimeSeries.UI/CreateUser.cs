using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TimeSeries.Core.DataAccess;
using TimeSeries.Core.Helper;
using TimeSeries.Core.Model;

namespace TimeSeries.UI
{
    public partial class CreateUser : Form
    {
        PasswordValidator _pwdValidator;
        EmailValidator _emailValidator;
        public CreateUser()
        {
            InitializeComponent();
            _pwdValidator = new PasswordValidator();
            _emailValidator = new EmailValidator();

        }


        private void txtLogin_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtLogin.Text))
            {
                errorProvidenrLog.SetError(txtLogin, "Please enter login");
            }
            else
            {
                errorProvidenrLog.Clear();
            }
        }

        private void txtPwd_Validating(object sender, CancelEventArgs e)
        {
            string error = _pwdValidator.Valide(txtPwd.Text);
            errorProvidenrLog.SetError(txtPwd, error);
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!_emailValidator.Valide(txtEmail.Text))
            {
                errorProvidenrLog.SetError(txtEmail, "Email incorrect");
            }
            else
            {
                errorProvidenrLog.SetError(txtEmail, "");
            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                errorProvidenrLog.SetError(txtFirstName, "Firts name is not empty");
            }
            else
            {
                errorProvidenrLog.SetError(txtFirstName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                errorProvidenrLog.SetError(txtLastName, "Last name is not empty");
            }
            else
            {
                errorProvidenrLog.SetError(txtLastName, "");
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (!ValideTextBoxes())
            {
                MessageBox.Show("Please fill the required field");
            }
            else
            {
                User user = new User();
                user.Login = txtLogin.Text;
                user.Password = txtLogin.Text;
                user.FirstName = txtFirstName.Text;
                user.LastName = txtLastName.Text;
                user.Email = txtEmail.Text;

                //TODO: Save into database
                using (var db = new TimeSeriesContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }

        }

        bool ValideTextBoxes()
        {
            var textBoxes = Controls.OfType<TextBox>();

            foreach (var control in textBoxes)
            {
                if (!String.IsNullOrEmpty(errorProvidenrLog.GetError(control)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
