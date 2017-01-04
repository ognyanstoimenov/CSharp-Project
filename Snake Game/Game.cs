using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

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
		public static readonly string highScorePath = Environment.CurrentDirectory + "\\highscores.txt";

		private static DateTime startTime;
		private static TimeSpan time;
		private static List<string> highScores;

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
			// Update score
			Console.SetCursorPosition(fieldWidth + 1, 0);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"Score: {Snake.Score}");
			// Update Time
			time = DateTime.Now - startTime;
			Console.SetCursorPosition(fieldWidth + 1, 2);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write($"Time: {time.Minutes:00}:{(time.Seconds):00}");
			// High scores
			Console.SetCursorPosition(fieldWidth + 1, 4);
			Console.Write("High scores:");
			for (int i = 0; i < highScores.Count; i++)
			{
				Console.SetCursorPosition(fieldWidth + 1, i + 5);
				Console.Write(highScores[i]);
			}

		}
		static void GameOverScreen()
		{
			// Show last frame
			Thread.Sleep(750);
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
				Console.SetCursorPosition(btnsX, btnsY + 1);
				Console.Write(new string(selected == 0 ? '-' : ' ', btns[0].Length));
				Console.SetCursorPosition(btnsX + btns[0].Length + 5, btnsY + 1);
				Console.Write(new string(selected == 1 ? '-' : ' ', btns[1].Length));
				// Change selection
				ConsoleKey pressedKey = Console.ReadKey(true).Key;
				if (pressedKey == ConsoleKey.RightArrow)
					selected = 1;
				else if (pressedKey == ConsoleKey.LeftArrow)
					selected = 0;
				else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.Spacebar)
				{
					if (selected == 0)
						Main();
					else
						return;
				}
			}
		}
		static void GetHighScores()
		{
			if (File.Exists(highScorePath))
			{
				highScores = new List<string>(File.ReadAllLines(highScorePath));
			}
			else
			{
				File.Create(highScorePath);
				highScores = new List<string>();
			}
		}
		static void SaveHighScores()
		{
			int index = -1;
			for (int i = 0; i < highScores.Count; i++)
			{
				int currentValue = int.Parse(highScores[i].Split('-')[1]);
				if (Snake.Score >= currentValue)
				{
					index = i;
					break;
				}
			}
			if (index > -1)
			{
				highScores.Insert(index, "MEME-" + Snake.Score.ToString());
				File.WriteAllLines(highScorePath, highScores.ToArray());
			}
		}

		static void Main()
		{
			SetupConsole();

			GetHighScores();
			Snake snake = new Snake(10, 10, 9);
			Food food = new Food();

			startTime = DateTime.Now;

			while (snake.Alive)
			{
				DateTime nextFrame = DateTime.Now.AddMilliseconds(
					snake.Direction == Directions.Up || snake.Direction == Directions.Down ?
						frameTime : frameTime * 0.7
				);

				snake.OverFood = false;
				if (snake.X == food.X && snake.Y == food.Y)
				{
					Snake.Score++;
					snake.OverFood = true;
					food = new Food();
				}

				snake.GetInput(nextFrame);
				snake.Update();
				food.Update();
				DrawWalls();
				UpdateTimeAndScore();
			}
			//SaveHighScores();
			GameOverScreen();
		}
	}
}