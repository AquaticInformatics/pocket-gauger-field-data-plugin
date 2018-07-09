using System.Collections.Generic;
using System.Linq;
using PocketGauger.Dtos;
using PocketGauger.Interfaces;

namespace PocketGauger.Parsers
{
    public class PanelParser : IPanelParser
    {
        public IReadOnlyCollection<PanelItem> Parse(PocketGaugerFiles pocketGaugerFiles)
        {
            var panels = pocketGaugerFiles.ParseType<Panels>();
            var verticals = pocketGaugerFiles.ParseType<Verticals>();

            panels.PanelItems = panels.PanelItems.OrderBy(item => item.PanelId).ToList();
            AssignVerticals(panels, verticals);

            return panels.PanelItems;
        }

        private static void AssignVerticals(Panels panels, Verticals verticals)
        {
            foreach (var panel in panels.PanelItems)
            {
                panel.Verticals = verticals.VerticalItems
                    .Where(vertical => vertical.PanelId == panel.PanelId)
                    .OrderBy(item => item.VerticalNo)
                    .ToList();
            }
        }
    }
}
