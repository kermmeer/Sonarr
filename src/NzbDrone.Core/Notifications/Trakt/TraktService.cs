using FluentValidation.Results;

namespace NzbDrone.Core.Notifications.Trakt
{
    public interface ITraktService
    {
        ValidationFailure Test(TraktSettings settings);
        void SendEpisodeCollected(TraktSettings settings, int seriesId, int seasonNumber, int episodeNumber);
    }

    public class TraktService : ITraktService
    {
        public void SendEpisodeCollected(TraktSettings settings, int seriesId, int seasonNumber, int episodeNumber)
        {
        }

        public ValidationFailure Test(TraktSettings settings)
        {
            return null;
        }
    }
}
