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
	public int X { get; private set; }
	public int Y { get; private set; }

	public Address(int x, int y)
	{
		X = x;
		Y = y;
	}

	public bool IsValid()
	{
		if (Board.BOARD_WIDTH - 1 > X || X < 1)
		{
			return false;
		}

		if (Board.BOARD_WIDTH - 1 > Y || Y < 1)
		{
			return false;
		}

		return true;
	}

	public static Address operator +(Address a, Address b) => new Address(a.X + b.X, a.Y + b.Y);
}
