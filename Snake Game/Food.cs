using System;

namespace SnakeGame
{
	class Food
	{
		private char drawChar = '■';
		private ConsoleColor color = ConsoleColor.Blue;

		public Food()
		{
			Random rnd = new Random();
			X = rnd.Next(1, Program.fieldWidth - 1);
			Y = rnd.Next(1, Program.fieldHeight - 1);
			Console.SetCursorPosition(X, Y);
			Console.ForegroundColor = color;
			Console.Write(drawChar);
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