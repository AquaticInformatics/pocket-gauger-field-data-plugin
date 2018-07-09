using FluentAssertions;
using NUnit.Framework;
using PocketGauger.Parsers;
using PocketGauger.UnitTests.TestData;

namespace PocketGauger.UnitTests.IntegrationTests
{
    public class GaugingSummaryParserTests : IntegrationTestBase
    {
        private GaugingSummaryParser _gaugingSummaryParser;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _gaugingSummaryParser = new GaugingSummaryParser();
        }

        [Test]
        public void Parse_PocketGaugerFilesContainsValidGaugingSummary_ReturnsExpectedDto()
        {
            AddPocketGaugerFile(FileNames.GaugingSummary);

            var result = _gaugingSummaryParser.Parse(PocketGaugerFiles);

            var expected = ExpectedGaugingSummaryData.CreateExpectedGaugingSummary();
            result.ShouldBeEquivalentTo(expected);
        }
    }
}
