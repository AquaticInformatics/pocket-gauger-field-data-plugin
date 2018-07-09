using System;
using System.Runtime.Serialization;

namespace PocketGauger.Exceptions
{
    [Serializable]
    public class PocketGaugerDataPersistenceException : Exception
    {
        public PocketGaugerDataPersistenceException()
        {
        }

        public PocketGaugerDataPersistenceException(string message) : base(message)
        {
        }

        public PocketGaugerDataPersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PocketGaugerDataPersistenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
