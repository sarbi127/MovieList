using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public class MovieViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }

        public Genre? Genre { get; set; }
        public string Title { get; set; }

    }
}
