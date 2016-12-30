using System;

namespace SnakeGame
{
	class Food
	{
		private const char drawChar = '■';
		private const ConsoleColor color = ConsoleColor.Magenta;

		public Food()
		{
			Random rnd = new Random();
			X = rnd.Next(1, Program.fieldWidth - 1);
			Y = rnd.Next(1, Program.fieldHeight - 1);
		}

		public int X { get; set; }
		public int Y { get; set; }

		public void Update()
		{
			Console.SetCursorPosition(X, Y);
			Console.BackgroundColor = Program.fieldColor;
			Console.ForegroundColor = color;
			Console.Write(drawChar);
		}
	}
}