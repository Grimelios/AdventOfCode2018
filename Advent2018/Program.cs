using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Program
	{
		private static PuzzleSolver[] solvers =
		{
			new Puzzle1(),
			new Puzzle2(),
			new Puzzle3(),
			new Puzzle4(),
			new Puzzle5(),
			new Puzzle6(),
			new Puzzle7(),
			new Puzzle8()
		};

		public static void Main(string[] args)
		{
			while (true)
			{
				Console.Write("Enter a puzzle (or blank to exit): ");

				string input = Console.ReadLine();

				if (input.Length == 0)
				{
					break;
				}

				// Specific portions of each puzzle can be specified with a period (e.g. puzzle 1.2).
				int dotIndex = input.IndexOf('.');
				int puzzle = 0;
				int part = -1;

				bool valid = true;

				if (dotIndex >= 0)
				{
					string s1 = input.Substring(0, dotIndex);
					string s2 = input.Substring(dotIndex + 1);

					if (!int.TryParse(s2, out part) || part < 1 || part > 2 || !int.TryParse(s1, out puzzle))
					{
						valid = false;
					}
				}
				else if (!int.TryParse(input, out puzzle))
				{
					valid = false;
				}

				if (puzzle < 1 || puzzle > 18)
				{
					valid = false;
				}

				if (!valid)
				{
					Console.WriteLine("Invalid\n");

					continue;
				}

				if (puzzle > solvers.Length || solvers[puzzle - 1] == null)
				{
					Console.WriteLine("Solver not found\n");

					continue;
				}

				if (part == -1 || part == 1)
				{
					Solve(puzzle, 1);
				}

				if (part == -1 || part == 2)
				{
					Solve(puzzle, 2);
				}

				Console.WriteLine();
			}
		}

		private static void Solve(int puzzle, int part)
		{
			string result;
			string p = $"The solution to puzzle {puzzle}.{part}";

			PuzzleSolver solver = solvers[puzzle - 1];

			try
			{
				result = part == 1 ? solver.SolvePart1() : solver.SolvePart2();
			}
			catch (NotImplementedException e)
			{
				Console.WriteLine(p + " has not been implemented");

				return;
			}

			if (result == null)
			{
				Console.WriteLine(p + " returned null");
			}
			else
			{
				Console.WriteLine(p + " is " + result);
			}
		}
	}
}
