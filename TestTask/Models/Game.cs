using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestTask.Models
{
    [Serializable]
    public class Game
    {
        [JsonInclude]
        public List<Psychic> Psychics { get; private set; } = new List<Psychic>();
        [JsonInclude]
        public List<int> UserNumbers { get; private set; } = new List<int>();
        [JsonInclude]
        public string Message { get; private set; }

        const int MaxValue = 100;
        const int MinValue = 10;
        const int CountOfPsychics = 2;


        public void StartTheGame()
        {
            for (int i = 0; i < CountOfPsychics; i++)
            {
                Psychics.Add(Psychic.GetPsychic(Psychics.Count));
            }
        }
        public void AddPsychic()
        {
            Psychics.Add(Psychic.GetPsychic(Psychics.Count));
        }
        public void AskPsychicsForAnswer()
        {
            for (int i = 0; i < Psychics.Count; i++)
            {
                Psychics[i].GuessTheNumber(MinValue, MaxValue);
            }
        }
        public bool Validate(int number)
        {
            return (number > MinValue) && (number < MaxValue);
        }
        public void CheckNumber(int number)
        {
            List<int> luckyIds = new List<int>();
            UserNumbers.Add(number);
            for (int i = 0; i < Psychics.Count; i++)
            {
                if (Psychics[i].Number == number)
                {
                    Psychics[i].UpAccuracy();
                    luckyIds.Add(Psychics[i].Id);
                } 
                else
                {
                    Psychics[i].DownAccuracy();
                }
            }
            ReturnRoundResult(luckyIds);

        }
        public void ReturnRoundResult(List<int> luckyIds)
        {
            Message = "Вам удалось обмануть наших экстрасенсов: никто из них не угадал ваше число";

            if (luckyIds.Count == 0)
            {
                return;
            }

            Message = "Ваше число угадали экстрасенсы с номерами: ";
            Message += string.Join(", ", luckyIds);
        }
    }
}
