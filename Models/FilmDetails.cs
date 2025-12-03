namespace FilmList.Models
{
    public class FilmDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }

        public FilmDetails(string title, int id, int genreId)
        {
            Title = title;
            Id = id;
            GenreId = genreId;
        }
        public FilmDetails()
        {

        }
    }
}