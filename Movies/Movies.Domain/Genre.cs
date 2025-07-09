using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
