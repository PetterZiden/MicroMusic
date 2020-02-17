using FluentResults;

using MicroMusic.Domain.Abstractions;

using Newtonsoft.Json;

using Serilog;

using SpotifyAPI.Web.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using ILogger = Serilog.ILogger;

namespace MicroMusic.Spotify.Mock
{
    public class SpotifyService : ISpotifyService
    {
        private const string MockDataPath = "C:\\Projects\\MicroMusic\\mock\\MicroMusic.Spotify.Mock\\spotify.mockdata.json";

        private readonly MockData _mockData;

        private readonly ILogger _logger;

        public SpotifyService()
        {
            _logger = Log.ForContext<SpotifyService>();

            _mockData = new MockData
            {
                Artists = new List<FullArtist>
                {
                    new FullArtist
                    {
                        Name = "Eminem",
                        Type = "Artist"
                    },
                    new FullArtist
                    {
                        Name = "BigL",
                        Type = "Artist"
                    }
                }
            };

            if (File.Exists(MockDataPath))
            {
                try
                {
                    _mockData = JsonConvert.DeserializeObject<MockData>(File.ReadAllText(MockDataPath));
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Could not load mockdata file");
                }
            }
        }

#pragma warning disable CS1998
        public async Task<Result<FullArtist>> GetArtistInformationAsync(string artistId)
#pragma warning restore CS1998
        {
            _logger.Verbose("Successfully retrieved artist from spotifyServiceMock ");
            return Results.Ok(_mockData.Artists.ToList().FirstOrDefault());
        }

#pragma warning disable CS1998
        public async Task<Result<IEnumerable<FullArtist>>> GetRelatedArtistsAsync(string artistName)
#pragma warning restore CS1998
        {
            _logger.Verbose("Successfully retrieved artists from spotifyServiceMock ");
            return Results.Ok(_mockData.Artists);
        }
    }
}
