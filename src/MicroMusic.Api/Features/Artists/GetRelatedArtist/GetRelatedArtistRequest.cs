using FluentResults;

using MediatR;

using MicroMusic.Domain.Models;

using System.Collections.Generic;

namespace MicroMusic.Api.Features.Artists.GetRelatedArtist
{
    public class GetRelatedArtistRequest : IRequest<Result<IEnumerable<Artist>>>
    {
        public string ArtistName { get; set; }
    }
}
