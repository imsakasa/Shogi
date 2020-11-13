using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
	public void PutPiece(Square[,] board)
	{
		BestHandInfo bestHand = ThinkBestHand(board);

		var moveFromSquare = bestHand.MoveInfo.SelectingSquare;
		var moveToSquare = board[bestHand.MoveInfo.MoveTo.X, bestHand.MoveInfo.MoveTo.Y];

		moveToSquare.SetPieceInfo(moveFromSquare.PieceInfo);
		moveFromSquare.ResetPieceInfo();
	}

	private BestHandInfo ThinkBestHand(Square[,] board)
	{
		BestHandInfo bestHandInfo = new BestHandInfo();

		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		{
			for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
			{
				Square square = board[x, y];
				if (!square.IsEnemy()) continue;

				EnemyPieceBase enemyPiece = PieceUtility.CreateEnemyPiece(square.PieceInfo);
				if (enemyPiece == null) continue;

				var bestHand = enemyPiece.GetBestHand(board, square.Address);
				if (bestHand.MaxPieceValue > bestHandInfo.MaxPieceValue)
				{
					bestHandInfo = bestHand;
				}
				else if (bestHand.MaxPieceValue == bestHandInfo.MaxPieceValue)
				{
					if (bestHandInfo.MyPieceValue > bestHand.MyPieceValue)
					{
						bestHandInfo = bestHand;
					}
				}
			}
		}

		return bestHandInfo;
	}
}

public class BestHandInfo
{
	private static readonly int MAX_MY_PIECE_VALUE = 10001;

	public int MaxPieceValue;
	public int MyPieceValue = MAX_MY_PIECE_VALUE;
	public PieceMoveInfo MoveInfo = new PieceMoveInfo();
}
