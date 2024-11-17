using MunicipalServicesApp.Models.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
namespace MunicipalServicesApp.Classes
{    
    //==============================================================[START OF CLASS]==============================================================
    public class SavedReports
    {
        //Global List to store all reports
        private static List<Report> reports = new List<Report>();

        //==============================================================[START OF AddReport]==============================================================
        //Method to add a report to the list
        public static void AddReport(Report report)
        {
            reports.Add(report);
        }
        //==============================================================[END OF AddReport]==============================================================

        //==============================================================[START OF GetReports]==============================================================
        //Method to get all reports
        public static List<Report> GetReports()
        {
            return reports;
        }
        //HAD TO UPDATE FOR PART THREE TO INCLUDE PRIORITY FOR USE OF HEAPS
        /// <summary>
        /// This is where you'll find the use of heaps in my project, specifically the MinHeap and MaxHeap classes.
        /// This method returns a list of reports sorted by priority in ascending or descending order.
        /// It is very efficient in comparison to standard list search/order by methods
        /// </summary>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public static List<Report> GetReportsSortedByPriority(bool ascending = true)
        {
            if (ascending)
            {
                var minHeap = new MinHeap<Report>();
                foreach (var report in reports)
                {
                    minHeap.Add(report);
                }
                var sortedReports = new List<Report>();
                while (minHeap.Size > 0)
                {
                    sortedReports.Add(minHeap.Remove());
                }
                return sortedReports;
            }
            else
            {
                var maxHeap = new MaxHeap<Report>();
                foreach (var report in reports)
                {
                    maxHeap.Add(report);
                }
                var sortedReports = new List<Report>();
                while (maxHeap.Size > 0)
                {
                    sortedReports.Add(maxHeap.Remove());
                }
                return sortedReports;
            }
        }
        //==============================================================[END OF GetReports]==============================================================
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================