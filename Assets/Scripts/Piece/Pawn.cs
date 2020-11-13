using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 歩兵
/// </summary>
public class Pawn : PieceBase
{
	public override bool CanMove(Square[,] board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(from.X, from.Y - 1));

		var validRanges = ranges.Where(pos => pos.IsValid() && !board[pos.X, pos.Y].IsSelf()).ToList();
		return validRanges;
	}
}
