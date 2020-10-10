using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 歩兵
/// </summary>
public class Pawn : IPiece
{
	public List<Address> MoveRanges(Address currentPos)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(currentPos.X, currentPos.Y + 1));

		return ranges;
	}
}
