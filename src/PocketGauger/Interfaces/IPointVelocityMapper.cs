using FieldDataPluginFramework.DataModel.ChannelMeasurements;
using FieldDataPluginFramework.DataModel.DischargeActivities;
using PocketGauger.Dtos;

namespace PocketGauger.Interfaces
{
    public interface IPointVelocityMapper
    {
        ManualGaugingDischargeSection Map(GaugingSummaryItem summaryItem, DischargeActivity dischargeActivity);
    }
}
