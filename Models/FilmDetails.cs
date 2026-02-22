using System.ComponentModel.DataAnnotations;

namespace FilmList.Models
{
    public class FilmDetails
    {
        public int FilmId { get; set; }

        [Required(ErrorMessage = "Film title is required.")]
        public string FilmTitle { get; set; }

        [Required(ErrorMessage = "Genre ID is required.")]
        public int GenreId { get; set; }

        public FilmDetails(string filmTitle, int filmId, int genreId)
        {
            FilmTitle = filmTitle;
            FilmId = filmId;
            GenreId = genreId;
        }
        public FilmDetails()
        {

        }
    }
}