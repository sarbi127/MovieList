﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieListContext _context;

        public MoviesController(MovieListContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        } 
        
        public async Task<IActionResult> Index2()
        {
           var movies =  await _context.Movie.ToListAsync();

            var model = new MovieViewModel()
            {
                Movies = movies,
                Genres = await GetGenresAsync()
            };

            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> GetGenresAsync()
        {
            return await _context.Movie
                .Select(g => g.Genre)
                .Distinct()
                .Select(m => new SelectListItem
                {
                    Text = m.ToString(),
                    Value = m.ToString()

                })
                .ToListAsync();
        }

        public async Task<IActionResult> Filter(string title, int? genre)
        {
            var model = string.IsNullOrWhiteSpace(title) ?
                await _context.Movie.ToListAsync() :
                await _context.Movie.Where(m => m.Title == title).ToListAsync();

            model = genre == null ?
                model :
                model.Where(m => m.Genre == (Genre)genre).ToList();

            return View(nameof(Index), model);
        }


        public async Task<IActionResult> Filter2(MovieViewModel viewModel)
        {
            var movies = string.IsNullOrWhiteSpace(viewModel.Title) ?
                await _context.Movie.ToListAsync() :
                await _context.Movie.Where(m => m.Title == viewModel.Title).ToListAsync();

            movies = viewModel.Genre == null ?
               movies :
               movies.Where(m => m.Genre == viewModel.Genre).ToList();

            var model = new MovieViewModel()
            {
                Movies = movies,
                Genres = await GetGenresAsync()
            };

            return View(nameof(Index2), model);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
