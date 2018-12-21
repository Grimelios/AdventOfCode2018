using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle5 : PuzzleSolver
	{
		public Puzzle5() : base(5)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			var list = ToLinkedList(lines[0]);

			return CollapsePolymer(list).ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			int minLength = int.MaxValue;

			for (char c = 'a'; c <= 'z'; c++)
			{
				var list = ToLinkedList(lines[0]);
				var node = list.First;

				while (node != null)
				{
					if (Math.Abs(node.Value - c) % 32 == 0)
					{
						var toRemove = node;

						node = node.Next;
						list.Remove(toRemove);
					}
					else
					{
						node = node.Next;
					}
				}

				minLength = Math.Min(minLength, CollapsePolymer(list));
			}

			return minLength.ToString();
		}

		private int CollapsePolymer(LinkedList<char> list)
		{
			bool done;

			do
			{
				done = true;

				var node = list.First;
				var next = node.Next;

				while (next != null)
				{
					if (Math.Abs(node.Value - next.Value) == 32)
					{
						var toRemove = node;

						node = next.Next;
						list.Remove(toRemove);
						list.Remove(next);
						done = false;
					}
					else
					{
						node = next;
					}

					next = node?.Next;
				}
			}
			while (!done);

			return list.Count;
		}

		private LinkedList<char> ToLinkedList(string line)
		{
			LinkedList<char> list = new LinkedList<char>();

			foreach (char c in line)
			{
				list.AddLast(c);
			}

			return list;
		}
	}
}
