//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System.Collections.Generic;
using System;

namespace MunicipalServicesApp.Classes
{
    //Enum to define the comparison mode as the CompareTo method kept getting overloaded and would sort by parameters I didn't want to
    //Now it is either by Id or by Priority
    public enum ComparisonMode
    {
        ById,
        ByPriority
    }
    //==============================================================[START OF CLASS]============================================================== 
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        //The comparison mode is set to ByPriority by default but both are used
        public static ComparisonMode CurrentComparisonMode { get; set; } = ComparisonMode.ByPriority;
        /// <summary>
        /// Service Request Variables including dependencies and Priority for advanced data structure implementation
        /// </summary>
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int[] Dependencies { get; set; }

        // This is the where the enum is used to determine the comparison mode
        public int CompareTo(ServiceRequest other)
        {
            if (other == null) return 1;

            switch (CurrentComparisonMode)
            {
                case ComparisonMode.ById:
                    return CompareById(other);

                case ComparisonMode.ByPriority:
                    return CompareByPriority(other);

                default:
                    throw new InvalidOperationException("Invalid comparison mode");
            }
        }
        /// <summary>
        /// Compares the Id of the Service Request
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareById(ServiceRequest other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }
        /// <summary>
        /// Compare the Priority of the Service Request
        /// Converts priority to an integer and compares the two
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareByPriority(ServiceRequest other)
        {
            if (other == null) return 1;

            var priorityOrder = new Dictionary<string, int>
            {
                { "High", 1 },
                { "Medium", 2 },
                { "Low", 3 }
            };

            if (priorityOrder[Priority] != priorityOrder[other.Priority])
            {
                return priorityOrder[Priority].CompareTo(priorityOrder[other.Priority]);
            }

            return Id.CompareTo(other.Id);
        }

        /// <summary>
        /// For the array of dependencies, this class is used to store the Id, Title and Status of the dependency
        /// </summary>
        public class Dependency
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Status { get; set; }
        }
    }
}