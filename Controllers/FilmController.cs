using Microsoft.AspNetCore.Mvc;
using FilmList.Models;

namespace FilmList.Controllers
{
    public class FilmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InsertFilm()
        {
            return View();
        }
        
        /*[HttpPost]
        public IActionResult InsertFilm(FilmDetails filmDetails)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            
            var filmMethods = new FilmMethods();
            string errorMessage = "";
            

            filmMethods.InsertFilm(filmDetails, out errorMessage);

            int rows = filmMethods.InsertFilm(filmDetails, out errorMessage);

            if (rows == 1 && string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.Message = "Film inserted successfully.";
            }
            else
            {
                ViewBag.Message = "Error inserting film: " + errorMessage;
            }

            return View();
        }*/


        [HttpPost]
        public IActionResult InsertFilm(FilmDetails filmDetails)
        {
            FilmMethods filmMethods = new FilmMethods();
            int i = 0;
            string error = "";
            
            //Ger 1 eller 0 beroende på om infogningen lyckades eller inte
            i = filmMethods.InsertFilm(filmDetails, out error);

            ViewBag.error = error;
            ViewBag.rows = i;

            ModelState.Clear();
            return View();
        }
    }
}