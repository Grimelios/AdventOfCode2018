using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle8 : PuzzleSolver
	{
		public Puzzle8() : base(8)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			var root = ParseRoot(lines);

			Stack<Node> stack = new Stack<Node>();
			stack.Push(root);

			int sum = 0;

			while (stack.Count > 0)
			{
				var node = stack.Pop();
				sum += node.Metadata.Sum();
				node.Children.ForEach(n => stack.Push(n));
			}

			return sum.ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			var root = ParseRoot(lines);

			return ComputeValue(root).ToString();
		}

		private int ComputeValue(Node node)
		{
			var children = node.Children;

			if (children.Count == 0)
			{
				return node.Metadata.Sum();
			}

			int sum = 0;

			foreach (int index in node.Metadata)
			{
				if (index > 0 && index <= children.Count)
				{
					sum += ComputeValue(children[index - 1]);
				}
			}

			return sum;
		}

		private Node ParseNode(string[] tokens, ref int index)
		{
			int childCount = int.Parse(tokens[index]);
			int metadataCount = int.Parse(tokens[index + 1]);

			index += 2;

			var node = new Node();

			for (int i = 0; i < childCount; i++)
			{
				node.Children.Add(ParseNode(tokens, ref index));
			}

			for (int i = 0; i < metadataCount; i++)
			{
				node.Metadata.Add(int.Parse(tokens[index]));
				index++;
			}

			return node;
		}

		private Node ParseRoot(string[] lines)
		{
			string[] tokens = lines[0].Split(' ');
			int index = 0;

			return ParseNode(tokens, ref index);
		}

		private class Node
		{
			public Node()
			{
				Children = new List<Node>();
				Metadata = new List<int>();
			}

			public List<Node> Children { get; }
			public List<int> Metadata { get; }
		}
	}
}
