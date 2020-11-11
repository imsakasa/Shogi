using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 金将
/// </summary>
public class Gold : PieceBase
{
	public static IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, -1),
		new Address(-1, -1),
		new Address(-1, 0),
		new Address(0, +1),
		new Address(1, 0),
		new Address(1, -1),
	};

	public override bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public override List<Address> MoveRanges(Board board, Address from)
	{
		var ranges = new List<Address>();
		for (int i = 0; i < MOVE_RANGE.Count; i++)
		{
			ranges.Add(from + MOVE_RANGE[i]);
		}

		var validRanges = ranges.Where(address => address.IsValid() && !board.IsPuttedSelfPiece(address)).ToList();
		return validRanges;
	}
}
