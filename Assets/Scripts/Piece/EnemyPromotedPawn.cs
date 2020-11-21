using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPromotedPawn : EnemyPieceBase
{
	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		var ranges = new List<Address>();
		for (int i = 0; i < EnemyGold.MOVE_RANGE.Count; i++)
		{
			ranges.Add(from + EnemyGold.MOVE_RANGE[i]);
		}

		var validRanges = ranges.Where(pos => pos.IsValid() && !board[pos.X, pos.Y].IsEnemy()).ToList();
		return validRanges;
	}
}
