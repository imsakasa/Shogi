using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 飛車
/// </summary>
public class Rook : IPiece
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, 1),
		new Address(1, 0),
		new Address(0, -1),
		new Address(-1, 0),
	};

	public List<Address> MoveRanges(Address currentPos)
	{
		return PieceUtility.CalcForeverMoveRange(currentPos, MOVE_RANGE);
	}
}
