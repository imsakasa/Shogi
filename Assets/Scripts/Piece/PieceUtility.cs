using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PieceUtility
{
	public static List<Address> CalcForeverMoveRange(Address currentPos, IReadOnlyList<Address> defineRanges)
	{
		var ranges = new List<Address>();

		var tmpAddress = currentPos;
		int defineRangeIndex = 0;
		while (true)
		{
			tmpAddress += defineRanges[defineRangeIndex];
			if (!tmpAddress.IsValid())
			{
				defineRangeIndex++;
				if (defineRangeIndex == defineRanges.Count)
				{
					break;
				}
				continue;
			}

			ranges.Add(tmpAddress);
		}

		return ranges;
	}
}
