using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PieceUtility
{
	public static List<Address> CalcForeverMoveRange(Board board, Address from, IReadOnlyList<Address> defineRanges)
	{
		var ranges = new List<Address>();

		var tmpAddress = from;
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

		var addresses = RemoveSelfSquare(board, ranges);
		return addresses;
	}

	public static List<Address> RemoveSelfSquare(Board board, List<Address> addresses)
	{
		var removeAddresses = new List<Address>();
		foreach (var address in addresses)
		{
			var square = board.GetSquare(address);
			if (square.IsSelf())
			{
				removeAddresses.Add(address);
			}
		}

		foreach (var address in removeAddresses)
		{
			addresses.Remove(address);
		}

		return addresses;
	} 
}
