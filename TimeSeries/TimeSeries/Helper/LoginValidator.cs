using System.Collections.Generic;
using System.Linq;
using TimeSeries.Core.DataAccess;
using TimeSeries.Core.Model;
using TimeSeries.Core.Security;

namespace TimeSeries.Core
{
    public class LoginError
    {
        public const string UserNotExist = "This user is not exist in system";

        public const string PasswordIncorrect = "This password is incorrect";

    }
    interface ILoginValidator
    {
        string SignIn(string user, string password);
    }
    public class LoginValidator : ILoginValidator
    {
        ICollection<User> _userList;
        ICrypter _crypter;

        public LoginValidator(ICollection<User> users, ICrypter crypter)
        {

            if (users != null)
            {
                _userList = users;
            }
            else
            {
                using (var db = new TimeSeriesContext())
                {
                    _userList = new List<User>(db.Users);
                }
            }
            _crypter = crypter;
        }

        public string SignIn(string username, string password)
        {
            var user = _userList.Where(x => x.Login.Equals(username)).FirstOrDefault();
            if (user == null)
            {
                return LoginError.UserNotExist;
            }
            string cryptedPassword = _crypter.Encrypt(password);

            if (!cryptedPassword.Equals(user.Password))
            {
                return LoginError.PasswordIncorrect;
            }
            return string.Empty;
        }
    }
}
