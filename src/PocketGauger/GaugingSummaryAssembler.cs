using System.Linq;
using PocketGauger.Dtos;
using PocketGauger.Interfaces;

namespace PocketGauger
{
    public class GaugingSummaryAssembler
    {
        private readonly IGaugingSummaryParser _gaugingSummaryParser;
        private readonly IMeterDetailsParser _meterDetailsParser;
        private readonly IPanelParser _panelParser;

        public GaugingSummaryAssembler(IGaugingSummaryParser gaugingSummaryParser, IMeterDetailsParser meterDetailsParser, IPanelParser panelParser)
        {
            _meterDetailsParser = meterDetailsParser;
            _panelParser = panelParser;
            _gaugingSummaryParser = gaugingSummaryParser;
        }

        public GaugingSummary Assemble(PocketGaugerFiles pocketGaugerFiles)
        {
            var gaugingSummary = _gaugingSummaryParser.Parse(pocketGaugerFiles);

            AttachMeterDetails(gaugingSummary, pocketGaugerFiles);
            AttachPanelItems(gaugingSummary, pocketGaugerFiles);

            return gaugingSummary;
        }

        private void AttachMeterDetails(GaugingSummary gaugingSummary, PocketGaugerFiles pocketGaugerFiles)
        {
            var meterDetails = _meterDetailsParser.Parse(pocketGaugerFiles);

            foreach (var gaugingSummaryItem in gaugingSummary.GaugingSummaryItems)
            {
                gaugingSummaryItem.MeterDetailsItem = meterDetails[gaugingSummaryItem.MeterId];
            }
        }

        private void AttachPanelItems(GaugingSummary gaugingSummary, PocketGaugerFiles pocketGaugerFiles)
        {
            var panelItems = _panelParser.Parse(pocketGaugerFiles);

            foreach (var gaugingSummaryItem in gaugingSummary.GaugingSummaryItems)
            {
                gaugingSummaryItem.PanelItems =
                    panelItems.Where(p => p.GaugingId == gaugingSummaryItem.GaugingId).ToList();
            }
        }
    }
}
