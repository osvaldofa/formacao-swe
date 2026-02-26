// Definição do nó da árvore
public class TreeNode
{
    public int Value;
    public TreeNode Left;
    public TreeNode Right;

    public TreeNode(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

// Funções auxiliares
public class BinaryTree
{
    public TreeNode Root;

    public void Insert(int value)
    {
        Root = InsertRec(Root, value);
    }

    private TreeNode InsertRec(TreeNode node, int value)
    {
        if (node == null)
            return new TreeNode(value);

        if (value < node.Value)
            node.Left = InsertRec(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRec(node.Right, value);

        return node;
    }

    public void PrintPreOrder(TreeNode node)
    {
        if (node == null) return;
        Console.Write(node.Value + " ");
        PrintPreOrder(node.Left);
        PrintPreOrder(node.Right);
    }

    public void PrintInOrder(TreeNode node)
    {
        if (node == null) return;
        PrintInOrder(node.Left);
        Console.Write(node.Value + " ");
        PrintInOrder(node.Right);
    }

    public void PrintPostOrder(TreeNode node)
    {
        if (node == null) return;
        PrintPostOrder(node.Left);
        PrintPostOrder(node.Right);
        Console.Write(node.Value + " ");
    }
}

// Demo
var tree = new BinaryTree();
tree.Insert(50);
tree.Insert(30);
tree.Insert(70);
tree.Insert(20);
tree.Insert(40);
tree.Insert(60);
tree.Insert(80);

Console.WriteLine("Pré-ordem:");
tree.PrintPreOrder(tree.Root);
Console.WriteLine();

Console.WriteLine("Em-ordem:");
tree.PrintInOrder(tree.Root);
Console.WriteLine();

Console.WriteLine("Pós-ordem:");
tree.PrintPostOrder(tree.Root);
Console.WriteLine();
