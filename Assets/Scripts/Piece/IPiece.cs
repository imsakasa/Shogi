using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyPiece
{
	List<Address> MoveRanges(Board board, Address from);
}

public struct Address
{
	public static readonly Address INVALID_ADDRESS = new Address(-1, -1);

	public int X { get; private set; }
	public int Y { get; private set; }

	public Address(int x, int y)
	{
		X = x;
		Y = y;
	}

	public bool IsValid()
	{
		if (X >= Board.BOARD_WIDTH - 1 || X < 1)
		{
			return false;
		}

		if (Y >= Board.BOARD_WIDTH - 1 || Y < 1)
		{
			return false;
		}

		return true;
	}

	public static Address operator +(Address a, Address b) => new Address(a.X + b.X, a.Y + b.Y);
	public static bool operator ==(Address a, Address b) => (a.X == b.X && a.Y == b.Y);
	public static bool operator !=(Address a, Address b) => (a.X != b.X || a.Y != b.Y);

	// 以下2行が無いと CS0660 の Warning が出るため記載
	public override bool Equals(object o) => true;
	public override int GetHashCode() => 0;

	public override string ToString()
	{
		return $"X:{X}, Y:{Y}";
	}
}
