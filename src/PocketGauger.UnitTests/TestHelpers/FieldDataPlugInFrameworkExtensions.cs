using System;
using FieldDataPluginFramework.DataModel;
using FieldDataPluginFramework.DataModel.ChannelMeasurements;
using FieldDataPluginFramework.DataModel.CrossSection;
using FieldDataPluginFramework.DataModel.DischargeActivities;
using FieldDataPluginFramework.DataModel.Verticals;
using NodaTime;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace PocketGauger.UnitTests.TestHelpers
{
    public static class FieldDataPluginFrameworkExtensions
    {
        public static void AddFieldDataPluginFrameworkTestingExtensions(this IFixture fixture)
        {
            //fixture.Customizations.Insert(0, new EnumGeneratorExcludingUnspecifiedValue());
            fixture.Customize(new MultipleCustomization());

            fixture.IgnoreFieldVisitPropertiesSetByConstructor();
            fixture.IgnoreCrossSectionSurveyPropertiesSetByConstructor();
            fixture.IgnoreDischargeActivityPropertiesSetByConstructor();
            fixture.IgnoreManualGaugingDischargeSectionPropertiesSetByConstructor();

            fixture.Register(() => CreateMeasurementConditionData(fixture));
            fixture.Register(() => CreateDischargeSubActivity(fixture));

            fixture.Register<Instant, TimeSpan, DateTimeInterval>((instant, timeSpan)
                => new DateTimeInterval(instant.ToDateTimeOffset(), timeSpan.Duration()));
        }

        private static void IgnoreFieldVisitPropertiesSetByConstructor(this IFixture fixture)
        {
            fixture.Customize<FieldVisitDetails>(inst => inst
                .Without(x => x.FieldVisitPeriod)
                );
        }

        private static void IgnoreCrossSectionSurveyPropertiesSetByConstructor(this IFixture fixture)
        {
            const int repeatCount = 3;

            fixture.Customize<CrossSectionSurvey>(inst => inst
                .Without(x => x.SurveyPeriod)
                .Without(x => x.ChannelName)
                .Without(x => x.RelativeLocationName)
                .Without(x => x.DistanceUnitId)
                .Without(x => x.StartPoint)
                .Do(x => x.CrossSectionPoints.AddMany(
                    fixture.Create<CrossSectionPoint>, repeatCount))
                );
        }

        private static void IgnoreDischargeActivityPropertiesSetByConstructor(this IFixture fixture)
        {
            fixture.Customize<DischargeActivity>(inst => inst
                .Without(x => x.MeasurementPeriod)
                .Without(x => x.Discharge)
                );
        }

        private static void IgnoreManualGaugingDischargeSectionPropertiesSetByConstructor(this IFixture fixture)
        {
            fixture.Customize<ManualGaugingDischargeSection>(inst => inst
                .Without(x => x.MeasurementPeriod)
                .Without(x => x.ChannelName)
                .Without(x => x.Discharge)
                );
        }

        private static ChannelMeasurementBase CreateDischargeSubActivity(ISpecimenBuilder fixture)
        {
            return fixture.Create<ManualGaugingDischargeSection>();
        }

        private static MeasurementConditionData CreateMeasurementConditionData(ISpecimenBuilder fixture)
        {
            if (fixture.Create<bool>())
            {
                return fixture.Create<IceCoveredData>();
            }

            return fixture.Create<OpenWaterData>();
        }
    }
}
