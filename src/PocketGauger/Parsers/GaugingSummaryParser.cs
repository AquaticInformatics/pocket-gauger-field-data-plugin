using PocketGauger.Dtos;
using PocketGauger.Interfaces;

namespace PocketGauger.Parsers
{
    public class GaugingSummaryParser : IGaugingSummaryParser
    {
        public GaugingSummary Parse(PocketGaugerFiles pocketGaugerFiles)
        {
            return pocketGaugerFiles.ParseType<GaugingSummary>();
        }
   }
}
