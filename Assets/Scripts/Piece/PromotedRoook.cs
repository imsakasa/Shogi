using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 竜王(成り飛車)
/// </summary>
public class PromotedRoook : IPiece
{
	public bool CanMove(Board board, PieceMoveInfo moveInfo)
	{
		var moveRanges = MoveRanges(board, moveInfo.MoveFrom);
		return moveRanges.Any(address => address == moveInfo.MoveTo);
	}

	public List<Address> MoveRanges(Board board, Address from)
	{
		var rookRanges = PieceUtility.CalcForeverMoveRange(board, from, Rook.MOVE_RANGE);

		var promotedRanges = new List<Address>();
		promotedRanges.Add(new Address(from.X - 1, from.Y - 1));
		promotedRanges.Add(new Address(from.X - 1, from.Y + 1));
		promotedRanges.Add(new Address(from.X + 1, from.Y + 1));
		promotedRanges.Add(new Address(from.X + 1, from.Y - 1));

		rookRanges.AddRange(promotedRanges);
		return rookRanges;
	}
}
