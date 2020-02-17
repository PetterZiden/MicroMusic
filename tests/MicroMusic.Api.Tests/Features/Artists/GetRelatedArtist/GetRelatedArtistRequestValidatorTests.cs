using MicroMusic.Api.Features.Artists.GetRelatedArtist;

using Xunit;

namespace MicroMusic.Api.Tests.Features.Artists.GetRelatedArtist
{
    public class GetRelatedArtistRequestValidatorTests
    {
        [Theory]
        [InlineData("Cage", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void CanValidateRequest(string artistName, bool expectedValidity)
        {
            var request = new GetRelatedArtistRequest
            {
                ArtistName = artistName
            };

            var validator = new GetRelatedArtistRequestValidator();

            var result = validator.Validate(request);

            Assert.Equal(expectedValidity, result.IsValid);
        }
    }
}
