using MicroMusic.Api.Features.Artists.GetArtist;
using Xunit;

namespace MicroMusic.Api.Tests.Features.Artists.GetArtist
{
    public class GetArtistRequestValidatorTests
    {
        [Theory]
        [InlineData("Cage", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void CanValidateRequest(string artistName, bool expectedValidity)
        {
            var request = new GetArtistRequest
            {
                ArtistName = artistName
            };

            var validator = new GetArtistRequestValidator();

            var result = validator.Validate(request);

            Assert.Equal(expectedValidity, result.IsValid);
        }
    }
}
