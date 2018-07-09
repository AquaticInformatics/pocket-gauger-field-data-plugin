using System.Collections.Generic;
using PocketGauger.Dtos;

namespace PocketGauger.Interfaces
{
    public interface IMeterDetailsParser
    {
        IReadOnlyDictionary<string, MeterDetailsItem> Parse(PocketGaugerFiles pocketGaugerFiles);
    }
}
