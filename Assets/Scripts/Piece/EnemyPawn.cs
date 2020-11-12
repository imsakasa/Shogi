using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 敵の歩兵
/// </summary>
public class EnemyPawn : EnemyPieceBase
{
	public override List<Address> MoveRanges(Board board, Address from)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(from.X, from.Y + 1));

		var validRanges = ranges.Where(address => address.IsValid() && !board.IsPuttedEnemyPiece(address)).ToList();
		return validRanges;
	}
}
