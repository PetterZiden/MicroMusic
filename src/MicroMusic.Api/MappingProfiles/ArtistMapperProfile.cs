using AutoMapper;

using MicroMusic.Domain.Models;

using SpotifyAPI.Web.Models;

namespace MicroMusic.Api.MappingProfiles
{
    public class ArtistMapperProfile : Profile
    {
        public ArtistMapperProfile()
        {
            CreateMap<FullArtist, Artist>();
        }
    }
}
