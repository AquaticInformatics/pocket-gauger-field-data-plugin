using System.Collections.Generic;
using System.Xml.Serialization;

namespace PocketGauger.Dtos
{
    [XmlRoot("GF_METER_CALIB")]
    public class MeterCalibration
    {
        [XmlElement("GF_METER_CALIBItem")]
        public List<MeterCalibrationItem> MeterCalibrationItems { get; set; } 
    }
}
