using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle09 : PuzzleSolver
	{
		public Puzzle09() : base(9)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			ParseInput(lines, out int playerCount, out int marbleCount);

			return Solve(playerCount, marbleCount);
		}

		protected override string SolvePart2(string[] lines)
		{
			ParseInput(lines, out int playerCount, out int marbleCount);

			return Solve(playerCount, marbleCount * 100);
		}

		private void ParseInput(string[] lines, out int playerCount, out int marbleCount)
		{
			string line = lines[0];

			int start = line.IndexOf('w') + 6;
			int length = line.IndexOf(' ', start) - start;

			playerCount = int.Parse(line.Substring(0, line.IndexOf(' ')));
			marbleCount = int.Parse(line.Substring(start, length)) + 1;
		}

		private string Solve(int playerCount, int marbleCount)
		{
			CircularLinkedList<long> marbles = new CircularLinkedList<long>();
			marbles.Append(0);

			var node = marbles.Head;

			long[] scores = new long[playerCount];
			int currentPlayer = 1;

			for (int i = 1; i < marbleCount; i++)
			{
				if (i % 23 == 0)
				{
					scores[currentPlayer] += i;

					for (int j = 0; j < 7; j++)
					{
						node = node.Previous;
					}

					scores[currentPlayer] += node.Data;

					var toRemove = node;

					marbles.Remove(toRemove);
					node = node.Next;
				}
				else
				{
					node = marbles.Insert(i, node.Next);
				}

				currentPlayer = ++currentPlayer % playerCount;
			}

			return scores.Max().ToString();
		}

		private class CircularLinkedList<T>
		{
			public CircularLinkedListNode<T> Head { get; private set; }
			public CircularLinkedListNode<T> Tail { get; private set; }

			public int Count { get; private set; }

			public CircularLinkedListNode<T> Append(T data)
			{
				var node = new CircularLinkedListNode<T>(data);

				if (Count == 0)
				{
					Head = node;
					Tail = node;
					node.Next = node;
					node.Previous = node;
				}
				else
				{
					Tail.Next = node;
					node.Previous = Tail;
					Tail = node;
					Tail.Next = Head;
					Head.Previous = Tail;
				}

				Count++;

				return Tail;
			}

			public CircularLinkedListNode<T> Insert(T data, CircularLinkedListNode<T> after)
			{
				var node = new CircularLinkedListNode<T>(data)
				{
					Previous = after,
					Next = after.Next
				};

				after.Next.Previous = node;
				after.Next = node;
				Count++;

				return node;
			}

			public void Remove(CircularLinkedListNode<T> node)
			{
				if (Count == 1)
				{
					Head = null;
					Tail = null;
				}
				else
				{
					node.Previous.Next = node.Next;
					node.Next.Previous = node.Previous;
				}

				Count--;
			}
		}

		private class CircularLinkedListNode<T>
		{
			public CircularLinkedListNode(T data)
			{
				Data = data;
			}

			public T Data { get; }
			public CircularLinkedListNode<T> Next { get; set; }
			public CircularLinkedListNode<T> Previous { get; set; }
		}
	}
}
