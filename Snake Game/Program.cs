using System;
using System.Threading;

namespace SnakeGame
{
	class Program
	{
		static void DrawWalls()
		{
			for (int i = 0; i < Console.BufferWidth - 1; i++)
			{
				Console.SetCursorPosition(i, 0);
				Console.Write('█');
				Console.SetCursorPosition(i, Console.BufferHeight - 1);
				Console.Write('█');
			}
			for (int i = 0; i < Console.BufferHeight - 1; i++)
			{
				Console.SetCursorPosition(0, i);
				Console.Write('█');
				Console.SetCursorPosition(Console.BufferWidth - 1, i);
				Console.Write('█');
			}
		}
		static void Main()
		{
			Snake snake = new Snake(10, 10, 2);
			Console.CursorVisible = false;
			Console.BufferHeight = Console.WindowHeight;

			DrawWalls();
			while (true)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKey pressedKey = Console.ReadKey().Key;
					if (pressedKey == ConsoleKey.UpArrow && snake.Direction != Directions.Down)
					{
						snake.Direction = Directions.Up;
					}
					else if (pressedKey == ConsoleKey.RightArrow && snake.Direction != Directions.Left)
					{
						snake.Direction = Directions.Right;
					}
					else if (pressedKey == ConsoleKey.DownArrow && snake.Direction != Directions.Up)
					{
						snake.Direction = Directions.Down;
					}
					else if (pressedKey == ConsoleKey.LeftArrow && snake.Direction != Directions.Right)
					{
						snake.Direction = Directions.Left;
					}
				}
				snake.Update();
				Thread.Sleep(50);
			}
		}
	}
}