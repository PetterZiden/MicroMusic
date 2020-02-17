using FluentResults;

using MediatR;

using MicroMusic.Domain.Models;

namespace MicroMusic.Api.Features.Artists.GetArtist
{
    public class GetArtistRequest : IRequest<Result<Artist>>
    {
        public string ArtistName { get; set; }
    }
}
