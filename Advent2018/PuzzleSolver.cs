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
		private string path;

		protected PuzzleSolver(int index)
		{
			string n = index / 10 > 0 ? index.ToString() : "0" + index;

			path = $"Input/Input{n}.txt";
		}

		public bool InputFound => File.Exists(path);

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
			return File.ReadAllLines(path);
		}
	}
}
