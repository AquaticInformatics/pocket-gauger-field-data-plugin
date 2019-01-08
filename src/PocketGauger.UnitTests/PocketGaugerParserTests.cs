using System.IO;
using System.Linq;
using System.Reflection;
using FieldDataPluginFramework.Context;
using FieldDataPluginFramework.DataModel;
using FieldDataPluginFramework.DataModel.DischargeActivities;
using FieldDataPluginFramework.Results;
using FluentAssertions;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace PocketGauger.UnitTests
{
    [TestFixture]
    public class PocketGaugerParserTests : PocketGaugerTestsBase
    {
        private PocketGaugerParser _pocketGaugerParser;
        private Stream _stream;

        [SetUp]
        public new void SetUp()
        {
            _pocketGaugerParser = new PocketGaugerParser();

            SetupStreamToLoadEmbeddedResource("PGData.zip");
        }

        private void SetupStreamToLoadEmbeddedResource(string path)
        {
            _stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"PocketGauger.UnitTests.TestData.{path}");
        }

        [TearDown]
        public void TearDown()
        {
            _stream.Close();
            _stream.Dispose();
        }

        [Test]
        public void ParseFile_FileStreamIsNotAValidZipFile_ReturnsCannotParse()
        {
            _stream = new MemoryStream(Fixture.Create<byte[]>());

            var parseFileResult = _pocketGaugerParser.ParseFile(_stream, FieldDataResultsAppender, Logger);

            AssertResultAllowsOtherPluginsToContinueProcessing(parseFileResult);
        }

        private static void AssertResultAllowsOtherPluginsToContinueProcessing(ParseFileResult parseFileResult)
        {
            parseFileResult.Status.Should().Be(ParseFileStatus.CannotParse);
            parseFileResult.ErrorMessage.Should().BeNullOrEmpty();
        }

        [Test]
        public void ParseFile_FileStreamZipDoesNotContainGaugingSummary_ReturnsCannotParse()
        {
            _stream = CreateZipStream(Fixture.Create<string>());

            var parseFileResult = _pocketGaugerParser.ParseFile(_stream, FieldDataResultsAppender, Logger);

            AssertResultAllowsOtherPluginsToContinueProcessing(parseFileResult);
        }

        private Stream CreateZipStream(string zipEntryName)
        {
            var memoryStream = new MemoryStream();
            var zipOutputStream = new ZipOutputStream(memoryStream);
            AddZipEntry(zipOutputStream, zipEntryName);

            zipOutputStream.IsStreamOwner = false;
            zipOutputStream.Close();
            memoryStream.Position = 0;

            return memoryStream;
        }

        private void AddZipEntry(ZipOutputStream zipOutputStream, string zipEntryName)
        {
            var zipEntry = new ZipEntry(zipEntryName);
            zipOutputStream.PutNextEntry(zipEntry);

            const int entrySize = 4096;
            var sourceData = Fixture.CreateMany<byte>(entrySize).ToArray();
            var buffer = new byte[4096];
            StreamUtils.Copy(new MemoryStream(sourceData), zipOutputStream, buffer);
            zipOutputStream.CloseEntry();
        }

        [Test]
        public void ParseFile_ValidFileStreamZip_ReturnsExpectedNumberOfParsedResults()
        {
            const int expectedNumberOfDischargeActivities = 3;

            var parseFileResult = _pocketGaugerParser.ParseFile(_stream, FieldDataResultsAppender, Logger);
            parseFileResult.Status.Should().Be(ParseFileStatus.SuccessfullyParsedAndDataValid);

            FieldDataResultsAppender
                .Received(expectedNumberOfDischargeActivities)
                .AddFieldVisit(Arg.Any<LocationInfo>(), Arg.Any<FieldVisitDetails>());

            FieldDataResultsAppender
                .Received(expectedNumberOfDischargeActivities)
                .AddDischargeActivity(Arg.Any<FieldVisitInfo>(), Arg.Any<DischargeActivity>());
        }

        [Test]
        public void ParseFile_DuplicateNestedFilename_ReturnsCannotParseWithoutErrorMessage()
        {
            SetupStreamToLoadEmbeddedResource("DuplicateNestedFilenames.zip");

            var parseFileResult = _pocketGaugerParser.ParseFile(_stream, FieldDataResultsAppender, Logger);

            AssertResultAllowsOtherPluginsToContinueProcessing(parseFileResult);
        }
    }
}
