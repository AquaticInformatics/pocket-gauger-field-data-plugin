using System.Collections.Generic;
using Ploeh.AutoFixture;
using PocketGauger.Dtos;

namespace PocketGauger.UnitTests.TestData
{
    public static class CollectionRegistrar
    {
        public static void Register(IFixture fixture)
        {
            fixture.Register<IReadOnlyList<MeterCalibrationItem>>(fixture.Create<List<MeterCalibrationItem>>);
            fixture.Register<IReadOnlyCollection<PanelItem>>(fixture.Create<List<PanelItem>>);
            fixture.Register<IReadOnlyList<VerticalItem>>(fixture.Create<List<VerticalItem>>);
        }
    }
}
