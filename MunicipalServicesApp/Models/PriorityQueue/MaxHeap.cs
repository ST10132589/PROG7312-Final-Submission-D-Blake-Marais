//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Models.PriorityQueue
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// This a generic implementation of a maxHeap data structure.
    /// It maintains the highest-priority element at the root and has heap operations
    /// such as addition, removal, and peeking at the root element.
    /// </summary>
    public class MaxHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// Internal list representation of the heap.
        /// </summary>
        private readonly List<T> _heap = new List<T>();

        /// <summary>
        /// Gets the current size of the heap.
        /// </summary>
        public int Size => _heap.Count;

        /// <summary>
        /// Adds a new element to the heap and maintains the heap property.
        /// </summary>
        public void Add(T item)
        {
            _heap.Add(item);
            HeapifyUp(Size - 1);
        }

        /// <summary>
        /// Removes and returns the root element (maximum) from the heap.
        /// </summary>
        /// <returns>The maximum element in the heap.</returns>
        public T Remove()
        {
            if (Size == 0) throw new InvalidOperationException("Heap is empty.");

            T root = _heap[0];
            _heap[0] = _heap[Size - 1];
            _heap.RemoveAt(Size - 1);
            HeapifyDown(0);

            return root;
        }

        /// <summary>
        /// Returns the root element (maximum) without removing it.
        /// </summary>
        /// <returns>The maximum element in the heap.</returns>
        public T Peek()
        {
            if (Size == 0) throw new InvalidOperationException("Heap is empty.");
            return _heap[0];
        }

        /// <summary>
        /// Restores the heap property by moving an element up the tree as needed.
        /// </summary>
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (_heap[index].CompareTo(_heap[parentIndex]) <= 0)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        /// <summary>
        /// Restores the heap property by moving an element down the tree as needed.
        /// </summary>
        private void HeapifyDown(int index)
        {
            while (index < Size)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int largestIndex = index;

                if (leftChildIndex < Size && _heap[leftChildIndex].CompareTo(_heap[largestIndex]) > 0)
                    largestIndex = leftChildIndex;

                if (rightChildIndex < Size && _heap[rightChildIndex].CompareTo(_heap[largestIndex]) > 0)
                    largestIndex = rightChildIndex;

                if (largestIndex == index)
                    break;

                Swap(index, largestIndex);
                index = largestIndex;
            }
        }

        /// <summary>
        /// Swaps two elements in the heap.
        /// </summary>
        private void Swap(int index1, int index2)
        {
            T temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================
