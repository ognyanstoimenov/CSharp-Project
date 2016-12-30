using System;
using System.Collections.Generic;

namespace SnakeGame
{
	class Snake
	{
		private char drawChar = '█';
		private ConsoleColor color = ConsoleColor.Black;

		public Snake(int x, int y, int length)
		{
			X = x;
			Y = y;
			Direction = Directions.Right;
			Tail = new List<TailElement>();
			Alive = true;
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

		public void GetInput()
		{
			if (Console.KeyAvailable)
			{
				ConsoleKey pressedKey = Console.ReadKey(true).Key;
				if (pressedKey == ConsoleKey.UpArrow && Direction != Directions.Down)
				{
					Direction = Directions.Up;
				}
				else if (pressedKey == ConsoleKey.RightArrow && Direction != Directions.Left)
				{
					Direction = Directions.Right;
				}
				else if (pressedKey == ConsoleKey.DownArrow && Direction != Directions.Up)
				{
					Direction = Directions.Down;
				}
				else if (pressedKey == ConsoleKey.LeftArrow && Direction != Directions.Right)
				{
					Direction = Directions.Left;
				}
			}
		}

		public void Update(bool isOverFood)
		{
			// Moving
			if (Direction == Directions.Up)
			{
				Y--;
			}
			if (Direction == Directions.Right)
			{
				X++;
			}
			if (Direction == Directions.Down)
			{
				Y++;
			}
			if (Direction == Directions.Left)
			{
				X--;
			}
			
			// Death
			if (X >= Program.fieldWidth - 1 || X < 1 || Y < 1 || Y >= Program.fieldHeight - 1)
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
			if (!isOverFood)
			{
				Console.SetCursorPosition(Tail[0].X, Tail[0].Y);
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
		Up,
		Right,
		Down,
		Left
	}

	class TailElement
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