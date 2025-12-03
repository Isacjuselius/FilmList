namespace FilmList.Models
{
    public class FilmDetails
    {
        public int Id { get; set; }
        public string FilmTitle { get; set; }
        public int GenreId { get; set; }

        public FilmDetails(string filmTitle, int id, int genreId)
        {
            FilmTitle = filmTitle;
            Id = id;
            GenreId = genreId;
        }
        public FilmDetails()
        {

        }
    }
}