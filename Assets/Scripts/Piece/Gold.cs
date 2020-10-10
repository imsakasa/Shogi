using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 金将
/// </summary>
public class Gold : IPiece
{
	public IReadOnlyList<Address> MOVE_RANGE = new List<Address>
	{
		new Address(0, 1),
		new Address(1, 1),
		new Address(1, 0),
		new Address(0, -1),
		new Address(-1, 0),
		new Address(-1, 1),
	};

	public List<Address> MoveRanges(Address currentPos)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(currentPos.X, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y + 1));
		ranges.Add(new Address(currentPos.X + 1, currentPos.Y));
		ranges.Add(new Address(currentPos.X, currentPos.Y - 1));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y));
		ranges.Add(new Address(currentPos.X - 1, currentPos.Y + 1));

		ranges.Where(address => address.IsValid());

		return ranges;
	}
}
