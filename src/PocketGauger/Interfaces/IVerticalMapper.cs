using System.Collections.Generic;
using FieldDataPluginFramework.DataModel.ChannelMeasurements;
using FieldDataPluginFramework.DataModel.Verticals;
using PocketGauger.Dtos;

namespace PocketGauger.Interfaces
{
    public interface IVerticalMapper
    {
        List<Vertical> Map(GaugingSummaryItem gaugingSummaryItem, DeploymentMethodType deploymentMethod);
    }
}
