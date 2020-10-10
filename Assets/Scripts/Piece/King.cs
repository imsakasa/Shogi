using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 王将・玉将
/// </summary>
public class King : IPiece
{
	public List<Address> MoveRanges(Address currentPos)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(currentPos.X, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y - 1));
		ranges.Add(new Address(currentPos.X, currentPos.Y - 1));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y - 1));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y + 1));

		ranges.Where(address => address.IsValid());

		return ranges;
	}
}
