using System;
using System.Collections.Generic;

namespace HelloEntityFramework.DataAccess
{
    public partial class Genre
    {
        public Genre()
        {
            Movie = new HashSet<Movie>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }
        public DateTime? DateModified { get; set; }

        // there's "redundant" data in what EF returns, especially when
        // we include nav properties with .Include.
        // EF is smart enough that we can make a change to only one of those
        // redundant places, and it will do the right thing and fix up the other
        // places when you call .SaveChanges.

        // e.g. I could do new Movie { }
        public virtual ICollection<Movie> Movie { get; set; }
    }
}
