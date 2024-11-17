//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Models.TreeStructures
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// An AVL tree is a self-balancing BST which meakes sure the height of the left and right subtrees
    /// are as close as possible. This is done by rotating the nodes in the tree.
    /// </summary>
    public class AVLNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public AVLNode<T> Left { get; set; }
        public AVLNode<T> Right { get; set; }
        public int Height { get; set; } = 1;
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        public AVLNode<T> Root { get; private set; }

        public void Insert(T data)
        {
            Root = Insert(Root, data);
        }

        private AVLNode<T> Insert(AVLNode<T> node, T data)
        {
            if (node == null) return new AVLNode<T> { Data = data };

            if (data.CompareTo(node.Data) < 0)
                node.Left = Insert(node.Left, data);
            else if (data.CompareTo(node.Data) > 0)
                node.Right = Insert(node.Right, data);

            return Balance(node);
        }

        private AVLNode<T> Balance(AVLNode<T> node)
        {
            UpdateHeight(node);

            int balanceFactor = GetBalanceFactor(node);
            if (balanceFactor > 1)
            {
                if (GetBalanceFactor(node.Left) < 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balanceFactor < -1)
            {
                if (GetBalanceFactor(node.Right) > 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        private AVLNode<T> RotateRight(AVLNode<T> y)
        {
            AVLNode<T> x = y.Left;
            AVLNode<T> T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        private AVLNode<T> RotateLeft(AVLNode<T> x)
        {
            AVLNode<T> y = x.Right;
            AVLNode<T> T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        private void UpdateHeight(AVLNode<T> node)
        {
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        private int GetHeight(AVLNode<T> node) => node?.Height ?? 0;

        private int GetBalanceFactor(AVLNode<T> node) => GetHeight(node.Left) - GetHeight(node.Right);
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================
