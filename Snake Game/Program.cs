using System;
using System.Threading;

namespace SnakeGame
{
	class Program
	{ 
		public static int fieldWidth = 60;
		public static int fieldHeight = 25;
		public static int windowWidth = 80;
		public static int windowHeight = fieldHeight;
		public static ConsoleColor fieldColor = ConsoleColor.DarkGreen;
		public static ConsoleColor wallColor = ConsoleColor.White;

		static void SetupConsole()
		{
			Console.CursorVisible = false;
			Console.BackgroundColor = fieldColor;
			Console.WindowHeight = windowHeight;
			Console.WindowWidth = windowWidth;
			Console.BufferHeight = Console.WindowHeight;
			Console.BufferWidth = Console.WindowWidth;
			Console.WindowWidth = Console.BufferWidth;
			Console.WindowHeight = Console.BufferHeight;
			Console.Clear();
		}
		static void DrawWalls()
		{
			char drawChar = '█';
			Console.ForegroundColor = wallColor;
			for (int i = 0; i < fieldWidth; i++)
			{
				Console.SetCursorPosition(i, 0);
				Console.Write(drawChar);
				Console.SetCursorPosition(i, fieldHeight - 1);
				Console.Write(drawChar);
			}
			for (int i = 0; i < fieldHeight; i++)
			{
				Console.SetCursorPosition(0, i);
				Console.Write(drawChar);
				Console.SetCursorPosition(fieldWidth - 1, i);
				Console.Write(drawChar);
			}
		}
		static void FinalScreen()
		{

		}

		static void Main()
		{
			SetupConsole();

			Snake snake = new Snake(10, 10, 9);
			Food food = new Food();

			DrawWalls();

			int ticks = 0;
			while (snake.Alive)
			{
				bool isOverFood = false;
				if ((snake.X == food.X && snake.Y == food.Y) || ticks == 80)
				{
					if (snake.X == food.X && snake.Y == food.Y)
					{
						isOverFood = true;
					}
					food.Remove();
					food = new Food();
					ticks = 0;
				}

				snake.GetInput();
				snake.Update(isOverFood);

				ticks++;
				if(snake.Direction == Directions.Up || snake.Direction == Directions.Down)
				{
					Thread.Sleep(70);
				}
				else
				Thread.Sleep(50);
			}
			FinalScreen();
		}
	}
}