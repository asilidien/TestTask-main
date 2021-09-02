using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public static class SessionExtention
    {
        public static void Save(this ISession session, Game game)
        {
            session.SetString("game", JsonSerializer.Serialize(game));
            return;
        }
        public static Game Load(this ISession session)
        {
            string sww = session.GetString("game");
            Game game = JsonSerializer.Deserialize<Game>(sww);
            return JsonSerializer.Deserialize<Game>(session.GetString("game")); 
        }
    }
}
