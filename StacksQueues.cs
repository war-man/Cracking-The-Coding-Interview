﻿using System;
using System.Collections.Generic;
namespace CrackingTheCodingInterview
{
	public class StackQueues
	{
		public static void Run()
		{
			//Q3_4();
			//Q3_6();
			Q3_7();
		}

		//Describe how you could use a single array to implement 3 stacks
		public static void Q3_1(int size1, int size2, int size3)
		{
			//int[] stack = new int[size1 + size2 + size3];

			//int[] head1 = 0, 
			//    head2 = size1-1, 
			//    head3 = size1 + size2 - 1;

			//push(int stackNum, item)
			//int head = heads[stackNum-1];
			//arr[head+1] = item;
			//heads[stackNum-1] = head + 1;

		}

		//design a stack with a function min() that finds
		//the minimum element in O(1) time
		public static void Q3_2()
		{
			//brainstorm
			//have another property in the class for min
			//when pushing, see if new item is less than the min
			//when popping, have to find the new min, which isn't O(1)
			//could use a min heap to find which is O(1), but inserts
			//are O(logn)

			//have another stack for the min
			//when pushing, see if new item is less than peek()
			//if so also push to the second stack
			//
			//min is just peek on the second stack
			//
			//when popping, peek on second stack to see if equal
			//if so also remove here

			//other solution which wastes more space
			//is for each node to keep track of the min below it


		}

		//implement a set of stacks. when first stack gets too tall/full
		//push onto another stack instead
		//push and pop interfaces should behave the same
		public static void Q3_3()
		{
			//keep a list of stacks
			//Push() if last one is full, create a new stack and push there
			//Pop() pop from last stack, if now empty, get rid of stack

			//popAt()
			//have to create another method to shift items from stack on the right
			//over to the left
		}

		//solve towers of hanoi
		public static void Q3_4()
		{
			var source = new MyStack<int>(3, 2, 1); //largest on bottom
			var aux = new MyStack<int>();
			var target = new MyStack<int>();

			move(3, source, target, aux);
		}

		//move disks from Towers of Hanoi
		private static void move(int n, MyStack<int> source,
								 MyStack<int> target, MyStack<int> aux)
		{
			if (n > 0)
			{
				//move n-1 disks from source to aux, so they are out of the way
				move(n - 1, source, aux, target);

				//move the nth disk from source to target;
				target.Push(source.Pop());

				//print progress
				Console.Write("Source : ");
				source.Print();
				Console.Write("Aux : ");
				aux.Print();
				Console.Write("Target : ");
				target.Print();
				Console.WriteLine();

				//move n-1 disks we left on aux onto target
				move(n - 1, aux, target, source);
			}
		}

		public static void Q3_5()
		{
			//HackerRank.TwoStackQueue();
		}

		public static void Q3_6()
		{
			//sort a stack in ascending order, with biggest items on top
			//can use additional stack but not other data structures
			//can't use count, just isEmpty()
			var s = new Stack<int>(); s.Push(3); s.Push(6); s.Push(5); s.Push(4); s.Push(1); s.Push(2);
			var minStack = new Stack<int>(); //smaller numbers on bottom
			var maxStack = new Stack<int>(); //bigger numbers are on bottom 
											 /* Don't really need this maxStack, can just push back onto the original stack*/

			minStack.Push(s.Pop());
			while (s.Count > 0)
			{
				var next = s.Peek();
				if (next >= minStack.Peek()) //next item is in ascending order
				{
					minStack.Push(s.Pop()); //push onto minstack
				}
				else {
					//empty min stack and put onto max stack
					while (minStack.Count > 0 && minStack.Peek() > next)
					{
						maxStack.Push(minStack.Pop());
					}

					//push next
					minStack.Push(s.Pop());

					//transfer from max stack onto min until we get to value of next
					while (maxStack.Count > 0 && maxStack.Peek() > next)
					{
						minStack.Push(maxStack.Pop());
					}

				}
			}
			//transfer anything left on maxstack
			while (maxStack.Count > 0)
			{
				minStack.Push(maxStack.Pop());
			}

			//print results
			while (minStack.Count > 0)
			{
				Console.Write(minStack.Pop() + " ");
			}

		}

		public static void Q3_7()
		{
			//enqueue, dequeueAny, dequeueDog, dequeueCat
			var shelter = new Shelter();
			shelter.Enqueue(new Animal("dog1", Species.Dog));
			shelter.Enqueue(new Animal("dog2", Species.Dog));
			shelter.Enqueue(new Animal("cat1", Species.Cat));
			shelter.Enqueue(new Animal("dog3", Species.Dog));
			shelter.Enqueue(new Animal("cat2", 0));

			Console.WriteLine(shelter.DequeueCat().ToString());
			Console.WriteLine(shelter.DequeueAny().ToString());
			Console.WriteLine(shelter.DequeueAny().ToString());
			Console.WriteLine(shelter.DequeueAny().ToString());
			Console.WriteLine(shelter.DequeueAny().ToString());
		}

	}

	public enum Species { Cat, Dog };
	internal class Animal
	{
		public Species species { get; private set; }
		public string name { get; set; }
		public Animal(string name, Species species)
		{
			this.name = name;
			this.species = species;
		}
		public override string ToString()
		{
			return name + " " + species;
		}

	}
	internal class Shelter
	{
		Queue<Animal> cats = new Queue<Animal>();
		Queue<Animal> dogs = new Queue<Animal>();
		Queue<Animal> all = new Queue<Animal>();

		public void Enqueue(Animal a)
		{
			all.Enqueue(a);
			if (a.species == 0)
			{
				cats.Enqueue(a);
			}
			else {
				dogs.Enqueue(a);
			}
		}

		public Animal DequeueAny()
		{
			var animal = all.Dequeue();
			while ((dogs.Count == 0 || animal.name != dogs.Peek().name) && 
			       (cats.Count == 0 || animal.name != cats.Peek().name))
			{
				animal = all.Dequeue();
			}

			if (animal.species == Species.Cat)
			{
				cats.Dequeue();
			}
			else {
				dogs.Dequeue();
			}

			return animal;
		}

		public Animal DequeueCat()
		{
			return cats.Dequeue();
		}

		public Animal DequeueDog()
		{
			return dogs.Dequeue();
		}
	}


	public class MyStack<T>
	{
		private Node<T> top;

		public MyStack(params T[] values)
		{
			foreach (T o in values)
			{
				this.Push(o);
			}
		}

		public T Pop()
		{
			if (top != null)
			{
				T data = top.data;
				top = top.next;
				return data;
			}
			return default(T);
		}

		public T Peek()
		{
			return top.data;
		}

		public void Push(T data)
		{
			Node<T> t = new Node<T>(data);
			t.next = top;
			top = t;
		}

		public bool IsEmpty()
		{
			return top == null;
		}

		public void Print()
		{
			if (top != null)
			{
				top.Print();
			}
			else {
				Console.WriteLine("<empty>");
			}
		}
	}
}