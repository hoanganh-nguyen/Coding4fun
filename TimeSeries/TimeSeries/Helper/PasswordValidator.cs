using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TimeSeries.Core.Helper
{
    public class PasswordError
    {
        public const string AtLeast6Characters = "Password must have at least 6 characters";
        public const string CompositionPassword = "Password must have at least 1 letter, 1 number and alphanumerical";
    }
    interface IPasswordValidator
    {
        string Valide(string pwd);
    }
    public class PasswordValidator : IPasswordValidator
    {
        public string Valide(string pwd)
        {
            if(string.IsNullOrEmpty(pwd)||pwd.Length <6)
            {
                return PasswordError.AtLeast6Characters;
            }
            Regex reg = new Regex(@"(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)");
            if(!reg.IsMatch(pwd))
            {
                return PasswordError.CompositionPassword;
            }

            return string.Empty;
        }
    }
}
