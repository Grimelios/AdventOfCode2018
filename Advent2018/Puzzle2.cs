using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle2 : PuzzleSolver
	{
		public Puzzle2() : base(2)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			int doubleCount = 0;
			int tripleCount = 0;

			foreach (string line in lines)
			{
				int[] characterCounts = new int[26];

				foreach (char c in line)
				{
					characterCounts[c - 'a']++;
				}

				bool hasDouble = false;
				bool hasTriple = false;

				foreach (int count in characterCounts)
				{
					switch (count)
					{
						case 2:
							if (!hasDouble)
							{
								doubleCount++;
								hasDouble = true;
							}

							break;

						case 3:
							if (!hasTriple)
							{
								tripleCount++;
								hasTriple = true;
							}

							break;
					}

					if (hasDouble && hasTriple)
					{
						break;
					}
				}
			}

			return (doubleCount * tripleCount).ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			for (int i = 0; i < lines.Length - 1; i++)
			{
				string line1 = lines[i];

				for (int j = i + 1; j < lines.Length; j++)
				{
					string line2 = lines[j];
					bool mismatchFound = false;
					int resultIndex = 0;

					for (int k = 0; k < line1.Length; k++)
					{
						char c1 = line1[k];
						char c2 = line2[k];

						if (c1 == c2)
						{
							continue;
						}

						if (mismatchFound)
						{
							resultIndex = -1;

							break;
						}

						mismatchFound = true;
						resultIndex = k;
					}

					if (resultIndex >= 0)
					{
						return line1.Substring(0, resultIndex) + line1.Substring(resultIndex + 1);
					}
				}
			}

			return null;
		}
	}
}
