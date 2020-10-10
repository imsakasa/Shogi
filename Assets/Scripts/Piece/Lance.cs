using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 香車
/// </summary>
public class Lance : IPiece
{
	public List<Address> MoveRanges(Address currentPos)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(currentPos.X, currentPos.Y + 1));

		return ranges;
	}
}
