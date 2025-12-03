using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace FilmList.Models
{
    public class FilmMethods
    {
        //Konstruktor
        public FilmMethods(){

        }

        public int InsertFilm(FilmDetails filmDetails, out string errorMessage)
        {
            //Skapa ett connection-objekt för att ansluta mot sql Server  
            SqlConnection sqlConnection = new SqlConnection();

            //Skapa koppling till lokal instans av databas
            sqlConnection.ConnectionString = "Server=localhost;Database=FilmList;User Id=sa;Password=Isal0037;TrustServerCertificate=True;";
            
            //SQL-förfrågan för att infoga en ny film
            String sqlstring = "INSERT INTO Film (FilmTitle, GenreId) VALUES (@FilmTitle, @GenreId)";

            //Skapa ett SqlCommand-objekt för att skicka SQL-förfrågan till databasen
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);
            
            //Skicka med parametrar till SQL-förfrågan
            sqlCommand.Parameters.AddWithValue("@FilmTitle", filmDetails.Title);
            sqlCommand.Parameters.AddWithValue("@GenreId", filmDetails.GenreId);

            try{
                
                //Öppna anslutningen till databasen
                sqlConnection.Open();

                //Exekvera SQL-förfrågan och få antal påverkade rader (ett eller noll)
                int rows = sqlCommand.ExecuteNonQuery();

                errorMessage = "";
                
                return rows;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
            
        }
    }
}