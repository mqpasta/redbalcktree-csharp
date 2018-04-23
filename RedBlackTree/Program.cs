using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    public class RedBlackTree
    {
        private BNode root;

        public void Insert(int data)
        {
            BNode newnode = insertHelper(data, ref root, null);
            newnode = insertFixUp(newnode);
            root.color = Color.Black;
        }

        private BNode insertHelper(int data, ref BNode node, BNode parent)
        {
            if (node == null)
            {
                node = new BNode(data);
                node.parent = parent;
            }
            else if (data <= node.Data)
                return insertHelper(data, ref node.left, node);
            else
                return insertHelper(data, ref node.right, node);

            //node.parent = parent;

            return node;
        }

        public BNode insertFixUp(BNode node)
        {
            // check if there is a violoation
            if (IsRed(node) && (node.parent != null && IsRed(node.parent)))
            {
                BNode uncle = GetUncle(node);
                BNode grandParent = GetGrandParent(node);
                if(IsRed(uncle))
                {
                    node.parent.color = Color.Black;
                    uncle.color = Color.Black;
                    grandParent.color = Color.Red;
                    node = insertFixUp(grandParent);
                }

                else if(IsBlack(uncle) && IsRight(node) && IsLeft(node.parent))
                {
                    node = RotateLeft(node.parent);
                    node = RotateRight(node.parent);
                    node.left.color = Color.Red;
                    node.right.color = Color.Red;
                    node.color = Color.Black;
                }
                else if(IsBlack(uncle) && IsLeft(node) && IsLeft(node.parent))
                {
                    node = RotateRight(grandParent);
                    node.left.color = Color.Red;
                    node.right.color = Color.Red;
                    node.color = Color.Black;
                }
                else if(IsBlack(uncle) && IsLeft(node) && IsRight(node.parent))
                {
                    node = RotateRight(node.parent);
                    node = RotateLeft(node.parent);
                    node.left.color = Color.Red;
                    node.right.color = Color.Red;
                    node.color = Color.Black;
                }
                else if(IsBlack(uncle) && IsRight(node) && IsRight(node.parent))
                {
                    node = RotateLeft(grandParent);
                    node.left.color = Color.Red;
                    node.right.color = Color.Red;
                    node.color = Color.Black;
                }
            }

            if (node.parent == null)
                this.root = node;

            return node;
        }

        /// <summary>
        /// a temporary function to display a tree from specific node
        /// </summary>
        /// <param name="node"></param>
        public void Display(BNode node)
        {
            InOrderWalk(node);
        }

        public void Display()
        {
            InOrderWalk(root);
        }

        private void InOrderWalk(BNode node)
        {
            if (node == null)
                return;

            InOrderWalk(node.left);
            Console.Write("{0} ", node);
            InOrderWalk(node.right);
        }

        /// <summary>
        /// should be private / public only for testing purpose
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public BNode RotateLeft(BNode node)
        {
            if(node != null && node.right != null)
            {
                BNode x = node;
                BNode y = node.right;

                y.parent = x.parent;
                if (IsLeft(x))
                    SetLeft(x.parent, y);
                else
                    SetRight(x.parent, y);

                SetRight(x, y.left);
                SetLeft(y, x);
                x.parent = y;

                node = y;
            }

            return node;
        }

        /// <summary>
        /// should be private / public only for testing purpose
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public BNode RotateRight(BNode node)
        {
            if(node != null && node.left != null)
            {
                BNode y = node;
                BNode x = node.left;

                x.parent = y.parent;
                SetLeft(y, x.right);
                SetRight(x, y);
                y.parent = x;

                node = x;
            }

            return node;
        }

        private void SetLeft(BNode node, BNode left)
        {
            if (node != null)
            {
                node.left = left;
                if (left != null)
                    left.parent = node;
            }
        }

        private void SetRight(BNode node, BNode right)
        {
            if (node != null)
            {
                node.right = right;
                if (right != null)
                    right.parent = node;
            }
        }

        public BNode GetUncle(BNode node)
        {
            if (node.parent == null)
                return null;

            return GetSibling(node.parent);
        }

        public BNode GetSibling(BNode node)
        {
            if (node.parent == null)
                return null;

            if (IsLeft(node))
                return node.parent.right;
            else
                return node.parent.left;
        }

        public BNode GetGrandParent(BNode node)
        {
            if (node.parent != null && node.parent.parent != null)
                return node.parent.parent;

            return null;
        }

        private bool IsLeft(BNode node)
        {
            if (node.parent != null && node.parent.left == node)
                return true;

            return false;
        }

        private bool IsRight(BNode node)
        {
            if (node.parent != null && node.parent.right == node)
                return true;

            return false;
        }

        private bool IsRed(BNode node)
        {
            if (node == null)
                return false;

            if (node.color == Color.Red)
                return true;

            return false;
        }

        private bool IsBlack(BNode node)
        {
            if (IsRed(node))
                return false;

            return true;
        }

        public BNode Search(BNode node, int data)
        {
            if (node == null)
                return null;

            if (node.Data == data)
                return node;
            if (data < node.Data)
                return Search(node.left, data);
            else
                return Search(node.right, data);
        }

        public BNode GetRoot() { return root; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //testInsert();
            //testInsert2();
            testInsert3();
            //testRotateLeft();
            //testRotateRight();
            //testSibling();
            //testUncle();
            //testFixupCase1();
            //testFixUpCase2();
            //testFixUpCase3();
        }
        
        static void testInsert()
        {
            RedBlackTree rb = new RedBlackTree();
            Console.WriteLine("inserting 10");
            rb.Insert(10);
            rb.Display();
            Console.WriteLine("inserting 5");
            rb.Insert(5);
            rb.Display();
            Console.WriteLine("inserting 2");
            rb.Insert(2);
            rb.Display();
            Console.WriteLine("inserting 20");
            rb.Insert(20);
            rb.Display();
        }

        static void testInsert2()
        {
            RedBlackTree rb2 = new RedBlackTree();
            rb2.Insert(10);
            rb2.Insert(20);
            rb2.Insert(30);
            rb2.Insert(40);
            rb2.Insert(50);
            rb2.Insert(60);

            rb2.Display();
        }

        static void testInsert3()
        {
            RedBlackTree rb = new RedBlackTree();
            rb.Insert(10);
            rb.Insert(20);
            rb.Insert(30);
            rb.Insert(15);
            rb.Display();
        }

        static void testRotateLeft()
        {
            RedBlackTree rb = new RedBlackTree();
            rb.Insert(5);
            rb.Insert(10);

            BNode root = rb.RotateLeft(rb.GetRoot());
            rb.Display(root);
            Console.WriteLine("");

            BNode root2 = rb.RotateRight(root);
            rb.Display(root2);
        }

        static void testRotateRight()
        {
            RedBlackTree rb = new RedBlackTree();
            BNode n11 = new BNode(11);
            BNode n7 = new BNode(7);
            BNode n2 = new BNode(2);
            BNode n8 = new BNode(8);
            BNode n14 = new BNode(14);
            n11.left = n7;
            n11.right = n14;
            n7.parent = n11;
            n14.parent = n11;

            n7.left = n2;
            n7.right = n8;
            n2.parent = n7;
            n8.parent = n7;


            rb.Display(n11);
            Console.WriteLine();
            BNode rot = rb.RotateRight(n11);
            Console.Write("Left:{0} -- Node:{1} -- Right:{2}", rot.left, rot, rot.right);
            Console.WriteLine();
            rb.Display(rot);
        }

        static void testSibling()
        {
            RedBlackTree rb = new RedBlackTree();
            rb.Insert(5);
            rb.Insert(1);
            rb.Insert(10);

            rb.Display();
            BNode node1 = rb.Search(rb.GetRoot(), 5);
            Console.WriteLine(node1.Data);
            BNode sibling = rb.GetSibling(node1);
            Console.WriteLine("Sibling:{0}", (sibling != null) ? sibling.ToString() :"not found");

        }

        static void testUncle()
        {
            RedBlackTree rb = new RedBlackTree();
            rb.Insert(25);
            rb.Insert(10);
            rb.Insert(50);
            rb.Insert(8);
            rb.Insert(12);
            rb.Insert(35);
            rb.Insert(75);

            rb.Display();
            BNode n = rb.Search(rb.GetRoot(), 12);
            BNode uncle = rb.GetUncle(n);
            Console.WriteLine("Uncle:{0}", (uncle != null) ? uncle.ToString() : "not found");

            n = rb.Search(rb.GetRoot(), 8);
            uncle = rb.GetUncle(n);
            Console.WriteLine("Uncle:{0}", (uncle != null) ? uncle.ToString() : "not found");

            n = rb.Search(rb.GetRoot(), 35);
            uncle = rb.GetUncle(n);
            Console.WriteLine("Uncle:{0}", (uncle != null) ? uncle.ToString() : "not found");
        }

        static void testFixupCase1()
        {
            RedBlackTree rb = new RedBlackTree();
            rb.Insert(10);
            rb.Insert(5);
            rb.Insert(15);
            rb.Insert(3);

            BNode nodeToFix = rb.Search(rb.GetRoot(), 3);
            BNode fix = rb.insertFixUp(nodeToFix);

            rb.Display(fix);

        }

        static void testFixUpCase2()
        {
            RedBlackTree rb = new RedBlackTree();
            BNode root = new BNode(11, Color.Black);
            BNode n2 = new BNode(2, Color.Red);
            BNode n14 = new BNode(14, Color.Black);
            root.left = n2;
            root.right = n14;
            n14.parent = root;
            n2.parent = root;


            BNode n1 = new BNode(1, Color.Black );
            BNode n7 = new BNode(7, Color.Red);
            n2.left = n1;
            n2.right = n7;
            n1.parent = n2;
            n7.parent = n2;

            BNode n5 = new BNode(5, Color.Black);
            BNode n8 = new BNode(8, Color.Black);
            n7.left = n5;
            n7.right = n8;
            n5.parent = n7;
            n8.parent = n7;

            BNode n4 = new BNode(4, Color.Red);
            n5.left = n4;
            n4.parent = n5;

            new RedBlackTree().Display(root);

            Console.WriteLine("after fixing");
            BNode fix = rb.insertFixUp(n7);
            rb.Display(fix);

            //BNode fix = rb.RotateLeft(n7.parent);
            //Console.WriteLine("Left:{0} -- Node:{1} -- Right{2}", fix.left,fix,fix.right);
            //BNode fix2 = rb.RotateRight(fix.parent);
            //Console.WriteLine("Left:{0} -- Node:{1} -- Right{2}", fix2.left, fix2, fix2.right);
        }

        static void testFixUpCase3()
        {
            BNode b11 = new BNode(11, Color.Black);
            BNode b7 = new BNode(7, Color.Red);
            BNode b14 = new BNode(14, Color.Black);
            BNode b2 = new BNode(2, Color.Red);

            b11.left = b7;
            b11.right = b14;
            b7.left = b2;

            b2.parent = b7;
            b7.parent = b11;
            b14.parent = b11;

            BNode fix = new RedBlackTree().insertFixUp(b2);
            new RedBlackTree().Display(fix);

            

        }
    }
}
