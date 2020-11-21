using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPromotedBishop : EnemyPieceBase
{
	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		var bishopRanges =  PieceUtility.CalcForeverMoveEnemyRange(board, from, EnemyBishop.MOVE_RANGE);

		var promotedRanges = new List<Address>();
		promotedRanges.Add(new Address(from.X, from.Y - 1));
		promotedRanges.Add(new Address(from.X - 1, from.Y));
		promotedRanges.Add(new Address(from.X, from.Y + 1));
		promotedRanges.Add(new Address(from.X + 1, from.Y));

		bishopRanges.AddRange(promotedRanges);

		return bishopRanges;
	}
}
