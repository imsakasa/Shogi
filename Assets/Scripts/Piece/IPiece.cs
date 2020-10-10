using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
	List<Address> MoveRanges(Address currentPos);
}

public struct Address
{
	public static readonly int MAX_WIDTH = 9;
	public int X { get; private set; }
	public int Y { get; private set; }

	public Address(int x, int y)
	{
		X = x;
		Y = y;
	}

	public bool IsValid()
	{
		if (MAX_WIDTH > X || X < 0)
		{
			return false;
		}

		if (MAX_WIDTH > Y || Y < 0)
		{
			return false;
		}

		return true;
	}

	public static Address operator +(Address a, Address b) => new Address(a.X + b.X, a.Y + b.Y);
}
