using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 王将・玉将
/// </summary>
public class King : IPiece
{
	public bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public List<Address> MoveRanges(Board board, Address from)
	{
		var ranges = new List<Address>();
		ranges.Add(new Address(from.X, from.Y - 1));
		ranges.Add(new Address(from.X - 1, from.Y - 1));
		ranges.Add(new Address(from.X - 1, from.Y));
		ranges.Add(new Address(from.X - 1, from.Y + 1));
		ranges.Add(new Address(from.X, from.Y + 1));
		ranges.Add(new Address(from.X + 1, from.Y + 1));
		ranges.Add(new Address(from.X + 1, from.Y));
		ranges.Add(new Address(from.X + 1, from.Y - 1));

		var validRanges = ranges.Where(address => address.IsValid() && board.IsPuttedSelfPiece(address)).ToList();
		return validRanges;
	}
}
