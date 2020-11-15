using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 成香
/// </summary>
public class PromotedLance : PlayerPieceBase
{
	public override bool CanMove(Square[][] board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public override List<Address> MoveRanges(Square[][] board, Address from)
	{
		var ranges = new List<Address>();
		for (int i = 0; i < Gold.MOVE_RANGE.Count; i++)
		{
			ranges.Add(from + Gold.MOVE_RANGE[i]);
		}

		var validRanges = ranges.Where(pos => pos.IsValid() && !board[pos.X][pos.Y].IsSelf()).ToList();
		return validRanges;
	}
}
