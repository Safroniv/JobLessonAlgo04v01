


public class TreeNode
{
    public int Value { get; set; }
    public TreeNode? LeftChild { get; set; }
    public TreeNode? RightChild { get; set; }
    public TreeNode(int value)
    {
        Value = value;
    }
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
    public int DateCompare(int date)
    {
        return Value.CompareTo(date);
    }
}


