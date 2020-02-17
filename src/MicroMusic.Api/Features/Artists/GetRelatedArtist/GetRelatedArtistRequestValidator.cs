using FluentValidation;

namespace MicroMusic.Api.Features.Artists.GetRelatedArtist
{
    public class GetRelatedArtistRequestValidator : AbstractValidator<GetRelatedArtistRequest>
    {
        public GetRelatedArtistRequestValidator()
        {
            RuleFor(r => r.ArtistName)
                .NotEmpty()
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Name of artist must not be null or empty");
        }
    }
}
