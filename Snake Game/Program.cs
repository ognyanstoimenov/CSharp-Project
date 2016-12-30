using System;
using System.Threading;

namespace SnakeGame
{
	class Program
	{
		static void FinalScreen()
		{

		}
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
			// setup console
			Console.CursorVisible = false;
			Console.BufferHeight = Console.WindowHeight;

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