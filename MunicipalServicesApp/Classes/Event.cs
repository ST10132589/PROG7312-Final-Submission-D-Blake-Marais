//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Classes
{
    //==============================================================[START OF CLASS]============================================================== 
    public class Event
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EventType { get; set; }
        public bool IsInterested { get; set; }
        public int Ranking { get; set; } 
        public double RecommendationScore { get; set; } 
    }
    //==============================================================[END OF CLASS]============================================================== 

}
//==============================================================[END OF FILE]==============================================================