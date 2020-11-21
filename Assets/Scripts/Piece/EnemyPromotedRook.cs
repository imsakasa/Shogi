using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPromotedRook : EnemyPieceBase
{
	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		var rookRanges = PieceUtility.CalcForeverMovePlayerRange(board, from, Rook.MOVE_RANGE);

		var promotedRanges = new List<Address>();
		promotedRanges.Add(new Address(from.X - 1, from.Y - 1));
		promotedRanges.Add(new Address(from.X - 1, from.Y + 1));
		promotedRanges.Add(new Address(from.X + 1, from.Y + 1));
		promotedRanges.Add(new Address(from.X + 1, from.Y - 1));

		rookRanges.AddRange(promotedRanges);
		return rookRanges;
	}
}
