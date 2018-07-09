using FieldDataPluginFramework;
using FieldDataPluginFramework.Context;
using FieldDataPluginFramework.Results;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using PocketGauger.UnitTests.TestHelpers;

namespace PocketGauger.UnitTests
{
    public abstract class PocketGaugerTestsBase
    {
        protected IFieldDataResultsAppender FieldDataResultsAppender;
        protected ILog Logger;

        protected IFixture Fixture;

        private LocationInfo _locationInfo;

        [SetUp]
        public void SetUp()
        {
            Fixture = new Fixture();

            Logger = new NullLog();
            FieldDataResultsAppender = Substitute.For<IFieldDataResultsAppender>();

            _locationInfo = LocationInfoHelper.GetTestLocationInfo(Fixture);

            FieldDataResultsAppender
                .GetLocationByUniqueId(Arg.Any<string>())
                .Returns(_locationInfo);

            FieldDataResultsAppender
                .GetLocationByIdentifier(Arg.Any<string>())
                .Returns(_locationInfo);
        }
    }
}
