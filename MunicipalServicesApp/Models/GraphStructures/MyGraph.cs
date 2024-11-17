//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Models.GraphStructures
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// Used to store each node and its neighbours for representation in the graph
    /// </summary>
    public class MyGraph
    {

        private readonly Dictionary<int, List<int>> _adjacencyList = new Dictionary<int, List<int>>();

        public Dictionary<int, List<int>> GetAdjacencyList()
        {
            return _adjacencyList;
        }

        public void AddNode(int node)
        {
            if (!_adjacencyList.ContainsKey(node))
                _adjacencyList[node] = new List<int>();
        }

        public void AddEdge(int from, int to)
        {
            if (_adjacencyList.ContainsKey(from) && !_adjacencyList[from].Contains(to))
                _adjacencyList[from].Add(to);
        }

        public List<int> GetNeighbors(int node)
        {
            return _adjacencyList.ContainsKey(node) ? _adjacencyList[node] : new List<int>();
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================
