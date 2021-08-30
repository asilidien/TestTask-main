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
            string str = JsonSerializer.Serialize(game);
            session.SetString("game", str);
            
            return;
        }
        public static Game Load(this ISession session)
        {
            return JsonSerializer.Deserialize<Game>(session.GetString("game"));
        }
    }
}
