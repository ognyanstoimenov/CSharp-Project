using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnamed_Game
{
	class Snake
	{
		public Snake(int x, int y, char c,int length)
		{
			this.SnakeChar = c;
			this.X = x;
			this.Y = y;
			this.Direction = Directions.Right;
			this.Tail = new List<TailElement>();

			for (int i = 0; i < length; i++)
			{
				Tail.Add(new TailElement(i, y));
			}

		}
		public char SnakeChar { get; }
		public int X { get; set; }
		public int Y { get; set; }
		public Directions Direction { get; set; }
		public List<TailElement> Tail { get; set; }

		public void Update()
		{
			int width = Console.BufferWidth;
			int height = Console.BufferHeight;
			Console.Clear();
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

			if (X >= width)
			{
				X = 0;
			}
			if (X < 0)
			{
				X = width - 1;
			}
			if (Y < 0)
			{
				Y = height - 1;
			}
			if (Y >= height)
			{
				Y = 0;
			}
			Tail.RemoveAt(0);
			Tail.Add(new TailElement(X, Y));
		}
		public void Draw()
		{
			Console.Clear();
			for (int i = 0; i < Tail.Count; i++)
			{
				Console.SetCursorPosition(Tail[i].X, Tail[i].Y);
				Console.Write(SnakeChar);
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
		public TailElement(int x,int y)
		{
			this.X = x;
			this.Y = y;
		}
		public int X { get; set; }
		public int Y { get; set; }
	}
}
