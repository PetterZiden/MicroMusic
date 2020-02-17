using AutoMapper;
using FluentResults;
using MicroMusic.Api.Features.Artists.GetArtist;
using MicroMusic.Domain.Abstractions;

using Moq;

using SpotifyAPI.Web.Models;

using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace MicroMusic.Api.Tests.Features.Artists.GetArtist
{
    public class GetArtistRequestHandlerTests
    {
        private readonly Mock<ISpotifyService> _mockSpotifyService;
        private readonly Mock<IMapper> _mockMapper;

        public GetArtistRequestHandlerTests()
        {
            _mockSpotifyService = new Mock<ISpotifyService>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void CanGetArtistByArtistName()
        {
            _mockSpotifyService.Setup(x => x.GetArtistInformationAsync(It.IsAny<string>())).ReturnsAsync(Results.Ok(GetArtist()));

            var handler = new GetArtistRequestHandler(_mockMapper.Object, _mockSpotifyService.Object);

            var result = handler.Handle(GetRequest(), CancellationToken.None).Result;

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void CanNotGetArtistByArtistNameWhenServiceReturnsError()
        {
            _mockSpotifyService.Setup(x => x.GetArtistInformationAsync(It.IsAny<string>()))
                .ReturnsAsync(Results.Fail(new FluentResults.Error("Error")));

            var handler = new GetArtistRequestHandler(_mockMapper.Object, _mockSpotifyService.Object);

            var result = handler.Handle(GetRequest(), CancellationToken.None).Result;

            Assert.True(result.IsFailed);
            Assert.Equal("Error", result.Errors[0].Message);
        }

        private static FullArtist GetArtist()
        {
            return
                new FullArtist
                {
                    Id = "8rktFgwwKwno4MLmIQChQ9",
                    Name = "Copywrite",
                    Genres = new List<string> { "Hiphop", "Rap" },
                    Type = "Artist",
                    Popularity = 8
                };
        }

        private static GetArtistRequest GetRequest()
        {
            return new GetArtistRequest
            {
                ArtistName = "Mock"
            };
        }
    }
}
