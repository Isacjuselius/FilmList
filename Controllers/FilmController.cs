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

        public IActionResult SelectFilm()
        {
            List<FilmDetails> filmDetailsList = new List<FilmDetails>();
            FilmMethods filmMethods = new FilmMethods();
            string errorMessage = "";
            filmDetailsList = filmMethods.GetUserDetailsList(out errorMessage);

            ViewBag.errorMessage = errorMessage;

            return View(filmDetailsList);
        }
    }
}