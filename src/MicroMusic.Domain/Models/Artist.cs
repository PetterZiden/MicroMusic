using System.Collections.Generic;

namespace MicroMusic.Domain.Models
{
    public class Artist
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Genres { get; set; }

        public string Type { get; set; }

        public int Popularity { get; set; }
    }
}
