using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace TestTask.Controllers
{
    public class NumberController : Controller
    {
        //public static List<Models.Extra> Extras = new List<Models.Extra>();

        public ActionResult Index()
        {
            List<Models.Extra> Extras = GetList();
            HttpContext.Session.SetString("extras", JsonSerializer.Serialize(Extras));
            HttpContext.Session.SetString("inputed", JsonSerializer.Serialize(new List<int>()));
            for (int i = 0; i < 2; i++)
            {
                addExtra();
            }
            return RedirectToAction("get", "Number");
        }
        public List<Models.Extra> GetList()
        {
            List<Models.Extra> extra = new List<Models.Extra>();
            return (extra);
        }
        public ActionResult Result(List<int> Ids)
        {
            string str = "Вам удалось обмануть наших экстрасенсов: никто из них не угадал ваше число";
            if (Ids.Count!=0)
            {
                str = "Ваше число угадали экстрасенсы с номерами: ";
                for (int i =0;i<Ids.Count;i++)
                {
                    str += Ids[i].ToString() + ", ";
                }
                
            }
            List<Models.Extra> Extras = GetList();
            Extras = JsonSerializer.Deserialize<List<Models.Extra>>(HttpContext.Session.GetString("extras"));
            ViewData["Message"] = str;
            return View(Extras);
        }
        
        [HttpGet]
        public IActionResult get() 
        {

            List<Models.Extra> Extras = GetList();
            Extras = JsonSerializer.Deserialize<List<Models.Extra>>(HttpContext.Session.GetString("extras"));
            Random rnd = new Random();
                for (int i = 0; i < Extras.Count; i++)
                {
                Extras[i].Number = rnd.Next(10, 100);
                }
            HttpContext.Session.SetString("extras", JsonSerializer.Serialize(Extras));
            ViewBag.inputed = JsonSerializer.Deserialize<List<int>>(HttpContext.Session.GetString("inputed"));
            return View(Extras);
            
            
        }
        [HttpPost]
       public ActionResult Check (int number)
       {
            List<int> inputed = new List<int>();
            inputed = JsonSerializer.Deserialize<List<int>>(HttpContext.Session.GetString("inputed"));
            inputed.Add(number);
            HttpContext.Session.SetString("inputed", JsonSerializer.Serialize(inputed));

            List<Models.Extra> Extras = GetList();
            Extras = JsonSerializer.Deserialize<List<Models.Extra>>(HttpContext.Session.GetString("extras"));
            List<int> Ids = new List<int>();
            for (int i = 0; i < Extras.Count; i++)
            {
                Extras[i].History.Add(Extras[i].Number);
                if (Extras[i].Number == number)
                {
                    if (Extras[i].Accuracy < 10)
                    {
                        Extras[i].Accuracy += 1;
                    }
                    Ids.Add(Extras[i].Id);
                }
                else
                {
                    if (Extras[i].Accuracy>0)
                    {
                        Extras[i].Accuracy -= 1;
                    }
                    
                }
            }
            HttpContext.Session.SetString("extras", JsonSerializer.Serialize(Extras));
            return RedirectToAction ("Result", "Number", new { Ids } );
       }
        public IActionResult addExtra()
        {
            List<Models.Extra> Extras = GetList();

            Extras = JsonSerializer.Deserialize<List<Models.Extra>>(HttpContext.Session.GetString("extras"));

            Extras.Add(new Models.Extra { Id = Extras.Count, Number = 0, Accuracy = 0, History = new List<int>() });
            HttpContext.Session.SetString("extras", JsonSerializer.Serialize(Extras));

            return RedirectToAction("get", "Number");
        }

    }
}
