using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class DataForViews
    {
        public string Title { get; set; }
        public List<int> Inputed { get; set; }
        public string Message { get; set; }
        public List<Extra> Extras { get; set; } 

        public static DataForViews createDataForViews (string Title, List<int> Inputed, string Message, List<Extra> Extras)
        {
            DataForViews data = new Models.DataForViews { Title = Title, Message = Message, Inputed = Inputed, Extras = Extras };


            return data;
        }
    } 
}
