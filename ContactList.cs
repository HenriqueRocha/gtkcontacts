using System;
using System.Collections;

public class ContactList
{
	private Node head;

	public void Add(Contact contact)
	{
		if (head == null)
		{
			head = new Node(contact, null);
		}
		else
		{
			Node n = head;
			while (n.Next != null)
			{
				n = n.Next;
			}
			n.Next = new Node(contact, null);
		}
	}

	public Contact Get(int index)
	{
		Node n = head;
		for (int i = 0; i < index; i++)
		{
			n = n.Next;
		}
		return n.Item;
	}

	public int Size()
	{
		int size = 0;
		Node n = head;
		while (n != null)
		{
			size++;
			n = n.Next;
		}
		return size;
	}

	public bool IsEmpty()
	{
		return head == null;
	}

	private class Node
	{
		private Contact item;
		private Node next;

		public Node(Contact item, Node next)
		{
			this.item = item;
			this.next = next;
		}

		public Contact Item
		{
			get { return item; }
		}

		public Node Next
		{
			get { return next; }
			set { next = value; }
		}
	}
}


