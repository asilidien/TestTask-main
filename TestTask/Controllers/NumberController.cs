using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace TestTask.Controllers
{
    public class NumberController : Controller
    {

        public ActionResult Index()
        {
            List<Models.Extra> Extras = GetListOfExtras();        
            for (int i = 0; i < 2; i++)
            {
               Extras=Models.Extra.AddExtra(Extras);
            }
            AddExtraToSession(Extras);
            AddInputedToSession(new List<int>());
            return RedirectToAction("GetNumberFromServer", "Number");
        }
        //.......................................................................................
        public void AddExtraToSession(List<Models.Extra> Extras)
        {
            HttpContext.Session.SetString("extras", JsonSerializer.Serialize(Extras));
        }
        public List<Models.Extra> GetExtraFromSession()
        {
            List<Models.Extra> Extras = GetListOfExtras();
            Extras = JsonSerializer.Deserialize<List<Models.Extra>>(HttpContext.Session.GetString("extras"));
            return Extras;
        }
        public List<int> GetInputedFromSession()
        {
            List<int> inputed = new List<int>();
            inputed = JsonSerializer.Deserialize<List<int>>(HttpContext.Session.GetString("inputed"));
            return inputed;
        }
        public void AddInputedToSession (List<int> inputed)
        {
            HttpContext.Session.SetString("inputed", JsonSerializer.Serialize(inputed));
        }
        public List<Models.Extra> GetListOfExtras()
        {
            List<Models.Extra> extra = new List<Models.Extra>();
            return extra;
        }
        //..........................................................................................

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
            List<Models.Extra> Extras = GetListOfExtras();
            Extras = GetExtraFromSession();
            ViewData["Message"] = str;
            Models.DataForViews data = Models.DataForViews.createDataForViews("Результат", null, str, Extras);
            return View(data);
        }

        
        [HttpGet]
        public IActionResult GetNumberFromServer() 
        {
            List<Models.Extra> Extras = GetExtraFromSession();
            Extras = Models.Extra.SetNumberFromServer(Extras);
            AddExtraToSession(Extras);
            Models.DataForViews data = Models.DataForViews.createDataForViews("Ответ", GetInputedFromSession(), "", Extras);
            return View(data);
            
            
        }
        [HttpPost]
       public ActionResult Check (int number)
       {
            if ((number < 101) & (number>9))
            {
                List<int> inputed = new List<int>();
                inputed = GetInputedFromSession();
                inputed.Add(number);
                AddInputedToSession(inputed);

                List<Models.Extra> Extras = GetListOfExtras();
                Extras = GetExtraFromSession();

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
                        if (Extras[i].Accuracy > 0)
                        {
                            Extras[i].Accuracy -= 1;
                        }

                    }
                }
                AddExtraToSession(Extras);
                return RedirectToAction("Result", "Number", new { Ids });
            }
            else
            {
                return RedirectToAction("GetNumberFromServer", "Number");
            }
 
            
       }
        public IActionResult addExtra()
        {
            List<Models.Extra> Extras = GetListOfExtras();

            Extras = GetExtraFromSession();

            Extras = Models.Extra.AddExtra(Extras);
            AddExtraToSession(Extras);

            return RedirectToAction("GetNumberFromServer", "Number");
        }

    }
}
