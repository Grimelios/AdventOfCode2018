using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2018
{
	public class Puzzle04 : PuzzleSolver
	{
		public Puzzle04() : base(4)
		{
		}

		protected override string SolvePart1(string[] lines)
		{
			Guard[] guards = ParseGuards(lines);
			Guard guard = null;

			int maxSleepTime = 0;

			foreach (Guard g in guards)
			{
				int sum = g.SleepArray.Sum();

				if (sum > maxSleepTime)
				{
					maxSleepTime = sum;
					guard = g;
				}
			}

			var array = guard.SleepArray;
			int max = 0;
			int index = 0;

			for (int i = 0; i < 60; i++)
			{
				int value = array[i];

				if (value > max)
				{
					max = value;
					index = i;
				}
			}

			int result = guard.Id * index;

			return result.ToString();
		}

		protected override string SolvePart2(string[] lines)
		{
			Guard[] guards = ParseGuards(lines);

			int max = 0;
			int id = 0;
			int index = 0;

			foreach (Guard g in guards)
			{
				var array = g.SleepArray;

				for (int i = 0; i < 60; i++)
				{
					int value = array[i];

					if (value > max)
					{
						max = value;
						id = g.Id;
						index = i;
					}
				}
			}

			return (id * index).ToString();
		}

		private Guard[] ParseGuards(string[] lines)
		{
			Timestamp[] timestamps = new Timestamp[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				timestamps[i] = new Timestamp(lines[i]);
			}

			Array.Sort(timestamps, (t1, t2) =>
			{
				if (t1.Year != t2.Year)
				{
					return t1.Year.CompareTo(t2.Year);
				}

				if (t1.Month != t2.Month)
				{
					return t1.Month.CompareTo(t2.Month);
				}

				if (t1.Day != t2.Day)
				{
					return t1.Day.CompareTo(t2.Day);
				}

				return t1.Hour != t2.Hour
					? t1.Hour.CompareTo(t2.Hour)
					: t1.Minute.CompareTo(t2.Minute);
			});

			Dictionary<int, Guard> guardMap = new Dictionary<int, Guard>();
			Guard guard = null;

			int fellAsleep = 0;
			bool asleep = false;

			foreach (Timestamp t in timestamps)
			{
				int id = t.GuardId;

				if (id != -1)
				{
					if (!guardMap.TryGetValue(id, out guard))
					{
						guard = new Guard(id);
						guardMap.Add(id, guard);
					}

					continue;
				}

				asleep = !asleep;

				if (asleep)
				{
					fellAsleep = t.Minute;
				}
				else
				{
					for (int i = fellAsleep; i < t.Minute; i++)
					{
						guard.SleepArray[i]++;
					}
				}
			}

			return guardMap.Values.ToArray();
		}

		private class Guard
		{
			public Guard(int id)
			{
				Id = id;
				SleepArray = new int[60];
			}

			public int Id { get; }
			public int[] SleepArray { get; }
		}

		private class Timestamp
		{
			public Timestamp(string line)
			{
				Year = int.Parse(line.Substring(1, 4));
				Month = int.Parse(line.Substring(6, 2));
				Day = int.Parse(line.Substring(9, 2));
				Hour = int.Parse(line.Substring(12, 2));
				Minute = int.Parse(line.Substring(15, 2));
				GuardId = line[19] == 'G' ? int.Parse(line.Substring(26, line.IndexOf(' ', 26) - 26)) : -1;
			}

			public int Year { get; }
			public int Month { get; }
			public int Day { get; }
			public int Hour { get; }
			public int Minute { get; }
			public int GuardId { get; }
		}
	}
}
