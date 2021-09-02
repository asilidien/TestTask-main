using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestTask.Models
{
    [Serializable]
    public class Game
    {
        Psychic psychic = new Psychic();
        [JsonInclude]
        public List<Psychic> Psychics { get; private set; } = new List<Psychic>();
        [JsonInclude]
        public List<int> Usernumbers { get; private set; } = new List<int>();
        [JsonInclude]
        public string Message { get; private set; }

        readonly int MaxValue = 100;
        readonly int MinValue = 10;
        readonly int CountOfPsychics = 2;


        public void StartTheGame()
        {
            for (int i = 0; i < CountOfPsychics; i++)
            {
                Psychics.Add(psychic.AddPsychic(Psychics.Count));
            }
        }
        public void AddPsychic()
        {
            Psychics.Add(psychic.AddPsychic(Psychics.Count));
        }
        public void GetAnswerFromPsychics()
        {
            for (int i = 0; i < Psychics.Count; i++)
            {
                Psychics[i].SetNumber(Psychic.GetTheNumberFromPsychic(MinValue, MaxValue));
                Psychics[i].History.Add(Psychics[i].Number);
            }
        }
        public bool CheckIfValid(int number)
        {
            if ((number > MinValue) && (number < MaxValue))
            {
                return true;
            }

            return false;

        }
        public void CheckNumbers(int number)
        {

            List<int> LuckyIds = new List<int>();
            if ((number > MinValue) && (number < MaxValue))
            {
                for (int i = 0; i < Psychics.Count; i++)
                {
                    if (Psychics[i].Number == number)
                    {
                        Psychics[i].ChangeAccuracy(true);
                        LuckyIds.Add(Psychics[i].Id);
                    }

                }
                Usernumbers.Add(number);

            }
            ReturnRoundResult(LuckyIds);

        }
        public void ReturnRoundResult(List<int> Luckyids)
        {
            Message = "Вам удалось обмануть наших экстрасенсов: никто из них не угадал ваше число";
            if (Luckyids.Count != 0)
            {
                Message = "Ваше число угадали экстрасенсы с номерами: ";
                for (int i = 0; i < Luckyids.Count; i++)
                {
                    Message += Luckyids[i].ToString() + ", ";
                }

            }
        }

    }
}
