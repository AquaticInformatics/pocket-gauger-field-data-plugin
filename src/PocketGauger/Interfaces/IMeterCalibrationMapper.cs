using PocketGauger.Dtos;
using MeterCalibration = FieldDataPluginFramework.DataModel.Meters.MeterCalibration;

namespace PocketGauger.Interfaces
{
    public interface IMeterCalibrationMapper
    {
        MeterCalibration Map(MeterDetailsItem meterDetailsItem);
    }
}
