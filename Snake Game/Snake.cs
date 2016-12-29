using System;
using System.Collections.Generic;

namespace SnakeGame
{
	class Snake
	{
		public Snake(int x, int y, int length)
		{
			X = x;
			Y = y;
			Direction = Directions.Right;
			Tail = new List<TailElement>();

			for (int i = X - length; i < X + 1; i++)
			{
				Tail.Add(new TailElement(i, y));
			}

		}
		public int X { get; set; }
		public int Y { get; set; }
		public Directions Direction { get; set; }
		public List<TailElement> Tail { get; set; }

		public void Update()
		{
			int width = Console.BufferWidth;
			int height = Console.BufferHeight;
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

			if (X >= width - 1)
			{
				X = 1;
			}
			if (X < 1)
			{
				X = width - 2;
			}
			if (Y < 1)
			{
				Y = height - 2;
			}
			if (Y >= height - 1)
			{
				Y = 1;
			}

			// Draw
			Console.SetCursorPosition(Tail[0].X, Tail[0].Y);
			Console.Write(" ");
			Tail.RemoveAt(0);
			Tail.Add(new TailElement(X, Y));
			for (int i = 0; i < Tail.Count - 1; i++)
			{
				Console.SetCursorPosition(Tail[i].X, Tail[i].Y);
				Console.Write('0');
			}
			Console.SetCursorPosition(Tail[Tail.Count - 1].X, Tail[Tail.Count - 1].Y);
			Console.Write('@');
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