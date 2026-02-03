using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
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
            String sqlstring = "INSERT INTO dbo.Film (FilmTitle, GenreId) VALUES (@FilmTitle, @GenreId)";

            //Skapa ett SqlCommand-objekt för att skicka SQL-förfrågan till databasen
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);
            
            //Skicka med parametrar till SQL-förfrågan
            sqlCommand.Parameters.AddWithValue("@FilmTitle", filmDetails.FilmTitle);
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
                Console.WriteLine("INSERT ERROR: " + e.Message);
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
            
        }

        public List<FilmDetails> GetFilmDetailsList(out string errorMessage)
        {
             //Skapa ett connection-objekt för att ansluta mot sql Server  
            SqlConnection sqlConnection = new SqlConnection();

            //Skapa koppling till lokal databas
            sqlConnection.ConnectionString = "Server=localhost;Database=FilmList;User Id=sa;Password=Isal0037;TrustServerCertificate=True;";
            
            //SQL-förfrågan för att hämta alla filmer
            String sqlstring = "SELECT * FROM Film";

            //Skapa ett SqlCommand-objekt för att skicka SQL-förfrågan till databasen
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            //Skapa ett SqlDataAdapter-objekt för att fylla en DataTable med resultatet från SQL-förfrågan
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataSet dataSet = new DataSet();

            List<FilmDetails> filmDetailsList = new List<FilmDetails>();

            try{
                
                sqlConnection.Open();
                
                //Lägger till en tabell med namnet "Film" i DataSet-objektet och fyller den med resultatet från SQL-förfrågan
                sqlDataAdapter.Fill(dataSet, "Film");

                int count = dataSet.Tables["Film"].Rows.Count;
                int i = 0;    

                if (count > 0)
                {
                    while (i < count)
                    {
                        //Läser ut varje rad från tabellen och skapar ett FilmDetails-objekt som läggs till i en lista
                        FilmDetails filmDetails = new FilmDetails();
                        filmDetails.FilmId = Convert.ToInt16(dataSet.Tables["Film"].Rows[i]["FilmId"]);
                        filmDetails.FilmTitle = dataSet.Tables["Film"].Rows[i]["FilmTitle"].ToString();
                        filmDetails.GenreId = Convert.ToInt16(dataSet.Tables["Film"].Rows[i]["GenreId"]);

                        filmDetailsList.Add(filmDetails);
                        i++;
                    }
                    errorMessage = "";
                    return filmDetailsList;
                }
                else
                {
                    errorMessage = "No films found.";
                    return null;
                }
    
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public List<FilmDetails> GetFilteredFilmDetailsList(int genreId, out string errorMessage)
        {
             //Skapa ett connection-objekt för att ansluta mot sql Server  
            SqlConnection sqlConnection = new SqlConnection();

            //Skapa koppling till lokal databas
            sqlConnection.ConnectionString = "Server=localhost;Database=FilmList;User Id=sa;Password=Isal0037;TrustServerCertificate=True;";
            
            //SQL-förfrågan för att hämta alla filmer
            String sqlstring = "SELECT * FROM Film WHERE GenreId = @GenreId";

            //Skapa ett SqlCommand-objekt för att skicka SQL-förfrågan till databasen
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@GenreId", genreId);

            //Skapa ett SqlDataAdapter-objekt för att fylla en DataTable med resultatet från SQL-förfrågan
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataSet dataSet = new DataSet();

            List<FilmDetails> filmDetailsList = new List<FilmDetails>();

            try{
                
                sqlConnection.Open();
                
                //Lägger till en tabell med namnet "Film" i DataSet-objektet och fyller den med resultatet från SQL-förfrågan
                sqlDataAdapter.Fill(dataSet, "Film");

                int count = dataSet.Tables["Film"].Rows.Count;
                int i = 0;    

                if (count > 0)
                {
                    while (i < count)
                    {
                        //Läser ut varje rad från tabellen och skapar ett FilmDetails-objekt som läggs till i en lista
                        FilmDetails filmDetails = new FilmDetails();
                        filmDetails.FilmId = Convert.ToInt16(dataSet.Tables["Film"].Rows[i]["FilmId"]);
                        filmDetails.FilmTitle = dataSet.Tables["Film"].Rows[i]["FilmTitle"].ToString();
                        filmDetails.GenreId = Convert.ToInt16(dataSet.Tables["Film"].Rows[i]["GenreId"]);

                        filmDetailsList.Add(filmDetails);
                        i++;
                    }
                    errorMessage = "";
                    return filmDetailsList;
                }
                else
                {
                    errorMessage = "No films found.";
                    return new List<FilmDetails>()  ;
                }
    
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return new List<FilmDetails>();
            }
            finally
            {
                sqlConnection.Close();
            }

        }
        
    }
}