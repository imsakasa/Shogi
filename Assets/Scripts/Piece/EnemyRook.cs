using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyRook : EnemyPieceBase
{
	public static IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, 1),
		new Address(1, 0),
		new Address(0, -1),
		new Address(-1, 0),
	};

	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		return PieceUtility.CalcForeverMoveEnemyRange(board, from, MOVE_RANGE);
	}
}
