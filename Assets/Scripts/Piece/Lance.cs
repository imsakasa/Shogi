using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 香車
/// </summary>
public class Lance : IPiece
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, -1),
	};

	public bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public List<Address> MoveRanges(Board board, Address from)
	{
		return PieceUtility.CalcForeverMoveRange(board, from, MOVE_RANGE);
	}
}
