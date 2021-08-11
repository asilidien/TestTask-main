using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    [Serializable]
    public class Extra
    {
        public int Id { get; set; }

        public int Number { get; set; }
        public int Accuracy { get; set; }
        public List<int> History { get; set; }
    }

}
