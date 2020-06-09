using FluentValidation;
using Newtonsoft.Json;
using NzbDrone.Core.Annotations;
using NzbDrone.Core.ThingiProvider;
using NzbDrone.Core.Validation;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace NzbDrone.Core.Notifications.Trakt
{
    public class TraktSettingsValidator : AbstractValidator<TraktSettings>
    {
        public TraktSettingsValidator()
        {
            RuleFor(c => c.Link).ValidRootUrl();

            RuleFor(c => c.Username)
                .Matches(@"^[A-Za-z0-9\-_ ]+$", RegexOptions.IgnoreCase)
                .WithMessage("Username is required");
        }
    }

    public class TraktSettings : IProviderConfig
    {
        private static readonly TraktSettingsValidator Validator = new TraktSettingsValidator();
        
        //https://trakt.docs.apiary.io/#reference/sync/add-to-collection/add-items-to-collection
        //https://trakt.docs.apiary.io/#reference/sync/get-watched/get-watched

        public TraktSettings()
        {
            Link = "https://api.trakt.tv";
            Username = "";
            RemoveFromWatchlist = true;
        }

        [FieldDefinition(0, Label = "Trakt API URL", HelpText = "Link to to Trakt API URL, do not change unless you know what you are doing.", Advanced = true)]
        public string Link { get; set; }

        [FieldDefinition(1, Label = "Username", HelpText = "The user for which to set the collected state.")]
        public string Username { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [FieldDefinition(2, Label = "Remove from watchlist.", HelpText = "Remove the episode from the watchlist on collect.", Type = FieldType.Checkbox)]
        public bool RemoveFromWatchlist { get; set; }

        [FieldDefinition(3, Label = "Additional Parameters", HelpText = "Additional Trakt API parameters", Advanced = true)]
        public string TraktAdditionalParameters { get; set; }


        public NzbDroneValidationResult Validate()
        {
            return new NzbDroneValidationResult(Validator.Validate(this));
        }
    }
}
