using NUnit.Framework;
using TimeSeries.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace TimeSeries.Core.Model.Tests
{
    [TestFixture()]
    public class UserTests
    {
        [Test()]
        public void UserTest()
        {
            User user = new User();
            user.Login.Should().BeNullOrEmpty();
            user.Login = "admin";
            user.Login.Should().Be("admin");
            user.Password = "abc";
            user.Password.Should().Be("abc");

        }
    }
}