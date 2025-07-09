using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Minutes { get; set; }
        public int GenreId { get; set; }
        public required Genre Genre { get; set; }
        public List<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
