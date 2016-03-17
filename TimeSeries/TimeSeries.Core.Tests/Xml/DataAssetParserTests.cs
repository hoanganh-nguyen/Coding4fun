using NUnit.Framework;
using System.IO;
using FluentAssertions;
using System;
using NSubstitute.ExceptionExtensions;

namespace TimeSeries.Core.Xml.Tests
{
    [TestFixture]
    public class DataAssetParserTests
    {
        IDataAssetParser _parser;



        [Test]
        public void ParseTestWithoutErro()
        {
            //Arrange
            _parser = new DataAssetParser("Toto",true);
            string xml = File.ReadAllText(@"..\..\Data\data.xml");
            //Act
            var tmp = _parser.Parse(xml);
            //Assert
            tmp.Should().NotBeNull();
            tmp.ImportedBy.Should().NotBeNull();
            tmp.Asset.Should().NotBeNullOrEmpty();
            tmp.Price.Should().HaveCount(5);


        }

        [Test]
        public void ParseTestWithErro()
        {
            //Arrange
            _parser = new DataAssetParser("Toto", true);
            string xml = File.ReadAllText(@"..\..\Data\errordata.xml");
            //Act
            var tmp = _parser.Parse(xml);
            //Assert
            tmp.Should().NotBeNull();
            tmp.Asset.Should().NotBeNullOrEmpty();
            tmp.Price.Should().HaveCount(4);

        }
        [Test]
        public void ParseTestWithErrorNonIgnoreError()
        {
            //Arrange
            _parser = new DataAssetParser("Toto", false);
            string xml = File.ReadAllText(@"..\..\Data\errordata.xml");
            //Act
            Action action =()=> _parser.Parse(xml);
            action.ShouldThrow<FormatException>();
        }
    }
}