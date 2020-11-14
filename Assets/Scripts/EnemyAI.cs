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
		var bestHandInfoList = new List<BestHandInfo>();
		bestHandInfoList.Add(new BestHandInfo());

		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		{
			for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
			{
				Square square = board[x, y];
				if (!square.IsEnemy()) continue;

				EnemyPieceBase enemyPiece = PieceUtility.CreateEnemyPiece(square.PieceInfo);
				if (enemyPiece == null) continue;

				var bestHand = enemyPiece.GetBestHand(board, square.Address);
				if (bestHand.MaxPieceValue > bestHandInfoList[0].MaxPieceValue)
				{
					bestHandInfoList.Clear();
					bestHandInfoList.Add(bestHand);
				}
				else if (bestHand.MaxPieceValue == bestHandInfoList[0].MaxPieceValue)
				{
					bestHandInfoList.Add(bestHand);
				}
			}
		}

		return bestHandInfoList[Random.Range(0, bestHandInfoList.Count)];
	}
}

public class BestHandInfo
{
	public int MaxPieceValue;
	public PieceMoveInfo MoveInfo = new PieceMoveInfo();
}
