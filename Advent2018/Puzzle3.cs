using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle3 : PuzzleSolver
	{
		public Puzzle3() : base(3)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			Rectangle[] rectangles = ParseRectangles(lines, out int[,] grid);

			foreach (Rectangle rect in rectangles)
			{
				for (int y = rect.Y; y <= rect.Bottom; y++)
				{
					for (int x = rect.X; x <= rect.Right; x++)
					{
						grid[x, y]++;
					}
				}
			}

			return grid.Cast<int>().Count(v => v >= 2).ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			Rectangle[] rectangles = ParseRectangles(lines, out int[,] grid);

			bool[] idOverlapped = new bool[lines.Length];

			foreach (Rectangle rect in rectangles)
			{
				for (int y = rect.Y; y <= rect.Bottom; y++)
				{
					for (int x = rect.X; x <= rect.Right; x++)
					{
						int value = grid[x, y];
						int id = rect.Id;

						switch (value)
						{
							case 0: grid[x, y] = id; break;
							case -1: idOverlapped[id - 1] = true; break;
							default:
								idOverlapped[id - 1] = true;
								idOverlapped[value - 1] = true;
								grid[x, y] = -1;

								break;
						}
					}
				}
			}

			for (int i = 0; i < idOverlapped.Length; i++)
			{
				if (!idOverlapped[i])
				{
					return (i + 1).ToString();
				}
			}

			return null;
		}

		private Rectangle[] ParseRectangles(string[] lines, out int[,] grid)
		{
			Rectangle[] rectangles = new Rectangle[lines.Length];

			int gridWidth = 0;
			int gridHeight = 0;

			for (int i = 0; i < lines.Length; i++)
			{
				Rectangle rect = new Rectangle(lines[i]);
				rectangles[i] = rect;
				gridWidth = Math.Max(gridWidth, rect.Right + 1);
				gridHeight = Math.Max(gridHeight, rect.Bottom + 1);
			}

			grid = new int[gridWidth, gridHeight];

			return rectangles;
		}

		private class Rectangle
		{
			private int width;
			private int height;

			public Rectangle(string line)
			{
				int index1 = line.IndexOf('@') + 2;
				int index2 = line.IndexOf(',', index1 + 1);

				Id = int.Parse(line.Substring(1, index1 - 4));
				X = int.Parse(line.Substring(index1, index2 - index1));
				index1 = index2 + 1;
				index2 = line.IndexOf(':', index1 + 1);

				Y = int.Parse(line.Substring(index1, index2 - index1));
				index1 = index2 + 2;
				index2 = line.IndexOf('x', index1 + 1);

				width = int.Parse(line.Substring(index1, index2 - index1));
				height = int.Parse(line.Substring(index2 + 1));
			}

			public int Id { get; }
			public int X { get; }
			public int Y { get; }
			public int Right => X + width - 1;
			public int Bottom => Y + height - 1;
		}
	}
}
