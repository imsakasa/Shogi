using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceBase
{
	public abstract bool CanMove(Square[,] board, PieceMoveInfo moveInfo);
	public abstract List<Address> MoveRanges(Square[,] board, Address from);
}
