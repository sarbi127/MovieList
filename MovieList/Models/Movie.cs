using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Range(1, 5)]
        public float Rating { get; set; }
    }

    public enum Genre
    {
        Horror,
        Thriller,
        Comedy,
        Drama,
        Action
    }
}
