using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyLance : EnemyPieceBase
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, 1),
	};

	public override List<Address> MoveRanges(Square[,] board, Address from)
	{
		return PieceUtility.CalcForeverMoveRange(board, from, MOVE_RANGE);
	}
}
