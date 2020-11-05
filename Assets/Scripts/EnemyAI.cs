using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
	public void PutPiece(Square[,] board)
	{
		PieceMoveInfo moveInfo = GetMoveInfo(board);

		var moveFromSquare = moveInfo.SelectingSquare;
		var moveToSquare = board[moveInfo.MoveTo.X, moveInfo.MoveTo.Y];

		moveToSquare.SetPieceInfo(moveFromSquare.PieceInfo);
		moveFromSquare.ResetPieceInfo();
	}

	private PieceMoveInfo GetMoveInfo(Square[,] board)
	{
		PieceMoveInfo moveInfo = new PieceMoveInfo();

		Square moveFrom = CalcMoveFromSquare(board);
		Square moveTo = CalcMoveToSquare(board, moveFrom);

		moveInfo.SetMoveFrom(moveFrom);
		moveInfo.SetMoveTo(moveTo.Address);

		return moveInfo;
	}

	private Square CalcMoveFromSquare(Square[,] board)
	{
		return board[1, 3];
	}

	private Square CalcMoveToSquare(Square[,] board, Square from)
	{
		return board[1, 4];
	}
}
