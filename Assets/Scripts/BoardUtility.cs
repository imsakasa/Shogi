using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardUtility
{
	static readonly int ENEMY_RANGE_THRESHOLD = 3;

	public static List<Address> VerticalRanges(int x)
	{
		var ranges = new List<Address>();
		for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
		{
			ranges.Add(new Address(x, y));
		}

		return ranges;
	}

	public static bool IsEnemyArea(Address targetAddress)
	{
		return targetAddress.Y <= ENEMY_RANGE_THRESHOLD;
	}

	public static Square[][] CreateCopyBoard(Square[][] sourceBoard)
	{
		Square[][] copyBoard = new Square[][] { new Square[Board.BOARD_WIDTH], new Square[Board.BOARD_WIDTH] };
		Array.Copy(sourceBoard, copyBoard, sourceBoard.Length);

		return copyBoard;
	}
}
