using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 角行
/// </summary>
public class Bishop : IPiece
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(1, 1),
		new Address(-1, -1),
		new Address(-1, 1),
		new Address(1, -1),
	};

	public List<Address> MoveRanges(Address currentPos)
	{
		return PieceUtility.CalcForeverMoveRange(currentPos, MOVE_RANGE);
	}
}
