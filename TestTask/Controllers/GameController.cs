using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class GameController : Controller
    {

        Game game = new Game();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Start()
        {      
            try
            {
                if (!HttpContext.Session.Keys.Contains("game"))
                {
                    HttpContext.Session.Save(game);
                }
                game = HttpContext.Session.Load();
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
            game.AskPsychicsForAnswer();
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
                if (game.Validate(number))
                {
                    game = HttpContext.Session.Load();
                    game.CheckNumber(number);
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
