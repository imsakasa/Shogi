using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 金将
/// </summary>
public class Gold : IPiece
{
	public List<Address> MoveRanges(Address currentPos)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(currentPos.X, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y));
		ranges.Add(new Address(currentPos.X, currentPos.Y - 1));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y + 1));

		return ranges;
	}
}
