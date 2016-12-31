using System;
using System.Threading;

namespace SnakeGame
{
	class Game
	{
		public const int frameTime = 60;
		public const int fieldWidth = 60;
		public const int fieldHeight = 25;
		public const int windowWidth = 80;
		public const int windowHeight = fieldHeight;
		public const ConsoleColor fieldColor = ConsoleColor.DarkGreen;
		public const ConsoleColor wallColor = ConsoleColor.White;

		private static DateTime startTime;
		private static TimeSpan time;

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
		static void UpdateTimeAndScore()
		{
			// Update Time
			time = DateTime.Now - startTime;
			Console.SetCursorPosition(fieldWidth + 1, 3);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"Time: {time.Minutes:00}:{(time.Seconds):00}");
			// Update score
			Console.SetCursorPosition(fieldWidth + 1, 0);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"Score: {Snake.Score}");
		}
		static void GameOverScreen()
		{
			// Show last frame
			Thread.Sleep(600);
			Console.Clear();
			string[] gameOver = ASCIIArt.gameOver.Split('\n');
			for (int i = 0; i < gameOver.Length; i++)
			{
				Console.SetCursorPosition(
					windowWidth / 2 - gameOver[i].Length / 2,
					windowHeight / 2 - gameOver.Length + i);
				Console.Write(gameOver[i]);
			}
			string scoreString = string.Format($"Score: {Snake.Score}");
			Console.SetCursorPosition(windowWidth / 2 - scoreString.Length / 2, windowHeight / 2);
			Console.Write(scoreString);
			string timeString = string.Format($"Time Alive: {time.Minutes:00}:{(time.Seconds):00}");
			Console.SetCursorPosition(windowWidth / 2 - timeString.Length / 2 - 1, windowHeight / 2 + 1);
			Console.Write(timeString);

			// Buttons
			string[] btns = { "Play Again", "Exit" };
			int btnsX = windowWidth / 2 - (btns[0].Length + 5 + btns[0].Length) / 2;
			int btnsY = windowHeight / 2 + 4;
			int selected = 0;
			Console.SetCursorPosition(btnsX, btnsY);
			Console.Write(btns[0]);
			Console.Write(new string(' ', 5));
			Console.Write(btns[1]);
			while (true)
			{
				// Underline selected
				char b0 = selected == 0 ? '-' : ' ';
				char b1 = selected == 1 ? '-' : ' ';
				Console.SetCursorPosition(btnsX, btnsY + 1);
				Console.Write(new string(b0, btns[0].Length));
				Console.SetCursorPosition(btnsX + btns[0].Length + 5, btnsY + 1);
				Console.Write(new string(b1, btns[1].Length));
				// Change selection
				ConsoleKey pressedKey = Console.ReadKey(true).Key;
				if (pressedKey == ConsoleKey.RightArrow)
					selected++;
				else if (pressedKey == ConsoleKey.LeftArrow)
					selected--;
				else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.Spacebar)
				{
					if (selected == 0)
						Main();
					else
						return;
				}
				selected = Math.Abs(selected) % 2;
			}
		}

		static void Main()
		{
			SetupConsole();

			Snake snake = new Snake(10, 10, 9);
			Food food = new Food();

			startTime = DateTime.Now;

			while (snake.Alive)
			{
				bool isOverFood = false;
				if (snake.X == food.X && snake.Y == food.Y)
				{
					Snake.Score++;
					isOverFood = true;
					food = new Food();
				}

				snake.GetInput();
				snake.Update(isOverFood);
				food.Update();
				DrawWalls();
				UpdateTimeAndScore();

				if (snake.Direction == Directions.Up || snake.Direction == Directions.Down)
					Thread.Sleep(frameTime);
				else
					Thread.Sleep((int)(frameTime * 0.7));
			}
			GameOverScreen();
		}
	}
}