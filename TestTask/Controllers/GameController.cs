using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class GameController : Controller
    {

        Models.Game game = new Models.Game();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Start()
        {
            if (!HttpContext.Session.Keys.Contains("game"))
            {
                HttpContext.Session.Save(game);
            }
            game = HttpContext.Session.Load();
            try
            {
                game.StartTheGame();
                HttpContext.Session.Save(game);
                return RedirectToAction("GoToTheMainWindow");
            }
            catch
            {
                return RedirectToAction("ShowError");
            }

        }
        public IActionResult GetResult()
        {
            try
            {
                game = HttpContext.Session.Load();
                return View(game);
            }
            catch
            {
                return RedirectToAction("ShowError");
            }

        }
        public IActionResult ShowError()
        {
            return View();
        }
        public void GetNumberFromServer()
        {
            game = HttpContext.Session.Load();
            game.GetAnswerFromPsychics();
            HttpContext.Session.Save(game);
        }

        [HttpGet]
        public IActionResult GoToTheMainWindow()
        {
            try
            {
                GetNumberFromServer();
                game = HttpContext.Session.Load();
                return View(game);
            }
            catch
            {
                return RedirectToAction("ShowError");
            }
        }
        [HttpPost]
        public ActionResult CheckNumbers(int number)
        {
            try
            {
                if (game.CheckIfValid(number))
                {
                    game = HttpContext.Session.Load();
                    game.CheckNumbers(number);
                    HttpContext.Session.Save(game);
                    return RedirectToAction("GetResult");
                }
                return RedirectToAction("GoToTheMainWindow");
            }
            catch
            {
                return RedirectToAction("ShowError");
            }

        }
        public IActionResult AddPsychic()
        {
            try
            {
                game = HttpContext.Session.Load();
                game.AddPsychic();
                HttpContext.Session.Save(game);
                return RedirectToAction("GoToTheMainWindow");
            }

            catch
            {
                return RedirectToAction("ShowError");
            }
        }
    }
}
