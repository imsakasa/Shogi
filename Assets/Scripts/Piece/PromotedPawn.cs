﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// と金
/// </summary>
public class PromotedPawn : PieceBase
{
	public override bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public override List<Address> MoveRanges(Board board, Address from)
	{
		var ranges = new List<Address>();
		for (int i = 0; i < Gold.MOVE_RANGE.Count; i++)
		{
			ranges.Add(from + Gold.MOVE_RANGE[i]);
		}

		var validRanges = ranges.Where(address => address.IsValid() && !board.IsPuttedSelfPiece(address)).ToList();
		return validRanges;
	}
}