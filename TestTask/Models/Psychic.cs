using System;
using System.Collections.Generic;

namespace TestTask.Models
{
    [Serializable]
    public class Psychic
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public int Accuracy { get; set; }
        public List<int> History { get; set; }


        public static int GetTheNumberFromPsychic (int MinValue, int MaxValue)
        {
            Random rnd = new Random();
            return rnd.Next(MinValue,MaxValue);
        }
        public Psychic AddPsychic (int Id)
        {
            Psychic psychic = new Psychic(){ Id = Id, Number = 0, Accuracy = 0, History = new List<int>()};
            return psychic;
        }
        
    }
    

}
