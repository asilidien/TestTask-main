using System;
using System.Collections.Generic;

namespace TestTask.Models
{
    [Serializable]
    public class Psychic
    {
        public int Id { get; private set; }

        public int Number { get; private set; }
        public int Accuracy { get; private set; }
        public List<int> History { get; private set; }


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
        public void SetNumber(int number)
        {
            Number = number;
        }
        public void ChangeAccuracy (bool Direction)
        {
            if (Direction)
            {
                Accuracy++;
            }
            else
            {
                if (Accuracy>0)
                {
                    Accuracy--;
                }
                
            }
        }

    }
    

}
