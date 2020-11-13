using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 敵の歩兵
/// </summary>
public class EnemyPawn : EnemyPieceBase
{
	// public BestHandInfo GetBestHand(Square[,] board, Address address)
	// {

	// }
	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(from.X, from.Y + 1));

		var validRanges = ranges.Where(pos => pos.IsValid() && !board[pos.X, pos.Y].IsEnemy()).ToList();
		return validRanges;
	}
}
