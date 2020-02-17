using FluentResults;

using MicroMusic.Domain.Abstractions;

using Serilog;

using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

using ILogger = Serilog.ILogger;

namespace MicroMusic.Spotify
{
    public class SpotifyService : ISpotifyService
    {
        private readonly ILogger _logger;
        private readonly SpotifyWebAPI _spotifyWebAPI;
        private readonly ISpotifyAuth _spotifyAuth;

        public SpotifyService(ISpotifyAuth spotifyAuth)
        {
            _logger = Log.ForContext<SpotifyService>();

            _spotifyAuth = spotifyAuth;
            _spotifyWebAPI = new SpotifyWebAPI
            {
                AccessToken = _spotifyAuth.GetTokenAsync().Result.AccessToken,
                TokenType = "Bearer"
            };
        }

        public async Task<Result<FullArtist>> GetArtistInformationAsync(string artistName)
        {
            var artist = await _spotifyWebAPI.SearchItemsAsync(artistName, SpotifyAPI.Web.Enums.SearchType.Artist);

            if(artist.HasError())
            {
                _logger.Error($"Error message from SpotifyWebAPI: {artist.Error.Message} with status {artist.Error.Status}");
                return await Task.FromResult(Results.Fail<FullArtist>(new FluentResults.Error("Error when calling SpotifyWebAPI")));
            }

            _logger.Information("Successfully fetched artist from SpotifyWebAPI");
            return await Task.FromResult(Results.Ok(artist.Artists.Items[0]));
        }

        public async Task<Result<IEnumerable<FullArtist>>> GetRelatedArtistsAsync(string artistName)
        {
            var artist = GetArtistInformationAsync(artistName);

            var response = await _spotifyWebAPI.GetRelatedArtistsAsync(artist.Result.Value.Id);

            if(response.HasError())
            {
                _logger.Error($"Error message from SpotifyWebAPI: {response.Error.Message} with status {response.Error.Status}");
                return await Task.FromResult(Results.Fail<IEnumerable<FullArtist>>(new FluentResults.Error("Error when calling SpotifyWebAPI")));
            }

            IEnumerable<FullArtist> relatedArtists = response.Artists;

            _logger.Information("Successfully fetched artist from SpotifyWebAPI");
            return await Task.FromResult(Results.Ok(relatedArtists));
        }


    }
}
