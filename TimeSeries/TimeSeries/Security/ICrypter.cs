using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSeries.Core.Security
{
    public interface ICrypter
    {
        string Encrypt(string entry);
        string Decrypt(string entry);
    }
}
