using System;
using System.IO;
using System.IO.Compression;
using FieldDataPluginFramework;
using FieldDataPluginFramework.Context;
using FieldDataPluginFramework.Results;
using PocketGauger.Dtos;
using PocketGauger.Exceptions;
using PocketGauger.Parsers;

namespace PocketGauger
{
    public class PocketGaugerParser : IFieldDataPlugin
    {
        public ParseFileResult ParseFile(Stream fileStream, IFieldDataResultsAppender fieldDataResultsAppender,
            ILog logger)
        {
            try
            {
                var gaugingSummary = ParseGaugingSummaryFromFile(fileStream);

                var gaugingSummaryProcessor = new GaugingSummaryProcessor(fieldDataResultsAppender, logger);
                gaugingSummaryProcessor.ProcessGaugingSummary(gaugingSummary);

                return ParseFileResult.SuccessfullyParsedAndDataValid();
            }
            catch (Exception ex)
                when (ex is PocketGaugerZipFileMissingRequiredContentException || ex is PocketGaugerZipFileException)
            {
                return ParseFileResult.CannotParse();
            }
            catch (PocketGaugerDataFormatException e)
            {
                return ParseFileResult.SuccessfullyParsedButDataInvalid(e);
            }
            catch (Exception e)
            {
                return ParseFileResult.CannotParse(e);
            }
        }

        private static GaugingSummary ParseGaugingSummaryFromFile(Stream fileStream)
        {
            GaugingSummary gaugingSummary;
            using (var zipArchive = GetZipArchive(fileStream))
            using (var zipContents = GetZipContents(zipArchive))
            {
                if (!zipContents.ContainsKey(FileNames.GaugingSummary))
                {
                    throw new PocketGaugerZipFileMissingRequiredContentException(
                        FormattableString.Invariant($"Zip file does not contain file {FileNames.GaugingSummary}"));
                }

                gaugingSummary = CreateGaugingSummaryAssembler().Assemble(zipContents);
            }
            return gaugingSummary;
        }

        public ParseFileResult ParseFile(Stream fileStream, LocationInfo selectedLocation, IFieldDataResultsAppender fieldDataResultsAppender,
            ILog logger)
        {
            return ParseFile(fileStream, fieldDataResultsAppender, logger);
        }

        private static ZipArchive GetZipArchive(Stream fileStream)
        {
            try
            {
                return new ZipArchive(fileStream, ZipArchiveMode.Read, leaveOpen: true);
            }
            catch (InvalidDataException)
            {
                throw new PocketGaugerZipFileException("File is not a zip file");
            }
        }

        private static PocketGaugerFiles GetZipContents(ZipArchive zipArchive)
        {
            var streams = new PocketGaugerFiles();
            foreach (var zipArchiveEntry in zipArchive.Entries)
            {
                streams[zipArchiveEntry.Name.ToLower()] = zipArchiveEntry.Open();
            }

            return streams;
        }

        private static GaugingSummaryAssembler CreateGaugingSummaryAssembler()
        {
            return new GaugingSummaryAssembler(new GaugingSummaryParser(), new MeterDetailsParser(), new PanelParser());
        }
    }
}
