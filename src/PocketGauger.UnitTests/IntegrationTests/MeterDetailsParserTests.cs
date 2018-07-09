using FluentAssertions;
using NUnit.Framework;
using PocketGauger.Parsers;
using PocketGauger.UnitTests.TestData;

namespace PocketGauger.UnitTests.IntegrationTests
{
    public class MeterDetailsParserTests : IntegrationTestBase
    {
        private MeterDetailsParser _meterDetailsParser;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _meterDetailsParser = new MeterDetailsParser();
        }

        [Test]
        public void Parse_PocketGaugerFilesContainsMeterFiles_ReturnsExpectedDto()
        {
            AddPocketGaugerFile(FileNames.MeterDetails);
            AddPocketGaugerFile(FileNames.MeterCalibrations);

            var expected = ExpectedMeterDetailsData.CreateExpectedThreeMeterDetails();

            var result = _meterDetailsParser.Parse(PocketGaugerFiles);

            result.ShouldAllBeEquivalentTo(expected);
        }
    }
}
