using AutoMapper;

using FluentResults;

using MicroMusic.Api.Features.Artists.GetRelatedArtist;
using MicroMusic.Domain.Abstractions;

using Moq;

using SpotifyAPI.Web.Models;

using System.Collections.Generic;
using System.Threading;

using Xunit;

namespace MicroMusic.Api.Tests.Features.Artists.GetRelatedArtist
{
    public class GetRelatedArtistRequestHandlerTests
    {
        private readonly Mock<ISpotifyService> _mockSpotifyService;
        private readonly Mock<IMapper> _mockMapper;

        public GetRelatedArtistRequestHandlerTests()
        {
            _mockSpotifyService = new Mock<ISpotifyService>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void CanGetRelatedArtistsByArtistName()
        {
            _mockSpotifyService.Setup(x => x.GetRelatedArtistsAsync(It.IsAny<string>())).ReturnsAsync(Results.Ok(GetArtists()));

            var handler = new GetRelatedArtistRequestHandler(_mockMapper.Object, _mockSpotifyService.Object);

            var result = handler.Handle(GetRequest(), CancellationToken.None).Result;

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CanNotGetRelatedArtistByArtistNameWhenServiceReturnsError()
        {
            _mockSpotifyService.Setup(x => x.GetRelatedArtistsAsync(It.IsAny<string>()))
                .ReturnsAsync(Results.Fail(new FluentResults.Error("Error")));

            var handler = new GetRelatedArtistRequestHandler(_mockMapper.Object, _mockSpotifyService.Object);

            var result = handler.Handle(GetRequest(), CancellationToken.None).Result;

            Assert.True(result.IsFailed);
            Assert.Equal("Error", result.Errors[0].Message);
        }



        private static IEnumerable<FullArtist> GetArtists()
        {
            return new List<FullArtist>
            {
                new FullArtist
                {
                    Id = "8rktFgwwKwno4MLmIQChQ9",
                    Name = "Copywrite",
                    Genres = new List<string> {"Hiphop", "Rap"},
                    Type = "Artist",
                    Popularity = 8
                },
                new FullArtist
                {
                     Id = "2ojjWnjeQiiyo9MVaEDCpP1",
                    Name = "Nas",
                    Genres = new List<string> {"Hiphop", "Rap"},
                    Type = "Artist",
                    Popularity = 7
                }
            };
        }

        private static GetRelatedArtistRequest GetRequest()
        {
            return new GetRelatedArtistRequest
            {
                ArtistName = "Mock"
            };
        }
    }
}
