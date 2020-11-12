using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyKnight : EnemyPieceBase
{
	public override List<Address> MoveRanges(Board board, Address from)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(from.X - 1, from.Y + 2));
		ranges.Add(new Address(from.X + 1, from.Y + 2));

		var validRanges = ranges.Where(address => address.IsValid() && !board.IsPuttedEnemyPiece(address)).ToList();
		return validRanges;
	}
}
