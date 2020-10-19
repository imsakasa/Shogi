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
			if (!tmpAddress.IsValid() || board.GetSquare(tmpAddress).IsSelf())
			{
				defineRangeIndex++;
				if (defineRangeIndex == defineRanges.Count)
				{
					break;
				}
				tmpAddress = from;
				continue;
			}

			ranges.Add(tmpAddress);
		}
		return ranges;
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

	public static IPiece CreatePiece(PieceInfo pieceInfo)
	{
		switch (pieceInfo)
		{
			case PieceInfo.King:
				return new King();
			case PieceInfo.Rook:
				return new Rook();
			case PieceInfo.Bishop:
				return new Bishop();
			case PieceInfo.Gold:
				return new Gold();
			case PieceInfo.Silver:
				return new Silver();
			case PieceInfo.Knight:
				return new Knight();
			case PieceInfo.Lance:
				return new Lance();
			case PieceInfo.Pawn:
				return new Pawn();
			default:
				return null;
		}
	}
}
