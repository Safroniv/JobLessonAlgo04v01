


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
        if (Root == null)
        {
            Root = new TreeNode(value)
            { Value= value, LeftChild = null, RightChild=null };
            return;
        }
        else
        {
            if (GetNodeByValue(value) == null)
            {
                AddNext (Root, value);
            }
        }
     
    }
    private void AddNext(TreeNode baseNode, int value)
    {
        if (baseNode.Value > value)
        {
            if (baseNode.RightChild == null)
            {
                baseNode.RightChild = new TreeNode(value);
            }
            else
            {
                AddNext(baseNode.RightChild, value);
            }
        }
        else
        {
            if (baseNode.LeftChild == null)
            {
                baseNode.LeftChild = new TreeNode(value);
            }
            else
            {
                AddNext(baseNode.LeftChild, value);
            }
        }
    }

    public TreeNode GetNodeByValue(int value)
    {
        var res = SearchByParent(value);
        return res.findNode;
    }

    public TreeNode GetRoot()
    {
        return Root;
    }

    public void PrintTree()
    {
        if (_root == null) return;
        string textFormat = "(0)";
        int tabs = 1;
        int headSpace = 2;
        int leftSpace = 2;
        int rootTop = Console.CursorTop + headSpace;
        var information = new List<informationText>();
        var nextNode = _root;
        for (int level = 0; nextNode != null; level++)
        {
            var textBlockInfo = new informationText { Node = nextNode, Text = nextNode.Value.ToString(textFormat) };
            if (level < information.Count)
            {
                textBlockInfo.Cursor = information[level].EndPos + tabs;
                information[level] = textBlockInfo;
            }
            else
            {
                textBlockInfo.Cursor = leftSpace;
                information.Add(textBlockInfo);
            }
            if (level > 0)
            {
                textBlockInfo.parentNode = information[level - 1];
                if (nextNode == textBlockInfo.parentNode.Node.LeftChild)
                {
                    textBlockInfo.parentNode.leftNode = textBlockInfo;
                    textBlockInfo.EndPos = Math.Max(textBlockInfo.EndPos, textBlockInfo.parentNode.Cursor - 1);
                }
                else
                {
                    textBlockInfo.parentNode.rightNode = textBlockInfo;
                    textBlockInfo.Cursor = Math.Max(textBlockInfo.Cursor, textBlockInfo.parentNode.EndPos + 1);
                }
            }
            nextNode = nextNode.LeftChild ?? nextNode.RightChild;
            for (; nextNode == null; textBlockInfo = textBlockInfo.parentNode)
            {
                int top = rootTop + 2 * level;
                Visialisation(textBlockInfo.Text, top, textBlockInfo.Cursor);
                if (textBlockInfo.leftNode != null)
                {
                    Visialisation("/", top + 1, textBlockInfo.leftNode.EndPos);
                    Visialisation("_", top, textBlockInfo.leftNode.EndPos + 1, textBlockInfo.Cursor);
                }
                if (textBlockInfo.rightNode != null)
                {
                    Visialisation("_", top, textBlockInfo.EndPos, textBlockInfo.rightNode.Cursor - 1);
                    Visialisation("\\", top + 1, textBlockInfo.rightNode.Cursor - 1);
                }
                if (--level < 0) break;
                if (textBlockInfo == textBlockInfo.parentNode.leftNode)
                {
                    textBlockInfo.parentNode.Cursor = textBlockInfo.EndPos + 1;
                    nextNode = textBlockInfo.parentNode.Node.RightChild;
                }
                else
                {
                    if (textBlockInfo.parentNode.leftNode == null)
                        textBlockInfo.parentNode.EndPos = textBlockInfo.Cursor - 1;
                    else
                        textBlockInfo.parentNode.Cursor += (textBlockInfo.Cursor - 1 - textBlockInfo.parentNode.EndPos) / 2;
                }
            }
        }
        Console.SetCursorPosition(0, rootTop + 2 * information.Count - 1);
    }
    private void Visialisation(string date, int main, int leftNode, int rightNode = -1)
    {
        Console.SetCursorPosition(leftNode, main);
        if (rightNode < 0)
        {
            rightNode = leftNode + date.Length;
        }
        while (Console.CursorLeft < rightNode)
        {
            Console.Write(date);
        }
    }
    class informationText
    {
        public TreeNode Node;
        public string Text;
        public int Cursor;
        public int textLenght { get { return Text.Length; } }
        public int EndPos { get { return Cursor + textLenght; } set { Cursor = value - textLenght; } }
        public informationText parentNode, leftNode, rightNode;
    }
    public void RemoveItem(int value)
    {
        var resultDate = SearchByParent(value);
        if (resultDate.findNode == null)
            return;
        RemoveItem(resultDate.findNode, resultDate.parent);
    }
    private void RemoveItem(TreeNode node, TreeNode parent)
    {
        if (node.LeftChild == null && node.RightChild == null)
        {
            if (ReferenceEquals(parent.LeftChild, node))
                parent.LeftChild = null;
            else
                parent.RightChild = null;
            return;
        }
        if (node.LeftChild == null || node.RightChild == null)
        {
            if (ReferenceEquals(parent.LeftChild, node))
                parent.LeftChild = node.LeftChild ?? node.RightChild;
            else
                parent.RightChild = node.LeftChild ?? node.RightChild;
            return;
        }

        var maxNode = node.LeftChild;
        var p = node;
        while (maxNode.RightChild != null)
        {
            p = maxNode;
            maxNode = maxNode.RightChild;
        }
        RemoveItem(maxNode, p);
    }

    private (TreeNode findNode, TreeNode parent) SearchByParent(int value)
    {
        if (Root == null)
            return (null, null);
        if (Root.Value == value)
            return (Root, null);
        return RecurseveSearch(Root, value);
    }

    private (TreeNode findNode, TreeNode parent) RecurseveSearch(TreeNode parent, int value)
    {
        if (parent == null)
            return (null, null);
        if (parent.LeftChild?.Value == value)
            return (parent.LeftChild, parent);
        if (parent.RightChild?.Value == value)
            return (parent.RightChild, parent);
        var left = RecurseveSearch(parent.LeftChild, value);
        if (left.findNode != null)
            return left;
        return RecurseveSearch(parent.RightChild, value);
    }
}


