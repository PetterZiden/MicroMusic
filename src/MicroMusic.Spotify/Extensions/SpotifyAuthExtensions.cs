using MicroMusic.Domain.Abstractions;
using MicroMusic.Spotify.Options;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace MicroMusic.Spotify.Extensions
{
    public static class SpotifyAuthExtensions
    {
        public static IServiceCollection AddSpotifyAuth(
        this IServiceCollection serviceCollection,
        Action<SpotifyAuthOptions> options)
        {
            serviceCollection.AddScoped<ISpotifyAuth, SpotifyAuth>();
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options),
                    @"Please provide options for SpotifyAuth.");
            }
            serviceCollection.Configure(options);
            return serviceCollection;
        }
    }
}
