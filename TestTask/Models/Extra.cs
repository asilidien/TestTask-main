using System;
using System.Collections.Generic;

namespace TestTask.Models
{
    [Serializable]
    public class Extra
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public int Accuracy { get; set; }
        public List<int> History { get; set; }

        //public Extra(int ID) { Id = ID; Number = 0; Accuracy = 0; History = new List<int>(); }
        public static List<Extra> AddExtra(List<Extra> Extras)
        {
            //Extras.Add(new Extra(Extras.Count));
            Extras.Add(new Extra { Id = Extras.Count, Number = 0, Accuracy = 0, History = new List<int>() });
            return Extras;
        }
        public static List<Extra> SetNumberFromServer (List<Extra> Extras)
        {
            Random rnd = new Random();
            for (int i = 0; i < Extras.Count; i++)
            {
                Extras[i].Number = rnd.Next(10, 100);
            }
            return Extras;
        }
    }
    

}
