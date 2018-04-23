using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    public enum Color { Red, Black};
    public class BNode
    {
        public int Data;
        public BNode parent;
        public BNode left;
        public BNode right;
        public Color color;

        public BNode()
        {
            Initialize();
        }

        public BNode (int data, Color c)
        {
            this.Data = data;
            this.color = c;
        }
        public BNode(int data)
        {
            Initialize();
            this.Data = data;
        }

        private void Initialize()
        {
            parent = null;
            left = null;
            right = null;
            color = Color.Red;
        }

        public override string ToString()
        {
            return string.Format("[{0}-{1}-P:{2}]", Data, color.ToString(), 
                parent != null? parent.Data: -1);
        }
    }
}
