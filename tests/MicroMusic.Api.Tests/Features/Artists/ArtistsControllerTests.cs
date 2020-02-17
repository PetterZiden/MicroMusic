using FluentResults;

using MediatR;

using MicroMusic.Api.Features.Artists;
using MicroMusic.Api.Features.Artists.GetArtist;
using MicroMusic.Api.Features.Artists.GetRelatedArtist;
using MicroMusic.Domain.Models;
using Microsoft.AspNetCore.Mvc;

using Moq;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Xunit;

namespace MicroMusic.Api.Tests.Features.Artists
{
    public class ArtistsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;

        public ArtistsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
        }

        //GetArtist
        [Fact]
        public void CanGetArtistByArtistNameAndReturnResultOk()
        {
            var artist = GetArtists().ToList().FirstOrDefault();
            _mockMediator.Setup(x => x.Send(It.IsAny<GetArtistRequest>(), CancellationToken.None)).ReturnsAsync(Results.Ok(artist));

            var result = new ArtistsController(_mockMediator.Object).GetArtistByName(It.IsAny<string>()).Result as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(artist.Id, (result.Value as Artist).Id);
        }

        [Fact]
        public void CanNotGetArtistByArtistNameAndReturnResultFail()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<GetArtistRequest>(), CancellationToken.None)).ReturnsAsync(Results.Fail("Error"));

            var result = new ArtistsController(_mockMediator.Object).GetArtistByName(It.IsAny<string>()).Result as BadRequestObjectResult;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Error", result.Value);
        }

        //GetRelatedArtist
        [Fact]
        public void CanGetRelatedArtistByArtistNameAndReturnResultOk()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<GetRelatedArtistRequest>(), CancellationToken.None)).ReturnsAsync(Results.Ok(GetArtists()));

            var result = new ArtistsController(_mockMediator.Object).GetRelatedArtistByName(It.IsAny<string>()).Result as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(GetArtists().ToList().FirstOrDefault().Id, (result.Value as IEnumerable<Artist>).ToList().FirstOrDefault().Id);
        }

        [Fact]
        public void CanNotGetRelatedArtistByArtistNameAndReturnResultFail()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<GetRelatedArtistRequest>(), CancellationToken.None)).ReturnsAsync(Results.Fail("Error"));

            var result = new ArtistsController(_mockMediator.Object).GetRelatedArtistByName(It.IsAny<string>()).Result as BadRequestObjectResult;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Error", result.Value);
        }

        private static IEnumerable<Artist> GetArtists()
        {
            return new List<Artist>
            {
                new Artist
                {
                    Id = "8rktFgwwKwno4MLmIQChQ9",
                    Name = "Copywrite",
                    Genres = new List<string> {"Hiphop", "Rap"},
                    Type = "Artist",
                    Popularity = 8
                },
                new Artist
                {
                     Id = "2ojjWnjeQiiyo9MVaEDCpP1",
                    Name = "Nas",
                    Genres = new List<string> {"Hiphop", "Rap"},
                    Type = "Artist",
                    Popularity = 7
                }
            };
        }
    }
}
