using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain
{
    public class MovieActor
    {
        public int ActorId { get; set; }
        public required Actor Actor { get; set; }
        public int MovieId { get; set; }
        public required Movie Movie { get; set; }
    }
}
