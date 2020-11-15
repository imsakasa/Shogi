using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 香車
/// </summary>
public class Lance : PlayerPieceBase
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, -1),
	};

	public override bool CanMove(Square[][] board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public override List<Address> MoveRanges(Square[][] board, Address from)
	{
		return PieceUtility.CalcForeverMoveRange(board, from, MOVE_RANGE);
	}
}
