//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
//==============================================================[START OF CLASS]============================================================== 
/// <summary>
/// This a variation of a binary tree where the left child is less than the parent
/// and the right child is greater than the parent.
/// This allows for super quick searching and sorting of data. As the smaller or bigger element
/// is always in the same location
/// </summary>
public class BinarySearchTree<T> where T : IComparable<T>
{
    public BSTNode<T> Root { get; set; }

    public void Insert(T data)
    {
        Root = InsertRec(Root, data);
    }

    private BSTNode<T> InsertRec(BSTNode<T> node, T data)
    {
        if (node == null)
        {
            node = new BSTNode<T>(data);
            return node;
        }

        if (data.CompareTo(node.Data) < 0)
        {
            node.Left = InsertRec(node.Left, data);
        }
        else if (data.CompareTo(node.Data) > 0)
        {
            node.Right = InsertRec(node.Right, data);
        }

        return node;
    }
    
    public bool Search(T data)
    {
        return SearchRec(Root, data) != null;
    }
    /// <summary>
    /// recursive search which takes advantage of the binary tree structure
    /// </summary>
    public BSTNode<T> SearchRec(BSTNode<T> node, T data)
    {
        if (node == null || node.Data.CompareTo(data) == 0)
            return node;

        if (data.CompareTo(node.Data) < 0)
            return SearchRec(node.Left, data);

        return SearchRec(node.Right, data);
    }
}
//==============================================================[END OF CLASS]============================================================== 

//==============================================================[START OF CLASS]============================================================== 
public class BSTNode<T>
{
    public T Data { get; set; }
    public BSTNode<T> Left { get; set; }
    public BSTNode<T> Right { get; set; }

    public BSTNode(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}
    //==============================================================[END OF CLASS]============================================================== 
//==============================================================[END OF FILE]==============================================================
