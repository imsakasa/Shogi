using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerPieceBase : PieceBase
{
	public abstract bool CanMove(Square[,] board, PieceMoveInfo moveInfo);
}
