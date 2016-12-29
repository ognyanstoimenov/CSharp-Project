using System;
using System.Threading;

namespace SnakeGame
{
	class Program
	{
		static void Main()
		{
			Snake snake = new Snake(9, 0, 10, '█');
			Console.CursorVisible = false;
			Console.BufferHeight = Console.WindowHeight;
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
				snake.Draw();
				Thread.Sleep(40);
			}
		}
	}
}