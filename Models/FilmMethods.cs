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
            String sqlstring = "INSERT INTO Film (FilmTitle, GenreId) VALUES (@FilmTitle, @GenreId)";

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
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
            
        }

        public List<FilmDetails> GetUserDetailsList(out string errorMessage)
        {
             //Skapa ett connection-objekt för att ansluta mot sql Server  
            SqlConnection sqlConnection = new SqlConnection();

            //Skapa koppling till lokal instans av databas
            sqlConnection.ConnectionString = "Server=localhost;Database=FilmList;User Id=sa;Password=Isal0037;TrustServerCertificate=True;";
            
            //SQL-förfrågan för att hämta alla filmer
            String sqlstring = "SELECT * FROM Film";

            //Skapa ett SqlCommand-objekt för att skicka SQL-förfrågan till databasen
            SqlCommand sqlCommand = new SqlCommand(sqlstring, sqlConnection);

            //Skapa ett SqlDataAdapter-objekt för att fylla ett DataSet med data från databasen
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataSet dataSet = new DataSet();

            List<FilmDetails> filmDetailsList = new List<FilmDetails>();

            try{
                
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet, "Film");

                int i = 0;
                int count = 0;

                count = dataSet.Tables["Film"].Rows.Count;

                if (count > 0){
                    
                    while (i < count)
                    {
                        FilmDetails filmDetails = new FilmDetails();

                        filmDetails.Id = Convert.ToInt32(dataSet.Tables["Film"].Rows[i]["FilmId"]);
                        filmDetails.FilmTitle = dataSet.Tables["Film"].Rows[i]["FilmTitle"].ToString();
                        filmDetails.GenreId = Convert.ToInt32(dataSet.Tables["Film"].Rows[i]["GenreId"]);

                        filmDetailsList.Add(filmDetails);

                        i++;
                    }
                    errorMessage = "";
                    return filmDetailsList;
                }else{
                    errorMessage = "No films found.";
                    return filmDetailsList;
                }

                
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return filmDetailsList;
            }
            finally
            {
                sqlConnection.Close();
            }

        }
        
    }
}