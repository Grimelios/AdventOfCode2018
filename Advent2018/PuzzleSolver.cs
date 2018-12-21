using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public abstract class PuzzleSolver
	{
		private int index;

		protected PuzzleSolver(int index)
		{
			this.index = index;
		}

		protected abstract string SolvePart1(string[] lines);
		protected abstract string SolvePart2(string[] lines);

		public string SolvePart1()
		{
			return SolvePart1(ReadInput());
		}

		public string SolvePart2()
		{
			return SolvePart2(ReadInput());
		}

		private string[] ReadInput()
		{
			return File.ReadAllLines($"Input/Input{index}.txt");
		}
	}
}
