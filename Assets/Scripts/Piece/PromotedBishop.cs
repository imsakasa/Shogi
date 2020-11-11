﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 竜馬(成り角)
/// </summary>
public class PromotedBishop : PieceBase
{
	public override bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public override List<Address> MoveRanges(Board board, Address from)
	{
		var bishopRanges = PieceUtility.CalcForeverMoveRange(board, from, Bishop.MOVE_RANGE);

		var promotedRanges = new List<Address>();
		promotedRanges.Add(new Address(from.X, from.Y - 1));
		promotedRanges.Add(new Address(from.X - 1, from.Y));
		promotedRanges.Add(new Address(from.X, from.Y + 1));
		promotedRanges.Add(new Address(from.X + 1, from.Y));

		bishopRanges.AddRange(promotedRanges);

		return bishopRanges;
	}
}
