using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
	class Food
	{
		public Food()
		{
			Random rnd = new Random();
			X = rnd.Next(1, Console.BufferWidth - 1);
			Y = rnd.Next(1, Console.BufferHeight - 1);
			Console.SetCursorPosition(X, Y);
			Console.Write('f');
		}

		public int X { get; set; }
		public int Y { get; set; }

		public void Remove()
		{
			Console.SetCursorPosition(X, Y);
			Console.Write(' ');
		}
	}
}