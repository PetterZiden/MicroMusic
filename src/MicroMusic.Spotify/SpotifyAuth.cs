using MicroMusic.Domain.Abstractions;
using MicroMusic.Spotify.Options;

using Microsoft.Extensions.Options;

using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;

using System.Threading.Tasks;

namespace MicroMusic.Spotify
{
    public class SpotifyAuth : ISpotifyAuth
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotifyAuth(IOptions<SpotifyAuthOptions> options)
        {
            _clientId = options.Value.ClientId;
            _clientSecret = options.Value.ClientSecret;
        }

        public async Task<Token> GetTokenAsync()
        {
            CredentialsAuth auth = new CredentialsAuth(_clientId, _clientSecret);
            var token = await auth.GetToken();

            return token;
        }
    }
}
