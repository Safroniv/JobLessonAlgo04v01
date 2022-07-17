
Console.WriteLine("Hello, World!");

public class TreeNode
{
    public int Value { get; set; }
    public TreeNode? LeftChild { get; set; }
    public TreeNode? RightChild { get; set; }
    public override bool Equals(object obj)
    {
        var node = obj as TreeNode;
        if (node == null)
            return false;
        return node.Value == Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, LeftChild, RightChild);
    }
}
public interface ITree
{
    TreeNode GetRoot();
    void AddItem(int value); // добавить узел
    void RemoveItem(int value); // удалить узел по значению
    TreeNode GetNodeByValue(int value); //получить узел дерева по значению
    void PrintTree(); //вывести дерево в консоль
}
public static class TreeHelper
{
    public static NodeInfo[] GetTreeInLine(ITree tree)
    {
        var bufer = new Queue<NodeInfo>();
        var returnArray = new List<NodeInfo>();
        var root = new NodeInfo() { Node = tree.GetRoot() };
        bufer.Enqueue(root);
        while (bufer.Count != 0)
        {
            var element = bufer.Dequeue();
            returnArray.Add(element);
            var depth = element.Depth + 1;
            if (element.Node.LeftChild != null)
            {
                var left = new NodeInfo()
                {
                    Node = element.Node.LeftChild,
                    Depth = depth,
                };
                bufer.Enqueue(left);
            }
            if (element.Node.RightChild != null)
            {
                var right = new NodeInfo()
                {
                    Node = element.Node.RightChild,
                    Depth = depth,
                };
                bufer.Enqueue(right);
            }
        }
        return returnArray.ToArray();
    }
}
public class NodeInfo
{
    public int Depth { get; set; }
    public TreeNode? Node { get; set; }
}

public class Tree : ITree
{
    private TreeNode? _root;

    public TreeNode Root
    {
        get { return _root; }
        set { _root = value; }
    }
    
    public void AddItem(int value)
    {
        throw new NotImplementedException();
    }

    public TreeNode GetNodeByValue(int value)
    {
        throw new NotImplementedException();
    }

    public TreeNode GetRoot()
    {
        return Root;
    }

    public void PrintTree()
    {
        throw new NotImplementedException();
    }

    public void RemoveItem(int value)
    {
        throw new NotImplementedException();
    }
}