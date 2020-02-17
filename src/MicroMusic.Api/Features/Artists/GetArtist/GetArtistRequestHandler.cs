using AutoMapper;

using FluentResults;

using MediatR;

using MicroMusic.Domain.Abstractions;
using MicroMusic.Domain.Models;

using System.Threading;
using System.Threading.Tasks;

namespace MicroMusic.Api.Features.Artists.GetArtist
{
    public class GetArtistRequestHandler : IRequestHandler<GetArtistRequest, Result<Artist>>
    {
        private readonly IMapper _mapper;
        private readonly ISpotifyService _spotifyService;

        public GetArtistRequestHandler(IMapper mapper, ISpotifyService spotifyService)
        {
            _mapper = mapper;
            _spotifyService = spotifyService;
        }

        public async Task<Result<Artist>> Handle(GetArtistRequest request, CancellationToken cancellationToken)
        {
            var result = await _spotifyService.GetArtistInformationAsync(request.ArtistName);

            if(!result.IsSuccess)
            {
                return Results.Fail<Artist>(result.Errors[0]);
            }

            return Results.Ok(_mapper.Map<Artist>(result.Value));
        }
    }
}
