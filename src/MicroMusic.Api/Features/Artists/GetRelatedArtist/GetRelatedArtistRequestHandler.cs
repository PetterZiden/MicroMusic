using AutoMapper;

using FluentResults;

using MediatR;

using MicroMusic.Domain.Abstractions;
using MicroMusic.Domain.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroMusic.Api.Features.Artists.GetRelatedArtist
{
    public class GetRelatedArtistRequestHandler : IRequestHandler<GetRelatedArtistRequest, Result<IEnumerable<Artist>>>
    {
        private readonly IMapper _mapper;
        private readonly ISpotifyService _spotifyService;

        public GetRelatedArtistRequestHandler(IMapper mapper, ISpotifyService spotifyService)
        {
            _mapper = mapper;
            _spotifyService = spotifyService;
        }

        public async Task<Result<IEnumerable<Artist>>> Handle(GetRelatedArtistRequest request, CancellationToken cancellationToken)
        {
            var result = await _spotifyService.GetRelatedArtistsAsync(request.ArtistName);

            if (!result.IsSuccess)
            {
                return Results.Fail<IEnumerable<Artist>>(result.Errors[0]);
            }

            return Results.Ok(result.Value.ToList().Select(_mapper.Map<Artist>));
        }
    }
}
