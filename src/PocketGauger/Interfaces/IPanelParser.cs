using System.Collections.Generic;
using PocketGauger.Dtos;

namespace PocketGauger.Interfaces
{
    public interface IPanelParser
    {
        IReadOnlyCollection<PanelItem> Parse(PocketGaugerFiles pocketGaugerFiles);
    }
}
