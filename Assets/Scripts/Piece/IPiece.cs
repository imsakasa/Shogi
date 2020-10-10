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
}
