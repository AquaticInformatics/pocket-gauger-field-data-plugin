using FieldDataPluginFramework;

namespace PocketGauger.UnitTests.TestHelpers
{
    public class NullLog : ILog
    {
        public void Info(string message)
        {
        }

        public void Error(string message)
        {
        }
    }
}
