using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle7 : PuzzleSolver
	{
		public Puzzle7() :base(7)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			Step[] steps = ParseSteps(lines);

			SortedLinkedList<Step> available = new SortedLinkedList<Step>();
			available.Add(steps.Where(s => s.Requirements.Count == 0).ToList());

			StringBuilder result = new StringBuilder();

			var list = available.List;

			while (list.Count > 0)
			{
				LinkedListNode<Step> node = list.First;

				do
				{
					Step step = node.Value;

					if (step.Requirements.All(r => r.Complete))
					{
						result.Append(step.Label);
						step.Complete = true;
						list.Remove(node);

						List<Step> toAdd = step.After.Where(s => !s.Complete && !list.Contains(s)).ToList();
						available.Add(toAdd);

						break;
					}

					node = node.Next;
				}
				while (true);
			}

			return result.ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			Step[] steps = ParseSteps(lines);
			SortedLinkedList<Step> available = new SortedLinkedList<Step>();
			available.Add(steps.Where(s => s.Requirements.Count == 0).ToList());

			StringBuilder builder = new StringBuilder();

			var mainList = available.List;

			Step[] workers = new Step[5];

			int totalTime = 0;

			while (mainList.Count > 0 || workers.Any(s => s != null))
			{
				for (int i = 0; i < workers.Length; i++)
				{
					if (mainList.Count == 0)
					{
						break;
					}

					if (workers[i] == null)
					{
						Step step = mainList.FirstOrDefault(s => s.Requirements.All(r => r.Complete));

						if (step == null)
						{
							break;
						}

						workers[i] = step;
						mainList.Remove(step);
					}
				}

				for (int i = 0; i < workers.Length; i++)
				{
					Step step = workers[i];

					if (step != null)
					{
						step.TimeRemaining--;

						if (step.TimeRemaining == 0)
						{
							step.Complete = true;
							builder.Append(step.Label);
							workers[i] = null;

							List<Step> toAdd = step.After.Where(s => !s.Complete && !mainList.Contains(s)).ToList();
							available.Add(toAdd);
						}
					}
				}

				totalTime++;
			}

			return totalTime.ToString();
		}

		private Step[] ParseSteps(string[] lines)
		{
			Tuple<int, int>[] tuples = new Tuple<int, int>[lines.Length];

			int nodeCount = -1;

			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];

				int step = line[5] - 'A';
				int before = line[36] - 'A';

				tuples[i] = new Tuple<int, int>(step, before);
				nodeCount = Math.Max(nodeCount, step + 1);
				nodeCount = Math.Max(nodeCount, before + 1);
			}

			Step[] steps = new Step[nodeCount];

			for (int i = 0; i < steps.Length; i++)
			{
				steps[i] = new Step((char)('A' + i));
			}

			foreach (var tuple in tuples)
			{
				Step step = steps[tuple.Item1];
				Step before = steps[tuple.Item2];

				step.After.Add(before);
				before.Requirements.Add(step);
			}

			return steps;
		}

		[DebuggerDisplay("{" + nameof(Label) + "}")]
		private class Step : IComparable<Step>
		{
			public Step(char label)
			{
				Label = label;
				After = new List<Step>();
				Requirements = new List<Step>();
				TimeRemaining = label - 'A' + 61;
			}

			public char Label { get; }
			public bool Complete { get; set; }
			public int TimeRemaining { get; set; }

			public List<Step> After { get; }
			public List<Step> Requirements { get; }

			public int CompareTo(Step other)
			{
				return Label.CompareTo(other.Label);
			}

			public override bool Equals(object obj)
			{
				return Label == ((Step)obj).Label;
			}
		}

		private class SortedLinkedList<T> where T : IComparable<T>
		{
			public SortedLinkedList()
			{
				List = new LinkedList<T>();
			}

			public LinkedList<T> List { get; }

			public void Add(List<T> items)
			{
				if (items.Count == 0)
				{
					return;
				}

				items.Sort();

				if (List.Count == 0)
				{
					items.ForEach(s => List.AddLast(s));

					return;
				}

				var current = List.First;

				foreach (T item in items)
				{
					LinkedListNode<T> newNode = new LinkedListNode<T>(item);

					while (current != null)
					{
						if (current.Value.CompareTo(item) > 0)
						{
							List.AddBefore(current, newNode);

							break;
						}

						current = current.Next;
					}

					if (current == null)
					{
						List.AddLast(newNode);
					}
				}
			}
		}
	}
}
