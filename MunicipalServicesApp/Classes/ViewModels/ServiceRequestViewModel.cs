//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Models.GraphStructures;
using MunicipalServicesApp.Models.TreeStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static MunicipalServicesApp.Classes.ServiceRequest;

namespace MunicipalServicesApp.Classes.ViewModels
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// The View Model uses INotiftPropertyChanged for the searching an filtering to update the UI
    /// </summary>
    public class ServiceRequestViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Property for the service requests
        /// </summary>
        private ServiceRequest _selectedRequest;
        private BinarySearchTree<ServiceRequest> requestBST; // Binary Search Tree of Service Requests
        private AVLTree<ServiceRequest> requestAVL; //AVL Tree of Service Requests
        private BinaryTree<ServiceRequest> globalBinaryTree; //Binary Tree of Service Requests
        private MyGraph dependencyGraph; //Graph Object to pass Service Requests into
        public Dictionary<int, ServiceRequest> ServiceRequests { get; private set; } //Dictionary of Service Requests

        /// <summary>
        /// Getter and setter of Selected Request, used to update UI
        /// </summary>
        public ServiceRequest SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                if (_selectedRequest != value)
                {
                    _selectedRequest = value;
                    OnPropertyChanged(nameof(SelectedRequest)); // Notify UI to update
                }
            }
        }

        /// <summary>
        /// Constructor for the ServiceRequestViewModel class 
        /// </summary>
        public ServiceRequestViewModel()
        {
            ServiceRequests = new Dictionary<int, ServiceRequest>();
            requestBST = new BinarySearchTree<ServiceRequest>();
            requestAVL = new AVLTree<ServiceRequest>();
            globalBinaryTree = new BinaryTree<ServiceRequest>();
            dependencyGraph = new MyGraph();
            GenerateDummyData();
        }

        /// <summary>
        /// This method generates the 20 service requests that you can view on ServiceRequestWindow.xaml
        /// There are 5 that have 3 dependencies, 5 that have 2 dependencies, 5 that have 1 dependency,
        /// and 5 that have no dependencies.
        /// This is to show varied data on the graph view.
        /// Once they are created they are added to the Dictionary, Binary Search Tree, AVL Tree, and Binary Tree.
        /// Finally, all of the nodes and edges are generated for the graph view.
        /// </summary>
        private void GenerateDummyData()
        {
            var requests = new List<ServiceRequest>
            {
                new ServiceRequest
                {
                    Id = 1,
                    Description = "Repair water main break",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-10),
                    Priority = "High",
                    Status = "Closed",
                    Dependencies = new int[] { 2, 3, 4 }
                },
                new ServiceRequest
                {
                    Id = 2,
                    Description = "Assess water damage in surrounding areas",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-9),
                    Priority = "Medium",
                    Status = "In Progress",
                    Dependencies = new int[] { 5, 6, 7 }
                },
                new ServiceRequest
                {
                    Id = 3,
                    Description = "Coordinate with repair team for infrastructure check",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-8),
                    Priority = "Medium",
                    Status = "Open",
                    Dependencies = new int[] { 8, 9, 10 }
                },
                new ServiceRequest
                {
                    Id = 4,
                    Description = "Inspect utility poles after storm damage",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-7),
                    Priority = "High",
                    Status = "In Progress",
                    Dependencies = new int[] { 11, 12, 13 }
                },
                new ServiceRequest
                {
                    Id = 5,
                    Description = "Coordinate repair of sewer line",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-6),
                    Priority = "Medium",
                    Status = "Open",
                    Dependencies = new int[] { 14, 15, 16 }
                },
                new ServiceRequest
                {
                    Id = 6,
                    Description = "Fix pothole in Main St.",
                    Category = "Roadworks",
                    SubmissionDate = DateTime.Now.AddDays(-7),
                    Priority = "High",
                    Status = "Open",
                    Dependencies = new int[] { 17, 18 }
                },
                new ServiceRequest
                {
                    Id = 7,
                    Description = "Replace damaged streetlights in Park Ave.",
                    Category = "Roadworks",
                    SubmissionDate = DateTime.Now.AddDays(-6),
                    Priority = "Medium",
                    Status = "In Progress",
                    Dependencies = new int[] { 19, 20 }
                },
                new ServiceRequest
                {
                    Id = 8,
                    Description = "Install new traffic signals at junction",
                    Category = "Roadworks",
                    SubmissionDate = DateTime.Now.AddDays(-5),
                    Priority = "Low",
                    Status = "Open",
                    Dependencies = new int[] { 1, 2 }
                },
                new ServiceRequest
                {
                    Id = 9,
                    Description = "Repair park gazebo roof",
                    Category = "Maintenance",
                    SubmissionDate = DateTime.Now.AddDays(-4),
                    Priority = "High",
                    Status = "In Progress",
                    Dependencies = new int[] { 3, 4 }
                },
                new ServiceRequest
                {
                    Id = 10,
                    Description = "Upgrade park playground equipment",
                    Category = "Maintenance",
                    SubmissionDate = DateTime.Now.AddDays(-3),
                    Priority = "Medium",
                    Status = "Open",
                    Dependencies = new int[] { 5, 6 }
                },
                new ServiceRequest
                {
                    Id = 11,
                    Description = "Clear blocked drains",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-4),
                    Priority = "Medium",
                    Status = "In Progress",
                    Dependencies = new int[] { 7 }
                },
                new ServiceRequest
                {
                    Id = 12,
                    Description = "Fix broken sidewalk in front of library",
                    Category = "Roadworks",
                    SubmissionDate = DateTime.Now.AddDays(-3),
                    Priority = "Medium",
                    Status = "Open",
                    Dependencies = new int[] { 8 }
                },
                new ServiceRequest
                {
                    Id = 13,
                    Description = "Replace old water meters",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-2),
                    Priority = "High",
                    Status = "In Progress",
                    Dependencies = new int[] { 9 }
                },
                new ServiceRequest
                {
                    Id = 14,
                    Description = "Repair street drainage system",
                    Category = "Utilities",
                    SubmissionDate = DateTime.Now.AddDays(-1),
                    Priority = "Low",
                    Status = "Open",
                    Dependencies = new int[] { 10 }
                },
                new ServiceRequest
                {
                    Id = 15,
                    Description = "Trim overgrown hedges in public spaces",
                    Category = "Maintenance",
                    SubmissionDate = DateTime.Now.AddDays(-3),
                    Priority = "Low",
                    Status = "Open",
                    Dependencies = new int[] { 11 }
                },
                new ServiceRequest
                {
                    Id = 16,
                    Description = "Clear public park of fallen branches",
                    Category = "Maintenance",
                    SubmissionDate = DateTime.Now.AddDays(-15),
                    Priority = "Low",
                    Status = "Closed",
                    Dependencies = Array.Empty<int>()
                },
                new ServiceRequest
                {
                    Id = 17,
                    Description = "Repaint pedestrian crossings",
                    Category = "Roadworks",
                    SubmissionDate = DateTime.Now.AddDays(-14),
                    Priority = "Medium",
                    Status = "Open",
                    Dependencies = Array.Empty<int>()
                },
                new ServiceRequest
                {
                    Id = 18,
                    Description = "Replace traffic signs on Central Blvd.",
                    Category = "Roadworks",
                    SubmissionDate = DateTime.Now.AddDays(-13),
                    Priority = "High",
                    Status = "In Progress",
                    Dependencies = Array.Empty<int>()
                },
                new ServiceRequest
                {
                    Id = 19,
                    Description = "Fix broken bench in city park",
                    Category = "Maintenance",
                    SubmissionDate = DateTime.Now.AddDays(-12),
                    Priority = "Low",
                    Status = "Closed",
                    Dependencies = Array.Empty<int>()
                },
                new ServiceRequest
                {
                    Id = 20,
                    Description = "Clear fallen trees on Main St.",
                    Category = "Maintenance",
                    SubmissionDate = DateTime.Now.AddDays(-11),
                    Priority = "Medium",
                    Status = "Open",
                    Dependencies = Array.Empty<int>()
                }
            };

            //Adds requests to the dictionary and trees
            foreach (var request in requests)
            {
                ServiceRequests.Add(request.Id, request);
                requestBST.Insert(request);
                requestAVL.Insert(request);
                globalBinaryTree.Add(request);
            }

            // Add nodes and edges to the dependency graph
            foreach (var request in ServiceRequests.Values)
            {
                dependencyGraph.AddNode(request.Id);
                foreach (var dependency in request.Dependencies)
                {
                    dependencyGraph.AddEdge(request.Id, dependency);
                }
            }
        }

        /// <summary>
        /// This is the filter method that is called when the user types in the search bar,
        /// or when they select an option for ordering by using the drop down.
        /// </summary>
        /// <param name="orderBy"></param>
        public void ReorderServiceRequests(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return;

            switch (orderBy)
            {
                case "Request ID":
                    ReorderById();
                    break;
                case "Category":
                    ReorderByCategory();
                    break;
                case "Priority":
                    ReorderByPriority();
                    break;
                case "Status":
                    ReorderByStatus();
                    break;
            }

            OnPropertyChanged(nameof(ServiceRequests)); // Notify UI to update
        }
        /// <summary>
        /// Reorders the service requests by ID from 1-20
        /// </summary>
        private void ReorderById()
        {
            var orderedRequests = ServiceRequests.Values
                                                  .OrderBy(request => request.Id)
                                                  .ToDictionary(request => request.Id);
            ServiceRequests = new Dictionary<int, ServiceRequest>(orderedRequests);
            OnPropertyChanged(nameof(ServiceRequests));
            Console.WriteLine("Reordered by ID");
        }

        /// <summary>
        /// Groups the service requests by category
        /// </summary>
        private void ReorderByCategory()
        {
            var orderedRequests = ServiceRequests.Values.OrderBy(r => r.Category)
                                                        .ToDictionary(r => r.Id);
            ServiceRequests = orderedRequests;
        }
        /// <summary>
        /// Makes use of the AVL Tree to reorder the service requests by priority.
        /// 
        /// </summary>
        private void ReorderByPriority()
        {
            var orderedRequests = new Dictionary<int, ServiceRequest>();
            TraverseInOrder(requestAVL.Root, request => orderedRequests.Add(request.Id, request));
            ServiceRequests = orderedRequests;
        }

        /// <summary>
        /// Groups the service requests by status
        /// </summary>
        private void ReorderByStatus()
        {
            var orderedRequests = ServiceRequests.Values.OrderBy(r => r.Status)
                                                        .ToDictionary(r => r.Id);
            ServiceRequests = orderedRequests;
        }
        /// <summary>
        /// Traverse in order for the AVL Tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void TraverseInOrder(AVLNode<ServiceRequest> node, Action<ServiceRequest> action)
        {
            if (node == null) return;
            TraverseInOrder(node.Left, action);
            action(node.Data);
            TraverseInOrder(node.Right, action);
        }

        /// <summary>
        /// Traverse in order for the Binary Search Tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void TraverseInOrder(BSTNode<ServiceRequest> node, Action<ServiceRequest> action)
        {
            if (node == null) return;
            TraverseInOrder(node.Left, action);
            action(node.Data);
            TraverseInOrder(node.Right, action);
        }

        /// <summary>
        /// Makes use of a binary Search tree for quick look up of ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceRequest SearchById(int id)
        {
            var dummyRequest = new ServiceRequest { Id = id };
            ServiceRequest.CurrentComparisonMode = ComparisonMode.ById;
            var resultNode = requestBST.SearchRec(requestBST.Root, dummyRequest);
            return resultNode?.Data as ServiceRequest;
        }

        /// <summary>
        /// Get the Details of all of the dependencies of a given service request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<Dependency> GetDependencies(ServiceRequest request)
        {
            var dependencies = new List<Dependency>();
            foreach (var depId in request.Dependencies)
            {
                if (ServiceRequests.TryGetValue(depId, out var dependency))
                {
                    dependencies.Add(new Dependency
                    {
                        Id = dependency.Id,
                        Title = dependency.Description,
                        Status = dependency.Status
                    });
                }
            }
            return dependencies;
        }
        /// <summary>
        /// Getting the Descriptions(titles) of all the service requests, as well as all the statuses for generation of the graph view
        /// </summary>
        /// <returns></returns>
        public GraphViewModel GetGraphViewModel()
        {
            var serviceRequestTitles = ServiceRequests.ToDictionary(sr => sr.Key, sr => sr.Value.Description);
            var serviceRequestStatuses = ServiceRequests.ToDictionary(sr => sr.Key, sr => sr.Value.Status);
            return new GraphViewModel(dependencyGraph, serviceRequestTitles,serviceRequestStatuses);
        }


        /// <summary>
        /// For updating the UI
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================