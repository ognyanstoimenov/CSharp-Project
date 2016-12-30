using System;

namespace SnakeGame
{
	class Food
	{
		private const char drawChar = '■';
		private const ConsoleColor color = ConsoleColor.Red;

		public Food()
		{
			Random rnd = new Random();
			X = rnd.Next(1, Game.fieldWidth - 1);
			Y = rnd.Next(1, Game.fieldHeight - 1);
		}

		public int X { get; set; }
		public int Y { get; set; }

		public void Update()
		{
			Console.SetCursorPosition(X, Y);
			Console.BackgroundColor = Game.fieldColor;
			Console.ForegroundColor = color;
			Console.Write(drawChar);
		}
	}
}