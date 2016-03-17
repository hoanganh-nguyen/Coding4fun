using NUnit.Framework;
using TimeSeries.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSeries.Core.Security;
using TimeSeries.Core.Model;
using FluentAssertions;
using NSubstitute;

namespace TimeSeries.Core.Tests
{
    [TestFixture()]
    public class LoginValidatorTests
    {
        LoginValidator _login;
        ICrypter _crypt;
        List<User> _userList;

        [SetUp]
        public void Init()
        {
            _crypt = NSubstitute.Substitute.For<ICrypter>();
            _userList = new List<User>();
            _userList.Add(new User() { Login = "admin", Password = "admin1234", FirstName = "Admin", LastName = "Reuters", Email = "abc@reuters.com" });
            _login = new LoginValidator(_userList, _crypt);

        }

        [Test]
        public void Should_reject_inexistant_user()
        {
            //Act
            var tmp = _login.SignIn("abc", "xyw");

            //Assert 
            tmp.Should().NotBeNullOrEmpty();
            tmp.Should().Be(LoginError.UserNotExist);
        }



        [Test]
        public void Should_reject_incorrect_password()
        {
            //Arrange
            _crypt.Encrypt("toto").Returns("admin123");
            //Act
            var tmp = _login.SignIn("admin", "toto");
            //Assert 
            tmp.Should().NotBeNullOrEmpty();
            tmp.Should().Be(LoginError.PasswordIncorrect);
        }

        [Test]
        public void Should_accept_good_user_and_good_password()
        {
            //Arrange
            _crypt.Encrypt("turo").Returns("admin1234");
            //Act
            var tmp = _login.SignIn("admin", "turo");
            //Assert 
            tmp.Should().BeNullOrEmpty();
          
        }

    }
}