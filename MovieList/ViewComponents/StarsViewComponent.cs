using Microsoft.AspNetCore.Mvc;
using MovieList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.ViewComponents
{
    public class StarsViewComponent : ViewComponent
    {
        private readonly MovieListContext context;

        public StarsViewComponent(MovieListContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int movieId)
        {
            var movie = await context.Movie.FindAsync(movieId);

            var doubleRating = (int)Math.Round(movie.Rating * 2);

            var model = new StarViewModel()
            {
                Stars = doubleRating / 2,
                IsHalfStar = doubleRating % 2 == 1
            };

            return View(model);
        }
    }
}
