using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
namespace MunicipalServicesApp.Classes
{
    //==============================================================[START OF CLASS]==============================================================
    //Class to store the details of a report
    //HAD TO UPDATE FOR PART THREE TO INCLUDE PRIORITY FOR USE OF HEAPS

    public class Report : IComparable<Report>
    {
        public string Location { get; set; }
        public string IssueType { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public int Priority { get; set; }
        /// <summary>
        /// For sorting the saved reports by priority
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Report other)
        {
            return this.Priority.CompareTo(other.Priority);
        }
    }

    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================