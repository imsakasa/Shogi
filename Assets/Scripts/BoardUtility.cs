using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

	public static Square[,] CreateCopyBoard(Square[,] sourceBoard)
	{
		Square[,] copyBoard = new Square[Board.BOARD_WIDTH, Board.BOARD_WIDTH];
		for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		{
			copyBoard[x, y] = sourceBoard[x, y].DeepCopy();
		}

		return copyBoard;
	}

	public static bool IsTwoPawn(Square[,] board, PieceMoveInfo pieceMoveInfo, int x)
	{
		if (!pieceMoveInfo.IsMoveAcquiredPiece)
		{
			return false;
		}

		if (pieceMoveInfo.PieceInfo != PieceInfo.Pawn)
		{
			return false;
		}

		var ranges = BoardUtility.VerticalRanges(x);
		return ranges.Any(address => board[address.X, address.Y].PieceInfo == PieceInfo.Pawn);
	}
}
