﻿using Server.BusinessInterfaces.FieldDataPluginCore.Context;
using Server.BusinessInterfaces.FieldDataPluginCore.DataModel;
using Server.BusinessInterfaces.FieldDataPluginCore.DataModel.CrossSection;
using Server.BusinessInterfaces.FieldDataPluginCore.Results;
using Server.Plugins.FieldVisit.CrossSection.Exceptions;
using Server.Plugins.FieldVisit.CrossSection.Helpers;
using Server.Plugins.FieldVisit.CrossSection.Interfaces;
using static System.FormattableString;

namespace Server.Plugins.FieldVisit.CrossSection.FieldVisitHandlers
{
    public abstract class FieldVisitHandlerBase : IFieldVisitHandler
    {
        protected IFieldDataResultsAppender FieldDataResultsAppender { get; }

        protected FieldVisitHandlerBase(IFieldDataResultsAppender fieldDataResultsAppender)
        {
            FieldDataResultsAppender = fieldDataResultsAppender;
        }

        protected FieldVisitHandlerBase()
        {
        }

        public abstract NewFieldVisitInfo GetFieldVisit(string locationIdentifier, CrossSectionSurvey crossSectionSurvey);

        protected static void CheckForExpectedLocation(string locationIdentifier, LocationInfo selectedLocation)
        {
            var selectedLocationLocationIdentifier = selectedLocation.LocationIdentifier;

            if (locationIdentifier.EqualsOrdinalIgnoreCase(selectedLocationLocationIdentifier))
                return;

            throw new CrossSectionSurveyDataFormatException(
                Invariant($"Location identifier '{selectedLocationLocationIdentifier}' does not match the identifier in the file: '{locationIdentifier}'"));
        }

        protected NewFieldVisitInfo CreateFieldVisit(LocationInfo location, CrossSectionSurvey crossSectionSurvey)
        {
            var fieldVisit = new FieldVisitDetails(crossSectionSurvey.SurveyPeriod) {Party = crossSectionSurvey.Party};
            return FieldDataResultsAppender.AddFieldVisit(location, fieldVisit);
        }
    }
}
