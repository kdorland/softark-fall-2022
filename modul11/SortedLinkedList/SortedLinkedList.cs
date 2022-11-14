namespace LinkedList
{
    class Node
    {
        public Node(User data, Node next) {
            this.Data = data;
            this.Next = next;
        }
        public User Data;
        public Node Next;
    }

    class SortedLinkedList
    {
        private Node first = null!;

        public void Add(User user)
        {
            // TODO: Implement this method
        }

        public User RemoveFirst()
        {
            if (first == null)
            {
                throw new InvalidOperationException();
            }
            User result = first.Data;
            first = first.Next;
            return result;
        }

        public void RemoveUser(User user)
        {
            Node node = first;
            Node previous = null!;
            bool found = false;
            
            while (!found && node != null)
            {
                if (node.Data.Name == user.Name)
                {
                    found = true;
                    if (node == first)
                    {
                        RemoveFirst();
                    } else {
                        previous.Next = node.Next;
                    }
                } else {
                    previous = node;
                    node = node.Next;
                }
            }
        }

        public User GetFirst() {
            return first.Data;
        }

        public User GetLast() {
            Node node = first;
            while (node.Next != null)
            {
                node = node.Next;
            }
            return node.Data;
        }

        public int CountUsers()
        {
            Node node = first;
            int count = 0;
            while (node != null)
            {
                count++;
                node = node.Next;
            }
            return count;
        }

        public bool Contains(User user) {
            Node node = first;
            while (node != null)
            {
                if (node.Data.Name == user.Name) {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public override String ToString() {
            Node node = first;
            String result = "";
            while (node != null) 
            {
                result += node.Data.Name + ", ";
                node = node.Next;
            }
            return result.Trim();
        }
    }
}