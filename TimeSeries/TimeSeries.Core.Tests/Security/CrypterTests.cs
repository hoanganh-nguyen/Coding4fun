using NUnit.Framework;
using TimeSeries.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace TimeSeries.Core.Security.Tests
{
    [TestFixture()]
    public class CrypterTests
    {

        [Test()]
        public void DecryptTest()
        {
            var _crypt = new Crypter( "Tuti");
            var tmp = _crypt.Encrypt("abc");
            _crypt.Decrypt(tmp).Should().Be("abc");
        }


    }
}