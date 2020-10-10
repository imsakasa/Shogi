using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 桂馬
/// </summary>
public class Knight : IPiece
{
	public List<Address> MoveRanges(Address currentPos)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y + 2));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y + 1));

		return ranges;
	}
}
