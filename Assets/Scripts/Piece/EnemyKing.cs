using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyKing : EnemyPieceBase
{
	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(from.X, from.Y - 1));
		ranges.Add(new Address(from.X - 1, from.Y - 1));
		ranges.Add(new Address(from.X - 1, from.Y));
		ranges.Add(new Address(from.X - 1, from.Y + 1));
		ranges.Add(new Address(from.X, from.Y + 1));
		ranges.Add(new Address(from.X + 1, from.Y + 1));
		ranges.Add(new Address(from.X + 1, from.Y));
		ranges.Add(new Address(from.X + 1, from.Y - 1));

		var validRanges = ranges.Where(pos => pos.IsValid() && !board[pos.X, pos.Y].IsEnemy()).ToList();
		return validRanges;
	}

	public override int GetPieceValue()
	{
		return PieceDefine.PieceValue.Gold;
	}
}
