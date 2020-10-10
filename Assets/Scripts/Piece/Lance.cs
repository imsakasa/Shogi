using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 香車
/// </summary>
public class Lance : IPiece
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, 1),
	};

	public List<Address> MoveRanges(Address currentPos)
	{
		return PieceUtility.CalcForeverMoveRange(currentPos, MOVE_RANGE);
	}
}
