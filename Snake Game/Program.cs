using System;
using System.Threading;

namespace SnakeGame
{
	class Program
	{
		public const int frameTime = 60;
		public const int fieldWidth = 60;
		public const int fieldHeight = 25;
		public const int windowWidth = 80;
		public const int windowHeight = fieldHeight;
		public const ConsoleColor fieldColor = ConsoleColor.DarkGreen;
		public const ConsoleColor wallColor = ConsoleColor.White;


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
			for (int i = fieldWidth; i < windowWidth; i++)
			{
				for (int j = 0; j < windowHeight; j++)
				{
					Console.SetCursorPosition(i, j);
					Console.BackgroundColor = ConsoleColor.Black;
					Console.Write(' ');
				}
			}
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
		static void UpdateTime(DateTime startTime)
		{
			TimeSpan time = DateTime.Now - startTime;
			Console.SetCursorPosition(fieldWidth + 1, 3);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"Time: {time.Minutes:00}:{(time.Seconds):00}");
		}

		static void GameOverScreen()
		{
			string[] gameOver = ASCIIArt.gameOver.Split('\n');
			Console.Clear();
			for (int i = 0; i < gameOver.Length; i++)
			{
				Console.SetCursorPosition(
					windowWidth / 2 - gameOver[i].Length / 2,
					windowHeight / 2 - gameOver.Length + i);
				Console.Write(gameOver[i]);
			}
		}

		static void Main()
		{
			SetupConsole();

			Snake snake = new Snake(10, 10, 9);
			Food food = new Food();
		
			DateTime startTime = DateTime.Now;

			int ticks = 0;
			while (snake.Alive)
			{
				bool isOverFood = false;
				if (snake.X == food.X && snake.Y == food.Y)
				{
					if (snake.X == food.X && snake.Y == food.Y)
					{
						isOverFood = true;
						snake.Score++;
					}
					food = new Food();
					ticks = 0;
				}

				snake.GetInput();
				snake.Update(isOverFood);
				food.Update();
				DrawWalls();
				UpdateTime(startTime);

				ticks++;
				if (snake.Direction == Directions.Up || snake.Direction == Directions.Down)
					Thread.Sleep(frameTime);
				else
					Thread.Sleep((int)(frameTime * 0.7));
			}
			GameOverScreen();
		}
	}
}