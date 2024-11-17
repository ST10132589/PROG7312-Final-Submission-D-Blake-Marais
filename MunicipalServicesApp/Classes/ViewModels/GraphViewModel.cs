//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Models.GraphStructures;
using System.Collections.Generic;

namespace MunicipalServicesApp.Classes.ViewModels
{
    //==============================================================[START OF CLASS]============================================================== 
    public class GraphViewModel
    {
        /// <summary>
        /// Service request title and staus dictionaries for use on the graph view.
        /// </summary>
        private MyGraph _graph;
        private Dictionary<int, string> _serviceRequestTitles;
        private Dictionary<int, string> _serviceRequestStatuses;

        /// <summary>
        /// Constructor for the GraphViewModel class.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="serviceRequestTitles"></param>
        /// <param name="serviceRequestStatuses"></param>
        public GraphViewModel(MyGraph graph, Dictionary<int, string> serviceRequestTitles, Dictionary<int, string> serviceRequestStatuses)
        {
            _graph = graph;
            _serviceRequestTitles = serviceRequestTitles;
            _serviceRequestStatuses = serviceRequestStatuses;
        }

        /// <summary>
        /// Retrieves all nodes in the graph.
        /// </summary>
        public IEnumerable<int> GetNodes()
        {
            return _graph.GetAdjacencyList().Keys;
        }
        /// <summary>
        /// Fetches the MyGraph object.
        /// </summary>
        /// <returns></returns>
        public MyGraph GetGraph()
        {
            return _graph;
        }

        /// <summary>
        /// Retrieves all neighbors (dependencies) of a specific node.
        /// </summary>
        public IEnumerable<int> GetDependencies(int node)
        {
            if (_graph.GetAdjacencyList().TryGetValue(node, out var neighbors))
            {
                return neighbors;
            }

            return new List<int>(); // Returns an empty list if no dependencies exist
        }

        /// <summary>
        /// Retrieves the count of dependencies for a specific node.
        /// </summary>
        public int GetDependencyCount(int node)
        {
            if (_graph.GetAdjacencyList().TryGetValue(node, out var neighbors))
            {
                return neighbors.Count;
            }

            return 0; // Returns 0 if the node has no dependencies
        }

        /// <summary>
        /// Retrieves all edges in the graph as a list of tuples.
        /// </summary>
        public IEnumerable<(int, int)> GetEdges()
        {
            var edges = new List<(int, int)>();
            foreach (var kvp in _graph.GetAdjacencyList())
            {
                foreach (var neighbor in kvp.Value)
                {
                    edges.Add((kvp.Key, neighbor));
                }
            }
            return edges;
        }

        /// <summary>
        /// Checks if a node exists in the graph.
        /// </summary>
        public bool NodeExists(int node)
        {
            return _graph.GetAdjacencyList().ContainsKey(node);
        }

        /// <summary>
        /// Retrieves the title of the service request for a specific node.
        /// </summary>
        public string GetServiceRequestTitle(int node)
        {
            if (_serviceRequestTitles.TryGetValue(node, out var title))
            {
                return title;
            }

            return "Unknown"; // Return "Unknown" if the title is not found
        }

        /// <summary>
        /// Fetces the status of the service request for a specific node.
        /// </summary>
        /// <returns></returns>
        public string GetServiceRequestStatus(int node)
        {
            return _serviceRequestStatuses.TryGetValue(node, out var status) ? status : "Unknown"; //Returns "Unknown" if the status is not found
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================
