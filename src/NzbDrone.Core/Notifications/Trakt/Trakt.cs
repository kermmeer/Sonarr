using FluentValidation.Results;
using NzbDrone.Common.Extensions;
using System.Collections.Generic;

namespace NzbDrone.Core.Notifications.Trakt
{
    public class Trakt : NotificationBase<TraktSettings>
    {
        private readonly ITraktService _service;

        public Trakt(ITraktService service)
        {
            _service = service;
        }

        public override string Name => "Trakt";

        public override string Link => "https://www.trakt.tv";

        public override void OnDownload(DownloadMessage message)
        {
            _service.SendEpisodeCollected(Settings, message.Series.Id, message.EpisodeFile.SeasonNumber, message.EpisodeFile.Id);
        }

        public override ValidationResult Test()
        {
            var failures = new List<ValidationFailure>();

            failures.AddIfNotNull(_service.Test(Settings));

            return new ValidationResult(failures);
        }
    }
}
