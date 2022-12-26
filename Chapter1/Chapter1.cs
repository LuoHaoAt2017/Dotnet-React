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
}
