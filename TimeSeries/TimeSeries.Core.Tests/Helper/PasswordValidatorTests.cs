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
    public class PasswordValidatorTests
    {
        PasswordValidator _validator;

        [SetUp]
        public void Init()
        {
            _validator = new PasswordValidator();
        }
        [Test()]
        public void Should_reject_password_have_lower_than_characters()
        {
            _validator.Valide("abc").Should().Be(PasswordError.AtLeast6Characters);
        }

        [Test()]
        public void Should_reject_password_have_special_characters()
        {
            _validator.Valide("abc12$4").Should().Be(PasswordError.CompositionPassword);
        }


        [Test()]
        public void Should_reject_password_have_only_letter()
        {
            _validator.Valide("abcxycys").Should().Be(PasswordError.CompositionPassword);
        }

        [Test()]
        public void Should_reject_password_have_only_number()
        {
            _validator.Valide("123456789").Should().Be(PasswordError.CompositionPassword);
        }


        [Test()]
        public void Should_accept_good_password()
        {
            _validator.Valide("1abc6789").Should().BeNullOrEmpty();
        }

    }
}