using FluentAssertions;
using NUnit.Framework;
using PocketGauger.Parsers;
using PocketGauger.UnitTests.TestData;

namespace PocketGauger.UnitTests.IntegrationTests
{
    public class PanelParserTests : IntegrationTestBase
    {
        private PanelParser _parser;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _parser = new PanelParser();
        }

        [Test]
        public void Parse_PocketGaugerFilesContainsPanelFiles_ReturnsExpectedDto()
        {
            AddPocketGaugerFile(FileNames.Panels);
            AddPocketGaugerFile(FileNames.Verticals);

            var result = _parser.Parse(PocketGaugerFiles);

            var expected = ExpectedPanelData.CreateExpectedPanels();
            result.ShouldAllBeEquivalentTo(expected);
        }
    }
}
