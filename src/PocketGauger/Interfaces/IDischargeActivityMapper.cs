using System;
using FieldDataPluginFramework.DataModel.DischargeActivities;
using PocketGauger.Dtos;

namespace PocketGauger.Interfaces
{
    public interface IDischargeActivityMapper
    {
        DischargeActivity Map(GaugingSummaryItem gaugingSummary, TimeSpan locationTimeZoneOffset);
    }
}
