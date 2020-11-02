using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 成銀
/// </summary>
public class PromotedSilver : IPiece
{
	public bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public List<Address> MoveRanges(Board board, Address from)
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
