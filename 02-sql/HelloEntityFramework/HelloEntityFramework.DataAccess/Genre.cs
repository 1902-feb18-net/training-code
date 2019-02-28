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

        public virtual ICollection<Movie> Movie { get; set; }
    }
}
