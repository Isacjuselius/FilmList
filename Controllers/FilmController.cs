using Microsoft.AspNetCore.Mvc;
using FilmList.Models;
using System.Diagnostics;
using System.Linq.Expressions;

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
            if (!ModelState.IsValid)
            {
                return View(filmDetails);
            }
            
            FilmMethods filmMethods = new FilmMethods();
            string error = "";
            
            //Ger 1 eller 0 beroende på om infogningen lyckades eller inte
            int i = filmMethods.InsertFilm(filmDetails, out error);

            if (i == 0)
            {
                ViewBag.error = "Ingen rad påverkades.";
                return View();
            } else
            {
                return RedirectToAction("SelectFilm");
            }

            
        }

        public IActionResult SelectFilm(string searchString, bool sorted)
        {
            
            FilmMethods filmMethods = new FilmMethods();
            string errorMessage = "";
            List<FilmDetails> filmDetailsList = filmMethods.GetFilmDetailsList(out errorMessage);

            //om användaren sorterar
            if(sorted)
            {
                filmDetailsList = filmDetailsList.OrderBy(f => f.FilmTitle).ToList();
            }

            if(!string.IsNullOrEmpty(searchString))
            {
                filmDetailsList = filmDetailsList.Where(f => f.FilmTitle.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }


            ViewBag.errorMessage = errorMessage;

            return View(filmDetailsList);
        }

    
        [HttpPost]  
        public IActionResult FilterFilm(int genre)
        {
            FilmMethods filmMethods = new FilmMethods();

            if (genre == 0)
            {
                return RedirectToAction("SelectFilm");
            }

            List<FilmDetails> filteredFilmDetailsList = filmMethods.GetFilteredFilmDetailsList(genre,out string errorMessage);

            ViewBag.errorMessage = errorMessage;
            return View("SelectFilm", filteredFilmDetailsList);
        }

        public IActionResult DeleteFilm(int id)
        {
            FilmMethods filmMethods = new FilmMethods();
            string errorMessage = "";
            int i = filmMethods.DeleteFilmByID(id, out errorMessage);

            if (i == 0)
            {
                ViewBag.errorMessage = "Ingen rad påverkades.";
                return RedirectToAction("SelectFilm");
            }
            else
            {
                return RedirectToAction("SelectFilm");
            }
        }

        [HttpGet]
        public IActionResult EditFilm(int id)
        {
            FilmMethods filmMethods = new FilmMethods();
            string errorMessage = "";
            FilmDetails filmDetails = filmMethods.GetFilmByID(id, out errorMessage);

            if (filmDetails == null)
            {
                ViewBag.errorMessage = "Filmen hittades inte.";
                return RedirectToAction("SelectFilm");
            }
            else
            {
                ViewBag.errorMessage = errorMessage;
                return View(filmDetails);
            }
        }

        [HttpPost]
        public IActionResult EditFilm(FilmDetails filmDetails)
        {
            FilmMethods filmMethods = new FilmMethods();
            string errorMessage = "";

            int i = filmMethods.EditFilmByFilmDetails(filmDetails, out errorMessage);
            if (i == 0)
            {
                Console.WriteLine("No rows affected for film ID: " + filmDetails.FilmId);
                ViewBag.errorMessage = "Ingen rad påverkades.";
            }
            return RedirectToAction("SelectFilm");
        }
    }
}