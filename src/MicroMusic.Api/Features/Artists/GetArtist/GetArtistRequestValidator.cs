using FluentValidation;

namespace MicroMusic.Api.Features.Artists.GetArtist
{
    public class GetArtistRequestValidator : AbstractValidator<GetArtistRequest>
    {
        public GetArtistRequestValidator()
        {
            RuleFor(r => r.ArtistName)
                .NotEmpty()
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("Name of artist must not be null or empty");
        }
    }
}
