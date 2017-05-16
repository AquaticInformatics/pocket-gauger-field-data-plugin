﻿using Server.BusinessInterfaces.FieldDataPluginCore.DataModel.ChannelMeasurements;

namespace Server.Plugins.FieldVisit.CrossSection.Helpers
{
    public static class CrossSectionParserConstants
    {
        public const string Header = "AQUARIUS Cross-Section CSV";
        public const string DefaultVersion = "1.0";

        public const string CrossSectionDataSeparator = ":";
        public const string CrossSectionPointDataSeparator = ",";

        public const string DefaultChannelName = "Main";
        public const string DefaultRelativeLocationName = "At the control";
        public const StartPointType DefaultStartPointType = StartPointType.Unspecified;
    }
}
