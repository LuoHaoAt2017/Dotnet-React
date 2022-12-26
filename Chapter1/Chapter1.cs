using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * 
 */
namespace csharp_advanced_programming.Chapter1
{
	internal class Chapter1
	{
		public static void Test()
		{
			// 1.多播委托：委托调用方法不应该有返回值。
			Action<int> operation1 = MathOperation.One;
			Action<int> operation2 = MathOperation.Two;
			Action<int> operations = operation1 + operation2;
			// 2.多播委托：正确地调用方式（异常处理方式)
			Delegate[] delegates = operations.GetInvocationList();
			foreach (Action<int> operation in delegates)
			{
				try
				{
					operation(3);
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		public static void Test1()
		{
			// 匿名方法
			Func<string, string> print = delegate(string mesg)
			{
				return mesg;
			};

			Console.WriteLine(print("Hello World"));
		}

		public static void Test2()
		{
			// lambda 表达式
			// 只要有委托参数类型的地方就可以使用 lambda 表达式。
			Func<string, string> print1 = (string mesg) =>
			{
				return mesg;
			};

			Func<string, string> print2 = (mesg) =>
			{
				return mesg;
			};

			Func<string, string> print3 = (mesg) => mesg;

			Console.WriteLine(print1("Hello World"));
			Console.WriteLine(print2("Hello World"));
			Console.WriteLine(print3("Hello World"));
		}

		public static void Test3()
		{
			// 在 lambda 表达式内部使用 lambda 表达式外部的变量，称为闭包。
			int y = 1;
			Func<int, int> f = (x) => x + y;
			y = 2;
			Console.WriteLine(f(3));
		}

		public static void Test4()
		{
			List<int> list = new List<int>() { 10, 20, 30 };
			List<Func<int>> funcs = new List<Func<int>>(3);

			foreach (int value in list)
			{
				funcs.Add(() => value);
			}

			// C# 4.0 的结果是 30 , 30, 30
			// C# 5.0 的结果是 10 , 20, 30
			foreach (Func<int> func in funcs)
			{
				Console.WriteLine(func());
			}
		}

		public static void Test5()
		{
			CarDealer carDealer = new CarDealer();
			Consumer consumer1 = new Consumer("1");
			Consumer consumer2 = new Consumer("1");
			Consumer consumer3 = new Consumer("1");
			carDealer.NewCarEvent += consumer1.NewCarIsHere;
			carDealer.NewCarEvent += consumer2.NewCarIsHere;
			carDealer.NewCarEvent += consumer3.NewCarIsHere;
			carDealer.NewCar("BENTIAN");
		}
	}

	class MathOperation
	{
		public static void One(int value)
		{
			Console.WriteLine($"one: {value}");
			throw new ArgumentException("参数异常");
		}

		public static void Two(int value)
		{
			Console.WriteLine($"two: {value}");
		}
	}

	public class CarInfoEventArgs: EventArgs
	{
		public string Car { get; private set; }

		public CarInfoEventArgs(string args)
		{
			this.Car = args;
		}
	}

	public class CarDealer
	{
		public event EventHandler<CarInfoEventArgs> NewCarInfo;

		public void NewCar(string car)
		{
			RaiseNewCarInfo(car);
		}

		private void RaiseNewCarInfo(string car)
		{
			EventHandler<CarInfoEventArgs> eventHandler = NewCarInfo;
			eventHandler?.Invoke(this, new CarInfoEventArgs(car));
		}
	}

	public class Consumer
	{
		private string name;

		public Consumer(string name)
		{
			this.name = name;
		}

		public void NewCarIsHere(object? sender, CarInfoEventArgs e)
		{
			Console.WriteLine($"{this.name}, {e.Car} is Coming.");
		}
	}
}
