using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle01 : PuzzleSolver
	{
		public Puzzle01() : base(1)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			int result = 0;

			foreach (string line in lines)
			{
				int value = int.Parse(line.Substring(1));

				if (line[0] == '+')
				{
					result += value;
				}
				else
				{
					result -= value;
				}
			}
			
			return result.ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			HashSet<int> resultSet = new HashSet<int>();

			int result = 0;
			int lineIndex = 0;

			while (true)
			{
				string line = lines[lineIndex];
				int value = int.Parse(line.Substring(1));

				if (line[0] == '+')
				{
					result += value;
				}
				else
				{
					result -= value;
				}

				if (resultSet.Contains(result))
				{
					return result.ToString();
				}

				resultSet.Add(result);
				lineIndex = ++lineIndex % lines.Length;
			}
		}
	}
}
