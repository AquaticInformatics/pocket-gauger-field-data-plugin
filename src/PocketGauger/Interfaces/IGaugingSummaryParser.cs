using PocketGauger.Dtos;

namespace PocketGauger.Interfaces
{
    public interface IGaugingSummaryParser
    {
        GaugingSummary Parse(PocketGaugerFiles pocketGaugerFiles);
    }
}
