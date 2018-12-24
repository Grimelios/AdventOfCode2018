using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle06 : PuzzleSolver
	{
		public Puzzle06() : base(6)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			Point[] points = ParsePoints(lines, out int gridWidth, out int gridHeight);

			int[] areaArray = new int[points.Length];

			for (int i = 0; i < gridHeight; i++)
			{
				for (int j = 0; j < gridWidth; j++)
				{
					Point p = new Point(j, i);

					int minDistance = int.MaxValue;
					int minIndex = -1;

					for (int k = 0; k < points.Length; k++)
					{
						int distance = points[k].ComputeManhattanDistance(p);

						if (distance < minDistance)
						{
							minDistance = distance;

							if (minIndex != -1)
							{
								areaArray[minIndex]--;
							}

							minIndex = k;
							areaArray[k]++;
						}
						else if (distance == minDistance && minIndex != -1)
						{
							areaArray[minIndex]--;
							minIndex = -1;
						}
					}
				}
			}

			List<Point> edgePoints = new List<Point>();

			for (int i = 0; i < gridWidth; i++)
			{
				edgePoints.Add(new Point(i, 0));
				edgePoints.Add(new Point(i, gridHeight - 1));
			}

			for (int i = 1; i <= gridHeight - 2; i++)
			{
				edgePoints.Add(new Point(0, i));
				edgePoints.Add(new Point(gridWidth - 1, i));
			}

			int maxArea = -1;

			for (int i = 0; i < points.Length; i++)
			{
				int area = areaArray[i];

				if (area > maxArea)
				{
					Point p = points[i];

					if (IsAreaInfinite(p, i, points, edgePoints))
					{
						continue;
					}

					maxArea = area;
				}
			}

			return maxArea.ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			Point[] points = ParsePoints(lines, out int gridWidth, out int gridHeight);

			int area = 0;

			for (int i = 0; i < gridHeight; i++)
			{
				for (int j = 0; j < gridWidth; j++)
				{
					Point p = new Point(j, i);

					if (points.Sum(point => p.ComputeManhattanDistance(point)) < 10000)
					{
						area++;
					}
				}
			}

			return area.ToString();
		}

		private Point[] ParsePoints(string[] lines, out int gridWidth, out int gridHeight)
		{
			Point[] points = new Point[lines.Length];
			Point min = new Point(int.MaxValue, int.MaxValue);
			Point max = new Point(int.MinValue, int.MinValue);

			for (int i = 0; i < points.Length; i++)
			{
				string[] tokens = lines[i].Split(',');

				int x = int.Parse(tokens[0]);
				int y = int.Parse(tokens[1].Substring(1));

				points[i] = new Point(x, y);
				min.X = Math.Min(min.X, x);
				min.Y = Math.Min(min.Y, y);
				max.X = Math.Max(max.X, x);
				max.Y = Math.Max(max.Y, y);
			}

			int[] areaArray = new int[points.Length];

			gridWidth = max.X - min.X + 1;
			gridHeight = max.Y - min.Y + 1;

			foreach (Point p in points)
			{
				p.X -= min.X;
				p.Y -= min.Y;
			}

			return points;
		}

		private bool IsAreaInfinite(Point p, int index, Point[] points, List<Point> edgePoints)
		{
			foreach (Point e in edgePoints)
			{
				int distance = p.ComputeManhattanDistance(e);

				bool closestToEdge = true;

				for (int i = 0; i < points.Length; i++)
				{
					if (i == index)
					{
						continue;
					}

					if (points[i].ComputeManhattanDistance(e) <= distance)
					{
						closestToEdge = false;

						break;
					}
				}

				if (closestToEdge)
				{
					return true;
				}
			}

			return false;
		}

		private class Point
		{
			public Point(int x, int y)
			{
				X = x;
				Y = y;
			}

			public int X { get; set; }
			public int Y { get; set; }

			public int ComputeManhattanDistance(Point p)
			{
				return Math.Abs(p.X - X) + Math.Abs(p.Y - Y);
			}
		}
	}
}
