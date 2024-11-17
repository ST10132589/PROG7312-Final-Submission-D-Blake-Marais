//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
//==============================================================[START OF CLASS]============================================================== 
/// <summary>
/// Each node (parent) has two children, left and right.
/// Great for sorted data retrieval like in ServiceRequestViewModel.
/// </summary>
public class BinaryTree<T> where T : IComparable<T>
{
    public BinaryTreeNode<T> Root { get; set; }

    public void Add(T data)
    {
        if (Root == null)
        {
            Root = new BinaryTreeNode<T>(data);
        }
        else
        {
            AddTo(Root, data);
        }
    }

    private void AddTo(BinaryTreeNode<T> node, T data)
    {
        if (data.CompareTo(node.Data) < 0)
        {
            if (node.Left == null)
            {
                node.Left = new BinaryTreeNode<T>(data);
            }
            else
            {
                AddTo(node.Left, data);
            }
        }
        else
        {
            if (node.Right == null)
            {
                node.Right = new BinaryTreeNode<T>(data);
            }
            else
            {
                AddTo(node.Right, data);
            }
        }
    }

    public void TraverseInOrder(Action<T> visit)
    {
        TraverseInOrder(Root, visit);
    }

    private void TraverseInOrder(BinaryTreeNode<T> node, Action<T> visit)
    {
        if (node == null) return;

        TraverseInOrder(node.Left, visit);
        visit(node.Data);
        TraverseInOrder(node.Right, visit);
    }
}
//==============================================================[END OF CLASS]============================================================== 

//==============================================================[START OF CLASS]============================================================== 
public class BinaryTreeNode<T>
{
    public T Data { get; set; }
    public BinaryTreeNode<T> Left { get; set; }
    public BinaryTreeNode<T> Right { get; set; }

    public BinaryTreeNode(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}
//==============================================================[END OF CLASS]============================================================== 
//==============================================================[END OF FILE]==============================================================

