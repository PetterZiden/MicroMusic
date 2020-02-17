using FluentResults;

using SpotifyAPI.Web.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroMusic.Domain.Abstractions
{
    public interface ISpotifyService
    {
        Task<Result<FullArtist>> GetArtistInformationAsync(string artistId);

        Task<Result<IEnumerable<FullArtist>>> GetRelatedArtistsAsync(string artistName);
    }
}
