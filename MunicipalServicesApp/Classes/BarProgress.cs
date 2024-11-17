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
    public class BarProgress
    {
        //Global Variables to store save progress of a report
        private bool locSaved;
        private bool issueSaved;
        private bool descSaved;
        private bool imgSaved;
        private bool prioritySaved;

        //==============================================================[START OF BarProgress]==============================================================
        //Blank Constructor
        public BarProgress()
        {

        }

        //==============================================================[END OF BarProgress]==============================================================

        //==============================================================[START OF Parameterized BarProgress]==============================================================
        //Parameterized Constructor
        //HAD TO UPDATE FOR PART THREE TO INCLUDE PRIORITY FOR USE OF HEAPS
        public BarProgress(bool locSaved, bool issueSaved, bool descSaved, bool imgSaved, bool prioritySaved)
        {
            this.LocSaved = locSaved;
            this.IssueSaved = issueSaved;
            this.DescSaved = descSaved;
            this.ImgSaved = imgSaved;
            this.prioritySaved = prioritySaved;
        }
        //==============================================================[END OF Parameterized BarProgress]==============================================================

        //==============================================================[START OF Getters and Setters]==============================================================
        //Getters and Setters
        public bool LocSaved { get => locSaved; set => locSaved = value; }
        public bool IssueSaved { get => issueSaved; set => issueSaved = value; }
        public bool DescSaved { get => descSaved; set => descSaved = value; }
        public bool ImgSaved { get => imgSaved; set => imgSaved = value; }
        public bool PrioritySaved { get => prioritySaved; set => prioritySaved = value; }
        //==============================================================[END OF Getters and Setters]==============================================================
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================
