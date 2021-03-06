﻿using System;
using System.IO;
using System.Xml.Serialization;
using PocketGauger.Exceptions;

namespace PocketGauger.Helpers
{
    public static class XmlDeserializerHelper
    {
        public static TPocketGaugerDto Deserialize<TPocketGaugerDto>(Stream fileStream)
        {
            var pocketGaugerType = typeof (TPocketGaugerDto);
            var serializer = new XmlSerializer(pocketGaugerType);

            try
            {
                return (TPocketGaugerDto)serializer.Deserialize(fileStream);
            }
            catch (InvalidOperationException ex)
            {
                throw new PocketGaugerDataFormatException(string.Format("Parsing {0} failed", FileNames.FileNameTypeMap[pocketGaugerType]), ex);
            }
        }
    }
}
