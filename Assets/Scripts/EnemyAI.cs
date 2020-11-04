using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
	public PieceMoveInfo GetMoveInfo(Square[,] board)
	{
		PieceMoveInfo moveInfo = new PieceMoveInfo();

		Square moveFrom = CalcMoveFromSquare(board);
		Square moveTo = CalcMoveToSquare(board, moveFrom);

		moveInfo.SetMoveFrom(moveFrom);
		moveInfo.SetMoveTo(moveTo.Address);

		return moveInfo;
	}

	public Square CalcMoveFromSquare(Square[,] board)
	{
		return board[1, 3];
	}

	public Square CalcMoveToSquare(Square[,] board, Square from)
	{
		return board[1, 4];
	}
}
