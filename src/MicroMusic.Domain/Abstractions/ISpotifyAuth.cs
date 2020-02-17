using SpotifyAPI.Web.Models;

using System.Threading.Tasks;

namespace MicroMusic.Domain.Abstractions
{
    public interface ISpotifyAuth
    {
        Task<Token> GetTokenAsync();
    }
}
