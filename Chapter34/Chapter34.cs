using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

// XML 处理
// 1. XML 读取器 XMLReader 抽象类
// 2. XML 写入器 XMLWriter 抽象类
// 3. 使用 System.Xml.Serialization 名称空间中的类 XMLSerializer，将 XML 文档创建一个对象。将对象序列化为 XML。
namespace csharp_advanced_programming.Chapter34
{
	public class Chapter
	{
		public static void Test()
		{
			BookStore bookStore = new BookStore();
			Book book1 = new Book();
			Book book2 = new Book();
			Book book3 = new Book();

			book1.title = "The Autobiography of Benjamin Franklin";
			book1.author = new Author("Benjamin", "Franklin");
			book1.price = "8.99";

			book2.title = "The Confidence Man";
			book2.author = new Author("Herman", "Melville");
			book2.price = "11.99";

			book3.title = "The Gorigias";
			book3.author = new Author("Plato", "Terlgea");
			book3.price = "9.99";

			Book[] books = { book1, book2, book3 };
			bookStore.books = books;
			ObjectToXml<BookStore>("books.xml", bookStore);
		}

		public static void Test1()
		{
			XmlReader reader = XmlReader.Create("books.xml");
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Text)
				{
					Console.WriteLine(reader.Value);
				}
			}
		}

		// 逆序列化（活化）
		public static void XMLToObject()
		{

		}

		// 序列化（钝化）
		public static void ObjectToXml<T>(string path, T obj)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			// 读取对象，转换成二进制流。
			MemoryStream stream = new MemoryStream();
			serializer.Serialize(stream, obj);
			stream.Position = 0;
			// 加载二进制流，转化成文档，保存到指定文件。
			XmlDocument doc = new XmlDocument();
			doc.Load(stream);
			stream.Close();
			doc.Save(path);
		}

		public class BookStore
		{
			public Book[] books { get; set; }
		}

		public class Book
		{
			public string title { get; set; }

			public Author author { get; set; }

			public string price { get; set; }
		}

		public class Author
		{
			public string firstName { get; set; }

			public string lastName { get; set; }

			public Author(string firstName, string lastName)
			{
				this.firstName = firstName;
				this.lastName = lastName;
			}

			public Author()
			{

			}
		}
	}
}
