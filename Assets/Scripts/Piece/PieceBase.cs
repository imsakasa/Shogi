using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceBase
{
	public abstract bool CanMove(Board board, PieceMoveInfo moveInfo);
	public abstract List<Address> MoveRanges(Board board, Address from);
}
