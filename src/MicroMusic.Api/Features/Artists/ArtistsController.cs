using MediatR;

using MicroMusic.Api.Features.Artists.GetArtist;
using MicroMusic.Api.Features.Artists.GetRelatedArtist;
using MicroMusic.Domain.Models;

using Microsoft.AspNetCore.Mvc;

using Serilog;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MicroMusic.Api.Features.Artists
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ArtistsController(IMediator mediator)
        {
            _logger = Log.ForContext<ArtistsController>();
            _mediator = mediator;
        }

        [HttpGet("artistName")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Artist), 200)]
        public async Task<IActionResult> GetArtistByName([Required]string artistName)
        {
            _logger.Information($"Received a call to get artist with id: {artistName}");

            var result = await _mediator.Send(new GetArtistRequest { ArtistName = artistName });

            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Errors[0].Message);
        }

        [HttpGet("relatedto/artistName")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Artist>), 200)]
        public async Task<IActionResult> GetRelatedArtistByName([Required]string artistName)
        {
            _logger.Information($"Received a call to get artist with id: {artistName}");

            var result = await _mediator.Send(new GetRelatedArtistRequest { ArtistName = artistName });

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Errors[0].Message);
        }
    }
}
