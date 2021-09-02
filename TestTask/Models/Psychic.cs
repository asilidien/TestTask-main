using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestTask.Models
{
    [Serializable]
    public class Psychic
    {
        [JsonInclude]
        public int Id { get; private set; }
        [JsonInclude]
        public int Number { get; private set; }
        [JsonInclude]
        public int Accuracy { get; private set; }
        [JsonInclude]
        public List<int> History { get; private set; }


        public void GuessTheNumber(int minValue, int maxValue)
        {
            Random rnd = new Random();
            Number = rnd.Next(minValue, maxValue);
            History.Add(Number);
        }
        public static Psychic GetPsychic(int id)
        {
            Psychic psychic = new Psychic() { Id = id, Number = 0, Accuracy = 0, History = new List<int>() };
            return psychic;
        }
        public void UpAccuracy()
        {
            Accuracy++;
        }
        public void DownAccuracy()
        {
            if (Accuracy > 0)
            {
                Accuracy--;
            }
        }
    }


}
