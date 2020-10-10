using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 銀将
/// </summary>
public class Silver : IPiece
{
	public List<Address> MoveRanges(Address currentPos)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(currentPos.X, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y - 1));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y - 1));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y + 1));

		return ranges;
	}
}
