using System;
using System.Runtime.Serialization;

namespace PocketGauger.Exceptions
{
    [Serializable]
    public class PocketGaugerZipFileMissingRequiredContentException : Exception
    {
        protected PocketGaugerZipFileMissingRequiredContentException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }

        public PocketGaugerZipFileMissingRequiredContentException()
        {
        }

        public PocketGaugerZipFileMissingRequiredContentException(string message)
            : base(message)
        {
        }

        public PocketGaugerZipFileMissingRequiredContentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}