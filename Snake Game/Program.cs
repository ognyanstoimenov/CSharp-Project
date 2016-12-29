using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Unnamed_Game
{
	class Program
	{
		static void Main()
		{
			Snake snake = new Snake(9, 0, 'O',10);
			Console.CursorVisible = false;
			Console.BufferHeight = Console.WindowHeight;
			while (true)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo pressedKey = Console.ReadKey();
					if (pressedKey.Key == ConsoleKey.DownArrow && snake.Direction != Directions.Up)
					{
						snake.Direction = Directions.Down;
					}

					else if (pressedKey.Key == ConsoleKey.UpArrow && snake.Direction != Directions.Down)
					{
						snake.Direction = Directions.Up;
					}

					else if (pressedKey.Key == ConsoleKey.RightArrow && snake.Direction != Directions.Left)
					{
						snake.Direction = Directions.Right;
					}

					else if (pressedKey.Key == ConsoleKey.LeftArrow && snake.Direction != Directions.Right)
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
