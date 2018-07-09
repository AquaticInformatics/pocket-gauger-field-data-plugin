using System.Collections.Generic;
using System.Xml.Serialization;

namespace PocketGauger.Dtos
{
    [XmlRoot("GF_GAUGING_SUMMARY")]
    public class GaugingSummary
    {
        [XmlElement("GF_GAUGING_SUMMARYItem")]
        public List<GaugingSummaryItem> GaugingSummaryItems { get; set; } 
    }
}
