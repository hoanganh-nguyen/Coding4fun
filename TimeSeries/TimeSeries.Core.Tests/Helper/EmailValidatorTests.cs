using NUnit.Framework;
using TimeSeries.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace TimeSeries.Core.Helper.Tests
{
    [TestFixture()]
    public class EmailValidatorTests
    {
        EmailValidator _validator = new EmailValidator();

        [Test()]
        [TestCase("hello@abc.com")]
        [TestCase("hello@abc.c")]
        public void Should_accept_good_email(string email)
        {
            _validator.Valide(email).Should().BeTrue();
        }

        [Test()]
        [TestCase("hello@abc")]
        [TestCase("hello@abc@")]
        public void Should_reject_bad_email_format(string email)
        {
            _validator.Valide(email).Should().BeFalse();
        }
    }
}