using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

///
/// IComparable
/// IFormattable
///

namespace csharp_advanced_programming.Chapter11
{
	class Chapter11
	{
		public static void Test()
		{
			Formulal.LinqQuery2();
			//string s = "Hello";
			//s.Foo();
		}
	}

	public static class StringExtension
	{
		// 拓展方法定义为静态方法
		// 拓展方法在一个静态类中声明
		// 拓展方法的第一个参数就是它拓展的类型
		// 拓展方法的第一个参数使用this关键字

		// 违反面向对象的规则：拓展方法给一个类型定义了新方法，但没有改变改类型或改变派生自它的类型。
		// 拓展方法不能访问它拓展的类型的私有成员。
		public static void Foo(this string s)
		{
			Console.WriteLine($"Foo invoked for {s}");
		}
	}

	public class Racer: IComparable<Racer>, IFormattable
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Country { get; set; }

		public int Starts { get; set; }

		public int Wins { get; set; }

		public IEnumerable<string>? Cars { get; set; }

		public IEnumerable<int>? Years { get; set; }

		public Racer(string firstName, string lastName, string country, int starts, int wins): this(firstName, lastName, country, starts, wins, null, null)
		{
		}

		public Racer(string firstName, string lastName, string country, int starts, int wins, IEnumerable<int>? years, IEnumerable<string>? cars)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Country = country;
			this.Starts = starts;
			this.Wins = wins;
			this.Years = years;
			this.Cars = cars;
		}

		public override string ToString()
		{
			return String.Format("{0} {1}", this.FirstName, this.LastName);
		}

		public string ToString(string format)
		{
			return ToString(format, null);
		}

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			switch(format)
			{
				case "N": return ToString();
				case "F": return this.FirstName;
				case "L": return this.LastName;
				case "C": return this.Country;
				case "S": return this.Starts.ToString();
				case "W": return this.Wins.ToString();
				case "A": return String.Format($"{FirstName}, {LastName}, {Country}, {Starts}, {Wins}");
				default: throw new FormatException($"{format} Not Support!");
			}
		}

		public int CompareTo(Racer? other)
		{
			if (other == null) return -1;
			return string.Compare(this.FirstName, other.LastName.ToString());
		}
	}

	public class Team
	{
		public Team(string name, params int[] years)
		{
			this.Name = name;
			this.Years = years;
		}

		public string Name { get; private set; }

		public IEnumerable<int> Years { get; private set; }
	}

	public class Formulal
	{
		private static List<Racer>? racers;
		public static IList<Racer> GetRacerChampions()
		{
			if (Formulal.racers == null)
			{
				racers = new List<Racer>(1)
				{
					new Racer("Lamsthim", "Farina", "Austria", 33, 22, new int[] { 2000}, new string[] {"Alfa Remeo"}),
					new Racer("Nino", "Farina", "Italy", 33, 25, new int[] { 1950}, new string[] {"Alfa Remeo"}),
					new Racer("Tom", "Farina", "Brazil", 33, 15, new int[] { 1950}, new string[] {"Alfa Remeo"}),
					new Racer("Jim", "Koroa", "Brazil", 33, 10, new int[] { 1980}, new string[] {"Alfa Remeo"}),
				};
			}
			return racers;
		}

		private static List<Team>? teams;
		public static IList<Team> GetTeamChampions()
		{
			if (teams == null)
			{
				teams = new List<Team>(3)
				{
					new Team("vanwall-1958", 1958),
					new Team("vanwall-1959", 1959),
					new Team("vanwall-1960", 1960),
				};
			}
			return teams;
		}

		/// <summary>
		/// 找出赢得比赛的巴西赛车手
		/// </summary>
		public static void LinqQuery()
		{
			var racers = from r in Formulal.GetRacerChampions().ToList() 
						 where r.Country == "Italy"
						 orderby r.Wins descending 
						 select r;
			// 查询表达式必须以 from 子句开头，以 select 或 group 子句结束。

			foreach(Racer racer in racers)
			{
				Console.WriteLine($"{racer:A}");
			}
		}

		/// <summary>
		/// 找出赢得至少15场比赛的巴西和奥地利赛车手
		/// </summary>
		public static void LinqQuery2()
		{
			var racers = from racer in Formulal.GetRacerChampions().ToList()
						 where racer.Wins >= 15 && (racer.Country == "Brazil" || racer.Country == "Austria")
						 select racer;
			foreach(Racer racer in racers)
			{
				Console.WriteLine($"{racer:A}");
			}
		}

		/// <summary>
		/// 找出赢得至少15场比赛的巴西和奥地利赛车手
		/// </summary>
		public static void LinqQuery3()
		{
			// 使用拓展方法 Select() 和 Where()
			var racers = Formulal.GetRacerChampions().Where(
				racer => racer.Wins >= 15 && (racer.Country == "Brazil" || racer.Country == "Austria")
			).Select(racer => racer);
			foreach (Racer racer in racers)
			{
				Console.WriteLine($"{racer:A}");
			}
		}

		/// <summary>
		/// 用索引筛选
		/// 找出姓名以A开头，索引为偶数的赛车手
		/// </summary>
		public static void LinqQuery4()
		{
			var racers = Formulal.GetRacerChampions().Where((r, i) => r.FirstName.StartsWith('A') && i % 2 == 0);
			foreach (Racer racer in racers)
			{
				Console.WriteLine($"{racer:A}");
			}
		}

		/// <summary>
		/// 类型筛选
		/// </summary>
		public static void LinqQuery5()
		{
			object[] data = { "one", 2, 3, "four", "five", 6 };
			var strs = data.OfType<string>();
			foreach (string str in strs)
			{
				Console.WriteLine($"{str}");
			}
		}

		/// <summary>
		/// 复合的 from 子句
		/// 筛选驾驶法拉第的所有冠军
		/// </summary>
		public static void LinqQuery6()
		{
			var drivers = from r in Formulal.GetRacerChampions()
						  from c in r.Cars // 根据对象的一个成员进行筛选，该成员本身是一个系列。
						  where c == "Ferrari"
						  select $"{c:N}";
			foreach (string str in drivers)
			{
				Console.WriteLine($"{str}");
			}
		}
	}
}
