using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
	class Snake
	{
		private const char drawChar = '█';
		private const ConsoleColor color = ConsoleColor.Black;

		public Snake(int x, int y, int length)
		{
			X = x;
			Y = y;
			Direction = Directions.Right;
			Tail = new List<TailElement>();
			Alive = true;
			Score = 0;
			OverFood = false;

			for (int i = X - length + 1; i < X + 1; i++)
			{
				Tail.Add(new TailElement(i, y));
			}
		}
		public int X { get; set; }
		public int Y { get; set; }
		public Directions Direction { get; set; }
		public List<TailElement> Tail { get; set; }
		public bool Alive { get; set; }
		public bool OverFood { get; set; }

		public static int Score { get; set; }

		public void GetInput(DateTime nextFrame)
		{
			Directions nextDirection = Directions.NoChange;
			// Prevent the snake from getting stuck when holding down a key. 
			// Allows for multiple inputs per frame - only the last one is used.
			while (nextFrame >= DateTime.Now)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKey pressedKey = Console.ReadKey(true).Key;
					if (pressedKey == ConsoleKey.UpArrow && Direction != Directions.Down)
					{
						nextDirection = Directions.Up;
					}
					else if (pressedKey == ConsoleKey.RightArrow && Direction != Directions.Left)
					{
						nextDirection = Directions.Right;
					}
					else if (pressedKey == ConsoleKey.DownArrow && Direction != Directions.Up)
					{
						nextDirection = Directions.Down;
					}
					else if (pressedKey == ConsoleKey.LeftArrow && Direction != Directions.Right)
					{
						nextDirection = Directions.Left;
					}
				}
				// Check for input every 5ms
				Thread.Sleep(5);
			}
			// Prevent coliding with yourself if you chnage directions twice in one frame
			if (nextDirection != Directions.NoChange)
			{
				Direction = nextDirection;
			}
		}
		public void Update()
		{
			// Moving
			if (Direction == Directions.Up)
				Y--;
			if (Direction == Directions.Right)
				X++;
			if (Direction == Directions.Down)
				Y++;
			if (Direction == Directions.Left)
				X--;

			// Death
			if (X >= Game.fieldWidth - 1 || X < 1 || Y < 1 || Y >= Game.fieldHeight - 1)
			{
				Alive = false;
			}

			for (int i = 0; i < Tail.Count; i++)
			{
				if (X == Tail[i].X && Y == Tail[i].Y)
				{
					Alive = false;
				}
			}

			// Draw
			if (!OverFood)
			{
				Console.SetCursorPosition(Tail[0].X, Tail[0].Y);
				Console.BackgroundColor = Game.fieldColor;
				Console.Write(' ');
				Tail.RemoveAt(0);
			}
			Tail.Add(new TailElement(X, Y));
			for (int i = 0; i < Tail.Count; i++)
			{
				Console.SetCursorPosition(Tail[i].X, Tail[i].Y);
				Console.ForegroundColor = color;
				Console.Write(drawChar);
			}
		}
	}

	public enum Directions
	{
		NoChange,
		Up,
		Right,
		Down,
		Left
	}

	struct TailElement
	{
		public TailElement(int x, int y)
		{
			X = x;
			Y = y;
		}
		public int X { get; set; }
		public int Y { get; set; }
	}
}