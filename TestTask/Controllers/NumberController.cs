using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class NumberController : Controller
    {
        
        Game game = new Game();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Start ()
        {
            if (!HttpContext.Session.Keys.Contains("game"))
            {
                HttpContext.Session.Save(game);
            }

            game = HttpContext.Session.Load();

            try
            {
                game.StartTheGame();
            }
            catch
            {
                //TO DO
            }
            HttpContext.Session.Save(game);
            return RedirectToAction("GetNumberFromServer", "Number");
        }
        public ActionResult Result()// rename
        {
            return View(game);
        }


        [HttpGet]
        public IActionResult GetNumberFromServer()// rename
        {
            game = HttpContext.Session.Load();
            game.GetAnswerFromPsychics();
            HttpContext.Session.Save(game);
            return View(game);


        }
        [HttpPost]
        public ActionResult CheckNumbers(int number)
        {
            game.CheckNumbers(number);

            return RedirectToAction("Result", "Number");

        }
    }
}
