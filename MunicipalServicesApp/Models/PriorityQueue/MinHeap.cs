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
    /// Pretty much the same as Max Heao but this time it maintains the lowest element at the root.
    /// essentially just the opposite of max heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> _elements = new List<T>();

        public int Size => _elements.Count;

        public void Add(T item)
        {
            _elements.Add(item);
            HeapifyUp(Size - 1);
        }

        public T Remove()
        {
            if (Size == 0) throw new InvalidOperationException("Heap is empty.");

            var root = _elements[0];
            _elements[0] = _elements[Size - 1];
            _elements.RemoveAt(Size - 1);

            HeapifyDown(0);
            return root;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0 && _elements[index].CompareTo(_elements[Parent(index)]) < 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        private void HeapifyDown(int index)
        {
            int smallest = index;

            int left = LeftChild(index);
            if (left < Size && _elements[left].CompareTo(_elements[smallest]) < 0)
                smallest = left;

            int right = RightChild(index);
            if (right < Size && _elements[right].CompareTo(_elements[smallest]) < 0)
                smallest = right;

            if (smallest != index)
            {
                Swap(index, smallest);
                HeapifyDown(smallest);
            }
        }

        private void Swap(int i, int j)
        {
            (_elements[i], _elements[j]) = (_elements[j], _elements[i]);
        }

        private int Parent(int index) => (index - 1) / 2;
        private int LeftChild(int index) => 2 * index + 1;
        private int RightChild(int index) => 2 * index + 2;
    }
    //==============================================================[END OF CLASS]============================================================== 
}

